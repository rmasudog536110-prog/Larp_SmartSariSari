namespace Larp.winforms.SmallTenant.Payroll
{
    partial class EditPayrollWinforms
    {
        private System.ComponentModel.IContainer components = null;

        private Guna.UI2.WinForms.Guna2Panel MainPanel;
        private Guna.UI2.WinForms.Guna2HtmlLabel TitleLabel;

        private Guna.UI2.WinForms.Guna2HtmlLabel EmployeeIdLabel;
        private Guna.UI2.WinForms.Guna2TextBox EmployeeId;

        private Guna.UI2.WinForms.Guna2HtmlLabel EmployeeNameLabel;
        private Guna.UI2.WinForms.Guna2TextBox EmployeeName;

        private Guna.UI2.WinForms.Guna2HtmlLabel PayPeriodStartLabel;
        private System.Windows.Forms.DateTimePicker PayPeriodStart;

        private Guna.UI2.WinForms.Guna2HtmlLabel PayPeriodEndLabel;
        private System.Windows.Forms.DateTimePicker PayPeriodEnd;

        private Guna.UI2.WinForms.Guna2HtmlLabel BasicSalaryLabel;
        private System.Windows.Forms.NumericUpDown BasicSalary;

        private Guna.UI2.WinForms.Guna2HtmlLabel AllowancesLabel;
        private System.Windows.Forms.NumericUpDown Allowances;

        private Guna.UI2.WinForms.Guna2HtmlLabel DeductionsLabel;
        private System.Windows.Forms.NumericUpDown Deductions;

        private Guna.UI2.WinForms.Guna2HtmlLabel StatusLabel;
        private System.Windows.Forms.ComboBox Status;

        private Guna.UI2.WinForms.Guna2Button SavePayroll;
        private Guna.UI2.WinForms.Guna2Button CancelPayroll;

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
            MainPanel = new Guna.UI2.WinForms.Guna2Panel();
            TitleLabel = new Guna.UI2.WinForms.Guna2HtmlLabel();
            EmployeeIdLabel = new Guna.UI2.WinForms.Guna2HtmlLabel();
            EmployeeId = new Guna.UI2.WinForms.Guna2TextBox();
            EmployeeNameLabel = new Guna.UI2.WinForms.Guna2HtmlLabel();
            EmployeeName = new Guna.UI2.WinForms.Guna2TextBox();
            PayPeriodStartLabel = new Guna.UI2.WinForms.Guna2HtmlLabel();
            PayPeriodStart = new DateTimePicker();
            PayPeriodEndLabel = new Guna.UI2.WinForms.Guna2HtmlLabel();
            PayPeriodEnd = new DateTimePicker();
            BasicSalaryLabel = new Guna.UI2.WinForms.Guna2HtmlLabel();
            BasicSalary = new NumericUpDown();
            AllowancesLabel = new Guna.UI2.WinForms.Guna2HtmlLabel();
            Allowances = new NumericUpDown();
            DeductionsLabel = new Guna.UI2.WinForms.Guna2HtmlLabel();
            Deductions = new NumericUpDown();
            StatusLabel = new Guna.UI2.WinForms.Guna2HtmlLabel();
            Status = new ComboBox();
            SavePayroll = new Guna.UI2.WinForms.Guna2Button();
            CancelPayroll = new Guna.UI2.WinForms.Guna2Button();
            MainPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)BasicSalary).BeginInit();
            ((System.ComponentModel.ISupportInitialize)Allowances).BeginInit();
            ((System.ComponentModel.ISupportInitialize)Deductions).BeginInit();
            SuspendLayout();
            // 
            // MainPanel
            // 
            MainPanel.BackColor = Color.White;
            MainPanel.BorderRadius = 12;
            MainPanel.Controls.Add(TitleLabel);
            MainPanel.Controls.Add(EmployeeIdLabel);
            MainPanel.Controls.Add(EmployeeId);
            MainPanel.Controls.Add(EmployeeNameLabel);
            MainPanel.Controls.Add(EmployeeName);
            MainPanel.Controls.Add(PayPeriodStartLabel);
            MainPanel.Controls.Add(PayPeriodStart);
            MainPanel.Controls.Add(PayPeriodEndLabel);
            MainPanel.Controls.Add(PayPeriodEnd);
            MainPanel.Controls.Add(BasicSalaryLabel);
            MainPanel.Controls.Add(BasicSalary);
            MainPanel.Controls.Add(AllowancesLabel);
            MainPanel.Controls.Add(Allowances);
            MainPanel.Controls.Add(DeductionsLabel);
            MainPanel.Controls.Add(Deductions);
            MainPanel.Controls.Add(StatusLabel);
            MainPanel.Controls.Add(Status);
            MainPanel.Controls.Add(SavePayroll);
            MainPanel.Controls.Add(CancelPayroll);
            MainPanel.CustomizableEdges = customizableEdges9;
            MainPanel.Dock = DockStyle.Fill;
            MainPanel.FillColor = Color.White;
            MainPanel.Location = new Point(0, 0);
            MainPanel.Margin = new Padding(3, 4, 3, 4);
            MainPanel.Name = "MainPanel";
            MainPanel.ShadowDecoration.CustomizableEdges = customizableEdges10;
            MainPanel.Size = new Size(823, 827);
            MainPanel.TabIndex = 0;
            // 
            // TitleLabel
            // 
            TitleLabel.BackColor = Color.Transparent;
            TitleLabel.Font = new Font("Segoe UI Semibold", 20F, FontStyle.Bold);
            TitleLabel.ForeColor = Color.FromArgb(28, 58, 42);
            TitleLabel.Location = new Point(40, 33);
            TitleLabel.Margin = new Padding(3, 4, 3, 4);
            TitleLabel.Name = "TitleLabel";
            TitleLabel.Size = new Size(173, 47);
            TitleLabel.TabIndex = 0;
            TitleLabel.Text = "Edit Payroll";
            // 
            // EmployeeIdLabel
            // 
            EmployeeIdLabel.BackColor = Color.Transparent;
            EmployeeIdLabel.Location = new Point(46, 113);
            EmployeeIdLabel.Margin = new Padding(3, 4, 3, 4);
            EmployeeIdLabel.Name = "EmployeeIdLabel";
            EmployeeIdLabel.Size = new Size(88, 22);
            EmployeeIdLabel.TabIndex = 1;
            EmployeeIdLabel.Text = "Employee ID";
            // 
            // EmployeeId
            // 
            EmployeeId.BorderRadius = 8;
            EmployeeId.CustomizableEdges = customizableEdges1;
            EmployeeId.DefaultText = "";
            EmployeeId.Font = new Font("Segoe UI", 9F);
            EmployeeId.Location = new Point(46, 147);
            EmployeeId.Margin = new Padding(3, 5, 3, 5);
            EmployeeId.Name = "EmployeeId";
            EmployeeId.PlaceholderText = "Enter employee ID";
            EmployeeId.SelectedText = "";
            EmployeeId.ShadowDecoration.CustomizableEdges = customizableEdges2;
            EmployeeId.Size = new Size(320, 53);
            EmployeeId.TabIndex = 2;
            // 
            // EmployeeNameLabel
            // 
            EmployeeNameLabel.BackColor = Color.Transparent;
            EmployeeNameLabel.Location = new Point(423, 113);
            EmployeeNameLabel.Margin = new Padding(3, 4, 3, 4);
            EmployeeNameLabel.Name = "EmployeeNameLabel";
            EmployeeNameLabel.Size = new Size(113, 22);
            EmployeeNameLabel.TabIndex = 3;
            EmployeeNameLabel.Text = "Employee Name";
            // 
            // EmployeeName
            // 
            EmployeeName.BorderRadius = 8;
            EmployeeName.CustomizableEdges = customizableEdges3;
            EmployeeName.DefaultText = "";
            EmployeeName.Font = new Font("Segoe UI", 9F);
            EmployeeName.Location = new Point(423, 147);
            EmployeeName.Margin = new Padding(3, 5, 3, 5);
            EmployeeName.Name = "EmployeeName";
            EmployeeName.PlaceholderText = "Enter employee name";
            EmployeeName.SelectedText = "";
            EmployeeName.ShadowDecoration.CustomizableEdges = customizableEdges4;
            EmployeeName.Size = new Size(343, 53);
            EmployeeName.TabIndex = 4;
            // 
            // PayPeriodStartLabel
            // 
            PayPeriodStartLabel.BackColor = Color.Transparent;
            PayPeriodStartLabel.Location = new Point(46, 233);
            PayPeriodStartLabel.Margin = new Padding(3, 4, 3, 4);
            PayPeriodStartLabel.Name = "PayPeriodStartLabel";
            PayPeriodStartLabel.Size = new Size(108, 22);
            PayPeriodStartLabel.TabIndex = 5;
            PayPeriodStartLabel.Text = "Pay Period Start";
            // 
            // PayPeriodStart
            // 
            PayPeriodStart.Format = DateTimePickerFormat.Short;
            PayPeriodStart.Location = new Point(46, 267);
            PayPeriodStart.Margin = new Padding(3, 4, 3, 4);
            PayPeriodStart.Name = "PayPeriodStart";
            PayPeriodStart.Size = new Size(319, 27);
            PayPeriodStart.TabIndex = 6;
            // 
            // PayPeriodEndLabel
            // 
            PayPeriodEndLabel.BackColor = Color.Transparent;
            PayPeriodEndLabel.Location = new Point(423, 233);
            PayPeriodEndLabel.Margin = new Padding(3, 4, 3, 4);
            PayPeriodEndLabel.Name = "PayPeriodEndLabel";
            PayPeriodEndLabel.Size = new Size(102, 22);
            PayPeriodEndLabel.TabIndex = 7;
            PayPeriodEndLabel.Text = "Pay Period End";
            // 
            // PayPeriodEnd
            // 
            PayPeriodEnd.Format = DateTimePickerFormat.Short;
            PayPeriodEnd.Location = new Point(423, 267);
            PayPeriodEnd.Margin = new Padding(3, 4, 3, 4);
            PayPeriodEnd.Name = "PayPeriodEnd";
            PayPeriodEnd.Size = new Size(342, 27);
            PayPeriodEnd.TabIndex = 8;
            // 
            // BasicSalaryLabel
            // 
            BasicSalaryLabel.BackColor = Color.Transparent;
            BasicSalaryLabel.Location = new Point(46, 327);
            BasicSalaryLabel.Margin = new Padding(3, 4, 3, 4);
            BasicSalaryLabel.Name = "BasicSalaryLabel";
            BasicSalaryLabel.Size = new Size(81, 22);
            BasicSalaryLabel.TabIndex = 9;
            BasicSalaryLabel.Text = "Basic Salary";
            // 
            // BasicSalary
            // 
            BasicSalary.DecimalPlaces = 2;
            BasicSalary.Increment = new decimal(new int[] { 100, 0, 0, 0 });
            BasicSalary.Location = new Point(46, 360);
            BasicSalary.Margin = new Padding(3, 4, 3, 4);
            BasicSalary.Maximum = new decimal(new int[] { 100000000, 0, 0, 0 });
            BasicSalary.Name = "BasicSalary";
            BasicSalary.Size = new Size(320, 27);
            BasicSalary.TabIndex = 10;
            BasicSalary.ThousandsSeparator = true;
            // 
            // AllowancesLabel
            // 
            AllowancesLabel.BackColor = Color.Transparent;
            AllowancesLabel.Location = new Point(423, 327);
            AllowancesLabel.Margin = new Padding(3, 4, 3, 4);
            AllowancesLabel.Name = "AllowancesLabel";
            AllowancesLabel.Size = new Size(78, 22);
            AllowancesLabel.TabIndex = 11;
            AllowancesLabel.Text = "Allowances";
            // 
            // Allowances
            // 
            Allowances.DecimalPlaces = 2;
            Allowances.Increment = new decimal(new int[] { 100, 0, 0, 0 });
            Allowances.Location = new Point(423, 360);
            Allowances.Margin = new Padding(3, 4, 3, 4);
            Allowances.Maximum = new decimal(new int[] { 100000000, 0, 0, 0 });
            Allowances.Name = "Allowances";
            Allowances.Size = new Size(343, 27);
            Allowances.TabIndex = 12;
            Allowances.ThousandsSeparator = true;
            // 
            // DeductionsLabel
            // 
            DeductionsLabel.BackColor = Color.Transparent;
            DeductionsLabel.Location = new Point(46, 420);
            DeductionsLabel.Margin = new Padding(3, 4, 3, 4);
            DeductionsLabel.Name = "DeductionsLabel";
            DeductionsLabel.Size = new Size(78, 22);
            DeductionsLabel.TabIndex = 13;
            DeductionsLabel.Text = "Deductions";
            // 
            // Deductions
            // 
            Deductions.DecimalPlaces = 2;
            Deductions.Increment = new decimal(new int[] { 100, 0, 0, 0 });
            Deductions.Location = new Point(46, 453);
            Deductions.Margin = new Padding(3, 4, 3, 4);
            Deductions.Maximum = new decimal(new int[] { 100000000, 0, 0, 0 });
            Deductions.Name = "Deductions";
            Deductions.Size = new Size(320, 27);
            Deductions.TabIndex = 14;
            Deductions.ThousandsSeparator = true;
            // 
            // StatusLabel
            // 
            StatusLabel.BackColor = Color.Transparent;
            StatusLabel.Location = new Point(423, 420);
            StatusLabel.Margin = new Padding(3, 4, 3, 4);
            StatusLabel.Name = "StatusLabel";
            StatusLabel.Size = new Size(43, 22);
            StatusLabel.TabIndex = 15;
            StatusLabel.Text = "Status";
            // 
            // Status
            // 
            Status.DropDownStyle = ComboBoxStyle.DropDownList;
            Status.FormattingEnabled = true;
            Status.Location = new Point(423, 453);
            Status.Margin = new Padding(3, 4, 3, 4);
            Status.Name = "Status";
            Status.Size = new Size(342, 28);
            Status.TabIndex = 16;
            // 
            // SavePayroll
            // 
            SavePayroll.BorderRadius = 8;
            SavePayroll.CustomizableEdges = customizableEdges5;
            SavePayroll.FillColor = Color.FromArgb(46, 107, 69);
            SavePayroll.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            SavePayroll.ForeColor = Color.White;
            SavePayroll.Location = new Point(423, 600);
            SavePayroll.Margin = new Padding(3, 4, 3, 4);
            SavePayroll.Name = "SavePayroll";
            SavePayroll.ShadowDecoration.CustomizableEdges = customizableEdges6;
            SavePayroll.Size = new Size(160, 56);
            SavePayroll.TabIndex = 17;
            SavePayroll.Text = "Save Changes";
            SavePayroll.Click += SavePayroll_Click;
            // 
            // CancelPayroll
            // 
            CancelPayroll.BorderRadius = 8;
            CancelPayroll.CustomizableEdges = customizableEdges7;
            CancelPayroll.FillColor = Color.Gray;
            CancelPayroll.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            CancelPayroll.ForeColor = Color.White;
            CancelPayroll.Location = new Point(600, 600);
            CancelPayroll.Margin = new Padding(3, 4, 3, 4);
            CancelPayroll.Name = "CancelPayroll";
            CancelPayroll.ShadowDecoration.CustomizableEdges = customizableEdges8;
            CancelPayroll.Size = new Size(166, 56);
            CancelPayroll.TabIndex = 18;
            CancelPayroll.Text = "Cancel";
            CancelPayroll.Click += CancelPayroll_Click;
            // 
            // EditPayrollWinforms
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(245, 242, 237);
            ClientSize = new Size(823, 827);
            Controls.Add(MainPanel);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Margin = new Padding(3, 4, 3, 4);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "EditPayrollWinforms";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Edit Payroll";
            MainPanel.ResumeLayout(false);
            MainPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)BasicSalary).EndInit();
            ((System.ComponentModel.ISupportInitialize)Allowances).EndInit();
            ((System.ComponentModel.ISupportInitialize)Deductions).EndInit();
            ResumeLayout(false);
        }
    }
}