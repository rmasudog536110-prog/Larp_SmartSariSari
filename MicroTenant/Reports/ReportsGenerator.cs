using Larp.domain.Entities;
using QuestPDF.Drawing;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Larp.winforms.MicroTenant.Reports
{
    public partial class ReportsGenerator : Form
    {
        private readonly HttpClient _httpClient;
        private readonly int _companyId;

        private string currentReportTitle = "";
        private List<string> currentHeaders = new();
        private List<List<string>> currentRows = new();

        // Only filled for the Income Statement: "Header", "Line", "Subtotal" or "Total"
        // for each row, so section rows can be shown in bold.
        private List<string> currentRowKinds = new();

        public ReportsGenerator(
            HttpClient httpClient,
            int companyId)
        {
            InitializeComponent();

            _httpClient = httpClient;
            _companyId = companyId;

            AppTheme.Apply(this);

            StartPosition =
                FormStartPosition.CenterScreen;

            dtpStartDate.Value =
                DateTime.Today.AddDays(-30);

            dtpEndDate.Value =
                DateTime.Today;

            cmbReportType.SelectedIndex = 0;

            dgvReport.CellDoubleClick += DgvReport_CellDoubleClick;

            UpdateFilterState();
        }

        // ---------------- Filters ----------------

        private void cmbReportType_SelectedIndexChanged(
            object? sender,
            EventArgs e)
        {
            UpdateFilterState();
            ClearResults();
        }

        // Inventory and Products do not depend on dates.
        private void UpdateFilterState()
        {
            string type =
                cmbReportType.SelectedItem?.ToString() ?? "";

            bool usesDates =
                type == "Sales Report" ||
                type == "Sales History" ||
                type == "Income Statement";

            dtpStartDate.Enabled = usesDates;
            dtpEndDate.Enabled = usesDates;
        }

        private void ClearResults()
        {
            dgvReport.DataSource = null;
            dgvReport.Columns.Clear();

            currentHeaders.Clear();
            currentRows.Clear();
            currentRowKinds.Clear();

            btnCsv.Enabled = false;
            btnPdf.Enabled = false;

            lblSummary.Text =
                "Choose a report and click Generate.";
        }

        private void BtnClose_Click(
            object? sender,
            EventArgs e)
        {
            Close();
        }

        // ---------------- Generate ----------------

        private async void BtnGenerate_Click(
            object? sender,
            EventArgs e)
        {
            await GenerateReport();
        }

        private async void DgvReport_CellDoubleClick(
            object? sender,
            DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            if (cmbReportType.SelectedItem?.ToString() !=
                "Sales History")
            {
                return;
            }

            if (e.RowIndex >= currentRows.Count)
                return;

            string saleNumber =
                currentRows[e.RowIndex][0];

            var sales =
                await _httpClient.GetFromJsonAsync<
                    List<SalesHistoryDto>>(
                        $"/tenant/{_companyId}/sales");

            var selected =
                sales?.FirstOrDefault(x =>
                    x.SaleNumber == saleNumber);

            if (selected == null)
                return;

            using var receipt =
                new Larp.winforms.MicroTenant.Sales.ReceiptForm(
                    _httpClient,
                    _companyId,
                    selected.SalesId);

            receipt.ShowDialog(this);
        }

        private async Task GenerateReport()
        {
            try
            {
                btnGenerate.Enabled = false;
                btnCsv.Enabled = false;
                btnPdf.Enabled = false;
                progress.Visible = true;

                dgvReport.DataSource = null;
                dgvReport.Columns.Clear();

                currentHeaders.Clear();
                currentRows.Clear();
                currentRowKinds.Clear();

                string reportType =
                    cmbReportType.SelectedItem?.ToString()
                    ?? "Sales Report";

                if (reportType == "Sales Report")
                {
                    await GenerateSalesReport();
                }
                else if (reportType == "Sales History")
                {
                    await GenerateSalesHistory();
                }
                else if (reportType == "Inventory Report")
                {
                    await GenerateInventoryReport();
                }
                else if (reportType == "Products Report")
                {
                    await GenerateProductsReport();
                }
                else if (reportType == "Income Statement")
                {
                    await GenerateIncomeStatement();
                }

                btnCsv.Enabled =
                    currentRows.Count > 0;

                btnPdf.Enabled =
                    currentRows.Count > 0;
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
                    "Unable to generate report.\n\n" +
                    ex.Message,
                    "Report Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            finally
            {
                progress.Visible = false;
                btnGenerate.Enabled = true;
            }
        }

        private bool DatesAreValid()
        {
            if (dtpEndDate.Value.Date <
                dtpStartDate.Value.Date)
            {
                MessageBox.Show(
                    "End date cannot be earlier than start date.",
                    "Invalid Date",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return false;
            }

            return true;
        }

        private async Task GenerateSalesReport()
        {
            if (!DatesAreValid())
                return;

            DateTime startDate =
                dtpStartDate.Value.Date;

            DateTime endDate =
                dtpEndDate.Value.Date;

            string url =
                $"/tenant/{_companyId}/reports/sales" +
                $"?startDate={Uri.EscapeDataString(startDate.ToString("o"))}" +
                $"&endDate={Uri.EscapeDataString(endDate.AddDays(1).AddTicks(-1).ToString("o"))}";

            var report =
                await _httpClient
                    .GetFromJsonAsync<SalesReportDto>(
                        url);

            if (report == null)
                return;

            currentReportTitle =
                $"Sales Report ({startDate:MMM dd, yyyy} - {endDate:MMM dd, yyyy})";

            currentHeaders = new List<string>
            {
                "Product",
                "Unit Price",
                "Quantity Sold",
                "Total Amount"
            };

            currentRows =
                report.Items
                    .Select(x => new List<string>
                    {
                        x.ProductName,
                        x.UnitPrice.ToString("₱#,##0.00"),
                        x.QuantitySold.ToString("0.##"),
                        x.TotalAmount.ToString("₱#,##0.00")
                    })
                    .ToList();

            lblSummary.Text =
                $"Transactions: {report.TotalTransactions}    " +
                $"Total Sales: {report.TotalSales:₱#,##0.00}    " +
                $"Items Sold: {report.TotalItemsSold:0.##}";

            DisplayReport();
        }

        private async Task GenerateSalesHistory()
        {
            if (!DatesAreValid())
                return;

            DateTime startDate =
                dtpStartDate.Value.Date;

            DateTime endDate =
                dtpEndDate.Value.Date.AddDays(1);

            var sales =
                await _httpClient.GetFromJsonAsync<
                    List<SalesHistoryDto>>(
                        $"/tenant/{_companyId}/sales");

            sales ??=
                new List<SalesHistoryDto>();

            sales =
                sales
                    .Where(x =>
                        x.SaleDate >= startDate &&
                        x.SaleDate < endDate)
                    .OrderByDescending(x => x.SaleDate)
                    .ToList();

            currentReportTitle =
                $"Sales History ({startDate:MMM dd, yyyy} - " +
                $"{endDate.AddDays(-1):MMM dd, yyyy})";

            currentHeaders = new List<string>
    {
        "Sale No.",
        "Date",
        "Cashier",
        "Payment",
        "Total",
        "Cash Received",
        "Change",
        "Status"
    };

            currentRows =
                sales
                    .Select(x => new List<string>
                    {
                x.SaleNumber,
                x.SaleDate.ToLocalTime()
                    .ToString("MMM dd, yyyy hh:mm tt"),
                x.Cashier,
                x.PaymentMethod,
                x.TotalAmount.ToString("₱#,##0.00"),
                x.CashReceived.ToString("₱#,##0.00"),
                x.ChangeAmount.ToString("₱#,##0.00"),
                x.Status
                    })
                    .ToList();

            lblSummary.Text =
                $"Transactions: {sales.Count}    " +
                $"Total Sales: " +
                $"{sales.Where(x => x.Status != "Cancelled")
                    .Sum(x => x.TotalAmount):₱#,##0.00}";

            DisplayReport();
        }

        private async Task GenerateIncomeStatement()
        {
            if (!DatesAreValid())
                return;

            // Whole days only (yyyy-MM-dd). The API handles the time zone.
            string url =
                $"/tenant/{_companyId}/reports/income-statement" +
                $"?startDate={dtpStartDate.Value:yyyy-MM-dd}" +
                $"&endDate={dtpEndDate.Value:yyyy-MM-dd}";

            var response = await _httpClient.GetAsync(url);

            if (!response.IsSuccessStatusCode)
            {
                MessageBox.Show(
                    await ReadMessageAsync(response),
                    "Income Statement",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            var statement =
                await response.Content
                    .ReadFromJsonAsync<IncomeStatementApi>();

            if (statement == null)
                return;

            currentReportTitle =
                $"Income Statement ({dtpStartDate.Value:MMM dd, yyyy} - " +
                $"{dtpEndDate.Value:MMM dd, yyyy})";

            currentHeaders = new List<string>
            {
                "Description",
                "Amount"
            };

            currentRows = new List<List<string>>();
            currentRowKinds = new List<string>();

            foreach (var line in statement.Lines)
            {
                // Indent normal lines under their section header.
                string label = line.Kind == "Line"
                    ? "    " + line.Label
                    : line.Label;

                string amount = line.Amount.HasValue
                    ? FormatMoney(line.Amount.Value)
                    : "";

                currentRows.Add(new List<string> { label, amount });
                currentRowKinds.Add(line.Kind);
            }

            lblSummary.Text =
                $"Sales: {FormatMoney(statement.TotalSales)}    " +
                $"Purchases: {FormatMoney(statement.TotalPurchases)}    " +
                $"Expenses: {FormatMoney(statement.TotalExpenses)}    " +
                $"Payroll: {FormatMoney(statement.TotalPayroll)}    " +
                $"Net Income: {FormatMoney(statement.NetIncome)}";

            DisplayReport();
        }

        private async Task GenerateInventoryReport()
        {
            var inventory =
                await _httpClient
                    .GetFromJsonAsync<
                        List<InventoryReportItem>>(
                        $"/tenant/{_companyId}/inventory");

            inventory ??=
                new List<InventoryReportItem>();

            currentReportTitle =
                $"Inventory Report ({DateTime.Today:MMM dd, yyyy})";

            currentHeaders = new List<string>
            {
                "Product",
                "Quantity On Hand",
                "Reorder Level",
                "Status"
            };

            currentRows =
                inventory
                    .OrderBy(x => x.ProductName)
                    .Select(x => new List<string>
                    {
                        x.ProductName,
                        x.QuantityOnHand.ToString("0.##"),
                        x.ReorderLevel.ToString("0.##"),
                        x.IsActive
                            ? "Active"
                            : "Inactive"
                    })
                    .ToList();

            lblSummary.Text =
                $"Total Products: {inventory.Count}";

            DisplayReport();
        }

        private async Task GenerateProductsReport()
        {
            var products =
                await _httpClient
                    .GetFromJsonAsync<List<Product>>(
                        $"/tenant/{_companyId}/products");

            products ??=
                new List<Product>();

            currentReportTitle =
                $"Products Report ({DateTime.Today:MMM dd, yyyy})";

            currentHeaders = new List<string>
            {
                "Product Code",
                "Product Name",
                "Unit Price",
                "Status",
                "Created At"
            };

            currentRows =
                products
                    .OrderBy(x => x.ProductCode)
                    .Select(x => new List<string>
                    {
                        x.ProductCode,
                        x.ProductName,
                        x.UnitPrice.ToString("₱#,##0.00"),
                        x.IsActive
                            ? "Active"
                            : "Inactive",
                        x.CreatedAt.ToLocalTime()
                            .ToString("MMM dd, yyyy")
                    })
                    .ToList();

            lblSummary.Text =
                $"Total Products: {products.Count}";

            DisplayReport();
        }

        // Negative amounts show as -₱1,234.00 instead of (₱1,234.00).
        private static string FormatMoney(decimal value)
        {
            return value < 0
                ? "-" + Math.Abs(value).ToString("₱#,##0.00")
                : value.ToString("₱#,##0.00");
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

        // ---------------- Grid ----------------

        private void DisplayReport()
        {
            dgvReport.Columns.Clear();
            dgvReport.Rows.Clear();

            foreach (string header in currentHeaders)
            {
                dgvReport.Columns.Add(
                    header,
                    header);
            }

            foreach (List<string> row in currentRows)
            {
                dgvReport.Rows.Add(
                    row.ToArray());
            }

            // Reports are shown in a fixed order, so sorting is switched off.
            // Number and money columns are right-aligned.
            for (int c = 0; c < dgvReport.Columns.Count; c++)
            {
                dgvReport.Columns[c].SortMode =
                    DataGridViewColumnSortMode.NotSortable;

                if (IsNumericColumn(c))
                {
                    dgvReport.Columns[c].DefaultCellStyle.Alignment =
                        DataGridViewContentAlignment.MiddleRight;

                    dgvReport.Columns[c].HeaderCell.Style.Alignment =
                        DataGridViewContentAlignment.MiddleRight;
                }
            }

            // Income Statement: section, subtotal and total rows in bold.
            if (currentRowKinds.Count == currentRows.Count &&
                currentRowKinds.Count > 0)
            {
                var boldFont = new Font(
                    "Segoe UI",
                    9F,
                    FontStyle.Bold);

                for (int i = 0; i < currentRowKinds.Count; i++)
                {
                    if (currentRowKinds[i] != "Line")
                    {
                        dgvReport.Rows[i].DefaultCellStyle.Font =
                            boldFont;
                    }
                }
            }

            if (currentRows.Count == 0)
            {
                lblSummary.Text += "    No records found.";
            }
        }

        // A column is numeric when every filled cell is a number or an amount.
        // Code columns (such as Product Code) are always treated as text.
        private bool IsNumericColumn(int index)
        {
            if (currentRows.Count == 0 ||
                index >= currentHeaders.Count)
            {
                return false;
            }

            if (currentHeaders[index].Contains("Code"))
            {
                return false;
            }

            bool any = false;

            foreach (var row in currentRows)
            {
                string value =
                    index < row.Count
                        ? row[index].Trim()
                        : "";

                if (value.Length == 0)
                {
                    continue;
                }

                string cleaned =
                    value.Replace("₱", "").Replace(",", "");

                if (!decimal.TryParse(
                        cleaned,
                        NumberStyles.Number | NumberStyles.AllowLeadingSign,
                        CultureInfo.InvariantCulture,
                        out _))
                {
                    return false;
                }

                any = true;
            }

            return any;
        }

        // ---------------- Export to Excel (.csv) ----------------

        private void BtnCsv_Click(
            object? sender,
            EventArgs e)
        {
            if (currentRows.Count == 0)
                return;

            using var dialog =
                new SaveFileDialog
                {
                    Title = "Export to Excel (.csv)",
                    Filter = "CSV file for Excel (*.csv)|*.csv",
                    FileName = SafeFileName(currentReportTitle) + ".csv"
                };

            if (dialog.ShowDialog() !=
                DialogResult.OK)
            {
                return;
            }

            try
            {
                var csv =
                    new StringBuilder();

                csv.AppendLine(
                    CsvLine(currentHeaders));

                foreach (var row in currentRows)
                {
                    csv.AppendLine(
                        CsvLine(row));
                }

                // UTF-8 with a byte-order mark, so Excel shows the peso sign correctly.
                File.WriteAllText(
                    dialog.FileName,
                    csv.ToString(),
                    Encoding.UTF8);

                MessageBox.Show(
                    "The CSV file was saved.",
                    "Export to Excel",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Unable to save the CSV file.\n\n" +
                    ex.Message,
                    "CSV Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private static string CsvLine(
            IEnumerable<string> values)
        {
            return string.Join(
                ",",
                values.Select(value =>
                    "\"" +
                    value.Replace("\"", "\"\"") +
                    "\""));
        }

        private static string SafeFileName(string title)
        {
            string name = title;

            foreach (char invalid in Path.GetInvalidFileNameChars())
            {
                name = name.Replace(invalid, '_');
            }

            return name.Replace(' ', '_');
        }

        // ---------------- Export to PDF ----------------

        private void BtnPdf_Click(
            object? sender,
            EventArgs e)
        {
            if (currentRows.Count == 0)
                return;

            using var dialog =
                new SaveFileDialog
                {
                    Title = "Export to PDF",
                    Filter = "PDF file (*.pdf)|*.pdf",
                    FileName =
                        SafeFileName(currentReportTitle) + ".pdf",
                    DefaultExt = "pdf",
                    AddExtension = true
                };

            if (dialog.ShowDialog() !=
                DialogResult.OK)
            {
                return;
            }

            try
            {
                GeneratePdf(dialog.FileName);

                MessageBox.Show(
                    "The PDF was saved.",
                    "Export to PDF",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Unable to save the PDF.\n\n" +
                    ex.Message,
                    "PDF Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private static string CompanyLabel()
        {
            return string.IsNullOrWhiteSpace(Session.CompanyName)
                ? "LARP Sari-Sari Smart App"
                : Session.CompanyName!;
        }

        private void GeneratePdf(string filePath)
        {
            QuestPDF.Settings.License =
                LicenseType.Community;

            FontManager.RegisterFont(
                File.OpenRead(@"C:\Windows\Fonts\arial.ttf"));

            FontManager.RegisterFont(
                File.OpenRead(@"C:\Windows\Fonts\arialbd.ttf"));

            FontManager.RegisterFont(
                File.OpenRead(@"C:\Windows\Fonts\ariali.ttf"));

            FontManager.RegisterFont(
                File.OpenRead(@"C:\Windows\Fonts\arialbi.ttf"));

            var document =
                Document.Create(documentContainer =>
                {
                    documentContainer.Page(page =>
                    {
                        page.Size(
                            PageSizes.A4.Landscape());

                        page.Margin(36);

                        page.DefaultTextStyle(
                            style => style
                                .FontFamily("Arial")
                                .FontSize(8));

                        page.Header()
                            .Column(column =>
                            {
                                column.Item()
                                    .Text(CompanyLabel())
                                    .FontSize(18)
                                    .Bold()
                                    .FontColor("#1C283F");

                                column.Item()
                                    .PaddingTop(3)
                                    .Text(currentReportTitle)
                                    .FontSize(11)
                                    .FontColor("#000000");

                                column.Item()
                                    .PaddingTop(3)
                                    .Text(
                                        $"Generated {DateTime.Now:MMM dd, yyyy hh:mm tt} by {Session.FullName}")
                                    .FontSize(7)
                                    .FontColor("#606979");

                                column.Item()
                                    .PaddingTop(7)
                                    .LineHorizontal(0.5f);
                            });

                        page.Content()
                            .PaddingTop(10)
                            .Element(BuildPdfContent);

                        page.Footer()
                            .AlignCenter()
                            .Text(
                                $"{CompanyLabel()} - read-only report")
                            .FontSize(7)
                            .FontColor("#606979");
                    });
                });

            document.GeneratePdf(filePath);
        }
        private void BuildPdfContent(
            IContainer container)
        {
            bool hasKinds =
                currentRowKinds.Count == currentRows.Count;

            container.Table(table =>
            {
                table.ColumnsDefinition(columns =>
                {
                    double[] weights =
                        GetPdfColumnWeights();

                    foreach (double weight in weights)
                    {
                        columns.RelativeColumn(
                            (float)weight);
                    }
                });

                table.Header(header =>
                {
                    foreach (string headerText in currentHeaders)
                    {
                        header.Cell()
                            .Background("#1C283F")
                            .Border(0.5f)
                            .BorderColor("#C8CBD2")
                            .PaddingVertical(6)
                            .PaddingHorizontal(5)
                            .AlignMiddle()
                            .Text(headerText)
                            .Bold()
                            .FontSize(8)
                            .FontColor("#FFFFFF");
                    }
                });

                for (int rowIndex = 0;
                     rowIndex < currentRows.Count;
                     rowIndex++)
                {
                    bool strong =
                        hasKinds &&
                        currentRowKinds[rowIndex] != "Line";

                    bool shaded =
                        rowIndex % 2 == 1;

                    for (int columnIndex = 0;
                         columnIndex < currentHeaders.Count;
                         columnIndex++)
                    {
                        string value =
                            columnIndex < currentRows[rowIndex].Count
                                ? currentRows[rowIndex][columnIndex]
                                : "";

                        string background =
                            strong
                                ? "#E8E9EC"
                                : shaded
                                    ? "#F5F6F8"
                                    : "#FFFFFF";

                        var cell =
                            table.Cell()
                                .Background(background)
                                .BorderBottom(0.5f)
                                .BorderColor("#DCDFE4")
                                .PaddingVertical(4)
                                .PaddingHorizontal(5)
                                .AlignMiddle();

                        if (IsNumericColumn(columnIndex))
                        {
                            cell.AlignRight();
                        }
                        else
                        {
                            cell.AlignLeft();
                        }

                        if (strong)
                        {
                            cell.Text(value)
                                .FontSize(8)
                                .FontColor("#1C283F")
                                .Bold();
                        }
                        else
                        {
                            cell.Text(value)
                                .FontSize(8)
                                .FontColor("#000000");
                        }
                    }
                }
            });
        }
        private double[] GetPdfColumnWeights()
        {
            int columnCount = currentHeaders.Count;

            if (columnCount == 0)
                return Array.Empty<double>();

            double[] weights =
                new double[columnCount];

            for (int columnIndex = 0;
                 columnIndex < columnCount;
                 columnIndex++)
            {
                int longest =
                    currentHeaders[columnIndex].Length;

                foreach (var row in currentRows)
                {
                    if (columnIndex >= row.Count)
                        continue;

                    int length =
                        row[columnIndex].Length;

                    if (length > longest)
                        longest = length;
                }

                weights[columnIndex] =
                    Math.Clamp(longest, 8, 40);
            }

            return weights;
        }

        // ---------------- DTOs ----------------

        public class SalesReportDto
        {
            public int TotalTransactions { get; set; }

            public decimal TotalSales { get; set; }

            public decimal TotalItemsSold { get; set; }

            public List<SalesReportItemDto> Items { get; set; }
                = new();
        }

        public class SalesHistoryDto
        {
            public int SalesId { get; set; }

            public string SaleNumber { get; set; } =
                string.Empty;

            public DateTime SaleDate { get; set; }

            public string Cashier { get; set; } =
                string.Empty;

            public string PaymentMethod { get; set; } =
                string.Empty;

            public decimal TotalAmount { get; set; }

            public decimal CashReceived { get; set; }

            public decimal ChangeAmount { get; set; }

            public string Status { get; set; } =
                string.Empty;
        }

        public class SalesReportItemDto
        {
            public int ProductId { get; set; }

            public string ProductName { get; set; }
                = string.Empty;

            public decimal UnitPrice { get; set; }

            public decimal QuantitySold { get; set; }

            public decimal TotalAmount { get; set; }
        }

        public class InventoryReportItem
        {
            public int ProductId { get; set; }

            public string ProductName { get; set; }
                = string.Empty;

            public decimal QuantityOnHand { get; set; }

            public decimal ReorderLevel { get; set; }

            public bool IsActive { get; set; }
        }

        public class IncomeStatementApi
        {
            public decimal TotalSales { get; set; }

            public decimal TotalPurchases { get; set; }

            public decimal TotalExpenses { get; set; }

            public decimal TotalPayroll { get; set; }

            public decimal NetIncome { get; set; }

            public List<IncomeStatementLineApi> Lines { get; set; }
                = new();
        }

        public class IncomeStatementLineApi
        {
            public string Label { get; set; }
                = string.Empty;

            public decimal? Amount { get; set; }

            public string Kind { get; set; }
                = "Line";
        }
    }
}