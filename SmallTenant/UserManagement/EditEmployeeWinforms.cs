using Larp.domain.Entities;
using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Larp.winforms.SmallTenant.UserManagement
{
    public partial class EditEmployeeWinforms : Form
    {
        private readonly HttpClient _httpClient;
        private readonly int _companyId;
        private readonly EmployeeEntity _employee;

        public EditEmployeeWinforms(
            HttpClient httpClient,
            int companyId,
            EmployeeEntity employee)
        {
            InitializeComponent();

            _httpClient = httpClient;
            _companyId = companyId;
            _employee = employee;

            LoadEmployeeData();
        }

        private void LoadEmployeeData()
        {
            txtEmployeeCode.Text =
                _employee.EmployeeCode;

            txtFirstName.Text =
                _employee.FirstName;

            txtMiddleName.Text =
                _employee.MiddleName ?? "";

            txtLastName.Text =
                _employee.LastName;

            txtContactNumber.Text =
                _employee.ContactNumber ?? "";

            txtEmailAddress.Text =
                _employee.EmailAddress ?? "";

            txtAddress.Text =
                _employee.Address ?? "";

            dtpDateHired.Value =
                _employee.DateHired.ToLocalTime();

            if (cmbPosition.Items.Contains(
                _employee.Position))
            {
                cmbPosition.SelectedItem =
                    _employee.Position;
            }
            else
            {
                cmbPosition.SelectedIndex =
                    -1;
            }

            if (cmbEmploymentStatus.Items.Contains(
                _employee.EmploymentStatus))
            {
                cmbEmploymentStatus.SelectedItem =
                    _employee.EmploymentStatus;
            }
            else
            {
                cmbEmploymentStatus.SelectedIndex =
                    0;
            }

            if (_employee.DateSeparated.HasValue)
            {
                dtpDateSeparated.Value =
                    _employee.DateSeparated.Value.ToLocalTime();
            }

            txtSeparationReason.Text =
                _employee.SeparationReason ?? "";

            UpdateSeparationFields();
        }

        private void cmbEmploymentStatus_SelectedIndexChanged(
            object? sender,
            EventArgs e)
        {
            UpdateSeparationFields();
        }

        private void UpdateSeparationFields()
        {
            bool separated =
                cmbEmploymentStatus.Text == "Resigned" ||
                cmbEmploymentStatus.Text == "Fired";

            lblDateSeparated.Visible =
                separated;

            dtpDateSeparated.Visible =
                separated;

            lblSeparationReason.Visible =
                separated;

            txtSeparationReason.Visible =
                separated;
        }

        private async void btnSave_Click(
            object? sender,
            EventArgs e)
        {
            if (!ValidateForm())
            {
                return;
            }

            try
            {
                btnSave.Enabled = false;

                string status =
                    cmbEmploymentStatus.Text;

                bool separated =
                    status == "Resigned" ||
                    status == "Fired";

                EmployeeEntity updated =
                    new EmployeeEntity
                    {
                        EmployeeId =
                            _employee.EmployeeId,

                        EmployeeCode =
                            txtEmployeeCode.Text.Trim(),

                        FirstName =
                            txtFirstName.Text.Trim(),

                        LastName =
                            txtLastName.Text.Trim(),

                        MiddleName =
                            string.IsNullOrWhiteSpace(
                                txtMiddleName.Text)
                                ? null
                                : txtMiddleName.Text.Trim(),

                        Position =
                            cmbPosition.SelectedItem?.ToString()
                            ?? string.Empty,

                        ContactNumber =
                            string.IsNullOrWhiteSpace(
                                txtContactNumber.Text)
                                ? null
                                : txtContactNumber.Text.Trim(),

                        EmailAddress =
                            string.IsNullOrWhiteSpace(
                                txtEmailAddress.Text)
                                ? null
                                : txtEmailAddress.Text.Trim(),

                        Address =
                            string.IsNullOrWhiteSpace(
                                txtAddress.Text)
                                ? null
                                : txtAddress.Text.Trim(),

                        DateHired =
                            dtpDateHired.Value.Date,

                        EmploymentStatus =
                            status,

                        DateSeparated =
                            separated
                                ? dtpDateSeparated.Value.Date
                                : null,

                        SeparationReason =
                            separated &&
                            !string.IsNullOrWhiteSpace(
                                txtSeparationReason.Text)
                                ? txtSeparationReason.Text.Trim()
                                : null,

                        IsActive =
                            status == "Active",

                        CreatedAt =
                            _employee.CreatedAt
                    };

                string url =
                    $"tenant/{_companyId}/employees/{_employee.EmployeeId}";

                using HttpResponseMessage response =
                    await _httpClient.PutAsJsonAsync(
                        url,
                        updated);

                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show(
                        "Employee updated successfully.",
                        "Employee Management",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    DialogResult =
                        DialogResult.OK;

                    Close();

                    return;
                }

                string message =
                    await GetErrorMessage(response);

                MessageBox.Show(
                    message,
                    "Unable to Update Employee",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Unable to update employee.\n\n" +
                    ex.Message,
                    "Employee Management",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            finally
            {
                btnSave.Enabled = true;
            }
        }

        private bool ValidateForm()
        {
            if (string.IsNullOrWhiteSpace(
                txtEmployeeCode.Text))
            {
                MessageBox.Show(
                    "Employee code is required.",
                    "Validation",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                txtEmployeeCode.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(
                txtFirstName.Text))
            {
                MessageBox.Show(
                    "First name is required.",
                    "Validation",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                txtFirstName.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(
                txtLastName.Text))
            {
                MessageBox.Show(
                    "Last name is required.",
                    "Validation",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                txtLastName.Focus();
                return false;
            }

            if (cmbPosition.SelectedIndex < 0)
            {
                MessageBox.Show(
                    "Position is required.",
                    "Validation",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                cmbPosition.Focus();
                return false;
            }

            if (cmbEmploymentStatus.Text == "Resigned" ||
                cmbEmploymentStatus.Text == "Fired")
            {
                if (string.IsNullOrWhiteSpace(
                    txtSeparationReason.Text))
                {
                    MessageBox.Show(
                        "Please provide the reason for separation.",
                        "Validation",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    txtSeparationReason.Focus();
                    return false;
                }
            }

            return true;
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

        private void btnCancel_Click(
            object? sender,
            EventArgs e)
        {
            DialogResult =
                DialogResult.Cancel;

            Close();
        }
    }
}
