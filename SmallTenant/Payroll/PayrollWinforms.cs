using Larp.domain.DTO;
using Larp.winforms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.Json;
using System.Drawing;

namespace Larp.winforms.SmallTenant.Payroll
{
    public partial class PayrollWinforms : Form
    {
        private readonly HttpClient _httpClient;
        private readonly int _companyId;

        public PayrollWinforms(
            HttpClient httpClient,
            int companyId)
        {
            InitializeComponent();

            _httpClient = httpClient;
            _companyId = companyId;

            AppTheme.Apply(this);

            StartPosition =
                FormStartPosition.CenterScreen;

            if (Session.Role == "Admin" || Session.Role == "SuperAdmin")
            {
                var btnRates = new Guna.UI2.WinForms.Guna2Button
                {
                    Text = "Contribution Rates",
                    BorderRadius = 8,
                    FillColor = Color.FromArgb(73, 83, 101),
                    Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold),
                    ForeColor = Color.White,
                    Location = new Point(700, 37),
                    Size = new Size(230, 53)
                };

                btnRates.Click += btnRates_Click;
                HeaderPanel.Controls.Add(btnRates);
            }

            Load += PayrollWinforms_Load;
            SearchPayroll.TextChanged += SearchPayroll_TextChanged;
            // AddPayroll.Click += AddPayroll_Click;
            PayrollGrid.CellContentClick += PayrollGrid_CellContentClick;
            PayrollGrid.CellPainting += PayrollGrid_CellPainting;
        }

        private void PayrollGrid_CellPainting(
    object sender,
    DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
                return;

            if (PayrollGrid.Columns[e.ColumnIndex].Name != "EditAction")
                return;

            if (PayrollGrid.Rows[e.RowIndex]
                .DataBoundItem is not PayrollRow row)
                return;

            if (row.Status != "Paid")
                return;

            e.PaintBackground(e.CellBounds, true);

            Rectangle buttonBounds = e.CellBounds;
            buttonBounds.Inflate(-4, -4);

            ControlPaint.DrawButton(
                e.Graphics,
                buttonBounds,
                ButtonState.Inactive);

            TextRenderer.DrawText(
                e.Graphics,
                "Edit",
                PayrollGrid.Font,
                buttonBounds,
                Color.Gray,
                TextFormatFlags.HorizontalCenter |
                TextFormatFlags.VerticalCenter);

            e.Handled = true;
        }

        private async void PayrollWinforms_Load(
            object sender,
            EventArgs e)
        {
            await LoadPayroll();
        }

        private async void SearchPayroll_TextChanged(
            object sender,
            EventArgs e)
        {
            await LoadPayroll(
                SearchPayroll.Text.Trim());
        }

        private async Task LoadPayroll(
            string search = "")
        {
            try
            {
                PayrollGrid.DataSource = null;
                PayrollGrid.Columns.Clear();

                var payrolls =
                    await _httpClient.GetFromJsonAsync<List<PayrollDto>>(
                        $"/tenant/{_companyId}/payroll");

                payrolls ??= new List<PayrollDto>();

                if (!string.IsNullOrWhiteSpace(search))
                {
                    payrolls = payrolls
                        .Where(x =>
                            x.EmployeeName.Contains(search,
                            StringComparison.OrdinalIgnoreCase)
                            ||
                            x.EmployeeCode.Contains(
                                search,
                                StringComparison.OrdinalIgnoreCase)
                            ||
                            x.EmployeeId
                                .ToString()
                                .Contains(search))
                        .ToList();
                }

                DisplayPayroll(payrolls);
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
                    $"Unable to load payroll.\n\n{ex.Message}",
                    "Payroll",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void DisplayPayroll(
            List<PayrollDto> payrolls)
        {
            PayrollGrid.AutoGenerateColumns = false;
            PayrollGrid.Columns.Clear();

            PayrollGrid.Columns.Add(
                new DataGridViewTextBoxColumn
                {
                    Name = "EmployeeCodeColumn",
                    HeaderText = "Employee ID",
                    DataPropertyName = "EmployeeCode",
                    Width = 130
                });

            PayrollGrid.Columns.Add(
                new DataGridViewTextBoxColumn
                {
                    Name = "EmployeeNameColumn",
                    HeaderText = "Employee Name",
                    DataPropertyName = "EmployeeName",
                    Width = 220
                });

            PayrollGrid.Columns.Add(
                new DataGridViewTextBoxColumn
                {
                    Name = "PositionColumn",
                    HeaderText = "Position",
                    DataPropertyName = "Position",
                    Width = 150
                });

            PayrollGrid.Columns.Add(
                new DataGridViewTextBoxColumn
                {
                    Name = "PayPeriodColumn",
                    HeaderText = "Pay Period",
                    DataPropertyName = "PayPeriod",
                    Width = 230
                });

            PayrollGrid.Columns.Add(
                new DataGridViewTextBoxColumn
                {
                    Name = "BasicSalaryColumn",
                    HeaderText = "Basic Salary",
                    DataPropertyName = "BasicSalary",
                    Width = 140,
                    DefaultCellStyle =
                    {
                Format = "₱#,##0.00"
                    }
                });

            PayrollGrid.Columns.Add(
                new DataGridViewTextBoxColumn
                {
                    Name = "NetPayColumn",
                    HeaderText = "Net Pay",
                    DataPropertyName = "NetPay",
                    Width = 140,
                    DefaultCellStyle =
                    {
                Format = "₱#,##0.00"
                    }
                });

            PayrollGrid.Columns.Add(
                new DataGridViewTextBoxColumn
                {
                    Name = "StatusColumn",
                    HeaderText = "Status",
                    DataPropertyName = "Status",
                    Width = 110
                });

            PayrollGrid.Columns.Add(
                new DataGridViewButtonColumn
                {
                    Name = "EditAction",
                    HeaderText = "",
                    Text = "Edit",
                    UseColumnTextForButtonValue = true,
                    Width = 80
                });

            PayrollGrid.Columns.Add(
                new DataGridViewButtonColumn
                {
                    Name = "DeleteAction",
                    HeaderText = "",
                    Text = "Delete",
                    UseColumnTextForButtonValue = true,
                    Width = 80
                });

            var rows = payrolls
                .Select(x => new PayrollRow
                {
                    PayrollId = x.PayrollId,
                    EmployeeId = x.EmployeeId,
                    EmployeeCode = x.EmployeeCode,
                    EmployeeName = x.EmployeeName,
                    Position = x.Position,
                    PayPeriodStart = x.PayPeriodStart,
                    PayPeriodEnd = x.PayPeriodEnd,
                    BasicSalary = x.BasicSalary,
                    Allowances = x.Allowances,
                    Deductions = x.Deductions,
                    SssEmployee = x.SssEmployee,
                    PhilHealthEmployee = x.PhilHealthEmployee,
                    PagibigEmployee = x.PagibigEmployee,
                    NetPay = x.NetPay,
                    Status = x.Status
                })
                .ToList();

            PayrollGrid.DataSource = rows;

            PayrollGrid.AutoSizeColumnsMode =
                DataGridViewAutoSizeColumnsMode.Fill;

            PayrollGrid.SelectionMode =
                DataGridViewSelectionMode.FullRowSelect;

            PayrollGrid.MultiSelect = false;

            PayrollGrid.ReadOnly = true;

            PayrollGrid.AllowUserToAddRows = false;

            PayrollGrid.AllowUserToDeleteRows = false;
        }

        private async void PayrollGrid_CellContentClick(
            object sender,
            DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
                return;

            if (PayrollGrid.Rows[e.RowIndex]
                .DataBoundItem is not PayrollRow row)
                return;

            string columnName =
                PayrollGrid.Columns[e.ColumnIndex].Name;

            if (columnName == "EditAction")
            {
                if (row.Status == "Paid")
                    return;

                await EditPayroll(row);
            }
            else if (columnName == "DeleteAction")
            {
                await DeletePayroll(row);
            }
        }

        private async Task EditPayroll(
            PayrollRow row)
        {
            using var form =
                new EditPayrollWinforms(
                    row.EmployeeId,
                    row.EmployeeName,
                    row.PayPeriodStart,
                    row.PayPeriodEnd,
                    row.BasicSalary,
                    row.Allowances,
                    row.Deductions,
                    row.Status);

            if (form.ShowDialog() != DialogResult.OK)
                return;

            var updatedPayroll =
                new PayrollDto
                {
                    PayrollId = row.PayrollId,
                    EmployeeId = form.EmployeeIdValue,
                    EmployeeCode = row.EmployeeCode,
                    EmployeeName = form.EmployeeNameValue,
                    Position = row.Position,
                    PayPeriodStart = form.PayPeriodStartValue,
                    PayPeriodEnd = form.PayPeriodEndValue,
                    BasicSalary = form.BasicSalaryValue,
                    Allowances = form.AllowancesValue,
                    Deductions = form.DeductionsValue,
                    Status = form.StatusValue
                };

            try
            {
                var response =
                    await _httpClient.PutAsJsonAsync(
                        $"/tenant/{_companyId}/payroll/{row.PayrollId}",
                        new
                        {
                            EmployeeId =
                                updatedPayroll.EmployeeId,

                            PayPeriodStart =
                                updatedPayroll.PayPeriodStart,

                            PayPeriodEnd =
                                updatedPayroll.PayPeriodEnd,

                            BasicSalary =
                                updatedPayroll.BasicSalary,

                            Allowances =
                                updatedPayroll.Allowances,

                            Deductions =
                                updatedPayroll.Deductions,

                            Status =
                                updatedPayroll.Status
                        });

                if (!response.IsSuccessStatusCode)
                {
                    var message =
                        await ReadMessageAsync(response);

                    MessageBox.Show(
                        $"Unable to update payroll.\n\n{message}",
                        "Payroll",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);

                    return;
                }

                MessageBox.Show(
                    "Payroll updated successfully.",
                    "Payroll",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                await LoadPayroll(
                    SearchPayroll.Text.Trim());
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Unable to update payroll.\n\n{ex.Message}",
                    "Payroll",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private async Task DeletePayroll(
            PayrollRow row)
        {
            var result =
                MessageBox.Show(
                    $"Are you sure you want to permanently delete the payroll record for '{row.EmployeeName}'?",
                    "Delete Payroll",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

            if (result != DialogResult.Yes)
                return;

            try
            {
                var response =
                    await _httpClient.DeleteAsync(
                        $"/tenant/{_companyId}/payroll/{row.PayrollId}");

                if (!response.IsSuccessStatusCode)
                {
                    var message =
                        await ReadMessageAsync(response);

                    MessageBox.Show(
                        $"Unable to delete payroll.\n\n{message}",
                        "Delete Payroll",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);

                    return;
                }

                MessageBox.Show(
                    "Payroll deleted successfully.",
                    "Delete Payroll",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                await LoadPayroll(
                    SearchPayroll.Text.Trim());
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Unable to delete payroll.\n\n{ex.Message}",
                    "Delete Payroll",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void btnRates_Click(object? sender, EventArgs e)
        {
            using var form = new ContributionRatesWinforms(_httpClient, _companyId);
            form.ShowDialog(this);
        }

        private void AddPayroll_Click(
            object sender,
            EventArgs e)
        {
            using var form =
                new AddPayrollWinforms(
                    _httpClient,
                    _companyId);

            if (form.ShowDialog() == DialogResult.OK)
            {
                _ = LoadPayroll(
                    SearchPayroll.Text.Trim());
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
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

    }

    public class PayrollRow
    {
        public int PayrollId { get; set; }

        public int EmployeeId { get; set; }

        public string EmployeeCode { get; set; } =
            string.Empty;

        public string EmployeeName { get; set; } =
            string.Empty;

        public string Position { get; set; } =
            string.Empty;

        public DateTime PayPeriodStart { get; set; }

        public DateTime PayPeriodEnd { get; set; }

        public decimal SssEmployee { get; set; }
        public decimal PhilHealthEmployee { get; set; }
        public decimal PagibigEmployee { get; set; }

        public string PayPeriod =>
            $"{PayPeriodStart:MMM dd, yyyy} - " +
            $"{PayPeriodEnd:MMM dd, yyyy}";

        public decimal BasicSalary { get; set; }

        public decimal Allowances { get; set; }

        public decimal Deductions { get; set; }

        public decimal NetPay { get; set; }

        public string Status { get; set; } =
            "Pending";
    }
}