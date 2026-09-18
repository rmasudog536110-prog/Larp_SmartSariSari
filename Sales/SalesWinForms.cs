using Guna.UI2.WinForms;
using Larp.winforms.Inventory;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Larp.winforms.Sales
{
    public partial class SalesWinForms : Form
    {
        private readonly HttpClient _httpClient;
        private readonly int _companyId;
        private readonly List<SaleItem> cart = new();

        public SalesWinForms(HttpClient httpClient, int companyId)
        {
            InitializeComponent();

            _httpClient = httpClient;
            _companyId = companyId;

            StartPosition = FormStartPosition.CenterScreen;
        }

        private async void SalesWinForms_Load(object sender, EventArgs e)
        {
            SetupTable();

            totaltxt.ReadOnly = true;
            cashtxt.ReadOnly = false;
            balancetxt.ReadOnly = true;

            totaltxt.Text = "0.00";
            cashtxt.Text = "0.00";
            balancetxt.Text = "0.00";

            SEARCHBTN.Click += SEARCHBTN_Click;
            ADDPRDCT.Click += ADDPRDCT_Click;
            CLEARBTN.Click += CLEARBTN_Click;
            VOID.Click += VOID_Click;
            PAYBTN.Click += PAYBTN_Click;
            QUANTITY.Click += QUANTITY_Click;
            ViewInventory.Click += ViewInventory_Click;

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
                    Format = "N2"
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
        }

        private async Task<List<SaleProduct>> GetProducts()
        {
            try
            {
                var products = await _httpClient.GetFromJsonAsync<List<SaleProduct>>(
                    $"/tenant/{_companyId}/products"
                );

                return products ?? new List<SaleProduct>();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Unable to load products.\n\n" + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );

                return new List<SaleProduct>();
            }
        }

        private async Task<SaleProduct?> GetProductBySearch(string search)
        {
            var products = await GetProducts();

            if (string.IsNullOrWhiteSpace(search))
                return null;

            return products.FirstOrDefault(p =>
                p.IsActive &&
                (
                    p.ProductName.Equals(search, StringComparison.OrdinalIgnoreCase) ||
                    p.ProductCode.Equals(search, StringComparison.OrdinalIgnoreCase)
                )
            );
        }

        private async void SEARCHBTN_Click(object sender, EventArgs e)
        {
            string searchtxt = search.Text.Trim();

            if (string.IsNullOrWhiteSpace(searchtxt))
            {
                MessageBox.Show(
                    "Please enter a product name or product code.",
                    "Search Product",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );

                search.Focus();
                return;
            }

            var product = await GetProductBySearch(searchtxt);

            if (product == null)
            {
                MessageBox.Show(
                    "Product not found.",
                    "Search Product",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );

                return;
            }

            MessageBox.Show(
                $"Product Found:\n\n" +
                $"Code: {product.ProductCode}\n" +
                $"Name: {product.ProductName}\n" +
                $"Price: ₱{product.UnitPrice:N2}",
                "Product Found",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );
        }

        private async void ADDPRDCT_Click(object sender, EventArgs e)
        {
            string searchtxt = search.Text.Trim();

            if (string.IsNullOrWhiteSpace(searchtxt))
            {
                MessageBox.Show(
                    "Please enter a product name or product code.",
                    "Add Product",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );

                search.Focus();
                return;
            }

            var product = await GetProductBySearch(searchtxt);

            if (product == null)
            {
                MessageBox.Show(
                    "Product not found.",
                    "Add Product",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );

                return;
            }

            var existingItem = cart.FirstOrDefault(
                x => x.ProductId == product.ProductId
            );

            if (existingItem != null)
            {
                existingItem.Quantity++;
            }
            else
            {
                cart.Add(new SaleItem
                {
                    ProductId = product.ProductId,
                    ProductName = product.ProductName,
                    UnitPrice = product.UnitPrice,
                    Quantity = 1
                });
            }

            RefreshTable();

            search.Clear();
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

            search.Clear();
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
                    MessageBoxIcon.Information
                );

                return;
            }

            decimal total = cart.Sum(item =>
                item.UnitPrice * item.Quantity
            );

            if (!decimal.TryParse(cashtxt.Text, out decimal cash))
            {
                MessageBox.Show(
                    "Please enter a valid cash amount.",
                    "Payment",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                cashtxt.Focus();
                return;
            }

            if (cash < total)
            {
                MessageBox.Show(
                    $"Insufficient cash.\n\n" +
                    $"Total: ₱{total:N2}\n" +
                    $"Cash: ₱{cash:N2}",
                    "Payment",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                cashtxt.Focus();
                return;
            }

            decimal balance = cash - total;

            PAYBTN.Enabled = false;

            try
            {
                foreach (var item in cart)
                {
                    bool success = await ReduceInventoryAfterSale(item);

                    if (!success)
                    {
                        MessageBox.Show(
                            $"The payment could not be completed because the inventory for " +
                            $"'{item.ProductName}' could not be updated.",
                            "Payment Error",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error
                        );

                        return;
                    }
                }

                MessageBox.Show(
                    $"Payment successful!\n\n" +
                    $"Total: ₱{total:N2}\n" +
                    $"Cash: ₱{cash:N2}\n" +
                    $"Change: ₱{balance:N2}",
                    "Payment Successful",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );

                cart.Clear();

                RefreshTable();

                search.Clear();
                cashtxt.Text = "0.00";
                balancetxt.Text = "0.00";

                search.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "An error occurred while processing the payment.\n\n" +
                    ex.Message,
                    "Payment Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
            finally
            {
                PAYBTN.Enabled = true;
            }
        }

        private async Task<bool> ReduceInventoryAfterSale(SaleItem item)
        {
            var inventory = await _httpClient.GetFromJsonAsync<StockInventory>(
                $"/tenant/{_companyId}/inventory/{item.ProductId}"
            );

            if (inventory == null)
                return false;

            decimal newQuantity = inventory.QuantityOnHand - item.Quantity;

            var request = new
            {
                QuantityOnHand = newQuantity,
                ReorderLevel = inventory.ReorderLevel
            };

            var response = await _httpClient.PutAsJsonAsync(
                $"/tenant/{_companyId}/inventory/{item.ProductId}",
                request
            );

            return response.IsSuccessStatusCode;
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

            NumericUpDown quantityBox = new NumericUpDown
            {
                Minimum = 1,
                Maximum = 100000,
                Value = selectedItem.Quantity,
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
            var form = new InventoryWinForms(
                _httpClient,
                _companyId
            );

            form.Show();

            //this.Close();
        }

        private void txtCash_TextChanged(object sender, EventArgs e)
        {
            CalculateBalance();
        }
    }

    public class SaleItem
    {
        public int ProductId { get; set; }

        public string ProductName { get; set; } = string.Empty;

        public decimal UnitPrice { get; set; }

        public int Quantity { get; set; }
    }

    public class SaleProduct
    {
        public int ProductId { get; set; }

        public string ProductCode { get; set; } = string.Empty;

        public string ProductName { get; set; } = string.Empty;

        public decimal UnitPrice { get; set; }

        public bool IsActive { get; set; }
    }

    public class StockInventory
    {
        public int ProductId { get; set; }

        public decimal QuantityOnHand { get; set; }

        public decimal ReorderLevel { get; set; }
    }
}