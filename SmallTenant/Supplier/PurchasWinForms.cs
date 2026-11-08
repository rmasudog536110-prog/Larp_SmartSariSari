using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Larp.winforms.SmallTenant.Supplier
{
    public partial class PurchasesWinForms : Form
    {
        private readonly HttpClient _httpClient;
        private readonly int _companyId;

        private List<PurchaseRow> _all = new();

        public PurchasesWinForms(HttpClient httpClient, int companyId)
        {
            InitializeComponent();

            _httpClient = httpClient;
            _companyId = companyId;

            AppTheme.Apply(this);

            ConfigureGrid();

            Shown += async (_, _) =>
            {
                await LoadPurchasesAsync();
            };
        }

        private void ConfigureGrid()
        {
            guna2DataGridView1.AutoGenerateColumns = false;
            guna2DataGridView1.AllowUserToAddRows = false;
            guna2DataGridView1.AllowUserToDeleteRows = false;
            guna2DataGridView1.ReadOnly = true;
            guna2DataGridView1.SelectionMode =
                DataGridViewSelectionMode.FullRowSelect;
            guna2DataGridView1.MultiSelect = false;

            guna2DataGridView1.Columns.Clear();

            guna2DataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "PurchaseNumber",
                HeaderText = "Purchase No.",
                DataPropertyName = "PurchaseNumber",
                Width = 220
            });

            guna2DataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "SupplierName",
                HeaderText = "Supplier",
                DataPropertyName = "SupplierName",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            });

            guna2DataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "PurchaseDate",
                HeaderText = "Date",
                DataPropertyName = "PurchaseDate",
                Width = 170,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Format = "MMM dd, yyyy hh:mm tt"
                }
            });

            guna2DataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "ItemCount",
                HeaderText = "Items",
                DataPropertyName = "ItemCount",
                Width = 80,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Alignment = DataGridViewContentAlignment.MiddleRight
                }
            });

            guna2DataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "TotalAmount",
                HeaderText = "Total",
                DataPropertyName = "TotalAmount",
                Width = 150,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Format = "₱#,##0.00",
                    Alignment = DataGridViewContentAlignment.MiddleRight
                }
            });

            guna2DataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Status",
                HeaderText = "Status",
                DataPropertyName = "Status",
                Width = 110
            });

            guna2DataGridView1.Columns.Add(new DataGridViewButtonColumn
            {
                Name = "Cancel",
                HeaderText = "",
                Text = "Cancel",
                UseColumnTextForButtonValue = true,
                Width = 90
            });
        }

        private async Task LoadPurchasesAsync()
        {
            try
            {
                guna2DataGridView1.Enabled = false;
                guna2ProgressIndicator1.Visible = true;

                var response = await _httpClient.GetAsync(
                    $"tenant/{_companyId}/purchases");

                if (!response.IsSuccessStatusCode)
                {
                    MessageBox.Show(
                        await ReadMessageAsync(response),
                        "Purchases",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);

                    return;
                }

                var purchases =
                    await response.Content.ReadFromJsonAsync<List<PurchaseApi>>()
                    ?? new List<PurchaseApi>();

                _all = purchases
                    .Select(p => new PurchaseRow
                    {
                        PurchaseId = p.PurchaseId,
                        PurchaseNumber = p.PurchaseNumber,
                        SupplierName = p.SupplierName,
                        PurchaseDate = DateTime
                            .SpecifyKind(p.PurchaseDate, DateTimeKind.Utc)
                            .ToLocalTime(),
                        ItemCount = p.Items.Count,
                        TotalAmount = p.TotalAmount,
                        Status = p.Status
                    })
                    .ToList();

                ApplyFilter();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Unable to load purchases.\n\n{ex.Message}",
                    "Purchases",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            finally
            {
                guna2ProgressIndicator1.Visible = false;
                guna2DataGridView1.Enabled = true;
            }
        }

        private void ApplyFilter()
        {
            string search = guna2TextBox1.Text.Trim();

            var rows = string.IsNullOrEmpty(search)
                ? _all.ToList()
                : _all.Where(x =>
                    x.PurchaseNumber.Contains(
                        search, StringComparison.OrdinalIgnoreCase) ||
                    x.SupplierName.Contains(
                        search, StringComparison.OrdinalIgnoreCase) ||
                    x.Status.Contains(
                        search, StringComparison.OrdinalIgnoreCase))
                    .ToList();

            guna2DataGridView1.DataSource = null;
            guna2DataGridView1.DataSource = rows;

            UpdateStats();
        }

        private void UpdateStats()
        {
            var completed = _all.Where(x => x.Status == "Completed").ToList();

            lblTotalPurchases.Text = _all.Count.ToString();
            lblCompletedPurchases.Text = completed.Count.ToString();
            lblCancelledPurchases.Text =
                _all.Count(x => x.Status == "Cancelled").ToString();
            lblTotalValue.Text =
                $"₱{completed.Sum(x => x.TotalAmount):N2}";
        }

        // Grey out cancelled rows and colour the status text.
        private void guna2DataGridView1_DataBindingComplete(
            object? sender,
            DataGridViewBindingCompleteEventArgs e)
        {
            foreach (DataGridViewRow row in guna2DataGridView1.Rows)
            {
                if (row.DataBoundItem is not PurchaseRow purchase)
                    continue;

                bool cancelled = purchase.Status == "Cancelled";

                row.DefaultCellStyle.ForeColor = cancelled
                    ? AppTheme.SlateLight
                    : AppTheme.TextPrimary;

                var statusStyle = row.Cells["Status"].Style;

                statusStyle.ForeColor = cancelled
                    ? AppTheme.Danger
                    : AppTheme.Success;

                statusStyle.Font = new System.Drawing.Font(
                    "Segoe UI",
                    9F,
                    System.Drawing.FontStyle.Bold);
            }
        }

        private void guna2TextBox1_TextChanged(object? sender, EventArgs e)
        {
            ApplyFilter();
        }

        private async void btnRefresh_Click(object? sender, EventArgs e)
        {
            guna2TextBox1.Clear();
            await LoadPurchasesAsync();
        }

        private async void btnNewPurchase_Click(object? sender, EventArgs e)
        {
            using var form = new Procurement(_httpClient, _companyId);

            form.ShowDialog(this);

            await LoadPurchasesAsync();
        }

        private void btnClose_Click(object? sender, EventArgs e)
        {
            Close();
        }

        private async void guna2DataGridView1_CellContentClick(
            object? sender,
            DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
                return;

            if (guna2DataGridView1.Columns[e.ColumnIndex].Name != "Cancel")
                return;

            if (guna2DataGridView1.Rows[e.RowIndex].DataBoundItem
                is not PurchaseRow purchase)
                return;

            if (purchase.Status == "Cancelled")
            {
                MessageBox.Show(
                    "This purchase is already cancelled.",
                    "Purchases",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                return;
            }

            var confirm = MessageBox.Show(
                $"Cancel purchase {purchase.PurchaseNumber}?\n\n" +
                "The stock it added will be removed from inventory.",
                "Cancel Purchase",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirm != DialogResult.Yes)
                return;

            try
            {
                guna2DataGridView1.Enabled = false;

                var response = await _httpClient.PutAsync(
                    $"tenant/{_companyId}/purchases/{purchase.PurchaseId}/cancel",
                    null);

                if (!response.IsSuccessStatusCode)
                {
                    MessageBox.Show(
                        await ReadMessageAsync(response),
                        "Cannot cancel purchase",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Unable to cancel purchase.\n\n{ex.Message}",
                    "Purchases",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                return;
            }
            finally
            {
                guna2DataGridView1.Enabled = true;
            }

            await LoadPurchasesAsync();
        }

        private static async Task<string> ReadMessageAsync(
            HttpResponseMessage response)
        {
            try
            {
                var body = await response.Content
                    .ReadFromJsonAsync<JsonElement>();

                if (body.TryGetProperty("message", out var m) &&
                    m.GetString() is string s)
                {
                    return s;
                }
            }
            catch
            {
            }

            return $"Request failed ({(int)response.StatusCode}).";
        }

        // ---------------- DTOs ----------------

        private class PurchaseApi
        {
            public int PurchaseId { get; set; }
            public string PurchaseNumber { get; set; } = string.Empty;
            public string SupplierName { get; set; } = string.Empty;
            public DateTime PurchaseDate { get; set; }
            public decimal TotalAmount { get; set; }
            public string Status { get; set; } = string.Empty;
            public List<JsonElement> Items { get; set; } = new();
        }

        private class PurchaseRow
        {
            public int PurchaseId { get; set; }
            public string PurchaseNumber { get; set; } = string.Empty;
            public string SupplierName { get; set; } = string.Empty;
            public DateTime PurchaseDate { get; set; }
            public int ItemCount { get; set; }
            public decimal TotalAmount { get; set; }
            public string Status { get; set; } = string.Empty;
        }

    }
}