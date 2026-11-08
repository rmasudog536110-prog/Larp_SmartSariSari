namespace Larp.winforms.Admin
{
    partial class PlansWinforms
    {
        private System.ComponentModel.IContainer components = null;

        private Guna.UI2.WinForms.Guna2Panel headerPanel;
        private Guna.UI2.WinForms.Guna2Panel summaryPanel;
        private Guna.UI2.WinForms.Guna2Panel plansSectionPanel;
        private Guna.UI2.WinForms.Guna2Panel tableSectionPanel;

        private Guna.UI2.WinForms.Guna2Panel totalPlansCard;
        private Guna.UI2.WinForms.Guna2Panel activePlansCard;
        private Guna.UI2.WinForms.Guna2Panel inactivePlansCard;
        private Guna.UI2.WinForms.Guna2Panel companiesCard;

        private Guna.UI2.WinForms.Guna2HtmlLabel lblTitle;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblSubtitle;

        private Guna.UI2.WinForms.Guna2HtmlLabel lblTotalPlans;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblTotalPlansValue;

        private Guna.UI2.WinForms.Guna2HtmlLabel lblActivePlans;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblActivePlansValue;

        private Guna.UI2.WinForms.Guna2HtmlLabel lblInactivePlans;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblInactivePlansValue;

        private Guna.UI2.WinForms.Guna2HtmlLabel lblCompanies;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblCompaniesValue;

        private Guna.UI2.WinForms.Guna2HtmlLabel lblPlansTitle;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblTableTitle;

        private FlowLayoutPanel plansPanel;
        private DataGridView gridPlans;

        private Guna.UI2.WinForms.Guna2Button btnAddPlan;
        private Guna.UI2.WinForms.Guna2Button btnDownloadPdf;
        private Guna.UI2.WinForms.Guna2Button btnRefresh;
        private Guna.UI2.WinForms.Guna2Button btnClose;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null)
                components.Dispose();

            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges9 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges10 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges3 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges4 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges5 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges6 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges7 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges8 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges19 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges20 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges11 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges12 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges13 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges14 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges15 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges16 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges17 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges18 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            headerPanel = new Guna.UI2.WinForms.Guna2Panel();
            lblTitle = new Guna.UI2.WinForms.Guna2HtmlLabel();
            lblSubtitle = new Guna.UI2.WinForms.Guna2HtmlLabel();
            btnAddPlan = new Guna.UI2.WinForms.Guna2Button();
            btnDownloadPdf = new Guna.UI2.WinForms.Guna2Button();
            btnRefresh = new Guna.UI2.WinForms.Guna2Button();
            btnClose = new Guna.UI2.WinForms.Guna2Button();
            summaryPanel = new Guna.UI2.WinForms.Guna2Panel();
            totalPlansCard = new Guna.UI2.WinForms.Guna2Panel();
            lblTotalPlans = new Guna.UI2.WinForms.Guna2HtmlLabel();
            lblTotalPlansValue = new Guna.UI2.WinForms.Guna2HtmlLabel();
            activePlansCard = new Guna.UI2.WinForms.Guna2Panel();
            lblActivePlans = new Guna.UI2.WinForms.Guna2HtmlLabel();
            lblActivePlansValue = new Guna.UI2.WinForms.Guna2HtmlLabel();
            inactivePlansCard = new Guna.UI2.WinForms.Guna2Panel();
            lblInactivePlans = new Guna.UI2.WinForms.Guna2HtmlLabel();
            lblInactivePlansValue = new Guna.UI2.WinForms.Guna2HtmlLabel();
            companiesCard = new Guna.UI2.WinForms.Guna2Panel();
            lblCompanies = new Guna.UI2.WinForms.Guna2HtmlLabel();
            lblCompaniesValue = new Guna.UI2.WinForms.Guna2HtmlLabel();
            plansSectionPanel = new Guna.UI2.WinForms.Guna2Panel();
            plansPanel = new FlowLayoutPanel();
            lblPlansTitle = new Guna.UI2.WinForms.Guna2HtmlLabel();
            tableSectionPanel = new Guna.UI2.WinForms.Guna2Panel();
            gridPlans = new DataGridView();
            lblTableTitle = new Guna.UI2.WinForms.Guna2HtmlLabel();
            headerPanel.SuspendLayout();
            summaryPanel.SuspendLayout();
            totalPlansCard.SuspendLayout();
            activePlansCard.SuspendLayout();
            inactivePlansCard.SuspendLayout();
            companiesCard.SuspendLayout();
            plansSectionPanel.SuspendLayout();
            tableSectionPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)gridPlans).BeginInit();
            SuspendLayout();
            // 
            // headerPanel
            // 
            headerPanel.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            headerPanel.BorderColor = Color.FromArgb(225, 227, 231);
            headerPanel.BorderThickness = 1;
            headerPanel.Controls.Add(lblTitle);
            headerPanel.Controls.Add(lblSubtitle);
            headerPanel.Controls.Add(btnAddPlan);
            headerPanel.Controls.Add(btnDownloadPdf);
            headerPanel.Controls.Add(btnRefresh);
            headerPanel.Controls.Add(btnClose);
            headerPanel.CustomizableEdges = customizableEdges9;
            headerPanel.FillColor = Color.White;
            headerPanel.Location = new Point(0, 0);
            headerPanel.Name = "headerPanel";
            headerPanel.ShadowDecoration.CustomizableEdges = customizableEdges10;
            headerPanel.Size = new Size(1471, 100);
            headerPanel.TabIndex = 0;
            // 
            // lblTitle
            // 
            lblTitle.BackColor = Color.Transparent;
            lblTitle.Font = new Font("Segoe UI Semibold", 22F);
            lblTitle.ForeColor = Color.FromArgb(28, 40, 63);
            lblTitle.Location = new Point(25, 12);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(255, 52);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "Plans & Pricing";
            // 
            // lblSubtitle
            // 
            lblSubtitle.BackColor = Color.Transparent;
            lblSubtitle.Font = new Font("Segoe UI", 10F);
            lblSubtitle.ForeColor = Color.FromArgb(96, 105, 121);
            lblSubtitle.Location = new Point(25, 60);
            lblSubtitle.Name = "lblSubtitle";
            lblSubtitle.Size = new Size(495, 25);
            lblSubtitle.TabIndex = 1;
            lblSubtitle.Text = "Manage subscription plans, pricing, limits, and included modules";
            // 
            // btnAddPlan
            // 
            btnAddPlan.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnAddPlan.BorderRadius = 7;
            btnAddPlan.CustomizableEdges = customizableEdges1;
            btnAddPlan.FillColor = Color.FromArgb(28, 40, 63);
            btnAddPlan.Font = new Font("Segoe UI Semibold", 9F);
            btnAddPlan.ForeColor = Color.White;
            btnAddPlan.Location = new Point(981, 30);
            btnAddPlan.Name = "btnAddPlan";
            btnAddPlan.ShadowDecoration.CustomizableEdges = customizableEdges2;
            btnAddPlan.Size = new Size(120, 38);
            btnAddPlan.TabIndex = 2;
            btnAddPlan.Text = "+ Add Plan";
            btnAddPlan.Click += BtnAddPlan_Click;
            // 
            // btnDownloadPdf
            // 
            btnDownloadPdf.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnDownloadPdf.BorderRadius = 7;
            btnDownloadPdf.CustomizableEdges = customizableEdges3;
            btnDownloadPdf.FillColor = Color.FromArgb(46, 107, 69);
            btnDownloadPdf.Font = new Font("Segoe UI Semibold", 9F);
            btnDownloadPdf.ForeColor = Color.White;
            btnDownloadPdf.Location = new Point(1116, 30);
            btnDownloadPdf.Name = "btnDownloadPdf";
            btnDownloadPdf.Padding = new Padding(0, 5, 0, 0);
            btnDownloadPdf.ShadowDecoration.CustomizableEdges = customizableEdges4;
            btnDownloadPdf.Size = new Size(130, 38);
            btnDownloadPdf.TabIndex = 3;
            btnDownloadPdf.Text = "Download PDF";
            btnDownloadPdf.Click += BtnPdf_Click;
            // 
            // btnRefresh
            // 
            btnRefresh.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnRefresh.BorderRadius = 7;
            btnRefresh.CustomizableEdges = customizableEdges5;
            btnRefresh.FillColor = Color.FromArgb(28, 40, 63);
            btnRefresh.Font = new Font("Segoe UI Semibold", 9F);
            btnRefresh.ForeColor = Color.White;
            btnRefresh.Location = new Point(1261, 30);
            btnRefresh.Name = "btnRefresh";
            btnRefresh.ShadowDecoration.CustomizableEdges = customizableEdges6;
            btnRefresh.Size = new Size(100, 38);
            btnRefresh.TabIndex = 4;
            btnRefresh.Text = "Refresh";
            btnRefresh.Click += BtnRefresh_Click;
            // 
            // btnClose
            // 
            btnClose.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnClose.BorderRadius = 7;
            btnClose.CustomizableEdges = customizableEdges7;
            btnClose.FillColor = Color.FromArgb(110, 116, 127);
            btnClose.Font = new Font("Segoe UI Semibold", 9F);
            btnClose.ForeColor = Color.White;
            btnClose.Location = new Point(1376, 30);
            btnClose.Name = "btnClose";
            btnClose.ShadowDecoration.CustomizableEdges = customizableEdges8;
            btnClose.Size = new Size(70, 38);
            btnClose.TabIndex = 5;
            btnClose.Text = "Close";
            btnClose.Click += BtnClose_Click;
            // 
            // summaryPanel
            // 
            summaryPanel.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            summaryPanel.Controls.Add(totalPlansCard);
            summaryPanel.Controls.Add(activePlansCard);
            summaryPanel.Controls.Add(inactivePlansCard);
            summaryPanel.Controls.Add(companiesCard);
            summaryPanel.CustomizableEdges = customizableEdges19;
            summaryPanel.FillColor = Color.FromArgb(245, 246, 248);
            summaryPanel.Location = new Point(0, 100);
            summaryPanel.Name = "summaryPanel";
            summaryPanel.ShadowDecoration.CustomizableEdges = customizableEdges20;
            summaryPanel.Size = new Size(1471, 105);
            summaryPanel.TabIndex = 1;
            // 
            // totalPlansCard
            // 
            totalPlansCard.BorderColor = Color.FromArgb(225, 227, 231);
            totalPlansCard.BorderRadius = 8;
            totalPlansCard.BorderThickness = 1;
            totalPlansCard.Controls.Add(lblTotalPlans);
            totalPlansCard.Controls.Add(lblTotalPlansValue);
            totalPlansCard.CustomizableEdges = customizableEdges11;
            totalPlansCard.FillColor = Color.White;
            totalPlansCard.Location = new Point(25, 13);
            totalPlansCard.Name = "totalPlansCard";
            totalPlansCard.ShadowDecoration.CustomizableEdges = customizableEdges12;
            totalPlansCard.Size = new Size(245, 78);
            totalPlansCard.TabIndex = 0;
            // 
            // lblTotalPlans
            // 
            lblTotalPlans.BackColor = Color.Transparent;
            lblTotalPlans.Font = new Font("Segoe UI Semibold", 9F);
            lblTotalPlans.ForeColor = Color.FromArgb(96, 105, 121);
            lblTotalPlans.Location = new Point(15, 9);
            lblTotalPlans.Name = "lblTotalPlans";
            lblTotalPlans.Size = new Size(97, 22);
            lblTotalPlans.TabIndex = 0;
            lblTotalPlans.Text = "TOTAL PLANS";
            // 
            // lblTotalPlansValue
            // 
            lblTotalPlansValue.BackColor = Color.Transparent;
            lblTotalPlansValue.Font = new Font("Segoe UI Semibold", 20F);
            lblTotalPlansValue.ForeColor = Color.FromArgb(28, 40, 63);
            lblTotalPlansValue.Location = new Point(190, 28);
            lblTotalPlansValue.Name = "lblTotalPlansValue";
            lblTotalPlansValue.Size = new Size(21, 47);
            lblTotalPlansValue.TabIndex = 1;
            lblTotalPlansValue.Text = "0";
            // 
            // activePlansCard
            // 
            activePlansCard.BorderColor = Color.FromArgb(225, 227, 231);
            activePlansCard.BorderRadius = 8;
            activePlansCard.BorderThickness = 1;
            activePlansCard.Controls.Add(lblActivePlans);
            activePlansCard.Controls.Add(lblActivePlansValue);
            activePlansCard.CustomizableEdges = customizableEdges13;
            activePlansCard.FillColor = Color.White;
            activePlansCard.Location = new Point(290, 13);
            activePlansCard.Name = "activePlansCard";
            activePlansCard.ShadowDecoration.CustomizableEdges = customizableEdges14;
            activePlansCard.Size = new Size(245, 78);
            activePlansCard.TabIndex = 1;
            // 
            // lblActivePlans
            // 
            lblActivePlans.BackColor = Color.Transparent;
            lblActivePlans.Font = new Font("Segoe UI Semibold", 9F);
            lblActivePlans.ForeColor = Color.FromArgb(96, 105, 121);
            lblActivePlans.Location = new Point(15, 9);
            lblActivePlans.Name = "lblActivePlans";
            lblActivePlans.Size = new Size(102, 22);
            lblActivePlans.TabIndex = 0;
            lblActivePlans.Text = "ACTIVE PLANS";
            // 
            // lblActivePlansValue
            // 
            lblActivePlansValue.BackColor = Color.Transparent;
            lblActivePlansValue.Font = new Font("Segoe UI Semibold", 20F);
            lblActivePlansValue.ForeColor = Color.FromArgb(46, 107, 69);
            lblActivePlansValue.Location = new Point(196, 28);
            lblActivePlansValue.Name = "lblActivePlansValue";
            lblActivePlansValue.Size = new Size(21, 47);
            lblActivePlansValue.TabIndex = 1;
            lblActivePlansValue.Text = "0";
            // 
            // inactivePlansCard
            // 
            inactivePlansCard.BorderColor = Color.FromArgb(225, 227, 231);
            inactivePlansCard.BorderRadius = 8;
            inactivePlansCard.BorderThickness = 1;
            inactivePlansCard.Controls.Add(lblInactivePlans);
            inactivePlansCard.Controls.Add(lblInactivePlansValue);
            inactivePlansCard.CustomizableEdges = customizableEdges15;
            inactivePlansCard.FillColor = Color.White;
            inactivePlansCard.Location = new Point(555, 13);
            inactivePlansCard.Name = "inactivePlansCard";
            inactivePlansCard.ShadowDecoration.CustomizableEdges = customizableEdges16;
            inactivePlansCard.Size = new Size(245, 78);
            inactivePlansCard.TabIndex = 2;
            // 
            // lblInactivePlans
            // 
            lblInactivePlans.BackColor = Color.Transparent;
            lblInactivePlans.Font = new Font("Segoe UI Semibold", 9F);
            lblInactivePlans.ForeColor = Color.FromArgb(96, 105, 121);
            lblInactivePlans.Location = new Point(15, 9);
            lblInactivePlans.Name = "lblInactivePlans";
            lblInactivePlans.Size = new Size(118, 22);
            lblInactivePlans.TabIndex = 0;
            lblInactivePlans.Text = "INACTIVE PLANS";
            // 
            // lblInactivePlansValue
            // 
            lblInactivePlansValue.BackColor = Color.Transparent;
            lblInactivePlansValue.Font = new Font("Segoe UI Semibold", 20F);
            lblInactivePlansValue.ForeColor = Color.FromArgb(150, 70, 70);
            lblInactivePlansValue.Location = new Point(195, 28);
            lblInactivePlansValue.Name = "lblInactivePlansValue";
            lblInactivePlansValue.Size = new Size(21, 47);
            lblInactivePlansValue.TabIndex = 1;
            lblInactivePlansValue.Text = "0";
            // 
            // companiesCard
            // 
            companiesCard.BorderColor = Color.FromArgb(225, 227, 231);
            companiesCard.BorderRadius = 8;
            companiesCard.BorderThickness = 1;
            companiesCard.Controls.Add(lblCompanies);
            companiesCard.Controls.Add(lblCompaniesValue);
            companiesCard.CustomizableEdges = customizableEdges17;
            companiesCard.FillColor = Color.White;
            companiesCard.Location = new Point(820, 13);
            companiesCard.Name = "companiesCard";
            companiesCard.ShadowDecoration.CustomizableEdges = customizableEdges18;
            companiesCard.Size = new Size(245, 78);
            companiesCard.TabIndex = 3;
            // 
            // lblCompanies
            // 
            lblCompanies.BackColor = Color.Transparent;
            lblCompanies.Font = new Font("Segoe UI Semibold", 9F);
            lblCompanies.ForeColor = Color.FromArgb(96, 105, 121);
            lblCompanies.Location = new Point(15, 9);
            lblCompanies.Name = "lblCompanies";
            lblCompanies.Size = new Size(141, 22);
            lblCompanies.TabIndex = 0;
            lblCompanies.Text = "ACTIVE COMPANIES";
            // 
            // lblCompaniesValue
            // 
            lblCompaniesValue.BackColor = Color.Transparent;
            lblCompaniesValue.Font = new Font("Segoe UI Semibold", 20F);
            lblCompaniesValue.ForeColor = Color.FromArgb(28, 40, 63);
            lblCompaniesValue.Location = new Point(190, 28);
            lblCompaniesValue.Name = "lblCompaniesValue";
            lblCompaniesValue.Size = new Size(21, 47);
            lblCompaniesValue.TabIndex = 1;
            lblCompaniesValue.Text = "0";
            // 
            // plansSectionPanel
            // 
            plansSectionPanel.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            plansSectionPanel.Controls.Add(plansPanel);
            plansSectionPanel.Controls.Add(lblPlansTitle);
            plansSectionPanel.CustomizableEdges = customizableEdges9;
            plansSectionPanel.FillColor = Color.White;
            plansSectionPanel.Location = new Point(0, 205);
            plansSectionPanel.Name = "plansSectionPanel";
            plansSectionPanel.Padding = new Padding(25, 15, 25, 15);
            plansSectionPanel.ShadowDecoration.CustomizableEdges = customizableEdges10;
            plansSectionPanel.Size = new Size(1471, 553);
            plansSectionPanel.TabIndex = 2;
            // 
            // plansPanel
            // 
            plansPanel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            plansPanel.AutoScroll = true;
            plansPanel.BackColor = Color.White;
            plansPanel.Location = new Point(25, 52);
            plansPanel.Name = "plansPanel";
            plansPanel.Padding = new Padding(5);
            plansPanel.Size = new Size(1421, 483);
            plansPanel.TabIndex = 1;
            plansPanel.WrapContents = false;
            // 
            // lblPlansTitle
            // 
            lblPlansTitle.BackColor = Color.Transparent;
            lblPlansTitle.Font = new Font("Segoe UI Semibold", 15F);
            lblPlansTitle.ForeColor = Color.FromArgb(28, 40, 63);
            lblPlansTitle.Location = new Point(25, 12);
            lblPlansTitle.Name = "lblPlansTitle";
            lblPlansTitle.Size = new Size(64, 37);
            lblPlansTitle.TabIndex = 0;
            lblPlansTitle.Text = "Plans";
            // 
            // tableSectionPanel
            // 
            tableSectionPanel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            tableSectionPanel.Controls.Add(gridPlans);
            tableSectionPanel.Controls.Add(lblTableTitle);
            tableSectionPanel.CustomizableEdges = customizableEdges1;
            tableSectionPanel.FillColor = Color.White;
            tableSectionPanel.Location = new Point(0, 764);
            tableSectionPanel.Name = "tableSectionPanel";
            tableSectionPanel.Padding = new Padding(25, 15, 25, 20);
            tableSectionPanel.ShadowDecoration.CustomizableEdges = customizableEdges2;
            tableSectionPanel.Size = new Size(1471, 315);
            tableSectionPanel.TabIndex = 3;
            // 
            // gridPlans
            // 
            gridPlans.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            gridPlans.BackgroundColor = Color.White;
            gridPlans.BorderStyle = BorderStyle.None;
            gridPlans.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = Color.FromArgb(28, 40, 63);
            dataGridViewCellStyle1.Font = new Font("Segoe UI Semibold", 9F);
            dataGridViewCellStyle1.ForeColor = Color.White;
            dataGridViewCellStyle1.SelectionBackColor = Color.FromArgb(46, 107, 69);
            dataGridViewCellStyle1.SelectionForeColor = Color.White;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            gridPlans.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            gridPlans.ColumnHeadersHeight = 42;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = Color.White;
            dataGridViewCellStyle2.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle2.ForeColor = Color.FromArgb(40, 45, 55);
            dataGridViewCellStyle2.SelectionBackColor = Color.FromArgb(225, 234, 228);
            dataGridViewCellStyle2.SelectionForeColor = Color.FromArgb(28, 40, 63);
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.False;
            gridPlans.DefaultCellStyle = dataGridViewCellStyle2;
            gridPlans.EnableHeadersVisualStyles = false;
            gridPlans.GridColor = Color.FromArgb(230, 232, 236);
            gridPlans.Location = new Point(25, 55);
            gridPlans.MultiSelect = false;
            gridPlans.Name = "gridPlans";
            gridPlans.RowHeadersVisible = false;
            gridPlans.RowHeadersWidth = 51;
            gridPlans.RowTemplate.Height = 54;
            gridPlans.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            gridPlans.Size = new Size(1421, 235);
            gridPlans.TabIndex = 1;
            // 
            // lblTableTitle
            // 
            lblTableTitle.BackColor = Color.Transparent;
            lblTableTitle.Font = new Font("Segoe UI Semibold", 15F);
            lblTableTitle.ForeColor = Color.FromArgb(28, 40, 63);
            lblTableTitle.Location = new Point(25, 12);
            lblTableTitle.Name = "lblTableTitle";
            lblTableTitle.Size = new Size(211, 37);
            lblTableTitle.TabIndex = 0;
            lblTableTitle.Text = "Plan Management";
            // 
            // PlansWinforms
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoScroll = true;
            BackColor = Color.FromArgb(245, 246, 248);
            ClientSize = new Size(1471, 950);
            Controls.Add(tableSectionPanel);
            Controls.Add(plansSectionPanel);
            Controls.Add(summaryPanel);
            Controls.Add(headerPanel);
            Name = "PlansWinforms";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Plans & Pricing";
            WindowState = FormWindowState.Maximized;
            headerPanel.ResumeLayout(false);
            headerPanel.PerformLayout();
            summaryPanel.ResumeLayout(false);
            totalPlansCard.ResumeLayout(false);
            totalPlansCard.PerformLayout();
            activePlansCard.ResumeLayout(false);
            activePlansCard.PerformLayout();
            inactivePlansCard.ResumeLayout(false);
            inactivePlansCard.PerformLayout();
            companiesCard.ResumeLayout(false);
            companiesCard.PerformLayout();
            plansSectionPanel.ResumeLayout(false);
            plansSectionPanel.PerformLayout();
            tableSectionPanel.ResumeLayout(false);
            tableSectionPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)gridPlans).EndInit();
            ResumeLayout(false);
        }
    }
}