using Guna.UI2.WinForms;
using Larp.winforms;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Windows.Forms;
using SupplierEntity = Larp.domain.Entities.Supplier;

namespace Larp.winforms.SmallTenant.Supplier
{
    public partial class SupplierWinforms : Form
    {
        private readonly HttpClient _httpClient;
        private readonly int _companyId;

        private List<SupplierEntity> _suppliers = new();

        public SupplierWinforms(HttpClient httpClient, int companyId)
        {
            InitializeComponent();

            _httpClient = httpClient;
            _companyId = companyId;

            AppTheme.Apply(this);

            ConfigureGrid();

            Shown += async (_, _) =>
            {
                await LoadSuppliersAsync();
            };
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

            guna2DataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "SupplierId",
                HeaderText = "ID",
                DataPropertyName = "SupplierId",
                Width = 60
            });

            guna2DataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "SupplierCode",
                HeaderText = "Supplier Code",
                DataPropertyName = "SupplierCode",
                Width = 130
            });

            guna2DataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "SupplierName",
                HeaderText = "Supplier Name",
                DataPropertyName = "SupplierName",
                Width = 180
            });

            guna2DataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "ContactPerson",
                HeaderText = "Contact Person",
                DataPropertyName = "ContactPerson",
                Width = 150
            });

            guna2DataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "ContactNumber",
                HeaderText = "Contact Number",
                DataPropertyName = "ContactNumber",
                Width = 130
            });

            guna2DataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "EmailAddress",
                HeaderText = "Email",
                DataPropertyName = "EmailAddress",
                Width = 180
            });

            guna2DataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Address",
                HeaderText = "Address",
                DataPropertyName = "Address",
                Width = 200
            });

            guna2DataGridView1.Columns.Add(new DataGridViewCheckBoxColumn
            {
                Name = "IsActive",
                HeaderText = "Active",
                DataPropertyName = "IsActive",
                Width = 70
            });

            var editColumn = new DataGridViewButtonColumn
            {
                Name = "Edit",
                HeaderText = "",
                Text = "Edit",
                UseColumnTextForButtonValue = true,
                Width = 75
            };

            guna2DataGridView1.Columns.Add(editColumn);

            var deleteColumn = new DataGridViewButtonColumn
            {
                Name = "Delete",
                HeaderText = "",
                Text = "Deactivate",
                UseColumnTextForButtonValue = true,
                Width = 95
            };

            guna2DataGridView1.Columns.Add(deleteColumn);
        }

        private async Task LoadSuppliersAsync(string search = "")
        {
            try
            {
                guna2DataGridView1.Enabled = false;
                guna2ProgressIndicator1.Visible = true;

                var url = $"tenant/{_companyId}/suppliers";

                var response =
                    await _httpClient.GetAsync(url);

                if (!response.IsSuccessStatusCode)
                {
                    MessageBox.Show(
                        $"Failed to load suppliers.\n\nStatus: {response.StatusCode}",
                        "Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);

                    return;
                }

                var suppliers =
                    await response.Content.ReadFromJsonAsync<List<SupplierEntity>>();

                _suppliers = suppliers ?? new List<SupplierEntity>();

                if (!string.IsNullOrWhiteSpace(search))
                {
                    search = search.Trim();

                    _suppliers = _suppliers.FindAll(x =>
                        x.SupplierCode.Contains(
                            search,
                            StringComparison.OrdinalIgnoreCase) ||
                        x.SupplierName.Contains(
                            search,
                            StringComparison.OrdinalIgnoreCase) ||
                        (x.ContactPerson?.Contains(
                            search,
                            StringComparison.OrdinalIgnoreCase) ?? false) ||
                        (x.ContactNumber?.Contains(
                            search,
                            StringComparison.OrdinalIgnoreCase) ?? false) ||
                        (x.EmailAddress?.Contains(
                            search,
                            StringComparison.OrdinalIgnoreCase) ?? false));
                }

                guna2DataGridView1.DataSource = null;
                guna2DataGridView1.DataSource = _suppliers;

                lblTotalSuppliers.Text = _suppliers.Count.ToString();
                lblActiveSuppliers.Text =
                    _suppliers.FindAll(x => x.IsActive).Count.ToString();
                lblInactiveSuppliers.Text =
                    _suppliers.FindAll(x => !x.IsActive).Count.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Unable to load suppliers.\n\n{ex.Message}",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            finally
            {
                guna2ProgressIndicator1.Visible = false;
                guna2DataGridView1.Enabled = true;
            }
        }

        private async void guna2TextBox1_TextChanged(
            object sender,
            EventArgs e)
        {
            await LoadSuppliersAsync(guna2TextBox1.Text);
        }

        private async void btnAddSupplier_Click(
            object sender,
            EventArgs e)
        {
            using var form =
                new AddSupplierWinforms(
                    _httpClient,
                    _companyId);

            if (form.ShowDialog() == DialogResult.OK)
            {
                await LoadSuppliersAsync(guna2TextBox1.Text);
            }
        }

        private async void guna2DataGridView1_CellContentClick(
            object sender,
            DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            if (e.ColumnIndex < 0)
                return;

            if (e.RowIndex >= _suppliers.Count)
                return;

            var supplier =
                _suppliers[e.RowIndex];

            var columnName =
                guna2DataGridView1.Columns[e.ColumnIndex].Name;

            if (columnName == "Edit")
            {
                using var form =
                    new EditSupplierWinforms(
                        _httpClient,
                        _companyId,
                        supplier);

                if (form.ShowDialog() == DialogResult.OK)
                {
                    await LoadSuppliersAsync(guna2TextBox1.Text);
                }
            }
            else if (columnName == "Delete")
            {
                if (!supplier.IsActive)
                {
                    MessageBox.Show(
                        "This supplier is already inactive.",
                        "Supplier",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    return;
                }

                var result = MessageBox.Show(
                    $"Deactivate supplier '{supplier.SupplierName}'?",
                    "Deactivate Supplier",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (result != DialogResult.Yes)
                    return;

                try
                {
                    var response =
                        await _httpClient.DeleteAsync(
                            $"tenant/{_companyId}/suppliers/{supplier.SupplierId}");

                    if (!response.IsSuccessStatusCode)
                    {
                        MessageBox.Show(
                            $"Failed to deactivate supplier.\n\nStatus: {response.StatusCode}",
                            "Error",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);

                        return;
                    }

                    await LoadSuppliersAsync(guna2TextBox1.Text);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(
                        $"Unable to deactivate supplier.\n\n{ex.Message}",
                        "Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
        }

        private async void btnRefresh_Click(
            object sender,
            EventArgs e)
        {
            guna2TextBox1.Clear();
            await LoadSuppliersAsync();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
