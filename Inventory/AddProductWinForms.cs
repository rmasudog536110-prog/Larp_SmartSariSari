using System;
using System.Globalization;
using System.Net.Http;
using System.Net.Http.Json;
using System.Windows.Forms;

namespace Larp.winforms.Inventory
{
    public partial class AddProductWinForms : Form
    {
        private readonly HttpClient? _httpClient;
        private readonly int _companyId;

        public AddProductWinForms()
        {
            InitializeComponent();
        }

        public AddProductWinForms(HttpClient httpClient, int companyId)
        {
            InitializeComponent();

            _httpClient = httpClient;
            _companyId = companyId;

            StartPosition = FormStartPosition.CenterScreen;
        }

        private void AddProductWinForms_Load(object sender, EventArgs e)
        {
            txtProductCode.Focus();
            chkIsActive.Checked = true;
        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            string productCode = txtProductCode.Text.Trim();
            string productName = txtProductName.Text.Trim();
            string priceText = txtUnitPrice.Text.Trim();

            if (string.IsNullOrWhiteSpace(productCode))
            {
                MessageBox.Show(
                    "Please enter a product code.",
                    "Validation",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                txtProductCode.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(productName))
            {
                MessageBox.Show(
                    "Please enter a product name.",
                    "Validation",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                txtProductName.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(priceText))
            {
                MessageBox.Show(
                    "Please enter a unit price.",
                    "Validation",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                txtUnitPrice.Focus();
                return;
            }

            if (!decimal.TryParse(
                priceText,
                NumberStyles.Number,
                CultureInfo.CurrentCulture,
                out decimal unitPrice))
            {
                MessageBox.Show(
                    "Please enter a valid unit price.",
                    "Validation",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                txtUnitPrice.Focus();
                txtUnitPrice.SelectAll();
                return;
            }

            if (unitPrice <= 0)
            {
                MessageBox.Show(
                    "Unit price must be greater than 0.",
                    "Validation",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                txtUnitPrice.Focus();
                txtUnitPrice.SelectAll();
                return;
            }

            if (_httpClient == null)
            {
                MessageBox.Show(
                    "The API connection is not configured.",
                    "Configuration Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                return;
            }

            var newProduct = new
            {
                ProductCode = productCode,
                ProductName = productName,
                UnitPrice = unitPrice,
                IsActive = chkIsActive.Checked
            };

            btnSave.Enabled = false;
            btnCancel.Enabled = false;

            try
            {
                string endpoint =
                    $"/{_companyId}/products";

                var response =
                    await _httpClient.PostAsJsonAsync(
                        endpoint,
                        newProduct);

                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show(
                        "Product saved successfully.",
                        "Success",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    DialogResult = DialogResult.OK;
                    Close();

                    return;
                }

                string responseBody =
                    await response.Content.ReadAsStringAsync();

                string message =
                    GetServerErrorMessage(
                        response.StatusCode,
                        responseBody);

                MessageBox.Show(
                    message,
                    "Save Failed",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            catch (HttpRequestException ex)
            {
                MessageBox.Show(
                    $"Unable to connect to the API.\n\n" +
                    $"Make sure the API is running and the BaseAddress " +
                    $"is configured correctly.\n\n" +
                    $"{ex.Message}",
                    "Connection Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            catch (TaskCanceledException)
            {
                MessageBox.Show(
                    "The request to the API timed out or was cancelled.",
                    "Request Timeout",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"An unexpected error occurred while saving the product.\n\n" +
                    $"{ex.Message}",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            finally
            {
                btnSave.Enabled = true;
                btnCancel.Enabled = true;
            }
        }

        private string GetServerErrorMessage(
            System.Net.HttpStatusCode statusCode,
            string responseBody)
        {
            if (statusCode == System.Net.HttpStatusCode.Conflict)
            {
                return
                    "The product could not be saved because " +
                    "a product with the same code or name already exists.\n\n" +
                    responseBody;
            }

            if (statusCode == System.Net.HttpStatusCode.BadRequest)
            {
                return
                    "The API rejected the product information.\n\n" +
                    responseBody;
            }

            if (statusCode ==
                System.Net.HttpStatusCode.NotFound)
            {
                return
                    $"The company/tenant could not be found.\n\n" +
                    responseBody;
            }

            if ((int)statusCode >= 500)
            {
                return
                    "The server encountered an error while saving the product.\n\n" +
                    responseBody;
            }

            return
                $"The server rejected the request " +
                $"({(int)statusCode} {statusCode}).\n\n" +
                responseBody;
        }

        private void btnCancel_Click_1(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            this.Close();
        }

    }
}