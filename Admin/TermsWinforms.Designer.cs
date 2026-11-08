using System.Drawing;
using System.Windows.Forms;

namespace Larp.winforms.Admin
{
    partial class TermsWinforms
    {
        private System.ComponentModel.IContainer components = null;

        private Guna.UI2.WinForms.Guna2Panel headerPanel;
        private Guna.UI2.WinForms.Guna2Panel contentPanel;

        private Label lblTitle;
        private Label lblSubtitle;
        private Label lblVersion;
        private Label lblEffectiveDate;

        private Guna.UI2.WinForms.Guna2TextBox txtVersion;
        private Guna.UI2.WinForms.Guna2DateTimePicker dtpEffectiveDate;

        private Guna.UI2.WinForms.Guna2Panel editorPanel;
        private RichTextBox txtTerms;

        private Guna.UI2.WinForms.Guna2Button btnRefresh;
        private Guna.UI2.WinForms.Guna2Button btnSave;
        private Guna.UI2.WinForms.Guna2Button btnExportPdf;
        private Guna.UI2.WinForms.Guna2Button btnClose;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null)
                components.Dispose();

            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges3 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges4 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges17 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges18 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
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
            headerPanel = new Guna.UI2.WinForms.Guna2Panel();
            lblTitle = new Label();
            lblSubtitle = new Label();
            btnClose = new Guna.UI2.WinForms.Guna2Button();
            contentPanel = new Guna.UI2.WinForms.Guna2Panel();
            editorPanel = new Guna.UI2.WinForms.Guna2Panel();
            txtTerms = new RichTextBox();
            btnExportPdf = new Guna.UI2.WinForms.Guna2Button();
            btnSave = new Guna.UI2.WinForms.Guna2Button();
            btnRefresh = new Guna.UI2.WinForms.Guna2Button();
            dtpEffectiveDate = new Guna.UI2.WinForms.Guna2DateTimePicker();
            txtVersion = new Guna.UI2.WinForms.Guna2TextBox();
            lblEffectiveDate = new Label();
            lblVersion = new Label();
            headerPanel.SuspendLayout();
            contentPanel.SuspendLayout();
            editorPanel.SuspendLayout();
            SuspendLayout();
            // 
            // headerPanel
            // 
            headerPanel.BackColor = Color.FromArgb(28, 40, 63);
            headerPanel.Controls.Add(lblTitle);
            headerPanel.Controls.Add(lblSubtitle);
            headerPanel.Controls.Add(btnClose);
            headerPanel.CustomizableEdges = customizableEdges3;
            headerPanel.Dock = DockStyle.Top;
            headerPanel.Location = new Point(0, 0);
            headerPanel.Name = "headerPanel";
            headerPanel.ShadowDecoration.CustomizableEdges = customizableEdges4;
            headerPanel.Size = new Size(1200, 105);
            headerPanel.TabIndex = 1;
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 19F, FontStyle.Bold);
            lblTitle.ForeColor = Color.White;
            lblTitle.Location = new Point(30, 20);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(345, 45);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "Terms and Conditions";
            // 
            // lblSubtitle
            // 
            lblSubtitle.AutoSize = true;
            lblSubtitle.Font = new Font("Segoe UI", 9.5F);
            lblSubtitle.ForeColor = Color.FromArgb(180, 190, 210);
            lblSubtitle.Location = new Point(32, 62);
            lblSubtitle.Name = "lblSubtitle";
            lblSubtitle.Size = new Size(460, 21);
            lblSubtitle.TabIndex = 1;
            lblSubtitle.Text = "Manage the terms governing the use of Larp Sari-Sari Smart App";
            // 
            // btnClose
            // 
            btnClose.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnClose.BorderRadius = 6;
            btnClose.CustomizableEdges = customizableEdges1;
            btnClose.FillColor = Color.FromArgb(58, 74, 105);
            btnClose.Font = new Font("Segoe UI", 9F);
            btnClose.ForeColor = Color.White;
            btnClose.Location = new Point(1080, 30);
            btnClose.Name = "btnClose";
            btnClose.ShadowDecoration.CustomizableEdges = customizableEdges2;
            btnClose.Size = new Size(90, 38);
            btnClose.TabIndex = 2;
            btnClose.Text = "Close";
            btnClose.Click += BtnClose_Click;
            // 
            // contentPanel
            // 
            contentPanel.BackColor = Color.FromArgb(245, 246, 248);
            contentPanel.Controls.Add(editorPanel);
            contentPanel.Controls.Add(btnExportPdf);
            contentPanel.Controls.Add(btnSave);
            contentPanel.Controls.Add(btnRefresh);
            contentPanel.Controls.Add(dtpEffectiveDate);
            contentPanel.Controls.Add(txtVersion);
            contentPanel.Controls.Add(lblEffectiveDate);
            contentPanel.Controls.Add(lblVersion);
            contentPanel.CustomizableEdges = customizableEdges17;
            contentPanel.Dock = DockStyle.Fill;
            contentPanel.Location = new Point(0, 105);
            contentPanel.Name = "contentPanel";
            contentPanel.Padding = new Padding(30);
            contentPanel.ShadowDecoration.CustomizableEdges = customizableEdges18;
            contentPanel.Size = new Size(1200, 595);
            contentPanel.TabIndex = 0;
            // 
            // editorPanel
            // 
            editorPanel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            editorPanel.BackColor = Color.White;
            editorPanel.BorderRadius = 8;
            editorPanel.Controls.Add(txtTerms);
            editorPanel.CustomizableEdges = customizableEdges5;
            editorPanel.Location = new Point(30, 105);
            editorPanel.Name = "editorPanel";
            editorPanel.ShadowDecoration.CustomizableEdges = customizableEdges6;
            editorPanel.Size = new Size(2140, 970);
            editorPanel.TabIndex = 0;
            // 
            // txtTerms
            // 
            txtTerms.BorderStyle = BorderStyle.None;
            txtTerms.Dock = DockStyle.Fill;
            txtTerms.Font = new Font("Segoe UI", 10F);
            txtTerms.ForeColor = Color.FromArgb(45, 50, 60);
            txtTerms.Location = new Point(0, 0);
            txtTerms.Margin = new Padding(20);
            txtTerms.Name = "txtTerms";
            txtTerms.ScrollBars = RichTextBoxScrollBars.Vertical;
            txtTerms.Size = new Size(2140, 970);
            txtTerms.TabIndex = 0;
            txtTerms.Text = "";
            // 
            // btnExportPdf
            // 
            btnExportPdf.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnExportPdf.BorderRadius = 6;
            btnExportPdf.CustomizableEdges = customizableEdges7;
            btnExportPdf.FillColor = Color.FromArgb(28, 40, 63);
            btnExportPdf.Font = new Font("Segoe UI Semibold", 9F);
            btnExportPdf.ForeColor = Color.White;
            btnExportPdf.Location = new Point(1950, 1090);
            btnExportPdf.Name = "btnExportPdf";
            btnExportPdf.ShadowDecoration.CustomizableEdges = customizableEdges8;
            btnExportPdf.Size = new Size(120, 40);
            btnExportPdf.TabIndex = 1;
            btnExportPdf.Text = "Export PDF";
            btnExportPdf.Click += BtnExportPdf_Click;
            // 
            // btnSave
            // 
            btnSave.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnSave.BorderRadius = 6;
            btnSave.CustomizableEdges = customizableEdges9;
            btnSave.FillColor = Color.FromArgb(46, 107, 69);
            btnSave.Font = new Font("Segoe UI Semibold", 9F);
            btnSave.ForeColor = Color.White;
            btnSave.Location = new Point(1820, 1090);
            btnSave.Name = "btnSave";
            btnSave.ShadowDecoration.CustomizableEdges = customizableEdges10;
            btnSave.Size = new Size(120, 40);
            btnSave.TabIndex = 2;
            btnSave.Text = "Save Changes";
            btnSave.Click += BtnSave_Click;
            // 
            // btnRefresh
            // 
            btnRefresh.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnRefresh.BorderRadius = 6;
            btnRefresh.CustomizableEdges = customizableEdges11;
            btnRefresh.FillColor = Color.FromArgb(28, 40, 63);
            btnRefresh.Font = new Font("Segoe UI Semibold", 9F);
            btnRefresh.ForeColor = Color.White;
            btnRefresh.Location = new Point(30, 1090);
            btnRefresh.Name = "btnRefresh";
            btnRefresh.ShadowDecoration.CustomizableEdges = customizableEdges12;
            btnRefresh.Size = new Size(110, 40);
            btnRefresh.TabIndex = 3;
            btnRefresh.Text = "Refresh";
            btnRefresh.Click += BtnRefresh_Click;
            // 
            // dtpEffectiveDate
            // 
            dtpEffectiveDate.BorderRadius = 6;
            dtpEffectiveDate.Checked = true;
            dtpEffectiveDate.CustomizableEdges = customizableEdges13;
            dtpEffectiveDate.Font = new Font("Segoe UI", 10F);
            dtpEffectiveDate.Format = DateTimePickerFormat.Long;
            dtpEffectiveDate.Location = new Point(230, 45);
            dtpEffectiveDate.MaxDate = new DateTime(9998, 12, 31, 0, 0, 0, 0);
            dtpEffectiveDate.MinDate = new DateTime(1753, 1, 1, 0, 0, 0, 0);
            dtpEffectiveDate.Name = "dtpEffectiveDate";
            dtpEffectiveDate.ShadowDecoration.CustomizableEdges = customizableEdges14;
            dtpEffectiveDate.Size = new Size(220, 40);
            dtpEffectiveDate.TabIndex = 4;
            dtpEffectiveDate.Value = new DateTime(2026, 10, 4, 14, 56, 49, 422);
            // 
            // txtVersion
            // 
            txtVersion.BorderRadius = 6;
            txtVersion.CustomizableEdges = customizableEdges15;
            txtVersion.DefaultText = "";
            txtVersion.Font = new Font("Segoe UI", 10F);
            txtVersion.Location = new Point(30, 45);
            txtVersion.Margin = new Padding(3, 4, 3, 4);
            txtVersion.Name = "txtVersion";
            txtVersion.PlaceholderText = "1.0";
            txtVersion.SelectedText = "";
            txtVersion.ShadowDecoration.CustomizableEdges = customizableEdges16;
            txtVersion.Size = new Size(180, 40);
            txtVersion.TabIndex = 5;
            // 
            // lblEffectiveDate
            // 
            lblEffectiveDate.AutoSize = true;
            lblEffectiveDate.Font = new Font("Segoe UI Semibold", 9.5F);
            lblEffectiveDate.ForeColor = Color.FromArgb(28, 40, 63);
            lblEffectiveDate.Location = new Point(230, 20);
            lblEffectiveDate.Name = "lblEffectiveDate";
            lblEffectiveDate.Size = new Size(112, 21);
            lblEffectiveDate.TabIndex = 6;
            lblEffectiveDate.Text = "Effective Date";
            // 
            // lblVersion
            // 
            lblVersion.AutoSize = true;
            lblVersion.Font = new Font("Segoe UI Semibold", 9.5F);
            lblVersion.ForeColor = Color.FromArgb(28, 40, 63);
            lblVersion.Location = new Point(30, 20);
            lblVersion.Name = "lblVersion";
            lblVersion.Size = new Size(64, 21);
            lblVersion.TabIndex = 7;
            lblVersion.Text = "Version";
            // 
            // TermsWinforms
            // 
            ClientSize = new Size(1200, 700);
            Controls.Add(contentPanel);
            Controls.Add(headerPanel);
            Name = "TermsWinforms";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Larp SmartSariSari - Terms & Conditions";
            WindowState = FormWindowState.Maximized;
            headerPanel.ResumeLayout(false);
            headerPanel.PerformLayout();
            contentPanel.ResumeLayout(false);
            contentPanel.PerformLayout();
            editorPanel.ResumeLayout(false);
            ResumeLayout(false);
        }
    }
}