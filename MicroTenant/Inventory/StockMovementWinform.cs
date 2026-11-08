using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Larp.winforms.SmallTenant.Supplier
{
    public partial class StockMovementWinforms : Form
    {
        private readonly HttpClient _httpClient;
        private readonly int _companyId;
        private readonly int _productId;
        private readonly string _productName;

        public StockMovementWinforms(
            HttpClient httpClient,
            int companyId,
            int productId,
            string productName)
        {
            InitializeComponent();

            _httpClient = httpClient;
            _companyId = companyId;
            _productId = productId;
            _productName = productName;

            ProductNameLabel.Text = _productName;

            ConfigureGrid();

            Shown += async (_, _) =>
            {
                await LoadMovementsAsync();
            };
        }

        private void ConfigureGrid()
        {
            StockMovementGrid.AutoGenerateColumns = false;
            StockMovementGrid.AllowUserToAddRows = false;
            StockMovementGrid.AllowUserToDeleteRows = false;
            StockMovementGrid.ReadOnly = true;
            StockMovementGrid.SelectionMode =
                DataGridViewSelectionMode.FullRowSelect;
            StockMovementGrid.MultiSelect = false;

            StockMovementGrid.Columns.Clear();

            StockMovementGrid.Columns.Add(
                new DataGridViewTextBoxColumn
                {
                    Name = "MovementDate",
                    HeaderText = "Date",
                    DataPropertyName = "MovementDate",
                    Width = 150
                });

            StockMovementGrid.Columns.Add(
                new DataGridViewTextBoxColumn
                {
                    Name = "MovementType",
                    HeaderText = "Movement",
                    DataPropertyName = "MovementType",
                    Width = 120
                });

            StockMovementGrid.Columns.Add(
                new DataGridViewTextBoxColumn
                {
                    Name = "Quantity",
                    HeaderText = "Quantity",
                    DataPropertyName = "Quantity",
                    Width = 100
                });

            StockMovementGrid.Columns.Add(
                new DataGridViewTextBoxColumn
                {
                    Name = "QuantityBefore",
                    HeaderText = "Before",
                    DataPropertyName = "QuantityBefore",
                    Width = 100
                });

            StockMovementGrid.Columns.Add(
                new DataGridViewTextBoxColumn
                {
                    Name = "QuantityAfter",
                    HeaderText = "After",
                    DataPropertyName = "QuantityAfter",
                    Width = 100
                });

            StockMovementGrid.Columns.Add(
                new DataGridViewTextBoxColumn
                {
                    Name = "EmployeeName",
                    HeaderText = "Transacted by",
                    DataPropertyName = "EmployeeName",
                    Width = 180
                });

            StockMovementGrid.Columns.Add(
                new DataGridViewTextBoxColumn
                {
                    Name = "Remarks",
                    HeaderText = "Remarks",
                    DataPropertyName = "Remarks",
                    AutoSizeMode =
                        DataGridViewAutoSizeColumnMode.Fill
                });
        }

        private async Task LoadMovementsAsync()
        {
            try
            {
                guna2ProgressIndicator1.Visible = true;
                StockMovementGrid.Enabled = false;

                var response =
                    await _httpClient.GetAsync(
                        $"/tenant/{_companyId}/stock-movements/product/{_productId}");

                if (!response.IsSuccessStatusCode)
                {
                    var error =
                        await response.Content.ReadAsStringAsync();

                    MessageBox.Show(
                        $"Unable to load stock history.\n\nStatus: {response.StatusCode}\n{error}",
                        "Stock History",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);

                    return;
                }

                var movements =
                    await response.Content
                        .ReadFromJsonAsync<List<StockMovementDto>>();

                var rows = new List<StockMovementRow>();

                if (movements != null)
                {
                    foreach (var movement in movements)
                    {
                        var employeeName =
                            movement.Employee != null
                                ? $"{movement.Employee.FirstName} {movement.Employee.LastName}"
                                : $"Employee #{movement.EmployeeId}";

                        rows.Add(new StockMovementRow
                        {
                            MovementDate =
                                movement.MovementDate
                                    .ToLocalTime()
                                    .ToString("MMM dd, yyyy hh:mm tt"),

                            MovementType = FriendlyType(movement.MovementType),

                            Quantity =
                                movement.Quantity,

                            QuantityBefore =
                                movement.QuantityBefore,

                            QuantityAfter =
                                movement.QuantityAfter,

                            EmployeeName =
                                employeeName,

                            Remarks =
                                string.IsNullOrWhiteSpace(
                                    movement.Remarks)
                                    ? "-"
                                    : movement.Remarks
                        });
                    }
                }

                StockMovementGrid.DataSource = null;
                StockMovementGrid.DataSource = rows;
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
                    $"Unable to load stock history.\n\n{ex.Message}",
                    "Stock History",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            finally
            {
                StockMovementGrid.Enabled = true;
                guna2ProgressIndicator1.Visible = false;
            }
        }

        private static string FriendlyType(string type) =>
            type switch
            {
                "Purchase" => "Purchase (stock in)",
                "PurchaseCancel" => "Purchase cancelled",
                "Sale" => "Sale (stock out)",
                "SaleCancel" => "Sale cancelled",
                _ => type
            };

        private async void RefreshButton_Click(
            object sender,
            EventArgs e)
        {
            await LoadMovementsAsync();
        }

        private void CloseButton_Click(
            object sender,
            EventArgs e)
        {
            Close();
        }
    }

    public class StockMovementDto
    {
        public int StockMovementId { get; set; }

        public int ProductId { get; set; }

        public string MovementType { get; set; } =
            string.Empty;

        public decimal Quantity { get; set; }

        public decimal QuantityBefore { get; set; }

        public decimal QuantityAfter { get; set; }

        public int? PurchaseId { get; set; }

        public int EmployeeId { get; set; }

        public DateTime MovementDate { get; set; }

        public string? Remarks { get; set; }

        public EmployeeDto? Employee { get; set; }
    }

    public class EmployeeDto
    {
        public int EmployeeId { get; set; }

        public string FirstName { get; set; } =
            string.Empty;

        public string LastName { get; set; } =
            string.Empty;
    }

    public class StockMovementRow
    {
        public string MovementDate { get; set; } =
            string.Empty;

        public string MovementType { get; set; } =
            string.Empty;

        public decimal Quantity { get; set; }

        public decimal QuantityBefore { get; set; }

        public decimal QuantityAfter { get; set; }

        public string EmployeeName { get; set; } =
            string.Empty;

        public string Remarks { get; set; } =
            string.Empty;
    }
}