using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Larp.winforms.Admin
{
    // Backup & Restore for each company (SuperAdmin only, enforced by the API).
    public partial class Backup : Form
    {
        private readonly HttpClient _httpClient;

        public Backup(HttpClient httpClient)
        {
            InitializeComponent();
            _httpClient = httpClient;

            AppTheme.FormatGrid(gridBackups);

            // Reload the list whenever another company is picked.
            cmbBackupTenant.SelectedIndexChanged += async (s, e) => await LoadBackupsAsync();

            Load += async (s, e) => await LoadTenantsAsync();
        }

        // ---------------- Helpers ----------------

        private static async Task<string> ReadMessageAsync(HttpResponseMessage r)
        {
            try
            {
                var body = await r.Content.ReadFromJsonAsync<JsonElement>();
                if (body.TryGetProperty("message", out var m) && m.GetString() is string s) return s;
            }
            catch { }
            return $"Request failed ({(int)r.StatusCode}).";
        }

        // Backup/restore can take minutes on a remote database, so they use a
        // separate client with a 5 minute timeout. It copies the shared client's
        // address and login token; the shared client is left untouched.
        private HttpClient CreateLongClient()
        {
            var client = new HttpClient
            {
                BaseAddress = _httpClient.BaseAddress,
                Timeout = TimeSpan.FromMinutes(5)
            };
            client.DefaultRequestHeaders.Authorization = _httpClient.DefaultRequestHeaders.Authorization;
            return client;
        }

        private void SetStatus(string text, bool isError)
        {
            lblBackupStatus.ForeColor = isError ? Color.Firebrick : Color.FromArgb(46, 107, 69);
            lblBackupStatus.Text = text;
        }

        private void SetBusy(bool busy, string? message = null)
        {
            btnBackupNow.Enabled = !busy;
            btnRestoreSelected.Enabled = !busy;
            cmbBackupTenant.Enabled = !busy;
            Cursor = busy ? Cursors.WaitCursor : Cursors.Default;

            if (busy && message != null)
            {
                lblBackupStatus.ForeColor = Color.FromArgb(28, 40, 63);
                lblBackupStatus.Text = message;
            }
        }

        private static string FormatSize(long bytes)
        {
            if (bytes >= 1024 * 1024) return $"{bytes / 1024.0 / 1024.0:0.0} MB";
            return $"{Math.Max(1, bytes / 1024)} KB";
        }

        // ---------------- Load ----------------

        private async Task LoadTenantsAsync()
        {
            try
            {
                var tenants = await _httpClient.GetFromJsonAsync<List<TenantRow>>("admin/tenants")
                              ?? new List<TenantRow>();

                cmbBackupTenant.Items.Clear();
                foreach (var t in tenants)
                    cmbBackupTenant.Items.Add(new TenantChoice(t.TenantId, t.Name));

                if (cmbBackupTenant.Items.Count > 0)
                    cmbBackupTenant.SelectedIndex = 0; // triggers LoadBackupsAsync
            }
            catch (Exception ex)
            {
                SetStatus("Could not load companies: " + ex.Message, true);
            }
        }

        private async Task LoadBackupsAsync()
        {
            if (cmbBackupTenant.SelectedItem is not TenantChoice tenant)
            {
                gridBackups.DataSource = null;
                return;
            }

            try
            {
                var rows = await _httpClient.GetFromJsonAsync<List<BackupRow>>(
                               $"admin/tenants/{tenant.Id}/backups")
                           ?? new List<BackupRow>();

                gridBackups.DataSource = rows.Select(b => new
                {
                    File = b.FileName,
                    Type = b.FileName.EndsWith("-pre-restore.zip", StringComparison.OrdinalIgnoreCase)
                        ? "Safety (before a restore)"
                        : "Manual",
                    Size = FormatSize(b.SizeBytes),
                    Created = b.CreatedUtc.ToLocalTime().ToString("yyyy-MM-dd HH:mm:ss")
                }).ToList();

                AppTheme.AlignRight(gridBackups, "Size");
            }
            catch (Exception ex)
            {
                SetStatus("Could not load backups: " + ex.Message, true);
            }
        }

        // ---------------- Buttons ----------------

        private async void BtnBackupNow_Click(object sender, EventArgs e)
        {
            if (cmbBackupTenant.SelectedItem is not TenantChoice tenant)
            {
                MessageBox.Show("Select a company first.");
                return;
            }

            SetBusy(true, $"Backing up {tenant.Name}...");
            try
            {
                using var client = CreateLongClient();
                var response = await client.PostAsync($"admin/tenants/{tenant.Id}/backup", null);

                if (!response.IsSuccessStatusCode)
                {
                    SetStatus(await ReadMessageAsync(response), true);
                    return;
                }

                SetStatus("Backup created.", false);
                await LoadBackupsAsync();
            }
            catch (Exception ex)
            {
                SetStatus("Backup failed: " + ex.Message, true);
            }
            finally
            {
                SetBusy(false);
            }
        }

        private async void BtnRestoreSelected_Click(object sender, EventArgs e)
        {
            if (cmbBackupTenant.SelectedItem is not TenantChoice tenant)
            {
                MessageBox.Show("Select a company first.");
                return;
            }

            if (gridBackups.SelectedRows.Count == 0)
            {
                MessageBox.Show("Select a backup from the list first.");
                return;
            }

            var fileName = (string)gridBackups.SelectedRows[0].Cells["File"].Value;

            using var dialog = new RestoreConfirmDialog(tenant.Name, fileName);
            if (dialog.ShowDialog() != DialogResult.OK) return;

            SetBusy(true, $"Restoring {tenant.Name}... this can take a few minutes. Do not close the app.");
            try
            {
                using var client = CreateLongClient();
                var response = await client.PostAsJsonAsync(
                    $"admin/tenants/{tenant.Id}/restore",
                    new { fileName, confirmCompanyName = dialog.TypedName });

                if (!response.IsSuccessStatusCode)
                {
                    SetStatus(await ReadMessageAsync(response), true);
                    return;
                }

                var body = await response.Content.ReadFromJsonAsync<JsonElement>();
                var rows = body.TryGetProperty("rows", out var r) ? r.GetInt32() : 0;
                var safety = body.TryGetProperty("safetyBackup", out var sb) ? sb.GetString() : null;

                SetStatus($"Restore completed ({rows} rows). Previous data saved as {safety}.", false);
                MessageBox.Show(
                    $"Restore completed.\n\nRows restored: {rows}\nThe data from before the restore was saved as:\n{safety}",
                    "Restore");
                await LoadBackupsAsync();
            }
            catch (TaskCanceledException)
            {
                SetStatus("The request timed out. The restore may still be running on the server. " +
                          "Wait a minute, then refresh the list and check the data.", true);
            }
            catch (Exception ex)
            {
                SetStatus("Restore failed: " + ex.Message, true);
            }
            finally
            {
                SetBusy(false);
            }
        }

        // ---------------- DTOs ----------------

        private class TenantRow
        {
            public int TenantId { get; set; }
            public string Name { get; set; } = "";
        }

        private class BackupRow
        {
            public string FileName { get; set; } = "";
            public long SizeBytes { get; set; }
            public DateTime CreatedUtc { get; set; }
        }

        // Item shown in the company drop-down.
        private class TenantChoice
        {
            public int Id { get; }
            public string Name { get; }
            public TenantChoice(int id, string name) { Id = id; Name = name; }
            public override string ToString() => Name;
        }
    }

    // Restore confirmation: the user must type the company name to continue.
    public class RestoreConfirmDialog : Form
    {
        private readonly TextBox txtConfirm = new();
        private readonly Button ok = new();
        private readonly string _companyName;

        public string TypedName => txtConfirm.Text.Trim();

        public RestoreConfirmDialog(string companyName, string fileName)
        {
            _companyName = companyName;

            Text = "Confirm Restore";
            StartPosition = FormStartPosition.CenterParent;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = MinimizeBox = false;
            ClientSize = new Size(460, 250);

            var warning = new Label
            {
                Text = $"This will REPLACE all current data of \"{companyName}\" with the backup:\n\n{fileName}\n\n" +
                       "A safety backup of the current data is taken first. " +
                       "To continue, type the company name below.",
                Location = new Point(20, 15),
                MaximumSize = new Size(420, 0),
                AutoSize = true
            };

            txtConfirm.Location = new Point(20, 150);
            txtConfirm.Width = 420;

            ok.Text = "Restore";
            ok.Location = new Point(270, 195);
            ok.Size = new Size(80, 32);
            ok.Enabled = false;
            ok.DialogResult = DialogResult.OK;

            var cancel = new Button
            {
                Text = "Cancel",
                Location = new Point(360, 195),
                Size = new Size(80, 32),
                DialogResult = DialogResult.Cancel
            };

            // Restore stays disabled until the typed name matches.
            txtConfirm.TextChanged += (s, e) =>
                ok.Enabled = string.Equals(TypedName, _companyName.Trim(), StringComparison.OrdinalIgnoreCase);

            Controls.Add(warning);
            Controls.Add(txtConfirm);
            Controls.Add(ok);
            Controls.Add(cancel);
            CancelButton = cancel;
        }
    }
}