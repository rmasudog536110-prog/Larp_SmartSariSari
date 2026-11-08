using Larp.domain.Entities;
using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Larp.winforms.SmallTenant.UserManagement
{
    public partial class AddEmployeeWinforms : Form
    {
        private readonly HttpClient _httpClient;
        private readonly int _companyId;

        public AddEmployeeWinforms(
            HttpClient httpClient,
            int companyId)
        {
            InitializeComponent();

            _httpClient = httpClient;
            _companyId = companyId;

            cmbEmploymentStatus.SelectedIndex = 0;
            cmbPosition.SelectedIndex = 0;
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

                EmployeeEntity employee =
                    new EmployeeEntity
                    {
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
                            "Active",

                        IsActive =
                            true
                    };

                string url =
                    $"tenant/{_companyId}/employees";

                using HttpResponseMessage response =
                    await _httpClient.PostAsJsonAsync(
                        url,
                        employee);

                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show(
                        "Employee added successfully.",
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
                    "Unable to Add Employee",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Unable to add employee.\n\n" +
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