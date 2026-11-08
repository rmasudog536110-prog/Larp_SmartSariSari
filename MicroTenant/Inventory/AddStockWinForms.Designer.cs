namespace Larp.winforms.MicroTenant.Inventory
{
    partial class AddStockWinForms
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
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges5 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges6 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges7 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges8 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges9 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges10 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Title = new Guna.UI2.WinForms.Guna2HtmlLabel();
            lblProduct = new Guna.UI2.WinForms.Guna2HtmlLabel();
            cmbProduct = new Guna.UI2.WinForms.Guna2ComboBox();
            lblCurrentStockTitle = new Guna.UI2.WinForms.Guna2HtmlLabel();
            txtCurrentStock = new Guna.UI2.WinForms.Guna2TextBox();
            lblQuantity = new Guna.UI2.WinForms.Guna2HtmlLabel();
            txtQuantity = new Guna.UI2.WinForms.Guna2TextBox();
            btnCancel = new Guna.UI2.WinForms.Guna2Button();
            btnAddStock = new Guna.UI2.WinForms.Guna2Button();
            SuspendLayout();
            // 
            // Title
            // 
            Title.BackColor = Color.Transparent;
            Title.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            Title.Location = new Point(301, 31);
            Title.Name = "Title";
            Title.Size = new Size(113, 33);
            Title.TabIndex = 0;
            Title.Text = "Add Stock";
            // 
            // lblProduct
            // 
            lblProduct.BackColor = Color.Transparent;
            lblProduct.Location = new Point(72, 103);
            lblProduct.Name = "lblProduct";
            lblProduct.Size = new Size(54, 22);
            lblProduct.TabIndex = 1;
            lblProduct.Text = "Product";
            // 
            // cmbProduct
            // 
            cmbProduct.BackColor = Color.Transparent;
            cmbProduct.CustomizableEdges = customizableEdges1;
            cmbProduct.DrawMode = DrawMode.OwnerDrawFixed;
            cmbProduct.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbProduct.FocusedColor = Color.FromArgb(94, 148, 255);
            cmbProduct.FocusedState.BorderColor = Color.FromArgb(94, 148, 255);
            cmbProduct.Font = new Font("Segoe UI", 10F);
            cmbProduct.ForeColor = Color.FromArgb(68, 88, 112);
            cmbProduct.ItemHeight = 30;
            cmbProduct.Location = new Point(72, 145);
            cmbProduct.Name = "cmbProduct";
            cmbProduct.ShadowDecoration.CustomizableEdges = customizableEdges2;
            cmbProduct.Size = new Size(584, 36);
            cmbProduct.TabIndex = 2;
            // 
            // lblCurrentStockTitle
            // 
            lblCurrentStockTitle.BackColor = Color.Transparent;
            lblCurrentStockTitle.Location = new Point(72, 214);
            lblCurrentStockTitle.Name = "lblCurrentStockTitle";
            lblCurrentStockTitle.Size = new Size(91, 22);
            lblCurrentStockTitle.TabIndex = 3;
            lblCurrentStockTitle.Text = "Current Stock";
            // 
            // txtCurrentStock
            // 
            txtCurrentStock.CustomizableEdges = customizableEdges3;
            txtCurrentStock.DefaultText = "";
            txtCurrentStock.DisabledState.BorderColor = Color.FromArgb(208, 208, 208);
            txtCurrentStock.DisabledState.FillColor = Color.FromArgb(226, 226, 226);
            txtCurrentStock.DisabledState.ForeColor = Color.FromArgb(138, 138, 138);
            txtCurrentStock.DisabledState.PlaceholderForeColor = Color.FromArgb(138, 138, 138);
            txtCurrentStock.FocusedState.BorderColor = Color.FromArgb(94, 148, 255);
            txtCurrentStock.Font = new Font("Segoe UI", 9F);
            txtCurrentStock.HoverState.BorderColor = Color.FromArgb(94, 148, 255);
            txtCurrentStock.Location = new Point(72, 243);
            txtCurrentStock.Margin = new Padding(3, 4, 3, 4);
            txtCurrentStock.Name = "txtCurrentStock";
            txtCurrentStock.PlaceholderText = "";
            txtCurrentStock.SelectedText = "";
            txtCurrentStock.ShadowDecoration.CustomizableEdges = customizableEdges4;
            txtCurrentStock.Size = new Size(584, 34);
            txtCurrentStock.TabIndex = 4;
            // 
            // lblQuantity
            // 
            lblQuantity.BackColor = Color.Transparent;
            lblQuantity.Location = new Point(72, 296);
            lblQuantity.Name = "lblQuantity";
            lblQuantity.Size = new Size(59, 22);
            lblQuantity.TabIndex = 5;
            lblQuantity.Text = "Quantity";
            // 
            // txtQuantity
            // 
            txtQuantity.CustomizableEdges = customizableEdges5;
            txtQuantity.DefaultText = "";
            txtQuantity.DisabledState.BorderColor = Color.FromArgb(208, 208, 208);
            txtQuantity.DisabledState.FillColor = Color.FromArgb(226, 226, 226);
            txtQuantity.DisabledState.ForeColor = Color.FromArgb(138, 138, 138);
            txtQuantity.DisabledState.PlaceholderForeColor = Color.FromArgb(138, 138, 138);
            txtQuantity.FocusedState.BorderColor = Color.FromArgb(94, 148, 255);
            txtQuantity.Font = new Font("Segoe UI", 9F);
            txtQuantity.HoverState.BorderColor = Color.FromArgb(94, 148, 255);
            txtQuantity.Location = new Point(72, 325);
            txtQuantity.Margin = new Padding(3, 4, 3, 4);
            txtQuantity.Name = "txtQuantity";
            txtQuantity.PlaceholderText = "";
            txtQuantity.SelectedText = "";
            txtQuantity.ShadowDecoration.CustomizableEdges = customizableEdges6;
            txtQuantity.Size = new Size(584, 34);
            txtQuantity.TabIndex = 6;
            // 
            // btnCancel
            // 
            btnCancel.CustomizableEdges = customizableEdges7;
            btnCancel.DisabledState.BorderColor = Color.DarkGray;
            btnCancel.DisabledState.CustomBorderColor = Color.DarkGray;
            btnCancel.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            btnCancel.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            btnCancel.Font = new Font("Segoe UI", 9F);
            btnCancel.ForeColor = Color.White;
            btnCancel.Location = new Point(400, 382);
            btnCancel.Name = "btnCancel";
            btnCancel.ShadowDecoration.CustomizableEdges = customizableEdges8;
            btnCancel.Size = new Size(126, 45);
            btnCancel.TabIndex = 7;
            btnCancel.Text = "Cancel";
            btnCancel.Click += btnCancel_Click;
            // 
            // btnAddStock
            // 
            btnAddStock.CustomizableEdges = customizableEdges9;
            btnAddStock.DisabledState.BorderColor = Color.DarkGray;
            btnAddStock.DisabledState.CustomBorderColor = Color.DarkGray;
            btnAddStock.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            btnAddStock.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            btnAddStock.Font = new Font("Segoe UI", 9F);
            btnAddStock.ForeColor = Color.White;
            btnAddStock.Location = new Point(532, 382);
            btnAddStock.Name = "btnAddStock";
            btnAddStock.ShadowDecoration.CustomizableEdges = customizableEdges10;
            btnAddStock.Size = new Size(126, 45);
            btnAddStock.TabIndex = 8;
            btnAddStock.Text = "Add Stock";
            btnAddStock.Click += btnAddStock_Click;
            // 
            // AddStockWinForms
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(btnAddStock);
            Controls.Add(btnCancel);
            Controls.Add(txtQuantity);
            Controls.Add(lblQuantity);
            Controls.Add(txtCurrentStock);
            Controls.Add(lblCurrentStockTitle);
            Controls.Add(cmbProduct);
            Controls.Add(lblProduct);
            Controls.Add(Title);
            Name = "AddStockWinForms";
            Text = "AddStockWinForms";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Guna.UI2.WinForms.Guna2HtmlLabel Title;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblProduct;
        private Guna.UI2.WinForms.Guna2ComboBox cmbProduct;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblCurrentStockTitle;
        private Guna.UI2.WinForms.Guna2TextBox txtCurrentStock;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblQuantity;
        private Guna.UI2.WinForms.Guna2TextBox txtQuantity;
        private Guna.UI2.WinForms.Guna2Button btnCancel;
        private Guna.UI2.WinForms.Guna2Button btnAddStock;
    }
}