using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Larp.winforms.SmallTenant.Approvals
{
    // Admin inbox: new employees, new suppliers and payroll waiting for approval.
    public partial class ApprovalsWinForms : Form
    {
        private readonly HttpClient _httpClient;
        private readonly int _companyId;

        public ApprovalsWinForms(HttpClient httpClient, int companyId)
        {
            InitializeComponent();

            _httpClient = httpClient;
            _companyId = companyId;

            AppTheme.Apply(this);

            ConfigureGrid();

            Shown += async (_, _) =>
            {
                await LoadItemsAsync();
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
                Name = "Type",
                HeaderText = "Type",
                DataPropertyName = "Type",
                Width = 100
            });

            guna2DataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Title",
                HeaderText = "Item",
                DataPropertyName = "Title",
                Width = 220
            });

            guna2DataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Details",
                HeaderText = "Details",
                DataPropertyName = "Details",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            });

            guna2DataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Requested",
                HeaderText = "Requested",
                DataPropertyName = "Requested",
                Width = 150
            });

            guna2DataGridView1.Columns.Add(new DataGridViewButtonColumn
            {
                Name = "Approve",
                HeaderText = "",
                Text = "Approve",
                UseColumnTextForButtonValue = true,
                Width = 90
            });

            guna2DataGridView1.Columns.Add(new DataGridViewButtonColumn
            {
                Name = "Reject",
                HeaderText = "",
                Text = "Reject",
                UseColumnTextForButtonValue = true,
                Width = 90
            });
        }

        private async Task LoadItemsAsync()
        {
            try
            {
                guna2DataGridView1.Enabled = false;
                guna2ProgressIndicator1.Visible = true;

                var response = await _httpClient.GetAsync(
                    $"tenant/{_companyId}/approvals");

                if (!response.IsSuccessStatusCode)
                {
                    MessageBox.Show(
                        await ReadMessageAsync(response),
                        "Approvals",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);

                    return;
                }

                var items = await response.Content.ReadFromJsonAsync<List<ApprovalRow>>()
                            ?? new List<ApprovalRow>();

                guna2DataGridView1.DataSource = null;
                guna2DataGridView1.DataSource = items;

                lblSubtitle.Text = items.Count == 0
                    ? "Nothing is waiting for approval."
                    : $"{items.Count} item(s) waiting for approval.";
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Unable to load approvals.\n\n{ex.Message}",
                    "Approvals",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            finally
            {
                guna2ProgressIndicator1.Visible = false;
                guna2DataGridView1.Enabled = true;
            }
        }

        private async void btnRefresh_Click(object? sender, EventArgs e)
        {
            await LoadItemsAsync();
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

            if (guna2DataGridView1.Rows[e.RowIndex].DataBoundItem is not ApprovalRow item)
                return;

            string column = guna2DataGridView1.Columns[e.ColumnIndex].Name;

            if (column != "Approve" && column != "Reject")
                return;

            bool approve = column == "Approve";

            var confirm = MessageBox.Show(
                approve
                    ? $"Approve {item.Type.ToLowerInvariant()} \"{item.Title}\"?"
                    : $"Reject {item.Type.ToLowerInvariant()} \"{item.Title}\"?",
                approve ? "Approve" : "Reject",
                MessageBoxButtons.YesNo,
                approve ? MessageBoxIcon.Question : MessageBoxIcon.Warning);

            if (confirm != DialogResult.Yes)
                return;

            try
            {
                guna2DataGridView1.Enabled = false;

                var response = await _httpClient.PostAsync(
                    $"tenant/{_companyId}/approvals/{item.Type.ToLowerInvariant()}/{item.ItemId}/" +
                    (approve ? "approve" : "reject"),
                    null);

                if (!response.IsSuccessStatusCode)
                {
                    MessageBox.Show(
                        await ReadMessageAsync(response),
                        "Approvals",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Unable to save the decision.\n\n{ex.Message}",
                    "Approvals",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            finally
            {
                guna2DataGridView1.Enabled = true;
            }

            await LoadItemsAsync();
        }

        private static async Task<string> ReadMessageAsync(HttpResponseMessage response)
        {
            try
            {
                var body = await response.Content.ReadFromJsonAsync<JsonElement>();

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

        private class ApprovalRow
        {
            public string Type { get; set; } = string.Empty;
            public int ItemId { get; set; }
            public string Title { get; set; } = string.Empty;
            public string Details { get; set; } = string.Empty;
            public DateTime RequestedAt { get; set; }

            // Shown in the grid (local time).
            public string Requested =>
                RequestedAt.ToLocalTime().ToString("MMM dd, yyyy hh:mm tt");
        }
    }
}