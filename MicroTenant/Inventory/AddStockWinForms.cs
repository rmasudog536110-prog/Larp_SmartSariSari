using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Larp.winforms.MicroTenant.Inventory
{
    public partial class AddStockWinForms : Form
    {
        private readonly HttpClient _httpClient;
        private readonly int _companyId;

        private List<StockProduct> _products = new();

        public AddStockWinForms(
            HttpClient httpClient,
            int companyId)
        {
            InitializeComponent();

            _httpClient = httpClient;
            _companyId = companyId;

            StartPosition = FormStartPosition.CenterParent;

            Load += AddStockWinForms_Load;
            cmbProduct.SelectedIndexChanged += cmbProduct_SelectedIndexChanged;
        }

        private async void AddStockWinForms_Load(
            object sender,
            EventArgs e)
        {
            txtCurrentStock.ReadOnly = true;
            txtCurrentStock.Text = "0";

            cmbProduct.DisplayMember = "ProductName";
            cmbProduct.ValueMember = "ProductId";

            await LoadProducts();
        }

        private async Task LoadProducts()
        {
            try
            {
                cmbProduct.Enabled = false;
                btnAddStock.Enabled = false;

                var products =
                    await _httpClient.GetFromJsonAsync<List<StockProduct>>
                    ($"/tenant/{_companyId}/products");

                _products = products ?? new List<StockProduct>();

                var activeProducts = _products
                    .FindAll(p => p.IsActive);

                cmbProduct.DataSource = null;

                if (activeProducts.Count == 0)
                {
                    MessageBox.Show(
                        "No active products were found in the database.",
                        "Inventory",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    return;
                }

                cmbProduct.DataSource = activeProducts;
                cmbProduct.DisplayMember = "ProductName";
                cmbProduct.ValueMember = "ProductId";

                cmbProduct.SelectedIndex = 0;
            }
            catch (HttpRequestException ex)
            {
                MessageBox.Show(
                    "Unable to connect to the API.\n\n" +
                    "API Address:\n" +
                    _httpClient.BaseAddress +
                    "\n\n" +
                    ex.Message,
                    "Connection Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "An error occurred while loading products.\n\n" +
                    ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            finally
            {
                cmbProduct.Enabled = true;
                btnAddStock.Enabled = true;
            }
        }

        private async void cmbProduct_SelectedIndexChanged(
            object sender,
            EventArgs e)
        {
            if (cmbProduct.SelectedItem is not StockProduct product)
                return;

            await LoadCurrentStock(product.ProductId);
        }

        private async Task LoadCurrentStock(int productId)
        {
            try
            {
                txtCurrentStock.Text = "0";

                var inventory =
                    await _httpClient.GetFromJsonAsync<StockInventory>(
                        $"/tenant/{_companyId}/inventory/{productId}");

                if (inventory == null)
                    return;

                txtCurrentStock.Text =
                    inventory.QuantityOnHand.ToString("0.##");
            }
            catch (HttpRequestException ex)
            {
                txtCurrentStock.Text = "0";

                MessageBox.Show(
                    "Unable to load the current stock.\n\n" +
                    ex.Message,
                    "Inventory Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                txtCurrentStock.Text = "0";

                MessageBox.Show(
                    "Unable to load the current stock.\n\n" +
                    ex.Message,
                    "Inventory Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private async void btnAddStock_Click(
            object sender,
            EventArgs e)
        {
            if (cmbProduct.SelectedItem is not StockProduct product)
            {
                MessageBox.Show(
                    "Please select a product.",
                    "Validation",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                cmbProduct.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtQuantity.Text))
            {
                MessageBox.Show(
                    "Please enter the quantity to add.",
                    "Validation",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                txtQuantity.Focus();
                return;
            }

            if (!decimal.TryParse(
                txtQuantity.Text.Trim(),
                out decimal quantity))
            {
                MessageBox.Show(
                    "Please enter a valid number.",
                    "Validation",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                txtQuantity.Focus();
                return;
            }

            if (quantity <= 0)
            {
                MessageBox.Show(
                    "Quantity must be greater than 0.",
                    "Validation",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                txtQuantity.Focus();
                return;
            }

            if (!decimal.TryParse(
                txtCurrentStock.Text,
                out decimal currentStock))
            {
                currentStock = 0;
            }

            decimal newStock = currentStock + quantity;

            try
            {
                btnAddStock.Enabled = false;
                cmbProduct.Enabled = false;

                var request = new InventoryStockRequest
                {
                    QuantityOnHand = newStock,
                    ReorderLevel = 10
                };

                var response =
                    await _httpClient.PutAsJsonAsync(
                        $"/tenant/{_companyId}/inventory/{product.ProductId}",
                        request);

                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show(
                        $"Stock added successfully.\n\n" +
                        $"Product: {product.ProductName}\n" +
                        $"Previous Stock: {currentStock:0.##}\n" +
                        $"Added Quantity: {quantity:0.##}\n" +
                        $"New Stock: {newStock:0.##}",
                        "Stock Added",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    DialogResult = DialogResult.OK;
                    this.Close();

                    return;
                }

                string responseBody =
                    await response.Content.ReadAsStringAsync();

                MessageBox.Show(
                    "The server rejected the stock update.\n\n" +
                    $"Status: {(int)response.StatusCode} " +
                    $"{response.StatusCode}\n\n" +
                    responseBody,
                    "Stock Update Failed",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            catch (HttpRequestException ex)
            {
                MessageBox.Show(
                    "Unable to connect to the API.\n\n" +
                    ex.Message,
                    "Connection Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "An error occurred while adding stock.\n\n" +
                    ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            finally
            {
                btnAddStock.Enabled = true;
                cmbProduct.Enabled = true;
            }
        }

        private void btnCancel_Click(
            object sender,
            EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }

    public class StockProduct
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

    public class InventoryStockRequest
    {
        public decimal QuantityOnHand { get; set; }

        public decimal ReorderLevel { get; set; }
    }
}