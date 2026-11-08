using static Guna.UI2.WinForms.Suite.Descriptions;
using static System.Net.Mime.MediaTypeNames;

namespace Larp.winforms.SmallTenant.Approvals
{
    partial class ApprovalsWinForms
    {
        private System.ComponentModel.IContainer components = null;

        private Guna.UI2.WinForms.Guna2Panel panelHeader;
        private Guna.UI2.WinForms.Guna2Panel panelTable;

        private Guna.UI2.WinForms.Guna2Button btnRefresh;
        private Guna.UI2.WinForms.Guna2Button btnClose;

        private Guna.UI2.WinForms.Guna2DataGridView guna2DataGridView1;
        private Guna.UI2.WinForms.Guna2ProgressIndicator guna2ProgressIndicator1;

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblSubtitle;

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
            components = new System.ComponentModel.Container();

            panelHeader = new Guna.UI2.WinForms.Guna2Panel();
            panelTable = new Guna.UI2.WinForms.Guna2Panel();

            btnRefresh = new Guna.UI2.WinForms.Guna2Button();
            btnClose = new Guna.UI2.WinForms.Guna2Button();

            guna2DataGridView1 = new Guna.UI2.WinForms.Guna2DataGridView();
            guna2ProgressIndicator1 = new Guna.UI2.WinForms.Guna2ProgressIndicator();

            lblTitle = new System.Windows.Forms.Label();
            lblSubtitle = new System.Windows.Forms.Label();

            panelHeader.SuspendLayout();
            panelTable.SuspendLayout();

            ((System.ComponentModel.ISupportInitialize)(guna2DataGridView1)).BeginInit();

            SuspendLayout();

            //
            // ApprovalsWinForms
            //
            AutoScaleDimensions = new System.Drawing.SizeF(6.9F, 13F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            BackColor = System.Drawing.Color.FromArgb(245, 247, 250);
            ClientSize = new System.Drawing.Size(1250, 750);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            Text = "Approvals";

            //
            // panelHeader
            //
            panelHeader.BackColor = System.Drawing.Color.White;
            panelHeader.BorderRadius = 10;
            panelHeader.Dock = System.Windows.Forms.DockStyle.Top;
            panelHeader.Location = new System.Drawing.Point(0, 0);
            panelHeader.Name = "panelHeader";
            panelHeader.Size = new System.Drawing.Size(1250, 115);
            panelHeader.TabIndex = 0;

            //
            // lblTitle
            //
            lblTitle.AutoSize = true;
            lblTitle.Font = new System.Drawing.Font(
                "Segoe UI",
                22F,
                System.Drawing.FontStyle.Bold);
            lblTitle.ForeColor = System.Drawing.Color.FromArgb(31, 41, 55);
            lblTitle.Location = new System.Drawing.Point(30, 20);
            lblTitle.Name = "lblTitle";
            lblTitle.Text = "Approvals";

            //
            // lblSubtitle
            //
            lblSubtitle.AutoSize = true;
            lblSubtitle.Font = new System.Drawing.Font("Segoe UI", 10F);
            lblSubtitle.ForeColor = System.Drawing.Color.FromArgb(107, 114, 128);
            lblSubtitle.Location = new System.Drawing.Point(33, 65);
            lblSubtitle.Name = "lblSubtitle";
            lblSubtitle.Text = "New employees, suppliers and payroll waiting for an Admin.";

            //
            // btnRefresh
            //
            btnRefresh.BorderRadius = 8;
            btnRefresh.FillColor = System.Drawing.Color.FromArgb(107, 114, 128);
            btnRefresh.Font = new System.Drawing.Font(
                "Segoe UI",
                10F,
                System.Drawing.FontStyle.Bold);
            btnRefresh.ForeColor = System.Drawing.Color.White;
            btnRefresh.Location = new System.Drawing.Point(990, 32);
            btnRefresh.Name = "btnRefresh";
            btnRefresh.Size = new System.Drawing.Size(100, 42);
            btnRefresh.Text = "Refresh";
            btnRefresh.Click += btnRefresh_Click;

            //
            // btnClose
            //
            btnClose.BorderRadius = 8;
            btnClose.FillColor = System.Drawing.Color.FromArgb(107, 114, 128);
            btnClose.Font = new System.Drawing.Font(
                "Segoe UI",
                10F,
                System.Drawing.FontStyle.Bold);
            btnClose.ForeColor = System.Drawing.Color.White;
            btnClose.Location = new System.Drawing.Point(1110, 32);
            btnClose.Name = "btnClose";
            btnClose.Size = new System.Drawing.Size(100, 42);
            btnClose.Text = "Close";
            btnClose.Click += btnClose_Click;

            panelHeader.Controls.Add(lblTitle);
            panelHeader.Controls.Add(lblSubtitle);
            panelHeader.Controls.Add(btnRefresh);
            panelHeader.Controls.Add(btnClose);

            //
            // panelTable
            //
            panelTable.BackColor = System.Drawing.Color.White;
            panelTable.BorderRadius = 10;
            panelTable.Dock = System.Windows.Forms.DockStyle.Fill;
            panelTable.Location = new System.Drawing.Point(0, 115);
            panelTable.Name = "panelTable";
            panelTable.Padding = new System.Windows.Forms.Padding(20);
            panelTable.Size = new System.Drawing.Size(1250, 635);
            panelTable.TabIndex = 1;

            //
            // guna2DataGridView1
            //
            guna2DataGridView1.AllowUserToAddRows = false;
            guna2DataGridView1.AllowUserToDeleteRows = false;
            guna2DataGridView1.AllowUserToResizeRows = false;

            guna2DataGridView1.BackgroundColor = System.Drawing.Color.White;
            guna2DataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            guna2DataGridView1.ColumnHeadersBorderStyle =
                System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;

            guna2DataGridView1.ColumnHeadersDefaultCellStyle =
                new System.Windows.Forms.DataGridViewCellStyle
                {
                    BackColor = System.Drawing.Color.FromArgb(248, 250, 252),
                    Font = new System.Drawing.Font(
                        "Segoe UI",
                        9F,
                        System.Drawing.FontStyle.Bold),
                    ForeColor = System.Drawing.Color.FromArgb(55, 65, 81),
                    SelectionBackColor = System.Drawing.Color.FromArgb(248, 250, 252),
                    SelectionForeColor = System.Drawing.Color.FromArgb(55, 65, 81),
                    Padding = new System.Windows.Forms.Padding(5)
                };

            guna2DataGridView1.DefaultCellStyle =
                new System.Windows.Forms.DataGridViewCellStyle
                {
                    BackColor = System.Drawing.Color.White,
                    Font = new System.Drawing.Font("Segoe UI", 9F),
                    ForeColor = System.Drawing.Color.FromArgb(55, 65, 81),
                    SelectionBackColor = System.Drawing.Color.FromArgb(232, 240, 236),
                    SelectionForeColor = System.Drawing.Color.FromArgb(31, 41, 55),
                    Padding = new System.Windows.Forms.Padding(5)
                };

            guna2DataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            guna2DataGridView1.GridColor = System.Drawing.Color.FromArgb(229, 231, 235);
            guna2DataGridView1.RowHeadersVisible = false;
            guna2DataGridView1.RowTemplate.Height = 42;
            guna2DataGridView1.ThemeStyle.AlternatingRowsStyle.BackColor = System.Drawing.Color.White;
            guna2DataGridView1.ThemeStyle.HeaderStyle.Height = 40;
            guna2DataGridView1.ThemeStyle.RowsStyle.Height = 42;

            guna2DataGridView1.Name = "guna2DataGridView1";
            guna2DataGridView1.Size = new System.Drawing.Size(1210, 595);

            guna2DataGridView1.CellContentClick += guna2DataGridView1_CellContentClick;

            //
            // guna2ProgressIndicator1
            //
            guna2ProgressIndicator1.Location = new System.Drawing.Point(590, 250);
            guna2ProgressIndicator1.Name = "guna2ProgressIndicator1";
            guna2ProgressIndicator1.Size = new System.Drawing.Size(60, 60);
            guna2ProgressIndicator1.TabIndex = 10;
            guna2ProgressIndicator1.Visible = false;

            panelTable.Controls.Add(guna2DataGridView1);
            panelTable.Controls.Add(guna2ProgressIndicator1);

            Controls.Add(panelTable);
            Controls.Add(panelHeader);

            panelHeader.ResumeLayout(false);
            panelHeader.PerformLayout();

            panelTable.ResumeLayout(false);

            ((System.ComponentModel.ISupportInitialize)(guna2DataGridView1)).EndInit();

            ResumeLayout(false);
        }
    }
}