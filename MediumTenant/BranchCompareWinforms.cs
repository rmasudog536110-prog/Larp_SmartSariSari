using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Larp.winforms.MediumTenant
{
    /// <summary>
    /// Every branch plus Unassigned side by side. Double-click a row to open its
    /// details. The last row is the total, checked against the company income statement.
    /// Built in code, no designer file.
    /// </summary>
    [System.ComponentModel.DesignerCategory("Code")]
    public class BranchCompareWinForms : Form
    {
        private const string MoneyFmt = "₱#,##0.00";

        private readonly HttpClient _http;
        private readonly int _companyId;

        private readonly Guna2DataGridView _grid = new();
        private readonly Guna2DateTimePicker _from = new();
        private readonly Guna2DateTimePicker _to = new();
        private readonly Label _lblCheck = new();

        private List<CompareRow> _rows = new();

        public BranchCompareWinForms(HttpClient http, int companyId)
        {
            _http = http;
            _companyId = companyId;

            Text = "Compare Branches";
            StartPosition = FormStartPosition.CenterParent;
            ClientSize = new Size(1250, 560);
            MinimumSize = new Size(950, 400);
            BackColor = Color.FromArgb(245, 242, 237);

            BuildUi();
            AppTheme.Apply(this);

            Shown += async (_, _) => await LoadAsync();
        }

        private void BuildUi()
        {
            ConfigureGrid();

            _lblCheck.Dock = DockStyle.Bottom;
            _lblCheck.Height = 36;
            _lblCheck.TextAlign = ContentAlignment.MiddleLeft;
            _lblCheck.Padding = new Padding(12, 0, 0, 0);
            _lblCheck.Font = new Font("Segoe UI Semibold", 9.5F);
            _lblCheck.BackColor = Color.White;

            var filters = new FlowLayoutPanel
            {
                Dock = DockStyle.Top,
                Height = 64,
                Padding = new Padding(12, 12, 12, 0),
                BackColor = Color.Transparent
            };

            _from.Format = DateTimePickerFormat.Short;
            _from.BorderRadius = 8;
            _from.Size = new Size(140, 36);
            _from.Value = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);

            _to.Format = DateTimePickerFormat.Short;
            _to.BorderRadius = 8;
            _to.Size = new Size(140, 36);
            _to.Value = DateTime.Today;

            var apply = new Guna2Button
            {
                Text = "Apply",
                BorderRadius = 8,
                FillColor = Color.FromArgb(230, 165, 45),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                Size = new Size(90, 36),
                Margin = new Padding(10, 0, 0, 0)
            };

            var close = new Guna2Button
            {
                Text = "Close",
                BorderRadius = 8,
                FillColor = Color.FromArgb(210, 75, 70),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                Size = new Size(90, 36),
                Margin = new Padding(10, 0, 0, 0)
            };

            apply.Click += async (_, _) => await LoadAsync();
            close.Click += (_, _) => Close();

            filters.Controls.Add(Caption("From"));
            filters.Controls.Add(_from);
            filters.Controls.Add(Caption("To"));
            filters.Controls.Add(_to);
            filters.Controls.Add(apply);
            filters.Controls.Add(close);

            Controls.Add(_grid);
            Controls.Add(_lblCheck);
            Controls.Add(filters);
        }

        private static Label Caption(string text) => new()
        {
            Text = text,
            AutoSize = true,
            Font = new Font("Segoe UI", 10F),
            Margin = new Padding(8, 10, 4, 0)
        };

        private void ConfigureGrid()
        {
            _grid.Dock = DockStyle.Fill;
            _grid.AutoGenerateColumns = false;
            _grid.AllowUserToAddRows = false;
            _grid.AllowUserToDeleteRows = false;
            _grid.AllowUserToResizeRows = false;
            _grid.ReadOnly = true;
            _grid.RowHeadersVisible = false;
            _grid.MultiSelect = false;
            _grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            _grid.ColumnHeadersHeight = 40;
            _grid.RowTemplate.Height = 36;
            _grid.ThemeStyle.HeaderStyle.BackColor = Color.FromArgb(28, 58, 42);
            _grid.ThemeStyle.HeaderStyle.ForeColor = Color.White;
            _grid.ThemeStyle.HeaderStyle.Height = 40;

            void Add(string prop, string header, int width, string? format = null, bool right = false)
            {
                var col = new DataGridViewTextBoxColumn
                {
                    Name = prop,
                    DataPropertyName = prop,
                    HeaderText = header
                };

                if (width == 0) col.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                else col.Width = width;

                if (format != null) col.DefaultCellStyle.Format = format;
                if (right) col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

                _grid.Columns.Add(col);
            }

            Add("BranchName", "Branch", 0);
            Add("Transactions", "Sales", 70, "N0", true);
            Add("GrossSales", "Gross Sales", 125, MoneyFmt, true);
            Add("VatAmount", "VAT", 105, MoneyFmt, true);
            Add("NetSales", "Net Sales", 125, MoneyFmt, true);
            Add("Purchases", "Purchases", 115, MoneyFmt, true);
            Add("Expenses", "Expenses", 115, MoneyFmt, true);
            Add("Payroll", "Payroll", 115, MoneyFmt, true);
            Add("TotalCosts", "Total Costs", 125, MoneyFmt, true);
            Add("Profit", "Profit", 125, MoneyFmt, true);
            Add("SharePercent", "Share", 75, "0.0'%'", true);

            _grid.CellDoubleClick += (_, e) =>
            {
                if (e.RowIndex < 0) return;
                if (_grid.Rows[e.RowIndex].DataBoundItem is not CompareRow row) return;
                if (row.IsTotal) return;

                using var details = new BranchDetailsWinForms(
                    _http, _companyId, row.BranchId, row.BranchName);

                details.ShowDialog(this);
            };
        }

        private static string Iso(DateTime d) =>
            d.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);

        private static string Money(decimal v) =>
            v.ToString(MoneyFmt, CultureInfo.InvariantCulture);

        private async Task LoadAsync()
        {
            if (_to.Value.Date < _from.Value.Date)
            {
                MessageBox.Show("The end date cannot be earlier than the start date.",
                    "Compare Branches", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                UseWaitCursor = true;
                _lblCheck.Text = "";

                string range = $"startDate={Iso(_from.Value)}&endDate={Iso(_to.Value)}";

                var response = await _http.GetAsync(
                    $"tenant/{_companyId}/branches/compare?{range}");

                if (!response.IsSuccessStatusCode)
                {
                    MessageBox.Show(await BranchesWinForms.ReadMessageAsync(response),
                        "Compare Branches", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                var data = await response.Content.ReadFromJsonAsync<CompareDto>()
                           ?? new CompareDto();

                _rows = new List<CompareRow>(data.Rows) { data.Total };
                data.Total.IsTotal = true;

                _grid.DataSource = null;
                _grid.DataSource = _rows;

                if (_grid.Rows.Count > 0)
                {
                    var last = _grid.Rows[_grid.Rows.Count - 1];
                    last.DefaultCellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
                    last.DefaultCellStyle.BackColor = Color.FromArgb(240, 243, 241);
                }

                await CheckAgainstIncomeStatementAsync(data.Total.Profit, range);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Unable to load the comparison.\n\n{ex.Message}",
                    "Compare Branches", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                UseWaitCursor = false;
            }
        }

        // All branches plus Unassigned should equal the company income statement.
        private async Task CheckAgainstIncomeStatementAsync(decimal branchTotalProfit, string range)
        {
            try
            {
                var statement = await _http.GetFromJsonAsync<IncomeStatementLite>(
                    $"tenant/{_companyId}/reports/income-statement?{range}");

                if (statement == null) return;

                decimal diff = branchTotalProfit - statement.NetIncome;

                if (Math.Abs(diff) < 0.01m)
                {
                    _lblCheck.ForeColor = Color.FromArgb(46, 107, 69);
                    _lblCheck.Text =
                        $"Reconciled: branch total profit equals the company income statement ({Money(statement.NetIncome)}).";
                }
                else
                {
                    _lblCheck.ForeColor = Color.Firebrick;
                    _lblCheck.Text =
                        $"Does NOT reconcile: branches {Money(branchTotalProfit)} vs income statement {Money(statement.NetIncome)} (difference {Money(diff)}).";
                }
            }
            catch
            {
                _lblCheck.Text = "";
            }
        }

        private class CompareDto
        {
            public List<CompareRow> Rows { get; set; } = new();
            public CompareRow Total { get; set; } = new();
        }

        private class CompareRow
        {
            public int? BranchId { get; set; }
            public string BranchName { get; set; } = string.Empty;
            public int Transactions { get; set; }
            public decimal GrossSales { get; set; }
            public decimal VatAmount { get; set; }
            public decimal NetSales { get; set; }
            public decimal Purchases { get; set; }
            public decimal Expenses { get; set; }
            public decimal Payroll { get; set; }
            public decimal TotalCosts { get; set; }
            public decimal Profit { get; set; }
            public decimal SharePercent { get; set; }
            public bool IsTotal { get; set; }
        }

        private class IncomeStatementLite
        {
            public decimal NetIncome { get; set; }
        }
    }
}