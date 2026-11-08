using Larp.domain.DTO;
using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Larp.winforms.Admin
{
    public partial class TermsWinforms : Form
    {
        private readonly HttpClient _http;

        public TermsWinforms(HttpClient httpClient)
        {
            InitializeComponent();

            _http = httpClient;

            Load += async (s, e) => await LoadTermsAsync();
        }

        private async Task LoadTermsAsync()
        {
            try
            {
                SetLoading(true);

                var terms =
                    await _http.GetFromJsonAsync<TermsAndConditionsDto>(
                        "admin/terms");

                if (terms == null)
                {
                    MessageBox.Show(
                        "Terms and Conditions could not be loaded.",
                        "Terms & Conditions",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    return;
                }

                txtVersion.Text = terms.Version;
                dtpEffectiveDate.Value =
                    terms.EffectiveDate == default
                        ? DateTime.Today
                        : terms.EffectiveDate;

                txtTerms.Text = terms.Content ?? "";
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Unable to load Terms & Conditions.\n\n" +
                    ex.Message,
                    "Terms & Conditions",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            finally
            {
                SetLoading(false);
            }
        }

        private async void BtnSave_Click(
            object sender,
            EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtVersion.Text))
            {
                MessageBox.Show(
                    "Please enter a terms version.",
                    "Validation",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                txtVersion.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtTerms.Text))
            {
                MessageBox.Show(
                    "Terms and Conditions content cannot be empty.",
                    "Validation",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                txtTerms.Focus();
                return;
            }

            try
            {
                SetLoading(true);

                var request = new SaveTermsAndConditionsRequest
                {
                    Version = txtVersion.Text.Trim(),
                    EffectiveDate = dtpEffectiveDate.Value.Date,
                    Content = txtTerms.Text.Trim(),
                    IsActive = true
                };

                var response =
                    await _http.PutAsJsonAsync(
                        "admin/terms",
                        request);

                if (!response.IsSuccessStatusCode)
                {
                    var error =
                        await response.Content.ReadAsStringAsync();

                    MessageBox.Show(
                        error,
                        "Unable to save Terms & Conditions",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);

                    return;
                }

                MessageBox.Show(
                    "Terms & Conditions have been saved successfully.",
                    "Saved",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Unable to save Terms & Conditions.\n\n" +
                    ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            finally
            {
                SetLoading(false);
            }
        }

        private async void BtnRefresh_Click(
            object sender,
            EventArgs e)
        {
            await LoadTermsAsync();
        }

        private async void BtnExportPdf_Click(
            object sender,
            EventArgs e)
        {
            try
            {
                SetLoading(true);

                using var response =
                    await _http.GetAsync(
                        "admin/terms/pdf");

                if (!response.IsSuccessStatusCode)
                {
                    var error =
                        await response.Content.ReadAsStringAsync();

                    MessageBox.Show(
                        error,
                        "Unable to generate PDF",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);

                    return;
                }

                var bytes =
                    await response.Content.ReadAsByteArrayAsync();

                using var dialog =
                    new SaveFileDialog
                    {
                        Filter =
                            "PDF Files (*.pdf)|*.pdf",

                        FileName =
                            $"Larp_SmartSariSari_Terms_{DateTime.Today:yyyyMMdd}.pdf",

                        Title =
                            "Export Terms & Conditions"
                    };

                if (dialog.ShowDialog(this) !=
                    DialogResult.OK)
                    return;

                await System.IO.File.WriteAllBytesAsync(
                    dialog.FileName,
                    bytes);

                MessageBox.Show(
                    "Terms & Conditions PDF has been exported successfully.",
                    "PDF Ready",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Unable to export PDF.\n\n" +
                    ex.Message,
                    "PDF Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            finally
            {
                SetLoading(false);
            }
        }

        private void BtnClose_Click(
            object sender,
            EventArgs e)
        {
            Close();
        }

        private void SetLoading(bool loading)
        {
            btnRefresh.Enabled = !loading;
            btnSave.Enabled = !loading;
            btnExportPdf.Enabled = !loading;
            txtVersion.Enabled = !loading;
            dtpEffectiveDate.Enabled = !loading;
            txtTerms.Enabled = !loading;
        }
    }
}