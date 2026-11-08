using System;
using System.Drawing;
using System.Windows.Forms;

namespace Larp.winforms.SmallTenant.Payroll
{
    public partial class EditPayrollWinforms : Form
    {
        public int EmployeeIdValue =>
            int.Parse(EmployeeId.Text.Trim());

        public string EmployeeNameValue =>
            EmployeeName.Text.Trim();

        public DateTime PayPeriodStartValue =>
            PayPeriodStart.Value.Date;

        public DateTime PayPeriodEndValue =>
            PayPeriodEnd.Value.Date;

        public decimal BasicSalaryValue =>
            BasicSalary.Value;

        public decimal AllowancesValue =>
            Allowances.Value;

        public decimal DeductionsValue =>
            Deductions.Value;

        public string StatusValue =>
            Status.SelectedItem?.ToString()
            ?? "Pending";

        public EditPayrollWinforms(
            int employeeId,
            string employeeName,
            DateTime payPeriodStart,
            DateTime payPeriodEnd,
            decimal basicSalary,
            decimal allowances,
            decimal deductions,
            string status)
        {
            InitializeComponent();

            EmployeeId.Text =
                employeeId.ToString();
            EmployeeId.ReadOnly = true;

            EmployeeName.Text =
                employeeName;
            EmployeeName.ReadOnly = true;

            PayPeriodStart.Value =
                payPeriodStart;

            PayPeriodEnd.Value =
                payPeriodEnd;

            BasicSalary.Value =
                basicSalary;

            Allowances.Value =
                allowances;

            Deductions.Value =
                deductions;

            // Add the choices first, then select, so nothing appears twice.
            Status.Items.AddRange(new object[] { "Pending", "Approved", "Rejected", "Paid", "Cancelled" });

            if (!Status.Items.Contains(status))
            {
                Status.Items.Add(status);
            }

            Status.SelectedItem = status;

            // A Manager cannot change the status; the API keeps it as it is.
            Status.Enabled = Session.Role != "Manager";

            AppTheme.Apply(this);

            // Salary and deductions are worked out by the system, never typed.
            BasicSalary.Enabled = false;
            Deductions.Enabled = false;

            // The system only recalculates a Pending payroll, so the period of an
            // approved or paid one is locked.
            bool pending = status == "Pending";
            PayPeriodStart.Enabled = pending;
            PayPeriodEnd.Enabled = pending;

            var note = new Label
            {
                Text = pending
                    ? "Salary and deductions come from the employee's Benefits and the contribution " +
                      "rates. They are recalculated when you save."
                    : "This payroll is no longer Pending, so its salary, deductions and period are " +
                      "frozen. Only allowances and status can change.",
                Font = new Font("Segoe UI", 9F),
                ForeColor = Color.FromArgb(96, 105, 121),
                Location = new Point(46, 500),
                Size = new Size(720, 60),
                BackColor = Color.Transparent
            };

            MainPanel.Controls.Add(note);
        }

        private void SavePayroll_Click(
            object sender,
            EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(
                EmployeeName.Text))
            {
                MessageBox.Show(
                    "Employee name is required.",
                    "Payroll",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            if (PayPeriodEnd.Value <
                PayPeriodStart.Value)
            {
                MessageBox.Show(
                    "Pay period end cannot be earlier than the start date.",
                    "Payroll",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            DialogResult =
                DialogResult.OK;

            Close();
        }

        private void CancelPayroll_Click(
            object sender,
            EventArgs e)
        {
            DialogResult =
                DialogResult.Cancel;

            Close();
        }
    }
}