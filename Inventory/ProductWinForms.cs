using Guna.UI2.WinForms;
using Larp.domain.Entities;
using Larp.winforms.Sales;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Larp.winforms.Inventory
{
    public partial class ProductWinForms : Form
    {
        private readonly HttpClient _httpClient;
        private readonly int _companyId;

        public ProductWinForms(HttpClient httpClient, int companyId)
        {
            InitializeComponent();

            _httpClient = httpClient;
            _companyId = companyId;

            Load += ProductWinForms_Load;

            StartPosition = FormStartPosition.CenterScreen;
        }

        private async void ProductWinForms_Load(object sender, EventArgs e)
        {
            await LoadProducts();
        }

        private async void SearchProducts_TextChanged(object sender, EventArgs e)
        {
            await LoadProducts(SearchProducts.Text.Trim());
        }

        private async Task LoadProducts(string search = "")
        {
            try
            {
                guna2DataGridView1.DataSource = null;
                guna2DataGridView1.Columns.Clear();

                var products = await _httpClient.GetFromJsonAsync<List<Product>>(
                     $"/tenant/{_companyId}/products");

                 products = products ?? new List<Product>();

                if (!string.IsNullOrWhiteSpace(search))
                {
                    products = products
                        .Where(p =>
                            p.ProductName.Contains(
                                search,
                                StringComparison.OrdinalIgnoreCase)
                            ||
                            p.ProductCode.Contains(
                                search,
                                StringComparison.OrdinalIgnoreCase))
                        .ToList();
                }

                guna2DataGridView1.AutoGenerateColumns = true;
                guna2DataGridView1.DataSource = products;

                ConfigureGrid();
            }
            catch (HttpRequestException ex)
            {
                MessageBox.Show(
                    $"Unable to connect to the API.\n\n{ex.Message}",
                    "Connection Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Unable to load products.\n\n{ex.Message}",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void ConfigureGrid()
        {
            if (guna2DataGridView1.Columns.Count == 0)
            {
                return;
            }

            var productCodeColumn =
                guna2DataGridView1.Columns["ProductCode"];

            if (productCodeColumn != null)
            {
                productCodeColumn.HeaderText = "Product Code";
            }

            var productNameColumn =
                guna2DataGridView1.Columns["ProductName"];

            if (productNameColumn != null)
            {
                productNameColumn.HeaderText = "Product Name";
            }

            var unitPriceColumn =
                guna2DataGridView1.Columns["UnitPrice"];

            if (unitPriceColumn != null)
            {
                unitPriceColumn.HeaderText = "Unit Price";
                unitPriceColumn.DefaultCellStyle.Format = "₱#,##0.00";
            }

            var isActiveColumn =
                guna2DataGridView1.Columns["IsActive"];

            if (isActiveColumn != null)
            {
                isActiveColumn.HeaderText = "Status";
            }

            var createdAtColumn =
                guna2DataGridView1.Columns["CreatedAt"];

            if (createdAtColumn != null)
            {
                createdAtColumn.HeaderText = "Created At";
                createdAtColumn.DefaultCellStyle.Format = "MMM dd, yyyy";
            }


            guna2DataGridView1.AutoSizeColumnsMode =
                DataGridViewAutoSizeColumnsMode.Fill;

            guna2DataGridView1.SelectionMode =
                DataGridViewSelectionMode.FullRowSelect;

            guna2DataGridView1.MultiSelect = false;

            guna2DataGridView1.ReadOnly = true;

            guna2DataGridView1.AllowUserToAddRows = false;

            guna2DataGridView1.AllowUserToDeleteRows = false;
        }

        private void GotoSales_Click(object sender, EventArgs e)
        {
            var form = new SalesWinForms(
                _httpClient,
                _companyId);

            form.Show();

            this.Close();
        }

        private void GotoInventory_Click(object sender, EventArgs e)
        {
            var form = new InventoryWinForms(
                _httpClient,
                _companyId);

            form.Show();

            this.Close();
        }

        private void AddProducts_Click(object sender, EventArgs e)
        {
            using var form = new AddProductWinForms( _httpClient,_companyId);

            if (form.ShowDialog() == DialogResult.OK)
            {
                _ = LoadProducts(SearchProducts.Text.Trim());
            }
        }
    }
}