namespace Larp.winforms.Inventory
{
    partial class ProductWinForms
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
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges3 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges4 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges5 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges6 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges7 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges8 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            label1 = new Label();
            SearchProducts = new Guna.UI2.WinForms.Guna2TextBox();
            AddProducts = new Guna.UI2.WinForms.Guna2GradientButton();
            guna2DataGridView1 = new Guna.UI2.WinForms.Guna2DataGridView();
            GotoSales = new Guna.UI2.WinForms.Guna2Button();
            GoToInventory = new Guna.UI2.WinForms.Guna2Button();
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
            label1.Size = new Size(211, 136);
            label1.TabIndex = 2;
            label1.Text = "Product";
            // 
            // SearchProducts
            // 
            SearchProducts.AutoSize = true;
            SearchProducts.BorderRadius = 10;
            SearchProducts.CustomizableEdges = customizableEdges1;
            SearchProducts.DefaultText = "";
            SearchProducts.DisabledState.BorderColor = Color.FromArgb(208, 208, 208);
            SearchProducts.DisabledState.FillColor = Color.FromArgb(226, 226, 226);
            SearchProducts.DisabledState.ForeColor = Color.FromArgb(138, 138, 138);
            SearchProducts.DisabledState.PlaceholderForeColor = Color.FromArgb(138, 138, 138);
            SearchProducts.FocusedState.BorderColor = Color.FromArgb(94, 148, 255);
            SearchProducts.Font = new Font("Segoe UI", 9F);
            SearchProducts.HoverState.BorderColor = Color.FromArgb(94, 148, 255);
            SearchProducts.Location = new Point(35, 107);
            SearchProducts.Margin = new Padding(3, 4, 3, 4);
            SearchProducts.Name = "SearchProducts";
            SearchProducts.PlaceholderText = "Search for products";
            SearchProducts.SelectedText = "";
            SearchProducts.ShadowDecoration.CustomizableEdges = customizableEdges2;
            SearchProducts.Size = new Size(286, 38);
            SearchProducts.TabIndex = 7;
            SearchProducts.TabStop = false;
            SearchProducts.WordWrap = false;
            SearchProducts.TextChanged += SearchProducts_TextChanged;
            // 
            // AddProducts
            // 
            AddProducts.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            AddProducts.BorderRadius = 10;
            AddProducts.CustomizableEdges = customizableEdges3;
            AddProducts.DisabledState.BorderColor = Color.DarkGray;
            AddProducts.DisabledState.CustomBorderColor = Color.DarkGray;
            AddProducts.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            AddProducts.DisabledState.FillColor2 = Color.FromArgb(169, 169, 169);
            AddProducts.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            AddProducts.Font = new Font("Segoe UI", 9F);
            AddProducts.ForeColor = Color.White;
            AddProducts.Location = new Point(648, 107);
            AddProducts.Name = "AddProducts";
            AddProducts.ShadowDecoration.CustomizableEdges = customizableEdges4;
            AddProducts.Size = new Size(138, 38);
            AddProducts.TabIndex = 8;
            AddProducts.Text = "Add Products +";
            AddProducts.Click += AddProducts_Click;
            // 
            // guna2DataGridView1
            // 
            dataGridViewCellStyle1.BackColor = Color.White;
            guna2DataGridView1.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            guna2DataGridView1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            guna2DataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            guna2DataGridView1.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = Color.FromArgb(100, 88, 255);
            dataGridViewCellStyle2.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle2.ForeColor = Color.White;
            dataGridViewCellStyle2.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.True;
            guna2DataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            guna2DataGridView1.ColumnHeadersHeight = 44;
            guna2DataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = Color.White;
            dataGridViewCellStyle3.Font = new Font("Segoe UI", 12F);
            dataGridViewCellStyle3.ForeColor = Color.FromArgb(71, 69, 94);
            dataGridViewCellStyle3.SelectionBackColor = Color.FromArgb(231, 229, 255);
            dataGridViewCellStyle3.SelectionForeColor = Color.FromArgb(71, 69, 94);
            dataGridViewCellStyle3.WrapMode = DataGridViewTriState.False;
            guna2DataGridView1.DefaultCellStyle = dataGridViewCellStyle3;
            guna2DataGridView1.GridColor = Color.FromArgb(231, 229, 255);
            guna2DataGridView1.Location = new Point(12, 161);
            guna2DataGridView1.Name = "guna2DataGridView1";
            guna2DataGridView1.RowHeadersVisible = false;
            guna2DataGridView1.RowHeadersWidth = 51;
            guna2DataGridView1.RowTemplate.Height = 30;
            guna2DataGridView1.Size = new Size(774, 213);
            guna2DataGridView1.TabIndex = 9;
            guna2DataGridView1.ThemeStyle.AlternatingRowsStyle.BackColor = Color.White;
            guna2DataGridView1.ThemeStyle.HeaderStyle.BorderStyle = DataGridViewHeaderBorderStyle.Single;
            guna2DataGridView1.ThemeStyle.HeaderStyle.Font = new Font("Segoe UI", 9F);
            guna2DataGridView1.ThemeStyle.HeaderStyle.Height = 44;
            guna2DataGridView1.ThemeStyle.RowsStyle.Font = new Font("Segoe UI", 12F);
            guna2DataGridView1.ThemeStyle.RowsStyle.Height = 30;
            // 
            // GotoSales
            // 
            GotoSales.BorderRadius = 15;
            GotoSales.CustomizableEdges = customizableEdges5;
            GotoSales.DisabledState.BorderColor = Color.DarkGray;
            GotoSales.DisabledState.CustomBorderColor = Color.DarkGray;
            GotoSales.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            GotoSales.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            GotoSales.FillColor = Color.Gray;
            GotoSales.Font = new Font("Segoe UI", 9F);
            GotoSales.ForeColor = Color.White;
            GotoSales.Location = new Point(12, 380);
            GotoSales.Name = "GotoSales";
            GotoSales.ShadowDecoration.CustomizableEdges = customizableEdges6;
            GotoSales.Size = new Size(135, 38);
            GotoSales.TabIndex = 10;
            GotoSales.Text = "Go to Sales";
            GotoSales.Click += GotoSales_Click;
            // 
            // GoToInventory
            // 
            GoToInventory.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            GoToInventory.BorderRadius = 15;
            GoToInventory.CustomizableEdges = customizableEdges7;
            GoToInventory.DisabledState.BorderColor = Color.DarkGray;
            GoToInventory.DisabledState.CustomBorderColor = Color.DarkGray;
            GoToInventory.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            GoToInventory.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            GoToInventory.FillColor = Color.Gray;
            GoToInventory.Font = new Font("Segoe UI", 9F);
            GoToInventory.ForeColor = Color.White;
            GoToInventory.Location = new Point(648, 380);
            GoToInventory.Name = "GoToInventory";
            GoToInventory.ShadowDecoration.CustomizableEdges = customizableEdges8;
            GoToInventory.Size = new Size(135, 38);
            GoToInventory.TabIndex = 11;
            GoToInventory.Text = "Go to Inventory";
            GoToInventory.Click += GotoInventory_Click;
            // 
            // ProductWinForms
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(GoToInventory);
            Controls.Add(GotoSales);
            Controls.Add(guna2DataGridView1);
            Controls.Add(AddProducts);
            Controls.Add(SearchProducts);
            Controls.Add(label1);
            Name = "ProductWinForms";
            Text = "v";
            WindowState = FormWindowState.Maximized;
            Load += ProductWinForms_Load;
            ((System.ComponentModel.ISupportInitialize)guna2DataGridView1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Guna.UI2.WinForms.Guna2TextBox SearchProducts;
        private Guna.UI2.WinForms.Guna2GradientButton AddProducts;
        private Guna.UI2.WinForms.Guna2DataGridView guna2DataGridView1;
        private Guna.UI2.WinForms.Guna2Button GotoSales;
        private Guna.UI2.WinForms.Guna2Button GoToInventory;
    }
}