using Guna.UI2.WinForms;
using Larp.winforms;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using Larp.winforms.SmallTenant.Payroll;
using System.Windows.Forms;
using EmployeeEntity = Larp.domain.Entities.EmployeeEntity;

namespace Larp.winforms.SmallTenant.UserManagement
{
    public partial class EmployeeWinforms : Form
    {
        private readonly HttpClient _httpClient;
        private readonly int _companyId;

        private List<EmployeeEntity> _employees = new();

        public EmployeeWinforms(
            HttpClient httpClient,
            int companyId)
        {
            InitializeComponent();

            AppTheme.Apply(this);

            _httpClient = httpClient;
            _companyId = companyId;

            guna2DataGridView1.AutoGenerateColumns = false;

            guna2DataGridView1.CellContentClick +=
                guna2DataGridView1_CellContentClick;

            guna2TextBox1.TextChanged +=
                guna2TextBox1_TextChanged;
        }

        private async void EmployeeWinforms_Load(
            object? sender,
            EventArgs e)
        {
            await LoadEmployeesAsync();
        }

        private async Task LoadEmployeesAsync()
        {
            try
            {
                guna2ProgressIndicator1.Visible = true;
                guna2ProgressIndicator1.Start();

                string url =
                    $"tenant/{_companyId}/employees";

                var employees =
                    await _httpClient.GetFromJsonAsync<List<EmployeeEntity>>(url);

                _employees =
                    employees ?? new List<EmployeeEntity>();

                DisplayEmployees(_employees);

                bool isAdmin =
                    Session.Role == "Admin" ||
                    Session.Role == "Super Admin";

                editColumn.Visible = isAdmin;
                deleteColumn.Visible = isAdmin;
                loginColumn.Visible = isAdmin;
                benefitsColumn.Visible = isAdmin;

                UpdateStatistics();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Unable to load employees.\n\n" +
                    ex.Message,
                    "Employee Management",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            finally
            {
                guna2ProgressIndicator1.Stop();
                guna2ProgressIndicator1.Visible = false;
            }
        }

        private void DisplayEmployees(
            IEnumerable<EmployeeEntity> employees)
        {
            guna2DataGridView1.Rows.Clear();

            foreach (var employee in employees)
            {
                string loginRole =
                    string.IsNullOrEmpty(employee.Role)
                        ? "No login"
                        : employee.Role;

                if (employee.ApprovalStatus == "Pending")
                    loginRole += " (pending approval)";
                else if (employee.ApprovalStatus == "Rejected")
                    loginRole += " (rejected)";

                string deleteColumn =
                    employee.EmploymentStatus == "Active"
                        ? "Deactivate"
                        : "Reactivate";

                int rowIndex =
                    guna2DataGridView1.Rows.Add(
                        employee.EmployeeCode,
                        GetFullName(employee),
                        employee.Position,
                        employee.ContactNumber ?? "-",
                        employee.EmploymentStatus,
                        employee.DateHired.ToLocalTime()
                            .ToString("MMM dd, yyyy"),
                        loginRole,
                        "Edit",
                        deleteColumn,
                        "Create Login");

                var row =
                    guna2DataGridView1.Rows[rowIndex];

                row.Tag = employee;

                if (employee.EmploymentStatus == "Active")
                {
                    row.Cells["Status"]
                        .Style.ForeColor =
                        Color.FromArgb(46, 107, 69);
                }
                else if (employee.EmploymentStatus == "Resigned")
                {
                    row.Cells["Status"]
                        .Style.ForeColor =
                        Color.FromArgb(190, 130, 30);
                }
                else if (employee.EmploymentStatus == "Fired")
                {
                    row.Cells["Status"]
                        .Style.ForeColor =
                        Color.FromArgb(190, 65, 65);
                }

                row.Cells["Status"]
                    .Style.Font =
                    new Font(
                        "Segoe UI",
                        9F,
                        FontStyle.Bold);
            }
        }
        private void UpdateStatistics()
        {
            int total =
                _employees.Count;

            int active =
                _employees.Count(
                    x => x.EmploymentStatus == "Active");

            int resigned =
                _employees.Count(
                    x => x.EmploymentStatus == "Resigned");

            int fired =
                _employees.Count(
                    x => x.EmploymentStatus == "Fired");

            lblTotalEmployees.Text =
                total.ToString("N0");

            lblActiveEmployees.Text =
                active.ToString("N0");

            lblResignedEmployees.Text =
                resigned.ToString("N0");

            lblFiredEmployees.Text =
                fired.ToString("N0");
        }

        private void guna2TextBox1_TextChanged(
            object? sender,
            EventArgs e)
        {
            string search =
                guna2TextBox1.Text
                    .Trim()
                    .ToLower();

            if (string.IsNullOrWhiteSpace(search))
            {
                DisplayEmployees(_employees);
                return;
            }

            var filtered =
                _employees.Where(x =>
                    x.EmployeeCode
                        .ToLower()
                        .Contains(search) ||
                    x.FirstName
                        .ToLower()
                        .Contains(search) ||
                    x.LastName
                        .ToLower()
                        .Contains(search) ||
                    (x.MiddleName ?? "")
                        .ToLower()
                        .Contains(search) ||
                    x.Position
                        .ToLower()
                        .Contains(search) ||
                    x.EmploymentStatus
                        .ToLower()
                        .Contains(search));

            DisplayEmployees(filtered);
        }

        private void btnAddEmployee_Click(
            object? sender,
            EventArgs e)
        {
            using AddEmployeeWinforms form =
                new AddEmployeeWinforms(
                    _httpClient,
                    _companyId);

            if (form.ShowDialog(this) ==
                DialogResult.OK)
            {
                _ = LoadEmployeesAsync();
            }
        }

        private async void btnRefresh_Click(
            object? sender,
            EventArgs e)
        {
            await LoadEmployeesAsync();
        }

        private async void guna2DataGridView1_CellContentClick(
            object? sender,
            DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
            {
                return;
            }

            var row =
                guna2DataGridView1.Rows[e.RowIndex];

            if (row.Tag is not EmployeeEntity employee)
            {
                return;
            }

            string columnName =
                guna2DataGridView1
                    .Columns[e.ColumnIndex]
                    .Name;

            if (columnName == "editColumn")
            {
                using var form =
                    new EditEmployeeWinforms(
                        _httpClient,
                        _companyId,
                        employee);

                if (form.ShowDialog(this) == DialogResult.OK)
                {
                    await LoadEmployeesAsync();
                }

                return;
            }

            if (columnName == "deleteColumn")
            {
                if (employee.EmploymentStatus == "Active")
                {
                    await DeactivateEmployeeAsync(employee);
                }
                else
                {
                    await ReactivateEmployeeAsync(employee);
                }

                return;
            }

            if (columnName == "loginColumn")
            {
                if (!string.IsNullOrEmpty(employee.UserId))
                {
                    MessageBox.Show(
                        "This employee already has a login.",
                        "Create Login",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    return;
                }

                if (!employee.IsActive)
                {
                    MessageBox.Show(
                        "This employee is inactive.",
                        "Create Login",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    return;
                }

                using var dialog =
                    new CreateLoginDialog(
                        GetFullName(employee));

                if (dialog.ShowDialog(this) != DialogResult.OK)
                {
                    return;
                }

                await CreateLoginAsync(
                    employee,
                    dialog.Username,
                    dialog.Password,
                    dialog.RoleName);

                return;
            }

            if (columnName == "benefitsColumn")
            {
                using var form =
                    new EmployeeBenefitsWinforms(
                        _httpClient,
                        _companyId,
                        employee.EmployeeId,
                        GetFullName(employee));

                form.ShowDialog(this);

                return;
            }
        }

        private async Task CreateLoginAsync(
            EmployeeEntity employee,
            string username,
            string password,
            string role)
        {
            try
            {
                var response =
                    await _httpClient.PostAsJsonAsync(
                        $"tenant/{_companyId}/employees/{employee.EmployeeId}/account",
                        new { username, password, role });

                if (!response.IsSuccessStatusCode)
                {
                    MessageBox.Show(
                        await GetErrorMessage(response),
                        "Create Login",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    return;
                }

                MessageBox.Show(
                    Session.Role == "Manager"
                        ? "Login created. An Admin must approve it in Approvals."
                        : "Login created.",
                    "Create Login",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                await LoadEmployeesAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Unable to create the login.\n\n" + ex.Message,
                    "Create Login",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private async Task DeactivateEmployeeAsync(
            EmployeeEntity employee)
        {
            DialogResult result =
                MessageBox.Show(
                    $"Are you sure you want to deactivate {GetFullName(employee)}?\n\n" +
                    "This employee will no longer be active.",
                    "Deactivate Employee",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

            if (result != DialogResult.Yes)
            {
                return;
            }

            try
            {
                string url =
                    $"tenant/{_companyId}/employees/{employee.EmployeeId}";

                using HttpResponseMessage response =
                    await _httpClient.DeleteAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show(
                        "Employee was deactivated successfully.",
                        "Employee Management",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    await LoadEmployeesAsync();
                    return;
                }

                string message =
                    await GetErrorMessage(response);

                MessageBox.Show(
                    message,
                    "Unable to Deactivate Employee",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Unable to deactivate employee.\n\n" +
                    ex.Message,
                    "Employee Management",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private async Task ReactivateEmployeeAsync(
            EmployeeEntity employee)
        {
            DialogResult result =
                MessageBox.Show(
                    $"Are you sure you want to reactivate {GetFullName(employee)}?",
                    "Reactivate Employee",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

            if (result != DialogResult.Yes)
            {
                return;
            }

            try
            {
                string url =
                    $"tenant/{_companyId}/employees/{employee.EmployeeId}/reactivate";

                using HttpResponseMessage response =
                    await _httpClient.PutAsync(url, null);

                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show(
                        "Employee was reactivated successfully.",
                        "Employee Management",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    await LoadEmployeesAsync();
                    return;
                }

                string message =
                    await GetErrorMessage(response);

                MessageBox.Show(
                    message,
                    "Unable to Reactivate Employee",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Unable to reactivate employee.\n\n" +
                    ex.Message,
                    "Employee Management",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private async Task<string> GetErrorMessage(
            HttpResponseMessage response)
        {
            try
            {
                string content =
                    await response.Content.ReadAsStringAsync();

                if (string.IsNullOrWhiteSpace(content))
                {
                    return $"Request failed with status code {(int)response.StatusCode}.";
                }

                using JsonDocument document =
                    JsonDocument.Parse(content);

                if (document.RootElement.TryGetProperty(
                    "message",
                    out JsonElement message))
                {
                    return message.GetString()
                        ?? "The request failed.";
                }

                return content;
            }
            catch
            {
                return $"Request failed with status code {(int)response.StatusCode}.";
            }
        }

        private string GetFullName(
            EmployeeEntity employee)
        {
            string middle =
                string.IsNullOrWhiteSpace(employee.MiddleName)
                    ? ""
                    : $" {employee.MiddleName}";

            return
                $"{employee.FirstName}{middle} {employee.LastName}"
                    .Trim();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }

    // Username, password and role for an employee's login. Built in code.
    internal class CreateLoginDialog : Form
    {
        private readonly TextBox txtUsername = new();
        private readonly TextBox txtPassword = new();
        private readonly ComboBox cmbRole = new();

        public string Username => txtUsername.Text.Trim();
        public string Password => txtPassword.Text;
        public string RoleName => cmbRole.SelectedItem?.ToString() ?? string.Empty;

        public CreateLoginDialog(string employeeName)
        {
            Text = "Create Login";
            StartPosition = FormStartPosition.CenterParent;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = MinimizeBox = false;
            ClientSize = new Size(420, 250);

            Controls.Add(new Label
            {
                Text = $"Login for {employeeName}",
                Location = new Point(20, 15),
                AutoSize = true
            });

            // Admin can create Manager or Staff. A Manager can only create Staff.
            string[] roles = Session.Role == "Manager"
                ? new[] { "Staff" }
                : new[] { "Manager", "Staff" };

            cmbRole.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbRole.Items.AddRange(roles);
            cmbRole.SelectedIndex = 0;

            txtPassword.UseSystemPasswordChar = true;

            var fields = new (string Label, Control Input)[]
            {
            ("Username", txtUsername),
            ("Password", txtPassword),
            ("Role", cmbRole)
            };

            int y = 55;
            foreach (var (label, input) in fields)
            {
                Controls.Add(new Label
                {
                    Text = label,
                    Location = new Point(20, y + 4),
                    AutoSize = true
                });

                input.Location = new Point(130, y);
                input.Width = 260;
                Controls.Add(input);
                y += 40;
            }

            Controls.Add(new Label
            {
                Text = "Password needs upper and lower case, a digit and a symbol.",
                Location = new Point(20, y + 5),
                AutoSize = true
            });

            var ok = new Button
            {
                Text = "Create",
                Location = new Point(220, 205),
                Size = new Size(80, 32)
            };

            var cancel = new Button
            {
                Text = "Cancel",
                Location = new Point(310, 205),
                Size = new Size(80, 32),
                DialogResult = DialogResult.Cancel
            };

            ok.Click += (s, e) =>
            {
                if (string.IsNullOrWhiteSpace(txtUsername.Text) ||
                    txtPassword.Text.Length < 6)
                {
                    MessageBox.Show(
                        "Enter a username and a password of at least 6 characters.");
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
