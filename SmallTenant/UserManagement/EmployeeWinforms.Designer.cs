namespace Larp.winforms.SmallTenant.UserManagement
{
    partial class EmployeeWinforms
    {
        private System.ComponentModel.IContainer components = null;

        private Guna.UI2.WinForms.Guna2Panel headerPanel;
        private Guna.UI2.WinForms.Guna2Panel contentPanel;

        private Guna.UI2.WinForms.Guna2HtmlLabel lblTitle;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblSubtitle;

        private Guna.UI2.WinForms.Guna2TextBox guna2TextBox1;
        private Guna.UI2.WinForms.Guna2Button btnAddEmployee;
        private Guna.UI2.WinForms.Guna2Button btnRefresh;

        private Guna.UI2.WinForms.Guna2Panel cardTotal;
        private Guna.UI2.WinForms.Guna2Panel cardActive;
        private Guna.UI2.WinForms.Guna2Panel cardResigned;
        private Guna.UI2.WinForms.Guna2Panel cardFired;

        private Guna.UI2.WinForms.Guna2HtmlLabel lblTotalTitle;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblTotalEmployees;

        private Guna.UI2.WinForms.Guna2HtmlLabel lblActiveTitle;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblActiveEmployees;

        private Guna.UI2.WinForms.Guna2HtmlLabel lblResignedTitle;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblResignedEmployees;

        private Guna.UI2.WinForms.Guna2HtmlLabel lblFiredTitle;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblFiredEmployees;

        private Guna.UI2.WinForms.Guna2DataGridView guna2DataGridView1;
        private Guna.UI2.WinForms.Guna2ProgressIndicator guna2ProgressIndicator1;

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
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges3 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges4 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges5 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges6 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges16 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges17 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges9 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges10 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges11 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges12 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges13 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges14 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges15 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges7 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges8 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            headerPanel = new Guna.UI2.WinForms.Guna2Panel();
            lblTitle = new Guna.UI2.WinForms.Guna2HtmlLabel();
            lblSubtitle = new Guna.UI2.WinForms.Guna2HtmlLabel();
            btnAddEmployee = new Guna.UI2.WinForms.Guna2Button();
            btnRefresh = new Guna.UI2.WinForms.Guna2Button();
            contentPanel = new Guna.UI2.WinForms.Guna2Panel();
            btnClose = new Guna.UI2.WinForms.Guna2Button();
            cardResigned = new Guna.UI2.WinForms.Guna2Panel();
            cardFired = new Guna.UI2.WinForms.Guna2Panel();
            guna2DataGridView1 = new Guna.UI2.WinForms.Guna2DataGridView();
            codeColumn = new DataGridViewTextBoxColumn();
            nameColumn = new DataGridViewTextBoxColumn();
            positionColumn = new DataGridViewTextBoxColumn();
            contactColumn = new DataGridViewTextBoxColumn();
            statusColumn = new DataGridViewTextBoxColumn();
            hiredColumn = new DataGridViewTextBoxColumn();
            roleColumn = new DataGridViewTextBoxColumn();
            editColumn = new DataGridViewButtonColumn();
            deleteColumn = new DataGridViewButtonColumn();
            loginColumn = new DataGridViewButtonColumn();
            benefitsColumn = new DataGridViewButtonColumn();
            guna2ProgressIndicator1 = new Guna.UI2.WinForms.Guna2ProgressIndicator();
            lblTotalTitle = new Guna.UI2.WinForms.Guna2HtmlLabel();
            lblTotalEmployees = new Guna.UI2.WinForms.Guna2HtmlLabel();
            lblActiveTitle = new Guna.UI2.WinForms.Guna2HtmlLabel();
            lblActiveEmployees = new Guna.UI2.WinForms.Guna2HtmlLabel();
            lblResignedTitle = new Guna.UI2.WinForms.Guna2HtmlLabel();
            lblResignedEmployees = new Guna.UI2.WinForms.Guna2HtmlLabel();
            lblFiredTitle = new Guna.UI2.WinForms.Guna2HtmlLabel();
            lblFiredEmployees = new Guna.UI2.WinForms.Guna2HtmlLabel();
            guna2TextBox1 = new Guna.UI2.WinForms.Guna2TextBox();
            headerPanel.SuspendLayout();
            contentPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)guna2DataGridView1).BeginInit();
            SuspendLayout();
            // 
            // headerPanel
            // 
            headerPanel.BackColor = Color.White;
            headerPanel.BorderRadius = 12;
            headerPanel.Controls.Add(lblTitle);
            headerPanel.Controls.Add(lblSubtitle);
            headerPanel.CustomizableEdges = customizableEdges1;
            headerPanel.Dock = DockStyle.Top;
            headerPanel.Location = new Point(0, 0);
            headerPanel.Name = "headerPanel";
            headerPanel.ShadowDecoration.CustomizableEdges = customizableEdges2;
            headerPanel.Size = new Size(1200, 95);
            headerPanel.TabIndex = 1;
            // 
            // lblTitle
            // 
            lblTitle.BackColor = Color.Transparent;
            lblTitle.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
            lblTitle.ForeColor = Color.FromArgb(28, 40, 63);
            lblTitle.Location = new Point(25, 15);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(370, 47);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "Employee Management";
            // 
            // lblSubtitle
            // 
            lblSubtitle.BackColor = Color.Transparent;
            lblSubtitle.Font = new Font("Segoe UI", 9F);
            lblSubtitle.ForeColor = Color.FromArgb(96, 105, 121);
            lblSubtitle.Location = new Point(27, 55);
            lblSubtitle.Name = "lblSubtitle";
            lblSubtitle.Size = new Size(292, 22);
            lblSubtitle.TabIndex = 1;
            lblSubtitle.Text = "Manage employees and employment status";
            // 
            // btnAddEmployee
            // 
            btnAddEmployee.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnAddEmployee.BorderRadius = 8;
            btnAddEmployee.CustomizableEdges = customizableEdges3;
            btnAddEmployee.FillColor = Color.FromArgb(46, 107, 69);
            btnAddEmployee.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnAddEmployee.ForeColor = Color.White;
            btnAddEmployee.Location = new Point(760, 116);
            btnAddEmployee.Name = "btnAddEmployee";
            btnAddEmployee.Padding = new Padding(0, 9, 0, 0);
            btnAddEmployee.ShadowDecoration.CustomizableEdges = customizableEdges4;
            btnAddEmployee.Size = new Size(92, 42);
            btnAddEmployee.TabIndex = 2;
            btnAddEmployee.Text = "+ Add Employee";
            btnAddEmployee.Click += btnAddEmployee_Click;
            // 
            // btnRefresh
            // 
            btnRefresh.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnRefresh.BorderRadius = 8;
            btnRefresh.CustomizableEdges = customizableEdges5;
            btnRefresh.FillColor = Color.FromArgb(73, 83, 101);
            btnRefresh.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnRefresh.ForeColor = Color.White;
            btnRefresh.Location = new Point(897, 116);
            btnRefresh.Name = "btnRefresh";
            btnRefresh.ShadowDecoration.CustomizableEdges = customizableEdges6;
            btnRefresh.Size = new Size(100, 42);
            btnRefresh.TabIndex = 3;
            btnRefresh.Text = "Refresh";
            btnRefresh.Click += btnRefresh_Click;
            // 
            // contentPanel
            // 
            contentPanel.BackColor = Color.FromArgb(232, 233, 236);
            contentPanel.Controls.Add(guna2TextBox1);
            contentPanel.Controls.Add(btnClose);
            contentPanel.Controls.Add(btnRefresh);
            contentPanel.Controls.Add(btnAddEmployee);
            contentPanel.Controls.Add(cardResigned);
            contentPanel.Controls.Add(cardFired);
            contentPanel.Controls.Add(guna2DataGridView1);
            contentPanel.Controls.Add(guna2ProgressIndicator1);
            contentPanel.CustomizableEdges = customizableEdges16;
            contentPanel.Dock = DockStyle.Fill;
            contentPanel.Location = new Point(0, 95);
            contentPanel.Name = "contentPanel";
            contentPanel.Padding = new Padding(20);
            contentPanel.ShadowDecoration.CustomizableEdges = customizableEdges17;
            contentPanel.Size = new Size(1200, 705);
            contentPanel.TabIndex = 0;
            // 
            // btnClose
            // 
            btnClose.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnClose.BorderRadius = 8;
            btnClose.CustomizableEdges = customizableEdges9;
            btnClose.DisabledState.BorderColor = Color.DarkGray;
            btnClose.DisabledState.CustomBorderColor = Color.DarkGray;
            btnClose.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            btnClose.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            btnClose.FillColor = Color.FromArgb(210, 75, 70);
            btnClose.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnClose.ForeColor = Color.White;
            btnClose.Location = new Point(1032, 118);
            btnClose.Margin = new Padding(3, 4, 3, 4);
            btnClose.Name = "btnClose";
            btnClose.ShadowDecoration.CustomizableEdges = customizableEdges10;
            btnClose.Size = new Size(103, 42);
            btnClose.TabIndex = 5;
            btnClose.Text = "Close";
            btnClose.Click += btnClose_Click;
            // 
            // cardResigned
            // 
            cardResigned.CustomizableEdges = customizableEdges11;
            cardResigned.Location = new Point(0, 0);
            cardResigned.Name = "cardResigned";
            cardResigned.ShadowDecoration.CustomizableEdges = customizableEdges12;
            cardResigned.Size = new Size(200, 100);
            cardResigned.TabIndex = 3;
            // 
            // cardFired
            // 
            cardFired.CustomizableEdges = customizableEdges13;
            cardFired.Location = new Point(0, 0);
            cardFired.Name = "cardFired";
            cardFired.ShadowDecoration.CustomizableEdges = customizableEdges14;
            cardFired.Size = new Size(200, 100);
            cardFired.TabIndex = 4;
            // 
            // guna2DataGridView1
            // 
            guna2DataGridView1.AllowUserToAddRows = false;
            guna2DataGridView1.AllowUserToDeleteRows = false;
            guna2DataGridView1.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = Color.White;
            guna2DataGridView1.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            guna2DataGridView1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            guna2DataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            guna2DataGridView1.ColumnHeadersHeight = 40;
            guna2DataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            guna2DataGridView1.Columns.AddRange(new DataGridViewColumn[] { codeColumn, nameColumn, positionColumn, contactColumn, statusColumn, hiredColumn, roleColumn, editColumn, deleteColumn, loginColumn, benefitsColumn });
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = Color.White;
            dataGridViewCellStyle3.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle3.ForeColor = Color.FromArgb(28, 40, 63);
            dataGridViewCellStyle3.SelectionBackColor = Color.FromArgb(232, 233, 236);
            dataGridViewCellStyle3.SelectionForeColor = Color.FromArgb(28, 40, 63);
            dataGridViewCellStyle3.WrapMode = DataGridViewTriState.False;
            guna2DataGridView1.DefaultCellStyle = dataGridViewCellStyle3;
            guna2DataGridView1.GridColor = Color.FromArgb(232, 233, 236);
            guna2DataGridView1.Location = new Point(23, 183);
            guna2DataGridView1.Name = "guna2DataGridView1";
            guna2DataGridView1.RowHeadersVisible = false;
            guna2DataGridView1.RowHeadersWidth = 51;
            guna2DataGridView1.RowTemplate.Height = 40;
            guna2DataGridView1.Size = new Size(1115, 455);
            guna2DataGridView1.TabIndex = 5;
            guna2DataGridView1.ThemeStyle.AlternatingRowsStyle.BackColor = Color.White;
            guna2DataGridView1.ThemeStyle.GridColor = Color.FromArgb(232, 233, 236);
            guna2DataGridView1.ThemeStyle.HeaderStyle.BackColor = Color.Empty;
            guna2DataGridView1.ThemeStyle.HeaderStyle.Font = null;
            guna2DataGridView1.ThemeStyle.HeaderStyle.ForeColor = Color.Empty;
            guna2DataGridView1.ThemeStyle.HeaderStyle.Height = 40;
            guna2DataGridView1.ThemeStyle.RowsStyle.Font = new Font("Segoe UI", 9F);
            guna2DataGridView1.ThemeStyle.RowsStyle.ForeColor = Color.FromArgb(28, 40, 63);
            guna2DataGridView1.ThemeStyle.RowsStyle.Height = 40;
            guna2DataGridView1.ThemeStyle.RowsStyle.SelectionBackColor = Color.FromArgb(232, 233, 236);
            guna2DataGridView1.ThemeStyle.RowsStyle.SelectionForeColor = Color.FromArgb(28, 40, 63);
            // 
            // codeColumn
            // 
            codeColumn.HeaderText = "Employee Code";
            codeColumn.MinimumWidth = 6;
            codeColumn.Name = "codeColumn";
            // 
            // nameColumn
            // 
            nameColumn.HeaderText = "Employee Name";
            nameColumn.MinimumWidth = 6;
            nameColumn.Name = "nameColumn";
            // 
            // positionColumn
            // 
            positionColumn.HeaderText = "Position";
            positionColumn.MinimumWidth = 6;
            positionColumn.Name = "positionColumn";
            // 
            // contactColumn
            // 
            contactColumn.HeaderText = "Contact Number";
            contactColumn.MinimumWidth = 6;
            contactColumn.Name = "contactColumn";
            // 
            // statusColumn
            // 
            statusColumn.HeaderText = "Status";
            statusColumn.MinimumWidth = 6;
            statusColumn.Name = "Status";
            // 
            // hiredColumn
            // 
            hiredColumn.HeaderText = "Date Hired";
            hiredColumn.MinimumWidth = 6;
            hiredColumn.Name = "hiredColumn";
            // 
            // roleColumn
            // 
            roleColumn.HeaderText = "Login Role";
            roleColumn.MinimumWidth = 6;
            roleColumn.Name = "roleColumn";
            // 
            // editColumn
            // 
            editColumn.HeaderText = "";
            editColumn.MinimumWidth = 6;
            editColumn.Name = "editColumn";
            editColumn.Text = "Edit";
            editColumn.UseColumnTextForButtonValue = true;
            // 
            // deleteColumn
            // 
            deleteColumn.HeaderText = "";
            deleteColumn.MinimumWidth = 6;
            deleteColumn.Name = "deleteColumn";
            deleteColumn.Text = "Deactivate";
            deleteColumn.UseColumnTextForButtonValue = false;
            // 
            // loginColumn
            // 
            loginColumn.HeaderText = "";
            loginColumn.MinimumWidth = 6;
            loginColumn.Name = "loginColumn";
            loginColumn.Text = "Create Login";
            loginColumn.UseColumnTextForButtonValue = true;
            //
            //Benefits Column
            //
            benefitsColumn.HeaderText = "";
            benefitsColumn.MinimumWidth = 6;
            benefitsColumn.Name = "benefitsColumn";
            benefitsColumn.Text = "Benefits";
            benefitsColumn.UseColumnTextForButtonValue = true;
            // 
            // guna2ProgressIndicator1
            // 
            guna2ProgressIndicator1.AutoStart = true;
            guna2ProgressIndicator1.CircleSize = 1F;
            guna2ProgressIndicator1.Location = new Point(548, 125);
            guna2ProgressIndicator1.Name = "guna2ProgressIndicator1";
            guna2ProgressIndicator1.ShadowDecoration.CustomizableEdges = customizableEdges15;
            guna2ProgressIndicator1.Size = new Size(35, 35);
            guna2ProgressIndicator1.TabIndex = 6;
            guna2ProgressIndicator1.Visible = false;
            // 
            // lblTotalTitle
            // 
            lblTotalTitle.BackColor = Color.Transparent;
            lblTotalTitle.Location = new Point(0, 0);
            lblTotalTitle.Name = "lblTotalTitle";
            lblTotalTitle.Size = new Size(3, 2);
            lblTotalTitle.TabIndex = 0;
            lblTotalTitle.Text = null;
            // 
            // lblTotalEmployees
            // 
            lblTotalEmployees.BackColor = Color.Transparent;
            lblTotalEmployees.Location = new Point(0, 0);
            lblTotalEmployees.Name = "lblTotalEmployees";
            lblTotalEmployees.Size = new Size(3, 2);
            lblTotalEmployees.TabIndex = 0;
            lblTotalEmployees.Text = null;
            // 
            // lblActiveTitle
            // 
            lblActiveTitle.BackColor = Color.Transparent;
            lblActiveTitle.Location = new Point(0, 0);
            lblActiveTitle.Name = "lblActiveTitle";
            lblActiveTitle.Size = new Size(3, 2);
            lblActiveTitle.TabIndex = 0;
            lblActiveTitle.Text = null;
            // 
            // lblActiveEmployees
            // 
            lblActiveEmployees.BackColor = Color.Transparent;
            lblActiveEmployees.Location = new Point(0, 0);
            lblActiveEmployees.Name = "lblActiveEmployees";
            lblActiveEmployees.Size = new Size(3, 2);
            lblActiveEmployees.TabIndex = 0;
            lblActiveEmployees.Text = null;
            // 
            // lblResignedTitle
            // 
            lblResignedTitle.BackColor = Color.Transparent;
            lblResignedTitle.Location = new Point(0, 0);
            lblResignedTitle.Name = "lblResignedTitle";
            lblResignedTitle.Size = new Size(3, 2);
            lblResignedTitle.TabIndex = 0;
            lblResignedTitle.Text = null;
            // 
            // lblResignedEmployees
            // 
            lblResignedEmployees.BackColor = Color.Transparent;
            lblResignedEmployees.Location = new Point(0, 0);
            lblResignedEmployees.Name = "lblResignedEmployees";
            lblResignedEmployees.Size = new Size(3, 2);
            lblResignedEmployees.TabIndex = 0;
            lblResignedEmployees.Text = null;
            // 
            // lblFiredTitle
            // 
            lblFiredTitle.BackColor = Color.Transparent;
            lblFiredTitle.Location = new Point(0, 0);
            lblFiredTitle.Name = "lblFiredTitle";
            lblFiredTitle.Size = new Size(3, 2);
            lblFiredTitle.TabIndex = 0;
            lblFiredTitle.Text = null;
            // 
            // lblFiredEmployees
            // 
            lblFiredEmployees.BackColor = Color.Transparent;
            lblFiredEmployees.Location = new Point(0, 0);
            lblFiredEmployees.Name = "lblFiredEmployees";
            lblFiredEmployees.Size = new Size(3, 2);
            lblFiredEmployees.TabIndex = 0;
            lblFiredEmployees.Text = null;
            // 
            // guna2TextBox1
            // 
            guna2TextBox1.BorderRadius = 8;
            guna2TextBox1.CustomizableEdges = customizableEdges7;
            guna2TextBox1.DefaultText = "";
            guna2TextBox1.Font = new Font("Segoe UI", 9F);
            guna2TextBox1.Location = new Point(20, 118);
            guna2TextBox1.Margin = new Padding(3, 4, 3, 4);
            guna2TextBox1.Name = "guna2TextBox1";
            guna2TextBox1.PlaceholderText = "Search employee...";
            guna2TextBox1.SelectedText = "";
            guna2TextBox1.ShadowDecoration.CustomizableEdges = customizableEdges8;
            guna2TextBox1.Size = new Size(350, 40);
            guna2TextBox1.TabIndex = 0;
            guna2TextBox1.TextChanged += guna2TextBox1_TextChanged;
            // 
            // EmployeeWinforms
            // 
            ClientSize = new Size(1200, 800);
            Controls.Add(contentPanel);
            Controls.Add(headerPanel);
            FormBorderStyle = FormBorderStyle.None;
            MinimumSize = new Size(1100, 700);
            Name = "EmployeeWinforms";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Employee Management";
            WindowState = FormWindowState.Maximized;
            Load += EmployeeWinforms_Load;
            headerPanel.ResumeLayout(false);
            headerPanel.PerformLayout();
            contentPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)guna2DataGridView1).EndInit();
            ResumeLayout(false);
        }

        private void ConfigureCard(
            Guna.UI2.WinForms.Guna2Panel panel,
            Point location,
            string title,
            Guna.UI2.WinForms.Guna2HtmlLabel titleLabel,
            Guna.UI2.WinForms.Guna2HtmlLabel valueLabel)
        {
            panel.BackColor =
                Color.White;

            panel.BorderRadius =
                12;

            panel.Location =
                location;

            panel.Size =
                new Size(240, 105);

            panel.ShadowDecoration.Depth =
                5;

            panel.ShadowDecoration.Enabled =
                true;

            titleLabel.BackColor =
                Color.Transparent;

            titleLabel.Font =
                new Font(
                    "Segoe UI",
                    9F);

            titleLabel.ForeColor =
                Color.FromArgb(
                    96,
                    105,
                    121);

            titleLabel.Location =
                new Point(18, 15);

            titleLabel.Text =
                title;

            valueLabel.BackColor =
                Color.Transparent;

            valueLabel.Font =
                new Font(
                    "Segoe UI",
                    20F,
                    FontStyle.Bold);

            valueLabel.ForeColor =
                Color.FromArgb(
                    28,
                    40,
                    63);

            valueLabel.Location =
                new Point(18, 42);

            valueLabel.Text =
                "0";

            panel.Controls.Add(
                titleLabel);

            panel.Controls.Add(
                valueLabel);
        }

        private DataGridViewTextBoxColumn codeColumn;
        private DataGridViewTextBoxColumn nameColumn;
        private DataGridViewTextBoxColumn positionColumn;
        private DataGridViewTextBoxColumn contactColumn;
        private DataGridViewTextBoxColumn statusColumn;
        private DataGridViewTextBoxColumn roleColumn;
        private DataGridViewButtonColumn loginColumn;
        private DataGridViewButtonColumn benefitsColumn;
        private DataGridViewTextBoxColumn hiredColumn;
        private DataGridViewButtonColumn editColumn;
        private DataGridViewButtonColumn deleteColumn;
        private Guna.UI2.WinForms.Guna2Button btnClose;
    }
}
