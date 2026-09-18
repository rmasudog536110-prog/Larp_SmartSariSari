namespace Larp.winforms.Inventory
{
    partial class InventoryWinForms
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges15 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges16 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges17 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges18 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges19 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges20 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            DataGridViewCellStyle dataGridViewCellStyle4 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle5 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle6 = new DataGridViewCellStyle();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges21 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges22 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges23 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges24 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges25 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges26 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges27 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges28 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            label1 = new Label();
            lblTotalProducts = new Guna.UI2.WinForms.Guna2CustomGradientPanel();
            TotalProductLabel = new Guna.UI2.WinForms.Guna2HtmlLabel();
            TotalProductTitle = new Guna.UI2.WinForms.Guna2HtmlLabel();
            lblLowStock = new Guna.UI2.WinForms.Guna2CustomGradientPanel();
            LowStockLabel = new Guna.UI2.WinForms.Guna2HtmlLabel();
            LowStockTitle = new Guna.UI2.WinForms.Guna2HtmlLabel();
            lblOutOfStock = new Guna.UI2.WinForms.Guna2CustomGradientPanel();
            OutOfStockLabel = new Guna.UI2.WinForms.Guna2HtmlLabel();
            OutOfStockTitle = new Guna.UI2.WinForms.Guna2HtmlLabel();
            guna2DataGridView1 = new Guna.UI2.WinForms.Guna2DataGridView();
            ProductNames = new DataGridViewTextBoxColumn();
            Category = new DataGridViewTextBoxColumn();
            Price = new DataGridViewTextBoxColumn();
            Status = new DataGridViewTextBoxColumn();
            Actions = new DataGridViewTextBoxColumn();
            AddStocks = new Guna.UI2.WinForms.Guna2GradientButton();
            guna2TextBox1 = new Guna.UI2.WinForms.Guna2TextBox();
            GotoProducts = new Guna.UI2.WinForms.Guna2Button();
            GotoSales = new Guna.UI2.WinForms.Guna2Button();
            lblTotalProducts.SuspendLayout();
            lblLowStock.SuspendLayout();
            lblOutOfStock.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)guna2DataGridView1).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            label1.AutoSize = true;
            label1.Font = new Font("Sans Serif Collection", 23.9999981F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(12, 9);
            label1.Name = "label1";
            label1.Size = new Size(248, 136);
            label1.TabIndex = 1;
            label1.Text = "Inventory";
            // 
            // lblTotalProducts
            // 
            lblTotalProducts.Controls.Add(TotalProductLabel);
            lblTotalProducts.Controls.Add(TotalProductTitle);
            lblTotalProducts.CustomizableEdges = customizableEdges15;
            lblTotalProducts.Location = new Point(36, 108);
            lblTotalProducts.Name = "lblTotalProducts";
            lblTotalProducts.ShadowDecoration.CustomizableEdges = customizableEdges16;
            lblTotalProducts.Size = new Size(201, 76);
            lblTotalProducts.TabIndex = 2;
            // 
            // TotalProductLabel
            // 
            TotalProductLabel.BackColor = Color.Transparent;
            TotalProductLabel.Font = new Font("Segoe UI Semibold", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            TotalProductLabel.Location = new Point(84, 36);
            TotalProductLabel.Name = "TotalProductLabel";
            TotalProductLabel.Size = new Size(20, 27);
            TotalProductLabel.TabIndex = 1;
            TotalProductLabel.Text = "12";
            // 
            // TotalProductTitle
            // 
            TotalProductTitle.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            TotalProductTitle.BackColor = Color.Transparent;
            TotalProductTitle.Font = new Font("Segoe UI Semibold", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            TotalProductTitle.Location = new Point(37, 3);
            TotalProductTitle.Name = "TotalProductTitle";
            TotalProductTitle.Size = new Size(116, 27);
            TotalProductTitle.TabIndex = 0;
            TotalProductTitle.Text = "Total Product";
            // 
            // lblLowStock
            // 
            lblLowStock.Anchor = AnchorStyles.Top;
            lblLowStock.Controls.Add(LowStockLabel);
            lblLowStock.Controls.Add(LowStockTitle);
            lblLowStock.CustomizableEdges = customizableEdges17;
            lblLowStock.Location = new Point(319, 108);
            lblLowStock.Name = "lblLowStock";
            lblLowStock.ShadowDecoration.CustomizableEdges = customizableEdges18;
            lblLowStock.Size = new Size(201, 76);
            lblLowStock.TabIndex = 3;
            // 
            // LowStockLabel
            // 
            LowStockLabel.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            LowStockLabel.BackColor = Color.Transparent;
            LowStockLabel.Font = new Font("Segoe UI Semibold", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            LowStockLabel.Location = new Point(90, 36);
            LowStockLabel.Name = "LowStockLabel";
            LowStockLabel.Size = new Size(20, 27);
            LowStockLabel.TabIndex = 2;
            LowStockLabel.Text = "12";
            // 
            // LowStockTitle
            // 
            LowStockTitle.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            LowStockTitle.BackColor = Color.Transparent;
            LowStockTitle.Font = new Font("Segoe UI Semibold", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            LowStockTitle.Location = new Point(61, 3);
            LowStockTitle.Name = "LowStockTitle";
            LowStockTitle.Size = new Size(87, 27);
            LowStockTitle.TabIndex = 1;
            LowStockTitle.Text = "Low Stock";
            // 
            // lblOutOfStock
            // 
            lblOutOfStock.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblOutOfStock.Controls.Add(OutOfStockLabel);
            lblOutOfStock.Controls.Add(OutOfStockTitle);
            lblOutOfStock.CustomizableEdges = customizableEdges19;
            lblOutOfStock.Location = new Point(596, 108);
            lblOutOfStock.Name = "lblOutOfStock";
            lblOutOfStock.ShadowDecoration.CustomizableEdges = customizableEdges20;
            lblOutOfStock.Size = new Size(201, 76);
            lblOutOfStock.TabIndex = 3;
            // 
            // OutOfStockLabel
            // 
            OutOfStockLabel.BackColor = Color.Transparent;
            OutOfStockLabel.Font = new Font("Segoe UI Semibold", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            OutOfStockLabel.Location = new Point(93, 36);
            OutOfStockLabel.Name = "OutOfStockLabel";
            OutOfStockLabel.Size = new Size(20, 27);
            OutOfStockLabel.TabIndex = 3;
            OutOfStockLabel.Text = "12";
            // 
            // OutOfStockTitle
            // 
            OutOfStockTitle.BackColor = Color.Transparent;
            OutOfStockTitle.Font = new Font("Segoe UI Semibold", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            OutOfStockTitle.Location = new Point(49, 3);
            OutOfStockTitle.Name = "OutOfStockTitle";
            OutOfStockTitle.Size = new Size(107, 27);
            OutOfStockTitle.TabIndex = 2;
            OutOfStockTitle.Text = "Out of Stock";
            // 
            // guna2DataGridView1
            // 
            dataGridViewCellStyle4.BackColor = Color.White;
            guna2DataGridView1.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle4;
            guna2DataGridView1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            guna2DataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            guna2DataGridView1.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle5.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = Color.FromArgb(100, 88, 255);
            dataGridViewCellStyle5.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle5.ForeColor = Color.White;
            dataGridViewCellStyle5.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = DataGridViewTriState.True;
            guna2DataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
            guna2DataGridView1.ColumnHeadersHeight = 44;
            guna2DataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            guna2DataGridView1.Columns.AddRange(new DataGridViewColumn[] { ProductNames, Category, Price, Status, Actions });
            dataGridViewCellStyle6.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = Color.White;
            dataGridViewCellStyle6.Font = new Font("Segoe UI", 12F);
            dataGridViewCellStyle6.ForeColor = Color.FromArgb(71, 69, 94);
            dataGridViewCellStyle6.SelectionBackColor = Color.FromArgb(231, 229, 255);
            dataGridViewCellStyle6.SelectionForeColor = Color.FromArgb(71, 69, 94);
            dataGridViewCellStyle6.WrapMode = DataGridViewTriState.False;
            guna2DataGridView1.DefaultCellStyle = dataGridViewCellStyle6;
            guna2DataGridView1.GridColor = Color.FromArgb(231, 229, 255);
            guna2DataGridView1.Location = new Point(23, 260);
            guna2DataGridView1.Name = "guna2DataGridView1";
            guna2DataGridView1.RowHeadersVisible = false;
            guna2DataGridView1.RowHeadersWidth = 51;
            guna2DataGridView1.RowTemplate.Height = 30;
            guna2DataGridView1.Size = new Size(774, 230);
            guna2DataGridView1.TabIndex = 4;
            guna2DataGridView1.ThemeStyle.AlternatingRowsStyle.BackColor = Color.White;
            guna2DataGridView1.ThemeStyle.HeaderStyle.BorderStyle = DataGridViewHeaderBorderStyle.Single;
            guna2DataGridView1.ThemeStyle.HeaderStyle.Font = new Font("Segoe UI", 9F);
            guna2DataGridView1.ThemeStyle.HeaderStyle.Height = 44;
            guna2DataGridView1.ThemeStyle.RowsStyle.Font = new Font("Segoe UI", 12F);
            guna2DataGridView1.ThemeStyle.RowsStyle.Height = 30;
            guna2DataGridView1.CellContentClick += guna2DataGridView1_CellContentClick;
            // 
            // ProductNames
            // 
            ProductNames.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            ProductNames.HeaderText = "Product Name";
            ProductNames.MinimumWidth = 6;
            ProductNames.Name = "ProductNames";
            // 
            // Category
            // 
            Category.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            Category.HeaderText = "Stock";
            Category.MinimumWidth = 6;
            Category.Name = "Category";
            // 
            // Price
            // 
            Price.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            Price.HeaderText = "ReOrder";
            Price.MinimumWidth = 6;
            Price.Name = "Price";
            // 
            // Status
            // 
            Status.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            Status.HeaderText = "Status";
            Status.MinimumWidth = 6;
            Status.Name = "Status";
            // 
            // Actions
            // 
            Actions.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            Actions.HeaderText = "Actions";
            Actions.MinimumWidth = 6;
            Actions.Name = "Actions";
            // 
            // AddStocks
            // 
            AddStocks.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            AddStocks.BorderRadius = 10;
            AddStocks.CustomizableEdges = customizableEdges21;
            AddStocks.DisabledState.BorderColor = Color.DarkGray;
            AddStocks.DisabledState.CustomBorderColor = Color.DarkGray;
            AddStocks.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            AddStocks.DisabledState.FillColor2 = Color.FromArgb(169, 169, 169);
            AddStocks.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            AddStocks.Font = new Font("Segoe UI", 9F);
            AddStocks.ForeColor = Color.White;
            AddStocks.Location = new Point(659, 216);
            AddStocks.Name = "AddStocks";
            AddStocks.ShadowDecoration.CustomizableEdges = customizableEdges22;
            AddStocks.Size = new Size(138, 38);
            AddStocks.TabIndex = 5;
            AddStocks.Text = "Add Stocks";
            AddStocks.Click += AddStocks_Click;
            // 
            // guna2TextBox1
            // 
            guna2TextBox1.BorderRadius = 10;
            guna2TextBox1.CustomizableEdges = customizableEdges23;
            guna2TextBox1.DefaultText = "";
            guna2TextBox1.DisabledState.BorderColor = Color.FromArgb(208, 208, 208);
            guna2TextBox1.DisabledState.FillColor = Color.FromArgb(226, 226, 226);
            guna2TextBox1.DisabledState.ForeColor = Color.FromArgb(138, 138, 138);
            guna2TextBox1.DisabledState.PlaceholderForeColor = Color.FromArgb(138, 138, 138);
            guna2TextBox1.FocusedState.BorderColor = Color.FromArgb(94, 148, 255);
            guna2TextBox1.Font = new Font("Segoe UI", 9F);
            guna2TextBox1.HoverState.BorderColor = Color.FromArgb(94, 148, 255);
            guna2TextBox1.Location = new Point(23, 216);
            guna2TextBox1.Margin = new Padding(3, 4, 3, 4);
            guna2TextBox1.Name = "guna2TextBox1";
            guna2TextBox1.PlaceholderText = "Search for products";
            guna2TextBox1.SelectedText = "";
            guna2TextBox1.ShadowDecoration.CustomizableEdges = customizableEdges24;
            guna2TextBox1.Size = new Size(286, 38);
            guna2TextBox1.TabIndex = 6;
            // 
            // GotoProducts
            // 
            GotoProducts.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            GotoProducts.BorderRadius = 15;
            GotoProducts.CustomizableEdges = customizableEdges25;
            GotoProducts.DisabledState.BorderColor = Color.DarkGray;
            GotoProducts.DisabledState.CustomBorderColor = Color.DarkGray;
            GotoProducts.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            GotoProducts.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            GotoProducts.FillColor = Color.Gray;
            GotoProducts.Font = new Font("Segoe UI", 9F);
            GotoProducts.ForeColor = Color.White;
            GotoProducts.Location = new Point(662, 496);
            GotoProducts.Name = "GotoProducts";
            GotoProducts.ShadowDecoration.CustomizableEdges = customizableEdges26;
            GotoProducts.Size = new Size(135, 38);
            GotoProducts.TabIndex = 7;
            GotoProducts.Text = "Go to Products";
            GotoProducts.Click += GotoProducts_Click;
            // 
            // GotoSales
            // 
            GotoSales.BorderRadius = 15;
            GotoSales.CustomizableEdges = customizableEdges27;
            GotoSales.DisabledState.BorderColor = Color.DarkGray;
            GotoSales.DisabledState.CustomBorderColor = Color.DarkGray;
            GotoSales.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            GotoSales.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            GotoSales.FillColor = Color.Gray;
            GotoSales.Font = new Font("Segoe UI", 9F);
            GotoSales.ForeColor = Color.White;
            GotoSales.Location = new Point(23, 496);
            GotoSales.Name = "GotoSales";
            GotoSales.ShadowDecoration.CustomizableEdges = customizableEdges28;
            GotoSales.Size = new Size(135, 38);
            GotoSales.TabIndex = 8;
            GotoSales.Text = "Go to Sales";
            GotoSales.Click += GotoSales_Click;
            // 
            // InventoryWinForms
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(834, 542);
            Controls.Add(GotoSales);
            Controls.Add(GotoProducts);
            Controls.Add(guna2TextBox1);
            Controls.Add(AddStocks);
            Controls.Add(guna2DataGridView1);
            Controls.Add(lblOutOfStock);
            Controls.Add(lblLowStock);
            Controls.Add(lblTotalProducts);
            Controls.Add(label1);
            Name = "InventoryWinForms";
            Text = "InventoryWinForms";
            WindowState = FormWindowState.Maximized;
            Load += InventoryWinForms_Load;
            lblTotalProducts.ResumeLayout(false);
            lblTotalProducts.PerformLayout();
            lblLowStock.ResumeLayout(false);
            lblLowStock.PerformLayout();
            lblOutOfStock.ResumeLayout(false);
            lblOutOfStock.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)guna2DataGridView1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Guna.UI2.WinForms.Guna2CustomGradientPanel lblTotalProducts;
        private Guna.UI2.WinForms.Guna2HtmlLabel TotalProductTitle;
        private Guna.UI2.WinForms.Guna2CustomGradientPanel lblLowStock;
        private Guna.UI2.WinForms.Guna2CustomGradientPanel lblOutOfStock;
        private Guna.UI2.WinForms.Guna2HtmlLabel TotalProductLabel;
        private Guna.UI2.WinForms.Guna2HtmlLabel LowStockTitle;
        private Guna.UI2.WinForms.Guna2HtmlLabel OutOfStockTitle;
        private Guna.UI2.WinForms.Guna2HtmlLabel LowStockLabel;
        private Guna.UI2.WinForms.Guna2HtmlLabel OutOfStockLabel;
        private Guna.UI2.WinForms.Guna2DataGridView guna2DataGridView1;
        private Guna.UI2.WinForms.Guna2GradientButton AddStocks;
        private DataGridViewTextBoxColumn ProductNames;
        private DataGridViewTextBoxColumn Category;
        private DataGridViewTextBoxColumn Price;
        private DataGridViewTextBoxColumn Status;
        private DataGridViewTextBoxColumn Actions;
        private Guna.UI2.WinForms.Guna2TextBox guna2TextBox1;
        private Guna.UI2.WinForms.Guna2Button GotoProducts;
        private Guna.UI2.WinForms.Guna2Button GotoSales;
    }
}