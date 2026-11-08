using Guna.UI2.WinForms;
using Larp.domain.Entities;
using Larp.winforms.MicroTenant.Inventory;
using Larp.winforms;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Windows.Forms;
using Larp.winforms.SmallTenant.Dashboard;

namespace Larp.winforms.MicroTenant.Sales
{
    public partial class SalesWinForms : Form
    {
        private readonly HttpClient _httpClient;
        private readonly int _companyId;
        private readonly List<SaleItem> cart = new();
        private List<SaleProduct> products = new();
        private SaleProduct? selectedProduct;
        private string _paymentMethod = "Cash";

        private bool _suppressSearch;

        public SalesWinForms(HttpClient httpClient, int companyId)
        {
            InitializeComponent();

            _httpClient = httpClient;
            _companyId = companyId;

            AppTheme.Apply(this);

            StartPosition = FormStartPosition.CenterScreen;
            CARDBTN.Click += CARDBTN_Click;
            GCASHBTN.Click += GCASHBTN_Click;
        }

        private async void SalesWinForms_Load(object sender, EventArgs e)
        {
            SetupTable();

            totaltxt.ReadOnly = true;
            cashtxt.ReadOnly = false;
            balancetxt.ReadOnly = true;

            string role = Session.Role;

            if (role == "Admin" || role == "Manager" || role == "SuperAdmin")
            {
                LogoutBtn.Text = "DASHBOARD";
            }
            else
            {
                LogoutBtn.Text = "LOGOUT";
            }


            totaltxt.Text = "0.00";
            cashtxt.Text = "0.00";
            balancetxt.Text = "0.00";



            await GetProducts();

            search.Focus();
        }

        private void SetupTable()
        {
            table.Columns.Clear();
            table.AutoGenerateColumns = false;
            table.AllowUserToAddRows = false;
            table.ReadOnly = true;
            table.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            table.MultiSelect = false;

            table.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "ProductId",
                HeaderText = "ID",
                DataPropertyName = "ProductId",
                Visible = false
            });

            table.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "ProductName",
                HeaderText = "Product",
                DataPropertyName = "ProductName",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            });

            table.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "UnitPrice",
                HeaderText = "Price",
                DataPropertyName = "UnitPrice",
                Width = 100,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Format = "₱#,##0.00"
                }
            });

            table.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Quantity",
                HeaderText = "Qty",
                DataPropertyName = "Quantity",
                Width = 80
            });

            table.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Subtotal",
                HeaderText = "Subtotal",
                Width = 120,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Format = "N2"
                }
            });

            AppTheme.AlignRight(table, "UnitPrice", "Quantity", "Subtotal");
        }

        private async Task<List<SaleProduct>> GetProducts()
        {
            try
            {
                var productsTask = _httpClient.GetFromJsonAsync<List<ProductDto>>($"/tenant/{_companyId}/products");
                var inventoryTask = _httpClient.GetFromJsonAsync<List<InventoryDto>>($"/tenant/{_companyId}/inventory");

                await Task.WhenAll(productsTask, inventoryTask);

                var productList = productsTask.Result ?? new List<ProductDto>();
                var inventoryList = inventoryTask.Result ?? new List<InventoryDto>();

                // Stock lookup by name (sum in case a product has several inventory rows)
                var stockByName = inventoryList
                    .GroupBy(i => i.ProductName.Trim(), StringComparer.OrdinalIgnoreCase)
                    .ToDictionary(g => g.Key, g => g.Sum(i => i.QuantityOnHand),
                                  StringComparer.OrdinalIgnoreCase);

                products = productList
                    .Where(p => p.IsActive)
                    .Select(p => new SaleProduct
                    {
                        ProductId = p.ProductId,
                        ProductCode = p.ProductCode,
                        ProductName = p.ProductName,
                        UnitPrice = p.UnitPrice,
                        IsVatable = p.IsVatable,
                        IsActive = p.IsActive,
                        QuantityOnHand = stockByName.TryGetValue(p.ProductName.Trim(), out var qty) ? qty : 0
                    })
                    .Where(p => p.QuantityOnHand > 0)
                    .OrderBy(p => p.ProductName)
                    .ToList();

                return products;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unable to load products.\n\n" + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                products = new List<SaleProduct>();
                return products;
            }
        }

        private void search_TextChanged(object sender, EventArgs e)
        {
            if (_suppressSearch)
                return;

            selectedProduct = null;

            ShowSuggestions();
        }

        private void ShowSuggestions()
        {
            string text = search.Text.Trim();

            if (text.Length == 0)
            {
                HideSuggestions();
                return;
            }

            var matches = products
                .Where(p =>
                    p.ProductName.Contains(text, StringComparison.OrdinalIgnoreCase) ||
                    p.ProductCode.Contains(text, StringComparison.OrdinalIgnoreCase))
                .OrderByDescending(p =>
                    p.ProductName.StartsWith(text, StringComparison.OrdinalIgnoreCase) ||
                    p.ProductCode.StartsWith(text, StringComparison.OrdinalIgnoreCase))
                .ThenBy(p => p.ProductName)
                .Take(8)
                .ToList();

            if (matches.Count == 0)
            {
                HideSuggestions();
                return;
            }

            suggestionList.BeginUpdate();
            suggestionList.Items.Clear();

            foreach (var match in matches)
                suggestionList.Items.Add(match);

            suggestionList.EndUpdate();

            suggestionList.SelectedIndex = 0;

            suggestionList.Location = new Point(search.Left, search.Bottom + 2);
            suggestionList.Width = search.Width;
            suggestionList.Height = matches.Count * suggestionList.ItemHeight + 6;
            suggestionList.Visible = true;
            suggestionList.BringToFront();
        }

        private void HideSuggestions()
        {
            suggestionList.Visible = false;
            suggestionList.Items.Clear();
        }

        private void ChooseProduct(SaleProduct product)
        {
            selectedProduct = product;

            _suppressSearch = true;
            search.Text = product.ProductName;
            _suppressSearch = false;

            HideSuggestions();
            search.Focus();
        }

        private void ClearSearch()
        {
            _suppressSearch = true;
            search.Text = "";
            _suppressSearch = false;

            selectedProduct = null;
            HideSuggestions();
        }

        private SaleProduct? ResolveSelection()
        {
            if (selectedProduct != null)
                return selectedProduct;

            string text = search.Text.Trim();

            if (text.Length == 0)
                return null;

            var matches = products
                .Where(p =>
                    p.ProductName.Contains(text, StringComparison.OrdinalIgnoreCase) ||
                    p.ProductCode.Contains(text, StringComparison.OrdinalIgnoreCase))
                .Take(2)
                .ToList();

            return matches.Count == 1 ? matches[0] : null;
        }

        private void search_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Down:
                    if (suggestionList.Visible && suggestionList.Items.Count > 0)
                    {
                        suggestionList.SelectedIndex = Math.Min(
                            suggestionList.SelectedIndex + 1,
                            suggestionList.Items.Count - 1);
                    }
                    else
                    {
                        ShowSuggestions();
                    }

                    e.Handled = true;
                    e.SuppressKeyPress = true;
                    break;

                case Keys.Up:
                    if (suggestionList.Visible && suggestionList.Items.Count > 0)
                    {
                        suggestionList.SelectedIndex = Math.Max(
                            suggestionList.SelectedIndex - 1,
                            0);
                    }

                    e.Handled = true;
                    e.SuppressKeyPress = true;
                    break;

                case Keys.Escape:
                    HideSuggestions();
                    e.Handled = true;
                    e.SuppressKeyPress = true;
                    break;

                case Keys.Enter:
                    e.Handled = true;
                    e.SuppressKeyPress = true;
                    HandleEnter();
                    break;
            }
        }

        private void HandleEnter()
        {
            string text = search.Text.Trim();

            // An exact product code or name (for example from a barcode scanner)
            // is added to the cart straight away.
            if (text.Length > 0)
            {
                var exact = products.FirstOrDefault(p =>
                    p.ProductCode.Equals(text, StringComparison.OrdinalIgnoreCase) ||
                    p.ProductName.Equals(text, StringComparison.OrdinalIgnoreCase));

                if (exact != null)
                {
                    selectedProduct = exact;
                    AddSelectedProductToCart();
                    return;
                }
            }

            // Otherwise Enter picks the highlighted suggestion...
            if (suggestionList.Visible && suggestionList.SelectedItem is SaleProduct picked)
            {
                ChooseProduct(picked);
                return;
            }

            // ...and Enter again adds the picked product.
            if (selectedProduct != null)
                AddSelectedProductToCart();
        }

        private void search_Leave(object sender, EventArgs e)
        {
            // Keep the list open when the mouse is over it (the user is clicking an item).
            var mouse = suggestionList.PointToClient(Cursor.Position);

            if (!suggestionList.ClientRectangle.Contains(mouse))
                HideSuggestions();
        }

        private void suggestionList_MouseClick(object sender, MouseEventArgs e)
        {
            int index = suggestionList.IndexFromPoint(e.Location);

            if (index >= 0 && suggestionList.Items[index] is SaleProduct product)
                ChooseProduct(product);
        }

        private void SEARCHBTN_Click(object sender, EventArgs e)
        {
            var product = ResolveSelection();

            if (product == null)
            {
                MessageBox.Show(
                    "Please select a product first.",
                    "Search Product",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );

                search.Focus();
                return;
            }

            MessageBox.Show(
                $"Product Found:\n\n" +
                $"Name: {product.ProductName}\n" +
                $"Price: ₱{product.UnitPrice:N2}",
                "Product Found",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );
        }

        private void CARDBTN_Click(object sender, EventArgs e)
        {
            _paymentMethod = "Card";
            cashtxt.Text = totaltxt.Text;
            CalculateBalance();
        }

        private void GCASHBTN_Click(object sender, EventArgs e)
        {
            _paymentMethod = "GCash";
            cashtxt.Text = totaltxt.Text;
            CalculateBalance();
        }

        private void SelectCashPayment()
        {
            _paymentMethod = "Cash";
        }

        private void ADDPRDCT_Click(object sender, EventArgs e)
        {
            var product = ResolveSelection();

            if (product == null)
            {
                MessageBox.Show(
                    "Please select a product first.",
                    "Add Product",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );

                search.Focus();
                return;
            }

            selectedProduct = product;
            AddSelectedProductToCart();
        }

        private void AddSelectedProductToCart()
        {
            if (selectedProduct == null)
                return;

            var existingItem = cart.FirstOrDefault(
                x => x.ProductId == selectedProduct.ProductId
            );

            // Cannot put more in the cart than the stock on hand.
            int alreadyInCart = existingItem?.Quantity ?? 0;

            if (alreadyInCart + 1 > selectedProduct.QuantityOnHand)
            {
                MessageBox.Show(
                    $"Only {selectedProduct.QuantityOnHand:0.##} of " +
                    $"{selectedProduct.ProductName} in stock.",
                    "Not enough stock",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );

                ClearSearch();
                search.Focus();
                return;
            }

            if (existingItem != null)
            {
                existingItem.Quantity++;
            }
            else
            {
                cart.Add(new SaleItem
                {
                    ProductId = selectedProduct.ProductId,
                    ProductName = selectedProduct.ProductName,
                    UnitPrice = selectedProduct.UnitPrice,
                    IsVatable = selectedProduct.IsVatable,
                    Quantity = 1
                });
            }

            RefreshTable();

            ClearSearch();
            search.Focus();
        }

        private void RefreshTable()
        {
            table.Rows.Clear();

            foreach (var item in cart)
            {
                decimal subtotal = item.UnitPrice * item.Quantity;

                int rowIndex = table.Rows.Add(
                    item.ProductId,
                    item.ProductName,
                    item.UnitPrice,
                    item.Quantity,
                    subtotal
                );

                table.Rows[rowIndex].Tag = item;
            }

            CalculateTotals();
        }

        private void CalculateTotals()
        {
            decimal total = cart.Sum(item =>
                item.UnitPrice * item.Quantity
            );

            totaltxt.Text = total.ToString("N2");

            CalculateBalance();
            ShowVatPreview();
        }

        private void CalculateBalance()
        {
            decimal total = cart.Sum(item =>
                item.UnitPrice * item.Quantity
            );

            if (!decimal.TryParse(cashtxt.Text, out decimal cash))
            {
                balancetxt.Text = "0.00";
                return;
            }

            decimal balance = cash - total;

            balancetxt.Text = balance.ToString("N2");
        }

        private void cashtxt_TextChanged(object sender, EventArgs e)
        {
            CalculateBalance();
        }

        private void CLEARBTN_Click(object sender, EventArgs e)
        {
            cart.Clear();

            RefreshTable();

            ClearSearch();
            cashtxt.Text = "0.00";
            balancetxt.Text = "0.00";

            search.Focus();
        }

        private void VOID_Click(object sender, EventArgs e)
        {
            if (table.SelectedRows.Count == 0)
            {
                MessageBox.Show(
                    "Please select a product to void.",
                    "Void Item",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );

                return;
            }

            var selectedRow = table.SelectedRows[0];

            if (selectedRow.Tag is not SaleItem selectedItem)
                return;

            var result = MessageBox.Show(
                $"Remove {selectedItem.ProductName} from the cart?",
                "Void Item",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (result != DialogResult.Yes)
                return;

            cart.Remove(selectedItem);

            RefreshTable();
        }
        private async void PAYBTN_Click(object sender, EventArgs e)
        {
            if (cart.Count == 0)
            {
                MessageBox.Show(
                    "The cart is empty.",
                    "Payment",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                return;
            }

            decimal subtotal = cart.Sum(item =>
                item.UnitPrice * item.Quantity);

            if (!decimal.TryParse(
                    cashtxt.Text,
                    out decimal cash))
            {
                MessageBox.Show(
                    "Please enter a valid payment amount.",
                    "Payment",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                cashtxt.Focus();
                return;
            }

            if (_paymentMethod == "Cash" &&
                cash < subtotal)
            {
                MessageBox.Show(
                    $"Insufficient cash.\n\n" +
                    $"Total: ₱{subtotal:N2}\n" +
                    $"Cash: ₱{cash:N2}",
                    "Payment",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                cashtxt.Focus();
                return;
            }

            PAYBTN.Enabled = false;

            try
            {
                var sale =
                    new Larp.domain.Entities.Sales
                    {
                        CustomerId = null,
                        Discount = 0,
                        PaymentMethod = _paymentMethod,
                        CashReceived = cash,

                        SaleItems = cart
                            .Select(item =>
                                new Larp.domain.Entities.SaleItem
                                {
                                    ProductId = item.ProductId,
                                    Quantity = item.Quantity,
                                    UnitPrice = item.UnitPrice,
                                    Discount = 0
                                })
                            .ToList()
                    };

                var response =
                    await _httpClient.PostAsJsonAsync(
                        $"/tenant/{_companyId}/sales",
                        sale);

                if (!response.IsSuccessStatusCode)
                {
                    string errorMessage =
                        await response.Content.ReadAsStringAsync();

                    MessageBox.Show(
                        $"The transaction was not saved.\n\n" +
                        errorMessage,
                        "Payment Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);

                    return;
                }

                var savedSale =
                    await response.Content
                        .ReadFromJsonAsync<SaleResponse>();

                if (savedSale == null)
                {
                    MessageBox.Show(
                        "The sale was saved, but the server returned no sale information.",
                        "Payment",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    return;
                }

                cart.Clear();

                RefreshTable();

                ClearSearch();

                cashtxt.Text = "0.00";
                balancetxt.Text = "0.00";

                await GetProducts();

                search.Focus();

                using var receipt =
                    new ReceiptForm(
                        _httpClient,
                        _companyId,
                        savedSale.SalesId);

                receipt.ShowDialog(this);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "An error occurred while saving the transaction.\n\n" +
                    ex.Message,
                    "Payment Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            finally
            {
                PAYBTN.Enabled = true;
            }
        }
        private void QUANTITY_Click(object sender, EventArgs e)
        {
            if (table.SelectedRows.Count == 0)
            {
                MessageBox.Show(
                    "Please select a product first.",
                    "Quantity",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );

                return;
            }

            var selectedRow = table.SelectedRows[0];

            if (selectedRow.Tag is not SaleItem selectedItem)
                return;

            using Form quantityForm = new Form();

            quantityForm.Text = "Change Quantity";
            quantityForm.StartPosition = FormStartPosition.CenterParent;
            quantityForm.FormBorderStyle = FormBorderStyle.FixedDialog;
            quantityForm.ClientSize = new Size(300, 160);
            quantityForm.MinimizeBox = false;
            quantityForm.MaximizeBox = false;

            Label label = new Label
            {
                Text = "Enter quantity:",
                AutoSize = true,
                Location = new Point(20, 20)
            };

            // Cannot sell more than the stock on hand.
            decimal stock = products
                .FirstOrDefault(p => p.ProductId == selectedItem.ProductId)?.QuantityOnHand
                ?? selectedItem.Quantity;
            decimal maxQuantity = Math.Max(1, Math.Floor(stock));

            NumericUpDown quantityBox = new NumericUpDown
            {
                Minimum = 1,
                Maximum = maxQuantity,
                Value = Math.Min(selectedItem.Quantity, maxQuantity),
                Location = new Point(20, 50),
                Width = 250
            };

            Button saveButton = new Button
            {
                Text = "Save",
                DialogResult = DialogResult.OK,
                Location = new Point(100, 100),
                Width = 100
            };

            quantityForm.Controls.Add(label);
            quantityForm.Controls.Add(quantityBox);
            quantityForm.Controls.Add(saveButton);

            quantityForm.AcceptButton = saveButton;

            if (quantityForm.ShowDialog(this) != DialogResult.OK)
                return;

            selectedItem.Quantity = (int)quantityBox.Value;

            RefreshTable();
        }

        private void ViewInventory_Click(object sender, EventArgs e)
        {
            FormNavigator.GoTo(this, () =>
                new InventoryWinForms(
                    _httpClient,
                    _companyId));
        }

        private void LogoutBtn_Click(object sender, EventArgs e)
        {
            string role = Session.Role;

            if (role == "Admin" || role == "Manager" || role == "SuperAdmin")
            {
                var answer1 = MessageBox.Show(
                "Go to Dashboard?",
                "Dashboard",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);
                if (answer1 != DialogResult.Yes)
                {
                    return; 
                }
                else
                {
                    FormNavigator.GoTo(this, () =>
                        new DashboardWinforms(
                            _httpClient,
                            _companyId));
                }

            }
            else
            {
                var answer2 = MessageBox.Show(
                "Log out of your account?",
                "Logout",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);
                if (answer2 != DialogResult.Yes) return;
            }

            Close();
        }

        private void txtCash_TextChanged(object sender, EventArgs e)
        {
            CalculateBalance();
        }

        private void ShowVatPreview()
        {
            decimal vat = cart.Where(i => i.IsVatable)
                .Sum(i => Math.Round(i.UnitPrice * i.Quantity / 1.12m * 0.12m, 2));
            decimal vatableGross = cart.Where(i => i.IsVatable).Sum(i => i.UnitPrice * i.Quantity);

            vattxt.Text = vat.ToString("N2");
            vatabletxt.Text = (vatableGross - vat).ToString("N2");
        }
    }

    public class SaleItem
    {
        public int ProductId { get; set; }

        public string ProductName { get; set; } = string.Empty;

        public decimal UnitPrice { get; set; }

        public bool IsVatable { get; set; }

        public decimal Discount { get; set; }
        public int Quantity { get; set; }
    }

    public class SaleProduct
    {
        public int ProductId { get; set; }

        public string ProductCode { get; set; } = string.Empty;

        public string ProductName { get; set; } = string.Empty;

        public decimal UnitPrice { get; set; }

        public bool IsActive { get; set; }

        public bool IsVatable { get; set; }

        public decimal QuantityOnHand { get; set; }

        // Text shown for each row of the suggestion list.
        public override string ToString() =>
            $"{ProductName} | ₱{UnitPrice:N2} | ({QuantityOnHand:0.##} left)";
    }

    public class SaleResponse
    {
        public int SalesId { get; set; }

        public string SaleNumber { get; set; } =
            string.Empty;

        public DateTime SaleDate { get; set; }

        public decimal Subtotal { get; set; }

        public decimal Discount { get; set; }

        public decimal TotalAmount { get; set; }

        public decimal VatableSales { get; set; }

        public decimal VatExempt { get; set; }

        public decimal VatAmount { get; set; }

        public decimal CashReceived { get; set; }

        public decimal ChangeAmount { get; set; }

        public string PaymentMethod { get; set; } =
            "Cash";

        public string Status { get; set; } =
            "Completed";
    }

    public class ProductDto
    {
        public int ProductId { get; set; }
        public string ProductCode { get; set; } = string.Empty;
        public string ProductName { get; set; } = string.Empty;
        public decimal UnitPrice { get; set; }
        public bool IsActive { get; set; }
        public bool IsVatable { get; set; }
    }

    public class InventoryDto
    {
        public string ProductName { get; set; } = string.Empty;
        public decimal QuantityOnHand { get; set; }
    }
}