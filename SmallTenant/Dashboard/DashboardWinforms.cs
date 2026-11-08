using Larp.winforms.Inventory;
using Larp.winforms.MediumTenant;
using Larp.winforms.MicroTenant.Inventory;
using Larp.winforms.MicroTenant.Products;
using Larp.winforms.MicroTenant.Reports;
using Larp.winforms.MicroTenant.Sales;
using Larp.winforms.SmallTenant.Approvals;
using Larp.winforms.SmallTenant.Expenses;
using Larp.winforms.SmallTenant.Payroll;
using Larp.winforms.SmallTenant.Supplier;
using Larp.winforms.SmallTenant.UserManagement;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Larp.winforms.SmallTenant.Dashboard
{
    public partial class DashboardWinforms : Form
    {
        private readonly HttpClient? _httpClient;
        private readonly int _companyId;
        private static readonly Color Accent = Color.FromArgb(37, 99, 235);

        private List<ProductDto> _products = new();
        private List<InventoryDto> _inventory = new();
        private SalesReportDto _sales = new();
        private readonly string _initials;

        public DashboardWinforms(
            HttpClient httpClient,
            int companyId)
        {
            InitializeComponent();

            _httpClient = httpClient;
            _companyId = companyId;

            string name = string.IsNullOrWhiteSpace(Session.FullName)
                ? "User"
                : Session.FullName;

            lblUserName.Text = name;
            lblUserRole.Text = Session.Role == "SuperAdmin"
                ? "Platform Admin"
                : Session.Role;

            _initials = GetInitials(name);
            ApplyRoleAccess();
            SetSelectedButton(btnDashboard);

            StartPosition = FormStartPosition.CenterScreen;
        }
        private static string GetInitials(string name)
        {
            var parts = name.Split(' ', StringSplitOptions.RemoveEmptyEntries);

            if (parts.Length == 0)
                return "U";

            if (parts.Length == 1)
                return parts[0].Substring(0, 1).ToUpperInvariant();

            return (parts[0].Substring(0, 1) +
                    parts[^1].Substring(0, 1))
                .ToUpperInvariant();
        }

        private void ApplyRoleAccess()
        {
            string role = Session.Role;

            var menu = new (Guna.UI2.WinForms.Guna2Button Button, string[] Roles, string? Module)[]
            {
                // Overview
                (btnDashboard, new[] { "Admin", "Manager" }, null),

                // Operations - Manager only
                (btnProducts,  new[] { "Manager" }, "products"),
                (btnInventory, new[] { "Manager" }, "inventory"),
                (btnSales,     new[] { "Manager" }, "sales"),

                // Purchasing - Admin and Manager
                (btnPurchases, new[] { "Admin", "Manager" }, "purchases"),
                (btnSuppliers, new[] { "Admin", "Manager" }, "suppliers"),
                (btnExpenses,  new[] { "Admin", "Manager" }, "expenses"),

                // People - Admin and Manager
                (btnEmployee,  new[] { "Admin", "Manager" }, "employees"),
                (btnPayroll,   new[] { "Admin", "Manager" }, "payroll"),

                // Administration - Reports for Admin/Manager;
                // Branches and Approvals are Admin only.
                (btnReports,   new[] { "Admin", "Manager" }, "reports"),
                (btnBranches,  new[] { "Admin" }, "branches"),
                (btnApprovals, new[] { "Admin" }, "approvals")
            };

            foreach (var (button, roles, module) in menu)
            {
                bool allowed = role == "SuperAdmin" ||
                    (roles.Contains(role) &&
                     (module == null || Session.HasModule(module)));

                button.Visible = allowed;
            }
            ApplySidebarLayout();
        }

        private void ApplySidebarLayout()
        {
            int y = 10;
            const int sectionGap = 14;
            const int buttonStep = 46;
            const int sectionHeight = 16;

            lblSectionOverview.Visible = true;
            if (Session.Role == "Manager" || Session.Role == "SuperAdmin")
            {
                lblSectionOperations.Visible = true;
            }
            else
            {
                lblSectionOperations.Visible = false;
            }
            lblSectionPurchasing.Visible = true;
            lblSectionPeople.Visible = true;
            lblSectionAdministration.Visible = true;

            // OVERVIEW
            lblSectionOverview.Location = new Point(22, y);
            y += sectionHeight;
            PlaceButton(btnDashboard, ref y, buttonStep,
                Session.Role == "SuperAdmin" || Session.Role == "Admin" || Session.Role == "Manager");
            y += sectionGap;

            // OPERATIONS
            lblSectionOperations.Location = new Point(22, y);
            y += sectionHeight;
            PlaceButton(btnProducts, ref y, buttonStep,
                IsAllowed(new[] { "Manager" }, "products"));
            PlaceButton(btnInventory, ref y, buttonStep,
                IsAllowed(new[] { "Manager" }, "inventory"));
            PlaceButton(btnSales, ref y, buttonStep,
                IsAllowed(new[] { "Manager" }, "sales"));
            y += sectionGap;

            // PURCHASING
            lblSectionPurchasing.Location = new Point(22, y);
            y += sectionHeight;
            PlaceButton(btnPurchases, ref y, buttonStep,
                IsAllowed(new[] { "Admin", "Manager" }, "purchases"));
            PlaceButton(btnSuppliers, ref y, buttonStep,
                IsAllowed(new[] { "Admin", "Manager" }, "suppliers"));
            PlaceButton(btnExpenses, ref y, buttonStep,
                IsAllowed(new[] { "Admin", "Manager" }, "expenses"));
            y += sectionGap;

            // PEOPLE
            lblSectionPeople.Location = new Point(22, y);
            y += sectionHeight;
            PlaceButton(btnEmployee, ref y, buttonStep,
                IsAllowed(new[] { "Admin", "Manager" }, "employees"));
            PlaceButton(btnPayroll, ref y, buttonStep,
                IsAllowed(new[] { "Admin", "Manager" }, "payroll"));
            y += sectionGap;

            // ADMINISTRATION
            lblSectionAdministration.Location = new Point(22, y);
            y += sectionHeight;
            PlaceButton(btnReports, ref y, buttonStep,
                IsAllowed(new[] { "Admin", "Manager" }, "reports"));
            PlaceButton(btnBranches, ref y, buttonStep,
                IsAllowed(new[] { "Admin" }, "branches"));
            PlaceButton(btnApprovals, ref y, buttonStep,
                IsAllowed(new[] { "Admin" }, "approvals"));
        }

        private bool IsAllowed(string[] roles, string? module)
        {
            return Session.Role == "SuperAdmin" ||
                (roles.Contains(Session.Role) &&
                 (module == null || Session.HasModule(module)));
        }

        private static void PlaceButton(
            Guna.UI2.WinForms.Guna2Button button,
            ref int y,
            int buttonStep,
            bool allowed)
        {
            if (!allowed)
            {
                return;
            }

            button.Location = new Point(12, y);
            y += buttonStep;
        }

        private async void Dashboard_Load(
            object? sender,
            EventArgs e)
        {
            if (_httpClient == null)
            {
                return;
            }

            await LoadDashboardAsync();
        }

        private async Task LoadDashboardAsync()
        {
            if (_httpClient == null)
            {
                return;
            }

            try
            {
                loadingIndicator.Visible = true;

                DateTime startDate = new DateTime(
                    DateTime.Today.Year,
                    DateTime.Today.Month,
                    1);

                DateTime endDate = DateTime.Today;

                string productsUrl =
                    $"tenant/{_companyId}/products";

                string inventoryUrl =
                    $"tenant/{_companyId}/inventory";

                string salesUrl =
                    $"tenant/{_companyId}/reports/sales" +
                    $"?startDate={startDate:yyyy-MM-dd}" +
                    $"&endDate={endDate:yyyy-MM-dd}";

                var productsTask =
                    _httpClient.GetFromJsonAsync<List<ProductDto>>(
                        productsUrl);

                var inventoryTask =
                    _httpClient.GetFromJsonAsync<List<InventoryDto>>(
                        inventoryUrl);

                var salesTask =
                    _httpClient.GetFromJsonAsync<SalesReportDto>(
                        salesUrl);

                await Task.WhenAll(
                    productsTask,
                    inventoryTask,
                    salesTask);

                _products =
                    productsTask.Result ??
                    new List<ProductDto>();

                _inventory =
                    inventoryTask.Result ??
                    new List<InventoryDto>();

                _sales =
                    salesTask.Result ??
                    new SalesReportDto();

                UpdateKpis();
                UpdateLowStockTable();

                salesChartPanel.Invalidate();
                inventoryChartPanel.Invalidate();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Unable to load dashboard data.\n\n" +
                    ex.Message,
                    "Dashboard Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            finally
            {
                loadingIndicator.Visible = false;
            }
        }

        private void UpdateKpis()
        {
            decimal totalSales =
                _sales.TotalSales;

            int totalTransactions =
                _sales.TotalTransactions;

            decimal totalItemsSold =
                _sales.TotalItemsSold;

            int activeProducts =
                _products.Count(
                    x => x.IsActive);

            lblSalesValue.Text =
                $"₱{totalSales:N2}";

            lblTransactionsValue.Text =
                totalTransactions.ToString("N0");

            lblItemsSoldValue.Text =
                totalItemsSold.ToString("N0");

            lblProductsValue.Text =
                activeProducts.ToString("N0");
        }

        private void UpdateLowStockTable()
        {
            lowStockGrid.Columns.Clear();
            lowStockGrid.Rows.Clear();

            lowStockGrid.Columns.Add(
                "ProductName",
                "Product");

            lowStockGrid.Columns.Add(
                "Quantity",
                "Quantity");

            lowStockGrid.Columns.Add(
                "ReorderLevel",
                "Reorder Level");

            lowStockGrid.Columns.Add(
                "Status",
                "Status");

            AppTheme.AlignRight(lowStockGrid, "Quantity", "ReorderLevel");

            var lowStockProducts =
                _inventory
                    .Where(x =>
                        x.IsActive &&
                        x.QuantityOnHand > 0 &&
                        x.QuantityOnHand <= x.ReorderLevel)
                    .OrderBy(x => x.QuantityOnHand)
                    .Take(10)
                    .ToList();


            foreach (var item in lowStockProducts)
            {
                int rowIndex =
                    lowStockGrid.Rows.Add(
                        item.ProductName,
                        item.QuantityOnHand.ToString("N0"),
                        item.ReorderLevel.ToString("N0"),
                        "Low Stock");

                lowStockGrid.Rows[rowIndex]
                    .Cells["Status"]
                    .Style.ForeColor =
                    Color.FromArgb(210, 130, 20);

                lowStockGrid.Rows[rowIndex]
                    .Cells["Status"]
                    .Style.Font =
                    new Font(
                        "Segoe UI",
                        9F,
                        FontStyle.Bold);
            }

            if (lowStockProducts.Count == 0)
            {
                int rowIndex =
                    lowStockGrid.Rows.Add(
                        "No low stock products",
                        "-",
                        "-",
                        "Good");

                lowStockGrid.Rows[rowIndex]
                    .Cells["Status"]
                    .Style.ForeColor =
                    Color.FromArgb(46, 107, 69);
            }
        }

        private void salesChartPanel_Paint(
            object? sender,
            PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            g.SmoothingMode =
                SmoothingMode.AntiAlias;

            Rectangle chartArea =
                new Rectangle(
                    25,
                    55,
                    salesChartPanel.Width - 45,
                    salesChartPanel.Height - 75);

            using Pen gridPen =
                new Pen(
                    Color.FromArgb(235, 238, 240),
                    1);

            using Pen axisPen =
                new Pen(
                    Color.FromArgb(190, 198, 193),
                    1);

            g.DrawLine(
                axisPen,
                chartArea.Left,
                chartArea.Bottom,
                chartArea.Right,
                chartArea.Bottom);

            g.DrawLine(
                axisPen,
                chartArea.Left,
                chartArea.Top,
                chartArea.Left,
                chartArea.Bottom);

            var items =
                _sales.Items?
                    .OrderByDescending(x => x.TotalAmount)
                    .Take(8)
                    .ToList()
                ?? new List<SalesReportItemDto>();

            if (items.Count == 0)
            {
                DrawCenteredText(
                    g,
                    "No sales data available",
                    salesChartPanel.ClientRectangle,
                    Color.FromArgb(150, 155, 152));

                return;
            }

            decimal maximum =
                items.Max(x => x.TotalAmount);

            if (maximum <= 0)
            {
                maximum = 1;
            }

            for (int i = 1; i <= 4; i++)
            {
                int y =
                    chartArea.Bottom -
                    (chartArea.Height * i / 4);

                g.DrawLine(
                    gridPen,
                    chartArea.Left,
                    y,
                    chartArea.Right,
                    y);
            }

            int count =
                items.Count;

            float slotWidth =
                (float)chartArea.Width / count;

            float barWidth =
                Math.Min(
                    48,
                    slotWidth * 0.55f);

            using Brush barBrush =
                new SolidBrush(
                    Color.FromArgb(46, 107, 69));

            using Brush textBrush =
                new SolidBrush(
                    Color.FromArgb(90, 100, 95));

            using Font valueFont =
                new Font(
                    "Segoe UI",
                    7.5F,
                    FontStyle.Bold);

            using Font nameFont =
                new Font(
                    "Segoe UI",
                    7F);

            for (int i = 0; i < count; i++)
            {
                var item =
                    items[i];

                float barHeight =
                    (float)(
                        item.TotalAmount /
                        maximum *
                        (chartArea.Height - 20));

                float x =
                    chartArea.Left +
                    i * slotWidth +
                    (slotWidth - barWidth) / 2;

                float y =
                    chartArea.Bottom -
                    barHeight;

                RectangleF bar =
                    new RectangleF(
                        x,
                        y,
                        barWidth,
                        barHeight);

                g.FillRectangle(
                    barBrush,
                    bar);

                string amount =
                    item.TotalAmount >= 1000
                        ? $"₱{item.TotalAmount / 1000:N1}k"
                        : $"₱{item.TotalAmount:N0}";

                SizeF amountSize =
                    g.MeasureString(
                        amount,
                        valueFont);

                g.DrawString(
                    amount,
                    valueFont,
                    textBrush,
                    x +
                    (barWidth - amountSize.Width) / 2,
                    y - 16);

                string name =
                    ShortenProductName(
                        item.ProductName,
                        10);

                SizeF nameSize =
                    g.MeasureString(
                        name,
                        nameFont);

                g.DrawString(
                    name,
                    nameFont,
                    textBrush,
                    x +
                    (barWidth - nameSize.Width) / 2,
                    chartArea.Bottom + 5);
            }
        }

        private void panelAvatar_Paint(object sender, PaintEventArgs e)
        {
            var g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;

            var circle = new Rectangle(0, 0, panelAvatar.Width - 1, panelAvatar.Height - 1);

            using (var fill = new SolidBrush(Accent))
                g.FillEllipse(fill, circle);

            using var font = new Font("Segoe UI", 11F, FontStyle.Bold);
            using var text = new SolidBrush(Color.White);
            using var format = new StringFormat
            {
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center
            };

            g.DrawString(_initials, font, text, circle, format);
        }

        private void inventoryChartPanel_Paint(
            object? sender,
            PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            g.SmoothingMode =
                SmoothingMode.AntiAlias;

            int activeStock =
                _inventory.Count(
                    x =>
                        x.IsActive &&
                        x.QuantityOnHand > 0 &&
                        x.QuantityOnHand > x.ReorderLevel);

            int lowStock =
                _inventory.Count(
                    x =>
                        x.IsActive &&
                        x.QuantityOnHand > 0 &&
                        x.QuantityOnHand <= x.ReorderLevel);

            int outOfStock =
                _inventory.Count(
                    x =>
                        x.IsActive &&
                        x.QuantityOnHand <= 0);

            int total =
                activeStock +
                lowStock +
                outOfStock;

            if (total == 0)
            {
                DrawCenteredText(
                    g,
                    "No inventory data",
                    inventoryChartPanel.ClientRectangle,
                    Color.FromArgb(150, 155, 152));

                return;
            }

            Rectangle pieRectangle =
                new Rectangle(
                    25,
                    58,
                    130,
                    130);

            float activeAngle =
                360f * activeStock / total;

            float lowAngle =
                360f * lowStock / total;

            float outAngle =
                360f * outOfStock / total;

            using Brush activeBrush =
                new SolidBrush(
                    Color.FromArgb(46, 107, 69));

            using Brush lowBrush =
                new SolidBrush(
                    Color.FromArgb(230, 165, 45));

            using Brush outBrush =
                new SolidBrush(
                    Color.FromArgb(210, 75, 70));

            float startAngle = -90;

            if (activeStock > 0)
            {
                g.FillPie(
                    activeBrush,
                    pieRectangle,
                    startAngle,
                    activeAngle);

                startAngle += activeAngle;
            }

            if (lowStock > 0)
            {
                g.FillPie(
                    lowBrush,
                    pieRectangle,
                    startAngle,
                    lowAngle);

                startAngle += lowAngle;
            }

            if (outOfStock > 0)
            {
                g.FillPie(
                    outBrush,
                    pieRectangle,
                    startAngle,
                    outAngle);
            }

            using Brush centerBrush =
                new SolidBrush(Color.White);

            Rectangle centerRectangle =
                new Rectangle(
                    58,
                    91,
                    64,
                    64);

            g.FillEllipse(
                centerBrush,
                centerRectangle);

            using Font totalFont =
                new Font(
                    "Segoe UI",
                    13F,
                    FontStyle.Bold);

            using Brush totalBrush =
                new SolidBrush(
                    Color.FromArgb(40, 50, 45));

            string totalText =
                total.ToString();

            SizeF totalSize =
                g.MeasureString(
                    totalText,
                    totalFont);

            g.DrawString(
                totalText,
                totalFont,
                totalBrush,
                centerRectangle.X +
                (centerRectangle.Width - totalSize.Width) / 2,
                centerRectangle.Y +
                (centerRectangle.Height - totalSize.Height) / 2);

            DrawLegend(
                g,
                180,
                75,
                "In Stock",
                activeStock,
                Color.FromArgb(46, 107, 69));

            DrawLegend(
                g,
                180,
                120,
                "Low Stock",
                lowStock,
                Color.FromArgb(230, 165, 45));

            DrawLegend(
                g,
                180,
                165,
                "Out of Stock",
                outOfStock,
                Color.FromArgb(210, 75, 70));
        }

        private void DrawLegend(
            Graphics g,
            int x,
            int y,
            string text,
            int value,
            Color color)
        {
            using Brush colorBrush =
                new SolidBrush(color);

            using Brush textBrush =
                new SolidBrush(
                    Color.FromArgb(80, 90, 85));

            using Font font =
                new Font(
                    "Segoe UI",
                    8.5F);

            g.FillRectangle(
                colorBrush,
                x,
                y + 3,
                10,
                10);

            g.DrawString(
                text,
                font,
                textBrush,
                x + 17,
                y);

            using Font valueFont =
                new Font(
                    "Segoe UI",
                    9F,
                    FontStyle.Bold);

            g.DrawString(
                value.ToString(),
                valueFont,
                textBrush,
                x + 105,
                y);
        }

        private void DrawCenteredText(
            Graphics g,
            string text,
            Rectangle area,
            Color color)
        {
            using Font font =
                new Font(
                    "Segoe UI",
                    9F);

            using Brush brush =
                new SolidBrush(color);

            SizeF size =
                g.MeasureString(
                    text,
                    font);

            float x =
                area.Left +
                (area.Width - size.Width) / 2;

            float y =
                area.Top +
                (area.Height - size.Height) / 2;

            g.DrawString(
                text,
                font,
                brush,
                x,
                y);
        }

        private string ShortenProductName(
            string name,
            int maxLength)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return "-";
            }

            if (name.Length <= maxLength)
            {
                return name;
            }

            return name.Substring(
                0,
                maxLength - 1) + "…";
        }

        private async void btnRefresh_Click(
            object? sender,
            EventArgs e)
        {
            await LoadDashboardAsync();
        }

        private void btnDashboard_Click(
            object? sender,
            EventArgs e)
        {
            SetSelectedButton(btnDashboard);
        }

        private void btnExpenses_Click(
            object? sender,
            EventArgs e)
        {
            SetSelectedButton(btnExpenses);

            if (_httpClient == null)
            {
                return;
            }

            using ExpensesWinForms form =
                new ExpensesWinForms(
                    _httpClient,
                    _companyId);

            form.ShowDialog(this);

            SetSelectedButton(btnDashboard);
        }

        private void btnProducts_Click(
            object? sender,
            EventArgs e)
        {
            SetSelectedButton(btnProducts);

            if (_httpClient == null)
            {
                return;
            }

            using ProductWinForms form =
                new ProductWinForms(
                    _httpClient,
                    _companyId);

            form.ShowDialog(this);

            _ = LoadDashboardAsync();
            SetSelectedButton(btnDashboard);
        }

        private void btnInventory_Click(
            object? sender,
            EventArgs e)
        {
            SetSelectedButton(btnInventory);

            if (_httpClient == null)
            {
                return;
            }

            using InventoryWinForms form =
                new InventoryWinForms(
                    _httpClient,
                    _companyId);

            form.ShowDialog(this);

            _ = LoadDashboardAsync();
            SetSelectedButton(btnDashboard);
        }

        private void btnSales_Click(
            object? sender,
            EventArgs e)
        {
            SetSelectedButton(btnSales);

            if (_httpClient == null)
            {
                return;
            }

            using SalesWinForms form =
                new SalesWinForms(
                    _httpClient,
                    _companyId);

            form.ShowDialog(this);

            _ = LoadDashboardAsync();
            SetSelectedButton(btnDashboard);
        }

        private void btnReports_Click(
            object? sender,
            EventArgs e)
        {
            SetSelectedButton(btnReports);

            if (_httpClient == null)
            {
                return;
            }

            using ReportsGenerator form =
                new ReportsGenerator(
                    _httpClient,
                    _companyId);

            form.ShowDialog(this);

            SetSelectedButton(btnDashboard);
        }

        private void btnEmployee_Click(
            object? sender,
            EventArgs e)
        {
            SetSelectedButton(btnEmployee);

            if (_httpClient == null)
            {
                return;
            }

            using EmployeeWinforms form =
                new EmployeeWinforms(
                    _httpClient,
                    _companyId);

            form.ShowDialog(this);

            SetSelectedButton(btnDashboard);
        }

        private void btnPayroll_Click(
            object? sender,
            EventArgs e)
        {
            SetSelectedButton(btnPayroll);

            if (_httpClient == null)
            {
                return;
            }

            using PayrollWinforms form =
                new PayrollWinforms(
                    _httpClient,
                    _companyId);

            form.ShowDialog(this);

            SetSelectedButton(btnDashboard);
        }

        private void btnSuppliers_Click(
            object? sender,
            EventArgs e)
        {
            SetSelectedButton(btnSuppliers);

            if (_httpClient == null)
            {
                return;
            }

            using SupplierWinforms form =
                new SupplierWinforms(
                    _httpClient,
                    _companyId);

            form.ShowDialog(this);

            SetSelectedButton(btnDashboard);
        }

        private void btnPurchases_Click(
            object? sender,
            EventArgs e)
        {
            SetSelectedButton(btnPurchases);

            if (_httpClient == null)
            {
                return;
            }

            using PurchasesWinForms form =
                new PurchasesWinForms(
                    _httpClient,
                    _companyId);

            form.ShowDialog(this);

            _ = LoadDashboardAsync();
            SetSelectedButton(btnDashboard);
        }

        private void btnBranches_Click(
            object? sender,
            EventArgs e)
        {
            SetSelectedButton(btnBranches);

            if (_httpClient == null)
            {
                return;
            }

            using BranchesWinForms form =
                new BranchesWinForms(
                    _httpClient,
                    _companyId);

            form.ShowDialog(this);

            SetSelectedButton(btnDashboard);
        }

        private void btnApprovals_Click(
            object? sender,
            EventArgs e)
        {
            SetSelectedButton(btnApprovals);

            if (_httpClient == null)
            {
                return;
            }

            using ApprovalsWinForms form =
                new ApprovalsWinForms(
                    _httpClient,
                    _companyId);

            form.ShowDialog(this);

            SetSelectedButton(btnDashboard);
        }

        private void SetSelectedButton(
            Guna.UI2.WinForms.Guna2Button selected)
        {
            Guna.UI2.WinForms.Guna2Button[] buttons =
            {
                btnDashboard,
                btnProducts,
                btnInventory,
                btnSales,
                btnReports,
                btnEmployee,
                btnPayroll,
                btnSuppliers,
                btnPurchases,
                btnExpenses,
                btnBranches,
                btnApprovals

            };

            foreach (var button in buttons)
            {
                button.Checked = false;
                button.FillColor =
                    Color.Transparent;
                button.HoverState.FillColor =
                    Color.FromArgb(44, 60, 92);
            }

            selected.Checked = true;
            selected.FillColor =
                Accent;
            selected.HoverState.FillColor =
                Accent;
        }

        private class ProductDto
        {
            public int ProductId { get; set; }

            public string ProductCode { get; set; }
                = string.Empty;

            public string ProductName { get; set; }
                = string.Empty;

            public decimal UnitPrice { get; set; }

            public bool IsActive { get; set; }

            public DateTime CreatedAt { get; set; }
        }

        private class InventoryDto
        {
            public int ProductId { get; set; }

            public string ProductName { get; set; }
                = string.Empty;

            public decimal QuantityOnHand { get; set; }

            public decimal ReorderLevel { get; set; }

            public bool IsActive { get; set; }
        }

        private class SalesReportDto
        {
            public int TotalTransactions { get; set; }

            public decimal TotalSales { get; set; }

            public decimal TotalItemsSold { get; set; }

            public List<SalesReportItemDto> Items { get; set; }
                = new();
        }

        private class SalesReportItemDto
        {
            public int ProductId { get; set; }

            public string ProductName { get; set; }
                = string.Empty;

            public decimal UnitPrice { get; set; }

            public decimal QuantitySold { get; set; }

            public decimal TotalAmount { get; set; }
        }

        private void LogoutBtn_Click(object sender, EventArgs e)
        {
            var answer = MessageBox.Show("Log out of your account?", "Logout",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (answer != DialogResult.Yes) return;

            Close();
        }
    }
}