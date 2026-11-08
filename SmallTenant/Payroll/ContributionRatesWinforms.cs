using Larp.domain.Entities;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Larp.winforms.SmallTenant.Payroll
{
    // Admin-only: the SSS / PhilHealth / Pag-IBIG rate table.
    public partial class ContributionRatesWinforms : Form
    {
        private readonly HttpClient _httpClient;
        private readonly int _companyId;

        private readonly DataGridView grid = new();
        private readonly ComboBox cmbType = new();
        private List<ContributionRate> _rates = new();

        public ContributionRatesWinforms(HttpClient httpClient, int companyId)
        {
            _httpClient = httpClient;
            _companyId = companyId;

            Text = "Contribution Rates";
            StartPosition = FormStartPosition.CenterParent;
            ClientSize = new Size(1000, 600);

            grid.Dock = DockStyle.Fill;
            grid.ReadOnly = true;
            grid.AllowUserToAddRows = false;
            grid.AllowUserToDeleteRows = false;
            grid.RowHeadersVisible = false;
            grid.MultiSelect = false;
            grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            foreach (var header in new[]
            {
                "Type", "Salary from", "Salary to", "Mode",
                "Employee share", "Employer share", "Cap", "Effective from"
            })
            {
                grid.Columns.Add(header, header);
            }

            var top = new Panel { Dock = DockStyle.Top, Height = 58 };

            cmbType.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbType.Items.AddRange(new object[] { "All", "SSS", "PhilHealth", "Pagibig" });
            cmbType.SelectedIndex = 0;
            cmbType.Location = new Point(15, 15);
            cmbType.Width = 140;
            cmbType.SelectedIndexChanged += (s, e) => ShowRates();

            Button MakeButton(string text, int x) => new()
            {
                Text = text,
                Location = new Point(x, 11),
                Size = new Size(90, 34)
            };

            var btnAdd = MakeButton("Add", 175);
            var btnEdit = MakeButton("Edit", 275);
            var btnDelete = MakeButton("Delete", 375);
            var btnClose = MakeButton("Close", ClientSize.Width - 105);
            btnClose.Anchor = AnchorStyles.Top | AnchorStyles.Right;

            btnAdd.Click += async (s, e) => await AddAsync();
            btnEdit.Click += async (s, e) => await EditAsync();
            btnDelete.Click += async (s, e) => await DeleteAsync();
            btnClose.Click += (s, e) => Close();

            top.Controls.AddRange(new Control[] { cmbType, btnAdd, btnEdit, btnDelete, btnClose });

            // Fill first, then Top, so the toolbar sits above the grid.
            Controls.Add(grid);
            Controls.Add(top);

            Load += async (s, e) => await LoadAsync();
        }

        private async Task LoadAsync()
        {
            try
            {
                _rates = await _httpClient.GetFromJsonAsync<List<ContributionRate>>(
                    $"/tenant/{_companyId}/contribution-rates") ?? new();

                ShowRates();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Unable to load rates.\n\n" + ex.Message,
                    "Contribution Rates",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void ShowRates()
        {
            grid.Rows.Clear();

            string type = cmbType.SelectedItem?.ToString() ?? "All";

            foreach (var r in _rates.Where(x => type == "All" || x.Type == type))
            {
                int index = grid.Rows.Add(
                    r.Type,
                    r.MinSalary.ToString("N2"),
                    r.MaxSalary.ToString("N2"),
                    r.IsPercentage ? "Percent" : "Fixed",
                    FormatShare(r, r.EmployeeShare),
                    FormatShare(r, r.EmployerShare),
                    r.MaxContribution?.ToString("N2") ?? "-",
                    r.EffectiveFrom.ToString("yyyy-MM-dd"));

                grid.Rows[index].Tag = r;
            }
        }

        private static string FormatShare(ContributionRate r, decimal value) =>
            r.IsPercentage ? $"{value * 100:0.####}%" : $"₱{value:N2}";

        private ContributionRate? SelectedRate() =>
            grid.CurrentRow?.Tag as ContributionRate;

        private async Task AddAsync()
        {
            using var dialog = new RateDialog(null);

            if (dialog.ShowDialog(this) != DialogResult.OK)
                return;

            await SendAsync(() => _httpClient.PostAsJsonAsync(
                $"/tenant/{_companyId}/contribution-rates",
                dialog.Rate));
        }

        private async Task EditAsync()
        {
            var rate = SelectedRate();

            if (rate == null)
            {
                MessageBox.Show("Select a rate first.", "Contribution Rates");
                return;
            }

            using var dialog = new RateDialog(rate);

            if (dialog.ShowDialog(this) != DialogResult.OK)
                return;

            await SendAsync(() => _httpClient.PutAsJsonAsync(
                $"/tenant/{_companyId}/contribution-rates/{rate.ContributionRateId}",
                dialog.Rate));
        }

        private async Task DeleteAsync()
        {
            var rate = SelectedRate();

            if (rate == null)
            {
                MessageBox.Show("Select a rate first.", "Contribution Rates");
                return;
            }

            var answer = MessageBox.Show(
                $"Delete this {rate.Type} rate ({rate.MinSalary:N2} to {rate.MaxSalary:N2})?\n\n" +
                "Saved payslips keep their numbers. When a rate changes, add a new row " +
                "with a later effective date instead of deleting.",
                "Delete Rate",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (answer != DialogResult.Yes)
                return;

            await SendAsync(() => _httpClient.DeleteAsync(
                $"/tenant/{_companyId}/contribution-rates/{rate.ContributionRateId}"));
        }

        private async Task SendAsync(Func<Task<HttpResponseMessage>> send)
        {
            try
            {
                using var response = await send();

                if (!response.IsSuccessStatusCode)
                {
                    MessageBox.Show(
                        await ApiError.ReadAsync(response),
                        "Contribution Rates",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    return;
                }

                await LoadAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "The request failed.\n\n" + ex.Message,
                    "Contribution Rates",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
    }

    // Add / edit one rate row. Percent shares are typed as 2.5 and stored as 0.025.
    internal class RateDialog : Form
    {
        private static NumericUpDown Num(int decimals, decimal max) => new()
        {
            DecimalPlaces = decimals,
            Minimum = 0,
            Maximum = max,
            ThousandsSeparator = true
        };

        private readonly int _id;

        private readonly ComboBox cmbType = new();
        private readonly NumericUpDown nudMin = Num(2, 999999999.99m);
        private readonly NumericUpDown nudMax = Num(2, 999999999.99m);
        private readonly NumericUpDown nudEmployee = Num(4, 1000000m);
        private readonly NumericUpDown nudEmployer = Num(4, 1000000m);
        private readonly NumericUpDown nudCap = Num(2, 1000000m);
        private readonly CheckBox chkPercent = new() { Text = "Shares are a percentage of salary", AutoSize = true };
        private readonly CheckBox chkCap = new() { Text = "Cap per share (₱)", AutoSize = true };
        private readonly DateTimePicker dtpEffective = new() { Format = DateTimePickerFormat.Short };

        private readonly Label lblEmployee;
        private readonly Label lblEmployer;

        public ContributionRate Rate { get; private set; } = new();

        public RateDialog(ContributionRate? existing)
        {
            _id = existing?.ContributionRateId ?? 0;

            Text = existing == null ? "Add Rate" : "Edit Rate";
            StartPosition = FormStartPosition.CenterParent;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            ClientSize = new Size(420, 430);

            cmbType.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbType.Items.AddRange(new object[] { "SSS", "PhilHealth", "Pagibig" });
            cmbType.SelectedIndex = 0;

            int y = 20;

            Label Row(string text, Control input)
            {
                var label = new Label { Text = text, Location = new Point(20, y + 4), AutoSize = true };
                input.Location = new Point(190, y);
                input.Width = 210;
                Controls.Add(label);
                Controls.Add(input);
                y += 38;
                return label;
            }

            Row("Type", cmbType);
            Row("Salary from (₱)", nudMin);
            Row("Salary to (₱)", nudMax);

            chkPercent.Location = new Point(20, y);
            Controls.Add(chkPercent);
            y += 34;

            lblEmployee = Row("Employee share", nudEmployee);
            lblEmployer = Row("Employer share", nudEmployer);

            chkCap.Location = new Point(20, y + 2);
            nudCap.Location = new Point(190, y);
            nudCap.Width = 210;
            nudCap.Enabled = false;
            Controls.Add(chkCap);
            Controls.Add(nudCap);
            y += 38;

            Row("Effective from", dtpEffective);

            Controls.Add(new Label
            {
                Text = "Percent shares: type 2.5 for 2.5%. When a rate changes, add a new row with a later date.",
                Location = new Point(20, y + 4),
                Size = new Size(380, 36)
            });

            chkPercent.CheckedChanged += (s, e) => UpdateLabels();
            chkCap.CheckedChanged += (s, e) => nudCap.Enabled = chkCap.Checked;

            if (existing != null)
            {
                cmbType.SelectedItem = existing.Type;
                nudMin.Value = existing.MinSalary;
                nudMax.Value = existing.MaxSalary;
                chkPercent.Checked = existing.IsPercentage;

                decimal scale = existing.IsPercentage ? 100m : 1m;
                nudEmployee.Value = existing.EmployeeShare * scale;
                nudEmployer.Value = existing.EmployerShare * scale;

                if (existing.MaxContribution is decimal cap)
                {
                    chkCap.Checked = true;
                    nudCap.Value = cap;
                }

                dtpEffective.Value = existing.EffectiveFrom.Date;
            }
            else
            {
                dtpEffective.Value = DateTime.Today;
            }

            UpdateLabels();

            var ok = new Button { Text = "Save", Location = new Point(220, 382), Size = new Size(80, 32) };
            var cancel = new Button
            {
                Text = "Cancel",
                Location = new Point(310, 382),
                Size = new Size(80, 32),
                DialogResult = DialogResult.Cancel
            };

            ok.Click += (s, e) =>
            {
                if (nudMax.Value < nudMin.Value)
                {
                    MessageBox.Show("'Salary to' must be at least 'Salary from'.", "Rate");
                    return;
                }

                decimal scale = chkPercent.Checked ? 100m : 1m;

                Rate = new ContributionRate
                {
                    ContributionRateId = _id,
                    Type = cmbType.Text,
                    MinSalary = nudMin.Value,
                    MaxSalary = nudMax.Value,
                    IsPercentage = chkPercent.Checked,
                    EmployeeShare = nudEmployee.Value / scale,
                    EmployerShare = nudEmployer.Value / scale,
                    MaxContribution = chkCap.Checked ? nudCap.Value : null,
                    EffectiveFrom = dtpEffective.Value.Date
                };

                DialogResult = DialogResult.OK;
            };

            Controls.Add(ok);
            Controls.Add(cancel);
            AcceptButton = ok;
            CancelButton = cancel;
        }

        private void UpdateLabels()
        {
            string unit = chkPercent.Checked ? "%" : "₱";
            lblEmployee.Text = $"Employee share ({unit})";
            lblEmployer.Text = $"Employer share ({unit})";
        }
    }
}