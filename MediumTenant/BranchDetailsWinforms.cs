using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Printing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Larp.winforms.MediumTenant
{
    /// <summary>
    /// Full-screen branch page: header, KPI cards and one tab per record type.
    /// Built in code (no designer file) so Visual Studio cannot rewrite the layout.
    /// branchId = null opens the "Unassigned" bucket.
    /// </summary>
    [System.ComponentModel.DesignerCategory("Code")]
    public class BranchDetailsWinForms : Form
    {
        private const string MoneyFmt = "₱#,##0.00";
        private static readonly Color Ink = Color.FromArgb(28, 40, 63);
        private static readonly Color Muted = Color.FromArgb(110, 118, 130);
        private static readonly Color Green = Color.FromArgb(46, 107, 69);
        private static readonly Color Amber = Color.FromArgb(230, 165, 45);
        private static readonly Color Slate = Color.FromArgb(52, 73, 94);

        private static readonly string[] KpiTitles =
        {
            "Gross Sales", "VAT (in gross)", "Net Sales", "Transactions",
            "Items Sold", "Average Sale", "Total Costs", "Profit"
        };

        private readonly HttpClient _http;
        private readonly int _companyId;
        private readonly string _branchKey;
        private string _branchName;

        private readonly Label lblTitle = new();
        private readonly Label lblSub = new();
        private readonly Guna2DateTimePicker dtpFrom = new();
        private readonly Guna2DateTimePicker dtpTo = new();
        private Guna2Button btnApply = null!, btnPdf = null!, btnClose = null!;

        private readonly Dictionary<string, Label> _kpi = new();
        private readonly TabControl _tabs = new();
        private readonly Dictionary<string, Guna2DataGridView> _grids = new();
        private readonly Dictionary<string, Label> _totals = new();
        private readonly HashSet<string> _loaded = new();
        private readonly Dictionary<string, object> _cache = new();

        private readonly ChartPanel pTrend = new() { Title = "Sales trend" };
        private readonly ChartPanel pTop = new() { Title = "Top products (by amount)" };
        private readonly ChartPanel pEmp = new() { Title = "Sales by employee" };
        private readonly ChartPanel pCost = new() { Title = "Cost breakdown" };

        private SummaryDto? _summary;
        private List<TrendPoint> _trend = new();
        private List<TopProduct> _top = new();
        private List<EmployeeRow> _employees = new();

        public BranchDetailsWinForms(HttpClient http, int companyId, int? branchId, string branchName)
        {
            _http = http;
            _companyId = companyId;
            _branchKey = branchId.HasValue
                ? branchId.Value.ToString(CultureInfo.InvariantCulture)
                : "unassigned";
            _branchName = branchName;

            Text = $"Branch Details - {branchName}";
            StartPosition = FormStartPosition.CenterScreen;
            WindowState = FormWindowState.Maximized;
            MinimumSize = new Size(1000, 650);
            BackColor = Color.FromArgb(245, 242, 237);

            BuildUi();
            AppTheme.Apply(this);

            Shown += async (_, _) => await ReloadAsync();
        }

        // ================= UI =================

        private void BuildUi()
        {
            pTrend.Draw = (g, a) => DrawTrend(g, a, _trend);

            pTop.Draw = (g, a) => DrawBars(g, a,
                _top.Select(x => (Label: x.ProductName, Value: x.Amount)).ToList(),
                Green, "No products sold in this range");

            pEmp.Draw = (g, a) => DrawBars(g, a,
                _employees.Where(x => x.SalesTotal > 0)
                          .OrderByDescending(x => x.SalesTotal)
                          .Take(8)
                          .Select(x => (Label: x.Name, Value: x.SalesTotal))
                          .ToList(),
                Slate, "No sales by employees in this range");

            pCost.Draw = DrawCost;

            _tabs.Dock = DockStyle.Fill;
            _tabs.Font = new Font("Segoe UI", 10F);
            _tabs.Padding = new Point(18, 6);

            _tabs.TabPages.Add(MakeOverviewPage());

            _tabs.TabPages.Add(MakeGridPage("Sales",
                new Col("SaleNumber", "Sale No.", 150),
                new Col("SaleDate", "Date", 160, "MMM dd, yyyy h:mm tt"),
                new Col("Cashier", "Cashier", 0),
                new Col("ItemCount", "Items", 70, "0.##", true),
                new Col("Subtotal", "Subtotal", 115, MoneyFmt, true),
                new Col("Discount", "Discount", 105, MoneyFmt, true),
                new Col("VatAmount", "VAT", 105, MoneyFmt, true),
                new Col("TotalAmount", "Total", 125, MoneyFmt, true),
                new Col("Status", "Status", 100)));

            _tabs.TabPages.Add(MakeGridPage("Purchases",
                new Col("PurchaseNumber", "Purchase No.", 170),
                new Col("PurchaseDate", "Date", 160, "MMM dd, yyyy h:mm tt"),
                new Col("Supplier", "Supplier", 0),
                new Col("Discount", "Discount", 115, MoneyFmt, true),
                new Col("TotalAmount", "Total", 130, MoneyFmt, true),
                new Col("Status", "Status", 110)));

            _tabs.TabPages.Add(MakeGridPage("Expenses",
                new Col("ExpenseDate", "Date", 120, "MMM dd, yyyy"),
                new Col("Category", "Category", 160),
                new Col("Description", "Description", 0),
                new Col("Amount", "Amount", 125, MoneyFmt, true),
                new Col("PaymentMethod", "Payment", 120),
                new Col("Status", "Status", 100)));

            _tabs.TabPages.Add(MakeGridPage("Employees",
                new Col("EmployeeCode", "Code", 110),
                new Col("Name", "Name", 0),
                new Col("Position", "Position", 170),
                new Col("Status", "Status", 110),
                new Col("SalesCount", "Sales", 80, "N0", true),
                new Col("SalesTotal", "Sales Total", 140, MoneyFmt, true)));

            _tabs.TabPages.Add(MakeGridPage("Payroll",
                new Col("Employee", "Employee", 0),
                new Col("PeriodStart", "Period Start", 120, "MMM dd, yyyy"),
                new Col("PeriodEnd", "Period End", 120, "MMM dd, yyyy"),
                new Col("Gross", "Basic + Allowances", 155, MoneyFmt, true),
                new Col("Deductions", "Deductions", 120, MoneyFmt, true),
                new Col("NetPay", "Net Pay", 125, MoneyFmt, true),
                new Col("Status", "Status", 100)));

            _tabs.SelectedIndexChanged += async (_, _) => await EnsureTabAsync();

            // KPI strip
            var kpi = new TableLayoutPanel
            {
                Dock = DockStyle.Top,
                Height = 92,
                ColumnCount = KpiTitles.Length,
                RowCount = 1,
                Padding = new Padding(10, 8, 10, 4),
                BackColor = Color.Transparent
            };

            for (int i = 0; i < KpiTitles.Length; i++)
                kpi.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f / KpiTitles.Length));

            kpi.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));

            foreach (var title in KpiTitles)
                kpi.Controls.Add(MakeCard(title));

            // Header
            var header = new Panel
            {
                Dock = DockStyle.Top,
                Height = 96,
                BackColor = Color.FromArgb(28, 58, 42)
            };

            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI Semibold", 20F, FontStyle.Bold);
            lblTitle.ForeColor = Color.White;
            lblTitle.Location = new Point(24, 12);
            lblTitle.Text = _branchName;

            lblSub.AutoSize = true;
            lblSub.Font = new Font("Segoe UI", 9.5F);
            lblSub.ForeColor = Color.FromArgb(200, 215, 205);
            lblSub.Location = new Point(26, 58);

            var flow = new FlowLayoutPanel
            {
                Dock = DockStyle.Right,
                Width = 800,
                Padding = new Padding(0, 28, 16, 0),
                FlowDirection = FlowDirection.LeftToRight,
                WrapContents = false,
                BackColor = Color.Transparent
            };

            dtpFrom.Format = DateTimePickerFormat.Short;
            dtpFrom.BorderRadius = 8;
            dtpFrom.Size = new Size(135, 36);
            dtpFrom.Value = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);

            dtpTo.Format = DateTimePickerFormat.Short;
            dtpTo.BorderRadius = 8;
            dtpTo.Size = new Size(135, 36);
            dtpTo.Value = DateTime.Today;

            btnApply = HeaderButton("Apply", Amber, 90);
            btnPdf = HeaderButton("Export PDF", Slate, 120);
            btnClose = HeaderButton("Close", Color.FromArgb(210, 75, 70), 90);

            btnApply.Click += async (_, _) => await ReloadAsync();
            btnPdf.Click += async (_, _) => await ExportPdfAsync();
            btnClose.Click += (_, _) => Close();

            flow.Controls.Add(HeaderCaption("From"));
            flow.Controls.Add(dtpFrom);
            flow.Controls.Add(HeaderCaption("To"));
            flow.Controls.Add(dtpTo);
            flow.Controls.Add(btnApply);
            flow.Controls.Add(btnPdf);
            flow.Controls.Add(btnClose);

            header.Controls.Add(lblTitle);
            header.Controls.Add(lblSub);
            header.Controls.Add(flow);

            // Fill first, then the docked edges (the last one added is docked first).
            Controls.Add(_tabs);
            Controls.Add(kpi);
            Controls.Add(header);
        }

        private static Label HeaderCaption(string text) => new()
        {
            Text = text,
            AutoSize = true,
            ForeColor = Color.White,
            Font = new Font("Segoe UI", 10F),
            Margin = new Padding(10, 10, 4, 0)
        };

        private static Guna2Button HeaderButton(string text, Color fill, int width) => new()
        {
            Text = text,
            BorderRadius = 8,
            FillColor = fill,
            ForeColor = Color.White,
            Font = new Font("Segoe UI", 10F, FontStyle.Bold),
            Size = new Size(width, 36),
            Margin = new Padding(10, 0, 0, 0)
        };

        private Guna2Panel MakeCard(string title)
        {
            var card = new Guna2Panel
            {
                Dock = DockStyle.Fill,
                Margin = new Padding(5),
                BorderRadius = 10,
                FillColor = Color.White,
                BackColor = Color.Transparent
            };

            var lblTitleCard = new Label
            {
                Text = title,
                AutoSize = true,
                Font = new Font("Segoe UI", 8.5F),
                ForeColor = Muted,
                BackColor = Color.Transparent,
                Location = new Point(12, 10)
            };

            var lblValue = new Label
            {
                Text = "-",
                AutoSize = true,
                Font = new Font("Segoe UI", 13F, FontStyle.Bold),
                ForeColor = Ink,
                BackColor = Color.Transparent,
                Location = new Point(12, 36)
            };

            card.Controls.Add(lblTitleCard);
            card.Controls.Add(lblValue);
            _kpi[title] = lblValue;

            return card;
        }

        private TabPage MakeOverviewPage()
        {
            var page = new TabPage("Overview")
            {
                Name = "Overview",
                BackColor = Color.FromArgb(245, 242, 237)
            };

            var t = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 2,
                RowCount = 2,
                Padding = new Padding(4)
            };

            t.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
            t.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
            t.RowStyles.Add(new RowStyle(SizeType.Percent, 50));
            t.RowStyles.Add(new RowStyle(SizeType.Percent, 50));

            foreach (var p in new[] { pTrend, pTop, pEmp, pCost })
            {
                p.Dock = DockStyle.Fill;
                p.Margin = new Padding(6);
            }

            t.Controls.Add(pTrend, 0, 0);
            t.Controls.Add(pTop, 1, 0);
            t.Controls.Add(pEmp, 0, 1);
            t.Controls.Add(pCost, 1, 1);

            page.Controls.Add(t);
            return page;
        }

        private readonly record struct Col(
            string Prop, string Header, int Width = 0, string? Format = null, bool Right = false);

        private TabPage MakeGridPage(string name, params Col[] cols)
        {
            var page = new TabPage(name) { Name = name, BackColor = Color.White };

            var grid = new Guna2DataGridView
            {
                Dock = DockStyle.Fill,
                AutoGenerateColumns = false,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                AllowUserToResizeRows = false,
                ReadOnly = true,
                RowHeadersVisible = false,
                MultiSelect = false,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                ColumnHeadersHeight = 38,
                BackgroundColor = Color.White
            };

            grid.RowTemplate.Height = 34;
            grid.ThemeStyle.HeaderStyle.BackColor = Color.FromArgb(28, 58, 42);
            grid.ThemeStyle.HeaderStyle.ForeColor = Color.White;
            grid.ThemeStyle.HeaderStyle.Height = 38;

            foreach (var c in cols)
            {
                var col = new DataGridViewTextBoxColumn
                {
                    Name = c.Prop,
                    DataPropertyName = c.Prop,
                    HeaderText = c.Header
                };

                if (c.Width == 0) col.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                else col.Width = c.Width;

                if (c.Format != null) col.DefaultCellStyle.Format = c.Format;
                if (c.Right) col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

                grid.Columns.Add(col);
            }

            var totals = new Label
            {
                Dock = DockStyle.Bottom,
                Height = 36,
                TextAlign = ContentAlignment.MiddleLeft,
                Padding = new Padding(10, 0, 0, 0),
                Font = new Font("Segoe UI Semibold", 9.5F),
                ForeColor = Ink,
                BackColor = Color.FromArgb(240, 243, 241)
            };

            // Fill first, then the bottom edge.
            page.Controls.Add(grid);
            page.Controls.Add(totals);

            _grids[name] = grid;
            _totals[name] = totals;

            return page;
        }

        // ================= Data =================

        private static string Money(decimal v) =>
            v.ToString(MoneyFmt, CultureInfo.InvariantCulture);

        private static string Iso(DateTime d) =>
            d.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);

        private string Url(string path) =>
            $"tenant/{_companyId}/branches/{_branchKey}/{path}" +
            $"?startDate={Iso(dtpFrom.Value)}&endDate={Iso(dtpTo.Value)}";

        private async Task<T?> GetAsync<T>(string path)
        {
            var response = await _http.GetAsync(Url(path));

            if (!response.IsSuccessStatusCode)
            {
                throw new InvalidOperationException(
                    await BranchesWinForms.ReadMessageAsync(response));
            }

            return await response.Content.ReadFromJsonAsync<T>();
        }

        private async Task<List<T>> ListAsync<T>(string key, string path)
        {
            if (_cache.TryGetValue(key, out var cached))
                return (List<T>)cached;

            var list = await GetAsync<List<T>>(path) ?? new List<T>();
            _cache[key] = list;
            return list;
        }

        private void SetBusy(bool busy)
        {
            UseWaitCursor = busy;
            btnApply.Enabled = !busy;
            btnPdf.Enabled = !busy;
        }

        private static void ShowError(Exception ex) =>
            MessageBox.Show(ex.Message, "Branch Details",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);

        private async Task ReloadAsync()
        {
            if (dtpTo.Value.Date < dtpFrom.Value.Date)
            {
                MessageBox.Show("The end date cannot be earlier than the start date.",
                    "Branch Details", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            _cache.Clear();
            _loaded.Clear();

            try
            {
                SetBusy(true);

                _summary = await GetAsync<SummaryDto>("summary");

                if (_summary != null)
                {
                    _branchName = _summary.BranchName;
                    UpdateHeader();
                    UpdateKpis();
                }
            }
            catch (Exception ex)
            {
                ShowError(ex);
                return;
            }
            finally
            {
                SetBusy(false);
            }

            await EnsureTabAsync();
        }

        private void UpdateHeader()
        {
            if (_summary == null) return;

            lblTitle.Text = _summary.BranchName;
            Text = $"Branch Details - {_summary.BranchName}";

            if (_summary.BranchId == null)
            {
                lblSub.Text = "Records with no branch: older records and employees without a branch.";
                return;
            }

            var parts = new List<string> { _summary.BranchCode };
            if (!string.IsNullOrWhiteSpace(_summary.Address)) parts.Add(_summary.Address!);
            if (!string.IsNullOrWhiteSpace(_summary.ContactNumber)) parts.Add(_summary.ContactNumber!);
            parts.Add($"{_summary.EmployeeCount} active employee(s)");
            if (!_summary.IsActive) parts.Add("INACTIVE");

            lblSub.Text = string.Join("   |   ", parts);
        }

        private void UpdateKpis()
        {
            var s = _summary;
            if (s == null) return;

            _kpi["Gross Sales"].Text = Money(s.GrossSales);
            _kpi["VAT (in gross)"].Text = Money(s.VatAmount);
            _kpi["Net Sales"].Text = Money(s.NetSales);
            _kpi["Transactions"].Text = s.Transactions.ToString("N0");
            _kpi["Items Sold"].Text = s.ItemsSold.ToString("0.##");
            _kpi["Average Sale"].Text = Money(s.AverageSale);
            _kpi["Total Costs"].Text = Money(s.TotalCosts);
            _kpi["Profit"].Text = Money(s.Profit);
            _kpi["Profit"].ForeColor = s.Profit < 0 ? Color.Firebrick : Green;
        }

        private async Task EnsureTabAsync()
        {
            string? name = _tabs.SelectedTab?.Name;
            if (name == null || _loaded.Contains(name)) return;

            try
            {
                SetBusy(true);
                await LoadTabAsync(name);
                _loaded.Add(name);
            }
            catch (Exception ex)
            {
                ShowError(ex);
            }
            finally
            {
                SetBusy(false);
            }
        }

        private async Task LoadTabAsync(string name)
        {
            switch (name)
            {
                case "Overview":
                    _trend = await ListAsync<TrendPoint>("trend", "trend");
                    _top = await ListAsync<TopProduct>("top", "top-products");
                    _employees = await ListAsync<EmployeeRow>("employees", "employees");
                    pTrend.Invalidate();
                    pTop.Invalidate();
                    pEmp.Invalidate();
                    pCost.Invalidate();
                    break;

                case "Sales":
                    {
                        var rows = await ListAsync<SaleRow>("sales", "sales");
                        Bind("Sales", rows, r => r.Counted);
                        var ok = rows.Where(r => r.Counted).ToList();
                        decimal gross = ok.Sum(r => r.TotalAmount), vat = ok.Sum(r => r.VatAmount);
                        _totals["Sales"].Text =
                            $"{ok.Count} completed sale(s)   Gross {Money(gross)}   VAT {Money(vat)}   Net {Money(gross - vat)}" +
                            "   (cancelled sales are gray and not counted)";
                        break;
                    }

                case "Purchases":
                    {
                        var rows = await ListAsync<PurchaseRow>("purchases", "purchases");
                        Bind("Purchases", rows, r => r.Counted);
                        var ok = rows.Where(r => r.Counted).ToList();
                        _totals["Purchases"].Text =
                            $"{ok.Count} purchase(s)   Total {Money(ok.Sum(r => r.TotalAmount))}" +
                            "   (cancelled purchases are gray and not counted)";
                        break;
                    }

                case "Expenses":
                    {
                        var rows = await ListAsync<ExpenseRowDto>("expenses", "expenses");
                        Bind("Expenses", rows, r => r.Counted);
                        var ok = rows.Where(r => r.Counted).ToList();
                        _totals["Expenses"].Text =
                            $"{ok.Count} expense(s)   Total {Money(ok.Sum(r => r.Amount))}" +
                            "   (cancelled expenses are gray and not counted)";
                        break;
                    }

                case "Employees":
                    {
                        var rows = await ListAsync<EmployeeRow>("employees", "employees");
                        Bind("Employees", rows, r => r.IsActive);
                        _totals["Employees"].Text =
                            $"{rows.Count(r => r.IsActive)} active employee(s)   " +
                            $"Sales in range {rows.Sum(r => r.SalesCount):N0}   Total {Money(rows.Sum(r => r.SalesTotal))}";
                        break;
                    }

                case "Payroll":
                    {
                        var rows = await ListAsync<PayrollRow>("payroll", "payroll");
                        Bind("Payroll", rows, r => r.Counted);
                        var ok = rows.Where(r => r.Counted).ToList();
                        _totals["Payroll"].Text =
                            $"{ok.Count} counted record(s)   Basic + allowances {Money(ok.Sum(r => r.Gross))}   " +
                            $"Net pay {Money(ok.Sum(r => r.NetPay))}   (pending, cancelled and rejected are gray and not counted)";
                        break;
                    }
            }
        }

        private void Bind<T>(string tab, List<T> rows, Func<T, bool> counted)
        {
            var grid = _grids[tab];

            grid.DataSource = null;
            grid.DataSource = rows;

            for (int i = 0; i < grid.Rows.Count && i < rows.Count; i++)
            {
                grid.Rows[i].DefaultCellStyle.ForeColor =
                    counted(rows[i]) ? Ink : Color.Gray;
            }
        }

        // ================= Charts =================

        private static string Short(decimal v) =>
            v >= 1_000_000 ? $"₱{v / 1_000_000:0.0}M"
            : v >= 1_000 ? $"₱{v / 1_000:0.0}k"
            : $"₱{v:0}";

        private static void DrawEmpty(Graphics g, Rectangle a, string text)
        {
            using var f = new Font("Segoe UI", 9F);
            using var b = new SolidBrush(Color.FromArgb(150, 155, 152));
            var size = g.MeasureString(text, f);
            g.DrawString(text, f, b,
                a.Left + (a.Width - size.Width) / 2,
                a.Top + (a.Height - size.Height) / 2);
        }

        private static void DrawTrend(Graphics g, Rectangle a, List<TrendPoint> pts)
        {
            if (pts.Count == 0 || pts.All(p => p.Amount <= 0))
            {
                DrawEmpty(g, a, "No sales in this range");
                return;
            }

            decimal max = pts.Max(p => p.Amount);
            int left = 56;
            var plot = new Rectangle(a.Left + left, a.Top + 8, a.Width - left, a.Height - 34);

            using var gridPen = new Pen(Color.FromArgb(235, 238, 240));
            using var txt = new SolidBrush(Muted);
            using var font = new Font("Segoe UI", 7.5F);
            using var bar = new SolidBrush(Green);

            for (int i = 0; i <= 4; i++)
            {
                int y = plot.Bottom - plot.Height * i / 4;
                g.DrawLine(gridPen, plot.Left, y, plot.Right, y);
                g.DrawString(Short(max * i / 4), font, txt, a.Left, y - 7);
            }

            float slot = (float)plot.Width / pts.Count;
            float bw = Math.Max(2f, Math.Min(36f, slot * 0.7f));
            int step = Math.Max(1, (int)Math.Ceiling(pts.Count / Math.Max(1f, plot.Width / 60f)));

            for (int i = 0; i < pts.Count; i++)
            {
                float h = (float)(pts[i].Amount / max) * plot.Height;
                float x = plot.Left + i * slot + (slot - bw) / 2;

                if (h > 0)
                    g.FillRectangle(bar, x, plot.Bottom - h, bw, h);

                if (i % step == 0)
                    g.DrawString(pts[i].Label, font, txt, x - 6, plot.Bottom + 4);
            }
        }

        private static void DrawBars(
            Graphics g, Rectangle a, List<(string Label, decimal Value)> items,
            Color color, string empty)
        {
            if (items.Count == 0 || items.All(i => i.Value <= 0))
            {
                DrawEmpty(g, a, empty);
                return;
            }

            decimal max = items.Max(i => i.Value);
            int rowH = Math.Min(34, Math.Max(20, a.Height / Math.Max(1, items.Count)));
            int labelW = 140, valueW = 80;

            using var f = new Font("Segoe UI", 8.5F);
            using var tb = new SolidBrush(Muted);
            using var bb = new SolidBrush(color);
            using var sf = new StringFormat
            {
                Trimming = StringTrimming.EllipsisCharacter,
                FormatFlags = StringFormatFlags.NoWrap,
                LineAlignment = StringAlignment.Center
            };

            for (int i = 0; i < items.Count; i++)
            {
                int y = a.Top + i * rowH;
                if (y + rowH > a.Bottom + 4) break;

                g.DrawString(items[i].Label, f, tb,
                    new RectangleF(a.Left, y, labelW - 6, rowH), sf);

                float maxW = Math.Max(10, a.Width - labelW - valueW);
                float w = Math.Max(2f, (float)(items[i].Value / max) * maxW);

                g.FillRectangle(bb, a.Left + labelW, y + 4, w, rowH - 8);
                g.DrawString(Short(items[i].Value), f, tb,
                    a.Left + labelW + w + 6, y + (rowH - 14) / 2f);
            }
        }

        private void DrawCost(Graphics g, Rectangle a)
        {
            var s = _summary;

            if (s == null || s.TotalCosts <= 0)
            {
                DrawEmpty(g, a, "No costs recorded in this range");
                return;
            }

            var parts = new (string Name, decimal Amount, Color Color)[]
            {
                ("Purchases", s.Purchases, Green),
                ("Expenses", s.Expenses, Amber),
                ("Payroll", s.Payroll, Slate)
            };

            int size = Math.Min(a.Height, 170);
            var pie = new Rectangle(a.Left, a.Top + (a.Height - size) / 2, size, size);

            float start = -90;

            foreach (var part in parts)
            {
                float sweep = (float)(part.Amount / s.TotalCosts * 360m);
                if (sweep <= 0) continue;

                using var brush = new SolidBrush(part.Color);
                g.FillPie(brush, pie, start, sweep);
                start += sweep;
            }

            using var f = new Font("Segoe UI", 9F);
            using var fb = new Font("Segoe UI", 9F, FontStyle.Bold);
            using var tb = new SolidBrush(Muted);
            using var ink = new SolidBrush(Ink);

            int x = pie.Right + 28;
            int y = pie.Top + 20;

            foreach (var part in parts)
            {
                using var box = new SolidBrush(part.Color);
                g.FillRectangle(box, x, y + 3, 11, 11);
                g.DrawString(part.Name, f, tb, x + 18, y);
                g.DrawString(
                    $"{Money(part.Amount)}  ({part.Amount / s.TotalCosts * 100m:0.#}%)",
                    fb, ink, x + 18, y + 17);
                y += 46;
            }
        }

        private sealed class ChartPanel : Panel
        {
            [System.ComponentModel.Browsable(false)]
            [System.ComponentModel.DesignerSerializationVisibility(
                System.ComponentModel.DesignerSerializationVisibility.Hidden)]
            public string Title { get; set; } = string.Empty;

            [System.ComponentModel.Browsable(false)]
            [System.ComponentModel.DesignerSerializationVisibility(
                System.ComponentModel.DesignerSerializationVisibility.Hidden)]
            public Action<Graphics, Rectangle>? Draw { get; set; }

            public ChartPanel()
            {
                DoubleBuffered = true;
                ResizeRedraw = true;
                BackColor = Color.White;
            }

            protected override void OnPaint(PaintEventArgs e)
            {
                base.OnPaint(e);

                var g = e.Graphics;
                g.SmoothingMode = SmoothingMode.AntiAlias;

                using var f = new Font("Segoe UI", 11F, FontStyle.Bold);
                using var b = new SolidBrush(Ink);
                g.DrawString(Title, f, b, 16, 12);

                Draw?.Invoke(g, new Rectangle(16, 46, Math.Max(10, Width - 32), Math.Max(10, Height - 62)));
            }
        }

        // ================= PDF export =================

        private sealed class PrintTable
        {
            public string Title = string.Empty;
            public string[] Headers = Array.Empty<string>();
            public float[] Weights = Array.Empty<float>();
            public bool[] Right = Array.Empty<bool>();
            public List<string[]> Rows = new();
            public string Footer = string.Empty;
        }

        private static string Safe(string s) =>
            string.Concat(s.Select(c => Path.GetInvalidFileNameChars().Contains(c) ? '_' : c));

        private async Task ExportPdfAsync()
        {
            if (_summary == null)
            {
                MessageBox.Show("Nothing to export yet. Wait for the figures to load.",
                    "Export PDF", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            using var dialog = new SaveFileDialog
            {
                Filter = "PDF file (*.pdf)|*.pdf",
                FileName = $"Branch-{Safe(_summary.BranchCode)}-{Iso(dtpFrom.Value)}-{Iso(dtpTo.Value)}.pdf"
            };

            if (dialog.ShowDialog(this) != DialogResult.OK) return;

            string path = dialog.FileName;

            try
            {
                SetBusy(true);

                var sales = await ListAsync<SaleRow>("sales", "sales");
                var purchases = await ListAsync<PurchaseRow>("purchases", "purchases");
                var expenses = await ListAsync<ExpenseRowDto>("expenses", "expenses");
                var employees = await ListAsync<EmployeeRow>("employees", "employees");
                var payroll = await ListAsync<PayrollRow>("payroll", "payroll");

                var s = _summary;

                var kpis = new List<(string Label, string Value)>
                {
                    ("Gross Sales", Money(s.GrossSales)),
                    ("VAT (in gross)", Money(s.VatAmount)),
                    ("Net Sales", Money(s.NetSales)),
                    ("Transactions", s.Transactions.ToString("N0")),
                    ("Items Sold", s.ItemsSold.ToString("0.##")),
                    ("Average Sale", Money(s.AverageSale)),
                    ("Total Costs", Money(s.TotalCosts)),
                    ("Profit", Money(s.Profit))
                };

                string D(DateTime d) => d.ToString("MMM dd, yyyy");
                string DT(DateTime d) => d.ToString("MMM dd, yyyy h:mm tt");

                var tables = new List<PrintTable>
                {
                    new()
                    {
                        Title = "Sales",
                        Headers = new[] { "Sale No.", "Date", "Cashier", "Items", "VAT", "Total", "Status" },
                        Weights = new[] { 2.2f, 2.4f, 2.4f, 0.8f, 1.3f, 1.6f, 1.2f },
                        Right = new[] { false, false, false, true, true, true, false },
                        Rows = sales.Select(r => new[]
                        {
                            r.SaleNumber, DT(r.SaleDate), r.Cashier, r.ItemCount.ToString("0.##"),
                            Money(r.VatAmount), Money(r.TotalAmount), r.Status
                        }).ToList(),
                        Footer = $"Completed sales total {Money(sales.Where(r => r.Counted).Sum(r => r.TotalAmount))} (cancelled not counted)"
                    },
                    new()
                    {
                        Title = "Purchases",
                        Headers = new[] { "Purchase No.", "Date", "Supplier", "Discount", "Total", "Status" },
                        Weights = new[] { 2.4f, 2.4f, 2.6f, 1.4f, 1.6f, 1.2f },
                        Right = new[] { false, false, false, true, true, false },
                        Rows = purchases.Select(r => new[]
                        {
                            r.PurchaseNumber, DT(r.PurchaseDate), r.Supplier,
                            Money(r.Discount), Money(r.TotalAmount), r.Status
                        }).ToList(),
                        Footer = $"Purchases total {Money(purchases.Where(r => r.Counted).Sum(r => r.TotalAmount))} (cancelled not counted)"
                    },
                    new()
                    {
                        Title = "Expenses",
                        Headers = new[] { "Date", "Category", "Description", "Amount", "Payment", "Status" },
                        Weights = new[] { 1.6f, 2f, 3.4f, 1.6f, 1.4f, 1.1f },
                        Right = new[] { false, false, false, true, false, false },
                        Rows = expenses.Select(r => new[]
                        {
                            D(r.ExpenseDate), r.Category, r.Description,
                            Money(r.Amount), r.PaymentMethod, r.Status
                        }).ToList(),
                        Footer = $"Expenses total {Money(expenses.Where(r => r.Counted).Sum(r => r.Amount))} (cancelled not counted)"
                    },
                    new()
                    {
                        Title = "Employees",
                        Headers = new[] { "Code", "Name", "Position", "Status", "Sales", "Sales Total" },
                        Weights = new[] { 1.4f, 3f, 2.4f, 1.4f, 0.9f, 1.8f },
                        Right = new[] { false, false, false, false, true, true },
                        Rows = employees.Select(r => new[]
                        {
                            r.EmployeeCode, r.Name, r.Position, r.Status,
                            r.SalesCount.ToString("N0"), Money(r.SalesTotal)
                        }).ToList()
                    },
                    new()
                    {
                        Title = "Payroll",
                        Headers = new[] { "Employee", "Period", "Basic + Allowances", "Deductions", "Net Pay", "Status" },
                        Weights = new[] { 2.8f, 3f, 2f, 1.5f, 1.6f, 1.1f },
                        Right = new[] { false, false, true, true, true, false },
                        Rows = payroll.Select(r => new[]
                        {
                            r.Employee, $"{D(r.PeriodStart)} - {D(r.PeriodEnd)}",
                            Money(r.Gross), Money(r.Deductions), Money(r.NetPay), r.Status
                        }).ToList(),
                        Footer = $"Counted payroll {Money(payroll.Where(r => r.Counted).Sum(r => r.Gross))} (pending, cancelled and rejected not counted)"
                    }
                };

                if (File.Exists(path))
                    File.SetAttributes(path, FileAttributes.Normal);

                PrintReport(path, kpis, tables);

                await WaitForFileAsync(path);

                // The PDF is a flat print (not editable text). Mark it read-only too.
                try { File.SetAttributes(path, FileAttributes.ReadOnly); } catch { }

                MessageBox.Show($"Report saved:\n{path}", "Export PDF",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Unable to export the PDF.\n\n{ex.Message}", "Export PDF",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                SetBusy(false);
            }
        }

        private static async Task WaitForFileAsync(string path)
        {
            for (int i = 0; i < 40; i++)
            {
                try
                {
                    using var fs = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.None);
                    if (fs.Length > 0) return;
                }
                catch
                {
                }

                await Task.Delay(250);
            }
        }

        // Prints through the Windows "Microsoft Print to PDF" printer. No extra library.
        private void PrintReport(
            string path,
            List<(string Label, string Value)> kpis,
            List<PrintTable> tables)
        {
            int ti = 0, ri = 0, page = 0;
            bool titleDone = false;

            var doc = new PrintDocument();
            doc.PrinterSettings.PrinterName = "Microsoft Print to PDF";
            doc.PrinterSettings.PrintToFile = true;
            doc.PrinterSettings.PrintFileName = path;

            if (!doc.PrinterSettings.IsValid)
            {
                throw new InvalidOperationException(
                    "The 'Microsoft Print to PDF' printer is not available on this PC.");
            }

            doc.DefaultPageSettings.Landscape = true;
            doc.DefaultPageSettings.Margins = new Margins(40, 40, 40, 50);
            doc.PrintController = new StandardPrintController();
            doc.DocumentName = $"Branch report - {_branchName}";

            doc.PrintPage += (_, e) =>
            {
                var g = e.Graphics!;
                var b = e.MarginBounds;
                float y = b.Top;
                page++;

                using var h1 = new Font("Segoe UI", 16F, FontStyle.Bold);
                using var h2 = new Font("Segoe UI", 11F, FontStyle.Bold);
                using var body = new Font("Segoe UI", 8.5F);
                using var bold = new Font("Segoe UI", 8.5F, FontStyle.Bold);
                using var ink = new SolidBrush(Ink);
                using var gray = new SolidBrush(Muted);
                using var dark = new SolidBrush(Color.FromArgb(28, 58, 42));
                using var line = new Pen(Color.FromArgb(225, 228, 226));

                void Footer()
                {
                    g.DrawString($"Page {page}", body, gray,
                        b.Left + b.Width / 2f - 20, e.PageBounds.Bottom - 40);
                }

                void DrawRow(PrintTable t, string[]? cells, float yy)
                {
                    bool header = cells == null;
                    float total = t.Weights.Sum();
                    float x = b.Left;

                    if (header)
                        g.FillRectangle(dark, b.Left, yy, b.Width, 20);

                    for (int c = 0; c < t.Headers.Length; c++)
                    {
                        float w = b.Width * t.Weights[c] / total;
                        var rect = new RectangleF(x + 3, yy + 2, w - 6, 18);

                        using var sf = new StringFormat
                        {
                            Trimming = StringTrimming.EllipsisCharacter,
                            FormatFlags = StringFormatFlags.NoWrap,
                            Alignment = t.Right[c] ? StringAlignment.Far : StringAlignment.Near
                        };

                        g.DrawString(
                            header ? t.Headers[c] : cells![c],
                            header ? bold : body,
                            header ? Brushes.White : ink,
                            rect, sf);

                        x += w;
                    }

                    if (!header)
                        g.DrawLine(line, b.Left, yy + 20, b.Right, yy + 20);
                }

                if (page == 1)
                {
                    var s = _summary!;

                    g.DrawString($"{s.BranchName} - Branch Report", h1, ink, b.Left, y);
                    y += 30;

                    g.DrawString(
                        $"Period: {dtpFrom.Value:MMM dd, yyyy} to {dtpTo.Value:MMM dd, yyyy}    " +
                        $"Generated: {DateTime.Now:MMM dd, yyyy h:mm tt}    " +
                        "Profit = gross sales less purchases, expenses and payroll (same as the income statement).",
                        body, gray, b.Left, y);
                    y += 26;

                    float cw = b.Width / 4f;

                    for (int i = 0; i < kpis.Count; i++)
                    {
                        float cx = b.Left + (i % 4) * cw;
                        float cy = y + (i / 4) * 40;

                        g.DrawString(kpis[i].Label, body, gray, cx, cy);
                        g.DrawString(kpis[i].Value, h2, ink, cx, cy + 14);
                    }

                    y += 90;
                }

                bool headerDone = false;

                while (ti < tables.Count)
                {
                    var t = tables[ti];

                    if (!titleDone)
                    {
                        if (y + 80 > b.Bottom)
                        {
                            Footer();
                            e.HasMorePages = true;
                            return;
                        }

                        g.DrawString(t.Title, h2, ink, b.Left, y);
                        y += 26;
                        titleDone = true;
                    }

                    if (t.Rows.Count == 0)
                    {
                        g.DrawString("No records in this range.", body, gray, b.Left, y);
                        y += 22;
                    }
                    else
                    {
                        while (ri < t.Rows.Count)
                        {
                            if (!headerDone)
                            {
                                DrawRow(t, null, y);
                                y += 22;
                                headerDone = true;
                            }

                            if (y + 20 > b.Bottom)
                            {
                                Footer();
                                e.HasMorePages = true;
                                return;
                            }

                            DrawRow(t, t.Rows[ri], y);
                            y += 20;
                            ri++;
                        }
                    }

                    if (t.Footer.Length > 0)
                    {
                        g.DrawString(t.Footer, bold, ink, b.Left, y + 4);
                        y += 24;
                    }

                    y += 14;
                    ti++;
                    ri = 0;
                    titleDone = false;
                    headerDone = false;
                }

                Footer();
                e.HasMorePages = false;
            };

            doc.Print();
        }

        // ================= Client-side DTOs (match the API JSON) =================

        private class SummaryDto
        {
            public int? BranchId { get; set; }
            public string BranchCode { get; set; } = string.Empty;
            public string BranchName { get; set; } = string.Empty;
            public string? Address { get; set; }
            public string? ContactNumber { get; set; }
            public bool IsActive { get; set; }
            public int EmployeeCount { get; set; }
            public decimal GrossSales { get; set; }
            public decimal VatAmount { get; set; }
            public decimal NetSales { get; set; }
            public int Transactions { get; set; }
            public decimal ItemsSold { get; set; }
            public decimal AverageSale { get; set; }
            public decimal Purchases { get; set; }
            public decimal Expenses { get; set; }
            public decimal Payroll { get; set; }
            public decimal TotalCosts { get; set; }
            public decimal Profit { get; set; }
        }

        private class TrendPoint
        {
            public string Label { get; set; } = string.Empty;
            public decimal Amount { get; set; }
            public int Count { get; set; }
        }

        private class TopProduct
        {
            public int ProductId { get; set; }
            public string ProductName { get; set; } = string.Empty;
            public decimal Quantity { get; set; }
            public decimal Amount { get; set; }
            public decimal SharePercent { get; set; }
        }

        private class SaleRow
        {
            public string SaleNumber { get; set; } = string.Empty;
            public DateTime SaleDate { get; set; }
            public string Cashier { get; set; } = string.Empty;
            public decimal ItemCount { get; set; }
            public decimal Subtotal { get; set; }
            public decimal Discount { get; set; }
            public decimal VatAmount { get; set; }
            public decimal TotalAmount { get; set; }
            public string Status { get; set; } = string.Empty;
            public bool Counted { get; set; }
        }

        private class PurchaseRow
        {
            public string PurchaseNumber { get; set; } = string.Empty;
            public DateTime PurchaseDate { get; set; }
            public string Supplier { get; set; } = string.Empty;
            public decimal Discount { get; set; }
            public decimal TotalAmount { get; set; }
            public string Status { get; set; } = string.Empty;
            public bool Counted { get; set; }
        }

        private class ExpenseRowDto
        {
            public DateTime ExpenseDate { get; set; }
            public string Category { get; set; } = string.Empty;
            public string Description { get; set; } = string.Empty;
            public decimal Amount { get; set; }
            public string PaymentMethod { get; set; } = string.Empty;
            public string Status { get; set; } = string.Empty;
            public bool Counted { get; set; }
        }

        private class EmployeeRow
        {
            public string EmployeeCode { get; set; } = string.Empty;
            public string Name { get; set; } = string.Empty;
            public string Position { get; set; } = string.Empty;
            public string Status { get; set; } = string.Empty;
            public bool IsActive { get; set; }
            public int SalesCount { get; set; }
            public decimal SalesTotal { get; set; }
        }

        private class PayrollRow
        {
            public string Employee { get; set; } = string.Empty;
            public DateTime PeriodStart { get; set; }
            public DateTime PeriodEnd { get; set; }
            public decimal Gross { get; set; }
            public decimal Deductions { get; set; }
            public decimal NetPay { get; set; }
            public string Status { get; set; } = string.Empty;
            public bool Counted { get; set; }
        }
    }
}