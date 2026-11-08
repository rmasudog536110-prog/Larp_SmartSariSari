namespace Larp.winforms.Admin
{
    partial class Backup
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Label lblBackupTitle;
        private System.Windows.Forms.ComboBox cmbBackupTenant;
        private Guna.UI2.WinForms.Guna2Button btnBackupNow;
        private Guna.UI2.WinForms.Guna2Button btnRestoreSelected;
        private System.Windows.Forms.DataGridView gridBackups;
        private System.Windows.Forms.Label lblBackupStatus;

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
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges3 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges4 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            lblBackupTitle = new Label();
            cmbBackupTenant = new ComboBox();
            btnBackupNow = new Guna.UI2.WinForms.Guna2Button();
            btnRestoreSelected = new Guna.UI2.WinForms.Guna2Button();
            gridBackups = new DataGridView();
            lblBackupStatus = new Label();
            ((System.ComponentModel.ISupportInitialize)gridBackups).BeginInit();
            SuspendLayout();
            // 
            // lblBackupTitle
            // 
            lblBackupTitle.AutoSize = true;
            lblBackupTitle.Font = new Font("Segoe UI", 15F, FontStyle.Bold);
            lblBackupTitle.ForeColor = Color.FromArgb(28, 40, 63);
            lblBackupTitle.Location = new Point(30, 27);
            lblBackupTitle.Name = "lblBackupTitle";
            lblBackupTitle.Size = new Size(247, 35);
            lblBackupTitle.TabIndex = 0;
            lblBackupTitle.Text = "Backup and Restore";
            // 
            // cmbBackupTenant
            // 
            cmbBackupTenant.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbBackupTenant.Location = new Point(30, 65);
            cmbBackupTenant.Name = "cmbBackupTenant";
            cmbBackupTenant.Size = new Size(300, 28);
            cmbBackupTenant.TabIndex = 1;
            // 
            // btnBackupNow
            // 
            btnBackupNow.BorderRadius = 6;
            btnBackupNow.CustomizableEdges = customizableEdges1;
            btnBackupNow.FillColor = Color.FromArgb(28, 40, 63);
            btnBackupNow.Font = new Font("Segoe UI", 9F);
            btnBackupNow.ForeColor = Color.White;
            btnBackupNow.Location = new Point(350, 62);
            btnBackupNow.Name = "btnBackupNow";
            btnBackupNow.ShadowDecoration.CustomizableEdges = customizableEdges2;
            btnBackupNow.Size = new Size(130, 36);
            btnBackupNow.TabIndex = 2;
            btnBackupNow.Text = "Backup Now";
            btnBackupNow.Click += BtnBackupNow_Click;
            // 
            // btnRestoreSelected
            // 
            btnRestoreSelected.BorderRadius = 6;
            btnRestoreSelected.CustomizableEdges = customizableEdges3;
            btnRestoreSelected.FillColor = Color.Firebrick;
            btnRestoreSelected.Font = new Font("Segoe UI", 9F);
            btnRestoreSelected.ForeColor = Color.White;
            btnRestoreSelected.Location = new Point(490, 62);
            btnRestoreSelected.Name = "btnRestoreSelected";
            btnRestoreSelected.Padding = new Padding(2, 5, 2, 2);
            btnRestoreSelected.ShadowDecoration.CustomizableEdges = customizableEdges4;
            btnRestoreSelected.Size = new Size(190, 36);
            btnRestoreSelected.TabIndex = 3;
            btnRestoreSelected.Text = "Restore Selected Backup";
            btnRestoreSelected.Click += BtnRestoreSelected_Click;
            // 
            // gridBackups
            // 
            gridBackups.AllowUserToAddRows = false;
            gridBackups.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            gridBackups.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            gridBackups.BackgroundColor = Color.White;
            gridBackups.ColumnHeadersHeight = 29;
            gridBackups.Location = new Point(30, 115);
            gridBackups.MultiSelect = false;
            gridBackups.Name = "gridBackups";
            gridBackups.ReadOnly = true;
            gridBackups.RowHeadersWidth = 51;
            gridBackups.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            gridBackups.Size = new Size(1120, 450);
            gridBackups.TabIndex = 4;
            // 
            // lblBackupStatus
            // 
            lblBackupStatus.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            lblBackupStatus.Font = new Font("Segoe UI", 9F);
            lblBackupStatus.Location = new Point(30, 585);
            lblBackupStatus.Name = "lblBackupStatus";
            lblBackupStatus.Size = new Size(1120, 40);
            lblBackupStatus.TabIndex = 5;
            // 
            // Backup
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(245, 246, 248);
            ClientSize = new Size(1180, 650);
            Controls.Add(lblBackupTitle);
            Controls.Add(cmbBackupTenant);
            Controls.Add(btnBackupNow);
            Controls.Add(btnRestoreSelected);
            Controls.Add(gridBackups);
            Controls.Add(lblBackupStatus);
            Name = "Backup";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Backup & Restore";
            ((System.ComponentModel.ISupportInitialize)gridBackups).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
    }
}