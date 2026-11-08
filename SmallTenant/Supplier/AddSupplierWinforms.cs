using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Windows.Forms;
using SupplierEntity = Larp.domain.Entities.Supplier;

namespace Larp.winforms.SmallTenant.Supplier
{
    public partial class AddSupplierWinforms : Form
    {
        private readonly HttpClient _httpClient;
        private readonly int _companyId;

        public AddSupplierWinforms(
            HttpClient httpClient,
            int companyId)
        {
            InitializeComponent();

            _httpClient = httpClient;
            _companyId = companyId;

            chkIsActive.Checked = true;
            txtSupplierCode.Text = "SUPP-";
            txtSupplierCode.ReadOnly = true;
            txtSupplierCode.TabStop = false;
        }

        private async void btnSave_Click(
            object sender,
            EventArgs e)
        {

            if (string.IsNullOrWhiteSpace(txtSupplierName.Text))
            {
                MessageBox.Show(
                    "Supplier name is required.",
                    "Validation",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                txtSupplierName.Focus();
                return;
            }

            try
            {
                btnSave.Enabled = false;

                var supplier = new SupplierEntity
                {
                    SupplierCode = string.Empty,
                    SupplierName = txtSupplierName.Text.Trim(),
                    ContactPerson =
                        string.IsNullOrWhiteSpace(txtContactPerson.Text)
                            ? null
                            : txtContactPerson.Text.Trim(),
                    ContactNumber =
                        string.IsNullOrWhiteSpace(txtContactNumber.Text)
                            ? null
                            : txtContactNumber.Text.Trim(),
                    EmailAddress =
                        string.IsNullOrWhiteSpace(txtEmailAddress.Text)
                            ? null
                            : txtEmailAddress.Text.Trim(),
                    Address =
                        string.IsNullOrWhiteSpace(txtAddress.Text)
                            ? null
                            : txtAddress.Text.Trim(),
                    IsActive = chkIsActive.Checked
                };

                var response =
                    await _httpClient.PostAsJsonAsync(
                        $"tenant/{_companyId}/suppliers",
                        supplier);

                if (!response.IsSuccessStatusCode)
                {
                    var message =
                        await response.Content.ReadAsStringAsync();

                    MessageBox.Show(
                        $"Failed to create supplier.\n\n{message}",
                        "Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);

                    return;
                }

                var created = await response.Content.ReadFromJsonAsync<SupplierEntity>();

                MessageBox.Show(
                    $"Supplier added.\n\nSupplier code: {created?.SupplierCode}",
                    "Success",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Unable to create supplier.\n\n{ex.Message}",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            finally
            {
                btnSave.Enabled = true;
            }
        }

        private void btnCancel_Click(
            object sender,
            EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

    }
}
