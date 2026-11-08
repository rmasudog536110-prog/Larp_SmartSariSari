namespace Larp.winforms.SmallTenant.Supplier
{
    partial class EditSupplierWinforms
    {
        private System.ComponentModel.IContainer components = null;

        private Guna.UI2.WinForms.Guna2TextBox txtSupplierCode;
        private Guna.UI2.WinForms.Guna2TextBox txtSupplierName;
        private Guna.UI2.WinForms.Guna2TextBox txtContactPerson;
        private Guna.UI2.WinForms.Guna2TextBox txtContactNumber;
        private Guna.UI2.WinForms.Guna2TextBox txtEmailAddress;
        private Guna.UI2.WinForms.Guna2TextBox txtAddress;
        private Guna.UI2.WinForms.Guna2CheckBox chkIsActive;
        private Guna.UI2.WinForms.Guna2Button btnSave;
        private Guna.UI2.WinForms.Guna2Button btnCancel;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblSupplierCode;
        private System.Windows.Forms.Label lblSupplierName;
        private System.Windows.Forms.Label lblContactPerson;
        private System.Windows.Forms.Label lblContactNumber;
        private System.Windows.Forms.Label lblEmail;
        private System.Windows.Forms.Label lblAddress;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null)
                components.Dispose();

            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();

            lblTitle = new System.Windows.Forms.Label();
            lblSupplierCode = new System.Windows.Forms.Label();
            lblSupplierName = new System.Windows.Forms.Label();
            lblContactPerson = new System.Windows.Forms.Label();
            lblContactNumber = new System.Windows.Forms.Label();
            lblEmail = new System.Windows.Forms.Label();
            lblAddress = new System.Windows.Forms.Label();

            txtSupplierCode = new Guna.UI2.WinForms.Guna2TextBox();
            txtSupplierName = new Guna.UI2.WinForms.Guna2TextBox();
            txtContactPerson = new Guna.UI2.WinForms.Guna2TextBox();
            txtContactNumber = new Guna.UI2.WinForms.Guna2TextBox();
            txtEmailAddress = new Guna.UI2.WinForms.Guna2TextBox();
            txtAddress = new Guna.UI2.WinForms.Guna2TextBox();

            chkIsActive = new Guna.UI2.WinForms.Guna2CheckBox();
            btnSave = new Guna.UI2.WinForms.Guna2Button();
            btnCancel = new Guna.UI2.WinForms.Guna2Button();

            SuspendLayout();

            Text = "Edit Supplier";
            StartPosition = FormStartPosition.CenterParent;
            ClientSize = new System.Drawing.Size(600, 620);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;

            lblTitle.Text = "Edit Supplier";
            lblTitle.Font = new System.Drawing.Font(
                "Segoe UI",
                18F,
                System.Drawing.FontStyle.Bold);
            lblTitle.AutoSize = true;
            lblTitle.Location = new System.Drawing.Point(30, 25);

            lblSupplierCode.Text = "Supplier Code";
            lblSupplierCode.AutoSize = true;
            lblSupplierCode.Location = new System.Drawing.Point(30, 80);

            txtSupplierCode.Location = new System.Drawing.Point(30, 102);
            txtSupplierCode.Size = new System.Drawing.Size(540, 40);
            txtSupplierCode.PlaceholderText = "Enter supplier code";

            lblSupplierName.Text = "Supplier Name";
            lblSupplierName.AutoSize = true;
            lblSupplierName.Location = new System.Drawing.Point(30, 155);

            txtSupplierName.Location = new System.Drawing.Point(30, 177);
            txtSupplierName.Size = new System.Drawing.Size(540, 40);
            txtSupplierName.PlaceholderText = "Enter supplier name";

            lblContactPerson.Text = "Contact Person";
            lblContactPerson.AutoSize = true;
            lblContactPerson.Location = new System.Drawing.Point(30, 230);

            txtContactPerson.Location = new System.Drawing.Point(30, 252);
            txtContactPerson.Size = new System.Drawing.Size(540, 40);
            txtContactPerson.PlaceholderText = "Optional";

            lblContactNumber.Text = "Contact Number";
            lblContactNumber.AutoSize = true;
            lblContactNumber.Location = new System.Drawing.Point(30, 305);

            txtContactNumber.Location = new System.Drawing.Point(30, 327);
            txtContactNumber.Size = new System.Drawing.Size(540, 40);
            txtContactNumber.PlaceholderText = "Optional";

            lblEmail.Text = "Email Address";
            lblEmail.AutoSize = true;
            lblEmail.Location = new System.Drawing.Point(30, 380);

            txtEmailAddress.Location = new System.Drawing.Point(30, 402);
            txtEmailAddress.Size = new System.Drawing.Size(540, 40);
            txtEmailAddress.PlaceholderText = "Optional";

            lblAddress.Text = "Address";
            lblAddress.AutoSize = true;
            lblAddress.Location = new System.Drawing.Point(30, 455);

            txtAddress.Location = new System.Drawing.Point(30, 477);
            txtAddress.Size = new System.Drawing.Size(540, 40);
            txtAddress.PlaceholderText = "Optional";

            chkIsActive.Text = "Active";
            chkIsActive.Checked = true;
            chkIsActive.AutoSize = true;
            chkIsActive.Location = new System.Drawing.Point(30, 535);

            btnCancel.Text = "Cancel";
            btnCancel.Location = new System.Drawing.Point(350, 570);
            btnCancel.Size = new System.Drawing.Size(105, 40);
            btnCancel.Click += btnCancel_Click;

            btnSave.Text = "Save";
            btnSave.Location = new System.Drawing.Point(465, 570);
            btnSave.Size = new System.Drawing.Size(105, 40);
            btnSave.Click += btnSave_Click;

            Controls.Add(lblTitle);
            Controls.Add(lblSupplierCode);
            Controls.Add(txtSupplierCode);
            Controls.Add(lblSupplierName);
            Controls.Add(txtSupplierName);
            Controls.Add(lblContactPerson);
            Controls.Add(txtContactPerson);
            Controls.Add(lblContactNumber);
            Controls.Add(txtContactNumber);
            Controls.Add(lblEmail);
            Controls.Add(txtEmailAddress);
            Controls.Add(lblAddress);
            Controls.Add(txtAddress);
            Controls.Add(chkIsActive);
            Controls.Add(btnCancel);
            Controls.Add(btnSave);

            ResumeLayout(false);
            PerformLayout();
        }
    }
}
