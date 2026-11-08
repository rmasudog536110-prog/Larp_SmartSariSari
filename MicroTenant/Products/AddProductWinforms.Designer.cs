namespace Larp.winforms.MicroTenant.Products
{
    partial class AddProductWinForms
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
            components = new System.ComponentModel.Container();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges29 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges30 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges27 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges28 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges25 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges26 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges23 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges24 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges21 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges22 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            guna2BorderlessForm1 = new Guna.UI2.WinForms.Guna2BorderlessForm(components);
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            txtProductCode = new Guna.UI2.WinForms.Guna2TextBox();
            txtProductName = new Guna.UI2.WinForms.Guna2TextBox();
            txtUnitPrice = new Guna.UI2.WinForms.Guna2TextBox();
            chkIsActive = new Guna.UI2.WinForms.Guna2CheckBox();
            btnSave = new Guna.UI2.WinForms.Guna2Button();
            btnCancel = new Guna.UI2.WinForms.Guna2Button();
            SuspendLayout();
            // 
            // guna2BorderlessForm1
            // 
            guna2BorderlessForm1.ContainerControl = this;
            guna2BorderlessForm1.DockIndicatorTransparencyValue = 0.6D;
            guna2BorderlessForm1.TransparentWhileDrag = true;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI Symbol", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.Location = new Point(41, 49);
            label1.Name = "label1";
            label1.Size = new Size(165, 28);
            label1.TabIndex = 0;
            label1.Text = "Product Bar Code";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI Symbol", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label2.Location = new Point(41, 127);
            label2.Name = "label2";
            label2.Size = new Size(138, 28);
            label2.TabIndex = 0;
            label2.Text = "Product Name";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI Symbol", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label3.Location = new Point(41, 205);
            label3.Name = "label3";
            label3.Size = new Size(54, 28);
            label3.TabIndex = 1;
            label3.Text = "Price";
            // 
            // txtProductCode
            // 
            txtProductCode.BorderRadius = 10;
            txtProductCode.BorderStyle = System.Drawing.Drawing2D.DashStyle.Dash;
            txtProductCode.CustomizableEdges = customizableEdges29;
            txtProductCode.DefaultText = "";
            txtProductCode.DisabledState.BorderColor = Color.FromArgb(208, 208, 208);
            txtProductCode.DisabledState.FillColor = Color.FromArgb(226, 226, 226);
            txtProductCode.DisabledState.ForeColor = Color.FromArgb(138, 138, 138);
            txtProductCode.DisabledState.PlaceholderForeColor = Color.FromArgb(138, 138, 138);
            txtProductCode.FocusedState.BorderColor = Color.FromArgb(94, 148, 255);
            txtProductCode.Font = new Font("Segoe UI", 9F);
            txtProductCode.HoverState.BorderColor = Color.FromArgb(94, 148, 255);
            txtProductCode.Location = new Point(41, 81);
            txtProductCode.Margin = new Padding(3, 4, 3, 4);
            txtProductCode.Name = "txtProductCode";
            txtProductCode.PlaceholderText = "";
            txtProductCode.ReadOnly = true;
            txtProductCode.SelectedText = "";
            txtProductCode.ShadowDecoration.CustomizableEdges = customizableEdges30;
            txtProductCode.Size = new Size(304, 42);
            txtProductCode.TabIndex = 2;
            // 
            // txtProductName
            // 
            txtProductName.BorderRadius = 10;
            txtProductName.BorderStyle = System.Drawing.Drawing2D.DashStyle.Dash;
            txtProductName.CustomizableEdges = customizableEdges27;
            txtProductName.DefaultText = "Oishi Cracklings";
            txtProductName.DisabledState.BorderColor = Color.FromArgb(208, 208, 208);
            txtProductName.DisabledState.FillColor = Color.FromArgb(226, 226, 226);
            txtProductName.DisabledState.ForeColor = Color.FromArgb(138, 138, 138);
            txtProductName.DisabledState.PlaceholderForeColor = Color.FromArgb(138, 138, 138);
            txtProductName.FocusedState.BorderColor = Color.FromArgb(94, 148, 255);
            txtProductName.Font = new Font("Segoe UI", 9F);
            txtProductName.HoverState.BorderColor = Color.FromArgb(94, 148, 255);
            txtProductName.Location = new Point(41, 159);
            txtProductName.Margin = new Padding(3, 4, 3, 4);
            txtProductName.Name = "txtProductName";
            txtProductName.PlaceholderText = "";
            txtProductName.SelectedText = "";
            txtProductName.ShadowDecoration.CustomizableEdges = customizableEdges28;
            txtProductName.Size = new Size(304, 42);
            txtProductName.TabIndex = 3;
            // 
            // txtUnitPrice
            // 
            txtUnitPrice.BorderRadius = 10;
            txtUnitPrice.BorderStyle = System.Drawing.Drawing2D.DashStyle.Dash;
            txtUnitPrice.CustomizableEdges = customizableEdges25;
            txtUnitPrice.DefaultText = "30.00";
            txtUnitPrice.DisabledState.BorderColor = Color.FromArgb(208, 208, 208);
            txtUnitPrice.DisabledState.FillColor = Color.FromArgb(226, 226, 226);
            txtUnitPrice.DisabledState.ForeColor = Color.FromArgb(138, 138, 138);
            txtUnitPrice.DisabledState.PlaceholderForeColor = Color.FromArgb(138, 138, 138);
            txtUnitPrice.FocusedState.BorderColor = Color.FromArgb(94, 148, 255);
            txtUnitPrice.Font = new Font("Segoe UI", 9F);
            txtUnitPrice.HoverState.BorderColor = Color.FromArgb(94, 148, 255);
            txtUnitPrice.Location = new Point(41, 237);
            txtUnitPrice.Margin = new Padding(3, 4, 3, 4);
            txtUnitPrice.Name = "txtUnitPrice";
            txtUnitPrice.PlaceholderText = "";
            txtUnitPrice.SelectedText = "";
            txtUnitPrice.ShadowDecoration.CustomizableEdges = customizableEdges26;
            txtUnitPrice.Size = new Size(304, 42);
            txtUnitPrice.TabIndex = 4;
            // 
            // chkIsActive
            // 
            chkIsActive.AutoSize = true;
            chkIsActive.BackColor = SystemColors.Control;
            chkIsActive.CheckedState.BorderColor = Color.FromArgb(94, 148, 255);
            chkIsActive.CheckedState.BorderRadius = 0;
            chkIsActive.CheckedState.BorderThickness = 0;
            chkIsActive.CheckedState.FillColor = Color.FromArgb(94, 148, 255);
            chkIsActive.Location = new Point(41, 298);
            chkIsActive.Name = "chkIsActive";
            chkIsActive.Size = new Size(148, 24);
            chkIsActive.TabIndex = 5;
            chkIsActive.Text = "Product Available";
            chkIsActive.UncheckedState.BorderColor = Color.FromArgb(125, 137, 149);
            chkIsActive.UncheckedState.BorderRadius = 0;
            chkIsActive.UncheckedState.BorderThickness = 0;
            chkIsActive.UncheckedState.FillColor = Color.FromArgb(125, 137, 149);
            chkIsActive.UseVisualStyleBackColor = false;
            // 
            // btnSave
            // 
            btnSave.BorderRadius = 12;
            btnSave.CustomizableEdges = customizableEdges23;
            btnSave.DisabledState.BorderColor = Color.DarkGray;
            btnSave.DisabledState.CustomBorderColor = Color.DarkGray;
            btnSave.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            btnSave.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            btnSave.Font = new Font("Segoe UI", 9F);
            btnSave.ForeColor = Color.White;
            btnSave.Location = new Point(241, 307);
            btnSave.Name = "btnSave";
            btnSave.ShadowDecoration.CustomizableEdges = customizableEdges24;
            btnSave.Size = new Size(104, 31);
            btnSave.TabIndex = 6;
            btnSave.Text = "Save";
            btnSave.Click += btnSave_Click;
            // 
            // btnCancel
            // 
            btnCancel.BorderRadius = 12;
            btnCancel.CustomizableEdges = customizableEdges21;
            btnCancel.DisabledState.BorderColor = Color.DarkGray;
            btnCancel.DisabledState.CustomBorderColor = Color.DarkGray;
            btnCancel.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            btnCancel.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            btnCancel.FillColor = SystemColors.Control;
            btnCancel.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnCancel.ForeColor = Color.Black;
            btnCancel.Location = new Point(337, 12);
            btnCancel.Name = "btnCancel";
            btnCancel.PressedColor = Color.LightGray;
            btnCancel.ShadowDecoration.CustomizableEdges = customizableEdges22;
            btnCancel.Size = new Size(35, 31);
            btnCancel.TabIndex = 7;
            btnCancel.Text = "x";
            btnCancel.Click += btnCancel_Click_1;
            // 
            // AddProductWinForms
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(384, 361);
            Controls.Add(btnCancel);
            Controls.Add(btnSave);
            Controls.Add(chkIsActive);
            Controls.Add(txtUnitPrice);
            Controls.Add(txtProductName);
            Controls.Add(txtProductCode);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            FormBorderStyle = FormBorderStyle.None;
            Name = "AddProductWinForms";
            Text = "AddProductWinForms";
            Load += AddProductWinForms_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Guna.UI2.WinForms.Guna2BorderlessForm guna2BorderlessForm1;
        private Label label3;
        private Label label2;
        private Label label1;
        private Guna.UI2.WinForms.Guna2TextBox txtProductCode;
        private Guna.UI2.WinForms.Guna2CheckBox chkIsActive;
        private Guna.UI2.WinForms.Guna2TextBox txtUnitPrice;
        private Guna.UI2.WinForms.Guna2TextBox txtProductName;
        private Guna.UI2.WinForms.Guna2Button btnSave;
        private Guna.UI2.WinForms.Guna2Button btnCancel;
        private Guna.UI2.WinForms.Guna2CheckBox chkIsVatable = null!;
    }
}