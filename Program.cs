using Larp.winforms.MicroTenant.Sales;
using Larp.winforms;
using System.Net.Http;
using System.Windows.Forms;

namespace Larp.winforms.Inventory;

internal static class Program
{
    /// <summary>
    ///  The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main()
    {
        // To customize application configuration such as set high DPI settings or default font,
        // see https://aka.ms/applicationconfiguration.

        var httpClient = new HttpClient
        {
            BaseAddress = new Uri("http://localhost:5084")
        };

        int companyId = 2;

        ApplicationConfiguration.Initialize();
        Application.Run(new Larp.winforms.SmallTenant.Login.LoginWinforms(httpClient));
    }
}