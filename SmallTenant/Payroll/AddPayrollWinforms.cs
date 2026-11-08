using Larp.domain.DTO;
using Larp.domain.Entities;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Larp.winforms.SmallTenant.Payroll
{
    public partial class AddPayrollWinforms : Form
    {
        private readonly HttpClient _httpClient;
        private readonly int _companyId;

        private List<EmployeeEntity> _employees = new();
        private List<PayrollDto> _payrolls = new();

        // Latest answer from the preview endpoint. Save is only allowed when it is ready.
        private PayrollPreviewDto? _preview;
        private int _previewVersion;
        private readonly System.Windows.Forms.Timer _debounce = new() { Interval = 400 };

        public AddPayrollWinforms(
            HttpClient httpClient,
            int companyId)
        {
            InitializeComponent();

            _httpClient = httpClient;
            _companyId = companyId;

            AppTheme.Apply(this);

            StartPosition =
                FormStartPosition.CenterScreen;

            lblStatusNote.Text =
                Session.Role == "Manager"
                    ? "Status: Pending. An Admin must approve it before it counts."
                    : "Status: Approved when saved.";

            // Salary and deductions come from the server (Benefits + contribution rates).
            numBasic.Enabled = false;
            numDeduct.Enabled = false;
            BasicSalaryLabel.Text = "Basic Salary (this period)";
            DeductionsLabel.Text = "Deductions (SSS, PhilHealth, Pag-IBIG)";
            btnSave.Enabled = false;

            SetDefaultPeriod();
            UpdateNetPay();

            // The designer line for this event is commented out, so it is wired here.
            cmbEmployee.SelectedIndexChanged += cmbEmployee_SelectedIndexChanged;
            dtpStart.ValueChanged += (_, _) => SchedulePreview();
            dtpEnd.ValueChanged += (_, _) => SchedulePreview();

            _debounce.Tick += async (_, _) =>
            {
                _debounce.Stop();
                await RefreshPreviewAsync();
            };

            FormClosed += (_, _) => _debounce.Dispose();

            Shown += async (_, _) => await LoadEmployeesAsync();
        }

        // Default to the current half of the month (1st-15th or 16th-end).
        private void SetDefaultPeriod()
        {
            DateTime today = DateTime.Today;

            if (today.Day <= 15)
            {
                dtpStart.Value = new DateTime(today.Year, today.Month, 1);
                dtpEnd.Value = new DateTime(today.Year, today.Month, 15);
            }
            else
            {
                dtpStart.Value = new DateTime(today.Year, today.Month, 16);
                dtpEnd.Value = new DateTime(
                    today.Year,
                    today.Month,
                    DateTime.DaysInMonth(today.Year, today.Month));
            }
        }

        // The cut-off that follows the last one: 1st-15th -> 16th-end, 16th-end -> next 1st-15th.
        private static (DateTime Start, DateTime End) NextPeriodAfter(DateTime lastEnd)
        {
            if (lastEnd.Day < 16)
            {
                return (
                    new DateTime(lastEnd.Year, lastEnd.Month, 16),
                    new DateTime(
                        lastEnd.Year,
                        lastEnd.Month,
                        DateTime.DaysInMonth(lastEnd.Year, lastEnd.Month)));
            }

            var first = new DateTime(lastEnd.Year, lastEnd.Month, 1).AddMonths(1);

            return (first, new DateTime(first.Year, first.Month, 15));
        }

        private async Task LoadEmployeesAsync()
        {
            try
            {
                var employees =
                    await _httpClient.GetFromJsonAsync<List<EmployeeEntity>>(
                        $"tenant/{_companyId}/employees")
                    ?? new List<EmployeeEntity>();

                _employees = employees
                    .Where(x => x.IsActive && x.ApprovalStatus == "Approved")
                    .OrderBy(x => x.LastName)
                    .ThenBy(x => x.FirstName)
                    .ToList();

                // Used for the next-period suggestion and the overlap warning. If it fails, carry on.
                try
                {
                    _payrolls =
                        await _httpClient.GetFromJsonAsync<List<PayrollDto>>(
                            $"tenant/{_companyId}/payroll")
                        ?? new List<PayrollDto>();
                }
                catch
                {
                    _payrolls = new List<PayrollDto>();
                }

                cmbEmployee.Items.Clear();

                foreach (var employee in _employees)
                {
                    cmbEmployee.Items.Add(new EmployeeChoice(employee));
                }

                cmbEmployee.SelectedIndex = -1;

                if (_employees.Count == 0)
                {
                    lblHint.Text =
                        "No active, approved employees are available.";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Unable to load employees.\n\n{ex.Message}",
                    "Payroll",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void cmbEmployee_SelectedIndexChanged(
            object? sender,
            EventArgs e)
        {
            if (cmbEmployee.SelectedItem is not EmployeeChoice choice)
            {
                txtEmployeeCode.Text = "";
                txtPosition.Text = "";
                txtContact.Text = "";
                txtDateHired.Text = "";

                _ = RefreshPreviewAsync();
                return;
            }

            EmployeeEntity employee = choice.Employee;

            txtEmployeeCode.Text = employee.EmployeeCode;
            txtPosition.Text = employee.Position;

            txtContact.Text =
                string.IsNullOrWhiteSpace(employee.ContactNumber)
                    ? "-"
                    : employee.ContactNumber;

            txtDateHired.Text =
                employee.DateHired.ToLocalTime().ToString("MMM dd, yyyy");

            // Suggest the next cut-off after this employee's last payroll.
            var last = _payrolls
                .Where(p =>
                    p.EmployeeId == employee.EmployeeId &&
                    p.Status != "Cancelled" &&
                    p.Status != "Rejected")
                .OrderByDescending(p => p.PayPeriodEnd)
                .FirstOrDefault();

            if (last != null)
            {
                var (start, end) = NextPeriodAfter(last.PayPeriodEnd.Date);

                dtpStart.Value = start;
                dtpEnd.Value = end;
            }
            else
            {
                SetDefaultPeriod();
            }

            _debounce.Stop();
            _ = RefreshPreviewAsync();
        }

        private void NumericValueChanged(
            object? sender,
            EventArgs e)
        {
            UpdateNetPay();

            // Only a change to allowances needs a new answer from the server.
            if (ReferenceEquals(sender, numAllow))
            {
                SchedulePreview();
            }
        }

        // Wait a moment after the last change, then ask the server.
        private void SchedulePreview()
        {
            btnSave.Enabled = false;

            _debounce.Stop();
            _debounce.Start();
        }

        private static string Iso(DateTime date) =>
            date.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);

        private async Task RefreshPreviewAsync()
        {
            int version = ++_previewVersion;

            _preview = null;
            btnSave.Enabled = false;

            if (cmbEmployee.SelectedItem is not EmployeeChoice choice)
            {
                ShowAmounts(0, 0);
                lblHint.Text = "Choose an employee to see their salary and deductions.";
                return;
            }

            try
            {
                string url =
                    $"tenant/{_companyId}/payroll/preview" +
                    $"?employeeId={choice.Employee.EmployeeId}" +
                    $"&start={Iso(dtpStart.Value.Date)}" +
                    $"&end={Iso(dtpEnd.Value.Date)}" +
                    $"&allowances={numAllow.Value.ToString(CultureInfo.InvariantCulture)}";

                var preview =
                    await _httpClient.GetFromJsonAsync<PayrollPreviewDto>(url);

                // A newer request has started: ignore this answer.
                if (version != _previewVersion || preview == null)
                    return;

                _preview = preview;

                if (!preview.Ready)
                {
                    ShowAmounts(0, 0);
                    lblHint.Text =
                        preview.Message ?? "The amounts could not be worked out.";
                    return;
                }

                ShowAmounts(preview.BasicSalary, preview.Deductions);

                lblHint.Text =
                    $"Deductions: SSS ₱{preview.SssEmployee:N2} | " +
                    $"PhilHealth ₱{preview.PhilHealthEmployee:N2} | " +
                    $"Pag-IBIG ₱{preview.PagibigEmployee:N2}";

                btnSave.Enabled = true;
            }
            catch (Exception ex)
            {
                if (version != _previewVersion)
                    return;

                ShowAmounts(0, 0);
                lblHint.Text = "Could not work out the amounts. " + ex.Message;
            }
        }

        private void ShowAmounts(decimal basic, decimal deductions)
        {
            numBasic.Value = Math.Min(basic, numBasic.Maximum);
            numDeduct.Value = Math.Min(deductions, numDeduct.Maximum);

            UpdateNetPay();
        }

        private void UpdateNetPay()
        {
            decimal net =
                numBasic.Value +
                numAllow.Value -
                numDeduct.Value;

            txtNetPay.Text =
                net < 0
                    ? "-" + Math.Abs(net).ToString("₱#,##0.00")
                    : net.ToString("₱#,##0.00");

            txtNetPay.ForeColor =
                net < 0
                    ? AppTheme.Danger
                    : AppTheme.TextPrimary;
        }

        private async void btnSave_Click(
            object? sender,
            EventArgs e)
        {
            if (cmbEmployee.SelectedItem is not EmployeeChoice choice)
            {
                MessageBox.Show(
                    "Select an employee first.",
                    "Payroll",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            EmployeeEntity employee = choice.Employee;

            DateTime start = dtpStart.Value.Date;
            DateTime end = dtpEnd.Value.Date;

            if (end < start)
            {
                MessageBox.Show(
                    "Pay period end cannot be earlier than pay period start.",
                    "Payroll",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            if (_preview is not { Ready: true })
            {
                MessageBox.Show(
                    _preview?.Message ?? "Wait for the amounts to load, then try again.",
                    "Payroll",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            var overlap = _payrolls.FirstOrDefault(p =>
                p.EmployeeId == employee.EmployeeId &&
                p.Status != "Cancelled" &&
                p.Status != "Rejected" &&
                p.PayPeriodStart.Date <= end &&
                p.PayPeriodEnd.Date >= start);

            if (overlap != null)
            {
                var answer = MessageBox.Show(
                    "This employee already has a payroll for " +
                    $"{overlap.PayPeriodStart:MMM dd, yyyy} - " +
                    $"{overlap.PayPeriodEnd:MMM dd, yyyy}.\n\n" +
                    "Save another one anyway?",
                    "Payroll",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (answer != DialogResult.Yes)
                {
                    return;
                }
            }

            try
            {
                btnSave.Enabled = false;

                // Salary and deductions are worked out again by the server when it saves.
                var response =
                    await _httpClient.PostAsJsonAsync(
                        $"tenant/{_companyId}/payroll",
                        new
                        {
                            employeeId = employee.EmployeeId,
                            payPeriodStart = start,
                            payPeriodEnd = end,
                            allowances = numAllow.Value
                        });

                if (!response.IsSuccessStatusCode)
                {
                    MessageBox.Show(
                        await ReadMessageAsync(response),
                        "Unable to save payroll",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    return;
                }

                MessageBox.Show(
                    Session.Role == "Manager"
                        ? "Payroll saved. An Admin must approve it before it counts."
                        : "Payroll saved and approved.",
                    "Payroll",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                DialogResult = DialogResult.OK;
                Close();
            }
            catch (HttpRequestException ex)
            {
                MessageBox.Show(
                    $"Unable to connect to the API.\n\n{ex.Message}",
                    "Connection Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Unable to save payroll.\n\n{ex.Message}",
                    "Payroll",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            finally
            {
                btnSave.Enabled = _preview is { Ready: true };
            }
        }

        private void btnCancel_Click(
            object? sender,
            EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
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

        // Item shown in the employee drop-down.
        private class EmployeeChoice
        {
            public EmployeeEntity Employee { get; }

            public EmployeeChoice(EmployeeEntity employee)
            {
                Employee = employee;
            }

            public override string ToString() =>
                $"{Employee.EmployeeCode} - {Employee.FirstName} {Employee.LastName}  ({Employee.Position})";
        }
    }
}