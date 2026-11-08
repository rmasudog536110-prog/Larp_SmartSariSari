using static Guna.UI2.WinForms.Suite.Descriptions;
using static System.Net.Mime.MediaTypeNames;

namespace Larp.winforms.SmallTenant.Supplier
{
    partial class PurchasesWinForms
    {
        private System.ComponentModel.IContainer components = null;

        private Guna.UI2.WinForms.Guna2Panel panelHeader;
        private Guna.UI2.WinForms.Guna2Panel panelStats;
        private Guna.UI2.WinForms.Guna2Panel panelTable;

        private Guna.UI2.WinForms.Guna2TextBox guna2TextBox1;
        private Guna.UI2.WinForms.Guna2Button btnNewPurchase;
        private Guna.UI2.WinForms.Guna2Button btnRefresh;
        private Guna.UI2.WinForms.Guna2Button btnClose;

        private Guna.UI2.WinForms.Guna2DataGridView guna2DataGridView1;
        private Guna.UI2.WinForms.Guna2ProgressIndicator guna2ProgressIndicator1;

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblSubtitle;

        private System.Windows.Forms.Label lblTotalTitle;
        private System.Windows.Forms.Label lblTotalPurchases;

        private System.Windows.Forms.Label lblCompletedTitle;
        private System.Windows.Forms.Label lblCompletedPurchases;

        private System.Windows.Forms.Label lblCancelledTitle;
        private System.Windows.Forms.Label lblCancelledPurchases;

        private System.Windows.Forms.Label lblValueTitle;
        private System.Windows.Forms.Label lblTotalValue;

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
            lblTitle = new Label();
            lblSubtitle = new Label();
            guna2TextBox1 = new Guna.UI2.WinForms.Guna2TextBox();
            btnRefresh = new Guna.UI2.WinForms.Guna2Button();
            btnNewPurchase = new Guna.UI2.WinForms.Guna2Button();
            btnClose = new Guna.UI2.WinForms.Guna2Button();
            panelStats = new Guna.UI2.WinForms.Guna2Panel();
            lblTotalTitle = new Label();
            lblTotalPurchases = new Label();
            lblCompletedTitle = new Label();
            lblCompletedPurchases = new Label();
            lblCancelledTitle = new Label();
            lblCancelledPurchases = new Label();
            lblValueTitle = new Label();
            lblTotalValue = new Label();
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
            panelHeader.Controls.Add(lblTitle);
            panelHeader.Controls.Add(lblSubtitle);
            panelHeader.Controls.Add(guna2TextBox1);
            panelHeader.Controls.Add(btnRefresh);
            panelHeader.Controls.Add(btnNewPurchase);
            panelHeader.Controls.Add(btnClose);
            panelHeader.CustomizableEdges = customizableEdges9;
            panelHeader.Dock = DockStyle.Top;
            panelHeader.Location = new Point(0, 0);
            panelHeader.Name = "panelHeader";
            panelHeader.ShadowDecoration.CustomizableEdges = customizableEdges10;
            panelHeader.Size = new Size(1250, 115);
            panelHeader.TabIndex = 0;
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new System.Drawing.Font("Segoe UI", 22F, FontStyle.Bold);
            lblTitle.ForeColor = Color.FromArgb(31, 41, 55);
            lblTitle.Location = new Point(30, 20);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(194, 50);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "Purchases";
            // 
            // lblSubtitle
            // 
            lblSubtitle.AutoSize = true;
            lblSubtitle.Font = new System.Drawing.Font("Segoe UI", 10F);
            lblSubtitle.ForeColor = Color.FromArgb(107, 114, 128);
            lblSubtitle.Location = new Point(33, 65);
            lblSubtitle.Name = "lblSubtitle";
            lblSubtitle.Size = new Size(409, 23);
            lblSubtitle.TabIndex = 1;
            lblSubtitle.Text = "Record supplier purchases and manage stock intake.";
            // 
            // guna2TextBox1
            // 
            guna2TextBox1.BorderRadius = 8;
            guna2TextBox1.Cursor = Cursors.IBeam;
            guna2TextBox1.CustomizableEdges = customizableEdges1;
            guna2TextBox1.DefaultText = "";
            guna2TextBox1.Font = new System.Drawing.Font("Segoe UI", 10F);
            guna2TextBox1.Location = new Point(489, 46);
            guna2TextBox1.Margin = new Padding(4);
            guna2TextBox1.Name = "guna2TextBox1";
            guna2TextBox1.PlaceholderText = "Search purchases...";
            guna2TextBox1.SelectedText = "";
            guna2TextBox1.ShadowDecoration.CustomizableEdges = customizableEdges2;
            guna2TextBox1.Size = new Size(280, 42);
            guna2TextBox1.TabIndex = 1;
            guna2TextBox1.TextChanged += guna2TextBox1_TextChanged;
            // 
            // btnRefresh
            // 
            btnRefresh.BorderRadius = 8;
            btnRefresh.CustomizableEdges = customizableEdges3;
            btnRefresh.FillColor = Color.FromArgb(107, 114, 128);
            btnRefresh.Font = new System.Drawing.Font("Segoe UI", 10F, FontStyle.Bold);
            btnRefresh.ForeColor = Color.White;
            btnRefresh.Location = new Point(815, 46);
            btnRefresh.Name = "btnRefresh";
            btnRefresh.ShadowDecoration.CustomizableEdges = customizableEdges4;
            btnRefresh.Size = new Size(100, 42);
            btnRefresh.TabIndex = 2;
            btnRefresh.Tag = "secondary";
            btnRefresh.Text = "Refresh";
            btnRefresh.Click += btnRefresh_Click;
            // 
            // btnNewPurchase
            // 
            btnNewPurchase.BorderRadius = 8;
            btnNewPurchase.CustomizableEdges = customizableEdges5;
            btnNewPurchase.FillColor = Color.FromArgb(46, 107, 69);
            btnNewPurchase.Font = new System.Drawing.Font("Segoe UI", 10F, FontStyle.Bold);
            btnNewPurchase.ForeColor = Color.White;
            btnNewPurchase.Location = new Point(941, 46);
            btnNewPurchase.Name = "btnNewPurchase";
            btnNewPurchase.ShadowDecoration.CustomizableEdges = customizableEdges6;
            btnNewPurchase.Size = new Size(150, 42);
            btnNewPurchase.TabIndex = 3;
            btnNewPurchase.Tag = "primary";
            btnNewPurchase.Text = "New Purchase";
            btnNewPurchase.Click += btnNewPurchase_Click;
            // 
            // btnClose
            // 
            btnClose.BorderRadius = 8;
            btnClose.CustomizableEdges = customizableEdges7;
            btnClose.FillColor = Color.FromArgb(107, 114, 128);
            btnClose.Font = new System.Drawing.Font("Segoe UI", 10F, FontStyle.Bold);
            btnClose.ForeColor = Color.White;
            btnClose.Location = new Point(1110, 46);
            btnClose.Name = "btnClose";
            btnClose.ShadowDecoration.CustomizableEdges = customizableEdges8;
            btnClose.Size = new Size(100, 42);
            btnClose.TabIndex = 4;
            btnClose.Tag = "secondary";
            btnClose.Text = "Close";
            btnClose.Click += btnClose_Click;
            // 
            // panelStats
            // 
            panelStats.BackColor = Color.Transparent;
            panelStats.Controls.Add(lblTotalTitle);
            panelStats.Controls.Add(lblTotalPurchases);
            panelStats.Controls.Add(lblCompletedTitle);
            panelStats.Controls.Add(lblCompletedPurchases);
            panelStats.Controls.Add(lblCancelledTitle);
            panelStats.Controls.Add(lblCancelledPurchases);
            panelStats.Controls.Add(lblValueTitle);
            panelStats.Controls.Add(lblTotalValue);
            panelStats.CustomizableEdges = customizableEdges11;
            panelStats.Dock = DockStyle.Top;
            panelStats.Location = new Point(0, 115);
            panelStats.Name = "panelStats";
            panelStats.ShadowDecoration.CustomizableEdges = customizableEdges12;
            panelStats.Size = new Size(1250, 109);
            panelStats.TabIndex = 1;
            // 
            // lblTotalTitle
            // 
            lblTotalTitle.AutoSize = true;
            lblTotalTitle.Font = new System.Drawing.Font("Segoe UI", 10F);
            lblTotalTitle.ForeColor = Color.FromArgb(107, 114, 128);
            lblTotalTitle.Location = new Point(35, 20);
            lblTotalTitle.Name = "lblTotalTitle";
            lblTotalTitle.Size = new Size(127, 23);
            lblTotalTitle.TabIndex = 0;
            lblTotalTitle.Text = "Total Purchases";
            // 
            // lblTotalPurchases
            // 
            lblTotalPurchases.AutoSize = true;
            lblTotalPurchases.Font = new System.Drawing.Font("Segoe UI", 24F, FontStyle.Bold);
            lblTotalPurchases.ForeColor = Color.FromArgb(31, 41, 55);
            lblTotalPurchases.Location = new Point(32, 43);
            lblTotalPurchases.Name = "lblTotalPurchases";
            lblTotalPurchases.Size = new Size(46, 54);
            lblTotalPurchases.TabIndex = 1;
            lblTotalPurchases.Text = "0";
            // 
            // lblCompletedTitle
            // 
            lblCompletedTitle.AutoSize = true;
            lblCompletedTitle.Font = new System.Drawing.Font("Segoe UI", 10F);
            lblCompletedTitle.ForeColor = Color.FromArgb(107, 114, 128);
            lblCompletedTitle.Location = new Point(300, 20);
            lblCompletedTitle.Name = "lblCompletedTitle";
            lblCompletedTitle.Size = new Size(94, 23);
            lblCompletedTitle.TabIndex = 2;
            lblCompletedTitle.Text = "Completed";
            // 
            // lblCompletedPurchases
            // 
            lblCompletedPurchases.AutoSize = true;
            lblCompletedPurchases.Font = new System.Drawing.Font("Segoe UI", 24F, FontStyle.Bold);
            lblCompletedPurchases.ForeColor = Color.FromArgb(46, 107, 69);
            lblCompletedPurchases.Location = new Point(297, 43);
            lblCompletedPurchases.Name = "lblCompletedPurchases";
            lblCompletedPurchases.Size = new Size(46, 54);
            lblCompletedPurchases.TabIndex = 3;
            lblCompletedPurchases.Text = "0";
            // 
            // lblCancelledTitle
            // 
            lblCancelledTitle.AutoSize = true;
            lblCancelledTitle.Font = new System.Drawing.Font("Segoe UI", 10F);
            lblCancelledTitle.ForeColor = Color.FromArgb(107, 114, 128);
            lblCancelledTitle.Location = new Point(565, 20);
            lblCancelledTitle.Name = "lblCancelledTitle";
            lblCancelledTitle.Size = new Size(84, 23);
            lblCancelledTitle.TabIndex = 4;
            lblCancelledTitle.Text = "Cancelled";
            // 
            // lblCancelledPurchases
            // 
            lblCancelledPurchases.AutoSize = true;
            lblCancelledPurchases.Font = new System.Drawing.Font("Segoe UI", 24F, FontStyle.Bold);
            lblCancelledPurchases.ForeColor = Color.FromArgb(220, 38, 38);
            lblCancelledPurchases.Location = new Point(562, 43);
            lblCancelledPurchases.Name = "lblCancelledPurchases";
            lblCancelledPurchases.Size = new Size(46, 54);
            lblCancelledPurchases.TabIndex = 5;
            lblCancelledPurchases.Text = "0";
            // 
            // lblValueTitle
            // 
            lblValueTitle.AutoSize = true;
            lblValueTitle.Font = new System.Drawing.Font("Segoe UI", 10F);
            lblValueTitle.ForeColor = Color.FromArgb(107, 114, 128);
            lblValueTitle.Location = new Point(830, 20);
            lblValueTitle.Name = "lblValueTitle";
            lblValueTitle.Size = new Size(226, 23);
            lblValueTitle.TabIndex = 6;
            lblValueTitle.Text = "Total Purchased (completed)";
            // 
            // lblTotalValue
            // 
            lblTotalValue.AutoSize = true;
            lblTotalValue.Font = new System.Drawing.Font("Segoe UI", 24F, FontStyle.Bold);
            lblTotalValue.ForeColor = Color.FromArgb(31, 41, 55);
            lblTotalValue.Location = new Point(827, 43);
            lblTotalValue.Name = "lblTotalValue";
            lblTotalValue.Size = new Size(129, 54);
            lblTotalValue.TabIndex = 7;
            lblTotalValue.Text = "₱0.00";
            // 
            // panelTable
            // 
            panelTable.BackColor = Color.White;
            panelTable.BorderRadius = 10;
            panelTable.Controls.Add(guna2DataGridView1);
            panelTable.Controls.Add(guna2ProgressIndicator1);
            panelTable.CustomizableEdges = customizableEdges14;
            panelTable.Dock = DockStyle.Fill;
            panelTable.Location = new Point(0, 224);
            panelTable.Name = "panelTable";
            panelTable.Padding = new Padding(20);
            panelTable.ShadowDecoration.CustomizableEdges = customizableEdges15;
            panelTable.Size = new Size(1250, 526);
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
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 9F);
            dataGridViewCellStyle3.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = DataGridViewTriState.False;
            guna2DataGridView1.DefaultCellStyle = dataGridViewCellStyle3;
            guna2DataGridView1.Dock = DockStyle.Fill;
            guna2DataGridView1.GridColor = Color.FromArgb(229, 231, 235);
            guna2DataGridView1.Location = new Point(20, 20);
            guna2DataGridView1.Name = "guna2DataGridView1";
            guna2DataGridView1.RowHeadersVisible = false;
            guna2DataGridView1.RowHeadersWidth = 51;
            guna2DataGridView1.RowTemplate.Height = 42;
            guna2DataGridView1.Size = new Size(1210, 486);
            guna2DataGridView1.TabIndex = 0;
            guna2DataGridView1.ThemeStyle.AlternatingRowsStyle.BackColor = Color.White;
            guna2DataGridView1.ThemeStyle.GridColor = Color.FromArgb(229, 231, 235);
            guna2DataGridView1.ThemeStyle.HeaderStyle.BackColor = Color.Empty;
            guna2DataGridView1.ThemeStyle.HeaderStyle.BorderStyle = DataGridViewHeaderBorderStyle.Single;
            guna2DataGridView1.ThemeStyle.HeaderStyle.Font = null;
            guna2DataGridView1.ThemeStyle.HeaderStyle.ForeColor = Color.Empty;
            guna2DataGridView1.ThemeStyle.HeaderStyle.Height = 40;
            guna2DataGridView1.ThemeStyle.RowsStyle.BackColor = SystemColors.Window;
            guna2DataGridView1.ThemeStyle.RowsStyle.Font = new System.Drawing.Font("Segoe UI", 9F);
            guna2DataGridView1.ThemeStyle.RowsStyle.ForeColor = SystemColors.ControlText;
            guna2DataGridView1.ThemeStyle.RowsStyle.Height = 42;
            guna2DataGridView1.ThemeStyle.RowsStyle.SelectionBackColor = SystemColors.Highlight;
            guna2DataGridView1.ThemeStyle.RowsStyle.SelectionForeColor = SystemColors.HighlightText;
            guna2DataGridView1.CellContentClick += guna2DataGridView1_CellContentClick;
            guna2DataGridView1.DataBindingComplete += guna2DataGridView1_DataBindingComplete;
            // 
            // guna2ProgressIndicator1
            // 
            guna2ProgressIndicator1.Location = new Point(590, 250);
            guna2ProgressIndicator1.Name = "guna2ProgressIndicator1";
            guna2ProgressIndicator1.ShadowDecoration.CustomizableEdges = customizableEdges13;
            guna2ProgressIndicator1.Size = new Size(60, 60);
            guna2ProgressIndicator1.TabIndex = 10;
            guna2ProgressIndicator1.Visible = false;
            // 
            // PurchasesWinForms
            // 
            AutoScaleDimensions = new SizeF(6.8F, 14F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(245, 247, 250);
            ClientSize = new Size(1250, 750);
            Controls.Add(panelTable);
            Controls.Add(panelStats);
            Controls.Add(panelHeader);
            FormBorderStyle = FormBorderStyle.None;
            Name = "PurchasesWinForms";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Purchases";
            panelHeader.ResumeLayout(false);
            panelHeader.PerformLayout();
            panelStats.ResumeLayout(false);
            panelStats.PerformLayout();
            panelTable.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)guna2DataGridView1).EndInit();
            ResumeLayout(false);
        }
    }
}