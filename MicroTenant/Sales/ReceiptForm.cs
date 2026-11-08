using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace Larp.winforms.MicroTenant.Sales
{
    public partial class ReceiptForm : Form
    {
        private readonly HttpClient _httpClient;
        private readonly int _companyId;
        private readonly int _salesId;

        private SaleReceiptDto? sale;

        public ReceiptForm(
            HttpClient httpClient,
            int companyId,
            int salesId)
        {
            InitializeComponent();

            _httpClient = httpClient;
            _companyId = companyId;
            _salesId = salesId;

            StartPosition =
                FormStartPosition.CenterParent;

            Load += ReceiptForm_Load;
            printButton.Click += PrintButton_Click;
            closeButton.Click += CloseButton_Click;
        }

        private async void ReceiptForm_Load(
            object? sender,
            EventArgs e)
        {
            await LoadReceipt();
        }

        private async Task LoadReceipt()
        {
            try
            {
                sale =
                    await _httpClient.GetFromJsonAsync<SaleReceiptDto>(
                        $"/tenant/{_companyId}/sales/{_salesId}");

                if (sale == null)
                {
                    receiptText.Text =
                        "Unable to load receipt.";

                    return;
                }

                receiptText.Text =
                    BuildReceipt(sale);
            }
            catch (Exception ex)
            {
                receiptText.Text =
                    "Unable to load receipt.\r\n\r\n" +
                    ex.Message;
            }
        }

        private string BuildReceipt(
            SaleReceiptDto sale)
        {
            string company =
                string.IsNullOrWhiteSpace(Session.CompanyName)
                    ? "LARP Sari-Sari Smart App"
                    : Session.CompanyName!;

            string cashier =
                sale.Employee == null
                    ? Session.FullName
                    : $"{sale.Employee.FirstName} {sale.Employee.LastName}";

            var lines = new List<string>();

            lines.Add(
                Center(company, 40));

            lines.Add(
                Center("SALES RECEIPT", 40));

            lines.Add(
                new string('-', 40));

            lines.Add(
                $"Sale No.: {sale.SaleNumber}");

            lines.Add(
                $"Date: {sale.SaleDate.ToLocalTime():MMM dd, yyyy hh:mm tt}");

            lines.Add(
                $"Cashier: {cashier}");

            lines.Add(
                new string('-', 40));

            foreach (var item in sale.SaleItems)
            {
                lines.Add(
                    item.Product?.ProductName
                    ?? $"Product #{item.ProductId}");

                string quantity =
                    $"{item.Quantity:0.##} x ₱{item.UnitPrice:N2}";

                string amount =
                    $"₱{item.TotalAmount:N2}";

                lines.Add(
                    PadLine(
                        quantity,
                        amount,
                        40));
            }

            lines.Add(
                new string('-', 40));

            lines.Add(
                PadLine(
                    "Subtotal",
                    $"₱{sale.Subtotal:N2}",
                    40));

            lines.Add(
                PadLine(
                    "Discount",
                    $"₱{sale.Discount:N2}",
                    40));

            lines.Add(
                PadLine(
                    "VATable Sales",
                    $"₱{sale.VatableSales:N2}",
                    40));

            lines.Add(
                PadLine(
                    "VAT",
                    $"₱{sale.VatAmount:N2}",
                    40));

            lines.Add(
                PadLine(
                    "VAT-Exempt",
                    $"₱{sale.VatExempt:N2}",
                    40));

            lines.Add(
                new string('-', 40));

            lines.Add(
                PadLine(
                    "TOTAL",
                    $"₱{sale.TotalAmount:N2}",
                    40));

            lines.Add(
                PadLine(
                    "Payment",
                    sale.PaymentMethod,
                    40));

            lines.Add(
                PadLine(
                    "Cash Received",
                    $"₱{sale.CashReceived:N2}",
                    40));

            lines.Add(
                PadLine(
                    "Change",
                    $"₱{sale.ChangeAmount:N2}",
                    40));

            lines.Add(
                new string('-', 40));

            lines.Add(
                Center(
                    "Thank you for your purchase!",
                    40));

            return string.Join(
                Environment.NewLine,
                lines);
        }

        private static string Center(
            string text,
            int width)
        {
            if (text.Length >= width)
                return text;

            int left =
                (width - text.Length) / 2;

            return new string(' ', left) + text;
        }

        private static string PadLine(
            string left,
            string right,
            int width)
        {
            int spaces =
                width -
                left.Length -
                right.Length;

            if (spaces < 1)
                spaces = 1;

            return left +
                   new string(' ', spaces) +
                   right;
        }

        private void PrintButton_Click(
            object? sender,
            EventArgs e)
        {
            if (sale == null)
                return;

            using var document =
                new PrintDocument();

            document.PrintPage +=
                Document_PrintPage;

            using var dialog =
                new PrintDialog
                {
                    Document = document
                };

            if (dialog.ShowDialog(this) ==
                DialogResult.OK)
            {
                document.Print();
            }
        }

        private void Document_PrintPage(
            object? sender,
            PrintPageEventArgs e)
        {
            using var font =
                new Font("Consolas", 9F);

            e.Graphics.DrawString(
                receiptText.Text,
                font,
                Brushes.Black,
                new RectangleF(
                    10,
                    10,
                    e.PageBounds.Width - 20,
                    e.PageBounds.Height - 20));
        }

        private void CloseButton_Click(
            object? sender,
            EventArgs e)
        {
            Close();
        }
    }

    public class SaleReceiptDto
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

        public SaleEmployeeDto? Employee { get; set; }

        public List<SaleReceiptItemDto> SaleItems { get; set; } =
            new();
    }

    public class SaleEmployeeDto
    {
        public string FirstName { get; set; } =
            string.Empty;

        public string LastName { get; set; } =
            string.Empty;
    }

    public class SaleReceiptItemDto
    {
        public int ProductId { get; set; }

        public decimal Quantity { get; set; }

        public decimal UnitPrice { get; set; }

        public decimal Discount { get; set; }

        public decimal TotalAmount { get; set; }

        public ProductReceiptDto? Product { get; set; }
    }

    public class ProductReceiptDto
    {
        public string ProductName { get; set; } =
            string.Empty;
    }
}