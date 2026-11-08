namespace Larp.winforms.MicroTenant.Reports
{
    partial class ReportsGenerator
    {
        private System.ComponentModel.IContainer components = null;

        private Guna.UI2.WinForms.Guna2Panel panelHeader;
        private Guna.UI2.WinForms.Guna2Panel panelFilters;
        private Guna.UI2.WinForms.Guna2Panel panelTable;

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblSubtitle;
        private Guna.UI2.WinForms.Guna2Button btnClose;

        private System.Windows.Forms.Label lblReportType;
        private Guna.UI2.WinForms.Guna2ComboBox cmbReportType;
        private System.Windows.Forms.Label lblFrom;
        private Guna.UI2.WinForms.Guna2DateTimePicker dtpStartDate;
        private System.Windows.Forms.Label lblTo;
        private Guna.UI2.WinForms.Guna2DateTimePicker dtpEndDate;
        private Guna.UI2.WinForms.Guna2Button btnGenerate;
        private Guna.UI2.WinForms.Guna2ProgressIndicator progress;

        private System.Windows.Forms.Label lblExportCaption;
        private Guna.UI2.WinForms.Guna2Button btnCsv;
        private Guna.UI2.WinForms.Guna2Button btnPdf;

        private System.Windows.Forms.Label lblSummary;
        private Guna.UI2.WinForms.Guna2DataGridView dgvReport;
        private System.Windows.Forms.ToolTip toolTip1;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }

            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges3 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges4 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges18 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges19 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
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
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges20 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges21 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            panelHeader = new Guna.UI2.WinForms.Guna2Panel();
            lblTitle = new Label();
            lblSubtitle = new Label();
            btnClose = new Guna.UI2.WinForms.Guna2Button();
            panelFilters = new Guna.UI2.WinForms.Guna2Panel();
            lblReportType = new Label();
            cmbReportType = new Guna.UI2.WinForms.Guna2ComboBox();
            lblFrom = new Label();
            dtpStartDate = new Guna.UI2.WinForms.Guna2DateTimePicker();
            lblTo = new Label();
            dtpEndDate = new Guna.UI2.WinForms.Guna2DateTimePicker();
            btnGenerate = new Guna.UI2.WinForms.Guna2Button();
            progress = new Guna.UI2.WinForms.Guna2ProgressIndicator();
            btnCsv = new Guna.UI2.WinForms.Guna2Button();
            btnPdf = new Guna.UI2.WinForms.Guna2Button();
            lblSummary = new Label();
            panelTable = new Guna.UI2.WinForms.Guna2Panel();
            dgvReport = new Guna.UI2.WinForms.Guna2DataGridView();
            toolTip1 = new ToolTip(components);
            panelHeader.SuspendLayout();
            panelFilters.SuspendLayout();
            panelTable.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvReport).BeginInit();
            SuspendLayout();
            // 
            // panelHeader
            // 
            panelHeader.BackColor = Color.White;
            panelHeader.Controls.Add(lblTitle);
            panelHeader.Controls.Add(lblSubtitle);
            panelHeader.Controls.Add(btnClose);
            panelHeader.CustomizableEdges = customizableEdges3;
            panelHeader.Dock = DockStyle.Top;
            panelHeader.Location = new Point(0, 0);
            panelHeader.Name = "panelHeader";
            panelHeader.ShadowDecoration.CustomizableEdges = customizableEdges4;
            panelHeader.Size = new Size(1250, 100);
            panelHeader.TabIndex = 0;
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 22F, FontStyle.Bold);
            lblTitle.ForeColor = Color.FromArgb(28, 40, 63);
            lblTitle.Location = new Point(30, 14);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(342, 50);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "Reports Generator";
            // 
            // lblSubtitle
            // 
            lblSubtitle.AutoSize = true;
            lblSubtitle.Font = new Font("Segoe UI", 10F);
            lblSubtitle.ForeColor = Color.FromArgb(96, 105, 121);
            lblSubtitle.Location = new Point(33, 62);
            lblSubtitle.Name = "lblSubtitle";
            lblSubtitle.Size = new Size(357, 23);
            lblSubtitle.TabIndex = 1;
            lblSubtitle.Text = "Generate, review and export business reports.";
            // 
            // btnClose
            // 
            btnClose.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnClose.BorderRadius = 8;
            btnClose.CustomizableEdges = customizableEdges1;
            btnClose.FillColor = Color.FromArgb(107, 114, 128);
            btnClose.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnClose.ForeColor = Color.White;
            btnClose.Location = new Point(1120, 30);
            btnClose.Name = "btnClose";
            btnClose.ShadowDecoration.CustomizableEdges = customizableEdges2;
            btnClose.Size = new Size(100, 40);
            btnClose.TabIndex = 2;
            btnClose.Tag = "secondary";
            btnClose.Text = "Close";
            btnClose.Click += BtnClose_Click;
            // 
            // panelFilters
            // 
            panelFilters.BackColor = Color.White;
            panelFilters.Controls.Add(lblReportType);
            panelFilters.Controls.Add(cmbReportType);
            panelFilters.Controls.Add(lblFrom);
            panelFilters.Controls.Add(dtpStartDate);
            panelFilters.Controls.Add(lblTo);
            panelFilters.Controls.Add(dtpEndDate);
            panelFilters.Controls.Add(btnGenerate);
            panelFilters.Controls.Add(progress);
            panelFilters.Controls.Add(btnCsv);
            panelFilters.Controls.Add(btnPdf);
            panelFilters.Controls.Add(lblSummary);
            panelFilters.CustomizableEdges = customizableEdges18;
            panelFilters.Dock = DockStyle.Top;
            panelFilters.Location = new Point(0, 100);
            panelFilters.Name = "panelFilters";
            panelFilters.ShadowDecoration.CustomizableEdges = customizableEdges19;
            panelFilters.Size = new Size(1250, 135);
            panelFilters.TabIndex = 1;
            // 
            // lblReportType
            // 
            lblReportType.AutoSize = true;
            lblReportType.Font = new Font("Segoe UI", 9.5F);
            lblReportType.ForeColor = Color.FromArgb(96, 105, 121);
            lblReportType.Location = new Point(32, 18);
            lblReportType.Name = "lblReportType";
            lblReportType.Size = new Size(91, 21);
            lblReportType.TabIndex = 0;
            lblReportType.Text = "Report type";
            // 
            // cmbReportType
            // 
            cmbReportType.BackColor = Color.Transparent;
            cmbReportType.BorderRadius = 8;
            cmbReportType.CustomizableEdges = customizableEdges5;
            cmbReportType.DrawMode = DrawMode.OwnerDrawFixed;
            cmbReportType.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbReportType.FocusedColor = Color.Empty;
            cmbReportType.Font = new Font("Segoe UI", 10F);
            cmbReportType.ForeColor = Color.FromArgb(68, 88, 112);
            cmbReportType.ItemHeight = 30;
            cmbReportType.Items.AddRange(new object[] { "Sales Report", "Sales History", "Inventory Report", "Products Report", "Income Statement" });
            cmbReportType.Location = new Point(30, 42);
            cmbReportType.Name = "cmbReportType";
            cmbReportType.ShadowDecoration.CustomizableEdges = customizableEdges6;
            cmbReportType.Size = new Size(240, 36);
            cmbReportType.TabIndex = 1;
            cmbReportType.SelectedIndexChanged += cmbReportType_SelectedIndexChanged;
            // 
            // lblFrom
            // 
            lblFrom.AutoSize = true;
            lblFrom.Font = new Font("Segoe UI", 9.5F);
            lblFrom.ForeColor = Color.FromArgb(96, 105, 121);
            lblFrom.Location = new Point(292, 18);
            lblFrom.Name = "lblFrom";
            lblFrom.Size = new Size(47, 21);
            lblFrom.TabIndex = 2;
            lblFrom.Text = "From";
            // 
            // dtpStartDate
            // 
            dtpStartDate.BorderRadius = 8;
            dtpStartDate.Checked = true;
            dtpStartDate.CustomizableEdges = customizableEdges7;
            dtpStartDate.Font = new Font("Segoe UI", 10F);
            dtpStartDate.Format = DateTimePickerFormat.Short;
            dtpStartDate.Location = new Point(290, 42);
            dtpStartDate.MaxDate = new DateTime(9998, 12, 31, 0, 0, 0, 0);
            dtpStartDate.MinDate = new DateTime(1753, 1, 1, 0, 0, 0, 0);
            dtpStartDate.Name = "dtpStartDate";
            dtpStartDate.ShadowDecoration.CustomizableEdges = customizableEdges8;
            dtpStartDate.Size = new Size(150, 36);
            dtpStartDate.TabIndex = 3;
            dtpStartDate.Value = new DateTime(2026, 10, 4, 2, 32, 26, 2);
            // 
            // lblTo
            // 
            lblTo.AutoSize = true;
            lblTo.Font = new Font("Segoe UI", 9.5F);
            lblTo.ForeColor = Color.FromArgb(96, 105, 121);
            lblTo.Location = new Point(457, 18);
            lblTo.Name = "lblTo";
            lblTo.Size = new Size(25, 21);
            lblTo.TabIndex = 4;
            lblTo.Text = "To";
            // 
            // dtpEndDate
            // 
            dtpEndDate.BorderRadius = 8;
            dtpEndDate.Checked = true;
            dtpEndDate.CustomizableEdges = customizableEdges9;
            dtpEndDate.Font = new Font("Segoe UI", 10F);
            dtpEndDate.Format = DateTimePickerFormat.Short;
            dtpEndDate.Location = new Point(455, 42);
            dtpEndDate.MaxDate = new DateTime(9998, 12, 31, 0, 0, 0, 0);
            dtpEndDate.MinDate = new DateTime(1753, 1, 1, 0, 0, 0, 0);
            dtpEndDate.Name = "dtpEndDate";
            dtpEndDate.ShadowDecoration.CustomizableEdges = customizableEdges10;
            dtpEndDate.Size = new Size(150, 36);
            dtpEndDate.TabIndex = 5;
            dtpEndDate.Value = new DateTime(2026, 10, 4, 2, 32, 26, 39);
            // 
            // btnGenerate
            // 
            btnGenerate.BorderRadius = 8;
            btnGenerate.CustomizableEdges = customizableEdges11;
            btnGenerate.FillColor = Color.FromArgb(28, 40, 63);
            btnGenerate.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnGenerate.ForeColor = Color.White;
            btnGenerate.Location = new Point(625, 42);
            btnGenerate.Name = "btnGenerate";
            btnGenerate.ShadowDecoration.CustomizableEdges = customizableEdges12;
            btnGenerate.Size = new Size(120, 36);
            btnGenerate.TabIndex = 6;
            btnGenerate.Tag = "primary";
            btnGenerate.Text = "Generate";
            btnGenerate.Click += BtnGenerate_Click;
            // 
            // progress
            // 
            progress.AutoStart = true;
            progress.Location = new Point(755, 42);
            progress.Name = "progress";
            progress.ShadowDecoration.CustomizableEdges = customizableEdges13;
            progress.Size = new Size(36, 36);
            progress.TabIndex = 7;
            progress.Visible = false;
            // 
            // btnCsv
            // 
            btnCsv.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnCsv.BorderRadius = 8;
            btnCsv.CustomizableEdges = customizableEdges14;
            btnCsv.Enabled = false;
            btnCsv.FillColor = Color.FromArgb(46, 125, 90);
            btnCsv.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnCsv.ForeColor = Color.White;
            btnCsv.Location = new Point(810, 42);
            btnCsv.Name = "btnCsv";
            btnCsv.ShadowDecoration.CustomizableEdges = customizableEdges15;
            btnCsv.Size = new Size(190, 36);
            btnCsv.TabIndex = 9;
            btnCsv.Tag = "success";
            btnCsv.Text = "Export to Excel (.csv)";
            toolTip1.SetToolTip(btnCsv, "Saves the table as a .csv file that you can open and edit in Excel.");
            btnCsv.Click += BtnCsv_Click;
            // 
            // btnPdf
            // 
            btnPdf.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnPdf.BorderRadius = 8;
            btnPdf.CustomizableEdges = customizableEdges16;
            btnPdf.Enabled = false;
            btnPdf.FillColor = Color.FromArgb(210, 212, 217);
            btnPdf.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnPdf.ForeColor = Color.FromArgb(28, 40, 63);
            btnPdf.Location = new Point(1010, 42);
            btnPdf.Name = "btnPdf";
            btnPdf.ShadowDecoration.CustomizableEdges = customizableEdges17;
            btnPdf.Size = new Size(210, 36);
            btnPdf.TabIndex = 10;
            btnPdf.Tag = "secondary";
            btnPdf.Text = "Export to PDF (read-only)";
            toolTip1.SetToolTip(btnPdf, "Saves a print-ready PDF that cannot be edited.");
            btnPdf.Click += BtnPdf_Click;
            // 
            // lblSummary
            // 
            lblSummary.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            lblSummary.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblSummary.ForeColor = Color.FromArgb(28, 40, 63);
            lblSummary.Location = new Point(30, 95);
            lblSummary.Name = "lblSummary";
            lblSummary.Size = new Size(1190, 24);
            lblSummary.TabIndex = 11;
            lblSummary.Text = "Choose a report and click Generate.";
            // 
            // panelTable
            // 
            panelTable.BackColor = Color.White;
            panelTable.Controls.Add(dgvReport);
            panelTable.CustomizableEdges = customizableEdges20;
            panelTable.Dock = DockStyle.Fill;
            panelTable.Location = new Point(0, 235);
            panelTable.Name = "panelTable";
            panelTable.Padding = new Padding(20);
            panelTable.ShadowDecoration.CustomizableEdges = customizableEdges21;
            panelTable.Size = new Size(1250, 515);
            panelTable.TabIndex = 2;
            // 
            // dgvReport
            // 
            dgvReport.AllowUserToAddRows = false;
            dgvReport.AllowUserToDeleteRows = false;
            dgvReport.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = Color.White;
            dgvReport.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = Color.FromArgb(28, 40, 63);
            dataGridViewCellStyle2.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            dataGridViewCellStyle2.ForeColor = Color.White;
            dataGridViewCellStyle2.SelectionBackColor = Color.FromArgb(28, 40, 63);
            dataGridViewCellStyle2.SelectionForeColor = Color.White;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.True;
            dgvReport.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            dgvReport.ColumnHeadersHeight = 40;
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = Color.White;
            dataGridViewCellStyle3.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle3.ForeColor = Color.FromArgb(28, 40, 63);
            dataGridViewCellStyle3.SelectionBackColor = Color.FromArgb(232, 233, 236);
            dataGridViewCellStyle3.SelectionForeColor = Color.FromArgb(28, 40, 63);
            dataGridViewCellStyle3.WrapMode = DataGridViewTriState.False;
            dgvReport.DefaultCellStyle = dataGridViewCellStyle3;
            dgvReport.Dock = DockStyle.Fill;
            dgvReport.GridColor = Color.FromArgb(232, 233, 236);
            dgvReport.Location = new Point(20, 20);
            dgvReport.MultiSelect = false;
            dgvReport.Name = "dgvReport";
            dgvReport.ReadOnly = true;
            dgvReport.RowHeadersVisible = false;
            dgvReport.RowHeadersWidth = 51;
            dgvReport.RowTemplate.Height = 36;
            dgvReport.Size = new Size(1210, 475);
            dgvReport.TabIndex = 0;
            dgvReport.ThemeStyle.AlternatingRowsStyle.BackColor = Color.White;
            dgvReport.ThemeStyle.GridColor = Color.FromArgb(232, 233, 236);
            dgvReport.ThemeStyle.HeaderStyle.BackColor = Color.FromArgb(28, 40, 63);
            dgvReport.ThemeStyle.HeaderStyle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            dgvReport.ThemeStyle.HeaderStyle.HeaightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgvReport.ThemeStyle.HeaderStyle.Height = 40;
            dgvReport.ThemeStyle.ReadOnly = true;
            dgvReport.ThemeStyle.RowsStyle.Font = new Font("Segoe UI", 9F);
            dgvReport.ThemeStyle.RowsStyle.ForeColor = Color.FromArgb(28, 40, 63);
            dgvReport.ThemeStyle.RowsStyle.Height = 36;
            dgvReport.ThemeStyle.RowsStyle.SelectionBackColor = Color.FromArgb(232, 233, 236);
            dgvReport.ThemeStyle.RowsStyle.SelectionForeColor = Color.FromArgb(28, 40, 63);
            // 
            // ReportsGenerator
            // 
            BackColor = Color.FromArgb(245, 247, 250);
            ClientSize = new Size(1250, 750);
            Controls.Add(panelTable);
            Controls.Add(panelFilters);
            Controls.Add(panelHeader);
            FormBorderStyle = FormBorderStyle.None;
            MinimumSize = new Size(1100, 650);
            Name = "ReportsGenerator";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Reports Generator";
            WindowState = FormWindowState.Maximized;
            panelHeader.ResumeLayout(false);
            panelHeader.PerformLayout();
            panelFilters.ResumeLayout(false);
            panelFilters.PerformLayout();
            panelTable.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvReport).EndInit();
            ResumeLayout(false);
        }
    }
}