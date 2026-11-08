using Microsoft.Extensions.Configuration;

namespace Larp.infrastructure.Services;

public record BackupInfo(string FileName, long SizeBytes, DateTime CreatedUtc);

public interface IBackupStorage
{
    Task SaveAsync(int companyId, string fileName, Stream content);
    Task<IReadOnlyList<BackupInfo>> ListAsync(int companyId);
    Task<Stream> OpenReadAsync(int companyId, string fileName);
}

/// <summary>
/// Demo storage: saves zips under {Backup:LocalPath}/{companyId}/.
/// Swap for an Azure Blob implementation of IBackupStorage later.
/// </summary>
public class LocalBackupStorage : IBackupStorage
{
    private readonly string _root;

    public LocalBackupStorage(IConfiguration config)
    {
        var path = config["Backup:LocalPath"] ?? "Backups";
        _root = Path.IsPathRooted(path)
            ? path
            : Path.Combine(Directory.GetCurrentDirectory(), path);
    }

    private string CompanyFolder(int companyId) => Path.Combine(_root, companyId.ToString());

    private static void ValidateFileName(string fileName)
    {
        if (string.IsNullOrWhiteSpace(fileName)
            || Path.GetFileName(fileName) != fileName
            || !fileName.EndsWith(".zip", StringComparison.OrdinalIgnoreCase))
            throw new InvalidOperationException("Invalid backup file name.");
    }

    public async Task SaveAsync(int companyId, string fileName, Stream content)
    {
        ValidateFileName(fileName);
        var folder = CompanyFolder(companyId);
        Directory.CreateDirectory(folder);

        await using var file = File.Create(Path.Combine(folder, fileName));
        content.Position = 0;
        await content.CopyToAsync(file);
    }

    public Task<IReadOnlyList<BackupInfo>> ListAsync(int companyId)
    {
        var folder = CompanyFolder(companyId);
        if (!Directory.Exists(folder))
            return Task.FromResult<IReadOnlyList<BackupInfo>>(Array.Empty<BackupInfo>());

        IReadOnlyList<BackupInfo> list = new DirectoryInfo(folder)
            .GetFiles("*.zip")
            .OrderByDescending(f => f.CreationTimeUtc)
            .Select(f => new BackupInfo(f.Name, f.Length, f.CreationTimeUtc))
            .ToList();
        return Task.FromResult(list);
    }

    public Task<Stream> OpenReadAsync(int companyId, string fileName)
    {
        ValidateFileName(fileName);
        var path = Path.Combine(CompanyFolder(companyId), fileName);
        if (!File.Exists(path))
            throw new InvalidOperationException("Backup not found.");
        return Task.FromResult<Stream>(File.OpenRead(path));
    }
}