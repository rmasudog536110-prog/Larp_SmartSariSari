using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Larp.winforms;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Larp.winforms.MediumTenant
{
    public partial class BranchesWinForms : Form
    {
        private readonly HttpClient _httpClient;
        private readonly int _companyId;

        private List<BranchRow> _all = new();

        public BranchesWinForms(HttpClient httpClient, int companyId)
        {
            InitializeComponent();

            _httpClient = httpClient;
            _companyId = companyId;

            AppTheme.Apply(this);

            ConfigureGrid();

            AddCompareButton();

            Shown += async (_, _) =>
            {
                await LoadBranchesAsync();
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
                Name = "BranchCode",
                HeaderText = "Code",
                DataPropertyName = "BranchCode",
                Width = 100
            });

            guna2DataGridView1.Columns.Add(new DataGridViewLinkColumn
            {
                Name = "BranchName",
                HeaderText = "Branch",
                DataPropertyName = "BranchName",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                LinkBehavior = LinkBehavior.HoverUnderline,
                LinkColor = Color.FromArgb(46, 107, 69),
                VisitedLinkColor = Color.FromArgb(46, 107, 69),
                TrackVisitedState = false
            });

            guna2DataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Address",
                HeaderText = "Address",
                DataPropertyName = "Address",
                Width = 220
            });

            guna2DataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "ContactNumber",
                HeaderText = "Contact",
                DataPropertyName = "ContactNumber",
                Width = 120
            });

            guna2DataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "EmployeeCount",
                HeaderText = "Employees",
                DataPropertyName = "EmployeeCount",
                Width = 90,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Alignment = DataGridViewContentAlignment.MiddleRight
                }
            });

            guna2DataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "SalesCount",
                HeaderText = "Sales",
                DataPropertyName = "SalesCount",
                Width = 70,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Alignment = DataGridViewContentAlignment.MiddleRight
                }
            });

            guna2DataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "SalesTotal",
                HeaderText = "Sales Total",
                DataPropertyName = "SalesTotal",
                Width = 130,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Format = "₱#,##0.00",
                    Alignment = DataGridViewContentAlignment.MiddleRight
                }
            });

            guna2DataGridView1.Columns.Add(new DataGridViewCheckBoxColumn
            {
                Name = "IsActive",
                HeaderText = "Active",
                DataPropertyName = "IsActive",
                Width = 60
            });

            guna2DataGridView1.Columns.Add(new DataGridViewButtonColumn
            {
                Name = "Edit",
                HeaderText = "",
                Text = "Edit",
                UseColumnTextForButtonValue = true,
                Width = 70
            });

            guna2DataGridView1.Columns.Add(new DataGridViewButtonColumn
            {
                Name = "Deactivate",
                HeaderText = "",
                Text = "Deactivate",
                UseColumnTextForButtonValue = true,
                Width = 95
            });
        }

        private void AddCompareButton()
        {
            guna2TextBox1.Width = 250;

            var btn = new Guna.UI2.WinForms.Guna2Button
            {
                Name = "btnCompare",
                Text = "Compare",
                BorderRadius = 8,
                FillColor = Color.FromArgb(52, 73, 94),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                Location = new Point(295, 40),
                Size = new Size(108, 53)
            };

            btn.Click += (_, _) =>
            {
                using var f = new BranchCompareWinForms(_httpClient, _companyId);
                f.ShowDialog(this);
            };

            Controls.Add(btn);
        }
        private async Task LoadBranchesAsync()
        {
            try
            {
                guna2DataGridView1.Enabled = false;
                guna2ProgressIndicator1.Visible = true;

                var response = await _httpClient.GetAsync(
                    $"tenant/{_companyId}/branches");

                if (!response.IsSuccessStatusCode)
                {
                    MessageBox.Show(
                        await ReadMessageAsync(response),
                        "Branches",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);

                    return;
                }

                _all = await response.Content.ReadFromJsonAsync<List<BranchRow>>()
                       ?? new List<BranchRow>();

                ApplyFilter();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Unable to load branches.\n\n{ex.Message}",
                    "Branches",
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
                    x.BranchCode.Contains(search, StringComparison.OrdinalIgnoreCase) ||
                    x.BranchName.Contains(search, StringComparison.OrdinalIgnoreCase) ||
                    (x.Address?.Contains(search, StringComparison.OrdinalIgnoreCase) ?? false))
                    .ToList();

            guna2DataGridView1.DataSource = null;
            guna2DataGridView1.DataSource = rows;
        }

        private void guna2TextBox1_TextChanged(object? sender, EventArgs e)
        {
            ApplyFilter();
        }

        private async void btnRefresh_Click(object? sender, EventArgs e)
        {
            guna2TextBox1.Clear();
            await LoadBranchesAsync();
        }

        private void btnClose_Click(object? sender, EventArgs e)
        {
            Close();
        }

        private async void btnAddBranch_Click(object? sender, EventArgs e)
        {
            using var dialog = new BranchDialog(null);
            if (dialog.ShowDialog(this) != DialogResult.OK) return;

            var response = await _httpClient.PostAsJsonAsync(
                $"tenant/{_companyId}/branches",
                dialog.Request);

            if (!response.IsSuccessStatusCode)
            {
                MessageBox.Show(
                    await ReadMessageAsync(response),
                    "Cannot add branch",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            await LoadBranchesAsync();
        }

        private async void btnAssignEmployee_Click(object? sender, EventArgs e)
        {
            if (guna2DataGridView1.SelectedRows.Count == 0 ||
                guna2DataGridView1.SelectedRows[0].DataBoundItem is not BranchRow branch)
            {
                MessageBox.Show(
                    "Select a branch first.",
                    "Branches",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                return;
            }

            if (!branch.IsActive)
            {
                MessageBox.Show(
                    "This branch is inactive.",
                    "Branches",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                return;
            }

            using var dialog = new AssignEmployeeDialog(
                _httpClient,
                _companyId,
                branch,
                _all);

            if (dialog.ShowDialog(this) == DialogResult.OK)
                await LoadBranchesAsync();
        }

        private async void guna2DataGridView1_CellContentClick(
            object? sender,
            DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
                return;

            if (guna2DataGridView1.Rows[e.RowIndex].DataBoundItem is not BranchRow branch)
                return;

            string column = guna2DataGridView1.Columns[e.ColumnIndex].Name;
            if (column == "BranchName")
            {
                using var details = new BranchDetailsWinForms(
                    _httpClient, _companyId, branch.BranchId, branch.BranchName);
                details.ShowDialog(this);
                return;
            }

            if (column == "Edit")
            {
                using var dialog = new BranchDialog(branch);
                if (dialog.ShowDialog(this) != DialogResult.OK) return;

                var response = await _httpClient.PutAsJsonAsync(
                    $"tenant/{_companyId}/branches/{branch.BranchId}",
                    dialog.Request);

                if (!response.IsSuccessStatusCode)
                {
                    MessageBox.Show(
                        await ReadMessageAsync(response),
                        "Cannot save branch",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    return;
                }

                await LoadBranchesAsync();
            }
            else if (column == "Deactivate")
            {
                if (!branch.IsActive)
                {
                    MessageBox.Show(
                        "This branch is already inactive.",
                        "Branches",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    return;
                }

                var confirm = MessageBox.Show(
                    $"Deactivate branch '{branch.BranchName}'?\n\n" +
                    "Its sales history is kept.",
                    "Deactivate Branch",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (confirm != DialogResult.Yes) return;

                var response = await _httpClient.DeleteAsync(
                    $"tenant/{_companyId}/branches/{branch.BranchId}");

                if (!response.IsSuccessStatusCode)
                {
                    MessageBox.Show(
                        await ReadMessageAsync(response),
                        "Cannot deactivate branch",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    return;
                }

                await LoadBranchesAsync();
            }
        }

        internal static async Task<string> ReadMessageAsync(HttpResponseMessage response)
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
    }

    internal class BranchRow
    {
        public int BranchId { get; set; }
        public string BranchCode { get; set; } = string.Empty;
        public string BranchName { get; set; } = string.Empty;
        public string? Address { get; set; }
        public string? ContactNumber { get; set; }
        public bool IsActive { get; set; }
        public int EmployeeCount { get; set; }
        public int SalesCount { get; set; }
        public decimal SalesTotal { get; set; }
    }

    // Add / edit a branch. Built in code, no designer file.
    internal class BranchDialog : Form
    {
        private readonly TextBox txtCode = new(), txtName = new(),
            txtAddress = new(), txtContact = new();
        private readonly CheckBox chkActive = new();

        public object Request => new
        {
            branchCode = txtCode.Text.Trim(),
            branchName = txtName.Text.Trim(),
            address = txtAddress.Text.Trim(),
            contactNumber = txtContact.Text.Trim(),
            isActive = chkActive.Checked
        };

        public BranchDialog(BranchRow? existing)
        {
            Text = existing == null ? "Add Branch" : "Edit Branch";
            StartPosition = FormStartPosition.CenterParent;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = MinimizeBox = false;
            ClientSize = new Size(420, 265);

            chkActive.Text = "Active";
            chkActive.Checked = existing?.IsActive ?? true;
            chkActive.Enabled = existing != null;

            if (existing != null)
            {
                txtCode.Text = existing.BranchCode;
                txtName.Text = existing.BranchName;
                txtAddress.Text = existing.Address ?? "";
                txtContact.Text = existing.ContactNumber ?? "";
            }

            var fields = new (string Label, Control Input)[]
            {
                ("Branch code", txtCode), ("Branch name", txtName),
                ("Address", txtAddress), ("Contact number", txtContact),
                ("", chkActive)
            };

            int y = 15;
            foreach (var (label, input) in fields)
            {
                if (label.Length > 0)
                    Controls.Add(new Label { Text = label, Location = new Point(20, y + 4), AutoSize = true });

                input.Location = new Point(160, y);
                input.Width = 240;
                Controls.Add(input);
                y += 38;
            }

            var ok = new Button { Text = "Save", Location = new Point(230, y + 15), Size = new Size(80, 32) };
            var cancel = new Button { Text = "Cancel", Location = new Point(320, y + 15), Size = new Size(80, 32), DialogResult = DialogResult.Cancel };

            ok.Click += (s, e) =>
            {
                if (string.IsNullOrWhiteSpace(txtCode.Text) || string.IsNullOrWhiteSpace(txtName.Text))
                {
                    MessageBox.Show("Branch code and name are required.");
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

    // Moves an employee into the selected branch.
    internal class AssignEmployeeDialog : Form
    {
        private readonly HttpClient _httpClient;
        private readonly int _companyId;
        private readonly BranchRow _branch;
        private readonly List<BranchRow> _branches;

        private readonly ComboBox cmbEmployee = new();
        private readonly Button ok = new();
        private readonly Label lblStatus = new();

        public AssignEmployeeDialog(
            HttpClient httpClient,
            int companyId,
            BranchRow branch,
            List<BranchRow> branches)
        {
            _httpClient = httpClient;
            _companyId = companyId;
            _branch = branch;
            _branches = branches;

            Text = "Assign Employee";
            StartPosition = FormStartPosition.CenterParent;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = MinimizeBox = false;
            ClientSize = new Size(460, 190);

            Controls.Add(new Label
            {
                Text = $"Assign an employee to \"{branch.BranchName}\"",
                Location = new Point(20, 20),
                AutoSize = true
            });

            cmbEmployee.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbEmployee.Location = new Point(20, 55);
            cmbEmployee.Width = 420;
            Controls.Add(cmbEmployee);

            lblStatus.Location = new Point(20, 95);
            lblStatus.Size = new Size(420, 20);
            Controls.Add(lblStatus);

            ok.Text = "Assign";
            ok.Location = new Point(270, 140);
            ok.Size = new Size(80, 32);
            ok.Enabled = false;
            ok.Click += async (s, e) => await AssignAsync();

            var cancel = new Button
            {
                Text = "Cancel",
                Location = new Point(360, 140),
                Size = new Size(80, 32),
                DialogResult = DialogResult.Cancel
            };

            Controls.Add(ok);
            Controls.Add(cancel);
            CancelButton = cancel;

            Shown += async (s, e) => await LoadEmployeesAsync();
        }

        private async Task LoadEmployeesAsync()
        {
            try
            {
                lblStatus.Text = "Loading employees...";

                var employees = await _httpClient.GetFromJsonAsync<List<EmployeeRow>>(
                                    $"tenant/{_companyId}/employees")
                                ?? new List<EmployeeRow>();

                foreach (var emp in employees.Where(x => x.IsActive))
                {
                    var current = emp.BranchId.HasValue
                        ? _branches.FirstOrDefault(b => b.BranchId == emp.BranchId.Value)?.BranchName
                        : null;

                    string text = $"{emp.EmployeeCode} - {emp.FirstName} {emp.LastName}";
                    if (current != null) text += $"   (now: {current})";

                    cmbEmployee.Items.Add(new EmployeeChoice(emp.EmployeeId, text));
                }

                if (cmbEmployee.Items.Count > 0)
                {
                    cmbEmployee.SelectedIndex = 0;
                    ok.Enabled = true;
                    lblStatus.Text = "";
                }
                else
                {
                    lblStatus.Text = "No active employees found.";
                }
            }
            catch (Exception ex)
            {
                lblStatus.Text = "Could not load employees: " + ex.Message;
            }
        }

        private async Task AssignAsync()
        {
            if (cmbEmployee.SelectedItem is not EmployeeChoice choice) return;

            ok.Enabled = false;

            try
            {
                var response = await _httpClient.PutAsync(
                    $"tenant/{_companyId}/branches/{_branch.BranchId}/employees/{choice.Id}",
                    null);

                if (!response.IsSuccessStatusCode)
                {
                    lblStatus.Text = await BranchesWinForms.ReadMessageAsync(response);
                    ok.Enabled = true;
                    return;
                }

                DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                lblStatus.Text = "Assign failed: " + ex.Message;
                ok.Enabled = true;
            }
        }

        private class EmployeeRow
        {
            public int EmployeeId { get; set; }
            public string EmployeeCode { get; set; } = string.Empty;
            public string FirstName { get; set; } = string.Empty;
            public string LastName { get; set; } = string.Empty;
            public bool IsActive { get; set; }
            public int? BranchId { get; set; }
        }

        private class EmployeeChoice
        {
            public int Id { get; }
            public string Text { get; }
            public EmployeeChoice(int id, string text) { Id = id; Text = text; }
            public override string ToString() => Text;
        }
    }
}