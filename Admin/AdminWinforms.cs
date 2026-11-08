using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Larp.winforms.Admin
{
    public partial class AdminWinforms : Form
    {
        // Palette
        private static readonly Color Navy = Color.FromArgb(28, 40, 63);
        private static readonly Color NavyHover = Color.FromArgb(44, 60, 92);
        private static readonly Color Accent = Color.FromArgb(37, 99, 235);   // active menu item
        private static readonly Color Muted = Color.FromArgb(96, 105, 121);
        private static readonly Color Good = Color.FromArgb(46, 107, 69);
        private static readonly Color Warn = Color.FromArgb(210, 130, 20);
        private static readonly Color Bad = Color.Firebrick;

        private readonly HttpClient _httpClient;
        private readonly int _companyId;
        private readonly string _initials;

        public AdminWinforms(HttpClient httpClient, int companyId)
        {
            InitializeComponent();

            _httpClient = httpClient;
            _companyId = companyId;

            string name = string.IsNullOrWhiteSpace(Session.FullName)
                ? "Platform Admin"
                : Session.FullName;

            lblAdminUser.Text = name;
            lblAdminRole.Text = Session.Role == "SuperAdmin" ? "Platform Admin" : Session.Role;
            _initials = GetInitials(name);

            ApplyIcons();
            SetActiveNav(btnNavOverview);

            Load += async (s, e) => await LoadOverviewAsync();
        }

        // ---------------- Sidebar ----------------

        private static string GetInitials(string name)
        {
            var parts = name.Split(' ', StringSplitOptions.RemoveEmptyEntries);

            if (parts.Length == 0) return "A";
            if (parts.Length == 1) return parts[0].Substring(0, 1).ToUpperInvariant();

            return (parts[0].Substring(0, 1) + parts[^1].Substring(0, 1)).ToUpperInvariant();
        }

        // Menu icons are drawn from the Windows icon font, so no image files are needed.
        private void ApplyIcons()
        {
            btnNavOverview.Image = MakeIcon("\uE80F");
            btnNavSubscriptions.Image = MakeIcon("\uE787");
            btnNavPlans.Image = MakeIcon("\uE8EC");
            btnNavTerms.Image = MakeIcon("\uE8A5");
            btnNavBackup.Image = MakeIcon("\uE777");
        }

        private static Image MakeIcon(string glyph)
        {
            var bmp = new Bitmap(20, 20);

            using var g = Graphics.FromImage(bmp);
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;

            using var font = new Font("Segoe MDL2 Assets", 11.5F, GraphicsUnit.Point);
            using var brush = new SolidBrush(Color.White);

            var size = g.MeasureString(glyph, font);
            g.DrawString(glyph, font, brush, (20 - size.Width) / 2f + 1, (20 - size.Height) / 2f);

            return bmp;
        }

        private void SetActiveNav(Guna.UI2.WinForms.Guna2Button active)
        {
            var buttons = new[]
            {
                btnNavOverview,
                btnNavSubscriptions,
                btnNavPlans,
                btnNavTerms,
                btnNavBackup
            };

            foreach (var button in buttons)
            {
                button.FillColor = button == active ? Accent : Color.Transparent;
                button.HoverState.FillColor = button == active ? Accent : NavyHover;
            }
        }

        private void panelAvatar_Paint(object sender, PaintEventArgs e)
        {
            var g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;

            var circle = new Rectangle(0, 0, panelAvatar.Width - 1, panelAvatar.Height - 1);

            using (var fill = new SolidBrush(Accent))
                g.FillEllipse(fill, circle);

            using var font = new Font("Segoe UI", 11F, FontStyle.Bold);
            using var text = new SolidBrush(Color.White);
            using var format = new StringFormat
            {
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center
            };

            g.DrawString(_initials, font, text, circle, format);
        }

        // ---------------- Menu actions ----------------

        private async void BtnNavOverview_Click(object sender, EventArgs e)
        {
            SetActiveNav(btnNavOverview);
            await LoadOverviewAsync();
        }

        private async void BtnRefreshOverview_Click(object sender, EventArgs e)
        {
            await LoadOverviewAsync();
        }

        private async void BtnNavSubscriptions_Click(object sender, EventArgs e)
        {
            SetActiveNav(btnNavSubscriptions);

            using var form = new Subscription(_httpClient);
            form.ShowDialog(this);

            SetActiveNav(btnNavOverview);
            await LoadOverviewAsync();
        }

        private void BtnNavPlans_Click(object sender, EventArgs e)
        {
            SetActiveNav(btnNavPlans);

            using var form = new PlansWinforms(_httpClient);
            form.ShowDialog(this);

            SetActiveNav(btnNavOverview);
        }

        private void BtnNavTerms_Click(object sender, EventArgs e)
        {
            SetActiveNav(btnNavTerms);

            using var form = new TermsWinforms(_httpClient);
            form.ShowDialog(this);

            SetActiveNav(btnNavOverview);
        }

        private void BtnNavBackup_Click(object sender, EventArgs e)
        {
            SetActiveNav(btnNavBackup);

            using var form = new Backup(_httpClient);
            form.ShowDialog(this);

            SetActiveNav(btnNavOverview);
        }

        private void LogoutBtn_Click(object sender, EventArgs e)
        {
            var answer = MessageBox.Show(
                "Log out of your account?",
                "Logout",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (answer != DialogResult.Yes)
                return;

            Close();
        }

        // ---------------- Overview ----------------

        private async Task LoadOverviewAsync()
        {
            btnRefreshOverview.Enabled = false;
            lblOverviewSubtitle.Text = "Loading companies...";

            try
            {
                var overviewTask = _httpClient.GetFromJsonAsync<List<OverviewRow>>("admin/overview");
                var tenantsTask = _httpClient.GetFromJsonAsync<List<TenantInfo>>("admin/tenants");

                await Task.WhenAll(overviewTask, tenantsTask);

                var rows = overviewTask.Result ?? new List<OverviewRow>();

                var tenants = (tenantsTask.Result ?? new List<TenantInfo>())
                    .GroupBy(t => t.TenantId)
                    .ToDictionary(g => g.Key, g => g.First());

                BuildSummaryCards(rows, tenants);
                BuildCompanyCards(rows, tenants);

                lblOverviewSubtitle.Text = rows.Count == 0
                    ? "No companies yet. Add one in Subscriptions."
                    : $"Platform-wide view across {rows.Count} compan{(rows.Count == 1 ? "y" : "ies")}.";
            }
            catch (Exception ex)
            {
                lblOverviewSubtitle.Text = "Could not load the overview.";

                MessageBox.Show(
                    "Could not load the overview.\n\n" + ex.Message,
                    "Admin Dashboard",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            finally
            {
                btnRefreshOverview.Enabled = true;
            }
        }

        private void BuildSummaryCards(
            List<OverviewRow> rows,
            Dictionary<int, TenantInfo> tenants)
        {
            int total = rows.Count;

            int active = rows.Count(r =>
                StatusOf(r, tenants.GetValueOrDefault(r.TenantId)).Text == "Active");

            int notActive = total - active;

            var reachable = rows.Where(r => r.Reachable).ToList();
            decimal salesToday = reachable.Sum(r => r.SalesToday);
            int lowStockCompanies = reachable.Count(r => r.LowStockCount > 0);
            int unreachable = rows.Count - reachable.Count;

            var cards = new List<Control>
            {
                MakeSummaryCard("Companies", total.ToString(), Navy),
                MakeSummaryCard("Active", active.ToString(), Good),
                MakeSummaryCard("Suspended or expired", notActive.ToString(), notActive > 0 ? Warn : Muted),
                MakeSummaryCard("Sales today (all companies)", salesToday.ToString("₱#,##0.00"), Navy),
                MakeSummaryCard("Companies with low stock", $"{lowStockCompanies} / {total}", lowStockCompanies > 0 ? Warn : Good)
            };

            if (unreachable > 0)
                cards.Add(MakeSummaryCard("Unreachable", unreachable.ToString(), Bad));

            ReplaceCards(flowSummary, cards);
        }

        private void BuildCompanyCards(
            List<OverviewRow> rows,
            Dictionary<int, TenantInfo> tenants)
        {
            var cards = rows
                .OrderBy(r => r.Name)
                .Select(r => MakeCompanyCard(r, tenants.GetValueOrDefault(r.TenantId)))
                .ToList();

            ReplaceCards(flowCompanies, cards);
        }

        private static void ReplaceCards(FlowLayoutPanel panel, List<Control> cards)
        {
            panel.SuspendLayout();

            foreach (Control old in panel.Controls.Cast<Control>().ToList())
            {
                panel.Controls.Remove(old);
                old.Dispose();
            }

            panel.Controls.AddRange(cards.ToArray());

            panel.ResumeLayout();
        }

        // ---------------- Card builders ----------------

        private static Guna.UI2.WinForms.Guna2Panel NewCard(Size size)
        {
            var card = new Guna.UI2.WinForms.Guna2Panel
            {
                Size = size,
                BackColor = Color.White,
                BorderRadius = 12,
                Margin = new Padding(0, 0, 18, 18)
            };

            card.ShadowDecoration.Enabled = true;
            card.ShadowDecoration.Depth = 5;

            return card;
        }

        private static Label NewLabel(
            string text,
            Font font,
            Color color,
            Point location,
            Size? fixedSize = null)
        {
            var label = new Label
            {
                Text = text,
                Font = font,
                ForeColor = color,
                BackColor = Color.Transparent,
                Location = location
            };

            if (fixedSize.HasValue)
            {
                label.AutoSize = false;
                label.Size = fixedSize.Value;
                label.AutoEllipsis = true;
            }
            else
            {
                label.AutoSize = true;
            }

            return label;
        }

        private static Control MakeSummaryCard(string title, string value, Color valueColor)
        {
            var card = NewCard(new Size(214, 104));

            card.Controls.Add(NewLabel(
                title,
                new Font("Segoe UI", 9F),
                Muted,
                new Point(16, 14),
                new Size(184, 20)));

            card.Controls.Add(NewLabel(
                value,
                new Font("Segoe UI", 18F, FontStyle.Bold),
                valueColor,
                new Point(14, 48)));

            return card;
        }

        private static Control MakeCompanyCard(OverviewRow row, TenantInfo? info)
        {
            var card = NewCard(new Size(300, 172));
            var status = StatusOf(row, info);

            card.Controls.Add(NewLabel(
                row.Name,
                new Font("Segoe UI Semibold", 12F),
                Navy,
                new Point(18, 14),
                new Size(176, 26)));

            var badge = NewLabel(
                status.Text,
                new Font("Segoe UI", 8.5F, FontStyle.Bold),
                status.Fore,
                new Point(198, 16),
                new Size(84, 22));
            badge.BackColor = status.Back;
            badge.TextAlign = ContentAlignment.MiddleCenter;
            card.Controls.Add(badge);

            string details = row.Code;

            if (!string.IsNullOrWhiteSpace(info?.Plan))
                details += $"  ·  {info!.Plan}";

            if (info?.Expiry != null)
                details += $"  ·  expires {info.Expiry.Value:yyyy-MM-dd}";

            card.Controls.Add(NewLabel(
                details,
                new Font("Segoe UI", 8.5F),
                Muted,
                new Point(18, 44),
                new Size(264, 18)));

            if (!row.Reachable)
            {
                card.Controls.Add(NewLabel(
                    "Unreachable",
                    new Font("Segoe UI", 11F, FontStyle.Bold),
                    Bad,
                    new Point(18, 78)));

                string error = string.IsNullOrWhiteSpace(row.Error)
                    ? "The company database could not be reached."
                    : row.Error!;

                if (error.Length > 150)
                    error = error.Substring(0, 150) + "...";

                var errorLabel = NewLabel(
                    error,
                    new Font("Segoe UI", 8.5F),
                    Bad,
                    new Point(18, 104),
                    new Size(264, 56));
                errorLabel.AutoEllipsis = false;
                card.Controls.Add(errorLabel);

                return card;
            }

            card.Controls.Add(NewLabel(
                "SALES TODAY",
                new Font("Segoe UI", 7.5F, FontStyle.Bold),
                Muted,
                new Point(18, 78)));

            card.Controls.Add(NewLabel(
                row.SalesToday.ToString("₱#,##0.00"),
                new Font("Segoe UI", 16F, FontStyle.Bold),
                Navy,
                new Point(16, 94)));

            bool low = row.LowStockCount > 0;

            card.Controls.Add(NewLabel(
                low ? $"{row.LowStockCount} low-stock item(s)" : "Stock levels OK",
                new Font("Segoe UI", 9F, FontStyle.Bold),
                low ? Warn : Good,
                new Point(18, 140),
                new Size(264, 20)));

            return card;
        }

        private static (string Text, Color Fore, Color Back) StatusOf(OverviewRow row, TenantInfo? info)
        {
            bool expired = info?.Expiry != null && info.Expiry.Value.Date < DateTime.Today;

            if (row.Status is "Suspended" or "Cancelled")
                return (row.Status, Bad, Color.FromArgb(253, 232, 232));

            if (info != null && !info.IsActive)
                return ("Inactive", Bad, Color.FromArgb(253, 232, 232));

            if (expired)
                return ("Expired", Warn, Color.FromArgb(254, 243, 224));

            if (row.Status == "Active")
                return ("Active", Good, Color.FromArgb(228, 242, 233));

            return (row.Status, Muted, Color.FromArgb(236, 238, 241));
        }

        // ---------------- DTOs ----------------

        private class OverviewRow
        {
            public int TenantId { get; set; }
            public string Name { get; set; } = "";
            public string Code { get; set; } = "";
            public string Status { get; set; } = "";
            public bool Reachable { get; set; }
            public decimal SalesToday { get; set; }
            public int LowStockCount { get; set; }
            public string? Error { get; set; }
        }

        private class TenantInfo
        {
            public int TenantId { get; set; }
            public string? Plan { get; set; }
            public DateTime? Expiry { get; set; }
            public bool IsActive { get; set; } = true;
        }
    }
}