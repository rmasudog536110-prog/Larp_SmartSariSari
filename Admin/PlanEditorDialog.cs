using Larp.domain.DTO;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Larp.winforms.Admin
{
    // Add or edit a plan. Built in code, so there is NO designer file for this class.
    //
    //   Add:  using var dialog = new PlanEditorDialog(null);
    //   Edit: using var dialog = new PlanEditorDialog(selectedPlanDto);
    //
    // When ShowDialog returns DialogResult.OK, read dialog.Plan (a PlanDto) or
    // dialog.Request (a SavePlanRequest, ready to send to POST/PUT admin/plans).
    public partial class PlanEditorDialog : Form
    {
        private readonly bool _isEdit;

        private readonly TextBox txtName = new();
        private readonly TextBox txtDescription = new();
        private readonly NumericUpDown numMonthly = new();
        private readonly NumericUpDown numYearly = new();
        private readonly NumericUpDown numEmployees = new();
        private readonly NumericUpDown numBranches = new();
        private readonly NumericUpDown numSortOrder = new();
        private readonly CheckedListBox modulesList = new();
        private readonly CheckBox chkActive = new();

        private readonly Button btnSave = new();
        private readonly Button btnCancel = new();

        public PlanDto Plan { get; private set; }

        public SavePlanRequest Request => new()
        {
            Name = Plan.Name,
            Description = Plan.Description,
            MonthlyPrice = Plan.MonthlyPrice,
            YearlyPrice = Plan.YearlyPrice,
            MaxEmployees = Plan.MaxEmployees,
            MaxBranches = Plan.MaxBranches,
            Modules = Plan.Modules.ToList(),
            IsActive = Plan.IsActive,
            SortOrder = Plan.SortOrder
        };

        public PlanEditorDialog(PlanDto? plan)
        {
            _isEdit = plan != null;

            Plan = plan == null
                ? new PlanDto
                {
                    IsActive = true,
                    MaxEmployees = 5,
                    SortOrder = 0,
                    Modules = new List<string>()
                }
                : new PlanDto
                {
                    PlanId = plan.PlanId,
                    Name = plan.Name,
                    Description = plan.Description,
                    MonthlyPrice = plan.MonthlyPrice,
                    YearlyPrice = plan.YearlyPrice,
                    MaxEmployees = plan.MaxEmployees,
                    MaxBranches = plan.MaxBranches,
                    Modules = plan.Modules?.ToList() ?? new List<string>(),
                    IsActive = plan.IsActive,
                    SortOrder = plan.SortOrder,
                    CompanyCount = plan.CompanyCount
                };

            BuildInterface();
            LoadValues();
        }

        // ---------------- Layout ----------------

        private void BuildInterface()
        {
            Text = _isEdit ? "Edit Plan" : "Add Plan";

            StartPosition = FormStartPosition.CenterParent;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            ShowInTaskbar = false;

            ClientSize = new Size(620, 700);
            BackColor = Color.FromArgb(245, 246, 248);

            var header = new Panel
            {
                Dock = DockStyle.Top,
                Height = 80,
                BackColor = Color.White
            };

            header.Controls.Add(new Label
            {
                Text = _isEdit ? "Edit Plan" : "Create Plan",
                Font = new Font("Segoe UI Semibold", 18F),
                ForeColor = Color.FromArgb(28, 40, 63),
                Location = new Point(28, 12),
                AutoSize = true
            });

            header.Controls.Add(new Label
            {
                Text = "Set the pricing, limits and modules this plan includes.",
                Font = new Font("Segoe UI", 9F),
                ForeColor = Color.FromArgb(96, 105, 121),
                Location = new Point(30, 50),
                AutoSize = true
            });

            Controls.Add(header);

            int y = 100;
            const int rowHeight = 44;

            AddLabel("Plan name", 30, y);
            PlaceTextBox(txtName, 210, y - 4, 365);
            txtName.MaxLength = 50;
            y += rowHeight;

            AddLabel("Suited for", 30, y);
            PlaceTextBox(txtDescription, 210, y - 4, 365);
            txtDescription.MaxLength = 200;
            y += rowHeight;

            AddLabel("Monthly price (PHP)", 30, y);
            PlaceNumeric(numMonthly, 210, y - 4, 170, 999999999, 2, 1);
            y += rowHeight;

            AddLabel("Yearly price (PHP)", 30, y);
            PlaceNumeric(numYearly, 210, y - 4, 170, 999999999, 2, 1);
            y += rowHeight;

            AddLabel("Max employees", 30, y);
            PlaceNumeric(numEmployees, 210, y - 4, 170, 100000, 0, 1);
            numEmployees.Minimum = 1;
            y += rowHeight;

            AddLabel("Max branches", 30, y);
            PlaceNumeric(numBranches, 210, y - 4, 170, 1000, 0, 1);
            Controls.Add(new Label
            {
                Text = "0 = no branching",
                Font = new Font("Segoe UI", 8.5F),
                ForeColor = Color.FromArgb(96, 105, 121),
                Location = new Point(392, y),
                AutoSize = true
            });
            y += rowHeight;

            AddLabel("Sort order", 30, y);
            PlaceNumeric(numSortOrder, 210, y - 4, 170, 999, 0, 1);
            y += rowHeight;

            AddLabel("Modules", 30, y);

            modulesList.Location = new Point(210, y - 4);
            modulesList.Size = new Size(365, 180);
            modulesList.Font = new Font("Segoe UI", 9.5F);
            modulesList.BorderStyle = BorderStyle.FixedSingle;
            modulesList.CheckOnClick = true;
            modulesList.IntegralHeight = false;

            foreach (var module in PlanModules.All)
                modulesList.Items.Add(module.Label);

            Controls.Add(modulesList);

            y += 190;

            chkActive.Text = "Plan is active and available for subscription";
            chkActive.Font = new Font("Segoe UI", 9F);
            chkActive.ForeColor = Color.FromArgb(40, 45, 55);
            chkActive.Location = new Point(210, y);
            chkActive.AutoSize = true;
            Controls.Add(chkActive);

            btnCancel.Text = "Cancel";
            btnCancel.Location = new Point(355, 645);
            btnCancel.Size = new Size(105, 38);
            btnCancel.FlatStyle = FlatStyle.Flat;
            btnCancel.BackColor = Color.White;
            btnCancel.ForeColor = Color.FromArgb(28, 40, 63);
            btnCancel.DialogResult = DialogResult.Cancel;

            btnSave.Text = _isEdit ? "Save Changes" : "Create Plan";
            btnSave.Location = new Point(470, 645);
            btnSave.Size = new Size(105, 38);
            btnSave.FlatStyle = FlatStyle.Flat;
            btnSave.BackColor = Color.FromArgb(46, 107, 69);
            btnSave.ForeColor = Color.White;
            btnSave.Click += BtnSave_Click;

            Controls.Add(btnCancel);
            Controls.Add(btnSave);

            AcceptButton = btnSave;
            CancelButton = btnCancel;
        }

        private void AddLabel(string text, int x, int y)
        {
            Controls.Add(new Label
            {
                Text = text,
                Font = new Font("Segoe UI Semibold", 9F),
                ForeColor = Color.FromArgb(28, 40, 63),
                Location = new Point(x, y),
                Size = new Size(170, 24)
            });
        }

        private void PlaceTextBox(TextBox box, int x, int y, int width)
        {
            box.Location = new Point(x, y);
            box.Width = width;
            box.Font = new Font("Segoe UI", 10F);
            Controls.Add(box);
        }

        private void PlaceNumeric(
            NumericUpDown control,
            int x,
            int y,
            int width,
            decimal maximum,
            int decimalPlaces,
            decimal increment)
        {
            control.Location = new Point(x, y);
            control.Width = width;
            control.Minimum = 0;
            control.Maximum = maximum;
            control.DecimalPlaces = decimalPlaces;
            control.Increment = increment;
            control.ThousandsSeparator = true;
            control.Font = new Font("Segoe UI", 10F);
            Controls.Add(control);
        }

        // ---------------- Values ----------------

        private void LoadValues()
        {
            txtName.Text = Plan.Name ?? "";
            txtDescription.Text = Plan.Description ?? "";

            numMonthly.Value = Clamp(numMonthly, Plan.MonthlyPrice);
            numYearly.Value = Clamp(numYearly, Plan.YearlyPrice);
            numEmployees.Value = Clamp(numEmployees, Plan.MaxEmployees);
            numBranches.Value = Clamp(numBranches, Plan.MaxBranches);
            numSortOrder.Value = Clamp(numSortOrder, Plan.SortOrder);

            chkActive.Checked = Plan.IsActive;

            var selected = Plan.Modules ?? new List<string>();

            for (int i = 0; i < PlanModules.All.Length; i++)
            {
                if (selected.Contains(PlanModules.All[i].Key, StringComparer.OrdinalIgnoreCase))
                    modulesList.SetItemChecked(i, true);
            }
        }

        private static decimal Clamp(NumericUpDown control, decimal value)
        {
            if (value < control.Minimum) return control.Minimum;
            if (value > control.Maximum) return control.Maximum;
            return value;
        }

        private void BtnSave_Click(object? sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                Warn("Please enter a plan name.");
                txtName.Focus();
                return;
            }

            if (numMonthly.Value <= 0 || numYearly.Value <= 0)
            {
                Warn("Monthly and yearly prices must be greater than zero.");
                return;
            }

            if (modulesList.CheckedIndices.Count == 0)
            {
                Warn("Select at least one module for this plan.");
                modulesList.Focus();
                return;
            }

            var modules = new List<string>();

            foreach (int index in modulesList.CheckedIndices)
                modules.Add(PlanModules.All[index].Key);

            Plan.Name = txtName.Text.Trim();
            Plan.Description = string.IsNullOrWhiteSpace(txtDescription.Text)
                ? null
                : txtDescription.Text.Trim();
            Plan.MonthlyPrice = numMonthly.Value;
            Plan.YearlyPrice = numYearly.Value;
            Plan.MaxEmployees = (int)numEmployees.Value;
            Plan.MaxBranches = (int)numBranches.Value;
            Plan.SortOrder = (int)numSortOrder.Value;
            Plan.IsActive = chkActive.Checked;
            Plan.Modules = modules;

            DialogResult = DialogResult.OK;
            Close();
        }

        private static void Warn(string message)
        {
            MessageBox.Show(
                message,
                "Plan",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
        }
    }
}