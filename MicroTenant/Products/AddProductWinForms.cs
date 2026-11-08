using System;
using System.Globalization;
using System.Net.Http;
using System.Net.Http.Json;
using System.Windows.Forms;
using System.Drawing;

namespace Larp.winforms.MicroTenant.Products
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

            AddVatCheckBox();

            _httpClient = httpClient;
            _companyId = companyId;

            StartPosition = FormStartPosition.CenterScreen;
        }

        private async void AddProductWinForms_Load(object sender, EventArgs e)
        {
            chkIsActive.Checked = true;
            chkIsVatable.Checked = true;

            txtProductCode.ReadOnly = true;

            await LoadNextProductCode();

            txtProductName.Focus();
        }

        private async Task LoadNextProductCode()
        {
            if (_httpClient == null)
            {
                MessageBox.Show(
                    "HTTP client is not available.",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                return;
            }

            try
            {
                var response =
                    await _httpClient.GetAsync(
                        $"/tenant/{_companyId}/products/next-code");

                if (response.IsSuccessStatusCode)
                {
                    string nextCode =
                        await response.Content.ReadAsStringAsync();

                    txtProductCode.Text =
                        nextCode.Trim('"');

                    return;
                }

                string error =
                    await response.Content.ReadAsStringAsync();

                MessageBox.Show(
                    GetServerErrorMessage(
                        response.StatusCode,
                        error),
                    "Unable to Generate Product Code",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            catch (HttpRequestException ex)
            {
                MessageBox.Show(
                    $"Unable to connect to the API.\n\n{ex.Message}",
                    "Connection Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            catch (TaskCanceledException)
            {
                MessageBox.Show(
                    "The request timed out.",
                    "Request Timeout",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"An unexpected error occurred.\n\n{ex.Message}",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            string productName =
                txtProductName.Text.Trim();

            string priceText =
                txtUnitPrice.Text.Trim();

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
                    "HTTP client is not available.",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                return;
            }

            var product = new
            {
                ProductName = productName,
                UnitPrice = unitPrice,
                IsVatable = chkIsVatable.Checked,
                IsActive = chkIsActive.Checked
            };

            btnSave.Enabled = false;
            btnCancel.Enabled = false;

            try
            {
                var response =
                    await _httpClient.PostAsJsonAsync(
                        $"/tenant/{_companyId}/products",
                        product);

                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show(
                        $"Product {txtProductCode.Text} saved successfully.",
                        "Success",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    DialogResult = DialogResult.OK;
                    Close();

                    return;
                }

                string error =
                    await response.Content.ReadAsStringAsync();

                MessageBox.Show(
                    GetServerErrorMessage(
                        response.StatusCode,
                        error),
                    "Save Failed",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            catch (HttpRequestException ex)
            {
                MessageBox.Show(
                    $"Unable to connect to the API.\n\n{ex.Message}",
                    "Connection Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            catch (TaskCanceledException)
            {
                MessageBox.Show(
                    "The request timed out.",
                    "Request Timeout",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"An unexpected error occurred.\n\n{ex.Message}",
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

        private void AddVatCheckBox()
        {
            chkIsVatable = new Guna.UI2.WinForms.Guna2CheckBox
            {
                AutoSize = true,
                Checked = true,
                Text = "VAT applies (12%)",
                Location = new Point(46, 337)
            };

            chkIsVatable.CheckedState.FillColor =
                chkIsActive.CheckedState.FillColor;

            chkIsVatable.CheckedState.BorderThickness = 0;

            chkIsVatable.UncheckedState.FillColor =
                chkIsActive.UncheckedState.FillColor;

            chkIsVatable.UncheckedState.BorderThickness = 0;

            ClientSize = new Size(384, 400);

            Controls.Add(chkIsVatable);
        }

        private string GetServerErrorMessage(
            System.Net.HttpStatusCode statusCode,
            string responseBody)
        {
            if (statusCode ==
                System.Net.HttpStatusCode.Conflict)
            {
                return
                    "The product could not be saved because " +
                    "a product with the same code or name already exists.\n\n" +
                    responseBody;
            }

            if (statusCode ==
                System.Net.HttpStatusCode.BadRequest)
            {
                return
                    "The API rejected the product information.\n\n" +
                    responseBody;
            }

            if (statusCode ==
                System.Net.HttpStatusCode.NotFound)
            {
                return
                    "The company/tenant could not be found.\n\n" +
                    responseBody;
            }

            if ((int)statusCode >= 500)
            {
                return
                    "The server encountered an error while saving " +
                    "the product.\n\n" +
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
            Close();
        }

        private void chkIsActive_CheckedChanged(
            object sender,
            EventArgs e)
        {
        }
    }
}