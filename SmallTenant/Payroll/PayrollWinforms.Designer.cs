namespace Larp.winforms.SmallTenant.Payroll
{
    partial class PayrollWinforms
    {
        private System.ComponentModel.IContainer components = null;

        private Guna.UI2.WinForms.Guna2Panel HeaderPanel;
        private Guna.UI2.WinForms.Guna2HtmlLabel TitleLabel;
        private Guna.UI2.WinForms.Guna2TextBox SearchPayroll;
        private Guna.UI2.WinForms.Guna2Button AddPayroll;
        private Guna.UI2.WinForms.Guna2DataGridView PayrollGrid;

        protected override void Dispose(bool disposing)
        {
            if (disposing &&
                (components != null))
            {
                components.Dispose();
            }

            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges7 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges8 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges3 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges4 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges5 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges6 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            HeaderPanel = new Guna.UI2.WinForms.Guna2Panel();
            btnClose = new Guna.UI2.WinForms.Guna2Button();
            TitleLabel = new Guna.UI2.WinForms.Guna2HtmlLabel();
            SearchPayroll = new Guna.UI2.WinForms.Guna2TextBox();
            AddPayroll = new Guna.UI2.WinForms.Guna2Button();
            PayrollGrid = new Guna.UI2.WinForms.Guna2DataGridView();
            HeaderPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)PayrollGrid).BeginInit();
            SuspendLayout();
            // 
            // HeaderPanel
            // 
            HeaderPanel.Controls.Add(btnClose);
            HeaderPanel.Controls.Add(TitleLabel);
            HeaderPanel.Controls.Add(SearchPayroll);
            HeaderPanel.Controls.Add(AddPayroll);
            HeaderPanel.CustomizableEdges = customizableEdges7;
            HeaderPanel.Dock = DockStyle.Top;
            HeaderPanel.FillColor = Color.White;
            HeaderPanel.Location = new Point(0, 0);
            HeaderPanel.Margin = new Padding(3, 4, 3, 4);
            HeaderPanel.Name = "HeaderPanel";
            HeaderPanel.ShadowDecoration.CustomizableEdges = customizableEdges8;
            HeaderPanel.Size = new Size(1371, 133);
            HeaderPanel.TabIndex = 4;
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
            btnClose.Location = new Point(1252, 38);
            btnClose.Margin = new Padding(3, 4, 3, 4);
            btnClose.Name = "btnClose";
            btnClose.ShadowDecoration.CustomizableEdges = customizableEdges2;
            btnClose.Size = new Size(107, 52);
            btnClose.TabIndex = 6;
            btnClose.Text = "Close";
            btnClose.Click += btnClose_Click;
            // 
            // TitleLabel
            // 
            TitleLabel.BackColor = Color.Transparent;
            TitleLabel.Font = new Font("Segoe UI Semibold", 20F, FontStyle.Bold);
            TitleLabel.ForeColor = Color.FromArgb(28, 58, 42);
            TitleLabel.Location = new Point(29, 33);
            TitleLabel.Margin = new Padding(3, 4, 3, 4);
            TitleLabel.Name = "TitleLabel";
            TitleLabel.Size = new Size(106, 47);
            TitleLabel.TabIndex = 0;
            TitleLabel.Text = "Payroll";
            // 
            // SearchPayroll
            // 
            SearchPayroll.BorderRadius = 8;
            SearchPayroll.Cursor = Cursors.IBeam;
            SearchPayroll.CustomizableEdges = customizableEdges3;
            SearchPayroll.DefaultText = "";
            SearchPayroll.Font = new Font("Segoe UI", 10F);
            SearchPayroll.Location = new Point(297, 37);
            SearchPayroll.Margin = new Padding(3, 5, 3, 5);
            SearchPayroll.Name = "SearchPayroll";
            SearchPayroll.PlaceholderText = "Search employee...";
            SearchPayroll.SelectedText = "";
            SearchPayroll.ShadowDecoration.CustomizableEdges = customizableEdges4;
            SearchPayroll.Size = new Size(343, 53);
            SearchPayroll.TabIndex = 1;
            // 
            // AddPayroll
            // 
            AddPayroll.BorderRadius = 8;
            AddPayroll.CustomizableEdges = customizableEdges5;
            AddPayroll.FillColor = Color.FromArgb(46, 107, 69);
            AddPayroll.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            AddPayroll.ForeColor = Color.White;
            AddPayroll.Location = new Point(1096, 38);
            AddPayroll.Margin = new Padding(3, 4, 3, 4);
            AddPayroll.Name = "AddPayroll";
            AddPayroll.ShadowDecoration.CustomizableEdges = customizableEdges6;
            AddPayroll.Size = new Size(150, 53);
            AddPayroll.TabIndex = 2;
            AddPayroll.Text = "+ Add Payroll";
            AddPayroll.Click += AddPayroll_Click;
            // 
            // PayrollGrid
            // 
            PayrollGrid.AllowUserToAddRows = false;
            PayrollGrid.AllowUserToDeleteRows = false;
            PayrollGrid.AllowUserToResizeRows = false;
            PayrollGrid.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            PayrollGrid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            PayrollGrid.ColumnHeadersHeight = 40;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = SystemColors.Window;
            dataGridViewCellStyle2.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle2.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.False;
            PayrollGrid.DefaultCellStyle = dataGridViewCellStyle2;
            PayrollGrid.Dock = DockStyle.Fill;
            PayrollGrid.GridColor = Color.FromArgb(230, 230, 230);
            PayrollGrid.Location = new Point(0, 133);
            PayrollGrid.Margin = new Padding(3, 4, 3, 4);
            PayrollGrid.Name = "PayrollGrid";
            PayrollGrid.ReadOnly = true;
            PayrollGrid.RowHeadersVisible = false;
            PayrollGrid.RowHeadersWidth = 51;
            PayrollGrid.RowTemplate.Height = 40;
            PayrollGrid.Size = new Size(1371, 800);
            PayrollGrid.TabIndex = 3;
            PayrollGrid.ThemeStyle.GridColor = Color.FromArgb(230, 230, 230);
            PayrollGrid.ThemeStyle.HeaderStyle.BackColor = Color.Empty;
            PayrollGrid.ThemeStyle.HeaderStyle.BorderStyle = DataGridViewHeaderBorderStyle.Single;
            PayrollGrid.ThemeStyle.HeaderStyle.Font = null;
            PayrollGrid.ThemeStyle.HeaderStyle.ForeColor = Color.Empty;
            PayrollGrid.ThemeStyle.HeaderStyle.HeaightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            PayrollGrid.ThemeStyle.HeaderStyle.Height = 40;
            PayrollGrid.ThemeStyle.ReadOnly = true;
            PayrollGrid.ThemeStyle.RowsStyle.BackColor = SystemColors.Window;
            PayrollGrid.ThemeStyle.RowsStyle.Font = new Font("Segoe UI", 9F);
            PayrollGrid.ThemeStyle.RowsStyle.ForeColor = SystemColors.ControlText;
            PayrollGrid.ThemeStyle.RowsStyle.Height = 40;
            PayrollGrid.ThemeStyle.RowsStyle.SelectionBackColor = SystemColors.Highlight;
            PayrollGrid.ThemeStyle.RowsStyle.SelectionForeColor = SystemColors.HighlightText;
            // 
            // PayrollWinforms
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(248, 249, 250);
            ClientSize = new Size(1371, 933);
            Controls.Add(PayrollGrid);
            Controls.Add(HeaderPanel);
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(3, 4, 3, 4);
            Name = "PayrollWinforms";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Payroll";
            HeaderPanel.ResumeLayout(false);
            HeaderPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)PayrollGrid).EndInit();
            ResumeLayout(false);
        }

        private Guna.UI2.WinForms.Guna2Button btnClose;
    }
}