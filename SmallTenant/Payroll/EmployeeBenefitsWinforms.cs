using Larp.domain.DTO;
using System;
using System.Drawing;
using System.Globalization;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Larp.winforms.SmallTenant.Payroll
{
    // Admin-only: government numbers and monthly salary for one employee.
    public partial class EmployeeBenefitsWinforms : Form
    {
        private readonly HttpClient _httpClient;
        private readonly int _companyId;
        private readonly int _employeeId;

        private readonly TextBox txtSss = new();
        private readonly TextBox txtPhilHealth = new();
        private readonly TextBox txtPagibig = new();
        private readonly TextBox txtSalary = new();
        private readonly Button btnSave = new();

        public EmployeeBenefitsWinforms(
            HttpClient httpClient,
            int companyId,
            int employeeId,
            string employeeName)
        {
            _httpClient = httpClient;
            _companyId = companyId;
            _employeeId = employeeId;

            Text = "Employee Benefits";
            StartPosition = FormStartPosition.CenterParent;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            ClientSize = new Size(440, 320);

            Controls.Add(new Label
            {
                Text = $"Benefits for {employeeName}",
                Font = new Font("Segoe UI", 11F, FontStyle.Bold),
                Location = new Point(20, 15),
                AutoSize = true
            });

            var fields = new (string Label, Control Input)[]
            {
                ("SSS number", txtSss),
                ("PhilHealth number", txtPhilHealth),
                ("Pag-IBIG number", txtPagibig),
                ("Monthly salary (₱)", txtSalary)
            };

            int y = 60;
            foreach (var (label, input) in fields)
            {
                Controls.Add(new Label
                {
                    Text = label,
                    Location = new Point(20, y + 4),
                    AutoSize = true
                });

                input.Location = new Point(190, y);
                input.Width = 220;
                Controls.Add(input);
                y += 40;
            }

            Controls.Add(new Label
            {
                Text = "Monthly basic pay. Payroll is every 15 days, so each payslip uses half of it.",
                Location = new Point(20, y + 5),
                Size = new Size(395, 40)
            });

            btnSave.Text = "Save";
            btnSave.Location = new Point(240, 272);
            btnSave.Size = new Size(80, 32);
            btnSave.Click += async (s, e) => await SaveAsync();

            var btnCancel = new Button
            {
                Text = "Cancel",
                Location = new Point(330, 272),
                Size = new Size(80, 32),
                DialogResult = DialogResult.Cancel
            };

            Controls.Add(btnSave);
            Controls.Add(btnCancel);
            AcceptButton = btnSave;
            CancelButton = btnCancel;

            Load += async (s, e) => await LoadAsync();
        }

        private async Task LoadAsync()
        {
            try
            {
                using var response = await _httpClient.GetAsync(
                    $"/tenant/{_companyId}/benefits/{_employeeId}");

                // 404 just means nothing has been entered yet.
                if (response.StatusCode == HttpStatusCode.NotFound)
                    return;

                if (!response.IsSuccessStatusCode)
                {
                    MessageBox.Show(
                        await ApiError.ReadAsync(response),
                        "Benefits",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    return;
                }

                var benefits = await response.Content.ReadFromJsonAsync<BenefitsDto>();

                if (benefits == null)
                    return;

                txtSss.Text = benefits.SssNumber ?? string.Empty;
                txtPhilHealth.Text = benefits.PhilHealthNumber ?? string.Empty;
                txtPagibig.Text = benefits.PagibigNumber ?? string.Empty;
                txtSalary.Text = benefits.MonthlySalary.ToString("N2");
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Unable to load benefits.\n\n" + ex.Message,
                    "Benefits",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private async Task SaveAsync()
        {
            if (!decimal.TryParse(
                    txtSalary.Text,
                    NumberStyles.Number,
                    CultureInfo.CurrentCulture,
                    out decimal salary) ||
                salary <= 0)
            {
                MessageBox.Show(
                    "Enter a monthly salary greater than zero.",
                    "Benefits",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                txtSalary.Focus();
                return;
            }

            var request = new SaveBenefitsRequest
            {
                SssNumber = txtSss.Text.Trim(),
                PhilHealthNumber = txtPhilHealth.Text.Trim(),
                PagibigNumber = txtPagibig.Text.Trim(),
                MonthlySalary = salary
            };

            btnSave.Enabled = false;

            try
            {
                using var response = await _httpClient.PutAsJsonAsync(
                    $"/tenant/{_companyId}/benefits/{_employeeId}",
                    request);

                if (!response.IsSuccessStatusCode)
                {
                    MessageBox.Show(
                        await ApiError.ReadAsync(response),
                        "Benefits",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    return;
                }

                MessageBox.Show(
                    "Benefits saved.",
                    "Benefits",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Unable to save benefits.\n\n" + ex.Message,
                    "Benefits",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            finally
            {
                btnSave.Enabled = true;
            }
        }
    }

    // Reads the { "message": "..." } body the API sends on errors.
    internal static class ApiError
    {
        public static async Task<string> ReadAsync(HttpResponseMessage response)
        {
            try
            {
                var body = await response.Content.ReadFromJsonAsync<JsonElement>();

                if (body.TryGetProperty("message", out var m) &&
                    m.GetString() is string text)
                {
                    return text;
                }
            }
            catch
            {
            }

            return $"Request failed ({(int)response.StatusCode}).";
        }
    }
}