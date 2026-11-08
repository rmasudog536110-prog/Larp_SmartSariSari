namespace Larp.winforms.SmallTenant.Payroll
{
    partial class AddPayrollWinforms
    {
        private System.ComponentModel.IContainer components = null;

        private Guna.UI2.WinForms.Guna2Panel MainPanel;
        private Guna.UI2.WinForms.Guna2HtmlLabel TitleLabel;
        private Guna.UI2.WinForms.Guna2HtmlLabel SubtitleLabel;

        private Guna.UI2.WinForms.Guna2HtmlLabel EmployeeLabel;
        private Guna.UI2.WinForms.Guna2ComboBox cmbEmployee;

        private Guna.UI2.WinForms.Guna2HtmlLabel EmployeeCodeLabel;
        private Guna.UI2.WinForms.Guna2TextBox txtEmployeeCode;
        private Guna.UI2.WinForms.Guna2HtmlLabel PositionLabel;
        private Guna.UI2.WinForms.Guna2TextBox txtPosition;
        private Guna.UI2.WinForms.Guna2HtmlLabel ContactLabel;
        private Guna.UI2.WinForms.Guna2TextBox txtContact;
        private Guna.UI2.WinForms.Guna2HtmlLabel DateHiredLabel;
        private Guna.UI2.WinForms.Guna2TextBox txtDateHired;

        private Guna.UI2.WinForms.Guna2HtmlLabel PayPeriodStartLabel;
        private Guna.UI2.WinForms.Guna2DateTimePicker dtpStart;
        private Guna.UI2.WinForms.Guna2HtmlLabel PayPeriodEndLabel;
        private Guna.UI2.WinForms.Guna2DateTimePicker dtpEnd;

        private Guna.UI2.WinForms.Guna2HtmlLabel BasicSalaryLabel;
        private System.Windows.Forms.NumericUpDown numBasic;
        private Guna.UI2.WinForms.Guna2HtmlLabel AllowancesLabel;
        private System.Windows.Forms.NumericUpDown numAllow;
        private Guna.UI2.WinForms.Guna2HtmlLabel DeductionsLabel;
        private System.Windows.Forms.NumericUpDown numDeduct;
        private Guna.UI2.WinForms.Guna2HtmlLabel NetPayLabel;
        private Guna.UI2.WinForms.Guna2TextBox txtNetPay;

        private System.Windows.Forms.Label lblHint;
        private System.Windows.Forms.Label lblStatusNote;

        private Guna.UI2.WinForms.Guna2Button btnSave;
        private Guna.UI2.WinForms.Guna2Button btnCancel;

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
            components = new System.ComponentModel.Container();

            MainPanel = new Guna.UI2.WinForms.Guna2Panel();
            TitleLabel = new Guna.UI2.WinForms.Guna2HtmlLabel();
            SubtitleLabel = new Guna.UI2.WinForms.Guna2HtmlLabel();
            EmployeeLabel = new Guna.UI2.WinForms.Guna2HtmlLabel();
            cmbEmployee = new Guna.UI2.WinForms.Guna2ComboBox();
            EmployeeCodeLabel = new Guna.UI2.WinForms.Guna2HtmlLabel();
            txtEmployeeCode = new Guna.UI2.WinForms.Guna2TextBox();
            PositionLabel = new Guna.UI2.WinForms.Guna2HtmlLabel();
            txtPosition = new Guna.UI2.WinForms.Guna2TextBox();
            ContactLabel = new Guna.UI2.WinForms.Guna2HtmlLabel();
            txtContact = new Guna.UI2.WinForms.Guna2TextBox();
            DateHiredLabel = new Guna.UI2.WinForms.Guna2HtmlLabel();
            txtDateHired = new Guna.UI2.WinForms.Guna2TextBox();
            PayPeriodStartLabel = new Guna.UI2.WinForms.Guna2HtmlLabel();
            dtpStart = new Guna.UI2.WinForms.Guna2DateTimePicker();
            PayPeriodEndLabel = new Guna.UI2.WinForms.Guna2HtmlLabel();
            dtpEnd = new Guna.UI2.WinForms.Guna2DateTimePicker();
            BasicSalaryLabel = new Guna.UI2.WinForms.Guna2HtmlLabel();
            numBasic = new System.Windows.Forms.NumericUpDown();
            AllowancesLabel = new Guna.UI2.WinForms.Guna2HtmlLabel();
            numAllow = new System.Windows.Forms.NumericUpDown();
            DeductionsLabel = new Guna.UI2.WinForms.Guna2HtmlLabel();
            numDeduct = new System.Windows.Forms.NumericUpDown();
            NetPayLabel = new Guna.UI2.WinForms.Guna2HtmlLabel();
            txtNetPay = new Guna.UI2.WinForms.Guna2TextBox();
            lblHint = new System.Windows.Forms.Label();
            lblStatusNote = new System.Windows.Forms.Label();
            btnSave = new Guna.UI2.WinForms.Guna2Button();
            btnCancel = new Guna.UI2.WinForms.Guna2Button();

            ((System.ComponentModel.ISupportInitialize)numBasic).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numAllow).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numDeduct).BeginInit();
            SuspendLayout();

            // MainPanel
            MainPanel.BackColor = System.Drawing.Color.White;
            MainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            MainPanel.FillColor = System.Drawing.Color.White;
            MainPanel.Location = new System.Drawing.Point(0, 0);
            MainPanel.Name = "MainPanel";
            MainPanel.Size = new System.Drawing.Size(760, 770);
            MainPanel.TabIndex = 0;

            // TitleLabel
            TitleLabel.BackColor = System.Drawing.Color.Transparent;
            TitleLabel.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            TitleLabel.ForeColor = System.Drawing.Color.FromArgb(28, 40, 63);
            TitleLabel.Location = new System.Drawing.Point(40, 22);
            TitleLabel.Name = "TitleLabel";
            TitleLabel.Text = "Add Payroll";

            // SubtitleLabel
            SubtitleLabel.BackColor = System.Drawing.Color.Transparent;
            SubtitleLabel.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            SubtitleLabel.ForeColor = System.Drawing.Color.FromArgb(96, 105, 121);
            SubtitleLabel.Location = new System.Drawing.Point(43, 68);
            SubtitleLabel.Name = "SubtitleLabel";
            SubtitleLabel.Text = "Choose an employee. Their details fill in automatically.";

            // EmployeeLabel
            EmployeeLabel.BackColor = System.Drawing.Color.Transparent;
            EmployeeLabel.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            EmployeeLabel.Location = new System.Drawing.Point(40, 112);
            EmployeeLabel.Name = "EmployeeLabel";
            EmployeeLabel.Text = "Employee";

            // cmbEmployee
            cmbEmployee.BackColor = System.Drawing.Color.Transparent;
            cmbEmployee.BorderRadius = 8;
            cmbEmployee.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            cmbEmployee.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            cmbEmployee.Font = new System.Drawing.Font("Segoe UI", 10F);
            cmbEmployee.ItemHeight = 34;
            cmbEmployee.Location = new System.Drawing.Point(40, 138);
            cmbEmployee.Name = "cmbEmployee";
            cmbEmployee.Size = new System.Drawing.Size(680, 40);
            cmbEmployee.TabIndex = 1;
            //cmbEmployee.SelectedIndexChanged += cmbEmployee_SelectedIndexChanged;

            // Employee code
            EmployeeCodeLabel.BackColor = System.Drawing.Color.Transparent;
            EmployeeCodeLabel.Location = new System.Drawing.Point(40, 198);
            EmployeeCodeLabel.Name = "EmployeeCodeLabel";
            EmployeeCodeLabel.Text = "Employee Code";

            txtEmployeeCode.BorderRadius = 8;
            txtEmployeeCode.DefaultText = "";
            txtEmployeeCode.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            txtEmployeeCode.Location = new System.Drawing.Point(40, 224);
            txtEmployeeCode.Name = "txtEmployeeCode";
            txtEmployeeCode.ReadOnly = true;
            txtEmployeeCode.Size = new System.Drawing.Size(330, 38);
            txtEmployeeCode.TabIndex = 2;
            txtEmployeeCode.TabStop = false;

            // Position
            PositionLabel.BackColor = System.Drawing.Color.Transparent;
            PositionLabel.Location = new System.Drawing.Point(390, 198);
            PositionLabel.Name = "PositionLabel";
            PositionLabel.Text = "Position";

            txtPosition.BorderRadius = 8;
            txtPosition.DefaultText = "";
            txtPosition.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            txtPosition.Location = new System.Drawing.Point(390, 224);
            txtPosition.Name = "txtPosition";
            txtPosition.ReadOnly = true;
            txtPosition.Size = new System.Drawing.Size(330, 38);
            txtPosition.TabIndex = 3;
            txtPosition.TabStop = false;

            // Contact
            ContactLabel.BackColor = System.Drawing.Color.Transparent;
            ContactLabel.Location = new System.Drawing.Point(40, 282);
            ContactLabel.Name = "ContactLabel";
            ContactLabel.Text = "Contact Number";

            txtContact.BorderRadius = 8;
            txtContact.DefaultText = "";
            txtContact.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            txtContact.Location = new System.Drawing.Point(40, 308);
            txtContact.Name = "txtContact";
            txtContact.ReadOnly = true;
            txtContact.Size = new System.Drawing.Size(330, 38);
            txtContact.TabIndex = 4;
            txtContact.TabStop = false;

            // Date hired
            DateHiredLabel.BackColor = System.Drawing.Color.Transparent;
            DateHiredLabel.Location = new System.Drawing.Point(390, 282);
            DateHiredLabel.Name = "DateHiredLabel";
            DateHiredLabel.Text = "Date Hired";

            txtDateHired.BorderRadius = 8;
            txtDateHired.DefaultText = "";
            txtDateHired.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            txtDateHired.Location = new System.Drawing.Point(390, 308);
            txtDateHired.Name = "txtDateHired";
            txtDateHired.ReadOnly = true;
            txtDateHired.Size = new System.Drawing.Size(330, 38);
            txtDateHired.TabIndex = 5;
            txtDateHired.TabStop = false;

            // Pay period start
            PayPeriodStartLabel.BackColor = System.Drawing.Color.Transparent;
            PayPeriodStartLabel.Location = new System.Drawing.Point(40, 366);
            PayPeriodStartLabel.Name = "PayPeriodStartLabel";
            PayPeriodStartLabel.Text = "Pay Period Start";

            dtpStart.BorderRadius = 8;
            dtpStart.Checked = true;
            dtpStart.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            dtpStart.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            dtpStart.Location = new System.Drawing.Point(40, 392);
            dtpStart.Name = "dtpStart";
            dtpStart.Size = new System.Drawing.Size(330, 38);
            dtpStart.TabIndex = 6;

            // Pay period end
            PayPeriodEndLabel.BackColor = System.Drawing.Color.Transparent;
            PayPeriodEndLabel.Location = new System.Drawing.Point(390, 366);
            PayPeriodEndLabel.Name = "PayPeriodEndLabel";
            PayPeriodEndLabel.Text = "Pay Period End";

            dtpEnd.BorderRadius = 8;
            dtpEnd.Checked = true;
            dtpEnd.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            dtpEnd.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            dtpEnd.Location = new System.Drawing.Point(390, 392);
            dtpEnd.Name = "dtpEnd";
            dtpEnd.Size = new System.Drawing.Size(330, 38);
            dtpEnd.TabIndex = 7;

            // Basic salary
            BasicSalaryLabel.BackColor = System.Drawing.Color.Transparent;
            BasicSalaryLabel.Location = new System.Drawing.Point(40, 450);
            BasicSalaryLabel.Name = "BasicSalaryLabel";
            BasicSalaryLabel.Text = "Basic Salary";

            numBasic.DecimalPlaces = 2;
            numBasic.Font = new System.Drawing.Font("Segoe UI", 10F);
            numBasic.Increment = new decimal(new int[] { 100, 0, 0, 0 });
            numBasic.Location = new System.Drawing.Point(40, 476);
            numBasic.Maximum = new decimal(new int[] { 100000000, 0, 0, 0 });
            numBasic.Name = "numBasic";
            numBasic.Size = new System.Drawing.Size(330, 30);
            numBasic.TabIndex = 8;
            numBasic.ThousandsSeparator = true;
            numBasic.ValueChanged += NumericValueChanged;

            // Allowances
            AllowancesLabel.BackColor = System.Drawing.Color.Transparent;
            AllowancesLabel.Location = new System.Drawing.Point(390, 450);
            AllowancesLabel.Name = "AllowancesLabel";
            AllowancesLabel.Text = "Allowances";

            numAllow.DecimalPlaces = 2;
            numAllow.Font = new System.Drawing.Font("Segoe UI", 10F);
            numAllow.Increment = new decimal(new int[] { 100, 0, 0, 0 });
            numAllow.Location = new System.Drawing.Point(390, 476);
            numAllow.Maximum = new decimal(new int[] { 100000000, 0, 0, 0 });
            numAllow.Name = "numAllow";
            numAllow.Size = new System.Drawing.Size(330, 30);
            numAllow.TabIndex = 9;
            numAllow.ThousandsSeparator = true;
            numAllow.ValueChanged += NumericValueChanged;

            // Deductions
            DeductionsLabel.BackColor = System.Drawing.Color.Transparent;
            DeductionsLabel.Location = new System.Drawing.Point(40, 526);
            DeductionsLabel.Name = "DeductionsLabel";
            DeductionsLabel.Text = "Deductions";

            numDeduct.DecimalPlaces = 2;
            numDeduct.Font = new System.Drawing.Font("Segoe UI", 10F);
            numDeduct.Increment = new decimal(new int[] { 100, 0, 0, 0 });
            numDeduct.Location = new System.Drawing.Point(40, 552);
            numDeduct.Maximum = new decimal(new int[] { 100000000, 0, 0, 0 });
            numDeduct.Name = "numDeduct";
            numDeduct.Size = new System.Drawing.Size(330, 30);
            numDeduct.TabIndex = 10;
            numDeduct.ThousandsSeparator = true;
            numDeduct.ValueChanged += NumericValueChanged;

            // Net pay
            NetPayLabel.BackColor = System.Drawing.Color.Transparent;
            NetPayLabel.Location = new System.Drawing.Point(390, 526);
            NetPayLabel.Name = "NetPayLabel";
            NetPayLabel.Text = "Net Pay";

            txtNetPay.BorderRadius = 8;
            txtNetPay.DefaultText = "₱0.00";
            txtNetPay.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            txtNetPay.Location = new System.Drawing.Point(390, 548);
            txtNetPay.Name = "txtNetPay";
            txtNetPay.ReadOnly = true;
            txtNetPay.Size = new System.Drawing.Size(330, 38);
            txtNetPay.TabIndex = 11;
            txtNetPay.TabStop = false;

            // lblHint
            lblHint.Font = new System.Drawing.Font("Segoe UI", 9F);
            lblHint.ForeColor = System.Drawing.Color.FromArgb(96, 105, 121);
            lblHint.Location = new System.Drawing.Point(40, 604);
            lblHint.Name = "lblHint";
            lblHint.Size = new System.Drawing.Size(680, 40);
            lblHint.TabIndex = 12;

            // lblStatusNote
            lblStatusNote.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            lblStatusNote.ForeColor = System.Drawing.Color.FromArgb(28, 40, 63);
            lblStatusNote.Location = new System.Drawing.Point(40, 650);
            lblStatusNote.Name = "lblStatusNote";
            lblStatusNote.Size = new System.Drawing.Size(680, 24);
            lblStatusNote.TabIndex = 13;

            // btnSave
            btnSave.BorderRadius = 8;
            btnSave.FillColor = System.Drawing.Color.FromArgb(28, 40, 63);
            btnSave.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            btnSave.ForeColor = System.Drawing.Color.White;
            btnSave.Location = new System.Drawing.Point(400, 700);
            btnSave.Name = "btnSave";
            btnSave.Size = new System.Drawing.Size(150, 44);
            btnSave.TabIndex = 14;
            btnSave.Tag = "primary";
            btnSave.Text = "Save Payroll";
            btnSave.Click += btnSave_Click;

            // btnCancel
            btnCancel.BorderRadius = 8;
            btnCancel.FillColor = System.Drawing.Color.FromArgb(210, 212, 217);
            btnCancel.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            btnCancel.ForeColor = System.Drawing.Color.FromArgb(28, 40, 63);
            btnCancel.Location = new System.Drawing.Point(570, 700);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new System.Drawing.Size(150, 44);
            btnCancel.TabIndex = 15;
            btnCancel.Tag = "secondary";
            btnCancel.Text = "Cancel";
            btnCancel.Click += btnCancel_Click;

            MainPanel.Controls.Add(TitleLabel);
            MainPanel.Controls.Add(SubtitleLabel);
            MainPanel.Controls.Add(EmployeeLabel);
            MainPanel.Controls.Add(cmbEmployee);
            MainPanel.Controls.Add(EmployeeCodeLabel);
            MainPanel.Controls.Add(txtEmployeeCode);
            MainPanel.Controls.Add(PositionLabel);
            MainPanel.Controls.Add(txtPosition);
            MainPanel.Controls.Add(ContactLabel);
            MainPanel.Controls.Add(txtContact);
            MainPanel.Controls.Add(DateHiredLabel);
            MainPanel.Controls.Add(txtDateHired);
            MainPanel.Controls.Add(PayPeriodStartLabel);
            MainPanel.Controls.Add(dtpStart);
            MainPanel.Controls.Add(PayPeriodEndLabel);
            MainPanel.Controls.Add(dtpEnd);
            MainPanel.Controls.Add(BasicSalaryLabel);
            MainPanel.Controls.Add(numBasic);
            MainPanel.Controls.Add(AllowancesLabel);
            MainPanel.Controls.Add(numAllow);
            MainPanel.Controls.Add(DeductionsLabel);
            MainPanel.Controls.Add(numDeduct);
            MainPanel.Controls.Add(NetPayLabel);
            MainPanel.Controls.Add(txtNetPay);
            MainPanel.Controls.Add(lblHint);
            MainPanel.Controls.Add(lblStatusNote);
            MainPanel.Controls.Add(btnSave);
            MainPanel.Controls.Add(btnCancel);

            // AddPayrollWinforms
            AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            BackColor = System.Drawing.Color.FromArgb(245, 247, 250);
            ClientSize = new System.Drawing.Size(760, 770);
            Controls.Add(MainPanel);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "AddPayrollWinforms";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            Text = "Add Payroll";

            ((System.ComponentModel.ISupportInitialize)numBasic).EndInit();
            ((System.ComponentModel.ISupportInitialize)numAllow).EndInit();
            ((System.ComponentModel.ISupportInitialize)numDeduct).EndInit();
            ResumeLayout(false);
        }
    }
}