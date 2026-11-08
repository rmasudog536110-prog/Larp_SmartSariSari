namespace Larp.winforms.SmallTenant.Dashboard
{
    partial class DashboardWinforms
    {
        private System.ComponentModel.IContainer components = null;

        // ---------------- Sidebar ----------------
        private Guna.UI2.WinForms.Guna2Panel sidebarPanel;

        private System.Windows.Forms.Panel panelBrand;
        private System.Windows.Forms.Panel panelLogo;
        private System.Windows.Forms.Label lblLogoLetter;
        private System.Windows.Forms.Label lblLogo;
        private System.Windows.Forms.Label lblAppName;

        private System.Windows.Forms.Panel panelMenu;
        private System.Windows.Forms.Label lblSectionOverview;
        private System.Windows.Forms.Label lblSectionOperations;
        private System.Windows.Forms.Label lblSectionPurchasing;
        private System.Windows.Forms.Label lblSectionPeople;
        private System.Windows.Forms.Label lblSectionAdministration;

        private Guna.UI2.WinForms.Guna2Button btnDashboard;
        private Guna.UI2.WinForms.Guna2Button btnExpenses;
        private Guna.UI2.WinForms.Guna2Button btnReports;
        private Guna.UI2.WinForms.Guna2Button btnProducts;
        private Guna.UI2.WinForms.Guna2Button btnInventory;
        private Guna.UI2.WinForms.Guna2Button btnSales;
        private Guna.UI2.WinForms.Guna2Button btnSuppliers;
        private Guna.UI2.WinForms.Guna2Button btnPurchases;
        private Guna.UI2.WinForms.Guna2Button btnEmployee;
        private Guna.UI2.WinForms.Guna2Button btnPayroll;
        private Guna.UI2.WinForms.Guna2Button btnBranches;
        private Guna.UI2.WinForms.Guna2Button btnApprovals;

        private System.Windows.Forms.Panel panelUser;
        private System.Windows.Forms.Panel panelUserLine;
        private System.Windows.Forms.Panel panelAvatar;
        private System.Windows.Forms.Label lblUserName;
        private System.Windows.Forms.Label lblUserRole;
        private Guna.UI2.WinForms.Guna2Button LogoutBtn;

        // ---------------- Content ----------------
        private Guna.UI2.WinForms.Guna2Panel contentPanel;

        private Guna.UI2.WinForms.Guna2HtmlLabel lblPageTitle;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblPageSubtitle;
        private Guna.UI2.WinForms.Guna2Button btnRefresh;

        private Guna.UI2.WinForms.Guna2Panel cardSales;
        private Guna.UI2.WinForms.Guna2Panel cardTransactions;
        private Guna.UI2.WinForms.Guna2Panel cardItemsSold;
        private Guna.UI2.WinForms.Guna2Panel cardProducts;

        private Guna.UI2.WinForms.Guna2HtmlLabel lblSalesTitle;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblSalesValue;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblTransactionsTitle;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblTransactionsValue;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblItemsSoldTitle;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblItemsSoldValue;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblProductsTitle;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblProductsValue;

        private Guna.UI2.WinForms.Guna2Panel salesChartPanel;
        private Guna.UI2.WinForms.Guna2Panel inventoryChartPanel;
        private Guna.UI2.WinForms.Guna2Panel lowStockPanel;

        private Guna.UI2.WinForms.Guna2HtmlLabel lblSalesChartTitle;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblInventoryChartTitle;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblLowStockTitle;

        private Guna.UI2.WinForms.Guna2DataGridView lowStockGrid;

        private Guna.UI2.WinForms.Guna2ProgressIndicator loadingIndicator;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }

            base.Dispose(disposing);
        }

        // Keep the sidebar part of this method designer-friendly: plain assignments and
        // named event handlers only. The menu positions are set in code (ApplyRoleAccess),
        // because the buttons shown depend on the role and the plan.
        // Colors: navy (28, 40, 63), accent blue (37, 99, 235), hover (44, 60, 92).
        private void InitializeComponent()
        {
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges27 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges28 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges3 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges4 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges5 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges6 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges7 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges8 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges9 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges10 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges11 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges12 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges13 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges14 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges15 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges16 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges17 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges18 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges19 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges20 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges21 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges22 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges23 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges24 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges25 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges26 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges46 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges47 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges29 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges30 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges31 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges32 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges33 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges34 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges35 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges36 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges37 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges38 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges39 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges40 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges41 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges42 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges43 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges44 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges45 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            sidebarPanel = new Guna.UI2.WinForms.Guna2Panel();
            panelMenu = new Panel();
            btnApprovals = new Guna.UI2.WinForms.Guna2Button();
            btnBranches = new Guna.UI2.WinForms.Guna2Button();
            btnReports = new Guna.UI2.WinForms.Guna2Button();
            btnPayroll = new Guna.UI2.WinForms.Guna2Button();
            btnEmployee = new Guna.UI2.WinForms.Guna2Button();
            btnExpenses = new Guna.UI2.WinForms.Guna2Button();
            btnSuppliers = new Guna.UI2.WinForms.Guna2Button();
            btnPurchases = new Guna.UI2.WinForms.Guna2Button();
            btnSales = new Guna.UI2.WinForms.Guna2Button();
            btnInventory = new Guna.UI2.WinForms.Guna2Button();
            btnProducts = new Guna.UI2.WinForms.Guna2Button();
            btnDashboard = new Guna.UI2.WinForms.Guna2Button();
            lblSectionAdministration = new Label();
            lblSectionPeople = new Label();
            lblSectionPurchasing = new Label();
            lblSectionOperations = new Label();
            lblSectionOverview = new Label();
            panelUser = new Panel();
            LogoutBtn = new Guna.UI2.WinForms.Guna2Button();
            lblUserRole = new Label();
            lblUserName = new Label();
            panelAvatar = new Panel();
            panelUserLine = new Panel();
            panelBrand = new Panel();
            lblAppName = new Label();
            lblLogo = new Label();
            panelLogo = new Panel();
            lblLogoLetter = new Label();
            contentPanel = new Guna.UI2.WinForms.Guna2Panel();
            btnRefresh = new Guna.UI2.WinForms.Guna2Button();
            lblPageTitle = new Guna.UI2.WinForms.Guna2HtmlLabel();
            lblPageSubtitle = new Guna.UI2.WinForms.Guna2HtmlLabel();
            cardSales = new Guna.UI2.WinForms.Guna2Panel();
            lblSalesTitle = new Guna.UI2.WinForms.Guna2HtmlLabel();
            lblSalesValue = new Guna.UI2.WinForms.Guna2HtmlLabel();
            cardTransactions = new Guna.UI2.WinForms.Guna2Panel();
            lblTransactionsTitle = new Guna.UI2.WinForms.Guna2HtmlLabel();
            lblTransactionsValue = new Guna.UI2.WinForms.Guna2HtmlLabel();
            cardItemsSold = new Guna.UI2.WinForms.Guna2Panel();
            lblItemsSoldTitle = new Guna.UI2.WinForms.Guna2HtmlLabel();
            lblItemsSoldValue = new Guna.UI2.WinForms.Guna2HtmlLabel();
            cardProducts = new Guna.UI2.WinForms.Guna2Panel();
            lblProductsTitle = new Guna.UI2.WinForms.Guna2HtmlLabel();
            lblProductsValue = new Guna.UI2.WinForms.Guna2HtmlLabel();
            salesChartPanel = new Guna.UI2.WinForms.Guna2Panel();
            lblSalesChartTitle = new Guna.UI2.WinForms.Guna2HtmlLabel();
            inventoryChartPanel = new Guna.UI2.WinForms.Guna2Panel();
            lblInventoryChartTitle = new Guna.UI2.WinForms.Guna2HtmlLabel();
            lowStockPanel = new Guna.UI2.WinForms.Guna2Panel();
            lblLowStockTitle = new Guna.UI2.WinForms.Guna2HtmlLabel();
            lowStockGrid = new Guna.UI2.WinForms.Guna2DataGridView();
            loadingIndicator = new Guna.UI2.WinForms.Guna2ProgressIndicator();
            sidebarPanel.SuspendLayout();
            panelMenu.SuspendLayout();
            panelUser.SuspendLayout();
            panelBrand.SuspendLayout();
            panelLogo.SuspendLayout();
            contentPanel.SuspendLayout();
            cardSales.SuspendLayout();
            cardTransactions.SuspendLayout();
            cardItemsSold.SuspendLayout();
            cardProducts.SuspendLayout();
            salesChartPanel.SuspendLayout();
            inventoryChartPanel.SuspendLayout();
            lowStockPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)lowStockGrid).BeginInit();
            SuspendLayout();
            // 
            // sidebarPanel
            // 
            sidebarPanel.BackColor = Color.FromArgb(28, 40, 63);
            sidebarPanel.Controls.Add(panelMenu);
            sidebarPanel.Controls.Add(panelUser);
            sidebarPanel.Controls.Add(panelBrand);
            sidebarPanel.CustomizableEdges = customizableEdges27;
            sidebarPanel.Dock = DockStyle.Left;
            sidebarPanel.Location = new Point(0, 0);
            sidebarPanel.Name = "sidebarPanel";
            sidebarPanel.ShadowDecoration.CustomizableEdges = customizableEdges28;
            sidebarPanel.Size = new Size(260, 800);
            sidebarPanel.TabIndex = 0;
            // 
            // panelMenu
            // 
            panelMenu.AutoScroll = true;
            panelMenu.BackColor = Color.FromArgb(28, 40, 63);
            panelMenu.Controls.Add(btnApprovals);
            panelMenu.Controls.Add(btnBranches);
            panelMenu.Controls.Add(btnReports);
            panelMenu.Controls.Add(btnPayroll);
            panelMenu.Controls.Add(btnEmployee);
            panelMenu.Controls.Add(btnExpenses);
            panelMenu.Controls.Add(btnSuppliers);
            panelMenu.Controls.Add(btnPurchases);
            panelMenu.Controls.Add(btnSales);
            panelMenu.Controls.Add(btnInventory);
            panelMenu.Controls.Add(btnProducts);
            panelMenu.Controls.Add(btnDashboard);
            panelMenu.Controls.Add(lblSectionAdministration);
            panelMenu.Controls.Add(lblSectionPeople);
            panelMenu.Controls.Add(lblSectionPurchasing);
            panelMenu.Controls.Add(lblSectionOperations);
            panelMenu.Controls.Add(lblSectionOverview);
            panelMenu.Dock = DockStyle.Fill;
            panelMenu.Location = new Point(0, 82);
            panelMenu.Name = "panelMenu";
            panelMenu.Size = new Size(260, 606);
            panelMenu.TabIndex = 1;
            // 
            // btnApprovals
            // 
            btnApprovals.BorderRadius = 6;
            btnApprovals.CustomizableEdges = customizableEdges1;
            btnApprovals.FillColor = Color.Transparent;
            btnApprovals.Font = new Font("Segoe UI", 10F);
            btnApprovals.ForeColor = Color.White;
            btnApprovals.HoverState.FillColor = Color.FromArgb(44, 60, 92);
            btnApprovals.ImageAlign = HorizontalAlignment.Left;
            btnApprovals.ImageOffset = new Point(14, 0);
            btnApprovals.Location = new Point(12, 556);
            btnApprovals.Name = "btnApprovals";
            btnApprovals.ShadowDecoration.CustomizableEdges = customizableEdges2;
            btnApprovals.Size = new Size(236, 42);
            btnApprovals.TabIndex = 0;
            btnApprovals.Text = "Approvals";
            btnApprovals.TextAlign = HorizontalAlignment.Left;
            btnApprovals.TextOffset = new Point(40, 0);
            btnApprovals.Click += btnApprovals_Click;
            // 
            // btnBranches
            // 
            btnBranches.BorderRadius = 6;
            btnBranches.CustomizableEdges = customizableEdges3;
            btnBranches.FillColor = Color.Transparent;
            btnBranches.Font = new Font("Segoe UI", 10F);
            btnBranches.ForeColor = Color.White;
            btnBranches.HoverState.FillColor = Color.FromArgb(44, 60, 92);
            btnBranches.ImageAlign = HorizontalAlignment.Left;
            btnBranches.ImageOffset = new Point(14, 0);
            btnBranches.Location = new Point(12, 512);
            btnBranches.Name = "btnBranches";
            btnBranches.ShadowDecoration.CustomizableEdges = customizableEdges4;
            btnBranches.Size = new Size(236, 42);
            btnBranches.TabIndex = 1;
            btnBranches.Text = "Branches";
            btnBranches.TextAlign = HorizontalAlignment.Left;
            btnBranches.TextOffset = new Point(40, 0);
            btnBranches.Click += btnBranches_Click;
            // 
            // btnReports
            // 
            btnReports.BorderRadius = 6;
            btnReports.CustomizableEdges = customizableEdges5;
            btnReports.FillColor = Color.Transparent;
            btnReports.Font = new Font("Segoe UI", 10F);
            btnReports.ForeColor = Color.White;
            btnReports.HoverState.FillColor = Color.FromArgb(44, 60, 92);
            btnReports.ImageAlign = HorizontalAlignment.Left;
            btnReports.ImageOffset = new Point(14, 0);
            btnReports.Location = new Point(12, 468);
            btnReports.Name = "btnReports";
            btnReports.ShadowDecoration.CustomizableEdges = customizableEdges6;
            btnReports.Size = new Size(236, 42);
            btnReports.TabIndex = 2;
            btnReports.Text = "Reports";
            btnReports.TextAlign = HorizontalAlignment.Left;
            btnReports.TextOffset = new Point(40, 0);
            btnReports.Click += btnReports_Click;
            // 
            // btnPayroll
            // 
            btnPayroll.BorderRadius = 6;
            btnPayroll.CustomizableEdges = customizableEdges7;
            btnPayroll.FillColor = Color.Transparent;
            btnPayroll.Font = new Font("Segoe UI", 10F);
            btnPayroll.ForeColor = Color.White;
            btnPayroll.HoverState.FillColor = Color.FromArgb(44, 60, 92);
            btnPayroll.ImageAlign = HorizontalAlignment.Left;
            btnPayroll.ImageOffset = new Point(14, 0);
            btnPayroll.Location = new Point(12, 414);
            btnPayroll.Name = "btnPayroll";
            btnPayroll.ShadowDecoration.CustomizableEdges = customizableEdges8;
            btnPayroll.Size = new Size(236, 42);
            btnPayroll.TabIndex = 3;
            btnPayroll.Text = "Payroll";
            btnPayroll.TextAlign = HorizontalAlignment.Left;
            btnPayroll.TextOffset = new Point(40, 0);
            btnPayroll.Click += btnPayroll_Click;
            // 
            // btnEmployee
            // 
            btnEmployee.BorderRadius = 6;
            btnEmployee.CustomizableEdges = customizableEdges9;
            btnEmployee.FillColor = Color.Transparent;
            btnEmployee.Font = new Font("Segoe UI", 10F);
            btnEmployee.ForeColor = Color.White;
            btnEmployee.HoverState.FillColor = Color.FromArgb(44, 60, 92);
            btnEmployee.ImageAlign = HorizontalAlignment.Left;
            btnEmployee.ImageOffset = new Point(14, 0);
            btnEmployee.Location = new Point(12, 370);
            btnEmployee.Name = "btnEmployee";
            btnEmployee.ShadowDecoration.CustomizableEdges = customizableEdges10;
            btnEmployee.Size = new Size(236, 42);
            btnEmployee.TabIndex = 4;
            btnEmployee.Text = "Employees";
            btnEmployee.TextAlign = HorizontalAlignment.Left;
            btnEmployee.TextOffset = new Point(40, 0);
            btnEmployee.Click += btnEmployee_Click;
            // 
            // btnExpenses
            // 
            btnExpenses.BorderRadius = 6;
            btnExpenses.CustomizableEdges = customizableEdges11;
            btnExpenses.FillColor = Color.Transparent;
            btnExpenses.Font = new Font("Segoe UI", 10F);
            btnExpenses.ForeColor = Color.White;
            btnExpenses.HoverState.FillColor = Color.FromArgb(44, 60, 92);
            btnExpenses.ImageAlign = HorizontalAlignment.Left;
            btnExpenses.ImageOffset = new Point(14, 0);
            btnExpenses.Location = new Point(12, 360);
            btnExpenses.Name = "btnExpenses";
            btnExpenses.ShadowDecoration.CustomizableEdges = customizableEdges12;
            btnExpenses.Size = new Size(236, 42);
            btnExpenses.TabIndex = 5;
            btnExpenses.Text = "Expenses";
            btnExpenses.TextAlign = HorizontalAlignment.Left;
            btnExpenses.TextOffset = new Point(40, 0);
            btnExpenses.Click += btnExpenses_Click;
            // 
            // btnSuppliers
            // 
            btnSuppliers.BorderRadius = 6;
            btnSuppliers.CustomizableEdges = customizableEdges13;
            btnSuppliers.FillColor = Color.Transparent;
            btnSuppliers.Font = new Font("Segoe UI", 10F);
            btnSuppliers.ForeColor = Color.White;
            btnSuppliers.HoverState.FillColor = Color.FromArgb(44, 60, 92);
            btnSuppliers.ImageAlign = HorizontalAlignment.Left;
            btnSuppliers.ImageOffset = new Point(14, 0);
            btnSuppliers.Location = new Point(12, 316);
            btnSuppliers.Name = "btnSuppliers";
            btnSuppliers.ShadowDecoration.CustomizableEdges = customizableEdges14;
            btnSuppliers.Size = new Size(236, 42);
            btnSuppliers.TabIndex = 6;
            btnSuppliers.Text = "Suppliers";
            btnSuppliers.TextAlign = HorizontalAlignment.Left;
            btnSuppliers.TextOffset = new Point(40, 0);
            btnSuppliers.Click += btnSuppliers_Click;
            // 
            // btnPurchases
            // 
            btnPurchases.BorderRadius = 6;
            btnPurchases.CustomizableEdges = customizableEdges15;
            btnPurchases.FillColor = Color.Transparent;
            btnPurchases.Font = new Font("Segoe UI", 10F);
            btnPurchases.ForeColor = Color.White;
            btnPurchases.HoverState.FillColor = Color.FromArgb(44, 60, 92);
            btnPurchases.ImageAlign = HorizontalAlignment.Left;
            btnPurchases.ImageOffset = new Point(14, 0);
            btnPurchases.Location = new Point(12, 272);
            btnPurchases.Name = "btnPurchases";
            btnPurchases.ShadowDecoration.CustomizableEdges = customizableEdges16;
            btnPurchases.Size = new Size(236, 42);
            btnPurchases.TabIndex = 7;
            btnPurchases.Text = "Purchases";
            btnPurchases.TextAlign = HorizontalAlignment.Left;
            btnPurchases.TextOffset = new Point(40, 0);
            btnPurchases.Click += btnPurchases_Click;
            // 
            // btnSales
            // 
            btnSales.BorderRadius = 6;
            btnSales.CustomizableEdges = customizableEdges17;
            btnSales.FillColor = Color.Transparent;
            btnSales.Font = new Font("Segoe UI", 10F);
            btnSales.ForeColor = Color.White;
            btnSales.HoverState.FillColor = Color.FromArgb(44, 60, 92);
            btnSales.ImageAlign = HorizontalAlignment.Left;
            btnSales.ImageOffset = new Point(14, 0);
            btnSales.Location = new Point(12, 214);
            btnSales.Name = "btnSales";
            btnSales.ShadowDecoration.CustomizableEdges = customizableEdges18;
            btnSales.Size = new Size(236, 42);
            btnSales.TabIndex = 8;
            btnSales.Text = "Sales";
            btnSales.TextAlign = HorizontalAlignment.Left;
            btnSales.TextOffset = new Point(40, 0);
            btnSales.Click += btnSales_Click;
            // 
            // btnInventory
            // 
            btnInventory.BorderRadius = 6;
            btnInventory.CustomizableEdges = customizableEdges19;
            btnInventory.FillColor = Color.Transparent;
            btnInventory.Font = new Font("Segoe UI", 10F);
            btnInventory.ForeColor = Color.White;
            btnInventory.HoverState.FillColor = Color.FromArgb(44, 60, 92);
            btnInventory.ImageAlign = HorizontalAlignment.Left;
            btnInventory.ImageOffset = new Point(14, 0);
            btnInventory.Location = new Point(12, 170);
            btnInventory.Name = "btnInventory";
            btnInventory.ShadowDecoration.CustomizableEdges = customizableEdges20;
            btnInventory.Size = new Size(236, 42);
            btnInventory.TabIndex = 9;
            btnInventory.Text = "Inventory";
            btnInventory.TextAlign = HorizontalAlignment.Left;
            btnInventory.TextOffset = new Point(40, 0);
            btnInventory.Click += btnInventory_Click;
            // 
            // btnProducts
            // 
            btnProducts.BorderRadius = 6;
            btnProducts.CustomizableEdges = customizableEdges21;
            btnProducts.FillColor = Color.Transparent;
            btnProducts.Font = new Font("Segoe UI", 10F);
            btnProducts.ForeColor = Color.White;
            btnProducts.HoverState.FillColor = Color.FromArgb(44, 60, 92);
            btnProducts.ImageAlign = HorizontalAlignment.Left;
            btnProducts.ImageOffset = new Point(14, 0);
            btnProducts.Location = new Point(12, 126);
            btnProducts.Name = "btnProducts";
            btnProducts.ShadowDecoration.CustomizableEdges = customizableEdges22;
            btnProducts.Size = new Size(236, 42);
            btnProducts.TabIndex = 10;
            btnProducts.Text = "Products";
            btnProducts.TextAlign = HorizontalAlignment.Left;
            btnProducts.TextOffset = new Point(40, 0);
            btnProducts.Click += btnProducts_Click;
            // 
            // btnDashboard
            // 
            btnDashboard.BorderRadius = 6;
            btnDashboard.CustomizableEdges = customizableEdges23;
            btnDashboard.FillColor = Color.FromArgb(37, 99, 235);
            btnDashboard.Font = new Font("Segoe UI", 10F);
            btnDashboard.ForeColor = Color.White;
            btnDashboard.HoverState.FillColor = Color.FromArgb(44, 60, 92);
            btnDashboard.ImageAlign = HorizontalAlignment.Left;
            btnDashboard.ImageOffset = new Point(14, 0);
            btnDashboard.Location = new Point(12, 30);
            btnDashboard.Name = "btnDashboard";
            btnDashboard.ShadowDecoration.CustomizableEdges = customizableEdges24;
            btnDashboard.Size = new Size(236, 42);
            btnDashboard.TabIndex = 11;
            btnDashboard.Text = "Dashboard";
            btnDashboard.TextAlign = HorizontalAlignment.Left;
            btnDashboard.TextOffset = new Point(40, 0);
            btnDashboard.Click += btnDashboard_Click;
            // 
            // lblSectionAdministration
            // 
            lblSectionAdministration.AutoSize = true;
            lblSectionAdministration.BackColor = Color.Transparent;
            lblSectionAdministration.Font = new Font("Segoe UI", 7.5F, FontStyle.Bold);
            lblSectionAdministration.ForeColor = Color.FromArgb(125, 140, 170);
            lblSectionAdministration.Location = new Point(22, 448);
            lblSectionAdministration.Name = "lblSectionAdministration";
            lblSectionAdministration.Size = new Size(120, 17);
            lblSectionAdministration.TabIndex = 12;
            lblSectionAdministration.Text = "ADMINISTRATION";
            // 
            // lblSectionPeople
            // 
            lblSectionPeople.AutoSize = true;
            lblSectionPeople.BackColor = Color.Transparent;
            lblSectionPeople.Font = new Font("Segoe UI", 7.5F, FontStyle.Bold);
            lblSectionPeople.ForeColor = Color.FromArgb(125, 140, 170);
            lblSectionPeople.Location = new Point(22, 350);
            lblSectionPeople.Name = "lblSectionPeople";
            lblSectionPeople.Size = new Size(55, 17);
            lblSectionPeople.TabIndex = 13;
            lblSectionPeople.Text = "PEOPLE";
            // 
            // lblSectionPurchasing
            // 
            lblSectionPurchasing.AutoSize = true;
            lblSectionPurchasing.BackColor = Color.Transparent;
            lblSectionPurchasing.Font = new Font("Segoe UI", 7.5F, FontStyle.Bold);
            lblSectionPurchasing.ForeColor = Color.FromArgb(125, 140, 170);
            lblSectionPurchasing.Location = new Point(22, 252);
            lblSectionPurchasing.Name = "lblSectionPurchasing";
            lblSectionPurchasing.Size = new Size(90, 17);
            lblSectionPurchasing.TabIndex = 14;
            lblSectionPurchasing.Text = "PURCHASING";
            // 
            // lblSectionOperations
            // 
            lblSectionOperations.AutoSize = true;
            lblSectionOperations.BackColor = Color.Transparent;
            lblSectionOperations.Font = new Font("Segoe UI", 7.5F, FontStyle.Bold);
            lblSectionOperations.ForeColor = Color.FromArgb(125, 140, 170);
            lblSectionOperations.Location = new Point(22, 106);
            lblSectionOperations.Name = "lblSectionOperations";
            lblSectionOperations.Size = new Size(88, 17);
            lblSectionOperations.TabIndex = 15;
            lblSectionOperations.Text = "OPERATIONS";
            // 
            // lblSectionOverview
            // 
            lblSectionOverview.AutoSize = true;
            lblSectionOverview.BackColor = Color.Transparent;
            lblSectionOverview.Font = new Font("Segoe UI", 7.5F, FontStyle.Bold);
            lblSectionOverview.ForeColor = Color.FromArgb(125, 140, 170);
            lblSectionOverview.Location = new Point(22, 10);
            lblSectionOverview.Name = "lblSectionOverview";
            lblSectionOverview.Size = new Size(75, 17);
            lblSectionOverview.TabIndex = 16;
            lblSectionOverview.Text = "OVERVIEW";
            // 
            // panelUser
            // 
            panelUser.BackColor = Color.FromArgb(22, 32, 52);
            panelUser.Controls.Add(LogoutBtn);
            panelUser.Controls.Add(lblUserRole);
            panelUser.Controls.Add(lblUserName);
            panelUser.Controls.Add(panelAvatar);
            panelUser.Controls.Add(panelUserLine);
            panelUser.Dock = DockStyle.Bottom;
            panelUser.Location = new Point(0, 688);
            panelUser.Name = "panelUser";
            panelUser.Size = new Size(260, 112);
            panelUser.TabIndex = 2;
            // 
            // LogoutBtn
            // 
            LogoutBtn.BorderRadius = 6;
            LogoutBtn.CustomizableEdges = customizableEdges25;
            LogoutBtn.FillColor = Color.Transparent;
            LogoutBtn.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            LogoutBtn.ForeColor = Color.FromArgb(239, 68, 68);
            LogoutBtn.HoverState.FillColor = Color.FromArgb(44, 60, 92);
            LogoutBtn.Location = new Point(62, 70);
            LogoutBtn.Name = "LogoutBtn";
            LogoutBtn.ShadowDecoration.CustomizableEdges = customizableEdges26;
            LogoutBtn.Size = new Size(150, 30);
            LogoutBtn.TabIndex = 13;
            LogoutBtn.Text = "Sign Out";
            LogoutBtn.TextAlign = HorizontalAlignment.Left;
            LogoutBtn.Click += LogoutBtn_Click;
            // 
            // lblUserRole
            // 
            lblUserRole.AutoEllipsis = true;
            lblUserRole.BackColor = Color.Transparent;
            lblUserRole.Font = new Font("Segoe UI", 8.5F);
            lblUserRole.ForeColor = Color.FromArgb(150, 163, 190);
            lblUserRole.Location = new Point(70, 42);
            lblUserRole.Name = "lblUserRole";
            lblUserRole.Size = new Size(180, 20);
            lblUserRole.TabIndex = 14;
            // 
            // lblUserName
            // 
            lblUserName.AutoEllipsis = true;
            lblUserName.BackColor = Color.Transparent;
            lblUserName.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblUserName.ForeColor = Color.White;
            lblUserName.Location = new Point(70, 20);
            lblUserName.Name = "lblUserName";
            lblUserName.Size = new Size(180, 22);
            lblUserName.TabIndex = 15;
            // 
            // panelAvatar
            // 
            panelAvatar.BackColor = Color.FromArgb(22, 32, 52);
            panelAvatar.Location = new Point(18, 20);
            panelAvatar.Name = "panelAvatar";
            panelAvatar.Size = new Size(42, 42);
            panelAvatar.TabIndex = 16;
            panelAvatar.Paint += panelAvatar_Paint;
            // 
            // panelUserLine
            // 
            panelUserLine.BackColor = Color.FromArgb(50, 64, 92);
            panelUserLine.Dock = DockStyle.Top;
            panelUserLine.Location = new Point(0, 0);
            panelUserLine.Name = "panelUserLine";
            panelUserLine.Size = new Size(260, 1);
            panelUserLine.TabIndex = 17;
            // 
            // panelBrand
            // 
            panelBrand.BackColor = Color.FromArgb(28, 40, 63);
            panelBrand.Controls.Add(lblAppName);
            panelBrand.Controls.Add(lblLogo);
            panelBrand.Controls.Add(panelLogo);
            panelBrand.Dock = DockStyle.Top;
            panelBrand.Location = new Point(0, 0);
            panelBrand.Name = "panelBrand";
            panelBrand.Size = new Size(260, 82);
            panelBrand.TabIndex = 0;
            // 
            // lblAppName
            // 
            lblAppName.AutoSize = true;
            lblAppName.BackColor = Color.Transparent;
            lblAppName.Font = new Font("Segoe UI", 8.5F);
            lblAppName.ForeColor = Color.FromArgb(150, 163, 190);
            lblAppName.Location = new Point(64, 43);
            lblAppName.Name = "lblAppName";
            lblAppName.Size = new Size(158, 20);
            lblAppName.TabIndex = 0;
            lblAppName.Text = "SARI-SARI SMART APP";
            // 
            // lblLogo
            // 
            lblLogo.AutoSize = true;
            lblLogo.BackColor = Color.Transparent;
            lblLogo.Font = new Font("Segoe UI", 13F, FontStyle.Bold);
            lblLogo.ForeColor = Color.White;
            lblLogo.Location = new Point(62, 20);
            lblLogo.Name = "lblLogo";
            lblLogo.Size = new Size(67, 30);
            lblLogo.TabIndex = 1;
            lblLogo.Text = "LARP";
            // 
            // panelLogo
            // 
            panelLogo.BackColor = Color.FromArgb(37, 99, 235);
            panelLogo.Controls.Add(lblLogoLetter);
            panelLogo.Location = new Point(20, 22);
            panelLogo.Name = "panelLogo";
            panelLogo.Size = new Size(34, 34);
            panelLogo.TabIndex = 0;
            // 
            // lblLogoLetter
            // 
            lblLogoLetter.BackColor = Color.Transparent;
            lblLogoLetter.Dock = DockStyle.Fill;
            lblLogoLetter.Font = new Font("Segoe UI", 13F, FontStyle.Bold);
            lblLogoLetter.ForeColor = Color.White;
            lblLogoLetter.Location = new Point(0, 0);
            lblLogoLetter.Name = "lblLogoLetter";
            lblLogoLetter.Size = new Size(34, 34);
            lblLogoLetter.TabIndex = 0;
            lblLogoLetter.Text = "L";
            lblLogoLetter.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // contentPanel
            // 
            contentPanel.BackColor = Color.FromArgb(232, 233, 236);
            contentPanel.Controls.Add(btnRefresh);
            contentPanel.Controls.Add(lblPageTitle);
            contentPanel.Controls.Add(lblPageSubtitle);
            contentPanel.Controls.Add(cardSales);
            contentPanel.Controls.Add(cardTransactions);
            contentPanel.Controls.Add(cardItemsSold);
            contentPanel.Controls.Add(cardProducts);
            contentPanel.Controls.Add(salesChartPanel);
            contentPanel.Controls.Add(inventoryChartPanel);
            contentPanel.Controls.Add(lowStockPanel);
            contentPanel.Controls.Add(loadingIndicator);
            contentPanel.CustomizableEdges = customizableEdges46;
            contentPanel.Dock = DockStyle.Fill;
            contentPanel.Location = new Point(260, 0);
            contentPanel.Name = "contentPanel";
            contentPanel.Padding = new Padding(28);
            contentPanel.ShadowDecoration.CustomizableEdges = customizableEdges47;
            contentPanel.Size = new Size(1140, 800);
            contentPanel.TabIndex = 1;
            // 
            // btnRefresh
            // 
            btnRefresh.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnRefresh.BorderRadius = 8;
            btnRefresh.CustomizableEdges = customizableEdges29;
            btnRefresh.FillColor = Color.FromArgb(28, 40, 63);
            btnRefresh.Font = new Font("Segoe UI", 9.5F);
            btnRefresh.ForeColor = Color.White;
            btnRefresh.Location = new Point(970, 30);
            btnRefresh.Name = "btnRefresh";
            btnRefresh.ShadowDecoration.CustomizableEdges = customizableEdges30;
            btnRefresh.Size = new Size(142, 40);
            btnRefresh.TabIndex = 10;
            btnRefresh.Text = "Refresh Data";
            btnRefresh.Click += btnRefresh_Click;
            // 
            // lblPageTitle
            // 
            lblPageTitle.BackColor = Color.Transparent;
            lblPageTitle.Font = new Font("Segoe UI", 22F, FontStyle.Bold);
            lblPageTitle.ForeColor = Color.FromArgb(28, 40, 63);
            lblPageTitle.Location = new Point(28, 13);
            lblPageTitle.Name = "lblPageTitle";
            lblPageTitle.Size = new Size(192, 52);
            lblPageTitle.TabIndex = 0;
            lblPageTitle.Text = "Dashboard";
            // 
            // lblPageSubtitle
            // 
            lblPageSubtitle.BackColor = Color.Transparent;
            lblPageSubtitle.Font = new Font("Segoe UI", 9.5F);
            lblPageSubtitle.ForeColor = Color.FromArgb(96, 105, 121);
            lblPageSubtitle.Location = new Point(31, 68);
            lblPageSubtitle.Name = "lblPageSubtitle";
            lblPageSubtitle.Size = new Size(284, 23);
            lblPageSubtitle.TabIndex = 1;
            lblPageSubtitle.Text = "Business overview for the current month";
            // 
            // cardSales
            // 
            cardSales.BackColor = Color.Transparent;
            cardSales.BorderRadius = 12;
            cardSales.Controls.Add(lblSalesTitle);
            cardSales.Controls.Add(lblSalesValue);
            cardSales.CustomizableEdges = customizableEdges31;
            cardSales.Location = new Point(28, 105);
            cardSales.Name = "cardSales";
            cardSales.ShadowDecoration.CustomizableEdges = customizableEdges32;
            cardSales.ShadowDecoration.Depth = 5;
            cardSales.ShadowDecoration.Enabled = true;
            cardSales.Size = new Size(240, 110);
            cardSales.TabIndex = 2;
            // 
            // lblSalesTitle
            // 
            lblSalesTitle.BackColor = Color.Transparent;
            lblSalesTitle.Font = new Font("Segoe UI", 9F);
            lblSalesTitle.ForeColor = Color.FromArgb(96, 105, 121);
            lblSalesTitle.Location = new Point(18, 16);
            lblSalesTitle.Name = "lblSalesTitle";
            lblSalesTitle.Size = new Size(75, 22);
            lblSalesTitle.TabIndex = 0;
            lblSalesTitle.Text = "Total Sales";
            // 
            // lblSalesValue
            // 
            lblSalesValue.BackColor = Color.Transparent;
            lblSalesValue.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
            lblSalesValue.ForeColor = Color.FromArgb(28, 40, 63);
            lblSalesValue.Location = new Point(71, 45);
            lblSalesValue.Name = "lblSalesValue";
            lblSalesValue.Size = new Size(22, 47);
            lblSalesValue.TabIndex = 1;
            lblSalesValue.Text = "0";
            // 
            // cardTransactions
            // 
            cardTransactions.BackColor = Color.Transparent;
            cardTransactions.BorderRadius = 12;
            cardTransactions.Controls.Add(lblTransactionsTitle);
            cardTransactions.Controls.Add(lblTransactionsValue);
            cardTransactions.CustomizableEdges = customizableEdges33;
            cardTransactions.Location = new Point(295, 105);
            cardTransactions.Name = "cardTransactions";
            cardTransactions.ShadowDecoration.CustomizableEdges = customizableEdges34;
            cardTransactions.ShadowDecoration.Depth = 5;
            cardTransactions.ShadowDecoration.Enabled = true;
            cardTransactions.Size = new Size(240, 110);
            cardTransactions.TabIndex = 3;
            // 
            // lblTransactionsTitle
            // 
            lblTransactionsTitle.BackColor = Color.Transparent;
            lblTransactionsTitle.Font = new Font("Segoe UI", 9F);
            lblTransactionsTitle.ForeColor = Color.FromArgb(96, 105, 121);
            lblTransactionsTitle.Location = new Point(18, 16);
            lblTransactionsTitle.Name = "lblTransactionsTitle";
            lblTransactionsTitle.Size = new Size(85, 22);
            lblTransactionsTitle.TabIndex = 0;
            lblTransactionsTitle.Text = "Transactions";
            // 
            // lblTransactionsValue
            // 
            lblTransactionsValue.BackColor = Color.Transparent;
            lblTransactionsValue.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
            lblTransactionsValue.ForeColor = Color.FromArgb(28, 40, 63);
            lblTransactionsValue.Location = new Point(165, 45);
            lblTransactionsValue.Name = "lblTransactionsValue";
            lblTransactionsValue.Size = new Size(22, 47);
            lblTransactionsValue.TabIndex = 1;
            lblTransactionsValue.Text = "0";
            // 
            // cardItemsSold
            // 
            cardItemsSold.BackColor = Color.Transparent;
            cardItemsSold.BorderRadius = 12;
            cardItemsSold.Controls.Add(lblItemsSoldTitle);
            cardItemsSold.Controls.Add(lblItemsSoldValue);
            cardItemsSold.CustomizableEdges = customizableEdges35;
            cardItemsSold.Location = new Point(562, 105);
            cardItemsSold.Name = "cardItemsSold";
            cardItemsSold.ShadowDecoration.CustomizableEdges = customizableEdges36;
            cardItemsSold.ShadowDecoration.Depth = 5;
            cardItemsSold.ShadowDecoration.Enabled = true;
            cardItemsSold.Size = new Size(240, 110);
            cardItemsSold.TabIndex = 4;
            // 
            // lblItemsSoldTitle
            // 
            lblItemsSoldTitle.BackColor = Color.Transparent;
            lblItemsSoldTitle.Font = new Font("Segoe UI", 9F);
            lblItemsSoldTitle.ForeColor = Color.FromArgb(96, 105, 121);
            lblItemsSoldTitle.Location = new Point(18, 16);
            lblItemsSoldTitle.Name = "lblItemsSoldTitle";
            lblItemsSoldTitle.Size = new Size(73, 22);
            lblItemsSoldTitle.TabIndex = 0;
            lblItemsSoldTitle.Text = "Items Sold";
            // 
            // lblItemsSoldValue
            // 
            lblItemsSoldValue.BackColor = Color.Transparent;
            lblItemsSoldValue.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
            lblItemsSoldValue.ForeColor = Color.FromArgb(28, 40, 63);
            lblItemsSoldValue.Location = new Point(159, 45);
            lblItemsSoldValue.Name = "lblItemsSoldValue";
            lblItemsSoldValue.Size = new Size(22, 47);
            lblItemsSoldValue.TabIndex = 1;
            lblItemsSoldValue.Text = "0";
            // 
            // cardProducts
            // 
            cardProducts.BackColor = Color.Transparent;
            cardProducts.BorderRadius = 12;
            cardProducts.Controls.Add(lblProductsTitle);
            cardProducts.Controls.Add(lblProductsValue);
            cardProducts.CustomizableEdges = customizableEdges37;
            cardProducts.Location = new Point(829, 105);
            cardProducts.Name = "cardProducts";
            cardProducts.ShadowDecoration.CustomizableEdges = customizableEdges38;
            cardProducts.ShadowDecoration.Depth = 5;
            cardProducts.ShadowDecoration.Enabled = true;
            cardProducts.Size = new Size(240, 110);
            cardProducts.TabIndex = 5;
            // 
            // lblProductsTitle
            // 
            lblProductsTitle.BackColor = Color.Transparent;
            lblProductsTitle.Font = new Font("Segoe UI", 9F);
            lblProductsTitle.ForeColor = Color.FromArgb(96, 105, 121);
            lblProductsTitle.Location = new Point(18, 16);
            lblProductsTitle.Name = "lblProductsTitle";
            lblProductsTitle.Size = new Size(105, 22);
            lblProductsTitle.TabIndex = 0;
            lblProductsTitle.Text = "Active Products";
            // 
            // lblProductsValue
            // 
            lblProductsValue.BackColor = Color.Transparent;
            lblProductsValue.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
            lblProductsValue.ForeColor = Color.FromArgb(28, 40, 63);
            lblProductsValue.Location = new Point(162, 45);
            lblProductsValue.Name = "lblProductsValue";
            lblProductsValue.Size = new Size(22, 47);
            lblProductsValue.TabIndex = 1;
            lblProductsValue.Text = "0";
            // 
            // salesChartPanel
            // 
            salesChartPanel.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            salesChartPanel.BackColor = Color.Transparent;
            salesChartPanel.BorderRadius = 12;
            salesChartPanel.Controls.Add(lblSalesChartTitle);
            salesChartPanel.CustomizableEdges = customizableEdges39;
            salesChartPanel.Location = new Point(28, 235);
            salesChartPanel.Name = "salesChartPanel";
            salesChartPanel.ShadowDecoration.CustomizableEdges = customizableEdges40;
            salesChartPanel.ShadowDecoration.Depth = 5;
            salesChartPanel.ShadowDecoration.Enabled = true;
            salesChartPanel.Size = new Size(635, 250);
            salesChartPanel.TabIndex = 6;
            salesChartPanel.Paint += salesChartPanel_Paint;
            // 
            // lblSalesChartTitle
            // 
            lblSalesChartTitle.BackColor = Color.Transparent;
            lblSalesChartTitle.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblSalesChartTitle.ForeColor = Color.FromArgb(28, 40, 63);
            lblSalesChartTitle.Location = new Point(20, 16);
            lblSalesChartTitle.Name = "lblSalesChartTitle";
            lblSalesChartTitle.Size = new Size(198, 30);
            lblSalesChartTitle.TabIndex = 0;
            lblSalesChartTitle.Text = "Top Selling Products";
            // 
            // inventoryChartPanel
            // 
            inventoryChartPanel.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            inventoryChartPanel.BackColor = Color.Transparent;
            inventoryChartPanel.BorderRadius = 12;
            inventoryChartPanel.Controls.Add(lblInventoryChartTitle);
            inventoryChartPanel.CustomizableEdges = customizableEdges41;
            inventoryChartPanel.Location = new Point(683, 235);
            inventoryChartPanel.Name = "inventoryChartPanel";
            inventoryChartPanel.ShadowDecoration.CustomizableEdges = customizableEdges42;
            inventoryChartPanel.ShadowDecoration.Depth = 5;
            inventoryChartPanel.ShadowDecoration.Enabled = true;
            inventoryChartPanel.Size = new Size(386, 250);
            inventoryChartPanel.TabIndex = 7;
            inventoryChartPanel.Paint += inventoryChartPanel_Paint;
            // 
            // lblInventoryChartTitle
            // 
            lblInventoryChartTitle.BackColor = Color.Transparent;
            lblInventoryChartTitle.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblInventoryChartTitle.ForeColor = Color.FromArgb(28, 40, 63);
            lblInventoryChartTitle.Location = new Point(20, 16);
            lblInventoryChartTitle.Name = "lblInventoryChartTitle";
            lblInventoryChartTitle.Size = new Size(159, 30);
            lblInventoryChartTitle.TabIndex = 0;
            lblInventoryChartTitle.Text = "Inventory Status";
            // 
            // lowStockPanel
            // 
            lowStockPanel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            lowStockPanel.BackColor = Color.Transparent;
            lowStockPanel.BorderRadius = 12;
            lowStockPanel.Controls.Add(lblLowStockTitle);
            lowStockPanel.Controls.Add(lowStockGrid);
            lowStockPanel.CustomizableEdges = customizableEdges43;
            lowStockPanel.Location = new Point(28, 505);
            lowStockPanel.Name = "lowStockPanel";
            lowStockPanel.ShadowDecoration.CustomizableEdges = customizableEdges44;
            lowStockPanel.ShadowDecoration.Depth = 5;
            lowStockPanel.ShadowDecoration.Enabled = true;
            lowStockPanel.Size = new Size(1041, 260);
            lowStockPanel.TabIndex = 8;
            // 
            // lblLowStockTitle
            // 
            lblLowStockTitle.BackColor = Color.Transparent;
            lblLowStockTitle.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblLowStockTitle.ForeColor = Color.FromArgb(28, 40, 63);
            lblLowStockTitle.Location = new Point(20, 15);
            lblLowStockTitle.Name = "lblLowStockTitle";
            lblLowStockTitle.Size = new Size(188, 30);
            lblLowStockTitle.TabIndex = 0;
            lblLowStockTitle.Text = "Low Stock Products";
            // 
            // lowStockGrid
            // 
            lowStockGrid.AllowUserToAddRows = false;
            lowStockGrid.AllowUserToDeleteRows = false;
            lowStockGrid.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = Color.White;
            lowStockGrid.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            lowStockGrid.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.SelectionBackColor = Color.FromArgb(232, 233, 236);
            dataGridViewCellStyle2.SelectionForeColor = Color.FromArgb(73, 83, 101);
            lowStockGrid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            lowStockGrid.ColumnHeadersHeight = 38;
            lowStockGrid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = Color.White;
            dataGridViewCellStyle3.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle3.ForeColor = Color.FromArgb(28, 40, 63);
            dataGridViewCellStyle3.SelectionBackColor = Color.FromArgb(232, 233, 236);
            dataGridViewCellStyle3.SelectionForeColor = Color.FromArgb(28, 40, 63);
            dataGridViewCellStyle3.WrapMode = DataGridViewTriState.False;
            lowStockGrid.DefaultCellStyle = dataGridViewCellStyle3;
            lowStockGrid.GridColor = Color.FromArgb(232, 233, 236);
            lowStockGrid.Location = new Point(20, 52);
            lowStockGrid.Name = "lowStockGrid";
            lowStockGrid.RowHeadersVisible = false;
            lowStockGrid.RowHeadersWidth = 51;
            lowStockGrid.RowTemplate.Height = 35;
            lowStockGrid.Size = new Size(1001, 190);
            lowStockGrid.TabIndex = 1;
            lowStockGrid.ThemeStyle.AlternatingRowsStyle.BackColor = Color.White;
            lowStockGrid.ThemeStyle.GridColor = Color.FromArgb(232, 233, 236);
            lowStockGrid.ThemeStyle.HeaderStyle.BackColor = Color.Empty;
            lowStockGrid.ThemeStyle.HeaderStyle.Font = null;
            lowStockGrid.ThemeStyle.HeaderStyle.ForeColor = Color.Empty;
            lowStockGrid.ThemeStyle.HeaderStyle.Height = 38;
            lowStockGrid.ThemeStyle.RowsStyle.Font = new Font("Segoe UI", 9F);
            lowStockGrid.ThemeStyle.RowsStyle.ForeColor = Color.FromArgb(28, 40, 63);
            lowStockGrid.ThemeStyle.RowsStyle.Height = 35;
            lowStockGrid.ThemeStyle.RowsStyle.SelectionBackColor = Color.FromArgb(232, 233, 236);
            lowStockGrid.ThemeStyle.RowsStyle.SelectionForeColor = Color.FromArgb(28, 40, 63);
            // 
            // loadingIndicator
            // 
            loadingIndicator.AutoStart = true;
            loadingIndicator.CircleSize = 1F;
            loadingIndicator.Location = new Point(555, 30);
            loadingIndicator.Name = "loadingIndicator";
            loadingIndicator.ShadowDecoration.CustomizableEdges = customizableEdges45;
            loadingIndicator.Size = new Size(35, 35);
            loadingIndicator.TabIndex = 9;
            loadingIndicator.Visible = false;
            // 
            // DashboardWinforms
            // 
            ClientSize = new Size(1400, 800);
            Controls.Add(contentPanel);
            Controls.Add(sidebarPanel);
            FormBorderStyle = FormBorderStyle.None;
            MinimumSize = new Size(1100, 700);
            Name = "DashboardWinforms";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "LARP Sari-Sari Smart App";
            WindowState = FormWindowState.Maximized;
            Load += Dashboard_Load;
            sidebarPanel.ResumeLayout(false);
            panelMenu.ResumeLayout(false);
            panelMenu.PerformLayout();
            panelUser.ResumeLayout(false);
            panelBrand.ResumeLayout(false);
            panelBrand.PerformLayout();
            panelLogo.ResumeLayout(false);
            contentPanel.ResumeLayout(false);
            contentPanel.PerformLayout();
            cardSales.ResumeLayout(false);
            cardSales.PerformLayout();
            cardTransactions.ResumeLayout(false);
            cardTransactions.PerformLayout();
            cardItemsSold.ResumeLayout(false);
            cardItemsSold.PerformLayout();
            cardProducts.ResumeLayout(false);
            cardProducts.PerformLayout();
            salesChartPanel.ResumeLayout(false);
            salesChartPanel.PerformLayout();
            inventoryChartPanel.ResumeLayout(false);
            inventoryChartPanel.PerformLayout();
            lowStockPanel.ResumeLayout(false);
            lowStockPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)lowStockGrid).EndInit();
            ResumeLayout(false);
        }
    }
}