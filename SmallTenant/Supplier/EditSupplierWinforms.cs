using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Windows.Forms;
using SupplierEntity = Larp.domain.Entities.Supplier;

namespace Larp.winforms.SmallTenant.Supplier
{
    public partial class EditSupplierWinforms : Form
    {
        private readonly HttpClient _httpClient;
        private readonly int _companyId;
        private readonly SupplierEntity _supplier;

        public EditSupplierWinforms(
            HttpClient httpClient,
            int companyId,
            SupplierEntity supplier)
        {
            InitializeComponent();

            _httpClient = httpClient;
            _companyId = companyId;
            _supplier = supplier;

            LoadSupplier();
        }

        private void LoadSupplier()
        {
            txtSupplierCode.ReadOnly = true;
            txtSupplierCode.TabStop = false;

            txtSupplierName.Text =
                _supplier.SupplierName;

            txtContactPerson.Text =
                _supplier.ContactPerson ?? string.Empty;

            txtContactNumber.Text =
                _supplier.ContactNumber ?? string.Empty;

            txtEmailAddress.Text =
                _supplier.EmailAddress ?? string.Empty;

            txtAddress.Text =
                _supplier.Address ?? string.Empty;

            chkIsActive.Checked =
                _supplier.IsActive;
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

                var request = new
                {
                    SupplierCode =
                        txtSupplierCode.Text.Trim(),

                    SupplierName =
                        txtSupplierName.Text.Trim(),

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

                    IsActive =
                        chkIsActive.Checked
                };

                var response =
                    await _httpClient.PutAsJsonAsync(
                        $"tenant/{_companyId}/suppliers/{_supplier.SupplierId}",
                        request);

                if (!response.IsSuccessStatusCode)
                {
                    var message =
                        await response.Content.ReadAsStringAsync();

                    MessageBox.Show(
                        $"Failed to update supplier.\n\n{message}",
                        "Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);

                    return;
                }

                MessageBox.Show(
                    "Supplier updated successfully.",
                    "Success",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Unable to update supplier.\n\n{ex.Message}",
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
