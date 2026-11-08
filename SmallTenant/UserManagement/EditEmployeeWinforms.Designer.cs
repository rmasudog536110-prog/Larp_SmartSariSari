namespace Larp.winforms.SmallTenant.UserManagement
{
    partial class EditEmployeeWinforms
    {
        private System.ComponentModel.IContainer components = null;

        private Guna.UI2.WinForms.Guna2Panel panelMain;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblTitle;

        private Guna.UI2.WinForms.Guna2HtmlLabel lblEmployeeCode;
        private Guna.UI2.WinForms.Guna2TextBox txtEmployeeCode;

        private Guna.UI2.WinForms.Guna2HtmlLabel lblFirstName;
        private Guna.UI2.WinForms.Guna2TextBox txtFirstName;

        private Guna.UI2.WinForms.Guna2HtmlLabel lblMiddleName;
        private Guna.UI2.WinForms.Guna2TextBox txtMiddleName;

        private Guna.UI2.WinForms.Guna2HtmlLabel lblLastName;
        private Guna.UI2.WinForms.Guna2TextBox txtLastName;

        private Guna.UI2.WinForms.Guna2HtmlLabel lblPosition;
        private Guna.UI2.WinForms.Guna2ComboBox cmbPosition;

        private Guna.UI2.WinForms.Guna2HtmlLabel lblContactNumber;
        private Guna.UI2.WinForms.Guna2TextBox txtContactNumber;

        private Guna.UI2.WinForms.Guna2HtmlLabel lblEmailAddress;
        private Guna.UI2.WinForms.Guna2TextBox txtEmailAddress;

        private Guna.UI2.WinForms.Guna2HtmlLabel lblAddress;
        private Guna.UI2.WinForms.Guna2TextBox txtAddress;

        private Guna.UI2.WinForms.Guna2HtmlLabel lblDateHired;
        private Guna.UI2.WinForms.Guna2DateTimePicker dtpDateHired;

        private Guna.UI2.WinForms.Guna2HtmlLabel lblEmploymentStatus;
        private Guna.UI2.WinForms.Guna2ComboBox cmbEmploymentStatus;

        private Guna.UI2.WinForms.Guna2HtmlLabel lblDateSeparated;
        private Guna.UI2.WinForms.Guna2DateTimePicker dtpDateSeparated;

        private Guna.UI2.WinForms.Guna2HtmlLabel lblSeparationReason;
        private Guna.UI2.WinForms.Guna2TextBox txtSeparationReason;

        private Guna.UI2.WinForms.Guna2Button btnSave;
        private Guna.UI2.WinForms.Guna2Button btnCancel;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null)
            {
                components.Dispose();
            }

            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            components =
                new System.ComponentModel.Container();

            panelMain =
                new Guna.UI2.WinForms.Guna2Panel();

            lblTitle =
                new Guna.UI2.WinForms.Guna2HtmlLabel();

            lblEmployeeCode =
                new Guna.UI2.WinForms.Guna2HtmlLabel();

            txtEmployeeCode =
                new Guna.UI2.WinForms.Guna2TextBox();

            lblFirstName =
                new Guna.UI2.WinForms.Guna2HtmlLabel();

            txtFirstName =
                new Guna.UI2.WinForms.Guna2TextBox();

            lblMiddleName =
                new Guna.UI2.WinForms.Guna2HtmlLabel();

            txtMiddleName =
                new Guna.UI2.WinForms.Guna2TextBox();

            lblLastName =
                new Guna.UI2.WinForms.Guna2HtmlLabel();

            txtLastName =
                new Guna.UI2.WinForms.Guna2TextBox();

            lblPosition =
                new Guna.UI2.WinForms.Guna2HtmlLabel();

            cmbPosition =
                new Guna.UI2.WinForms.Guna2ComboBox();

            lblContactNumber =
                new Guna.UI2.WinForms.Guna2HtmlLabel();

            txtContactNumber =
                new Guna.UI2.WinForms.Guna2TextBox();

            lblEmailAddress =
                new Guna.UI2.WinForms.Guna2HtmlLabel();

            txtEmailAddress =
                new Guna.UI2.WinForms.Guna2TextBox();

            lblAddress =
                new Guna.UI2.WinForms.Guna2HtmlLabel();

            txtAddress =
                new Guna.UI2.WinForms.Guna2TextBox();

            lblDateHired =
                new Guna.UI2.WinForms.Guna2HtmlLabel();

            dtpDateHired =
                new Guna.UI2.WinForms.Guna2DateTimePicker();

            lblEmploymentStatus =
                new Guna.UI2.WinForms.Guna2HtmlLabel();

            cmbEmploymentStatus =
                new Guna.UI2.WinForms.Guna2ComboBox();

            lblDateSeparated =
                new Guna.UI2.WinForms.Guna2HtmlLabel();

            dtpDateSeparated =
                new Guna.UI2.WinForms.Guna2DateTimePicker();

            lblSeparationReason =
                new Guna.UI2.WinForms.Guna2HtmlLabel();

            txtSeparationReason =
                new Guna.UI2.WinForms.Guna2TextBox();

            btnSave =
                new Guna.UI2.WinForms.Guna2Button();

            btnCancel =
                new Guna.UI2.WinForms.Guna2Button();

            SuspendLayout();

            panelMain.BackColor =
                Color.White;

            panelMain.BorderRadius =
                15;

            panelMain.Dock =
                DockStyle.Fill;

            panelMain.Padding =
                new Padding(30);

            lblTitle.BackColor =
                Color.Transparent;

            lblTitle.Font =
                new Font(
                    "Segoe UI",
                    18F,
                    FontStyle.Bold);

            lblTitle.ForeColor =
                Color.FromArgb(
                    28,
                    40,
                    63);

            lblTitle.Location =
                new Point(
                    30,
                    20);

            lblTitle.Text =
                "Edit Employee";

            ConfigureLabel(
                lblEmployeeCode,
                "Employee Code",
                30,
                70);

            ConfigureTextBox(
                txtEmployeeCode,
                30,
                95);

            ConfigureLabel(
                lblFirstName,
                "First Name",
                285,
                70);

            ConfigureTextBox(
                txtFirstName,
                285,
                95);

            ConfigureLabel(
                lblMiddleName,
                "Middle Name",
                540,
                70);

            ConfigureTextBox(
                txtMiddleName,
                540,
                95);

            ConfigureLabel(
                lblLastName,
                "Last Name",
                30,
                150);

            ConfigureTextBox(
                txtLastName,
                30,
                175);

            ConfigureLabel(
                lblPosition,
                "Position",
                285,
                150);

            cmbPosition.BorderRadius =
                8;

            cmbPosition.Font =
                new Font(
                    "Segoe UI",
                    9F);

            cmbPosition.DropDownStyle =
                ComboBoxStyle.DropDownList;

            cmbPosition.Items.Add(
                "Manager");

            cmbPosition.Items.Add(
                "Cashier");

            cmbPosition.Items.Add(
                "Sales Associate");

            cmbPosition.Items.Add(
                "Inventory Clerk");

            cmbPosition.Items.Add(
                "Purchasing Clerk");

            cmbPosition.Items.Add(
                "HR Staff");

            cmbPosition.Items.Add(
                "Accountant");

            cmbPosition.Location =
                new Point(
                    285,
                    175);

            cmbPosition.Size =
                new Size(
                    230,
                    40);

            ConfigureLabel(
                lblContactNumber,
                "Contact Number",
                540,
                150);

            ConfigureTextBox(
                txtContactNumber,
                540,
                175);

            ConfigureLabel(
                lblEmailAddress,
                "Email Address",
                30,
                230);

            ConfigureTextBox(
                txtEmailAddress,
                30,
                255);

            ConfigureLabel(
                lblDateHired,
                "Date Hired",
                285,
                230);

            dtpDateHired.BorderRadius =
                8;

            dtpDateHired.Format =
                DateTimePickerFormat.Short;

            dtpDateHired.Location =
                new Point(
                    285,
                    255);

            dtpDateHired.Size =
                new Size(
                    230,
                    40);

            ConfigureLabel(
                lblEmploymentStatus,
                "Employment Status",
                540,
                230);

            cmbEmploymentStatus.BorderRadius =
                8;

            cmbEmploymentStatus.Font =
                new Font(
                    "Segoe UI",
                    9F);

            cmbEmploymentStatus.DropDownStyle =
                ComboBoxStyle.DropDownList;

            cmbEmploymentStatus.Items.Add(
                "Active");

            cmbEmploymentStatus.Items.Add(
                "Resigned");

            cmbEmploymentStatus.Items.Add(
                "Fired");

            cmbEmploymentStatus.Location =
                new Point(
                    540,
                    255);

            cmbEmploymentStatus.Size =
                new Size(
                    230,
                    40);

            cmbEmploymentStatus.SelectedIndexChanged +=
                cmbEmploymentStatus_SelectedIndexChanged;

            ConfigureLabel(
                lblAddress,
                "Address",
                30,
                310);

            txtAddress.BorderRadius =
                8;

            txtAddress.Multiline =
                true;

            txtAddress.Location =
                new Point(
                    30,
                    335);

            txtAddress.Size =
                new Size(
                    485,
                    65);

            ConfigureLabel(
                lblDateSeparated,
                "Date Separated",
                540,
                310);

            dtpDateSeparated.BorderRadius =
                8;

            dtpDateSeparated.Format =
                DateTimePickerFormat.Short;

            dtpDateSeparated.Location =
                new Point(
                    540,
                    335);

            dtpDateSeparated.Size =
                new Size(
                    230,
                    40);

            ConfigureLabel(
                lblSeparationReason,
                "Reason",
                30,
                420);

            txtSeparationReason.BorderRadius =
                8;

            txtSeparationReason.Multiline =
                true;

            txtSeparationReason.Location =
                new Point(
                    30,
                    445);

            txtSeparationReason.Size =
                new Size(
                    485,
                    65);

            btnSave.BorderRadius =
                8;

            btnSave.FillColor =
                Color.FromArgb(
                    46,
                    107,
                    69);

            btnSave.Font =
                new Font(
                    "Segoe UI",
                    9F,
                    FontStyle.Bold);

            btnSave.ForeColor =
                Color.White;

            btnSave.Location =
                new Point(
                    540,
                    445);

            btnSave.Size =
                new Size(
                    110,
                    42);

            btnSave.Text =
                "Save";

            btnSave.Click +=
                btnSave_Click;

            btnCancel.BorderRadius =
                8;

            btnCancel.FillColor =
                Color.FromArgb(
                    100,
                    108,
                    120);

            btnCancel.Font =
                new Font(
                    "Segoe UI",
                    9F,
                    FontStyle.Bold);

            btnCancel.ForeColor =
                Color.White;

            btnCancel.Location =
                new Point(
                    660,
                    445);

            btnCancel.Size =
                new Size(
                    110,
                    42);

            btnCancel.Text =
                "Cancel";

            btnCancel.Click +=
                btnCancel_Click;

            panelMain.Controls.Add(
                lblTitle);

            panelMain.Controls.Add(
                lblEmployeeCode);

            panelMain.Controls.Add(
                txtEmployeeCode);

            panelMain.Controls.Add(
                lblFirstName);

            panelMain.Controls.Add(
                txtFirstName);

            panelMain.Controls.Add(
                lblMiddleName);

            panelMain.Controls.Add(
                txtMiddleName);

            panelMain.Controls.Add(
                lblLastName);

            panelMain.Controls.Add(
                txtLastName);

            panelMain.Controls.Add(
                lblPosition);

            panelMain.Controls.Add(
                cmbPosition);

            panelMain.Controls.Add(
                lblContactNumber);

            panelMain.Controls.Add(
                txtContactNumber);

            panelMain.Controls.Add(
                lblEmailAddress);

            panelMain.Controls.Add(
                txtEmailAddress);

            panelMain.Controls.Add(
                lblDateHired);

            panelMain.Controls.Add(
                dtpDateHired);

            panelMain.Controls.Add(
                lblEmploymentStatus);

            panelMain.Controls.Add(
                cmbEmploymentStatus);

            panelMain.Controls.Add(
                lblAddress);

            panelMain.Controls.Add(
                txtAddress);

            panelMain.Controls.Add(
                lblDateSeparated);

            panelMain.Controls.Add(
                dtpDateSeparated);

            panelMain.Controls.Add(
                lblSeparationReason);

            panelMain.Controls.Add(
                txtSeparationReason);

            panelMain.Controls.Add(
                btnSave);

            panelMain.Controls.Add(
                btnCancel);

            Controls.Add(
                panelMain);

            ClientSize =
                new Size(
                    820,
                    540);

            FormBorderStyle =
                FormBorderStyle.None;

            StartPosition =
                FormStartPosition.CenterParent;

            Text =
                "Edit Employee";

            ResumeLayout(false);
        }

        private void ConfigureLabel(
            Guna.UI2.WinForms.Guna2HtmlLabel label,
            string text,
            int x,
            int y)
        {
            label.BackColor =
                Color.Transparent;

            label.Font =
                new Font(
                    "Segoe UI",
                    9F,
                    FontStyle.Bold);

            label.ForeColor =
                Color.FromArgb(
                    73,
                    83,
                    101);

            label.Location =
                new Point(
                    x,
                    y);

            label.Text =
                text;
        }

        private void ConfigureTextBox(
            Guna.UI2.WinForms.Guna2TextBox textBox,
            int x,
            int y)
        {
            textBox.BorderRadius =
                8;

            textBox.Font =
                new Font(
                    "Segoe UI",
                    9F);

            textBox.Location =
                new Point(
                    x,
                    y);

            textBox.Size =
                new Size(
                    230,
                    40);
        }
    }
}
