using Larp.domain.DTO;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using Larp.winforms;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Larp.winforms.Admin
{
    public partial class Subscription : Form
    {
        private readonly HttpClient _httpClient;

        public Subscription(HttpClient httpClient)
        {
            InitializeComponent();

            _httpClient = httpClient;

            WindowState = FormWindowState.Maximized;

            AppTheme.FormatGrid(gridPlans);
            AppTheme.FormatGrid(gridSubscriptions);

            ConfigurePlanGrid();
            ConfigureSubscriptionGrid();

            Resize += (s, e) =>
            {
                int margin = 30;
                int titleHeight = 46;
                int planTitleHeight = 30;
                int planHeight = 190;
                int gap = 20;
                int buttonHeight = 40;
                int bottomMargin = 25;

                lblSubscriptionsTitle.Location = new Point(margin, 20);

                lblPlansTitle.Location =
                    new Point(margin, 78);

                gridPlans.Location =
                    new Point(margin, 115);

                gridPlans.Width =
                    ClientSize.Width - (margin * 2);

                gridPlans.Height = planHeight;

                int subscriptionTop =
                    gridPlans.Bottom + gap + 20;

                gridSubscriptions.Location =
                    new Point(margin, subscriptionTop);

                gridSubscriptions.Width =
                    ClientSize.Width - (margin * 2);

                gridSubscriptions.Height =
                    ClientSize.Height -
                    subscriptionTop -
                    buttonHeight -
                    bottomMargin -
                    20;

                int buttonY =
                    ClientSize.Height - buttonHeight - bottomMargin;

                btnSuspendTenant.Location =
                    new Point(30, buttonY);

                btnReactivateTenant.Location =
                    new Point(200, buttonY);

                btnNewCompany.Location =
                    new Point(370, buttonY);

                btnUpdateDatabase.Location =
                    new Point(540, buttonY);
            };

            Load += async (s, e) =>
            {
                await LoadPlansAsync();
                await LoadSubscriptionsAsync();
            };
        }

        private void ConfigurePlanGrid()
        {
            gridPlans.Columns.Clear();

            gridPlans.ColumnHeadersDefaultCellStyle.BackColor =
                Color.FromArgb(28, 40, 63);

            gridPlans.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;

            gridPlans.ColumnHeadersDefaultCellStyle.Font =
                new Font("Segoe UI Semibold", 9F, FontStyle.Bold);

            gridPlans.EnableHeadersVisualStyles = false;

            gridPlans.Columns.Add("PlanId", "Plan ID");
            gridPlans.Columns.Add("Plan", "Plan");
            gridPlans.Columns.Add("SuitedTo", "Suited To");
            gridPlans.Columns.Add("Monthly", "Monthly");
            gridPlans.Columns.Add("Yearly", "Yearly");
            gridPlans.Columns.Add("Employees", "Employees");
            gridPlans.Columns.Add("Branches", "Branches");
            gridPlans.Columns.Add("Modules", "Modules");
            gridPlans.Columns.Add("Status", "Status");
            gridPlans.Columns.Add("Companies", "Active Companies");

            gridPlans.Columns["PlanId"].Visible = false;

            gridPlans.Columns["Monthly"].DefaultCellStyle.Alignment =
                DataGridViewContentAlignment.MiddleRight;

            gridPlans.Columns["Yearly"].DefaultCellStyle.Alignment =
                DataGridViewContentAlignment.MiddleRight;

            gridPlans.Columns["Employees"].DefaultCellStyle.Alignment =
                DataGridViewContentAlignment.MiddleCenter;

            gridPlans.Columns["Branches"].DefaultCellStyle.Alignment =
                DataGridViewContentAlignment.MiddleCenter;

            gridPlans.Columns["Modules"].DefaultCellStyle.Alignment =
                DataGridViewContentAlignment.MiddleCenter;

            gridPlans.Columns["Companies"].DefaultCellStyle.Alignment =
                DataGridViewContentAlignment.MiddleCenter;

            gridPlans.SelectionMode =
                DataGridViewSelectionMode.FullRowSelect;

            gridPlans.MultiSelect = false;
            gridPlans.ReadOnly = true;
            gridPlans.AllowUserToAddRows = false;
            gridPlans.AllowUserToDeleteRows = false;
            gridPlans.RowHeadersVisible = false;
        }

        private void ConfigureSubscriptionGrid()
        {
            gridSubscriptions.ColumnHeadersDefaultCellStyle.BackColor =
                Color.FromArgb(28, 40, 63);

            gridSubscriptions.ColumnHeadersDefaultCellStyle.ForeColor =
                Color.White;

            gridSubscriptions.ColumnHeadersDefaultCellStyle.Font =
                new Font("Segoe UI Semibold", 9F, FontStyle.Bold);

            gridSubscriptions.EnableHeadersVisualStyles = false;

            gridSubscriptions.SelectionMode =
                DataGridViewSelectionMode.FullRowSelect;

            gridSubscriptions.MultiSelect = false;
            gridSubscriptions.ReadOnly = true;
            gridSubscriptions.AllowUserToAddRows = false;
            gridSubscriptions.AllowUserToDeleteRows = false;
            gridSubscriptions.RowHeadersVisible = false;
        }

        private static async Task<string> ReadMessageAsync(HttpResponseMessage r)
        {
            try
            {
                var body = await r.Content.ReadFromJsonAsync<JsonElement>();

                if (body.TryGetProperty("message", out var m) &&
                    m.GetString() is string s)
                {
                    return s;
                }
            }
            catch
            {
            }

            return $"Request failed ({(int)r.StatusCode}).";
        }

        private int? GetSelectedTenantId()
        {
            if (gridSubscriptions.SelectedRows.Count == 0)
                return null;

            var value = gridSubscriptions.SelectedRows[0]
                .Cells["TenantId"]
                .Value;

            if (value == null)
                return null;

            return Convert.ToInt32(value);
        }

        private async Task<List<PlanDto>> LoadPlansAsync()
        {
            try
            {
                var plans =
                    await _httpClient.GetFromJsonAsync<List<PlanDto>>(
                        "admin/plans")
                    ?? new List<PlanDto>();

                gridPlans.Rows.Clear();

                foreach (var p in plans.OrderBy(x => x.SortOrder))
                {
                    int index = gridPlans.Rows.Add(
                        p.PlanId,
                        p.Name,
                        string.IsNullOrWhiteSpace(p.Description)
                            ? "-"
                            : p.Description,
                        $"PHP {p.MonthlyPrice:N2}",
                        $"PHP {p.YearlyPrice:N2}",
                        p.MaxEmployees,
                        p.MaxBranches == 0
                            ? "None"
                            : p.MaxBranches.ToString(),
                        p.Modules?.Count ?? 0,
                        p.IsActive ? "Active" : "Inactive",
                        p.CompanyCount
                    );

                    gridPlans.Rows[index].Tag = p;

                    if (!p.IsActive)
                    {
                        gridPlans.Rows[index].DefaultCellStyle.ForeColor =
                            Color.Gray;
                    }
                }

                return plans;
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Could not load plans.\n\n" + ex.Message,
                    "Plans",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return new List<PlanDto>();
            }
        }

        private async Task LoadSubscriptionsAsync()
        {
            try
            {
                var tenants =
                    await _httpClient.GetFromJsonAsync<List<TenantRow>>(
                        "admin/tenants")
                    ?? new List<TenantRow>();

                gridSubscriptions.DataSource =
                    tenants.Select(t => new
                    {
                        t.TenantId,
                        Tenant = t.Name,
                        Code = t.Code,
                        Status = t.Status,
                        Plan = t.Plan ?? "—",
                        Expiry = t.Expiry?.ToString("yyyy-MM-dd") ?? "—",
                        Database = t.DatabaseName ?? "(none)"
                    }).ToList();

                if (gridSubscriptions.Columns["TenantId"] != null)
                {
                    gridSubscriptions.Columns["TenantId"].Visible = false;
                }

                if (gridSubscriptions.Columns["Tenant"] != null)
                    gridSubscriptions.Columns["Tenant"].HeaderText = "Company";

                if (gridSubscriptions.Columns["Code"] != null)
                    gridSubscriptions.Columns["Code"].HeaderText = "Company Code";

                if (gridSubscriptions.Columns["Plan"] != null)
                    gridSubscriptions.Columns["Plan"].HeaderText = "Subscription Plan";

                if (gridSubscriptions.Columns["Expiry"] != null)
                    gridSubscriptions.Columns["Expiry"].HeaderText = "Expiry Date";

                if (gridSubscriptions.Columns["Database"] != null)
                    gridSubscriptions.Columns["Database"].HeaderText = "Database";
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Could not load tenants.\n\n" + ex.Message,
                    "Subscriptions",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
        }

        private async Task<List<string>> LoadPlanNamesAsync()
        {
            try
            {
                var plans =
                    await _httpClient.GetFromJsonAsync<List<PlanDto>>(
                        "admin/plans")
                    ?? new List<PlanDto>();

                var names = plans
                    .Where(p => p.IsActive)
                    .OrderBy(p => p.SortOrder)
                    .Select(p => p.Name)
                    .ToList();

                if (names.Count == 0)
                {
                    MessageBox.Show(
                        "There are no active plans. Add one in Plans and Pricing first.",
                        "Plans");
                }

                return names;
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Could not load plans.\n\n" + ex.Message,
                    "Plans");

                return new List<string>();
            }
        }

        // ---------------- Buttons ----------------

        private async void BtnSuspendTenant_Click(object sender, EventArgs e)
        {
            var id = GetSelectedTenantId();
            if (id == null) { MessageBox.Show("Select a tenant first."); return; }

            if (MessageBox.Show("Suspend this tenant? Their employees won't be able to log in.",
                "Confirm", MessageBoxButtons.YesNo) != DialogResult.Yes) return;

            var response = await _httpClient.PutAsync($"admin/tenants/{id}/suspend", null);
            if (!response.IsSuccessStatusCode)
                MessageBox.Show(await ReadMessageAsync(response));

            await LoadSubscriptionsAsync();
        }

        private async void BtnReactivateTenant_Click(object sender, EventArgs e)
        {
            var id = GetSelectedTenantId();
            if (id == null) { MessageBox.Show("Select a tenant first."); return; }

            var plans = await LoadPlanNamesAsync();
            if (plans.Count == 0) return;

            using var dialog = new ChangePlanDialog(plans);
            if (dialog.ShowDialog() != DialogResult.OK) return;

            var response = await _httpClient.PutAsJsonAsync($"admin/tenants/{id}/reactivate",
                new { plan = dialog.Plan, expiry = dialog.Expiry });

            if (!response.IsSuccessStatusCode)
                MessageBox.Show(await ReadMessageAsync(response));

            await LoadSubscriptionsAsync();
        }

        private async void BtnNewCompany_Click(object sender, EventArgs e)
        {
            var plans = await LoadPlanNamesAsync();
            if (plans.Count == 0) return;

            using var dialog = new CreateCompanyDialog(plans);
            if (dialog.ShowDialog() != DialogResult.OK) return;

            Cursor = Cursors.WaitCursor;
            try
            {
                var response = await _httpClient.PostAsJsonAsync("admin/tenants", dialog.Request);
                if (!response.IsSuccessStatusCode)
                {
                    MessageBox.Show(await ReadMessageAsync(response), "Could not create company");
                    return;
                }

                var created = await response.Content.ReadFromJsonAsync<JsonElement>();
                string? newCode =
                    created.TryGetProperty("code", out var codeValue)
                        ? codeValue.GetString()
                        : null;

                MessageBox.Show(
                    $"Company created with code {newCode} and its database is ready.");
                await LoadSubscriptionsAsync();
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private async void BtnEditCompany_Click(object? sender, EventArgs e)
        {
            var id = GetSelectedTenantId();
            if (id == null) { MessageBox.Show("Select a tenant first."); return; }

            var plans = await LoadPlanNamesAsync();
            if (plans.Count == 0) return;

            TenantDetail? detail;

            try
            {
                detail = await _httpClient.GetFromJsonAsync<TenantDetail>($"admin/tenants/{id}");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Could not load the company.\n\n" + ex.Message);
                return;
            }

            if (detail == null) return;

            using var dialog = new EditCompanyDialog(detail, plans);
            if (dialog.ShowDialog(this) != DialogResult.OK) return;

            var response = await _httpClient.PutAsJsonAsync($"admin/tenants/{id}", dialog.Request);

            if (!response.IsSuccessStatusCode)
            {
                MessageBox.Show(await ReadMessageAsync(response), "Could not save company",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            await LoadSubscriptionsAsync();
        }

        private async void BtnDeleteCompany_Click(object? sender, EventArgs e)
        {
            var id = GetSelectedTenantId();
            if (id == null) { MessageBox.Show("Select a tenant first."); return; }

            var answer = MessageBox.Show(
                "Delete this company?\n\nIts users can no longer log in. Nothing is erased: " +
                "the database is kept and you can bring the company back with Reactivate Selected.",
                "Delete Company", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (answer != DialogResult.Yes) return;

            var response = await _httpClient.DeleteAsync($"admin/tenants/{id}");

            if (!response.IsSuccessStatusCode)
                MessageBox.Show(await ReadMessageAsync(response));

            await LoadSubscriptionsAsync();
        }

        private async void BtnUpdateDatabase_Click(object sender, EventArgs e)
        {
            var id = GetSelectedTenantId();
            if (id == null) { MessageBox.Show("Select a tenant first."); return; }

            if (MessageBox.Show(
                    "Apply all pending database updates to this tenant?\n\nThis is safe to repeat.",
                    "Update Database",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question) != DialogResult.Yes)
            {
                return;
            }

            // Migrations can outlast the shared client's default timeout, so use
            // a separate client that copies its address and login token.
            using var longClient = new HttpClient
            {
                BaseAddress = _httpClient.BaseAddress,
                Timeout = TimeSpan.FromMinutes(5)
            };
            longClient.DefaultRequestHeaders.Authorization =
                _httpClient.DefaultRequestHeaders.Authorization;

            btnUpdateDatabase.Enabled = false;
            Cursor = Cursors.WaitCursor;

            try
            {
                var response = await longClient.PostAsync($"admin/tenants/{id}/migrate", null);

                if (!response.IsSuccessStatusCode)
                {
                    MessageBox.Show(
                        await ReadMessageAsync(response),
                        "Update failed",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    return;
                }

                var body = await response.Content.ReadFromJsonAsync<JsonElement>();

                string message =
                    body.TryGetProperty("message", out var m) && m.GetString() is string s
                        ? s
                        : "Done.";

                var applied = new List<string>();

                if (body.TryGetProperty("applied", out var arr) &&
                    arr.ValueKind == JsonValueKind.Array)
                {
                    foreach (var item in arr.EnumerateArray())
                    {
                        if (item.GetString() is string name)
                            applied.Add(name);
                    }
                }

                if (applied.Count > 0)
                    message += "\n\n" + string.Join("\n", applied);

                MessageBox.Show(
                    message,
                    "Update Database",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            catch (TaskCanceledException)
            {
                MessageBox.Show(
                    "The request timed out on this computer. The update may still be running on the server.\n\n" +
                    "Wait a minute and press Update Database again to check. It is safe to repeat.",
                    "Update Database");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unable to update the database.\n\n" + ex.Message);
            }
            finally
            {
                btnUpdateDatabase.Enabled = true;
                Cursor = Cursors.Default;
            }
        }

        // ---------------- DTO ----------------

        private class TenantRow
        {
            public int TenantId { get; set; }
            public string Name { get; set; } = "";
            public string Code { get; set; } = "";
            public string Status { get; set; } = "";
            public string? Plan { get; set; }
            public DateTime? Expiry { get; set; }
            public string? DatabaseName { get; set; }
        }
    }

    internal class TenantDetail
    {
        public int TenantId { get; set; }
        public string Code { get; set; } = "";
        public string Name { get; set; } = "";
        public string? Plan { get; set; }
        public DateTime? Expiry { get; set; }
        public string? Address { get; set; }
        public string? Tin { get; set; }
        public string? ContactNumber { get; set; }
    }

    public class CreateCompanyDialog : Form
    {
        private readonly TextBox txtCode = new(), txtName = new(),
            txtServer = new(), txtDatabase = new(), txtKey = new();
        private readonly ComboBox cmbPlan = new();
        private readonly DateTimePicker dtpExpiry = new();

        public object Request => new
        {
            companyName = txtName.Text.Trim(),
            subscriptionPlan = cmbPlan.Text,
            subscriptionExpiry = dtpExpiry.Value.Date,
            serverName = txtServer.Text.Trim(),
            databaseName = txtDatabase.Text.Trim(),
            credentialKey = txtKey.Text.Trim()
        };

        public CreateCompanyDialog(IEnumerable<string>? plans = null)
        {
            Text = "New Company";
            StartPosition = FormStartPosition.CenterParent;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = MinimizeBox = false;
            ClientSize = new Size(420, 360);

            cmbPlan.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbPlan.Items.AddRange((plans ?? new[] { "Basic", "Premium", "Enterprise" }).Cast<object>().ToArray());
            cmbPlan.SelectedIndex = 0;
            dtpExpiry.Value = DateTime.Today.AddYears(1);

            txtCode.Text = "COM-";
            txtCode.Enabled = false;

            var fields = new (string Label, Control Input)[]
            {
                ("Company code", txtCode), ("Company name", txtName),
                ("Plan", cmbPlan), ("Subscription expiry", dtpExpiry),
                ("Database server", txtServer), ("Database name", txtDatabase),
                ("Credential key", txtKey)
            };

            int y = 15;
            foreach (var (label, input) in fields)
            {
                Controls.Add(new Label { Text = label, Location = new Point(20, y + 4), AutoSize = true });
                input.Location = new Point(160, y);
                input.Width = 240;
                Controls.Add(input);
                y += 38;
            }

            var ok = new Button { Text = "Create", Location = new Point(230, y + 15), Size = new Size(80, 32) };
            var cancel = new Button { Text = "Cancel", Location = new Point(320, y + 15), Size = new Size(80, 32), DialogResult = DialogResult.Cancel };
            ok.Click += (s, e) =>
            {
                if (new[] { txtName, txtServer, txtDatabase, txtKey }.Any(t => string.IsNullOrWhiteSpace(t.Text)))
                {
                    MessageBox.Show("Please fill in every field.");
                    return;
                }
                DialogResult = DialogResult.OK;
            };
            Controls.Add(ok);
            Controls.Add(cancel);
            AcceptButton = ok;
            CancelButton = cancel;
        }
    }

    // Replaces ReactivateDialog: plan comes from the plans list, plus a new expiry date.
    public class ChangePlanDialog : Form
    {
        private readonly ComboBox cmbPlan = new();
        private readonly DateTimePicker dtpExpiry = new();

        public string Plan => cmbPlan.Text;
        public DateTime Expiry => dtpExpiry.Value.Date;

        public ChangePlanDialog(IEnumerable<string> plans)
        {
            Text = "Reactivate Company";
            StartPosition = FormStartPosition.CenterParent;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = MinimizeBox = false;
            ClientSize = new Size(380, 170);

            cmbPlan.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbPlan.Items.AddRange(plans.Cast<object>().ToArray());
            cmbPlan.SelectedIndex = 0;
            dtpExpiry.Value = DateTime.Today.AddYears(1);

            Controls.Add(new Label { Text = "Plan", Location = new Point(20, 24), AutoSize = true });
            cmbPlan.Location = new Point(150, 20);
            cmbPlan.Width = 210;
            Controls.Add(cmbPlan);

            Controls.Add(new Label { Text = "New expiry date", Location = new Point(20, 64), AutoSize = true });
            dtpExpiry.Location = new Point(150, 60);
            dtpExpiry.Width = 210;
            Controls.Add(dtpExpiry);

            var ok = new Button { Text = "Reactivate", Location = new Point(170, 115), Size = new Size(90, 32) };
            var cancel = new Button { Text = "Cancel", Location = new Point(270, 115), Size = new Size(90, 32), DialogResult = DialogResult.Cancel };
            ok.Click += (s, e) => DialogResult = DialogResult.OK;
            Controls.Add(ok);
            Controls.Add(cancel);
            AcceptButton = ok;
            CancelButton = cancel;
        }
    }

    // View / edit a company: name, code, plan, expiry and the receipt header details.
    internal class EditCompanyDialog : Form
    {
        private readonly TextBox txtCode = new(), txtName = new(),
            txtAddress = new(), txtTin = new(), txtContact = new();
        private readonly ComboBox cmbPlan = new();
        private readonly DateTimePicker dtpExpiry = new();

        public object Request => new
        {
            code = txtCode.Text.Trim(),
            name = txtName.Text.Trim(),
            plan = cmbPlan.Text,
            expiry = dtpExpiry.Value.Date,
            address = txtAddress.Text.Trim(),
            tin = txtTin.Text.Trim(),
            contactNumber = txtContact.Text.Trim()
        };

        public EditCompanyDialog(TenantDetail detail, IEnumerable<string> plans)
        {
            Text = "Edit Company";
            StartPosition = FormStartPosition.CenterParent;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = MinimizeBox = false;
            ClientSize = new Size(440, 370);

            var planList = plans.ToList();

            // Keep the company's current plan selectable even if that plan was switched off.
            if (!string.IsNullOrWhiteSpace(detail.Plan) && !planList.Contains(detail.Plan))
                planList.Add(detail.Plan);

            cmbPlan.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbPlan.Items.AddRange(planList.Cast<object>().ToArray());
            cmbPlan.SelectedItem = detail.Plan ?? planList[0];

            txtCode.Text = detail.Code;
            txtName.Text = detail.Name;
            txtAddress.Text = detail.Address ?? "";
            txtTin.Text = detail.Tin ?? "";
            txtContact.Text = detail.ContactNumber ?? "";
            dtpExpiry.Value = detail.Expiry ?? DateTime.Today.AddYears(1);

            var fields = new (string Label, Control Input)[]
            {
                ("Company code", txtCode), ("Company name", txtName),
                ("Plan", cmbPlan), ("Subscription expiry", dtpExpiry),
                ("Address (on receipts)", txtAddress), ("TIN (on receipts)", txtTin),
                ("Contact number", txtContact)
            };

            int y = 15;
            foreach (var (label, input) in fields)
            {
                Controls.Add(new Label { Text = label, Location = new Point(20, y + 4), AutoSize = true });
                input.Location = new Point(180, y);
                input.Width = 240;
                Controls.Add(input);
                y += 38;
            }

            var ok = new Button { Text = "Save", Location = new Point(250, y + 15), Size = new Size(80, 32) };
            var cancel = new Button { Text = "Cancel", Location = new Point(340, y + 15), Size = new Size(80, 32), DialogResult = DialogResult.Cancel };
            ok.Click += (s, e) =>
            {
                if (string.IsNullOrWhiteSpace(txtCode.Text) || string.IsNullOrWhiteSpace(txtName.Text))
                {
                    MessageBox.Show("Company code and name are required.");
                    return;
                }
                DialogResult = DialogResult.OK;
            };
            Controls.Add(ok);
            Controls.Add(cancel);
            AcceptButton = ok;
            CancelButton = cancel;
        }
    }
}