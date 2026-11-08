namespace Larp.winforms.SmallTenant.Login
{
    partial class LoginWinforms
    {
        private System.ComponentModel.IContainer components = null;

        private Guna.UI2.WinForms.Guna2Panel panelLeft;
        private Guna.UI2.WinForms.Guna2Panel panelRight;
        private System.Windows.Forms.Label lblBrand;
        private System.Windows.Forms.Label lblTagline;
        private System.Windows.Forms.Label lblSignIn;
        private System.Windows.Forms.Label lblUsername;
        private System.Windows.Forms.Label lblPassword;
        private Guna.UI2.WinForms.Guna2TextBox txtUsername;
        private Guna.UI2.WinForms.Guna2TextBox txtPassword;
        private Guna.UI2.WinForms.Guna2Button btnLogin;
        private Guna.UI2.WinForms.Guna2CheckBox chkShowPassword;
        private System.Windows.Forms.Label lblStatus;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();

            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges9 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges10 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges3 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges4 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges5 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges6 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges7 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges8 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            panelLeft = new Guna.UI2.WinForms.Guna2Panel();
            lblBrand = new Label();
            lblTagline = new Label();
            panelRight = new Guna.UI2.WinForms.Guna2Panel();
            lblSignIn = new Label();
            lblUsername = new Label();
            txtUsername = new Guna.UI2.WinForms.Guna2TextBox();
            lblPassword = new Label();
            txtPassword = new Guna.UI2.WinForms.Guna2TextBox();
            chkShowPassword = new Guna.UI2.WinForms.Guna2CheckBox();
            btnLogin = new Guna.UI2.WinForms.Guna2Button();
            lblStatus = new Label();
            panelLeft.SuspendLayout();
            panelRight.SuspendLayout();
            SuspendLayout();
            // 
            // panelLeft
            // 
            panelLeft.BackColor = Color.FromArgb(28, 40, 63);
            panelLeft.Controls.Add(lblBrand);
            panelLeft.Controls.Add(lblTagline);
            panelLeft.CustomizableEdges = customizableEdges1;
            panelLeft.Dock = DockStyle.Left;
            panelLeft.Location = new Point(0, 0);
            panelLeft.Name = "panelLeft";
            panelLeft.ShadowDecoration.CustomizableEdges = customizableEdges2;
            panelLeft.Size = new Size(360, 520);
            panelLeft.TabIndex = 1;
            // 
            // lblBrand
            // 
            lblBrand.Anchor = AnchorStyles.Top;
            lblBrand.AutoSize = true;
            lblBrand.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
            lblBrand.ForeColor = Color.White;
            lblBrand.Location = new Point(68, 186);
            lblBrand.Name = "lblBrand";
            lblBrand.Size = new Size(238, 46);
            lblBrand.TabIndex = 1;
            lblBrand.Text = "SmartSariSari";
            // 
            // lblTagline
            // 
            lblTagline.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            lblTagline.Font = new Font("Segoe UI", 10F);
            lblTagline.ForeColor = Color.FromArgb(180, 190, 210);
            lblTagline.Location = new Point(34, 232);
            lblTagline.Name = "lblTagline";
            lblTagline.Size = new Size(320, 45);
            lblTagline.TabIndex = 2;
            lblTagline.Text = "Multi-tenant sari-sari store management";
            lblTagline.TextAlign = ContentAlignment.TopCenter;
            // 
            // panelRight
            // 
            panelRight.BackColor = Color.White;
            panelRight.Controls.Add(lblSignIn);
            panelRight.Controls.Add(lblUsername);
            panelRight.Controls.Add(txtUsername);
            panelRight.Controls.Add(lblPassword);
            panelRight.Controls.Add(txtPassword);
            panelRight.Controls.Add(chkShowPassword);
            panelRight.Controls.Add(btnLogin);
            panelRight.Controls.Add(lblStatus);
            panelRight.CustomizableEdges = customizableEdges9;
            panelRight.Dock = DockStyle.Fill;
            panelRight.Location = new Point(360, 0);
            panelRight.Name = "panelRight";
            panelRight.ShadowDecoration.CustomizableEdges = customizableEdges10;
            panelRight.Size = new Size(520, 520);
            panelRight.TabIndex = 0;
            // 
            // lblSignIn
            // 
            lblSignIn.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            lblSignIn.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblSignIn.ForeColor = Color.FromArgb(28, 40, 63);
            lblSignIn.Location = new Point(70, 90);
            lblSignIn.Name = "lblSignIn";
            lblSignIn.Size = new Size(360, 37);
            lblSignIn.TabIndex = 0;
            lblSignIn.Text = "Sign in to your account";
            lblSignIn.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblUsername
            // 
            lblUsername.AutoSize = true;
            lblUsername.Font = new Font("Segoe UI", 9.5F);
            lblUsername.ForeColor = Color.FromArgb(90, 100, 120);
            lblUsername.Location = new Point(72, 165);
            lblUsername.Name = "lblUsername";
            lblUsername.Size = new Size(81, 21);
            lblUsername.TabIndex = 1;
            lblUsername.Text = "Username";
            // 
            // txtUsername
            // 
            txtUsername.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtUsername.BorderColor = Color.FromArgb(210, 214, 222);
            txtUsername.BorderRadius = 8;
            txtUsername.CustomizableEdges = customizableEdges3;
            txtUsername.DefaultText = "";
            txtUsername.FocusedState.BorderColor = Color.FromArgb(28, 40, 63);
            txtUsername.Font = new Font("Segoe UI", 9F);
            txtUsername.Location = new Point(70, 190);
            txtUsername.Margin = new Padding(3, 4, 3, 4);
            txtUsername.Name = "txtUsername";
            txtUsername.PlaceholderText = "e.g. jcruz";
            txtUsername.SelectedText = "";
            txtUsername.ShadowDecoration.CustomizableEdges = customizableEdges4;
            txtUsername.Size = new Size(360, 42);
            txtUsername.TabIndex = 2;
            // 
            // lblPassword
            // 
            lblPassword.AutoSize = true;
            lblPassword.Font = new Font("Segoe UI", 9.5F);
            lblPassword.ForeColor = Color.FromArgb(90, 100, 120);
            lblPassword.Location = new Point(72, 250);
            lblPassword.Name = "lblPassword";
            lblPassword.Size = new Size(76, 21);
            lblPassword.TabIndex = 3;
            lblPassword.Text = "Password";
            // 
            // txtPassword
            // 
            txtPassword.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtPassword.BorderColor = Color.FromArgb(210, 214, 222);
            txtPassword.BorderRadius = 8;
            txtPassword.CustomizableEdges = customizableEdges5;
            txtPassword.DefaultText = "";
            txtPassword.FocusedState.BorderColor = Color.FromArgb(28, 40, 63);
            txtPassword.Font = new Font("Segoe UI", 9F);
            txtPassword.Location = new Point(70, 275);
            txtPassword.Margin = new Padding(3, 4, 3, 4);
            txtPassword.Name = "txtPassword";
            txtPassword.PasswordChar = '●';
            txtPassword.PlaceholderText = "";
            txtPassword.SelectedText = "";
            txtPassword.ShadowDecoration.CustomizableEdges = customizableEdges6;
            txtPassword.Size = new Size(360, 42);
            txtPassword.TabIndex = 4;
            // 
            // chkShowPassword
            // 
            chkShowPassword.CheckedState.BorderRadius = 0;
            chkShowPassword.CheckedState.BorderThickness = 0;
            chkShowPassword.Font = new Font("Segoe UI", 8.5F);
            chkShowPassword.Location = new Point(70, 325);
            chkShowPassword.Name = "chkShowPassword";
            chkShowPassword.Size = new Size(160, 24);
            chkShowPassword.TabIndex = 5;
            chkShowPassword.Text = "Show password";
            chkShowPassword.UncheckedState.BorderRadius = 0;
            chkShowPassword.UncheckedState.BorderThickness = 0;
            chkShowPassword.CheckedChanged += chkShowPassword_CheckedChanged;
            // 
            // btnLogin
            // 
            btnLogin.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            btnLogin.BorderRadius = 8;
            btnLogin.CustomizableEdges = customizableEdges7;
            btnLogin.FillColor = Color.FromArgb(28, 40, 63);
            btnLogin.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            btnLogin.ForeColor = Color.White;
            btnLogin.Location = new Point(70, 375);
            btnLogin.Name = "btnLogin";
            btnLogin.ShadowDecoration.CustomizableEdges = customizableEdges8;
            btnLogin.Size = new Size(360, 46);
            btnLogin.TabIndex = 6;
            btnLogin.Text = "Log In";
            btnLogin.Click += btnLogin_Click;
            // 
            // lblStatus
            // 
            lblStatus.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            lblStatus.Font = new Font("Segoe UI", 9F);
            lblStatus.ForeColor = Color.Firebrick;
            lblStatus.Location = new Point(70, 430);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(360, 40);
            lblStatus.TabIndex = 7;
            // 
            // LoginWinforms
            // 
            AcceptButton = btnLogin;
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(880, 520);
            Controls.Add(panelRight);
            Controls.Add(panelLeft);
            FormBorderStyle = FormBorderStyle.None;
            MinimumSize = new Size(800, 500);
            Name = "LoginWinforms";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Larp SmartSariSari - Sign In";
            panelLeft.ResumeLayout(false);
            panelLeft.PerformLayout();
            panelRight.ResumeLayout(false);
            panelRight.PerformLayout();
            ResumeLayout(false);
        }
    }
}
