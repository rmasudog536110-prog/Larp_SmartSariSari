using Larp.domain.DTO;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Larp.winforms.Admin
{
    public partial class PlansWinforms : Form
    {
        private readonly HttpClient _http;
        private List<PlanDto> _plans = new();

        public PlansWinforms(HttpClient httpClient)
        {
            InitializeComponent();

            _http = httpClient;

            WindowState = FormWindowState.Maximized;
            StartPosition = FormStartPosition.CenterScreen;

            ConfigureGrid();

            Load += async (s, e) => await LoadPlansAsync();
        }

        private void ConfigureGrid()
        {
            gridPlans.AutoGenerateColumns = false;
            gridPlans.AllowUserToAddRows = false;
            gridPlans.AllowUserToDeleteRows = false;
            gridPlans.ReadOnly = true;
            gridPlans.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            gridPlans.MultiSelect = false;
            gridPlans.RowHeadersVisible = false;
            gridPlans.RowTemplate.Height = 54;

            gridPlans.Columns.Clear();

            gridPlans.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "PlanId",
                DataPropertyName = "PlanId",
                Visible = false
            });

            gridPlans.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Plan",
                HeaderText = "PLAN",
                Width = 160
            });

            gridPlans.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Description",
                HeaderText = "SUITED FOR",
                Width = 230
            });

            gridPlans.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Monthly",
                HeaderText = "MONTHLY",
                Width = 110
            });

            gridPlans.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Yearly",
                HeaderText = "YEARLY",
                Width = 110
            });

            gridPlans.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Employees",
                HeaderText = "EMPLOYEES",
                Width = 110
            });

            gridPlans.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Branches",
                HeaderText = "BRANCHES",
                Width = 100
            });

            gridPlans.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Modules",
                HeaderText = "INCLUDED MODULES",
                Width = 350
            });

            gridPlans.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Status",
                HeaderText = "STATUS",
                Width = 100
            });

            gridPlans.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Companies",
                HeaderText = "ACTIVE COMPANIES",
                Width = 130
            });

            gridPlans.Columns.Add(new DataGridViewButtonColumn
            {
                Name = "Edit",
                HeaderText = "",
                Text = "Edit",
                UseColumnTextForButtonValue = true,
                Width = 75
            });

            gridPlans.Columns.Add(new DataGridViewButtonColumn
            {
                Name = "Toggle",
                HeaderText = "",
                Text = "Deactivate",
                UseColumnTextForButtonValue = true,
                Width = 85
            });

            gridPlans.CellContentClick += GridPlans_CellContentClick;
        }

        private async Task LoadPlansAsync()
        {
            try
            {
                SetLoading(true);

                var result =
                    await _http.GetFromJsonAsync<List<PlanDto>>(
                        "admin/plans");

                _plans = result?
                    .OrderBy(p => p.SortOrder)
                    .ThenBy(p => p.PlanId)
                    .ToList()
                    ?? new List<PlanDto>();

                RenderPlanCards();
                RenderGrid();
                UpdateSummary();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Unable to load plans.\n\n{ex.Message}",
                    "Plans & Pricing",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            finally
            {
                SetLoading(false);
            }
        }

        private void RenderPlanCards()
        {
            plansPanel.Controls.Clear();

            foreach (var plan in _plans)
            {
                plansPanel.Controls.Add(CreatePlanCard(plan));
            }
        }

        private Panel CreatePlanCard(PlanDto plan)
        {
            var card = new Panel
            {
                Width = 290,
                Height = 345,
                Margin = new Padding(10),
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle
            };

            var accent = new Panel
            {
                Dock = DockStyle.Top,
                Height = 7,
                BackColor = plan.IsActive
                    ? Color.FromArgb(46, 107, 69)
                    : Color.FromArgb(150, 155, 165)
            };

            card.Controls.Add(accent);

            var name = new Label
            {
                Text = plan.Name,
                Font = new Font("Segoe UI Semibold", 18F),
                ForeColor = Color.FromArgb(28, 40, 63),
                AutoSize = false,
                Location = new Point(20, 25),
                Size = new Size(250, 32)
            };

            card.Controls.Add(name);

            var description = new Label
            {
                Text = string.IsNullOrWhiteSpace(plan.Description)
                    ? "Business management plan"
                    : plan.Description,
                Font = new Font("Segoe UI", 9F),
                ForeColor = Color.FromArgb(96, 105, 121),
                AutoSize = false,
                Location = new Point(20, 62),
                Size = new Size(250, 42)
            };

            card.Controls.Add(description);

            var monthly = new Label
            {
                Text = $"₱{plan.MonthlyPrice:N0}",
                Font = new Font("Segoe UI Semibold", 25F),
                ForeColor = Color.FromArgb(28, 40, 63),
                AutoSize = false,
                Location = new Point(20, 108),
                Size = new Size(160, 42)
            };

            card.Controls.Add(monthly);

            var monthText = new Label
            {
                Text = "/ month",
                Font = new Font("Segoe UI", 9F),
                ForeColor = Color.FromArgb(96, 105, 121),
                AutoSize = false,
                Location = new Point(177, 123),
                Size = new Size(80, 25)
            };

            card.Controls.Add(monthText);

            var yearly = new Label
            {
                Text = $"₱{plan.YearlyPrice:N0} / year",
                Font = new Font("Segoe UI", 9F),
                ForeColor = Color.FromArgb(46, 107, 69),
                AutoSize = false,
                Location = new Point(20, 153),
                Size = new Size(250, 25)
            };

            card.Controls.Add(yearly);

            var separator = new Panel
            {
                Location = new Point(20, 185),
                Size = new Size(250, 1),
                BackColor = Color.FromArgb(225, 227, 231)
            };

            card.Controls.Add(separator);

            var limits = new Label
            {
                Text =
                    $"✓ Up to {plan.MaxEmployees} employees\n" +
                    $"✓ {(plan.MaxBranches > 0 ? $"Up to {plan.MaxBranches} branches" : "Branch management not included")}",
                Font = new Font("Segoe UI", 9F),
                ForeColor = Color.FromArgb(50, 55, 65),
                AutoSize = false,
                Location = new Point(20, 198),
                Size = new Size(250, 45)
            };

            card.Controls.Add(limits);

            var moduleLabel = new Label
            {
                Text = FormatModules(plan.Modules),
                Font = new Font("Segoe UI", 8.5F),
                ForeColor = Color.FromArgb(96, 105, 121),
                AutoSize = false,
                Location = new Point(20, 245),
                Size = new Size(250, 48)
            };

            card.Controls.Add(moduleLabel);

            var status = new Label
            {
                Text = plan.IsActive ? "ACTIVE" : "INACTIVE",
                Font = new Font("Segoe UI Semibold", 8.5F),
                ForeColor = plan.IsActive
                    ? Color.FromArgb(46, 107, 69)
                    : Color.FromArgb(150, 70, 70),
                AutoSize = false,
                Location = new Point(20, 303),
                Size = new Size(910, 40)
            };

            card.Controls.Add(status);

            var edit = new Button
            {
                Text = "Edit",
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.FromArgb(28, 40, 63),
                ForeColor = Color.White,
                Font = new Font("Segoe UI Semibold", 8.5F),
                Location = new Point(910, 40),
                Size = new Size(85, 31),
                Cursor = Cursors.Hand
            };

            edit.FlatAppearance.BorderSize = 0;

            edit.Click += (s, e) =>
            {
                OpenEditPlan(plan);
            };

            card.Controls.Add(edit);

            return card;
        }

        private string FormatModules(List<string>? modules)
        {
            if (modules == null || modules.Count == 0)
                return "No modules included";

            var labels = modules
                .Take(3)
                .Select(GetModuleLabel)
                .Select(x => $"✓ {x}")
                .ToList();

            if (modules.Count > 3)
                labels.Add("✓ More modules included");

            return string.Join("\n", labels);
        }

        private string GetModuleLabel(string key)
        {
            var module = PlanModules.All.FirstOrDefault(
                x => string.Equals(
                    x.Key,
                    key,
                    StringComparison.OrdinalIgnoreCase));

            return module == default
                ? key
                : module.Label;
        }

        private void RenderGrid()
        {
            gridPlans.Rows.Clear();

            foreach (var plan in _plans)
            {
                int row = gridPlans.Rows.Add();

                gridPlans.Rows[row].Cells["PlanId"].Value =
                    plan.PlanId;

                gridPlans.Rows[row].Cells["Plan"].Value =
                    plan.Name;

                gridPlans.Rows[row].Cells["Description"].Value =
                    plan.Description;

                gridPlans.Rows[row].Cells["Monthly"].Value =
                    $"₱{plan.MonthlyPrice:N2}";

                gridPlans.Rows[row].Cells["Yearly"].Value =
                    $"₱{plan.YearlyPrice:N2}";

                gridPlans.Rows[row].Cells["Employees"].Value =
                    plan.MaxEmployees;

                gridPlans.Rows[row].Cells["Branches"].Value =
                    plan.MaxBranches > 0
                        ? plan.MaxBranches.ToString()
                        : "Not included";

                gridPlans.Rows[row].Cells["Modules"].Value =
                    plan.Modules == null || plan.Modules.Count == 0
                        ? "-"
                        : string.Join(
                            ", ",
                            plan.Modules.Select(GetModuleLabel));

                gridPlans.Rows[row].Cells["Status"].Value =
                    plan.IsActive
                        ? "ACTIVE"
                        : "INACTIVE";

                gridPlans.Rows[row].Cells["Companies"].Value =
                    plan.CompanyCount;

                gridPlans.Rows[row].Cells["Toggle"].Value =
                    plan.IsActive
                        ? "Disable"
                        : "Activate";

                gridPlans.Rows[row].DefaultCellStyle.BackColor =
                    plan.IsActive
                        ? Color.White
                        : Color.FromArgb(245, 245, 246);

                gridPlans.Rows[row].DefaultCellStyle.ForeColor =
                    plan.IsActive
                        ? Color.FromArgb(40, 45, 55)
                        : Color.FromArgb(130, 135, 145);
            }
        }

        private void UpdateSummary()
        {
            lblTotalPlansValue.Text =
                _plans.Count.ToString();

            lblActivePlansValue.Text =
                _plans.Count(p => p.IsActive).ToString();

            lblInactivePlansValue.Text =
                _plans.Count(p => !p.IsActive).ToString();

            lblCompaniesValue.Text =
                _plans.Sum(p => p.CompanyCount).ToString();
        }

        private async void GridPlans_CellContentClick(
            object sender,
            DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            var row = gridPlans.Rows[e.RowIndex];

            if (!int.TryParse(
                row.Cells["PlanId"].Value?.ToString(),
                out int planId))
                return;

            var plan =
                _plans.FirstOrDefault(p => p.PlanId == planId);

            if (plan == null)
                return;

            if (gridPlans.Columns[e.ColumnIndex].Name == "Edit")
            {
                OpenEditPlan(plan);
            }
            else if (
                gridPlans.Columns[e.ColumnIndex].Name == "Toggle")
            {
                await TogglePlanAsync(plan);
            }
        }

        private void OpenEditPlan(PlanDto plan)
        {
            using var dialog =
                new PlanEditorDialog(plan);

            if (dialog.ShowDialog(this) != DialogResult.OK)
                return;

            _ = UpdatePlanAsync(dialog.Plan);
        }

        private async Task TogglePlanAsync(PlanDto plan)
        {
            string action =
                plan.IsActive
                    ? "deactivate"
                    : "activate";

            var result = MessageBox.Show(
                $"Are you sure you want to {action} the {plan.Name} plan?",
                "Confirm",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result != DialogResult.Yes)
                return;

            try
            {
                var request = new SavePlanRequest
                {
                    Name = plan.Name,
                    Description = plan.Description,
                    MonthlyPrice = plan.MonthlyPrice,
                    YearlyPrice = plan.YearlyPrice,
                    MaxEmployees = plan.MaxEmployees,
                    MaxBranches = plan.MaxBranches,
                    Modules = plan.Modules ?? new List<string>(),
                    IsActive = !plan.IsActive,
                    SortOrder = plan.SortOrder
                };

                var response =
                    await _http.PutAsJsonAsync(
                        $"admin/plans/{plan.PlanId}",
                        request);

                if (!response.IsSuccessStatusCode)
                {
                    var error =
                        await response.Content.ReadAsStringAsync();

                    MessageBox.Show(
                        error,
                        "Unable to update plan",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);

                    return;
                }

                await LoadPlansAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private async Task UpdatePlanAsync(PlanDto plan)
        {
            try
            {
                var request = new SavePlanRequest
                {
                    Name = plan.Name,
                    Description = plan.Description,
                    MonthlyPrice = plan.MonthlyPrice,
                    YearlyPrice = plan.YearlyPrice,
                    MaxEmployees = plan.MaxEmployees,
                    MaxBranches = plan.MaxBranches,
                    Modules = plan.Modules ?? new List<string>(),
                    IsActive = plan.IsActive,
                    SortOrder = plan.SortOrder
                };

                var response =
                    await _http.PutAsJsonAsync(
                        $"admin/plans/{plan.PlanId}",
                        request);

                if (!response.IsSuccessStatusCode)
                {
                    var error =
                        await response.Content.ReadAsStringAsync();

                    MessageBox.Show(
                        error,
                        "Unable to update plan",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);

                    return;
                }

                await LoadPlansAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private async void BtnAddPlan_Click(
            object sender,
            EventArgs e)
        {
            using var dialog =
                new PlanEditorDialog(null);

            if (dialog.ShowDialog(this) != DialogResult.OK)
                return;

            try
            {
                var request = new SavePlanRequest
                {
                    Name = dialog.Plan.Name,
                    Description = dialog.Plan.Description,
                    MonthlyPrice = dialog.Plan.MonthlyPrice,
                    YearlyPrice = dialog.Plan.YearlyPrice,
                    MaxEmployees = dialog.Plan.MaxEmployees,
                    MaxBranches = dialog.Plan.MaxBranches,
                    Modules = dialog.Plan.Modules ?? new List<string>(),
                    IsActive = dialog.Plan.IsActive,
                    SortOrder = dialog.Plan.SortOrder
                };

                var response =
                    await _http.PostAsJsonAsync(
                        "admin/plans",
                        request);

                if (!response.IsSuccessStatusCode)
                {
                    var error =
                        await response.Content.ReadAsStringAsync();

                    MessageBox.Show(
                        error,
                        "Unable to create plan",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);

                    return;
                }

                await LoadPlansAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private async void BtnPdf_Click(
            object sender,
            EventArgs e)
        {
            try
            {
                using var response =
                    await _http.GetAsync(
                        "admin/plans/pdf");

                if (!response.IsSuccessStatusCode)
                {
                    var error =
                        await response.Content.ReadAsStringAsync();

                    MessageBox.Show(
                        error,
                        "Unable to generate PDF",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);

                    return;
                }

                var bytes =
                    await response.Content.ReadAsByteArrayAsync();

                using var dialog =
                    new SaveFileDialog
                    {
                        Filter =
                            "PDF Files (*.pdf)|*.pdf",

                        FileName =
                            $"Larp_SmartSariSari_Plans_{DateTime.Today:yyyyMMdd}.pdf",

                        Title =
                            "Save Plans & Pricing PDF"
                    };

                if (dialog.ShowDialog(this) !=
                    DialogResult.OK)
                    return;

                await System.IO.File.WriteAllBytesAsync(
                    dialog.FileName,
                    bytes);

                MessageBox.Show(
                    "Plans & Pricing PDF has been created successfully.",
                    "PDF Ready",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.Message,
                    "PDF Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private async void BtnRefresh_Click(
            object sender,
            EventArgs e)
        {
            await LoadPlansAsync();
        }

        private void BtnClose_Click(
            object sender,
            EventArgs e)
        {
            Close();
        }

        private void SetLoading(bool loading)
        {
            btnAddPlan.Enabled = !loading;
            btnDownloadPdf.Enabled = !loading;
            btnRefresh.Enabled = !loading;
        }

    }
}