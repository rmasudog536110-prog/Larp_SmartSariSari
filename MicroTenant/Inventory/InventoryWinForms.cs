using Guna.UI2.WinForms;
using Larp.winforms.MicroTenant.Products;
using Larp.winforms.MicroTenant.Sales;
using Larp.winforms.SmallTenant.Supplier;
using Larp.winforms;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Larp.winforms.MicroTenant.Inventory
{
    public partial class InventoryWinForms : Form
    {
        private readonly HttpClient _httpClient;
        private readonly int _companyId;

        private List<InventoryProduct> _inventory = new();

        public InventoryWinForms(
            HttpClient httpClient,
            int companyId)
        {
            InitializeComponent();

            _httpClient = httpClient;
            _companyId = companyId;

            AppTheme.Apply(this);

            WindowState = FormWindowState.Maximized;
            FormBorderStyle = FormBorderStyle.None;

            ConfigureGrid();
            ApplyRoleAccess();

            Shown += async (_, _) =>
            {
                await LoadInventoryAsync();
            };
        }

        private void ApplyRoleAccess()
        {
            if (Session.Role != "Staff") return;

            GotoProducts.Visible = false;
            guna2DataGridView1.Columns["ViewMovements"].Visible = false;
        }

        private void GotoProducts_Click(
            object sender,
            EventArgs e)
        {
            FormNavigator.GoTo(this, () =>
                new ProductWinForms(
                    _httpClient,
                    _companyId));
        }

        private void GotoSales_Click(
            object sender,
            EventArgs e)
        {
            FormNavigator.GoTo(this, () =>
                new SalesWinForms(
                    _httpClient,
                    _companyId));
        }

        private void ConfigureGrid()
        {
            guna2DataGridView1.AutoGenerateColumns = false;
            guna2DataGridView1.AllowUserToAddRows = false;
            guna2DataGridView1.AllowUserToDeleteRows = false;
            guna2DataGridView1.ReadOnly = true;
            guna2DataGridView1.SelectionMode =
                DataGridViewSelectionMode.FullRowSelect;
            guna2DataGridView1.MultiSelect = false;

            guna2DataGridView1.Columns.Clear();

            guna2DataGridView1.Columns.Add(
                new DataGridViewTextBoxColumn
                {
                    Name = "ProductName",
                    HeaderText = "Product",
                    DataPropertyName = "ProductName",
                    AutoSizeMode =
                        DataGridViewAutoSizeColumnMode.Fill
                });

            guna2DataGridView1.Columns.Add(
                new DataGridViewTextBoxColumn
                {
                    Name = "QuantityOnHand",
                    HeaderText = "Current Stock",
                    DataPropertyName = "QuantityOnHand",
                    Width = 150
                });

            guna2DataGridView1.Columns.Add(
                new DataGridViewTextBoxColumn
                {
                    Name = "ReorderLevel",
                    HeaderText = "Reorder Level",
                    DataPropertyName = "ReorderLevel",
                    Width = 150
                });

            guna2DataGridView1.Columns.Add(
                new DataGridViewTextBoxColumn
                {
                    Name = "StockStatus",
                    HeaderText = "Stock Status",
                    DataPropertyName = "StockStatus",
                    Width = 150
                });

            guna2DataGridView1.Columns.Add(
                new DataGridViewButtonColumn
                {
                    Name = "ViewMovements",
                    HeaderText = "Actions",
                    Text = "History",
                    UseColumnTextForButtonValue = true,
                    Width = 100
                });

            guna2DataGridView1.CellContentClick -=
                guna2DataGridView1_CellContentClick;

            guna2DataGridView1.CellContentClick +=
                guna2DataGridView1_CellContentClick;
        }

        private async Task LoadInventoryAsync()
        {
            try
            {
                guna2DataGridView1.Enabled = false;
                guna2ProgressIndicator1.Visible = true;

                var response =
                    await _httpClient.GetAsync(
                        $"/tenant/{_companyId}/inventory");

                if (!response.IsSuccessStatusCode)
                {
                    string error =
                        await response.Content.ReadAsStringAsync();

                    MessageBox.Show(
                        $"Failed to load inventory.\n\n{error}",
                        "Inventory",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);

                    return;
                }

                var inventory =
                    await response.Content
                        .ReadFromJsonAsync<List<InventoryProduct>>();

                _inventory =
                    inventory ?? new List<InventoryProduct>();

                UpdateCards();
                DisplayInventory();
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
                    $"Unable to load inventory.\n\n{ex.Message}",
                    "Inventory Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            finally
            {
                guna2DataGridView1.Enabled = true;
                guna2ProgressIndicator1.Visible = false;
            }
        }

        private void UpdateCards()
        {
            var activeInventory =
                _inventory
                    .Where(x => x.IsActive)
                    .ToList();

            int totalProducts =
                activeInventory.Count;

            int lowStock =
                activeInventory.Count(x =>
                    x.QuantityOnHand > 0 &&
                    x.QuantityOnHand <= x.ReorderLevel);

            int outOfStock =
                activeInventory.Count(x =>
                    x.QuantityOnHand <= 0);

            TotalProductLabel.Text =
                totalProducts.ToString();

            LowStockLabel.Text =
                lowStock.ToString();

            OutOfStockLabel.Text =
                outOfStock.ToString();
        }

        private void DisplayInventory()
        {
            var rows =
                _inventory
                    .Where(x => x.IsActive)
                    .Select(x => new InventoryRow
                    {
                        ProductId = x.ProductId,
                        ProductName = x.ProductName,
                        QuantityOnHand = x.QuantityOnHand,
                        ReorderLevel = x.ReorderLevel,
                        StockStatus = GetStockStatus(x)
                    })
                    .ToList();

            guna2DataGridView1.DataSource = null;
            guna2DataGridView1.DataSource = rows;
        }

        private string GetStockStatus(
            InventoryProduct product)
        {
            if (product.QuantityOnHand <= 0)
                return "Out of Stock";

            if (product.QuantityOnHand <=
                product.ReorderLevel)
                return "Low Stock";

            return "In Stock";
        }

        private void guna2DataGridView1_CellContentClick(
           object sender,
           DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            if (e.ColumnIndex < 0)
                return;

            if (guna2DataGridView1.Rows[e.RowIndex].DataBoundItem
                is not InventoryRow row)
            {
                return;
            }

            string columnName =
                guna2DataGridView1.Columns[e.ColumnIndex].Name;

            if (columnName == "ViewMovements")
            {
                ViewStockHistory(row);
            }
        }

        private void ViewStockHistory(InventoryRow row)
        {
            try
            {
                using var form =
                    new StockMovementWinforms(
                        _httpClient,
                        _companyId,
                        row.ProductId,
                        row.ProductName);

                form.ShowDialog(this);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Unable to open stock history.\n\n{ex.Message}",
                    "Stock History",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private async void btnRefresh_Click(
            object sender,
            EventArgs e)
        {
            await LoadInventoryAsync();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }

    public class InventoryProduct
    {
        public int ProductId { get; set; }

        public string ProductName { get; set; } =
            string.Empty;

        public decimal QuantityOnHand { get; set; }

        public decimal ReorderLevel { get; set; }

        public bool IsActive { get; set; }
    }

    public class InventoryRow
    {
        public int ProductId { get; set; }

        public string ProductName { get; set; } =
            string.Empty;

        public decimal QuantityOnHand { get; set; }

        public decimal ReorderLevel { get; set; }

        public string StockStatus { get; set; } =
            string.Empty;
    }
}