namespace Larp.winforms.SmallTenant.Supplier
{
    partial class SupplierWinforms
    {
        private System.ComponentModel.IContainer components = null;

        private Guna.UI2.WinForms.Guna2Panel panelHeader;
        private Guna.UI2.WinForms.Guna2Panel panelStats;
        private Guna.UI2.WinForms.Guna2Panel panelTable;

        private Guna.UI2.WinForms.Guna2TextBox guna2TextBox1;
        private Guna.UI2.WinForms.Guna2Button btnAddSupplier;
        private Guna.UI2.WinForms.Guna2Button btnRefresh;

        private Guna.UI2.WinForms.Guna2DataGridView guna2DataGridView1;
        private Guna.UI2.WinForms.Guna2ProgressIndicator guna2ProgressIndicator1;

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblSubtitle;

        private System.Windows.Forms.Label lblTotalTitle;
        private System.Windows.Forms.Label lblTotalSuppliers;

        private System.Windows.Forms.Label lblActiveTitle;
        private System.Windows.Forms.Label lblActiveSuppliers;

        private System.Windows.Forms.Label lblInactiveTitle;
        private System.Windows.Forms.Label lblInactiveSuppliers;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null)
            {
                components.Dispose();
            }

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
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges11 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges12 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges14 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges15 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges13 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            panelHeader = new Guna.UI2.WinForms.Guna2Panel();
            btnClose = new Guna.UI2.WinForms.Guna2Button();
            lblTitle = new Label();
            lblSubtitle = new Label();
            guna2TextBox1 = new Guna.UI2.WinForms.Guna2TextBox();
            btnRefresh = new Guna.UI2.WinForms.Guna2Button();
            btnAddSupplier = new Guna.UI2.WinForms.Guna2Button();
            panelStats = new Guna.UI2.WinForms.Guna2Panel();
            lblTotalTitle = new Label();
            lblTotalSuppliers = new Label();
            lblActiveTitle = new Label();
            lblActiveSuppliers = new Label();
            lblInactiveTitle = new Label();
            lblInactiveSuppliers = new Label();
            panelTable = new Guna.UI2.WinForms.Guna2Panel();
            guna2DataGridView1 = new Guna.UI2.WinForms.Guna2DataGridView();
            guna2ProgressIndicator1 = new Guna.UI2.WinForms.Guna2ProgressIndicator();
            panelHeader.SuspendLayout();
            panelStats.SuspendLayout();
            panelTable.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)guna2DataGridView1).BeginInit();
            SuspendLayout();
            // 
            // panelHeader
            // 
            panelHeader.BackColor = Color.White;
            panelHeader.BorderRadius = 10;
            panelHeader.Controls.Add(btnClose);
            panelHeader.Controls.Add(lblTitle);
            panelHeader.Controls.Add(lblSubtitle);
            panelHeader.Controls.Add(guna2TextBox1);
            panelHeader.Controls.Add(btnRefresh);
            panelHeader.Controls.Add(btnAddSupplier);
            panelHeader.CustomizableEdges = customizableEdges9;
            panelHeader.Dock = DockStyle.Top;
            panelHeader.Location = new Point(0, 0);
            panelHeader.Margin = new Padding(3, 4, 3, 4);
            panelHeader.Name = "panelHeader";
            panelHeader.ShadowDecoration.CustomizableEdges = customizableEdges10;
            panelHeader.Size = new Size(1429, 153);
            panelHeader.TabIndex = 0;
            // 
            // btnClose
            // 
            btnClose.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnClose.BorderRadius = 8;
            btnClose.CustomizableEdges = customizableEdges1;
            btnClose.DisabledState.BorderColor = Color.DarkGray;
            btnClose.DisabledState.CustomBorderColor = Color.DarkGray;
            btnClose.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            btnClose.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            btnClose.FillColor = Color.FromArgb(210, 75, 70);
            btnClose.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnClose.ForeColor = Color.White;
            btnClose.Location = new Point(1281, 43);
            btnClose.Margin = new Padding(3, 4, 3, 4);
            btnClose.Name = "btnClose";
            btnClose.ShadowDecoration.CustomizableEdges = customizableEdges2;
            btnClose.Size = new Size(125, 56);
            btnClose.TabIndex = 6;
            btnClose.Text = "Close";
            btnClose.Click += btnClose_Click;
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 22F, FontStyle.Bold);
            lblTitle.ForeColor = Color.FromArgb(31, 41, 55);
            lblTitle.Location = new Point(34, 27);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(184, 50);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "Suppliers";
            // 
            // lblSubtitle
            // 
            lblSubtitle.AutoSize = true;
            lblSubtitle.Font = new Font("Segoe UI", 10F);
            lblSubtitle.ForeColor = Color.FromArgb(107, 114, 128);
            lblSubtitle.Location = new Point(38, 87);
            lblSubtitle.Name = "lblSubtitle";
            lblSubtitle.Size = new Size(377, 23);
            lblSubtitle.TabIndex = 1;
            lblSubtitle.Text = "Manage your suppliers and contact information.";
            // 
            // guna2TextBox1
            // 
            guna2TextBox1.BorderRadius = 8;
            guna2TextBox1.Cursor = Cursors.IBeam;
            guna2TextBox1.CustomizableEdges = customizableEdges3;
            guna2TextBox1.DefaultText = "";
            guna2TextBox1.Font = new Font("Segoe UI", 10F);
            guna2TextBox1.Location = new Point(594, 43);
            guna2TextBox1.Margin = new Padding(5);
            guna2TextBox1.Name = "guna2TextBox1";
            guna2TextBox1.PlaceholderText = "Search suppliers...";
            guna2TextBox1.SelectedText = "";
            guna2TextBox1.ShadowDecoration.CustomizableEdges = customizableEdges4;
            guna2TextBox1.Size = new Size(343, 56);
            guna2TextBox1.TabIndex = 1;
            guna2TextBox1.TextChanged += guna2TextBox1_TextChanged;
            // 
            // btnRefresh
            // 
            btnRefresh.BorderRadius = 8;
            btnRefresh.CustomizableEdges = customizableEdges5;
            btnRefresh.FillColor = Color.FromArgb(107, 114, 128);
            btnRefresh.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnRefresh.ForeColor = Color.White;
            btnRefresh.Location = new Point(949, 43);
            btnRefresh.Margin = new Padding(3, 4, 3, 4);
            btnRefresh.Name = "btnRefresh";
            btnRefresh.ShadowDecoration.CustomizableEdges = customizableEdges6;
            btnRefresh.Size = new Size(126, 56);
            btnRefresh.TabIndex = 2;
            btnRefresh.Text = "Refresh";
            btnRefresh.Click += btnRefresh_Click;
            // 
            // btnAddSupplier
            // 
            btnAddSupplier.BorderRadius = 8;
            btnAddSupplier.CustomizableEdges = customizableEdges7;
            btnAddSupplier.FillColor = Color.FromArgb(46, 107, 69);
            btnAddSupplier.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnAddSupplier.ForeColor = Color.White;
            btnAddSupplier.Location = new Point(1086, 43);
            btnAddSupplier.Margin = new Padding(3, 4, 3, 4);
            btnAddSupplier.Name = "btnAddSupplier";
            btnAddSupplier.ShadowDecoration.CustomizableEdges = customizableEdges8;
            btnAddSupplier.Size = new Size(171, 56);
            btnAddSupplier.TabIndex = 3;
            btnAddSupplier.Text = "Add Supplier";
            btnAddSupplier.Click += btnAddSupplier_Click;
            // 
            // panelStats
            // 
            panelStats.BackColor = Color.Transparent;
            panelStats.Controls.Add(lblTotalTitle);
            panelStats.Controls.Add(lblTotalSuppliers);
            panelStats.Controls.Add(lblActiveTitle);
            panelStats.Controls.Add(lblActiveSuppliers);
            panelStats.Controls.Add(lblInactiveTitle);
            panelStats.Controls.Add(lblInactiveSuppliers);
            panelStats.CustomizableEdges = customizableEdges11;
            panelStats.Dock = DockStyle.Top;
            panelStats.Location = new Point(0, 153);
            panelStats.Margin = new Padding(3, 4, 3, 4);
            panelStats.Name = "panelStats";
            panelStats.ShadowDecoration.CustomizableEdges = customizableEdges12;
            panelStats.Size = new Size(1429, 153);
            panelStats.TabIndex = 1;
            // 
            // lblTotalTitle
            // 
            lblTotalTitle.AutoSize = true;
            lblTotalTitle.Font = new Font("Segoe UI", 10F);
            lblTotalTitle.ForeColor = Color.FromArgb(107, 114, 128);
            lblTotalTitle.Location = new Point(40, 27);
            lblTotalTitle.Name = "lblTotalTitle";
            lblTotalTitle.Size = new Size(120, 23);
            lblTotalTitle.TabIndex = 0;
            lblTotalTitle.Text = "Total Suppliers";
            // 
            // lblTotalSuppliers
            // 
            lblTotalSuppliers.AutoSize = true;
            lblTotalSuppliers.Font = new Font("Segoe UI", 24F, FontStyle.Bold);
            lblTotalSuppliers.ForeColor = Color.FromArgb(31, 41, 55);
            lblTotalSuppliers.Location = new Point(37, 57);
            lblTotalSuppliers.Name = "lblTotalSuppliers";
            lblTotalSuppliers.Size = new Size(46, 54);
            lblTotalSuppliers.TabIndex = 1;
            lblTotalSuppliers.Text = "0";
            // 
            // lblActiveTitle
            // 
            lblActiveTitle.AutoSize = true;
            lblActiveTitle.Font = new Font("Segoe UI", 10F);
            lblActiveTitle.ForeColor = Color.FromArgb(107, 114, 128);
            lblActiveTitle.Location = new Point(377, 27);
            lblActiveTitle.Name = "lblActiveTitle";
            lblActiveTitle.Size = new Size(130, 23);
            lblActiveTitle.TabIndex = 2;
            lblActiveTitle.Text = "Active Suppliers";
            // 
            // lblActiveSuppliers
            // 
            lblActiveSuppliers.AutoSize = true;
            lblActiveSuppliers.Font = new Font("Segoe UI", 24F, FontStyle.Bold);
            lblActiveSuppliers.ForeColor = Color.FromArgb(46, 107, 69);
            lblActiveSuppliers.Location = new Point(374, 57);
            lblActiveSuppliers.Name = "lblActiveSuppliers";
            lblActiveSuppliers.Size = new Size(46, 54);
            lblActiveSuppliers.TabIndex = 3;
            lblActiveSuppliers.Text = "0";
            // 
            // lblInactiveTitle
            // 
            lblInactiveTitle.AutoSize = true;
            lblInactiveTitle.Font = new Font("Segoe UI", 10F);
            lblInactiveTitle.ForeColor = Color.FromArgb(107, 114, 128);
            lblInactiveTitle.Location = new Point(714, 27);
            lblInactiveTitle.Name = "lblInactiveTitle";
            lblInactiveTitle.Size = new Size(143, 23);
            lblInactiveTitle.TabIndex = 4;
            lblInactiveTitle.Text = "Inactive Suppliers";
            // 
            // lblInactiveSuppliers
            // 
            lblInactiveSuppliers.AutoSize = true;
            lblInactiveSuppliers.Font = new Font("Segoe UI", 24F, FontStyle.Bold);
            lblInactiveSuppliers.ForeColor = Color.FromArgb(220, 38, 38);
            lblInactiveSuppliers.Location = new Point(711, 57);
            lblInactiveSuppliers.Name = "lblInactiveSuppliers";
            lblInactiveSuppliers.Size = new Size(46, 54);
            lblInactiveSuppliers.TabIndex = 5;
            lblInactiveSuppliers.Text = "0";
            // 
            // panelTable
            // 
            panelTable.BackColor = Color.White;
            panelTable.BorderRadius = 10;
            panelTable.Controls.Add(guna2DataGridView1);
            panelTable.Controls.Add(guna2ProgressIndicator1);
            panelTable.CustomizableEdges = customizableEdges14;
            panelTable.Dock = DockStyle.Fill;
            panelTable.Location = new Point(0, 306);
            panelTable.Margin = new Padding(3, 4, 3, 4);
            panelTable.Name = "panelTable";
            panelTable.Padding = new Padding(23, 27, 23, 27);
            panelTable.ShadowDecoration.CustomizableEdges = customizableEdges15;
            panelTable.Size = new Size(1429, 694);
            panelTable.TabIndex = 2;
            // 
            // guna2DataGridView1
            // 
            guna2DataGridView1.AllowUserToAddRows = false;
            guna2DataGridView1.AllowUserToDeleteRows = false;
            guna2DataGridView1.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = Color.White;
            guna2DataGridView1.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            guna2DataGridView1.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            guna2DataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            guna2DataGridView1.ColumnHeadersHeight = 40;
            guna2DataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = SystemColors.Window;
            dataGridViewCellStyle3.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle3.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = DataGridViewTriState.False;
            guna2DataGridView1.DefaultCellStyle = dataGridViewCellStyle3;
            guna2DataGridView1.Dock = DockStyle.Fill;
            guna2DataGridView1.GridColor = Color.FromArgb(229, 231, 235);
            guna2DataGridView1.Location = new Point(23, 27);
            guna2DataGridView1.Margin = new Padding(3, 4, 3, 4);
            guna2DataGridView1.Name = "guna2DataGridView1";
            guna2DataGridView1.RowHeadersVisible = false;
            guna2DataGridView1.RowHeadersWidth = 51;
            guna2DataGridView1.RowTemplate.Height = 42;
            guna2DataGridView1.Size = new Size(1383, 640);
            guna2DataGridView1.TabIndex = 0;
            guna2DataGridView1.ThemeStyle.AlternatingRowsStyle.BackColor = Color.White;
            guna2DataGridView1.ThemeStyle.GridColor = Color.FromArgb(229, 231, 235);
            guna2DataGridView1.ThemeStyle.HeaderStyle.BackColor = Color.Empty;
            guna2DataGridView1.ThemeStyle.HeaderStyle.BorderStyle = DataGridViewHeaderBorderStyle.Single;
            guna2DataGridView1.ThemeStyle.HeaderStyle.Font = null;
            guna2DataGridView1.ThemeStyle.HeaderStyle.ForeColor = Color.Empty;
            guna2DataGridView1.ThemeStyle.HeaderStyle.Height = 40;
            guna2DataGridView1.ThemeStyle.RowsStyle.BackColor = SystemColors.Window;
            guna2DataGridView1.ThemeStyle.RowsStyle.Font = new Font("Segoe UI", 9F);
            guna2DataGridView1.ThemeStyle.RowsStyle.ForeColor = SystemColors.ControlText;
            guna2DataGridView1.ThemeStyle.RowsStyle.Height = 42;
            guna2DataGridView1.ThemeStyle.RowsStyle.SelectionBackColor = SystemColors.Highlight;
            guna2DataGridView1.ThemeStyle.RowsStyle.SelectionForeColor = SystemColors.HighlightText;
            guna2DataGridView1.CellContentClick += guna2DataGridView1_CellContentClick;
            // 
            // guna2ProgressIndicator1
            // 
            guna2ProgressIndicator1.Location = new Point(674, 333);
            guna2ProgressIndicator1.Margin = new Padding(3, 4, 3, 4);
            guna2ProgressIndicator1.Name = "guna2ProgressIndicator1";
            guna2ProgressIndicator1.ShadowDecoration.CustomizableEdges = customizableEdges13;
            guna2ProgressIndicator1.Size = new Size(69, 80);
            guna2ProgressIndicator1.TabIndex = 10;
            guna2ProgressIndicator1.Visible = false;
            // 
            // SupplierWinforms
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(245, 247, 250);
            ClientSize = new Size(1429, 1000);
            Controls.Add(panelTable);
            Controls.Add(panelStats);
            Controls.Add(panelHeader);
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(3, 4, 3, 4);
            Name = "SupplierWinforms";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Suppliers";
            panelHeader.ResumeLayout(false);
            panelHeader.PerformLayout();
            panelStats.ResumeLayout(false);
            panelStats.PerformLayout();
            panelTable.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)guna2DataGridView1).EndInit();
            ResumeLayout(false);
        }

        private Guna.UI2.WinForms.Guna2Button btnClose;
    }
}
