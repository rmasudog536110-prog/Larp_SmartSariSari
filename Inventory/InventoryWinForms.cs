using Larp.winforms.Sales;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Larp.winforms.Inventory
{
    public partial class InventoryWinForms : Form
    {
        private readonly HttpClient _httpClient;
        private readonly int _companyId;

        public InventoryWinForms(HttpClient httpClient, int companyId)
        {
            InitializeComponent();

            _httpClient = httpClient;
            _companyId = companyId;

            WindowState = FormWindowState.Maximized;
            FormBorderStyle = FormBorderStyle.None;

            Load += InventoryWinForms_Load;
        }

        private async void InventoryWinForms_Load(object sender, EventArgs e)
        {
            SetupInventoryTable();
            await LoadInventory();
        }

        private async Task LoadInventory()
        {
            try
            {
                var inventory =
                    await _httpClient.GetFromJsonAsync<List<InventoryProduct>>
                    ($"/tenant/{_companyId}/inventory");

                if (inventory == null)
                {
                    MessageBox.Show(
                        "The API returned no inventory data.",
                        "Inventory",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    return;
                }

                UpdateCards(inventory);
                DisplayInventory(inventory);
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
                    "Unable to load inventory.\n\n" +
                    ex.Message,
                    "Inventory Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void UpdateCards(List<InventoryProduct> inventory)
        {
            int totalProducts = inventory.Count;

            int lowStock = inventory.Count(x =>
                x.QuantityOnHand > 0 &&
                x.QuantityOnHand <= x.ReorderLevel);

            int outOfStock = inventory.Count(x =>
                x.QuantityOnHand <= 0);

            TotalProductLabel.Text = totalProducts.ToString();
            LowStockLabel.Text = lowStock.ToString();
            OutOfStockLabel.Text = outOfStock.ToString();
        }

        private void SetupInventoryTable()
        {
            guna2DataGridView1.AutoGenerateColumns = false;
            guna2DataGridView1.Columns.Clear();
            guna2DataGridView1.Rows.Clear();

            var productColumn = new DataGridViewTextBoxColumn
            {
                Name = "ProductColumn",
                HeaderText = "Product Name",
                DataPropertyName = "ProductName",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            };

            var stockColumn = new DataGridViewTextBoxColumn
            {
                Name = "StockColumn",
                HeaderText = "Stock",
                DataPropertyName = "QuantityOnHand",
                Width = 180
            };

            var reorderColumn = new DataGridViewTextBoxColumn
            {
                Name = "ReorderColumn",
                HeaderText = "ReOrder",
                DataPropertyName = "ReorderStatus",
                Width = 180
            };

            var statusColumn = new DataGridViewTextBoxColumn
            {
                Name = "StatusColumn",
                HeaderText = "Status",
                DataPropertyName = "Status",
                Width = 180
            };

            var actionColumn = new DataGridViewButtonColumn
            {
                Name = "StockAction",
                HeaderText = "Actions",
                Text = "Edit / Delete",
                UseColumnTextForButtonValue = true,
                Width = 180
            };

            guna2DataGridView1.Columns.Add(productColumn);
            guna2DataGridView1.Columns.Add(stockColumn);
            guna2DataGridView1.Columns.Add(reorderColumn);
            guna2DataGridView1.Columns.Add(statusColumn);
            guna2DataGridView1.Columns.Add(actionColumn);

            guna2DataGridView1.SelectionMode =
                DataGridViewSelectionMode.FullRowSelect;

            guna2DataGridView1.MultiSelect = false;
            guna2DataGridView1.ReadOnly = true;
            guna2DataGridView1.AllowUserToAddRows = false;
            guna2DataGridView1.AllowUserToDeleteRows = false;
        }

        private void DisplayInventory(List<InventoryProduct> inventory)
        {
            var rows = inventory.Select(x => new InventoryRow
            {
                ProductId = x.ProductId,
                ProductName = x.ProductName,
                QuantityOnHand = x.QuantityOnHand,

                ReorderStatus =
                    x.QuantityOnHand <= x.ReorderLevel
                        ? "Yes"
                        : "No",

                Status =
                    x.IsActive
                        ? "Active"
                        : "Inactive"
            }).ToList();

            guna2DataGridView1.DataSource = rows;
        }

        private void guna2DataGridView1_CellContentClick(
            object sender,
            DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            if (e.ColumnIndex < 0)
                return;

            if (guna2DataGridView1.Columns[e.ColumnIndex].Name !=
                "StockAction")
            {
                return;
            }

            if (guna2DataGridView1.Rows[e.RowIndex].DataBoundItem
                is not InventoryRow row)
            {
                return;
            }

            string productName = row.ProductName;

            var result = MessageBox.Show(
                $"What would you like to do with {productName}?\n\n" +
                "Yes = Edit\n" +
                "No = Delete\n" +
                "Cancel = Close",
                "Inventory Action",
                MessageBoxButtons.YesNoCancel,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                MessageBox.Show(
                    $"Edit {productName}.",
                    "Edit Product",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            else if (result == DialogResult.No)
            {
                MessageBox.Show(
                    $"Delete {productName}.",
                    "Delete Product",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
        }

        private void GotoProducts_Click(object sender, EventArgs e)
        {
            var form = new ProductWinForms(
                _httpClient,
                _companyId);

            form.Show();
            Close();
        }

        private void GotoSales_Click(object sender, EventArgs e)
        {
            var form = new SalesWinForms(
                _httpClient,
                _companyId);

            form.Show();
            Close();
        }

        private async void AddStocks_Click(object sender, EventArgs e)
        {
            using var form = new AddStockWinForms(
                _httpClient,
                _companyId);

            if (form.ShowDialog() == DialogResult.OK)
            {
                await LoadInventory();
            }
        }

    }

    public class InventoryProduct
    {
        public int ProductId { get; set; }

        public string ProductName { get; set; } = string.Empty;

        public decimal QuantityOnHand { get; set; }

        public decimal ReorderLevel { get; set; }

        public bool IsActive { get; set; }
    }

    public class InventoryRow
    {
        public int ProductId { get; set; }

        public string ProductName { get; set; } = string.Empty;

        public decimal QuantityOnHand { get; set; }

        public string ReorderStatus { get; set; } = string.Empty;

        public string Status { get; set; } = string.Empty;
    }
}