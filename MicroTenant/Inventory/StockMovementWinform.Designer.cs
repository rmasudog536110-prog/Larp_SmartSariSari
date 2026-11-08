namespace Larp.winforms.SmallTenant.Supplier
{
    partial class StockMovementWinforms
    {
        private System.ComponentModel.IContainer components = null;

        private Guna.UI2.WinForms.Guna2Panel headerPanel;
        private Guna.UI2.WinForms.Guna2HtmlLabel titleLabel;
        private Guna.UI2.WinForms.Guna2HtmlLabel productLabel;
        private Guna.UI2.WinForms.Guna2HtmlLabel ProductNameLabel;
        private Guna.UI2.WinForms.Guna2DataGridView StockMovementGrid;
        private Guna.UI2.WinForms.Guna2Panel footerPanel;
        private Guna.UI2.WinForms.Guna2Button RefreshButton;
        private Guna.UI2.WinForms.Guna2Button CloseButton;
        private Guna.UI2.WinForms.Guna2ProgressIndicator guna2ProgressIndicator1;

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
            components =
                new System.ComponentModel.Container();

            headerPanel =
                new Guna.UI2.WinForms.Guna2Panel();

            titleLabel =
                new Guna.UI2.WinForms.Guna2HtmlLabel();

            productLabel =
                new Guna.UI2.WinForms.Guna2HtmlLabel();

            ProductNameLabel =
                new Guna.UI2.WinForms.Guna2HtmlLabel();

            StockMovementGrid =
                new Guna.UI2.WinForms.Guna2DataGridView();

            footerPanel =
                new Guna.UI2.WinForms.Guna2Panel();

            RefreshButton =
                new Guna.UI2.WinForms.Guna2Button();

            CloseButton =
                new Guna.UI2.WinForms.Guna2Button();

            guna2ProgressIndicator1 =
                new Guna.UI2.WinForms.Guna2ProgressIndicator();

            headerPanel.SuspendLayout();

            ((System.ComponentModel.ISupportInitialize)
                StockMovementGrid).BeginInit();

            footerPanel.SuspendLayout();

            SuspendLayout();

            headerPanel.BackColor =
                System.Drawing.Color.White;

            headerPanel.Dock =
                System.Windows.Forms.DockStyle.Top;

            headerPanel.FillColor =
                System.Drawing.Color.White;

            headerPanel.Location =
                new System.Drawing.Point(0, 0);

            headerPanel.Name =
                "headerPanel";

            headerPanel.Padding =
                new System.Windows.Forms.Padding(
                    25,
                    18,
                    25,
                    15);

            headerPanel.Size =
                new System.Drawing.Size(1200, 105);

            headerPanel.TabIndex = 0;

            titleLabel.BackColor =
                System.Drawing.Color.Transparent;

            titleLabel.Font =
                new System.Drawing.Font(
                    "Segoe UI Semibold",
                    18F,
                    System.Drawing.FontStyle.Bold,
                    System.Drawing.GraphicsUnit.Point);

            titleLabel.ForeColor =
                System.Drawing.Color.FromArgb(
                    28,
                    58,
                    42);

            titleLabel.Location =
                new System.Drawing.Point(25, 18);

            titleLabel.Name =
                "titleLabel";

            titleLabel.Size =
                new System.Drawing.Size(250, 34);

            titleLabel.TabIndex = 0;

            titleLabel.Text =
                "Stock Movement History";

            productLabel.BackColor =
                System.Drawing.Color.Transparent;

            productLabel.Font =
                new System.Drawing.Font(
                    "Segoe UI",
                    10F,
                    System.Drawing.FontStyle.Regular,
                    System.Drawing.GraphicsUnit.Point);

            productLabel.ForeColor =
                System.Drawing.Color.FromArgb(
                    100,
                    100,
                    100);

            productLabel.Location =
                new System.Drawing.Point(28, 62);

            productLabel.Name =
                "productLabel";

            productLabel.Size =
                new System.Drawing.Size(55, 21);

            productLabel.TabIndex = 1;

            productLabel.Text =
                "Product:";

            ProductNameLabel.BackColor =
                System.Drawing.Color.Transparent;

            ProductNameLabel.Font =
                new System.Drawing.Font(
                    "Segoe UI Semibold",
                    10F,
                    System.Drawing.FontStyle.Bold,
                    System.Drawing.GraphicsUnit.Point);

            ProductNameLabel.ForeColor =
                System.Drawing.Color.FromArgb(
                    46,
                    107,
                    69);

            ProductNameLabel.Location =
                new System.Drawing.Point(90, 62);

            ProductNameLabel.Name =
                "ProductNameLabel";

            ProductNameLabel.Size =
                new System.Drawing.Size(300, 21);

            ProductNameLabel.TabIndex = 2;

            ProductNameLabel.Text =
                "Product Name";

            StockMovementGrid.AllowUserToAddRows =
                false;

            StockMovementGrid.AllowUserToDeleteRows =
                false;

            StockMovementGrid.AllowUserToResizeRows =
                false;

            StockMovementGrid.AutoSizeColumnsMode =
                System.Windows.Forms.DataGridViewAutoSizeColumnsMode.None;

            StockMovementGrid.BackgroundColor =
                System.Drawing.Color.White;

            StockMovementGrid.BorderStyle =
                System.Windows.Forms.BorderStyle.None;

            StockMovementGrid.ColumnHeadersBorderStyle =
                System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;

            StockMovementGrid.ColumnHeadersDefaultCellStyle =
                new System.Windows.Forms.DataGridViewCellStyle
                {
                    Alignment =
                        System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft,

                    BackColor =
                        System.Drawing.Color.FromArgb(
                            28,
                            58,
                            42),

                    Font =
                        new System.Drawing.Font(
                            "Segoe UI Semibold",
                            10F,
                            System.Drawing.FontStyle.Bold,
                            System.Drawing.GraphicsUnit.Point),

                    ForeColor =
                        System.Drawing.Color.White,

                    SelectionBackColor =
                        System.Drawing.Color.FromArgb(
                            28,
                            58,
                            42),

                    SelectionForeColor =
                        System.Drawing.Color.White
                };

            StockMovementGrid.ColumnHeadersHeight = 42;

            StockMovementGrid.ColumnHeadersHeightSizeMode =
                System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;

            StockMovementGrid.DefaultCellStyle =
                new System.Windows.Forms.DataGridViewCellStyle
                {
                    Alignment =
                        System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft,

                    BackColor =
                        System.Drawing.Color.White,

                    Font =
                        new System.Drawing.Font(
                            "Segoe UI",
                            9.5F,
                            System.Drawing.FontStyle.Regular,
                            System.Drawing.GraphicsUnit.Point),

                    ForeColor =
                        System.Drawing.Color.FromArgb(
                            50,
                            50,
                            50),

                    SelectionBackColor =
                        System.Drawing.Color.FromArgb(
                            225,
                            239,
                            230),

                    SelectionForeColor =
                        System.Drawing.Color.FromArgb(
                            28,
                            58,
                            42)
                };

            StockMovementGrid.Dock =
                System.Windows.Forms.DockStyle.Fill;

            StockMovementGrid.GridColor =
                System.Drawing.Color.FromArgb(
                    230,
                    230,
                    230);

            StockMovementGrid.Location =
                new System.Drawing.Point(0, 105);

            StockMovementGrid.Name =
                "StockMovementGrid";

            StockMovementGrid.ReadOnly = true;

            StockMovementGrid.RowHeadersVisible = false;

            StockMovementGrid.RowTemplate.Height = 38;

            StockMovementGrid.SelectionMode =
                System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;

            StockMovementGrid.Size =
                new System.Drawing.Size(1200, 555);

            StockMovementGrid.TabIndex = 1;

            StockMovementGrid.ThemeStyle.AlternatingRowsStyle.BackColor =
                System.Drawing.Color.White;

            StockMovementGrid.ThemeStyle.BackColor =
                System.Drawing.Color.White;

            StockMovementGrid.ThemeStyle.GridColor =
                System.Drawing.Color.FromArgb(
                    230,
                    230,
                    230);

            StockMovementGrid.ThemeStyle.HeaderStyle.BackColor =
                System.Drawing.Color.FromArgb(
                    28,
                    58,
                    42);

            StockMovementGrid.ThemeStyle.HeaderStyle.BorderStyle =
                System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;

            StockMovementGrid.ThemeStyle.HeaderStyle.Font =
                new System.Drawing.Font(
                    "Segoe UI Semibold",
                    10F,
                    System.Drawing.FontStyle.Bold,
                    System.Drawing.GraphicsUnit.Point);

            StockMovementGrid.ThemeStyle.HeaderStyle.ForeColor =
                System.Drawing.Color.White;

            StockMovementGrid.ThemeStyle.HeaderStyle.Height = 42;

            StockMovementGrid.ThemeStyle.ReadOnly = true;

            StockMovementGrid.ThemeStyle.RowsStyle.BackColor =
                System.Drawing.Color.White;

            StockMovementGrid.ThemeStyle.RowsStyle.BorderStyle =
                System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;

            StockMovementGrid.ThemeStyle.RowsStyle.Font =
                new System.Drawing.Font(
                    "Segoe UI",
                    9.5F,
                    System.Drawing.FontStyle.Regular,
                    System.Drawing.GraphicsUnit.Point);

            StockMovementGrid.ThemeStyle.RowsStyle.ForeColor =
                System.Drawing.Color.FromArgb(
                    50,
                    50,
                    50);

            StockMovementGrid.ThemeStyle.RowsStyle.Height = 38;

            StockMovementGrid.ThemeStyle.RowsStyle.SelectionBackColor =
                System.Drawing.Color.FromArgb(
                    225,
                    239,
                    230);

            StockMovementGrid.ThemeStyle.RowsStyle.SelectionForeColor =
                System.Drawing.Color.FromArgb(
                    28,
                    58,
                    42);

            footerPanel.BackColor =
                System.Drawing.Color.White;

            footerPanel.Dock =
                System.Windows.Forms.DockStyle.Bottom;

            footerPanel.FillColor =
                System.Drawing.Color.White;

            footerPanel.Location =
                new System.Drawing.Point(0, 660);

            footerPanel.Name =
                "footerPanel";

            footerPanel.Size =
                new System.Drawing.Size(1200, 80);

            footerPanel.TabIndex = 2;

            CloseButton.BorderRadius = 8;

            CloseButton.FillColor =
                System.Drawing.Color.FromArgb(
                    110,
                    110,
                    110);

            CloseButton.Font =
                new System.Drawing.Font(
                    "Segoe UI Semibold",
                    10F,
                    System.Drawing.FontStyle.Bold,
                    System.Drawing.GraphicsUnit.Point);

            CloseButton.ForeColor =
                System.Drawing.Color.White;

            CloseButton.Location =
                new System.Drawing.Point(1055, 20);

            CloseButton.Name =
                "CloseButton";

            CloseButton.Size =
                new System.Drawing.Size(115, 40);

            CloseButton.TabIndex = 0;

            CloseButton.Text =
                "Close";

            CloseButton.Click +=
                CloseButton_Click;

            RefreshButton.BorderRadius = 8;

            RefreshButton.FillColor =
                System.Drawing.Color.FromArgb(
                    46,
                    107,
                    69);

            RefreshButton.Font =
                new System.Drawing.Font(
                    "Segoe UI Semibold",
                    10F,
                    System.Drawing.FontStyle.Bold,
                    System.Drawing.GraphicsUnit.Point);

            RefreshButton.ForeColor =
                System.Drawing.Color.White;

            RefreshButton.Location =
                new System.Drawing.Point(925, 20);

            RefreshButton.Name =
                "RefreshButton";

            RefreshButton.Size =
                new System.Drawing.Size(115, 40);

            RefreshButton.TabIndex = 1;

            RefreshButton.Text =
                "Refresh";

            RefreshButton.Click +=
                RefreshButton_Click;

            guna2ProgressIndicator1.AutoStart = true;

            guna2ProgressIndicator1.BackColor =
                System.Drawing.Color.Transparent;

            guna2ProgressIndicator1.Location =
                new System.Drawing.Point(577, 350);

            guna2ProgressIndicator1.Name =
                "guna2ProgressIndicator1";

            guna2ProgressIndicator1.ProgressColor =
                System.Drawing.Color.FromArgb(
                    46,
                    107,
                    69);

            guna2ProgressIndicator1.Size =
                new System.Drawing.Size(45, 45);

            guna2ProgressIndicator1.TabIndex = 3;

            guna2ProgressIndicator1.Visible = false;

            footerPanel.Controls.Add(
                CloseButton);

            footerPanel.Controls.Add(
                RefreshButton);

            Controls.Add(
                StockMovementGrid);

            Controls.Add(
                guna2ProgressIndicator1);

            Controls.Add(
                footerPanel);

            Controls.Add(
                headerPanel);

            AutoScaleDimensions =
                new System.Drawing.SizeF(
                    7F,
                    15F);

            AutoScaleMode =
                System.Windows.Forms.AutoScaleMode.Font;

            BackColor =
                System.Drawing.Color.FromArgb(
                    245,
                    242,
                    237);

            ClientSize =
                new System.Drawing.Size(
                    1200,
                    740);

            FormBorderStyle =
                System.Windows.Forms.FormBorderStyle.FixedDialog;

            MaximizeBox = false;

            MinimizeBox = false;

            Name =
                "StockMovementWinforms";

            StartPosition =
                System.Windows.Forms.FormStartPosition.CenterParent;

            Text =
                "Stock Movement History";

            headerPanel.ResumeLayout(false);
            headerPanel.PerformLayout();

            ((System.ComponentModel.ISupportInitialize)
                StockMovementGrid).EndInit();

            footerPanel.ResumeLayout(false);

            ResumeLayout(false);
        }
    }
}