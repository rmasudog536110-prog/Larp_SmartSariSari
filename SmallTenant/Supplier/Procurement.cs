using Guna.UI2.WinForms;
using Larp.winforms.SmallTenant.Expenses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Larp.winforms.SmallTenant.Supplier
{
    public partial class Procurement : Form
    {
        private readonly HttpClient _httpClient;
        private readonly int _companyId;

        private readonly List<ProcurementItem> _items = new();            
        private Guna.UI2.WinForms.Guna2ComboBox? cmbBranch;
            private bool IsAdmin => Session.Role == "Admin" || Session.Role == "SuperAdmin";

        public Procurement(HttpClient httpClient, int companyId)
        {
            InitializeComponent();

            _httpClient = httpClient;
            _companyId = companyId;

            btnRemoveItem.Tag = "danger";

            AppTheme.Apply(this);

            if (IsAdmin) BuildBranchPicker();


            // The server sets the purchase date, so the picker is display-only.
            dtpPurchaseDate.Enabled = false;
            purchaseDateLabel.Text = "Purchase Date (automatic)";

            ConfigureGrid();

            Load += Procurement_Load;
            btnAddItem.Click += btnAddItem_Click;
            btnRemoveItem.Click += btnRemoveItem_Click;
            btnSavePurchase.Click += btnSavePurchase_Click;

            btnClear.Click += btnClear_Click;
            btnClose.Click += btnClose_Click;
        }

private void BuildBranchPicker()
        {
            var lbl = new Guna.UI2.WinForms.Guna2HtmlLabel
            {
                Text = "Branch (optional)",
                Font = new Font("Segoe UI Semibold", 10F),
                BackColor = Color.Transparent,
                Location = new Point(640, 105)
            };

            cmbBranch = new Guna.UI2.WinForms.Guna2ComboBox
            {
                BorderRadius = 8,
                DrawMode = DrawMode.OwnerDrawFixed,
                DropDownStyle = ComboBoxStyle.DropDownList,
                Font = new Font("Segoe UI", 10F),
                ForeColor = Color.FromArgb(68, 88, 112),
                BackColor = Color.Transparent,
                ItemHeight = 30,
                Location = new Point(640, 130),
                Size = new Size(260, 36)
            };

            Controls.Add(lbl);
            Controls.Add(cmbBranch);
        }

        private async Task LoadBranchesAsync()
        {
            if (cmbBranch == null) return;

            var list = await _httpClient.GetFromJsonAsync<List<BranchLite>>($"tenant/{_companyId}/branches")
                       ?? new List<BranchLite>();

            var items = new List<BranchLite> { new() { BranchId = 0, BranchName = "(Default: my branch)" } };
            items.AddRange(list.Where(b => b.IsActive).OrderBy(b => b.BranchName));

            cmbBranch.DataSource = items;
            cmbBranch.DisplayMember = "BranchName";
            cmbBranch.SelectedIndex = 0;
        }

        private class BranchLite
            {
                public int BranchId { get; set; }
                public string BranchName { get; set; } = string.Empty;
                public bool IsActive { get; set; }
        }

        private async void Procurement_Load(object? sender, EventArgs e)
        {
            try
            {
                guna2ProgressIndicator1.Visible = true;

                await LoadSuppliersAsync();
                await LoadProductsAsync();
                await LoadBranchesAsync();

                dtpPurchaseDate.Value = DateTime.Now;
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Unable to load procurement data.\n\n{ex.Message}",
                    "Procurement",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            finally
            {
                guna2ProgressIndicator1.Visible = false;
            }
        }

        private async Task LoadSuppliersAsync()
        {
            var response = await _httpClient.GetAsync(
                $"tenant/{_companyId}/suppliers");

            response.EnsureSuccessStatusCode();

            var suppliers =
                await response.Content.ReadFromJsonAsync<List<SupplierDto>>()
                ?? new List<SupplierDto>();

            // Only active suppliers can be used in a purchase.
            cmbSupplier.DataSource = suppliers
                .Where(x => x.IsActive)
                .OrderBy(x => x.SupplierName)
                .ToList();

            cmbSupplier.DisplayMember = "SupplierName";
            cmbSupplier.ValueMember = "SupplierId";
            cmbSupplier.SelectedIndex = -1;
        }

        private async Task LoadProductsAsync()
        {
            var response = await _httpClient.GetAsync(
                $"tenant/{_companyId}/products");

            response.EnsureSuccessStatusCode();

            var products =
                await response.Content.ReadFromJsonAsync<List<ProductDto>>()
                ?? new List<ProductDto>();

            cmbProduct.DataSource = products
                .Where(x => x.IsActive)
                .OrderBy(x => x.ProductName)
                .ToList();

            cmbProduct.DisplayMember = "ProductName";
            cmbProduct.ValueMember = "ProductId";
            cmbProduct.SelectedIndex = -1;
        }

        private void ConfigureGrid()
        {
            PurchaseGrid.AutoGenerateColumns = false;
            PurchaseGrid.Columns.Clear();

            PurchaseGrid.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "ProductId",
                DataPropertyName = "ProductId",
                Visible = false
            });

            PurchaseGrid.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "ProductName",
                HeaderText = "Product",
                DataPropertyName = "ProductName",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            });

            PurchaseGrid.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Quantity",
                HeaderText = "Quantity",
                DataPropertyName = "Quantity",
                Width = 100
            });

            PurchaseGrid.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "UnitCost",
                HeaderText = "Unit Cost",
                DataPropertyName = "UnitCost",
                Width = 120,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Format = "₱#,##0.00"
                }
            });

            PurchaseGrid.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "TotalAmount",
                HeaderText = "Total",
                DataPropertyName = "TotalAmount",
                Width = 130,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Format = "₱#,##0.00"
                }
            });
        }

        private void btnAddItem_Click(object? sender, EventArgs e)
        {
            if (cmbProduct.SelectedIndex < 0)
            {
                MessageBox.Show(
                    "Please select a product.",
                    "Procurement",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            if (!decimal.TryParse(
                    txtQuantity.Text.Trim(),
                    out decimal quantity) ||
                quantity <= 0)
            {
                MessageBox.Show(
                    "Please enter a valid quantity.",
                    "Procurement",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            if (!decimal.TryParse(
                    txtUnitCost.Text.Trim(),
                    out decimal unitCost) ||
                unitCost < 0)
            {
                MessageBox.Show(
                    "Please enter a valid unit cost.",
                    "Procurement",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            var product = cmbProduct.SelectedItem as ProductDto;

            if (product == null)
            {
                MessageBox.Show(
                    "The selected product could not be loaded.",
                    "Procurement",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            var existing =
                _items.FirstOrDefault(x =>
                    x.ProductId == product.ProductId);

            if (existing != null)
            {
                existing.Quantity += quantity;
                existing.UnitCost = unitCost;
                existing.TotalAmount =
                    existing.Quantity * existing.UnitCost;
            }
            else
            {
                _items.Add(new ProcurementItem
                {
                    ProductId = product.ProductId,
                    ProductName = product.ProductName,
                    Quantity = quantity,
                    UnitCost = unitCost,
                    TotalAmount = quantity * unitCost
                });
            }

            RefreshGrid();

            cmbProduct.SelectedIndex = -1;
            txtQuantity.Clear();
            txtUnitCost.Clear();
        }

        private void btnRemoveItem_Click(object? sender, EventArgs e)
        {
            if (PurchaseGrid.CurrentRow == null)
            {
                return;
            }

            if (PurchaseGrid.CurrentRow.DataBoundItem
                is not ProcurementItem item)
            {
                return;
            }

            _items.Remove(item);

            RefreshGrid();
        }

        private void RefreshGrid()
        {
            PurchaseGrid.DataSource = null;
            PurchaseGrid.DataSource = _items;

            decimal total = _items.Sum(x => x.TotalAmount);

            lblGrandTotal.Text = $"₱{total:N2}";
        }

        private async void btnSavePurchase_Click(
            object? sender,
            EventArgs e)
        {
            if (cmbSupplier.SelectedIndex < 0)
            {
                MessageBox.Show(
                    "Please select a supplier.",
                    "Procurement",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            if (_items.Count == 0)
            {
                MessageBox.Show(
                    "Please add at least one product.",
                    "Procurement",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            try
            {
                btnSavePurchase.Enabled = false;
                guna2ProgressIndicator1.Visible = true;


                var request = new PurchaseCreateRequest
                {
                    SupplierId = Convert.ToInt32(cmbSupplier.SelectedValue),
                    BranchId = (cmbBranch?.SelectedItem as BranchLite) is { BranchId: > 0 } b ? b.BranchId : null,
                    Items = _items.Select(x =>
                        new PurchaseItemCreateRequest
                        {
                            ProductId = x.ProductId,
                            Quantity = x.Quantity,
                            UnitCost = x.UnitCost
                        }).ToList()
                };

                var response =
                    await _httpClient.PostAsJsonAsync(
                        $"tenant/{_companyId}/purchases",
                        request);

                if (!response.IsSuccessStatusCode)
                {
                    MessageBox.Show(
                        await ReadMessageAsync(response),
                        "Purchase Failed",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);

                    return;
                }

                MessageBox.Show(
                    "Purchase recorded successfully.\n\nInventory has been updated.",
                    "Procurement",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Unable to save purchase.\n\n{ex.Message}",
                    "Procurement",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            finally
            {
                btnSavePurchase.Enabled = true;
                guna2ProgressIndicator1.Visible = false;
            }
        }

        private void btnClear_Click(object? sender, EventArgs e)
        {
            ClearForm();
        }

        private void ClearForm()
        {
            _items.Clear();

            PurchaseGrid.DataSource = null;
            PurchaseGrid.DataSource = _items;

            cmbSupplier.SelectedIndex = -1;
            cmbProduct.SelectedIndex = -1;

            txtQuantity.Clear();
            txtUnitCost.Clear();

            dtpPurchaseDate.Value = DateTime.Now;

            lblGrandTotal.Text = "₱0.00";
        }

        private void btnClose_Click(object? sender, EventArgs e)
        {
            Close();
        }

        private static async Task<string> ReadMessageAsync(
            HttpResponseMessage response)
        {
            try
            {
                var body = await response.Content
                    .ReadFromJsonAsync<JsonElement>();

                if (body.TryGetProperty("message", out var m) &&
                    m.GetString() is string s)
                {
                    return s;
                }
            }
            catch
            {
            }

            return $"Request failed ({(int)response.StatusCode}).";
        }

        private class SupplierDto
        {
            public int SupplierId { get; set; }
            public string SupplierName { get; set; } = string.Empty;
            public bool IsActive { get; set; }
        }

        private class ProductDto
        {
            public int ProductId { get; set; }
            public string ProductCode { get; set; } = string.Empty;
            public string ProductName { get; set; } = string.Empty;
            public decimal UnitPrice { get; set; }
            public bool IsActive { get; set; }
        }

        private class ProcurementItem
        {
            public int ProductId { get; set; }
            public string ProductName { get; set; } = string.Empty;
            public decimal Quantity { get; set; }
            public decimal UnitCost { get; set; }
            public decimal TotalAmount { get; set; }
        }

        private class PurchaseCreateRequest
        {
            public int SupplierId { get; set; }
            public List<PurchaseItemCreateRequest> Items { get; set; }
                = new();
            public int? BranchId { get; set; }
        }

        private class PurchaseItemCreateRequest
        {
            public int ProductId { get; set; }
            public decimal Quantity { get; set; }
            public decimal UnitCost { get; set; }
        }
    }
}