using Larp.winforms.Admin;
using Larp.winforms.MicroTenant.Sales;
using Larp.winforms.SmallTenant.Dashboard;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using System.Windows.Forms;

namespace Larp.winforms.SmallTenant.Login
{
    public partial class LoginWinforms : Form
    {
        private readonly HttpClient _httpClient;

        public LoginWinforms(HttpClient httpClient)
        {
            InitializeComponent();
            _httpClient = httpClient;
        }

        private async void btnLogin_Click(object sender, EventArgs e)
        {
            lblStatus.Text = "";
            btnLogin.Enabled = false;
            btnLogin.Text = "Signing in...";
            Cursor = Cursors.WaitCursor;

            try
            {
                var response = await _httpClient.PostAsJsonAsync("auth/login", new
                {
                    username = txtUsername.Text.Trim(),
                    password = txtPassword.Text
                });

                if (!response.IsSuccessStatusCode)
                {
                    lblStatus.Text = await ReadErrorAsync(response);
                    return;
                }

                var result = await response.Content.ReadFromJsonAsync<LoginResponse>(
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                if (result == null)
                {
                    lblStatus.Text = "Login failed. Please try again.";
                    return;
                }

                Session.Start(result.Token, result.FullName, result.Role,
                    result.CompanyId, result.CompanyName, result.EmployeeId,
                    result.PlanName, result.Modules, result.MaxEmployees, result.MaxBranches);

                _httpClient.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", result.Token);

                OpenAppropriateForm();
            }
            catch (HttpRequestException)
            {
                lblStatus.Text = "Cannot reach the server.";
            }
            catch (Exception ex)
            {
                lblStatus.Text = $"Unexpected error: {ex.Message}";
            }
            finally
            {
                btnLogin.Enabled = true;
                btnLogin.Text = "Log In";
                Cursor = Cursors.Default;
            }
        }

        private static async System.Threading.Tasks.Task<string> ReadErrorAsync(
            HttpResponseMessage response)
        {
            try
            {
                var body = await response.Content.ReadFromJsonAsync<JsonElement>();
                if (body.TryGetProperty("message", out var m) && m.GetString() is string s)
                    return s;
            }
            catch { }
            return "Login failed. Please try again.";
        }

        private void OpenAppropriateForm()
        {
            Hide();
            txtPassword.Clear();

            Form next = Session.Role switch
            {
                "SuperAdmin" => new AdminWinforms(_httpClient, 0),
                "Staff" => new SalesWinForms(_httpClient, Session.CompanyId!.Value),
                _ => new DashboardWinforms(_httpClient, Session.CompanyId!.Value)
            };

            using (next)
            {
                next.ShowDialog();
            }

            // Back at the login screen: clear the session.
            Session.Clear();
            _httpClient.DefaultRequestHeaders.Authorization = null;
            Show();
        }

        private void chkShowPassword_CheckedChanged(object sender, EventArgs e)
        {
            txtPassword.PasswordChar = chkShowPassword.Checked ? '\0' : '●';
        }

        private class LoginResponse
        {
            public string Token { get; set; } = "";
            public string FullName { get; set; } = "";
            public string Role { get; set; } = "";
            public int? CompanyId { get; set; }
            public string? CompanyName { get; set; }
            public int? EmployeeId { get; set; }
            public string? PlanName { get; set; }
            public List<string> Modules { get; set; } = new();
            public int MaxEmployees { get; set; }
            public int MaxBranches { get; set; }
        }
    }
}