using Guna.UI2.WinForms;
using Larp.winforms.MediumTenant;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Larp.winforms.SmallTenant.Expenses
{
    /// <summary>
    /// Expenses list with Add / Edit / Delete. Built in code, no designer file.
    /// Admin sees a Branch drop-down in the dialog; everyone else's expenses
    /// are tagged with their own branch by the server.
    /// </summary>
    [System.ComponentModel.DesignerCategory("Code")]
    public class ExpensesWinForms : Form
    {
        private const string Money = "₱#,##0.00";

        private readonly HttpClient _http;
        private readonly int _companyId;
        private readonly bool _isAdmin =
            Session.Role == "Admin" || Session.Role == "SuperAdmin";

        private readonly Guna2DataGridView _grid = new();
        private readonly Guna2DateTimePicker _from = new();
        private readonly Guna2DateTimePicker _to = new();
        private readonly Label _lblTotal = new();

        private List<ExpenseRow> _rows = new();

        public ExpensesWinForms(HttpClient http, int companyId)
        {
            _http = http;
            _companyId = companyId;

            Text = "Expenses";
            StartPosition = FormStartPosition.CenterScreen;
            ClientSize = new Size(1150, 700);
            MinimumSize = new Size(900, 500);
            BackColor = Color.FromArgb(245, 242, 237);

            BuildUi();
            AppTheme.Apply(this);

            Shown += async (_, _) => await LoadAsync();
        }

        private void BuildUi()
        {
            ConfigureGrid();

            _lblTotal.Dock = DockStyle.Bottom;
            _lblTotal.Height = 36;
            _lblTotal.TextAlign = ContentAlignment.MiddleLeft;
            _lblTotal.Padding = new Padding(12, 0, 0, 0);
            _lblTotal.Font = new Font("Segoe UI Semibold", 10F);
            _lblTotal.BackColor = Color.White;

            var filters = new FlowLayoutPanel
            {
                Dock = DockStyle.Top,
                Height = 64,
                Padding = new Padding(12, 12, 12, 0),
                BackColor = Color.Transparent
            };

            _from.Format = DateTimePickerFormat.Short;
            _from.BorderRadius = 8;
            _from.Size = new Size(140, 36);
            _from.Value = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);

            _to.Format = DateTimePickerFormat.Short;
            _to.BorderRadius = 8;
            _to.Size = new Size(140, 36);
            _to.Value = DateTime.Today;

            var btnRefresh = MakeButton("Refresh", Color.FromArgb(230, 165, 45), 100);
            var btnAdd = MakeButton("+ Add Expense", Color.FromArgb(46, 107, 69), 150);
            var btnClose = MakeButton("Close", Color.FromArgb(210, 75, 70), 90);

            btnRefresh.Click += async (_, _) => await LoadAsync();
            btnAdd.Click += async (_, _) => await AddAsync();
            btnClose.Click += (_, _) => Close();

            filters.Controls.Add(Caption("From"));
            filters.Controls.Add(_from);
            filters.Controls.Add(Caption("To"));
            filters.Controls.Add(_to);
            filters.Controls.Add(btnRefresh);
            filters.Controls.Add(btnAdd);
            filters.Controls.Add(btnClose);

            var header = new Panel
            {
                Dock = DockStyle.Top,
                Height = 70,
                BackColor = Color.FromArgb(28, 58, 42)
            };

            header.Controls.Add(new Label
            {
                Text = "Expenses",
                ForeColor = Color.White,
                Font = new Font("Segoe UI Semibold", 20F, FontStyle.Bold),
                AutoSize = true,
                Location = new Point(24, 14)
            });

            // Fill first, then the docked edges (last added is docked first).
            Controls.Add(_grid);
            Controls.Add(_lblTotal);
            Controls.Add(filters);
            Controls.Add(header);
        }

        private static Label Caption(string text) => new()
        {
            Text = text,
            AutoSize = true,
            Font = new Font("Segoe UI", 10F),
            Margin = new Padding(8, 10, 4, 0)
        };

        private static Guna2Button MakeButton(string text, Color fill, int width) => new()
        {
            Text = text,
            BorderRadius = 8,
            FillColor = fill,
            ForeColor = Color.White,
            Font = new Font("Segoe UI", 10F, FontStyle.Bold),
            Size = new Size(width, 36),
            Margin = new Padding(8, 0, 0, 0)
        };

        private void ConfigureGrid()
        {
            _grid.Dock = DockStyle.Fill;
            _grid.AutoGenerateColumns = false;
            _grid.AllowUserToAddRows = false;
            _grid.AllowUserToDeleteRows = false;
            _grid.AllowUserToResizeRows = false;
            _grid.ReadOnly = true;
            _grid.RowHeadersVisible = false;
            _grid.MultiSelect = false;
            _grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            _grid.ColumnHeadersHeight = 40;
            _grid.RowTemplate.Height = 36;
            _grid.ThemeStyle.HeaderStyle.BackColor = Color.FromArgb(28, 58, 42);
            _grid.ThemeStyle.HeaderStyle.ForeColor = Color.White;
            _grid.ThemeStyle.HeaderStyle.Height = 40;

            void Add(string prop, string header, int width, string? format = null, bool right = false)
            {
                var col = new DataGridViewTextBoxColumn
                {
                    Name = prop,
                    DataPropertyName = prop,
                    HeaderText = header
                };

                if (width == 0) col.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                else col.Width = width;

                if (format != null) col.DefaultCellStyle.Format = format;
                if (right) col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

                _grid.Columns.Add(col);
            }

            Add("ExpenseDate", "Date", 110, "MMM dd, yyyy");
            Add("CategoryName", "Category", 150);
            Add("Description", "Description", 0);
            Add("Amount", "Amount", 120, Money, true);
            Add("PaymentMethod", "Payment", 110);
            Add("Status", "Status", 90);
            Add("BranchName", "Branch", 140);

            _grid.Columns.Add(new DataGridViewButtonColumn
            {
                Name = "Edit",
                HeaderText = "",
                Text = "Edit",
                UseColumnTextForButtonValue = true,
                Width = 70
            });

            _grid.Columns.Add(new DataGridViewButtonColumn
            {
                Name = "Delete",
                HeaderText = "",
                Text = "Delete",
                UseColumnTextForButtonValue = true,
                Width = 80
            });

            _grid.CellContentClick += async (_, e) => await GridClickAsync(e);
        }

        private static string Iso(DateTime d) =>
            d.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);

        private async Task LoadAsync()
        {
            if (_to.Value.Date < _from.Value.Date)
            {
                MessageBox.Show("The end date cannot be earlier than the start date.",
                    "Expenses", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                UseWaitCursor = true;

                var response = await _http.GetAsync(
                    $"tenant/{_companyId}/expenses/date-range" +
                    $"?from={Iso(_from.Value)}&to={Iso(_to.Value)}");

                if (!response.IsSuccessStatusCode)
                {
                    MessageBox.Show(await BranchesWinForms.ReadMessageAsync(response),
                        "Expenses", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                _rows = await response.Content.ReadFromJsonAsync<List<ExpenseRow>>()
                        ?? new List<ExpenseRow>();

                _grid.DataSource = null;
                _grid.DataSource = _rows;

                for (int i = 0; i < _grid.Rows.Count && i < _rows.Count; i++)
                {
                    if (!_rows[i].Counted)
                        _grid.Rows[i].DefaultCellStyle.ForeColor = Color.Gray;
                }

                var counted = _rows.Where(r => r.Counted).ToList();

                _lblTotal.Text =
                    $"{counted.Count} expense(s), total {counted.Sum(r => r.Amount).ToString(Money, CultureInfo.InvariantCulture)}" +
                    "   (cancelled expenses are shown in gray and not counted)";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Unable to load expenses.\n\n{ex.Message}",
                    "Expenses", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                UseWaitCursor = false;
            }
        }

        private async Task AddAsync()
        {
            using var dialog = new ExpenseDialog(_http, _companyId, null, _isAdmin);
            if (dialog.ShowDialog(this) != DialogResult.OK) return;

            var response = await _http.PostAsJsonAsync(
                $"tenant/{_companyId}/expenses", dialog.Request);

            if (!response.IsSuccessStatusCode)
            {
                MessageBox.Show(await BranchesWinForms.ReadMessageAsync(response),
                    "Cannot add expense", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            await LoadAsync();
        }

        private async Task GridClickAsync(DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;
            if (_grid.Rows[e.RowIndex].DataBoundItem is not ExpenseRow row) return;

            string column = _grid.Columns[e.ColumnIndex].Name;

            if (column == "Edit")
            {
                using var dialog = new ExpenseDialog(_http, _companyId, row, _isAdmin);
                if (dialog.ShowDialog(this) != DialogResult.OK) return;

                var response = await _http.PutAsJsonAsync(
                    $"tenant/{_companyId}/expenses/{row.ExpenseId}", dialog.Request);

                if (!response.IsSuccessStatusCode)
                {
                    MessageBox.Show(await BranchesWinForms.ReadMessageAsync(response),
                        "Cannot save expense", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                await LoadAsync();
            }
            else if (column == "Delete")
            {
                var confirm = MessageBox.Show(
                    $"Delete this expense?\n\n{row.Description}  ({row.Amount.ToString(Money, CultureInfo.InvariantCulture)})\n\n" +
                    "This cannot be undone. To keep a record, edit it and set the status to Cancelled instead.",
                    "Delete Expense", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (confirm != DialogResult.Yes) return;

                var response = await _http.DeleteAsync(
                    $"tenant/{_companyId}/expenses/{row.ExpenseId}");

                if (!response.IsSuccessStatusCode)
                {
                    MessageBox.Show(await BranchesWinForms.ReadMessageAsync(response),
                        "Cannot delete expense", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                await LoadAsync();
            }
        }
    }

    internal class ExpenseRow
    {
        public int ExpenseId { get; set; }
        public int ExpenseCategoryId { get; set; }
        public string Description { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public DateTime ExpenseDate { get; set; }
        public string PaymentMethod { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public int? BranchId { get; set; }

        public CategoryLite? ExpenseCategory { get; set; }
        public BranchLite? Branch { get; set; }

        public string CategoryName => ExpenseCategory?.CategoryName ?? "-";
        public string BranchName => Branch?.BranchName ?? "Unassigned";
        public bool Counted => Status != "Cancelled" && Status != "Voided";
    }

    internal class CategoryLite
    {
        public int ExpenseCategoryId { get; set; }
        public string CategoryName { get; set; } = string.Empty;
    }

    internal class BranchLite
    {
        public int BranchId { get; set; }
        public string BranchName { get; set; } = string.Empty;
        public bool IsActive { get; set; }
    }

    internal class Choice
    {
        public int? Id { get; }
        public string Text { get; }
        public Choice(int? id, string text) { Id = id; Text = text; }
        public override string ToString() => Text;
    }

    // Add / edit one expense. Built in code, no designer file.
    [System.ComponentModel.DesignerCategory("Code")]
    internal class ExpenseDialog : Form
    {
        private static readonly string[] Methods =
            { "Cash", "GCash", "Bank Transfer", "Card", "Check" };

        private static readonly string[] Statuses = { "Paid", "Cancelled" };

        private readonly HttpClient _http;
        private readonly int _companyId;
        private readonly ExpenseRow? _existing;
        private readonly bool _isAdmin;

        private readonly ComboBox cmbCategory = new();
        private readonly ComboBox cmbMethod = new();
        private readonly ComboBox cmbStatus = new();
        private readonly ComboBox cmbBranch = new();
        private readonly Button btnNewCategory = new();
        private readonly Button ok = new();
        private readonly TextBox txtDescription = new();
        private readonly NumericUpDown numAmount = new();
        private readonly DateTimePicker dtpDate = new();
        private readonly Label lblStatus = new();

        public object Request => new
        {
            expenseCategoryId = (cmbCategory.SelectedItem as Choice)?.Id ?? 0,
            description = txtDescription.Text.Trim(),
            amount = numAmount.Value,
            expenseDate = dtpDate.Value.Date,
            paymentMethod = cmbMethod.Text,
            status = cmbStatus.Text,
            // Ignored by the server unless the caller is an Admin.
            branchId = _isAdmin ? (cmbBranch.SelectedItem as Choice)?.Id : null
        };

        public ExpenseDialog(HttpClient http, int companyId, ExpenseRow? existing, bool isAdmin)
        {
            _http = http;
            _companyId = companyId;
            _existing = existing;
            _isAdmin = isAdmin;

            Text = existing == null ? "Add Expense" : "Edit Expense";
            StartPosition = FormStartPosition.CenterParent;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = MinimizeBox = false;

            cmbCategory.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbMethod.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbStatus.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbBranch.DropDownStyle = ComboBoxStyle.DropDownList;

            cmbMethod.Items.AddRange(Methods);
            cmbStatus.Items.AddRange(Statuses);

            numAmount.DecimalPlaces = 2;
            numAmount.Maximum = 100000000;
            numAmount.ThousandsSeparator = true;

            dtpDate.Format = DateTimePickerFormat.Short;

            txtDescription.MaxLength = 300;

            btnNewCategory.Text = "+ New";
            btnNewCategory.Click += async (_, _) => await AddCategoryAsync();

            if (existing != null)
            {
                txtDescription.Text = existing.Description;
                numAmount.Value = Math.Min(numAmount.Maximum, Math.Max(0, existing.Amount));
                dtpDate.Value = existing.ExpenseDate.Date;

                if (!cmbMethod.Items.Contains(existing.PaymentMethod))
                    cmbMethod.Items.Add(existing.PaymentMethod);
                if (!cmbStatus.Items.Contains(existing.Status))
                    cmbStatus.Items.Add(existing.Status);

                cmbMethod.SelectedItem = existing.PaymentMethod;
                cmbStatus.SelectedItem = existing.Status;
            }
            else
            {
                dtpDate.Value = DateTime.Today;
                cmbMethod.SelectedIndex = 0;
                cmbStatus.SelectedIndex = 0;
            }

            var fields = new List<(string Label, Control Input)>
            {
                ("Category", cmbCategory),
                ("Description", txtDescription),
                ("Amount", numAmount),
                ("Date", dtpDate),
                ("Payment method", cmbMethod),
                ("Status", cmbStatus)
            };

            if (isAdmin) fields.Add(("Branch", cmbBranch));

            int y = 15;
            foreach (var (label, input) in fields)
            {
                Controls.Add(new Label { Text = label, Location = new Point(20, y + 4), AutoSize = true });

                input.Location = new Point(160, y);
                input.Width = input == cmbCategory ? 170 : 240;
                Controls.Add(input);
                y += 38;
            }

            btnNewCategory.Location = new Point(335, 14);
            btnNewCategory.Size = new Size(65, 26);
            Controls.Add(btnNewCategory);

            lblStatus.Location = new Point(20, y + 4);
            lblStatus.Size = new Size(380, 20);
            lblStatus.ForeColor = Color.Firebrick;
            Controls.Add(lblStatus);

            ok.Text = "Save";
            ok.Location = new Point(230, y + 30);
            ok.Size = new Size(80, 32);
            ok.Enabled = false;
            ok.Click += (_, _) => Validate_();

            var cancel = new Button
            {
                Text = "Cancel",
                Location = new Point(320, y + 30),
                Size = new Size(80, 32),
                DialogResult = DialogResult.Cancel
            };

            Controls.Add(ok);
            Controls.Add(cancel);
            AcceptButton = ok;
            CancelButton = cancel;

            ClientSize = new Size(420, y + 80);

            Shown += async (_, _) => await LoadListsAsync();
        }

        private void Validate_()
        {
            if (cmbCategory.SelectedItem is not Choice)
            {
                lblStatus.Text = "Choose a category.";
                return;
            }

            if (string.IsNullOrWhiteSpace(txtDescription.Text))
            {
                lblStatus.Text = "Description is required.";
                return;
            }

            if (numAmount.Value <= 0)
            {
                lblStatus.Text = "Amount must be greater than zero.";
                return;
            }

            DialogResult = DialogResult.OK;
        }

        private async Task LoadListsAsync()
        {
            try
            {
                lblStatus.Text = "";
                await LoadCategoriesAsync(null);
                if (_isAdmin) await LoadBranchesAsync();
                ok.Enabled = cmbCategory.Items.Count > 0;

                if (cmbCategory.Items.Count == 0)
                    lblStatus.Text = "No categories yet. Click \"+ New\" to add one.";
            }
            catch (Exception ex)
            {
                lblStatus.Text = "Could not load lists: " + ex.Message;
            }
        }

        private async Task LoadCategoriesAsync(string? selectName)
        {
            var list = await _http.GetFromJsonAsync<List<CategoryLite>>(
                           $"tenant/{_companyId}/expenses/categories")
                       ?? new List<CategoryLite>();

            cmbCategory.Items.Clear();

            foreach (var c in list)
                cmbCategory.Items.Add(new Choice(c.ExpenseCategoryId, c.CategoryName));

            if (cmbCategory.Items.Count == 0) return;

            int index = 0;

            if (selectName != null)
            {
                index = list.FindIndex(c =>
                    string.Equals(c.CategoryName, selectName, StringComparison.OrdinalIgnoreCase));
            }
            else if (_existing != null)
            {
                index = list.FindIndex(c => c.ExpenseCategoryId == _existing.ExpenseCategoryId);
            }

            cmbCategory.SelectedIndex = index < 0 ? 0 : index;
            ok.Enabled = true;
        }

        private async Task LoadBranchesAsync()
        {
            var list = await _http.GetFromJsonAsync<List<BranchLite>>(
                           $"tenant/{_companyId}/branches")
                       ?? new List<BranchLite>();

            cmbBranch.Items.Clear();

            cmbBranch.Items.Add(new Choice(null,
                _existing == null ? "(Default: my branch)" : "(No branch / Unassigned)"));

            foreach (var b in list.Where(b => b.IsActive || b.BranchId == _existing?.BranchId)
                                  .OrderBy(b => b.BranchName))
            {
                cmbBranch.Items.Add(new Choice(b.BranchId, b.BranchName));
            }

            int index = 0;

            if (_existing?.BranchId != null)
            {
                for (int i = 0; i < cmbBranch.Items.Count; i++)
                {
                    if (((Choice)cmbBranch.Items[i]!).Id == _existing.BranchId)
                    {
                        index = i;
                        break;
                    }
                }
            }

            cmbBranch.SelectedIndex = index;
        }

        private async Task AddCategoryAsync()
        {
            using var f = new Form
            {
                Text = "New expense category",
                FormBorderStyle = FormBorderStyle.FixedDialog,
                StartPosition = FormStartPosition.CenterParent,
                ClientSize = new Size(340, 110),
                MaximizeBox = false,
                MinimizeBox = false
            };

            var tb = new TextBox { Location = new Point(20, 20), Width = 300, MaxLength = 100 };
            var save = new Button { Text = "Save", DialogResult = DialogResult.OK, Location = new Point(160, 60), Size = new Size(75, 30) };
            var cancel = new Button { Text = "Cancel", DialogResult = DialogResult.Cancel, Location = new Point(245, 60), Size = new Size(75, 30) };

            f.Controls.AddRange(new Control[] { tb, save, cancel });
            f.AcceptButton = save;
            f.CancelButton = cancel;

            if (f.ShowDialog(this) != DialogResult.OK || string.IsNullOrWhiteSpace(tb.Text))
                return;

            string name = tb.Text.Trim();

            var response = await _http.PostAsJsonAsync(
                $"tenant/{_companyId}/expenses/categories",
                new { categoryName = name });

            if (!response.IsSuccessStatusCode)
            {
                lblStatus.Text = await BranchesWinForms.ReadMessageAsync(response);
                return;
            }

            lblStatus.Text = "";
            await LoadCategoriesAsync(name);
        }
    }
}