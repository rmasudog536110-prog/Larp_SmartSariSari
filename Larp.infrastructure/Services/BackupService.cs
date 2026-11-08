using System.Data;
using System.Globalization;
using System.IO.Compression;
using System.Text.Json;
using Larp.infrastructure.Data;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Larp.infrastructure.Services;

public record RestoreResult(int Tables, int Rows, string SafetyBackup);

public class BackupService
{
    private static readonly HashSet<string> AllowedReasons = new() { "manual", "pre-restore" };
    private const string ManifestName = "manifest.json";

    private readonly ITenantDBContextFactory _factory;
    private readonly IBackupStorage _storage;

    public BackupService(ITenantDBContextFactory factory, IBackupStorage storage)
    {
        _factory = factory;
        _storage = storage;
    }

    // ---------------------------------------------------------------- BACKUP

    public async Task<BackupInfo> CreateBackupAsync(int companyId, string reason = "manual")
    {
        if (!AllowedReasons.Contains(reason))
            throw new InvalidOperationException("Invalid backup reason.");

        await using var ctx = await _factory.CreateAsync(companyId);
        var tables = GetOrderedTables(ctx);
        var conn = (SqlConnection)ctx.Database.GetDbConnection();
        await ctx.Database.OpenConnectionAsync();

        var ms = new MemoryStream();
        var counts = new List<object>();

        using (var zip = new ZipArchive(ms, ZipArchiveMode.Create, leaveOpen: true))
        {
            foreach (var (key, quoted) in tables)
            {
                var entry = zip.CreateEntry($"data/{key}.json", CompressionLevel.Optimal);
                int rows = 0;

                await using (var es = entry.Open())
                await using (var writer = new Utf8JsonWriter(es))
                {
                    writer.WriteStartArray();

                    await using var cmd = new SqlCommand($"SELECT * FROM {quoted}", conn);
                    await using var reader = await cmd.ExecuteReaderAsync();
                    while (await reader.ReadAsync())
                    {
                        writer.WriteStartObject();
                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            writer.WritePropertyName(reader.GetName(i));
                            WriteValue(writer, reader.GetValue(i));
                        }
                        writer.WriteEndObject();
                        rows++;
                    }

                    writer.WriteEndArray();
                    await writer.FlushAsync();
                }

                counts.Add(new { name = key, rows });
            }

            var manifest = zip.CreateEntry(ManifestName);
            await using var mstream = manifest.Open();
            await JsonSerializer.SerializeAsync(mstream, new
            {
                companyId,
                createdUtc = DateTime.UtcNow,
                reason,
                tables = counts
            });
        }

        var fileName = $"{DateTime.UtcNow:yyyyMMdd-HHmmss}-{reason}.zip";
        await _storage.SaveAsync(companyId, fileName, ms);
        return new BackupInfo(fileName, ms.Length, DateTime.UtcNow);
    }

    public Task<IReadOnlyList<BackupInfo>> ListAsync(int companyId) => _storage.ListAsync(companyId);

    // --------------------------------------------------------------- RESTORE

    public async Task<RestoreResult> RestoreAsync(int companyId, string fileName)
    {
        // 1. Load the zip fully and check it belongs to this company.
        var buffer = new MemoryStream();
        await using (var src = await _storage.OpenReadAsync(companyId, fileName))
            await src.CopyToAsync(buffer);
        buffer.Position = 0;

        using var zip = new ZipArchive(buffer, ZipArchiveMode.Read);
        var manifestEntry = zip.GetEntry(ManifestName)
            ?? throw new InvalidOperationException("Backup is missing its manifest.");

        using (var mdoc = await JsonDocument.ParseAsync(manifestEntry.Open()))
        {
            if (!mdoc.RootElement.TryGetProperty("companyId", out var cid) || cid.GetInt32() != companyId)
                throw new InvalidOperationException("This backup belongs to a different company.");
        }

        // 2. Safety backup of the current data before wiping.
        var safety = await CreateBackupAsync(companyId, "pre-restore");

        // 3. Wipe and reload in one transaction.
        await using var ctx = await _factory.CreateAsync(companyId);
        var tables = GetOrderedTables(ctx);
        var conn = (SqlConnection)ctx.Database.GetDbConnection();
        await ctx.Database.OpenConnectionAsync();

        await using var tx = (SqlTransaction)await conn.BeginTransactionAsync();
        int totalRows = 0;
        try
        {
            // children first
            foreach (var (_, quoted) in Enumerable.Reverse(tables))
            {
                await using var del = new SqlCommand($"DELETE FROM {quoted}", conn, tx);
                await del.ExecuteNonQueryAsync();
            }

            // parents first
            foreach (var (key, quoted) in tables)
            {
                var entry = zip.GetEntry($"data/{key}.json");
                if (entry == null) continue; // table not in backup: stays empty

                using var doc = await JsonDocument.ParseAsync(entry.Open());

                // Empty DataTable with the real column types
                var dt = new DataTable();
                using (var sel = new SqlCommand($"SELECT * FROM {quoted} WHERE 1 = 0", conn, tx))
                using (var adapter = new SqlDataAdapter(sel))
                    adapter.FillSchema(dt, SchemaType.Source);

                var usable = dt.Columns.Cast<DataColumn>()
                    .Where(c => !c.ReadOnly || c.AutoIncrement)
                    .ToList();

                foreach (var row in doc.RootElement.EnumerateArray())
                {
                    var dr = dt.NewRow();
                    foreach (var col in usable)
                    {
                        dr[col] = row.TryGetProperty(col.ColumnName, out var val)
                            ? FromJson(val, col.DataType)
                            : DBNull.Value;
                    }
                    dt.Rows.Add(dr);
                }

                if (dt.Rows.Count == 0) continue;

                using var bulk = new SqlBulkCopy(conn, SqlBulkCopyOptions.KeepIdentity, tx)
                {
                    DestinationTableName = quoted,
                    BulkCopyTimeout = 120
                };
                foreach (var col in usable)
                    bulk.ColumnMappings.Add(col.ColumnName, col.ColumnName);

                await bulk.WriteToServerAsync(dt);
                totalRows += dt.Rows.Count;
            }

            await tx.CommitAsync();
        }
        catch (Exception ex)
        {
            await tx.RollbackAsync();
            throw new InvalidOperationException($"Restore failed and was rolled back: {ex.Message}");
        }

        return new RestoreResult(tables.Count, totalRows, safety.FileName);
    }

    // --------------------------------------------------------------- HELPERS

    /// <summary>Tables in FK-safe order (parents before children), as (key, quotedName).</summary>
    private static List<(string Key, string Quoted)> GetOrderedTables(DbContext ctx)
    {
        var deps = new Dictionary<string, HashSet<string>>();
        var names = new Dictionary<string, (string Schema, string Table)>();

        string SchemaOf(IEntityType t) => t.GetSchema() ?? t.Model.GetDefaultSchema() ?? "dbo";

        foreach (var t in ctx.Model.GetEntityTypes())
        {
            var table = t.GetTableName();
            if (table == null) continue;

            var key = $"{SchemaOf(t)}.{table}";
            names[key] = (SchemaOf(t), table);
            if (!deps.TryGetValue(key, out var set))
                deps[key] = set = new HashSet<string>();

            foreach (var fk in t.GetForeignKeys())
            {
                var pTable = fk.PrincipalEntityType.GetTableName();
                if (pTable == null) continue;
                var pKey = $"{SchemaOf(fk.PrincipalEntityType)}.{pTable}";
                if (pKey != key) set.Add(pKey);
            }
        }

        var result = new List<(string, string)>();
        var visited = new HashSet<string>();

        void Visit(string key)
        {
            if (!visited.Add(key)) return;
            if (deps.TryGetValue(key, out var parents))
                foreach (var p in parents.Where(names.ContainsKey)) Visit(p);
            var (schema, table) = names[key];
            result.Add((key, $"[{schema}].[{table}]"));
        }

        foreach (var key in names.Keys.OrderBy(k => k)) Visit(key);
        return result;
    }

    private static void WriteValue(Utf8JsonWriter w, object v)
    {
        switch (v)
        {
            case DBNull: w.WriteNullValue(); break;
            case string s: w.WriteStringValue(s); break;
            case bool b: w.WriteBooleanValue(b); break;
            case byte or short or int: w.WriteNumberValue(Convert.ToInt32(v)); break;
            case long l: w.WriteNumberValue(l); break;
            case decimal d: w.WriteNumberValue(d); break;
            case double d: w.WriteNumberValue(d); break;
            case float f: w.WriteNumberValue(f); break;
            case DateTime dt: w.WriteStringValue(dt.ToString("O", CultureInfo.InvariantCulture)); break;
            case DateTimeOffset dto: w.WriteStringValue(dto.ToString("O", CultureInfo.InvariantCulture)); break;
            case Guid g: w.WriteStringValue(g); break;
            case TimeSpan ts: w.WriteStringValue(ts.ToString("c", CultureInfo.InvariantCulture)); break;
            case byte[] bytes: w.WriteBase64StringValue(bytes); break;
            default: w.WriteStringValue(Convert.ToString(v, CultureInfo.InvariantCulture)); break;
        }
    }

    private static object FromJson(JsonElement e, Type t)
    {
        if (e.ValueKind == JsonValueKind.Null) return DBNull.Value;
        if (t == typeof(string)) return e.GetString()!;
        if (t == typeof(bool)) return e.GetBoolean();
        if (t == typeof(byte[])) return e.GetBytesFromBase64();
        if (t == typeof(DateTime)) return e.GetDateTime();
        if (t == typeof(DateTimeOffset)) return e.GetDateTimeOffset();
        if (t == typeof(Guid)) return e.GetGuid();
        if (t == typeof(TimeSpan)) return TimeSpan.ParseExact(e.GetString()!, "c", CultureInfo.InvariantCulture);
        if (t == typeof(decimal)) return e.GetDecimal();
        return Convert.ChangeType(e.GetRawText(), t, CultureInfo.InvariantCulture);
    }
}