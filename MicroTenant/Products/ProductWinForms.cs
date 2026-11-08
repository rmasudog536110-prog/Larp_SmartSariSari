using Guna.UI2.WinForms;
using Larp.domain.Entities;
using Larp.winforms.MicroTenant.Inventory;
using Larp.winforms.MicroTenant.Products;
using Larp.winforms.MicroTenant.Reports;
using Larp.winforms.MicroTenant.Sales;
using Larp.winforms;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Larp.winforms.MicroTenant.Products
{
    public partial class ProductWinForms : Form
    {
        private readonly HttpClient _httpClient;
        private readonly int _companyId;

        public ProductWinForms(
            HttpClient httpClient,
            int companyId)
        {
            InitializeComponent();

            _httpClient = httpClient;
            _companyId = companyId;

            AppTheme.Apply(this);

            StartPosition = FormStartPosition.CenterScreen;

            Load += ProductWinForms_Load;
        }

        private async void ProductWinForms_Load(
            object sender,
            EventArgs e)
        {
            SetupProductTable();
            await LoadProducts();
        }

        private async void SearchProducts_TextChanged(
            object sender,
            EventArgs e)
        {
            await LoadProducts(SearchProducts.Text.Trim());
        }

        private async Task LoadProducts(string search = "")
        {
            try
            {
                var products =
                    await _httpClient.GetFromJsonAsync<List<Product>>(
                        $"/tenant/{_companyId}/products");

                products ??= new List<Product>();

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

                DisplayProducts(products);
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
                    "Unable to load products.\n\n" +
                    ex.Message,
                    "Product Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void SetupProductTable()
        {
            guna2DataGridView1.DataSource = null;
            guna2DataGridView1.Columns.Clear();
            guna2DataGridView1.AutoGenerateColumns = false;

            var productCodeColumn =
                new DataGridViewTextBoxColumn
                {
                    Name = "ProductCodeColumn",
                    HeaderText = "Product Code",
                    DataPropertyName = "ProductCode",
                    Width = 150
                };

            var productNameColumn =
                new DataGridViewTextBoxColumn
                {
                    Name = "ProductNameColumn",
                    HeaderText = "Product Name",
                    DataPropertyName = "ProductName",
                    AutoSizeMode =
                        DataGridViewAutoSizeColumnMode.Fill
                };

            var unitPriceColumn =
                new DataGridViewTextBoxColumn
                {
                    Name = "UnitPriceColumn",
                    HeaderText = "Unit Price",
                    DataPropertyName = "UnitPrice",
                    Width = 150
                };

            var vatColumn = new DataGridViewTextBoxColumn
            {
                Name = "VatColumn",
                HeaderText = "VAT",
                DataPropertyName = "VatLabel",
                Width = 90
            };
            guna2DataGridView1.Columns.Add(vatColumn);


            unitPriceColumn.DefaultCellStyle.Format =
                "₱#,##0.00";

            var statusColumn =
                new DataGridViewTextBoxColumn
                {
                    Name = "StatusColumn",
                    HeaderText = "Status",
                    DataPropertyName = "Status",
                    Width = 150
                };

            var createdAtColumn =
                new DataGridViewTextBoxColumn
                {
                    Name = "CreatedAtColumn",
                    HeaderText = "Created At",
                    DataPropertyName = "CreatedAt",
                    Width = 180
                };

            createdAtColumn.DefaultCellStyle.Format =
                "MMM dd, yyyy";

            var editColumn =
                new DataGridViewButtonColumn
                {
                    Name = "EditAction",
                    HeaderText = "Edit",
                    Text = "Edit",
                    UseColumnTextForButtonValue = true,
                    Width = 100
                };

            var deleteColumn =
                new DataGridViewButtonColumn
                {
                    Name = "DeleteAction",
                    HeaderText = "Delete",
                    Text = "Delete",
                    UseColumnTextForButtonValue = true,
                    Width = 100
                };

            guna2DataGridView1.Columns.Add(
                productCodeColumn);

            guna2DataGridView1.Columns.Add(
                productNameColumn);

            guna2DataGridView1.Columns.Add(
                unitPriceColumn);

            guna2DataGridView1.Columns.Add(
                statusColumn);

            guna2DataGridView1.Columns.Add(
                createdAtColumn);

            guna2DataGridView1.Columns.Add(
                editColumn);

            guna2DataGridView1.Columns.Add(
                deleteColumn);

            guna2DataGridView1.SelectionMode =
                DataGridViewSelectionMode.FullRowSelect;

            guna2DataGridView1.MultiSelect = false;

            guna2DataGridView1.ReadOnly = true;

            guna2DataGridView1.AllowUserToAddRows = false;

            guna2DataGridView1.AllowUserToDeleteRows = false;

            guna2DataGridView1.AutoSizeColumnsMode =
                DataGridViewAutoSizeColumnsMode.Fill;

            guna2DataGridView1.CellContentClick -=
                guna2DataGridView1_CellContentClick;

            guna2DataGridView1.CellContentClick +=
                guna2DataGridView1_CellContentClick;
        }

        private void DisplayProducts(List<Product> products)
        {
            var rows = products
                .Select(product => new ProductRow
                {
                    ProductId = product.ProductId,
                    ProductCode = product.ProductCode,
                    ProductName = product.ProductName,
                    UnitPrice = product.UnitPrice,
                    IsActive = product.IsActive,
                    IsVatable = product.IsVatable,
                    CreatedAt = product.CreatedAt
                })
                .ToList();

            guna2DataGridView1.DataSource = rows;
        }

        private async void guna2DataGridView1_CellContentClick(
            object sender,
            DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            if (e.ColumnIndex < 0)
                return;

            if (guna2DataGridView1.Rows[e.RowIndex]
                .DataBoundItem is not ProductRow row)
            {
                return;
            }

            string columnName =
                guna2DataGridView1.Columns[e.ColumnIndex].Name;

            if (columnName == "EditAction")
            {
                await EditProduct(row);
            }
            else if (columnName == "DeleteAction")
            {
                await DeactivateProduct(row);
            }
        }

        private async Task EditProduct(ProductRow row)
        {
            using var form =
                new EditProductWinForms(
                    row.ProductName,
                    row.UnitPrice,
                    row.IsActive,
                    row.IsVatable);

            if (form.ShowDialog() != DialogResult.OK)
                return;

            try
            {
                var updatedProduct = new Product
                {
                    ProductId = row.ProductId,
                    ProductCode = row.ProductCode,
                    ProductName = form.ProductNameValue,
                    UnitPrice = form.UnitPriceValue,
                    IsActive = form.IsActiveValue,
                    IsVatable = form.IsVatableValue,
                    CreatedAt = row.CreatedAt
                };

                var response =
                    await _httpClient.PutAsJsonAsync(
                        $"/tenant/{_companyId}/products/{row.ProductId}",
                        updatedProduct);

                if (!response.IsSuccessStatusCode)
                {
                    var message =
                        await response.Content.ReadAsStringAsync();

                    MessageBox.Show(
                        "Unable to update product.\n\n" +
                        message,
                        "Update Product",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);

                    return;
                }

                MessageBox.Show(
                    "Product updated successfully.",
                    "Update Product",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                await LoadProducts(
                    SearchProducts.Text.Trim());
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Unable to update product.\n\n" +
                    ex.Message,
                    "Update Product",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private async Task DeactivateProduct(
            ProductRow row)
        {
            if (row.Status == "Inactive")
            {
                MessageBox.Show(
                    "This product is already inactive.",
                    "Product Status",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                return;
            }

            var result = MessageBox.Show(
                $"Are you sure you want to deactivate " +
                $"'{row.ProductName}'?\n\n" +
                "The product will remain in the Products table " +
                "but its status will become Inactive.",
                "Deactivate Product",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (result != DialogResult.Yes)
                return;

            try
            {
                var request = new Product
                {
                    ProductId = row.ProductId,
                    ProductCode = row.ProductCode,
                    ProductName = row.ProductName,
                    UnitPrice = row.UnitPrice,
                    IsVatable = row.IsVatable,
                    IsActive = false
                };

                var response =
                    await _httpClient.PutAsJsonAsync(
                        $"/tenant/{_companyId}/products/" +
                        $"{row.ProductId}/status",
                        request);

                if (!response.IsSuccessStatusCode)
                {
                    var message =
                        await response.Content.ReadAsStringAsync();

                    MessageBox.Show(
                        "Unable to deactivate product.\n\n" +
                        message,
                        "Deactivate Product",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);

                    return;
                }

                MessageBox.Show(
                    "Product has been set to Inactive.",
                    "Product Status",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                await LoadProducts(
                    SearchProducts.Text.Trim());
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Unable to deactivate product.\n\n" +
                    ex.Message,
                    "Product Status",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
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

        private void GotoInventory_Click(
            object sender,
            EventArgs e)
        {
            FormNavigator.GoTo(this, () =>
                new InventoryWinForms(
                    _httpClient,
                    _companyId));
        }

        private void AddProducts_Click(
            object sender,
            EventArgs e)
        {
            using var form =
                new AddProductWinForms(
                    _httpClient,
                    _companyId);

            if (form.ShowDialog() == DialogResult.OK)
            {
                _ = LoadProducts(
                    SearchProducts.Text.Trim());
            }
        }

        private void GenerateReport_Click(object sender, EventArgs e)
        {
            var reportsForm = new ReportsGenerator(_httpClient, _companyId); reportsForm.ShowDialog(this);
        }
    }

    public class ProductRow
    {
        public int ProductId { get; set; }

        public string ProductCode { get; set; } =
            string.Empty;

        public string ProductName { get; set; } =
            string.Empty;

        public decimal UnitPrice { get; set; }

        public bool IsActive { get; set; }
        
        public bool IsVatable { get; set; }

        public string VatLabel => IsVatable ? "VAT 12%" : "Exempt";

        public string Status =>
            IsActive ? "Active" : "Inactive";

        public DateTime CreatedAt { get; set; }
    }

    public class EditProductWinForms : Form
    {
        private readonly TextBox txtProductName;
        private readonly NumericUpDown txtUnitPrice;
        private readonly ComboBox cmbStatus;
        private readonly Button btnSave;
        private readonly Button btnCancel;

        private readonly CheckBox chkIsVatable;

        public string ProductNameValue =>
            txtProductName.Text.Trim();

        public decimal UnitPriceValue =>
            txtUnitPrice.Value;

        public bool IsActiveValue =>
            cmbStatus.SelectedItem?.ToString() == "Active";


        public bool IsVatableValue => chkIsVatable.Checked;

        public EditProductWinForms(
            string productName,
            decimal unitPrice,
            bool isActive,
            bool isVatable)
        {
            Text = "Edit Product";
            Width = 430;
            Height = 340;

            StartPosition =
                FormStartPosition.CenterParent;

            var nameLabel = new Label
            {
                Text = "Product Name",
                Left = 30,
                Top = 25,
                Width = 120
            };

            txtProductName = new TextBox
            {
                Left = 160,
                Top = 20,
                Width = 210,
                Text = productName
            };

            var priceLabel = new Label
            {
                Text = "Unit Price",
                Left = 30,
                Top = 70,
                Width = 120
            };

            txtUnitPrice = new NumericUpDown
            {
                Left = 160,
                Top = 65,
                Width = 210,
                DecimalPlaces = 2,
                Maximum = 1000000,
                Minimum = 0,
                Value = unitPrice
            };

            var statusLabel = new Label
            {
                Text = "Status",
                Left = 30,
                Top = 115,
                Width = 120
            };

            chkIsVatable = new CheckBox { Text = "VAT applies (12%)", Left = 160, Top = 145, Width = 210, Checked = isVatable };
            Controls.Add(chkIsVatable);

            cmbStatus = new ComboBox
            {
                Left = 160,
                Top = 110,
                Width = 210,
                DropDownStyle =
                    ComboBoxStyle.DropDownList
            };

            cmbStatus.Items.Add("Active");
            cmbStatus.Items.Add("Inactive");

            cmbStatus.SelectedItem =
                isActive ? "Active" : "Inactive";

            btnSave = new Button
            {
                Text = "Save",
                Left = 160,
                Top = 200,
                Width = 100
            };

            btnCancel = new Button
            {
                Text = "Cancel",
                Left = 270,
                Top = 200,
                Width = 100
            };

            btnSave.Click += BtnSave_Click;
            btnCancel.Click += BtnCancel_Click;

            Controls.Add(nameLabel);
            Controls.Add(txtProductName);

            Controls.Add(priceLabel);
            Controls.Add(txtUnitPrice);

            Controls.Add(statusLabel);
            Controls.Add(cmbStatus);

            Controls.Add(btnSave);
            Controls.Add(btnCancel);

            AcceptButton = btnSave;
            CancelButton = btnCancel;
        }



        private void BtnSave_Click(
            object? sender,
            EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(
                txtProductName.Text))
            {
                MessageBox.Show(
                    "Product name is required.",
                    "Edit Product",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                txtProductName.Focus();
                return;
            }

            if (txtUnitPrice.Value <= 0)
            {
                MessageBox.Show(
                    "Unit price must be greater than 0.",
                    "Edit Product",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                txtUnitPrice.Focus();
                return;
            }

            DialogResult = DialogResult.OK;
            Close();
        }

        private void BtnCancel_Click(
            object? sender,
            EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}