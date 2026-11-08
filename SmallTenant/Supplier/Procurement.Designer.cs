namespace Larp.winforms.SmallTenant.Supplier
{
    partial class Procurement
    {
        private System.ComponentModel.IContainer components = null;

        private Guna.UI2.WinForms.Guna2Panel headerPanel;
        private Guna.UI2.WinForms.Guna2HtmlLabel titleLabel;
        private Guna.UI2.WinForms.Guna2HtmlLabel supplierLabel;
        private Guna.UI2.WinForms.Guna2ComboBox cmbSupplier;
        private Guna.UI2.WinForms.Guna2HtmlLabel purchaseDateLabel;
        private Guna.UI2.WinForms.Guna2DateTimePicker dtpPurchaseDate;

        private Guna.UI2.WinForms.Guna2Panel itemPanel;
        private Guna.UI2.WinForms.Guna2HtmlLabel productLabel;
        private Guna.UI2.WinForms.Guna2ComboBox cmbProduct;
        private Guna.UI2.WinForms.Guna2HtmlLabel quantityLabel;
        private Guna.UI2.WinForms.Guna2TextBox txtQuantity;
        private Guna.UI2.WinForms.Guna2HtmlLabel unitCostLabel;
        private Guna.UI2.WinForms.Guna2TextBox txtUnitCost;
        private Guna.UI2.WinForms.Guna2Button btnAddItem;

        private Guna.UI2.WinForms.Guna2DataGridView PurchaseGrid;

        private Guna.UI2.WinForms.Guna2HtmlLabel grandTotalTitleLabel;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblGrandTotal;

        private Guna.UI2.WinForms.Guna2Button btnRemoveItem;
        private Guna.UI2.WinForms.Guna2Button btnClear;
        private Guna.UI2.WinForms.Guna2Button btnSavePurchase;
        private Guna.UI2.WinForms.Guna2Button btnClose;

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
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges3 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges4 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges5 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges6 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges15 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges16 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges7 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges8 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges9 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges10 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges11 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges12 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges13 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges14 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges17 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges18 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges19 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges20 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges21 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges22 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges23 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges24 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges25 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            headerPanel = new Guna.UI2.WinForms.Guna2Panel();
            titleLabel = new Guna.UI2.WinForms.Guna2HtmlLabel();
            supplierLabel = new Guna.UI2.WinForms.Guna2HtmlLabel();
            cmbSupplier = new Guna.UI2.WinForms.Guna2ComboBox();
            purchaseDateLabel = new Guna.UI2.WinForms.Guna2HtmlLabel();
            dtpPurchaseDate = new Guna.UI2.WinForms.Guna2DateTimePicker();
            itemPanel = new Guna.UI2.WinForms.Guna2Panel();
            productLabel = new Guna.UI2.WinForms.Guna2HtmlLabel();
            cmbProduct = new Guna.UI2.WinForms.Guna2ComboBox();
            quantityLabel = new Guna.UI2.WinForms.Guna2HtmlLabel();
            txtQuantity = new Guna.UI2.WinForms.Guna2TextBox();
            unitCostLabel = new Guna.UI2.WinForms.Guna2HtmlLabel();
            txtUnitCost = new Guna.UI2.WinForms.Guna2TextBox();
            btnAddItem = new Guna.UI2.WinForms.Guna2Button();
            PurchaseGrid = new Guna.UI2.WinForms.Guna2DataGridView();
            grandTotalTitleLabel = new Guna.UI2.WinForms.Guna2HtmlLabel();
            lblGrandTotal = new Guna.UI2.WinForms.Guna2HtmlLabel();
            btnRemoveItem = new Guna.UI2.WinForms.Guna2Button();
            btnClear = new Guna.UI2.WinForms.Guna2Button();
            btnSavePurchase = new Guna.UI2.WinForms.Guna2Button();
            btnClose = new Guna.UI2.WinForms.Guna2Button();
            guna2ProgressIndicator1 = new Guna.UI2.WinForms.Guna2ProgressIndicator();
            headerPanel.SuspendLayout();
            itemPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)PurchaseGrid).BeginInit();
            SuspendLayout();
            // 
            // headerPanel
            // 
            headerPanel.BackColor = Color.FromArgb(28, 58, 42);
            headerPanel.Controls.Add(titleLabel);
            headerPanel.CustomizableEdges = customizableEdges1;
            headerPanel.Dock = DockStyle.Top;
            headerPanel.Location = new Point(0, 0);
            headerPanel.Name = "headerPanel";
            headerPanel.ShadowDecoration.CustomizableEdges = customizableEdges2;
            headerPanel.Size = new Size(1200, 78);
            headerPanel.TabIndex = 13;
            // 
            // titleLabel
            // 
            titleLabel.BackColor = Color.Transparent;
            titleLabel.Font = new Font("Segoe UI Semibold", 20F, FontStyle.Bold);
            titleLabel.ForeColor = Color.White;
            titleLabel.Location = new Point(30, 22);
            titleLabel.Name = "titleLabel";
            titleLabel.Size = new Size(197, 47);
            titleLabel.TabIndex = 0;
            titleLabel.Text = "Procurement";
            // 
            // supplierLabel
            // 
            supplierLabel.BackColor = Color.Transparent;
            supplierLabel.Font = new Font("Segoe UI Semibold", 10F);
            supplierLabel.Location = new Point(35, 105);
            supplierLabel.Name = "supplierLabel";
            supplierLabel.Size = new Size(65, 25);
            supplierLabel.TabIndex = 12;
            supplierLabel.Text = "Supplier";
            // 
            // cmbSupplier
            // 
            cmbSupplier.BackColor = Color.Transparent;
            cmbSupplier.BorderRadius = 8;
            cmbSupplier.CustomizableEdges = customizableEdges3;
            cmbSupplier.DrawMode = DrawMode.OwnerDrawFixed;
            cmbSupplier.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbSupplier.FocusedColor = Color.Empty;
            cmbSupplier.Font = new Font("Segoe UI", 10F);
            cmbSupplier.ForeColor = Color.FromArgb(68, 88, 112);
            cmbSupplier.ItemHeight = 30;
            cmbSupplier.Location = new Point(35, 130);
            cmbSupplier.Name = "cmbSupplier";
            cmbSupplier.ShadowDecoration.CustomizableEdges = customizableEdges4;
            cmbSupplier.Size = new Size(360, 36);
            cmbSupplier.TabIndex = 11;
            // 
            // purchaseDateLabel
            // 
            purchaseDateLabel.BackColor = Color.Transparent;
            purchaseDateLabel.Font = new Font("Segoe UI Semibold", 10F);
            purchaseDateLabel.Location = new Point(430, 105);
            purchaseDateLabel.Name = "purchaseDateLabel";
            purchaseDateLabel.Size = new Size(113, 25);
            purchaseDateLabel.TabIndex = 10;
            purchaseDateLabel.Text = "Purchase Date";
            // 
            // dtpPurchaseDate
            // 
            dtpPurchaseDate.BorderRadius = 8;
            dtpPurchaseDate.Checked = true;
            dtpPurchaseDate.CustomizableEdges = customizableEdges5;
            dtpPurchaseDate.Font = new Font("Segoe UI", 9F);
            dtpPurchaseDate.Format = DateTimePickerFormat.Short;
            dtpPurchaseDate.Location = new Point(430, 130);
            dtpPurchaseDate.MaxDate = new DateTime(9998, 12, 31, 0, 0, 0, 0);
            dtpPurchaseDate.MinDate = new DateTime(1753, 1, 1, 0, 0, 0, 0);
            dtpPurchaseDate.Name = "dtpPurchaseDate";
            dtpPurchaseDate.ShadowDecoration.CustomizableEdges = customizableEdges6;
            dtpPurchaseDate.Size = new Size(180, 36);
            dtpPurchaseDate.TabIndex = 9;
            dtpPurchaseDate.Value = new DateTime(2026, 9, 30, 8, 0, 49, 122);
            // 
            // itemPanel
            // 
            itemPanel.BorderColor = Color.FromArgb(225, 225, 225);
            itemPanel.BorderRadius = 10;
            itemPanel.BorderThickness = 1;
            itemPanel.Controls.Add(productLabel);
            itemPanel.Controls.Add(cmbProduct);
            itemPanel.Controls.Add(quantityLabel);
            itemPanel.Controls.Add(txtQuantity);
            itemPanel.Controls.Add(unitCostLabel);
            itemPanel.Controls.Add(txtUnitCost);
            itemPanel.Controls.Add(btnAddItem);
            itemPanel.CustomizableEdges = customizableEdges15;
            itemPanel.FillColor = Color.FromArgb(250, 250, 250);
            itemPanel.Location = new Point(35, 190);
            itemPanel.Name = "itemPanel";
            itemPanel.ShadowDecoration.CustomizableEdges = customizableEdges16;
            itemPanel.Size = new Size(1130, 130);
            itemPanel.TabIndex = 8;
            // 
            // productLabel
            // 
            productLabel.BackColor = Color.Transparent;
            productLabel.Font = new Font("Segoe UI Semibold", 9F);
            productLabel.Location = new Point(20, 15);
            productLabel.Name = "productLabel";
            productLabel.Size = new Size(57, 22);
            productLabel.TabIndex = 0;
            productLabel.Text = "Product";
            // 
            // cmbProduct
            // 
            cmbProduct.BackColor = Color.Transparent;
            cmbProduct.BorderRadius = 8;
            cmbProduct.CustomizableEdges = customizableEdges7;
            cmbProduct.DrawMode = DrawMode.OwnerDrawFixed;
            cmbProduct.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbProduct.FocusedColor = Color.Empty;
            cmbProduct.Font = new Font("Segoe UI", 10F);
            cmbProduct.ForeColor = Color.FromArgb(68, 88, 112);
            cmbProduct.ItemHeight = 30;
            cmbProduct.Location = new Point(20, 40);
            cmbProduct.Name = "cmbProduct";
            cmbProduct.ShadowDecoration.CustomizableEdges = customizableEdges8;
            cmbProduct.Size = new Size(390, 36);
            cmbProduct.TabIndex = 1;
            // 
            // quantityLabel
            // 
            quantityLabel.BackColor = Color.Transparent;
            quantityLabel.Font = new Font("Segoe UI Semibold", 9F);
            quantityLabel.Location = new Point(435, 15);
            quantityLabel.Name = "quantityLabel";
            quantityLabel.Size = new Size(62, 22);
            quantityLabel.TabIndex = 2;
            quantityLabel.Text = "Quantity";
            // 
            // txtQuantity
            // 
            txtQuantity.BorderRadius = 8;
            txtQuantity.CustomizableEdges = customizableEdges9;
            txtQuantity.DefaultText = "";
            txtQuantity.Font = new Font("Segoe UI", 10F);
            txtQuantity.Location = new Point(435, 40);
            txtQuantity.Margin = new Padding(3, 4, 3, 4);
            txtQuantity.Name = "txtQuantity";
            txtQuantity.PlaceholderText = "0";
            txtQuantity.SelectedText = "";
            txtQuantity.ShadowDecoration.CustomizableEdges = customizableEdges10;
            txtQuantity.Size = new Size(150, 36);
            txtQuantity.TabIndex = 3;
            // 
            // unitCostLabel
            // 
            unitCostLabel.BackColor = Color.Transparent;
            unitCostLabel.Font = new Font("Segoe UI Semibold", 9F);
            unitCostLabel.Location = new Point(610, 15);
            unitCostLabel.Name = "unitCostLabel";
            unitCostLabel.Size = new Size(65, 22);
            unitCostLabel.TabIndex = 4;
            unitCostLabel.Text = "Unit Cost";
            // 
            // txtUnitCost
            // 
            txtUnitCost.BorderRadius = 8;
            txtUnitCost.CustomizableEdges = customizableEdges11;
            txtUnitCost.DefaultText = "";
            txtUnitCost.Font = new Font("Segoe UI", 10F);
            txtUnitCost.Location = new Point(610, 40);
            txtUnitCost.Margin = new Padding(3, 4, 3, 4);
            txtUnitCost.Name = "txtUnitCost";
            txtUnitCost.PlaceholderText = "0.00";
            txtUnitCost.SelectedText = "";
            txtUnitCost.ShadowDecoration.CustomizableEdges = customizableEdges12;
            txtUnitCost.Size = new Size(170, 36);
            txtUnitCost.TabIndex = 5;
            // 
            // btnAddItem
            // 
            btnAddItem.BorderRadius = 8;
            btnAddItem.CustomizableEdges = customizableEdges13;
            btnAddItem.FillColor = Color.FromArgb(46, 107, 69);
            btnAddItem.Font = new Font("Segoe UI Semibold", 10F);
            btnAddItem.ForeColor = Color.White;
            btnAddItem.Location = new Point(820, 40);
            btnAddItem.Name = "btnAddItem";
            btnAddItem.ShadowDecoration.CustomizableEdges = customizableEdges14;
            btnAddItem.Size = new Size(140, 36);
            btnAddItem.TabIndex = 6;
            btnAddItem.Text = "Add Item";
            // 
            // PurchaseGrid
            // 
            PurchaseGrid.AllowUserToAddRows = false;
            PurchaseGrid.AllowUserToDeleteRows = false;
            PurchaseGrid.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = Color.White;
            PurchaseGrid.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = Color.FromArgb(28, 58, 42);
            dataGridViewCellStyle2.Font = new Font("Segoe UI Semibold", 9F);
            dataGridViewCellStyle2.ForeColor = Color.White;
            dataGridViewCellStyle2.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.True;
            PurchaseGrid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            PurchaseGrid.ColumnHeadersHeight = 40;
            PurchaseGrid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = Color.White;
            dataGridViewCellStyle3.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle3.ForeColor = Color.FromArgb(71, 69, 94);
            dataGridViewCellStyle3.SelectionBackColor = Color.FromArgb(231, 229, 255);
            dataGridViewCellStyle3.SelectionForeColor = Color.FromArgb(71, 69, 94);
            dataGridViewCellStyle3.WrapMode = DataGridViewTriState.False;
            PurchaseGrid.DefaultCellStyle = dataGridViewCellStyle3;
            PurchaseGrid.GridColor = Color.FromArgb(231, 229, 255);
            PurchaseGrid.Location = new Point(35, 345);
            PurchaseGrid.Name = "PurchaseGrid";
            PurchaseGrid.ReadOnly = true;
            PurchaseGrid.RowHeadersVisible = false;
            PurchaseGrid.RowHeadersWidth = 51;
            PurchaseGrid.RowTemplate.Height = 36;
            PurchaseGrid.Size = new Size(1130, 250);
            PurchaseGrid.TabIndex = 7;
            PurchaseGrid.ThemeStyle.AlternatingRowsStyle.BackColor = Color.White;
            PurchaseGrid.ThemeStyle.HeaderStyle.BackColor = Color.FromArgb(28, 58, 42);
            PurchaseGrid.ThemeStyle.HeaderStyle.Font = new Font("Segoe UI Semibold", 9F);
            PurchaseGrid.ThemeStyle.HeaderStyle.Height = 40;
            PurchaseGrid.ThemeStyle.ReadOnly = true;
            PurchaseGrid.ThemeStyle.RowsStyle.Font = new Font("Segoe UI", 9F);
            PurchaseGrid.ThemeStyle.RowsStyle.Height = 36;
            // 
            // grandTotalTitleLabel
            // 
            grandTotalTitleLabel.BackColor = Color.Transparent;
            grandTotalTitleLabel.Font = new Font("Segoe UI Semibold", 12F);
            grandTotalTitleLabel.Location = new Point(865, 615);
            grandTotalTitleLabel.Name = "grandTotalTitleLabel";
            grandTotalTitleLabel.Size = new Size(114, 30);
            grandTotalTitleLabel.TabIndex = 4;
            grandTotalTitleLabel.Text = "Grand Total:";
            // 
            // lblGrandTotal
            // 
            lblGrandTotal.BackColor = Color.Transparent;
            lblGrandTotal.Font = new Font("Segoe UI Semibold", 16F, FontStyle.Bold);
            lblGrandTotal.ForeColor = Color.FromArgb(46, 107, 69);
            lblGrandTotal.Location = new Point(990, 610);
            lblGrandTotal.Name = "lblGrandTotal";
            lblGrandTotal.Size = new Size(72, 39);
            lblGrandTotal.TabIndex = 5;
            lblGrandTotal.Text = "₱0.00";
            // 
            // btnRemoveItem
            // 
            btnRemoveItem.BorderRadius = 8;
            btnRemoveItem.CustomizableEdges = customizableEdges17;
            btnRemoveItem.FillColor = Color.FromArgb(210, 75, 70);
            btnRemoveItem.Font = new Font("Segoe UI Semibold", 9F);
            btnRemoveItem.ForeColor = Color.White;
            btnRemoveItem.Location = new Point(35, 615);
            btnRemoveItem.Name = "btnRemoveItem";
            btnRemoveItem.ShadowDecoration.CustomizableEdges = customizableEdges18;
            btnRemoveItem.Size = new Size(125, 38);
            btnRemoveItem.TabIndex = 6;
            btnRemoveItem.Text = "Remove Item";
            // 
            // btnClear
            // 
            btnClear.BorderRadius = 8;
            btnClear.CustomizableEdges = customizableEdges19;
            btnClear.FillColor = Color.FromArgb(110, 110, 110);
            btnClear.Font = new Font("Segoe UI Semibold", 9F);
            btnClear.ForeColor = Color.White;
            btnClear.Location = new Point(675, 680);
            btnClear.Name = "btnClear";
            btnClear.ShadowDecoration.CustomizableEdges = customizableEdges20;
            btnClear.Size = new Size(115, 42);
            btnClear.TabIndex = 3;
            btnClear.Text = "Clear";
            // 
            // btnSavePurchase
            // 
            btnSavePurchase.BorderRadius = 8;
            btnSavePurchase.CustomizableEdges = customizableEdges21;
            btnSavePurchase.FillColor = Color.FromArgb(46, 107, 69);
            btnSavePurchase.Font = new Font("Segoe UI Semibold", 10F);
            btnSavePurchase.ForeColor = Color.White;
            btnSavePurchase.Location = new Point(805, 680);
            btnSavePurchase.Name = "btnSavePurchase";
            btnSavePurchase.ShadowDecoration.CustomizableEdges = customizableEdges22;
            btnSavePurchase.Size = new Size(170, 42);
            btnSavePurchase.TabIndex = 2;
            btnSavePurchase.Text = "Save Purchase";
            // 
            // btnClose
            // 
            btnClose.BorderRadius = 8;
            btnClose.CustomizableEdges = customizableEdges23;
            btnClose.FillColor = Color.FromArgb(80, 80, 80);
            btnClose.Font = new Font("Segoe UI Semibold", 10F);
            btnClose.ForeColor = Color.White;
            btnClose.Location = new Point(990, 680);
            btnClose.Name = "btnClose";
            btnClose.ShadowDecoration.CustomizableEdges = customizableEdges24;
            btnClose.Size = new Size(115, 42);
            btnClose.TabIndex = 1;
            btnClose.Text = "Close";
            // 
            // guna2ProgressIndicator1
            // 
            guna2ProgressIndicator1.BackColor = Color.Transparent;
            guna2ProgressIndicator1.Location = new Point(605, 313);
            guna2ProgressIndicator1.Name = "guna2ProgressIndicator1";
            guna2ProgressIndicator1.ProgressColor = Color.Blue;
            guna2ProgressIndicator1.ShadowDecoration.CustomizableEdges = customizableEdges25;
            guna2ProgressIndicator1.Size = new Size(40, 40);
            guna2ProgressIndicator1.TabIndex = 0;
            guna2ProgressIndicator1.Visible = false;
            // 
            // Procurement
            // 
            ClientSize = new Size(1200, 750);
            Controls.Add(guna2ProgressIndicator1);
            Controls.Add(btnClose);
            Controls.Add(btnSavePurchase);
            Controls.Add(btnClear);
            Controls.Add(grandTotalTitleLabel);
            Controls.Add(lblGrandTotal);
            Controls.Add(btnRemoveItem);
            Controls.Add(PurchaseGrid);
            Controls.Add(itemPanel);
            Controls.Add(dtpPurchaseDate);
            Controls.Add(purchaseDateLabel);
            Controls.Add(cmbSupplier);
            Controls.Add(supplierLabel);
            Controls.Add(headerPanel);
            FormBorderStyle = FormBorderStyle.None;
            Name = "Procurement";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Procurement";
            headerPanel.ResumeLayout(false);
            headerPanel.PerformLayout();
            itemPanel.ResumeLayout(false);
            itemPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)PurchaseGrid).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }
    }
}