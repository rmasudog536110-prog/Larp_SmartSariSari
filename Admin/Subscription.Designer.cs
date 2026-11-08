namespace Larp.winforms.Admin
{
    partial class Subscription
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Label lblSubscriptionsTitle;
        private System.Windows.Forms.Label lblPlansTitle;
        private System.Windows.Forms.DataGridView gridPlans;
        private System.Windows.Forms.DataGridView gridSubscriptions;

        private Guna.UI2.WinForms.Guna2Button btnSuspendTenant;
        private Guna.UI2.WinForms.Guna2Button btnReactivateTenant;
        private Guna.UI2.WinForms.Guna2Button btnNewCompany;
        private Guna.UI2.WinForms.Guna2Button btnUpdateDatabase;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }

            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

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

            lblSubscriptionsTitle = new Label();
            lblPlansTitle = new Label();
            gridPlans = new DataGridView();
            gridSubscriptions = new DataGridView();

            btnSuspendTenant = new Guna.UI2.WinForms.Guna2Button();
            btnReactivateTenant = new Guna.UI2.WinForms.Guna2Button();
            btnNewCompany = new Guna.UI2.WinForms.Guna2Button();
            btnUpdateDatabase = new Guna.UI2.WinForms.Guna2Button();

            ((System.ComponentModel.ISupportInitialize)gridPlans).BeginInit();
            ((System.ComponentModel.ISupportInitialize)gridSubscriptions).BeginInit();
            SuspendLayout();

            // 
            // lblSubscriptionsTitle
            // 
            lblSubscriptionsTitle.AutoSize = true;
            lblSubscriptionsTitle.Font = new Font(
                "Segoe UI",
                19.8000011F,
                FontStyle.Bold,
                GraphicsUnit.Point,
                0);
            lblSubscriptionsTitle.ForeColor = Color.FromArgb(28, 40, 63);
            lblSubscriptionsTitle.Location = new Point(30, 20);
            lblSubscriptionsTitle.Name = "lblSubscriptionsTitle";
            lblSubscriptionsTitle.Size = new Size(354, 46);
            lblSubscriptionsTitle.TabIndex = 0;
            lblSubscriptionsTitle.Text = "Tenant Subscriptions";

            // 
            // lblPlansTitle
            // 
            lblPlansTitle.AutoSize = true;
            lblPlansTitle.Font = new Font(
                "Segoe UI Semibold",
                13F,
                FontStyle.Bold,
                GraphicsUnit.Point,
                0);
            lblPlansTitle.ForeColor = Color.FromArgb(28, 40, 63);
            lblPlansTitle.Location = new Point(30, 78);
            lblPlansTitle.Name = "lblPlansTitle";
            lblPlansTitle.Size = new Size(160, 30);
            lblPlansTitle.TabIndex = 1;
            lblPlansTitle.Text = "Plans & Pricing";

            // 
            // gridPlans
            // 
            gridPlans.AllowUserToAddRows = false;
            gridPlans.AllowUserToDeleteRows = false;
            gridPlans.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            gridPlans.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            gridPlans.BackgroundColor = Color.White;
            gridPlans.ColumnHeadersHeight = 32;
            gridPlans.Location = new Point(30, 115);
            gridPlans.MultiSelect = false;
            gridPlans.Name = "gridPlans";
            gridPlans.ReadOnly = true;
            gridPlans.RowHeadersVisible = false;
            gridPlans.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            gridPlans.Size = new Size(1500, 190);
            gridPlans.TabIndex = 2;

            // 
            // gridSubscriptions
            // 
            gridSubscriptions.AllowUserToAddRows = false;
            gridSubscriptions.AllowUserToDeleteRows = false;
            gridSubscriptions.Anchor =
                AnchorStyles.Top |
                AnchorStyles.Bottom |
                AnchorStyles.Left |
                AnchorStyles.Right;
            gridSubscriptions.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            gridSubscriptions.BackgroundColor = Color.White;
            gridSubscriptions.ColumnHeadersHeight = 32;
            gridSubscriptions.Location = new Point(30, 365);
            gridSubscriptions.MultiSelect = false;
            gridSubscriptions.Name = "gridSubscriptions";
            gridSubscriptions.ReadOnly = true;
            gridSubscriptions.RowHeadersVisible = false;
            gridSubscriptions.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            gridSubscriptions.Size = new Size(1500, 450);
            gridSubscriptions.TabIndex = 3;

            // 
            // btnSuspendTenant
            // 
            btnSuspendTenant.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnSuspendTenant.BorderRadius = 6;
            btnSuspendTenant.CustomizableEdges = customizableEdges1;
            btnSuspendTenant.FillColor = Color.Firebrick;
            btnSuspendTenant.Font = new Font("Segoe UI", 9F);
            btnSuspendTenant.ForeColor = Color.White;
            btnSuspendTenant.Location = new Point(30, 835);
            btnSuspendTenant.Name = "btnSuspendTenant";
            btnSuspendTenant.ShadowDecoration.CustomizableEdges = customizableEdges2;
            btnSuspendTenant.Size = new Size(160, 40);
            btnSuspendTenant.TabIndex = 4;
            btnSuspendTenant.Text = "Suspend Selected";
            btnSuspendTenant.Click += BtnSuspendTenant_Click;

            // 
            // btnReactivateTenant
            // 
            btnReactivateTenant.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnReactivateTenant.BorderRadius = 6;
            btnReactivateTenant.CustomizableEdges = customizableEdges3;
            btnReactivateTenant.FillColor = Color.FromArgb(28, 40, 63);
            btnReactivateTenant.Font = new Font("Segoe UI", 9F);
            btnReactivateTenant.ForeColor = Color.White;
            btnReactivateTenant.Location = new Point(200, 835);
            btnReactivateTenant.Name = "btnReactivateTenant";
            btnReactivateTenant.ShadowDecoration.CustomizableEdges = customizableEdges4;
            btnReactivateTenant.Size = new Size(160, 40);
            btnReactivateTenant.TabIndex = 5;
            btnReactivateTenant.Text = "Reactivate Selected";
            btnReactivateTenant.Click += BtnReactivateTenant_Click;

            // 
            // btnNewCompany
            // 
            btnNewCompany.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnNewCompany.BorderRadius = 6;
            btnNewCompany.CustomizableEdges = customizableEdges5;
            btnNewCompany.FillColor = Color.FromArgb(46, 107, 69);
            btnNewCompany.Font = new Font("Segoe UI", 9F);
            btnNewCompany.ForeColor = Color.White;
            btnNewCompany.Location = new Point(370, 835);
            btnNewCompany.Name = "btnNewCompany";
            btnNewCompany.ShadowDecoration.CustomizableEdges = customizableEdges6;
            btnNewCompany.Size = new Size(160, 40);
            btnNewCompany.TabIndex = 6;
            btnNewCompany.Text = "New Company";
            btnNewCompany.Click += BtnNewCompany_Click;

            // 
            // btnUpdateDatabase
            // 
            btnUpdateDatabase.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnUpdateDatabase.BorderRadius = 6;
            btnUpdateDatabase.CustomizableEdges = customizableEdges7;
            btnUpdateDatabase.FillColor = Color.FromArgb(73, 83, 101);
            btnUpdateDatabase.Font = new Font("Segoe UI", 9F);
            btnUpdateDatabase.ForeColor = Color.White;
            btnUpdateDatabase.Location = new Point(540, 835);
            btnUpdateDatabase.Name = "btnUpdateDatabase";
            btnUpdateDatabase.ShadowDecoration.CustomizableEdges = customizableEdges8;
            btnUpdateDatabase.Size = new Size(160, 40);
            btnUpdateDatabase.TabIndex = 7;
            btnUpdateDatabase.Text = "Update Database";
            btnUpdateDatabase.Click += BtnUpdateDatabase_Click;

            // 
            // Subscription
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(245, 246, 248);

            ClientSize = new Size(1600, 920);

            Controls.Add(lblSubscriptionsTitle);
            Controls.Add(lblPlansTitle);
            Controls.Add(gridPlans);
            Controls.Add(gridSubscriptions);

            Controls.Add(btnSuspendTenant);
            Controls.Add(btnReactivateTenant);
            Controls.Add(btnNewCompany);
            Controls.Add(btnUpdateDatabase);

            Name = "Subscription";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Subscriptions";
            WindowState = FormWindowState.Maximized;

            ((System.ComponentModel.ISupportInitialize)gridPlans).EndInit();
            ((System.ComponentModel.ISupportInitialize)gridSubscriptions).EndInit();

            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
    }
}