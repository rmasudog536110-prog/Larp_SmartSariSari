using System;
using System.Drawing;
using System.Windows.Forms;
using Guna.UI2.WinForms;

namespace Larp.winforms
{
    /// <summary>
    /// Recolors an existing form to the navy palette at runtime.
    /// It only changes colors: sizes, radii, shadows, fonts, layout and events are untouched.
    /// Call AppTheme.Apply(this) right after InitializeComponent().
    /// </summary>
    public static class AppTheme
    {
        // ---- Palette (your Shades / Tints / Tones) ----
        public static readonly Color Navy = Color.FromArgb(28, 40, 63);          // baseNavyBlue
        public static readonly Color NavyDark = Color.FromArgb(25, 36, 57);      // darkMidnightBlue
        public static readonly Color NightNavy = Color.FromArgb(14, 20, 32);     // nightNavyBlue
        public static readonly Color NavyHover = Color.FromArgb(50, 62, 82);     // softSlateBlue
        public static readonly Color Dusky = Color.FromArgb(73, 83, 101);        // duskyBlue
        public static readonly Color Slate = Color.FromArgb(96, 105, 121);       // softDenimBlue
        public static readonly Color SlateLight = Color.FromArgb(164, 169, 178); // silverBlueGray
        public static readonly Color Border = Color.FromArgb(210, 212, 217);     // iceBlueSurfaceGray
        public static readonly Color Background = Color.FromArgb(232, 233, 236); // offWhiteBlueTint
        public static readonly Color Surface = Color.White;                      // pureWhite

        public static readonly Color TextPrimary = Navy;
        public static readonly Color TextMuted = Slate;
        public static readonly Color TextOnDark = Color.White;
        public static readonly Color TextOnDarkMuted = SlateLight;

        // ---- Status colors (not in your palette; muted to sit well with navy) ----
        public static readonly Color Success = Color.FromArgb(46, 125, 90);
        public static readonly Color Warning = Color.FromArgb(217, 138, 20);
        public static readonly Color Danger = Color.FromArgb(192, 57, 43);

        private static readonly Color SuccessHover = Color.FromArgb(38, 105, 76);
        private static readonly Color SuccessPressed = Color.FromArgb(30, 85, 62);
        private static readonly Color DangerHover = Color.FromArgb(168, 50, 38);
        private static readonly Color DangerPressed = Color.FromArgb(140, 40, 30);

        public static readonly Color[] ChartPalette =
        {
            Color.FromArgb(28, 40, 63),
            Color.FromArgb(96, 105, 121),
            Color.FromArgb(164, 169, 178),
            Color.FromArgb(73, 83, 101),
            Color.FromArgb(142, 148, 159)
        };

        private enum ButtonRole
        {
            Primary, Secondary, Muted, Success, Danger, Nav, Chip, Accent
        }

        // Optional: set a control's Tag to force a button style:
        // "primary", "secondary", "success", "danger"

        public static void Apply(Form form)
        {
            form.BackColor = Background;
            form.ForeColor = TextPrimary;

            foreach (Control child in form.Controls)
            {
                Style(child);
            }
        }

        private static void Style(Control c)
        {
            if (c is Guna2CustomGradientPanel customGradient)
            {
                customGradient.FillColor = Navy;
                customGradient.FillColor2 = NavyHover;
                customGradient.FillColor3 = NavyDark;
                customGradient.FillColor4 = Navy;
            }
            else if (c is Guna2GradientPanel gradient)
            {
                gradient.FillColor = NavyHover;
                gradient.FillColor2 = NightNavy;
            }
            else if (c is Guna2Panel gunaPanel)
            {
                Color current = gunaPanel.FillColor.A > 0
                    ? gunaPanel.FillColor
                    : gunaPanel.BackColor;

                Color? role = PanelRole(current);

                if (role.HasValue)
                {
                    gunaPanel.BackColor = role.Value;

                    if (gunaPanel.FillColor.A > 0)
                    {
                        gunaPanel.FillColor = role.Value;
                    }
                }
            }
            else if (c is Guna2GradientButton gradientButton)
            {
                StyleGradientButton(gradientButton);
            }
            else if (c is Guna2Button button)
            {
                ButtonRole role = ResolveRole(
                    button,
                    button.FillColor,
                    button.ForeColor,
                    button.ButtonMode == Guna.UI2.WinForms.Enums.ButtonMode.RadioButton);

                StyleButton(button, role);
            }
            else if (c is Guna2TextBox textBox)
            {
                StyleTextBox(textBox);
            }
            else if (c is Guna2ComboBox comboBox)
            {
                comboBox.BorderColor = Border;
                comboBox.ForeColor = TextPrimary;
                comboBox.FocusedColor = Navy;
                comboBox.FocusedState.BorderColor = Navy;
                comboBox.HoverState.BorderColor = Slate;
            }
            else if (c is Guna2CheckBox checkBox)
            {
                checkBox.BackColor = Backdrop(checkBox);
                checkBox.ForeColor = TextPrimary;
                checkBox.CheckedState.FillColor = Navy;
                checkBox.CheckedState.BorderColor = Navy;
                checkBox.UncheckedState.FillColor = Slate;
                checkBox.UncheckedState.BorderColor = Slate;
            }
            else if (c is DataGridView grid)
            {
                StyleGrid(grid);
            }
            else if (c is Guna2ProgressIndicator indicator)
            {
                indicator.ProgressColor = Navy;
            }
            else if (c is Guna2HtmlLabel htmlLabel)
            {
                StyleLabel(htmlLabel);
            }
            else if (c is Label label)
            {
                StyleLabel(label);
            }
            else if (c is Panel panel)
            {
                Color? role = PanelRole(panel.BackColor);

                if (role.HasValue)
                {
                    panel.BackColor = role.Value;
                }
            }



            foreach (Control child in c.Controls)
            {
                Style(child);
            }
        }

        // ---------- Panels ----------

        private static Color? PanelRole(Color current)
        {
            if (current.A == 0)
            {
                return null;
            }

            double lum = Lum(current);

            if (lum < 100) return Navy;
            if (lum >= 253) return Surface;
            if (lum >= 200) return Background;

            return null;
        }

        // ---------- Labels ----------

        private static void StyleLabel(Control label)
        {
            bool bold = (label.Font.Style & FontStyle.Bold) != 0;
            double foreLum = Lum(label.ForeColor);

            label.BackColor = Color.Transparent;

            if (IsDark(Backdrop(label)))
            {
                label.ForeColor = (bold || foreLum >= 235)
                    ? TextOnDark
                    : TextOnDarkMuted;
            }
            else
            {
                label.ForeColor = (bold || foreLum < 90)
                    ? TextPrimary
                    : TextMuted;
            }
        }

        // ---------- Buttons ----------

        private static ButtonRole ResolveRole(
            Control button,
            Color fill,
            Color fore,
            bool isRadio)
        {
            switch ((button.Tag as string)?.ToLowerInvariant())
            {
                case "primary": return ButtonRole.Primary;
                case "secondary": return ButtonRole.Secondary;
                case "success": return ButtonRole.Success;
                case "danger": return ButtonRole.Danger;
            }

            if (isRadio)
            {
                return ButtonRole.Nav;
            }

            if (IsDark(Backdrop(button)))
            {
                return Lum(fore) < 128
                    ? ButtonRole.Chip
                    : ButtonRole.Accent;
            }

            if (fill.A > 0 && (Lum(fill) > 200 || Lum(fore) < 100))
            {
                return ButtonRole.Secondary;
            }

            if (fill.A > 0 && IsGray(fill))
            {
                return ButtonRole.Muted;
            }

            return ButtonRole.Primary;
        }

        private static void StyleButton(Guna2Button b, ButtonRole role)
        {
            if (role == ButtonRole.Nav)
            {
                b.FillColor = b.Checked ? Dusky : Color.Transparent;
                b.ForeColor = TextOnDark;
                b.HoverState.FillColor = NavyHover;
                b.CheckedState.FillColor = Dusky;
                b.CheckedState.ForeColor = TextOnDark;
                b.DisabledState.FillColor = NavyHover;
                b.DisabledState.ForeColor = TextOnDarkMuted;
                b.DisabledState.BorderColor = NavyHover;
                b.DisabledState.CustomBorderColor = NavyHover;
                return;
            }

            var (fill, fore, hover, pressed) = role switch
            {
                ButtonRole.Secondary => (Border, Navy, Background, SlateLight),
                ButtonRole.Chip => (Border, Navy, Background, SlateLight),
                ButtonRole.Muted => (Slate, Color.White, Dusky, Navy),
                ButtonRole.Success => (Success, Color.White, SuccessHover, SuccessPressed),
                ButtonRole.Danger => (Danger, Color.White, DangerHover, DangerPressed),
                ButtonRole.Accent => (Dusky, Color.White, Slate, NavyHover),
                _ => (Navy, Color.White, NavyHover, NavyDark)
            };

            b.FillColor = fill;
            b.ForeColor = fore;
            b.HoverState.FillColor = hover;
            b.PressedColor = pressed;

            b.DisabledState.FillColor = Border;
            b.DisabledState.ForeColor = Slate;
            b.DisabledState.BorderColor = Border;
            b.DisabledState.CustomBorderColor = Border;
        }

        private static void StyleGradientButton(Guna2GradientButton b)
        {
            b.FillColor = Navy;
            b.FillColor2 = NavyHover;
            b.ForeColor = Color.White;

            b.DisabledState.FillColor = Border;
            b.DisabledState.FillColor2 = Border;
            b.DisabledState.ForeColor = Slate;
            b.DisabledState.BorderColor = Border;
            b.DisabledState.CustomBorderColor = Border;
        }

        // ---------- Inputs ----------

        private static void StyleTextBox(Guna2TextBox t)
        {
            if (t.FillColor.A > 0 && Lum(t.FillColor) < 250)
            {
                t.FillColor = Border;
            }

            if (t.BorderColor.A > 0)
            {
                t.BorderColor = Border;
            }

            t.ForeColor = TextPrimary;
            t.PlaceholderForeColor = Slate;
            t.HoverState.BorderColor = Slate;
            t.FocusedState.BorderColor = Navy;

            t.DisabledState.FillColor = Border;
            t.DisabledState.BorderColor = Border;
            t.DisabledState.ForeColor = Navy;
            t.DisabledState.PlaceholderForeColor = Slate;
        }

        // ---------- Grids ----------

        private static readonly Font GridFont =
            new Font("Segoe UI", 9F);

        private static readonly Font GridBoldFont =
            new Font("Segoe UI", 9F, FontStyle.Bold);

        // Use this on forms that do not call AppTheme.Apply (Admin, Subscription, Backup).
        public static void FormatGrid(DataGridView grid)
        {
            StyleGrid(grid);
        }

        // Right-aligns columns whose values are text (for example "₱1,250.00").
        public static void AlignRight(DataGridView grid, params string[] columnNames)
        {
            foreach (string name in columnNames)
            {
                if (!grid.Columns.Contains(name))
                {
                    continue;
                }

                var column = grid.Columns[name];

                column.DefaultCellStyle.Alignment =
                    DataGridViewContentAlignment.MiddleRight;

                column.HeaderCell.Style.Alignment =
                    DataGridViewContentAlignment.MiddleRight;
            }
        }

        private static void StyleGrid(DataGridView g)
        {
            Color zebra = Color.FromArgb(247, 248, 250);

            g.EnableHeadersVisualStyles = false;
            g.RowHeadersVisible = false;
            g.AllowUserToResizeRows = false;
            g.BorderStyle = BorderStyle.None;
            g.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            g.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            g.ColumnHeadersHeightSizeMode =
                DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            g.ColumnHeadersHeight = 40;
            g.RowTemplate.Height = 38;
            g.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            g.MultiSelect = false;
            g.BackgroundColor = Surface;
            g.GridColor = Border;

            var head = g.ColumnHeadersDefaultCellStyle;
            head.BackColor = Navy;
            head.ForeColor = TextOnDark;
            head.Font = GridBoldFont;
            head.Alignment = DataGridViewContentAlignment.MiddleLeft;
            head.SelectionBackColor = Navy;
            head.SelectionForeColor = TextOnDark;
            head.Padding = new Padding(8, 0, 8, 0);
            head.WrapMode = DataGridViewTriState.False;

            var body = g.DefaultCellStyle;
            body.BackColor = Surface;
            body.ForeColor = TextPrimary;
            body.Font = GridFont;
            body.SelectionBackColor = Border;
            body.SelectionForeColor = Navy;
            body.Padding = new Padding(8, 0, 8, 0);
            body.WrapMode = DataGridViewTriState.False;

            var alt = g.AlternatingRowsDefaultCellStyle;
            alt.BackColor = zebra;
            alt.ForeColor = TextPrimary;
            alt.SelectionBackColor = Border;
            alt.SelectionForeColor = Navy;

            if (g is Guna2DataGridView guna)
            {
                guna.ThemeStyle.BackColor = Surface;
                guna.ThemeStyle.GridColor = Border;

                guna.ThemeStyle.HeaderStyle.BackColor = Navy;
                guna.ThemeStyle.HeaderStyle.ForeColor = TextOnDark;
                guna.ThemeStyle.HeaderStyle.Font = GridBoldFont;
                guna.ThemeStyle.HeaderStyle.Height = 40;

                guna.ThemeStyle.RowsStyle.BackColor = Surface;
                guna.ThemeStyle.RowsStyle.ForeColor = TextPrimary;
                guna.ThemeStyle.RowsStyle.Font = GridFont;
                guna.ThemeStyle.RowsStyle.Height = 38;
                guna.ThemeStyle.RowsStyle.SelectionBackColor = Border;
                guna.ThemeStyle.RowsStyle.SelectionForeColor = Navy;

                guna.ThemeStyle.AlternatingRowsStyle.BackColor = zebra;
                guna.ThemeStyle.AlternatingRowsStyle.ForeColor = TextPrimary;
                guna.ThemeStyle.AlternatingRowsStyle.SelectionBackColor = Border;
                guna.ThemeStyle.AlternatingRowsStyle.SelectionForeColor = Navy;
            }

            g.DataBindingComplete -= GridBound;
            g.DataBindingComplete += GridBound;

            g.CellFormatting -= GridCellFormatting;
            g.CellFormatting += GridCellFormatting;
        }

        // After a grid is bound to a list: right-align number columns,
        // drop useless trailing zeros and use one date format.
        private static void GridBound(object? sender, DataGridViewBindingCompleteEventArgs e)
        {
            if (sender is not DataGridView g)
            {
                return;
            }

            foreach (DataGridViewColumn column in g.Columns)
            {
                if (!column.Visible ||
                    column is DataGridViewButtonColumn ||
                    column is DataGridViewCheckBoxColumn)
                {
                    continue;
                }

                object? sample = null;

                foreach (DataGridViewRow row in g.Rows)
                {
                    object? value = row.Cells[column.Index].Value;

                    if (value != null)
                    {
                        sample = value;
                        break;
                    }
                }

                if (sample == null)
                {
                    continue;
                }

                bool isId =
                    column.HeaderText.Trim().Equals("ID", StringComparison.OrdinalIgnoreCase) ||
                    column.Name.EndsWith("Id", StringComparison.Ordinal);

                bool isDecimal = sample is decimal || sample is double || sample is float;
                bool isInteger = sample is int || sample is long || sample is short;

                if ((isDecimal || isInteger) && !isId)
                {
                    column.DefaultCellStyle.Alignment =
                        DataGridViewContentAlignment.MiddleRight;

                    column.HeaderCell.Style.Alignment =
                        DataGridViewContentAlignment.MiddleRight;

                    if (isDecimal &&
                        string.IsNullOrEmpty(column.DefaultCellStyle.Format))
                    {
                        column.DefaultCellStyle.Format = "#,##0.##";
                    }
                }
                else if (sample is DateTime &&
                         string.IsNullOrEmpty(column.DefaultCellStyle.Format))
                {
                    column.DefaultCellStyle.Format = "MMM dd, yyyy";
                }
            }
        }

        // Colors the text of any column that holds a status.
        private static void GridCellFormatting(object? sender, DataGridViewCellFormattingEventArgs e)
        {
            if (sender is not DataGridView g || e.RowIndex < 0 || e.ColumnIndex < 0)
            {
                return;
            }

            var column = g.Columns[e.ColumnIndex];
            string key = (column.HeaderText + " " + column.Name).ToLowerInvariant();

            if (!key.Contains("status") && !key.Contains("reachable"))
            {
                return;
            }

            string text = (e.Value?.ToString() ?? string.Empty).Trim().ToLowerInvariant();

            if (text.Length == 0)
            {
                return;
            }

            Color? color = StatusColor(text);

            if (color == null)
            {
                return;
            }

            e.CellStyle.ForeColor = color.Value;
            e.CellStyle.SelectionForeColor = color.Value;
            e.CellStyle.Font = GridBoldFont;
        }

        private static Color? StatusColor(string t)
        {
            if (t.StartsWith("inactive") || t.StartsWith("cancel") ||
                t.StartsWith("reject") || t.StartsWith("out of stock") ||
                t.StartsWith("fired") || t.StartsWith("suspend") ||
                t.StartsWith("unreachable") || t.StartsWith("expired"))
            {
                return Danger;
            }

            if (t.StartsWith("pending") || t.StartsWith("low stock") ||
                t.StartsWith("resigned") || t.StartsWith("past due") ||
                t.StartsWith("pastdue"))
            {
                return Warning;
            }

            if (t.StartsWith("active") || t.StartsWith("in stock") ||
                t.StartsWith("complete") || t.StartsWith("approved") ||
                t.StartsWith("paid") || t.StartsWith("ok") ||
                t.StartsWith("good"))
            {
                return Success;
            }

            return null;
        }

        // ---------- Helpers ----------

        private static Color Backdrop(Control c)
        {
            for (Control? p = c.Parent; p != null; p = p.Parent)
            {
                Color fill = FillOf(p);

                if (fill.A > 0)
                {
                    return fill;
                }
            }

            return Background;
        }

        private static Color FillOf(Control c)
        {
            if (c is Guna2CustomGradientPanel customGradient)
            {
                return customGradient.FillColor;
            }

            if (c is Guna2GradientPanel gradient)
            {
                return Blend(gradient.FillColor, gradient.FillColor2);
            }

            if (c is Guna2Panel panel && panel.FillColor.A > 0)
            {
                return panel.FillColor;
            }

            return c.BackColor;
        }

        private static double Lum(Color c)
        {
            return 0.299 * c.R + 0.587 * c.G + 0.114 * c.B;
        }

        private static bool IsDark(Color c)
        {
            return Lum(c) < 140;
        }

        private static bool IsGray(Color c)
        {
            int max = Math.Max(c.R, Math.Max(c.G, c.B));
            int min = Math.Min(c.R, Math.Min(c.G, c.B));
            double lum = Lum(c);

            return (max - min) < 20 && lum > 40 && lum < 215;
        }

        private static Color Blend(Color a, Color b)
        {
            return Color.FromArgb(
                (a.R + b.R) / 2,
                (a.G + b.G) / 2,
                (a.B + b.B) / 2);
        }
    }
}