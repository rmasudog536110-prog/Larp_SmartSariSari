using Larp.domain.DTO;
using Larp.infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace Larp.api.Controllers
{
    [ApiController]
    public class TermsAndConditionsController : ControllerBase
    {
        private readonly TermsAndConditionsService _service;

        public TermsAndConditionsController(
            TermsAndConditionsService service)
        {
            _service = service;
        }

        [HttpGet("admin/terms")]
        public async Task<IActionResult> Get()
        {
            var terms = await _service.GetActiveAsync();

            if (terms == null)
            {
                return Ok(new TermsAndConditionsDto
                {
                    TermsAndConditionsId = 0,
                    Version = "1.0",
                    EffectiveDate = DateTime.Today,
                    Content = GetDefaultTerms(),
                    IsActive = true,
                    UpdatedAt = DateTime.UtcNow
                });
            }

            return Ok(terms);
        }

        [HttpPut("admin/terms")]
        public async Task<IActionResult> Save(
            [FromBody] SaveTermsAndConditionsRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Version))
                return BadRequest("Version is required.");

            if (string.IsNullOrWhiteSpace(request.Content))
                return BadRequest("Terms & Conditions content is required.");

            var result = await _service.SaveAsync(request);

            return Ok(result);
        }

        [HttpGet("admin/terms/pdf")]
        public async Task<IActionResult> ExportPdf()
        {
            QuestPDF.Settings.License = LicenseType.Community;

            var terms = await _service.GetActiveAsync();

            if (terms == null)
            {
                terms = new TermsAndConditionsDto
                {
                    Version = "1.0",
                    EffectiveDate = DateTime.Today,
                    Content = GetDefaultTerms(),
                    IsActive = true
                };
            }

            var pdf = Document.Create(document =>
            {
                document.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(45);
                    page.DefaultTextStyle(style =>
                        style
                            .FontFamily("Arial")
                            .FontSize(9.5f)
                            .FontColor("#000000"));

                    page.Content()
                        .Column(column =>
                        {
                            column.Spacing(8);

                            column.Item()
                                .Text("Larp Sari-Sari Smart App")
                                .FontSize(18)
                                .Bold()
                                .FontColor("#000000");

                            column.Item()
                                .Text(
                                    $"Terms & Conditions — Version {terms.Version}")
                                .FontSize(12)
                                .Bold()
                                .FontColor("#000000");

                            column.Item()
                                .Text(
                                    $"Effective Date: {terms.EffectiveDate:MMMM dd, yyyy}")
                                .FontSize(9.5f)
                                .FontColor("#000000");

                            column.Item()
                                .PaddingTop(10)
                                .Column(contentColumn =>
                                {
                                    var paragraphs = terms.Content
                                        .Replace("\r\n", "\n")
                                        .Split('\n');

                                    foreach (var paragraph in paragraphs)
                                    {
                                        if (string.IsNullOrWhiteSpace(paragraph))
                                        {
                                            contentColumn.Item()
                                                .Height(8);

                                            continue;
                                        }

                                        contentColumn.Item()
                                            .Text(paragraph.Trim())
                                            .FontSize(9.5f)
                                            .LineHeight(1.35f)
                                            .FontColor("#000000");
                                    }
                                });
                        });

                    page.Footer()
                        .AlignCenter()
                        .Text(text =>
                        {
                            text.Span("Larp Sari-Sari Smart App")
                                .FontSize(8)
                                .FontColor("#606979");

                            text.Span(" • ")
                                .FontSize(8)
                                .FontColor("#606979");

                            text.CurrentPageNumber()
                                .FontSize(8)
                                .FontColor("#606979");

                            text.Span(" / ")
                                .FontSize(8)
                                .FontColor("#606979");

                            text.TotalPages()
                                .FontSize(8)
                                .FontColor("#606979");
                        });
                });
            }).GeneratePdf();

            return File(
                pdf,
                "application/pdf",
                $"Terms-and-Conditions-v{terms.Version}.pdf");
        }

        private static string GetDefaultTerms()
        {
            return """
                1. Acceptance of Terms

                By using the Larp Sari-Sari Smart App, the customer agrees to these Terms and Conditions.

                2. Software License

                The application is provided as a software service for business management, inventory tracking, sales recording, purchasing, reporting, and related operations.

                3. Customer Account and Access

                Customers are responsible for maintaining the confidentiality of their account credentials and for all activities performed using their account.

                4. Customer Data

                Customers retain ownership of the business information and records entered into the application.

                5. Data Backup

                Customers are responsible for maintaining appropriate backups of important business information.

                6. Software Updates and Maintenance

                The application may receive updates, improvements, maintenance, security fixes, and feature changes.

                7. Technical Support

                Technical support may be provided through the support channels made available by the service provider.

                8. Intellectual Property

                The Larp Sari-Sari Smart App, including its software, design, branding, and related materials, remains the property of its respective owner unless otherwise stated.

                9. Suspension and Termination

                Access may be suspended or terminated for violations of these Terms, non-payment, misuse, or other legitimate reasons.

                10. Warranty Disclaimer

                The application is provided on an as-available basis. No guarantee is made that the software will always operate without interruption or error.

                11. Limitation of Liability

                To the extent permitted by applicable law, the service provider shall not be liable for indirect or consequential losses resulting from the use of the application.

                12. Changes to Terms

                These Terms and Conditions may be updated when necessary. The latest active version will be made available through the application.

                13. Governing Law

                These Terms and Conditions shall be governed by the applicable laws of the Republic of the Philippines.

                14. Severability

                If any provision of these Terms is found to be invalid or unenforceable, the remaining provisions shall continue to apply.

                15. Entire Agreement

                These Terms and Conditions constitute the agreement between the customer and the service provider regarding the use of the application.
                """;
        }
    }
}