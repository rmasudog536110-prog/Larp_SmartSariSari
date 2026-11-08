using System.Windows.Forms;
using System.Drawing;

namespace Larp.winforms.Admin
{
    partial class AdminWinforms
    {
        private System.ComponentModel.IContainer components = null;

        // Sidebar
        private Guna.UI2.WinForms.Guna2Panel panelNav;
        private System.Windows.Forms.Panel panelLogo;
        private System.Windows.Forms.Label lblLogoLetter;
        private System.Windows.Forms.Label lblAdminBrand;
        private System.Windows.Forms.Label lblAdminSub;

        private System.Windows.Forms.Label lblSectionOverview;
        private System.Windows.Forms.Label lblSectionSubscriptions;
        private System.Windows.Forms.Label lblSectionLegal;
        private System.Windows.Forms.Label lblSectionAdministration;

        private Guna.UI2.WinForms.Guna2Button btnNavOverview;
        private Guna.UI2.WinForms.Guna2Button btnNavSubscriptions;
        private Guna.UI2.WinForms.Guna2Button btnNavPlans;
        private Guna.UI2.WinForms.Guna2Button btnNavTerms;
        private Guna.UI2.WinForms.Guna2Button btnNavBackup;

        private System.Windows.Forms.Panel panelUser;
        private System.Windows.Forms.Panel panelUserLine;
        private System.Windows.Forms.Panel panelAvatar;
        private System.Windows.Forms.Label lblAdminUser;
        private System.Windows.Forms.Label lblAdminRole;
        private Guna.UI2.WinForms.Guna2Button LogoutBtn;

        // Content
        private System.Windows.Forms.Panel panelContent;
        private System.Windows.Forms.Panel panelHeader;
        private System.Windows.Forms.Label lblOverviewTitle;
        private System.Windows.Forms.Label lblOverviewSubtitle;
        private Guna.UI2.WinForms.Guna2Button btnRefreshOverview;
        private System.Windows.Forms.FlowLayoutPanel flowSummary;
        private System.Windows.Forms.Label lblCompaniesTitle;
        private System.Windows.Forms.FlowLayoutPanel flowCompanies;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();

            base.Dispose(disposing);
        }

        // Keep this method designer-friendly: plain property assignments and named
        // event handlers only (no lambdas, local functions or local variables).
        // Colors: navy (28, 40, 63), accent blue (37, 99, 235), nav hover (44, 60, 92).
        private void InitializeComponent()
        {
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges13 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges14 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
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
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges15 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges16 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            panelNav = new Guna.UI2.WinForms.Guna2Panel();
            panelUser = new Panel();
            LogoutBtn = new Guna.UI2.WinForms.Guna2Button();
            lblAdminRole = new Label();
            lblAdminUser = new Label();
            panelAvatar = new Panel();
            panelUserLine = new Panel();
            btnNavBackup = new Guna.UI2.WinForms.Guna2Button();
            lblSectionAdministration = new Label();
            btnNavTerms = new Guna.UI2.WinForms.Guna2Button();
            lblSectionLegal = new Label();
            btnNavPlans = new Guna.UI2.WinForms.Guna2Button();
            btnNavSubscriptions = new Guna.UI2.WinForms.Guna2Button();
            lblSectionSubscriptions = new Label();
            btnNavOverview = new Guna.UI2.WinForms.Guna2Button();
            lblSectionOverview = new Label();
            lblAdminSub = new Label();
            lblAdminBrand = new Label();
            panelLogo = new Panel();
            lblLogoLetter = new Label();
            panelContent = new Panel();
            flowCompanies = new FlowLayoutPanel();
            lblCompaniesTitle = new Label();
            flowSummary = new FlowLayoutPanel();
            panelHeader = new Panel();
            btnRefreshOverview = new Guna.UI2.WinForms.Guna2Button();
            lblOverviewSubtitle = new Label();
            lblOverviewTitle = new Label();
            panelNav.SuspendLayout();
            panelUser.SuspendLayout();
            panelLogo.SuspendLayout();
            panelContent.SuspendLayout();
            panelHeader.SuspendLayout();
            SuspendLayout();
            // 
            // panelNav
            // 
            panelNav.BackColor = Color.FromArgb(28, 40, 63);
            panelNav.Controls.Add(panelUser);
            panelNav.Controls.Add(btnNavBackup);
            panelNav.Controls.Add(lblSectionAdministration);
            panelNav.Controls.Add(btnNavTerms);
            panelNav.Controls.Add(lblSectionLegal);
            panelNav.Controls.Add(btnNavPlans);
            panelNav.Controls.Add(btnNavSubscriptions);
            panelNav.Controls.Add(lblSectionSubscriptions);
            panelNav.Controls.Add(btnNavOverview);
            panelNav.Controls.Add(lblSectionOverview);
            panelNav.Controls.Add(lblAdminSub);
            panelNav.Controls.Add(lblAdminBrand);
            panelNav.Controls.Add(panelLogo);
            panelNav.CustomizableEdges = customizableEdges13;
            panelNav.Dock = DockStyle.Left;
            panelNav.Location = new Point(0, 0);
            panelNav.Name = "panelNav";
            panelNav.ShadowDecoration.CustomizableEdges = customizableEdges14;
            panelNav.Size = new Size(260, 720);
            panelNav.TabIndex = 1;
            // 
            // panelUser
            // 
            panelUser.BackColor = Color.FromArgb(22, 32, 52);
            panelUser.Controls.Add(LogoutBtn);
            panelUser.Controls.Add(lblAdminRole);
            panelUser.Controls.Add(lblAdminUser);
            panelUser.Controls.Add(panelAvatar);
            panelUser.Controls.Add(panelUserLine);
            panelUser.Dock = DockStyle.Bottom;
            panelUser.Location = new Point(0, 608);
            panelUser.Name = "panelUser";
            panelUser.Size = new Size(260, 112);
            panelUser.TabIndex = 12;
            // 
            // LogoutBtn
            // 
            LogoutBtn.BorderRadius = 6;
            LogoutBtn.CustomizableEdges = customizableEdges1;
            LogoutBtn.FillColor = Color.Transparent;
            LogoutBtn.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            LogoutBtn.ForeColor = Color.FromArgb(239, 68, 68);
            LogoutBtn.HoverState.FillColor = Color.FromArgb(44, 60, 92);
            LogoutBtn.Location = new Point(62, 70);
            LogoutBtn.Name = "LogoutBtn";
            LogoutBtn.ShadowDecoration.CustomizableEdges = customizableEdges2;
            LogoutBtn.Size = new Size(150, 30);
            LogoutBtn.TabIndex = 13;
            LogoutBtn.Text = "Sign Out";
            LogoutBtn.TextAlign = HorizontalAlignment.Left;
            LogoutBtn.Click += LogoutBtn_Click;
            // 
            // lblAdminRole
            // 
            lblAdminRole.AutoEllipsis = true;
            lblAdminRole.BackColor = Color.Transparent;
            lblAdminRole.Font = new Font("Segoe UI", 8.5F);
            lblAdminRole.ForeColor = Color.FromArgb(150, 163, 190);
            lblAdminRole.Location = new Point(70, 42);
            lblAdminRole.Name = "lblAdminRole";
            lblAdminRole.Size = new Size(180, 20);
            lblAdminRole.TabIndex = 14;
            // 
            // lblAdminUser
            // 
            lblAdminUser.AutoEllipsis = true;
            lblAdminUser.BackColor = Color.Transparent;
            lblAdminUser.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblAdminUser.ForeColor = Color.White;
            lblAdminUser.Location = new Point(70, 20);
            lblAdminUser.Name = "lblAdminUser";
            lblAdminUser.Size = new Size(180, 22);
            lblAdminUser.TabIndex = 15;
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
            // btnNavBackup
            // 
            btnNavBackup.BorderRadius = 6;
            btnNavBackup.CustomizableEdges = customizableEdges3;
            btnNavBackup.FillColor = Color.Transparent;
            btnNavBackup.Font = new Font("Segoe UI", 10F);
            btnNavBackup.ForeColor = Color.White;
            btnNavBackup.HoverState.FillColor = Color.FromArgb(44, 60, 92);
            btnNavBackup.ImageAlign = HorizontalAlignment.Left;
            btnNavBackup.ImageOffset = new Point(14, 0);
            btnNavBackup.Location = new Point(14, 386);
            btnNavBackup.Name = "btnNavBackup";
            btnNavBackup.ShadowDecoration.CustomizableEdges = customizableEdges4;
            btnNavBackup.Size = new Size(236, 52);
            btnNavBackup.TabIndex = 11;
            btnNavBackup.Text = "Backup and Restore";
            btnNavBackup.TextAlign = HorizontalAlignment.Left;
            btnNavBackup.TextOffset = new Point(40, 0);
            btnNavBackup.Click += BtnNavBackup_Click;
            // 
            // lblSectionAdministration
            // 
            lblSectionAdministration.AutoSize = true;
            lblSectionAdministration.BackColor = Color.Transparent;
            lblSectionAdministration.Font = new Font("Segoe UI", 7.5F, FontStyle.Bold);
            lblSectionAdministration.ForeColor = Color.FromArgb(125, 140, 170);
            lblSectionAdministration.Location = new Point(22, 366);
            lblSectionAdministration.Name = "lblSectionAdministration";
            lblSectionAdministration.Size = new Size(120, 17);
            lblSectionAdministration.TabIndex = 10;
            lblSectionAdministration.Text = "ADMINISTRATION";
            // 
            // btnNavTerms
            // 
            btnNavTerms.BorderRadius = 6;
            btnNavTerms.CustomizableEdges = customizableEdges5;
            btnNavTerms.FillColor = Color.Transparent;
            btnNavTerms.Font = new Font("Segoe UI", 10F);
            btnNavTerms.ForeColor = Color.White;
            btnNavTerms.HoverState.FillColor = Color.FromArgb(44, 60, 92);
            btnNavTerms.ImageAlign = HorizontalAlignment.Left;
            btnNavTerms.ImageOffset = new Point(14, 0);
            btnNavTerms.Location = new Point(12, 308);
            btnNavTerms.Name = "btnNavTerms";
            btnNavTerms.ShadowDecoration.CustomizableEdges = customizableEdges6;
            btnNavTerms.Size = new Size(236, 55);
            btnNavTerms.TabIndex = 9;
            btnNavTerms.Text = "Terms & Conditions";
            btnNavTerms.TextAlign = HorizontalAlignment.Left;
            btnNavTerms.TextOffset = new Point(40, 0);
            btnNavTerms.Click += BtnNavTerms_Click;
            // 
            // lblSectionLegal
            // 
            lblSectionLegal.AutoSize = true;
            lblSectionLegal.BackColor = Color.Transparent;
            lblSectionLegal.Font = new Font("Segoe UI", 7.5F, FontStyle.Bold);
            lblSectionLegal.ForeColor = Color.FromArgb(125, 140, 170);
            lblSectionLegal.Location = new Point(22, 288);
            lblSectionLegal.Name = "lblSectionLegal";
            lblSectionLegal.Size = new Size(47, 17);
            lblSectionLegal.TabIndex = 8;
            lblSectionLegal.Text = "LEGAL";
            // 
            // btnNavPlans
            // 
            btnNavPlans.BorderRadius = 6;
            btnNavPlans.CustomizableEdges = customizableEdges7;
            btnNavPlans.FillColor = Color.Transparent;
            btnNavPlans.Font = new Font("Segoe UI", 10F);
            btnNavPlans.ForeColor = Color.White;
            btnNavPlans.HoverState.FillColor = Color.FromArgb(44, 60, 92);
            btnNavPlans.ImageAlign = HorizontalAlignment.Left;
            btnNavPlans.ImageOffset = new Point(14, 0);
            btnNavPlans.Location = new Point(12, 231);
            btnNavPlans.Name = "btnNavPlans";
            btnNavPlans.ShadowDecoration.CustomizableEdges = customizableEdges8;
            btnNavPlans.Size = new Size(236, 54);
            btnNavPlans.TabIndex = 7;
            btnNavPlans.Text = "Plans & Pricing";
            btnNavPlans.TextAlign = HorizontalAlignment.Left;
            btnNavPlans.TextOffset = new Point(40, 0);
            btnNavPlans.Click += BtnNavPlans_Click;
            // 
            // btnNavSubscriptions
            // 
            btnNavSubscriptions.BorderRadius = 6;
            btnNavSubscriptions.CustomizableEdges = customizableEdges9;
            btnNavSubscriptions.FillColor = Color.Transparent;
            btnNavSubscriptions.Font = new Font("Segoe UI", 10F);
            btnNavSubscriptions.ForeColor = Color.White;
            btnNavSubscriptions.HoverState.FillColor = Color.FromArgb(44, 60, 92);
            btnNavSubscriptions.ImageAlign = HorizontalAlignment.Left;
            btnNavSubscriptions.ImageOffset = new Point(14, 0);
            btnNavSubscriptions.Location = new Point(14, 186);
            btnNavSubscriptions.Name = "btnNavSubscriptions";
            btnNavSubscriptions.ShadowDecoration.CustomizableEdges = customizableEdges10;
            btnNavSubscriptions.Size = new Size(236, 53);
            btnNavSubscriptions.TabIndex = 6;
            btnNavSubscriptions.Text = "Subscriptions";
            btnNavSubscriptions.TextAlign = HorizontalAlignment.Left;
            btnNavSubscriptions.TextOffset = new Point(40, 0);
            btnNavSubscriptions.Click += BtnNavSubscriptions_Click;
            // 
            // lblSectionSubscriptions
            // 
            lblSectionSubscriptions.AutoSize = true;
            lblSectionSubscriptions.BackColor = Color.Transparent;
            lblSectionSubscriptions.Font = new Font("Segoe UI", 7.5F, FontStyle.Bold);
            lblSectionSubscriptions.ForeColor = Color.FromArgb(125, 140, 170);
            lblSectionSubscriptions.Location = new Point(22, 166);
            lblSectionSubscriptions.Name = "lblSectionSubscriptions";
            lblSectionSubscriptions.Size = new Size(106, 17);
            lblSectionSubscriptions.TabIndex = 5;
            lblSectionSubscriptions.Text = "SUBSCRIPTIONS";
            // 
            // btnNavOverview
            // 
            btnNavOverview.BorderRadius = 6;
            btnNavOverview.CustomizableEdges = customizableEdges11;
            btnNavOverview.FillColor = Color.FromArgb(37, 99, 235);
            btnNavOverview.Font = new Font("Segoe UI", 10F);
            btnNavOverview.ForeColor = Color.White;
            btnNavOverview.HoverState.FillColor = Color.FromArgb(44, 60, 92);
            btnNavOverview.ImageAlign = HorizontalAlignment.Left;
            btnNavOverview.ImageOffset = new Point(14, 0);
            btnNavOverview.Location = new Point(21, 108);
            btnNavOverview.Name = "btnNavOverview";
            btnNavOverview.ShadowDecoration.CustomizableEdges = customizableEdges12;
            btnNavOverview.Size = new Size(236, 55);
            btnNavOverview.TabIndex = 4;
            btnNavOverview.Text = "Overview";
            btnNavOverview.TextAlign = HorizontalAlignment.Left;
            btnNavOverview.TextOffset = new Point(40, 0);
            btnNavOverview.Click += BtnNavOverview_Click;
            // 
            // lblSectionOverview
            // 
            lblSectionOverview.AutoSize = true;
            lblSectionOverview.BackColor = Color.Transparent;
            lblSectionOverview.Font = new Font("Segoe UI", 7.5F, FontStyle.Bold);
            lblSectionOverview.ForeColor = Color.FromArgb(125, 140, 170);
            lblSectionOverview.Location = new Point(22, 88);
            lblSectionOverview.Name = "lblSectionOverview";
            lblSectionOverview.Size = new Size(159, 17);
            lblSectionOverview.TabIndex = 3;
            lblSectionOverview.Text = "BUSINESS INTELLIGENCE";
            // 
            // lblAdminSub
            // 
            lblAdminSub.AutoSize = true;
            lblAdminSub.BackColor = Color.Transparent;
            lblAdminSub.Font = new Font("Segoe UI", 8.5F);
            lblAdminSub.ForeColor = Color.FromArgb(150, 163, 190);
            lblAdminSub.Location = new Point(64, 43);
            lblAdminSub.Name = "lblAdminSub";
            lblAdminSub.Size = new Size(146, 20);
            lblAdminSub.TabIndex = 2;
            lblAdminSub.Text = "SmartSariSari Admin";
            // 
            // lblAdminBrand
            // 
            lblAdminBrand.AutoSize = true;
            lblAdminBrand.BackColor = Color.Transparent;
            lblAdminBrand.Font = new Font("Segoe UI", 13F, FontStyle.Bold);
            lblAdminBrand.ForeColor = Color.White;
            lblAdminBrand.Location = new Point(62, 20);
            lblAdminBrand.Name = "lblAdminBrand";
            lblAdminBrand.Size = new Size(67, 30);
            lblAdminBrand.TabIndex = 1;
            lblAdminBrand.Text = "LARP";
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
            // panelContent
            // 
            panelContent.BackColor = Color.FromArgb(245, 246, 248);
            panelContent.Controls.Add(flowCompanies);
            panelContent.Controls.Add(lblCompaniesTitle);
            panelContent.Controls.Add(flowSummary);
            panelContent.Controls.Add(panelHeader);
            panelContent.Dock = DockStyle.Fill;
            panelContent.Location = new Point(260, 0);
            panelContent.Name = "panelContent";
            panelContent.Padding = new Padding(28, 0, 28, 24);
            panelContent.Size = new Size(920, 720);
            panelContent.TabIndex = 0;
            // 
            // flowCompanies
            // 
            flowCompanies.AutoScroll = true;
            flowCompanies.BackColor = Color.Transparent;
            flowCompanies.Dock = DockStyle.Fill;
            flowCompanies.Location = new Point(28, 140);
            flowCompanies.Name = "flowCompanies";
            flowCompanies.Padding = new Padding(0, 10, 0, 0);
            flowCompanies.Size = new Size(864, 556);
            flowCompanies.TabIndex = 3;
            // 
            // lblCompaniesTitle
            // 
            lblCompaniesTitle.Dock = DockStyle.Top;
            lblCompaniesTitle.Font = new Font("Segoe UI", 13F, FontStyle.Bold);
            lblCompaniesTitle.ForeColor = Color.FromArgb(28, 40, 63);
            lblCompaniesTitle.Location = new Point(28, 96);
            lblCompaniesTitle.Name = "lblCompaniesTitle";
            lblCompaniesTitle.Size = new Size(864, 44);
            lblCompaniesTitle.TabIndex = 2;
            lblCompaniesTitle.Text = "Companies";
            lblCompaniesTitle.TextAlign = ContentAlignment.BottomLeft;
            // 
            // flowSummary
            // 
            flowSummary.AutoSize = true;
            flowSummary.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            flowSummary.BackColor = Color.Transparent;
            flowSummary.Dock = DockStyle.Top;
            flowSummary.Location = new Point(28, 92);
            flowSummary.Name = "flowSummary";
            flowSummary.Padding = new Padding(0, 4, 0, 0);
            flowSummary.Size = new Size(864, 4);
            flowSummary.TabIndex = 1;
            // 
            // panelHeader
            // 
            panelHeader.BackColor = Color.Transparent;
            panelHeader.Controls.Add(btnRefreshOverview);
            panelHeader.Controls.Add(lblOverviewSubtitle);
            panelHeader.Controls.Add(lblOverviewTitle);
            panelHeader.Dock = DockStyle.Top;
            panelHeader.Location = new Point(28, 0);
            panelHeader.Name = "panelHeader";
            panelHeader.Size = new Size(864, 92);
            panelHeader.TabIndex = 0;
            // 
            // btnRefreshOverview
            // 
            btnRefreshOverview.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnRefreshOverview.BorderRadius = 6;
            btnRefreshOverview.CustomizableEdges = customizableEdges15;
            btnRefreshOverview.FillColor = Color.FromArgb(28, 40, 63);
            btnRefreshOverview.Font = new Font("Segoe UI", 9.5F);
            btnRefreshOverview.ForeColor = Color.White;
            btnRefreshOverview.Location = new Point(744, 24);
            btnRefreshOverview.Name = "btnRefreshOverview";
            btnRefreshOverview.ShadowDecoration.CustomizableEdges = customizableEdges16;
            btnRefreshOverview.Size = new Size(120, 40);
            btnRefreshOverview.TabIndex = 2;
            btnRefreshOverview.Text = "Refresh";
            btnRefreshOverview.Click += BtnRefreshOverview_Click;
            // 
            // lblOverviewSubtitle
            // 
            lblOverviewSubtitle.AutoSize = true;
            lblOverviewSubtitle.Font = new Font("Segoe UI", 9.5F);
            lblOverviewSubtitle.ForeColor = Color.FromArgb(96, 105, 121);
            lblOverviewSubtitle.Location = new Point(3, 62);
            lblOverviewSubtitle.Name = "lblOverviewSubtitle";
            lblOverviewSubtitle.Size = new Size(306, 21);
            lblOverviewSubtitle.TabIndex = 1;
            lblOverviewSubtitle.Text = "Platform-wide view across every company.";
            // 
            // lblOverviewTitle
            // 
            lblOverviewTitle.AutoSize = true;
            lblOverviewTitle.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
            lblOverviewTitle.ForeColor = Color.FromArgb(28, 40, 63);
            lblOverviewTitle.Location = new Point(0, 18);
            lblOverviewTitle.Name = "lblOverviewTitle";
            lblOverviewTitle.Size = new Size(170, 46);
            lblOverviewTitle.TabIndex = 0;
            lblOverviewTitle.Text = "Overview";
            // 
            // AdminWinforms
            // 
            ClientSize = new Size(1180, 720);
            Controls.Add(panelContent);
            Controls.Add(panelNav);
            Name = "AdminWinforms";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Larp SmartSariSari - Admin Panel";
            WindowState = FormWindowState.Maximized;
            panelNav.ResumeLayout(false);
            panelNav.PerformLayout();
            panelUser.ResumeLayout(false);
            panelLogo.ResumeLayout(false);
            panelContent.ResumeLayout(false);
            panelContent.PerformLayout();
            panelHeader.ResumeLayout(false);
            panelHeader.PerformLayout();
            ResumeLayout(false);
        }
    }
}