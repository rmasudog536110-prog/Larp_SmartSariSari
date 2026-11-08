using Larp.domain.DTO;
using Larp.domain.Entities;
using QuestPDF.Fluent;
using QuestPDF.Drawing;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using System.Globalization;

namespace Larp.api.Documents;

public static class PlanPdfBuilder
{
    public static byte[] Build(IReadOnlyList<Plan> plans)
    {
        QuestPDF.Settings.License = LicenseType.Community;
        QuestPDF.Settings.License = QuestPDF.Infrastructure.LicenseType.Community;

        FontManager.RegisterFont(
            File.OpenRead(@"C:\Windows\Fonts\arial.ttf")
        );

        FontManager.RegisterFont(
    File.OpenRead(@"C:\Windows\Fonts\arial.ttf")
);

        FontManager.RegisterFont(
            File.OpenRead(@"C:\Windows\Fonts\arialbd.ttf")
        );

        FontManager.RegisterFont(
            File.OpenRead(@"C:\Windows\Fonts\ariali.ttf")
        );

        FontManager.RegisterFont(
            File.OpenRead(@"C:\Windows\Fonts\arialbi.ttf")
        );

        var document = Document.Create(container =>
        {
            container.Page(page =>
            {
                page.Size(PageSizes.A4.Landscape());
                page.Margin(36);
                page.DefaultTextStyle(x => x.FontFamily("Arial").FontSize(9.5f));

                page.Header()
                    .Column(column =>
                    {
                        column.Item()
                            .Text("Larp SmartSariSari")
                            .FontSize(20)
                            .Bold()
                            .FontColor("#1C283F");

                        column.Item()
                            .Row(row =>
                            {
                                row.RelativeItem()
                                    .Text("Plans and Pricing")
                                    .FontSize(11)
                                    .FontColor("#606979");

                                row.RelativeItem()
                                    .AlignRight()
                                    .Text($"As of {DateTime.Today:MMMM d, yyyy}")
                                    .FontSize(11)
                                    .FontColor("#606979");
                            });
                    });

                page.Content()
                    .PaddingTop(10)
                    .Element(content => BuildContent(content, plans));

                page.Footer()
                    .Text("Prices are in Philippine pesos (PHP). Prices and features are subject to change.")
                    .FontSize(8)
                    .FontColor("#606979");
            });
        });

        return document.GeneratePdf();
    }

    private static void BuildContent(
        IContainer container,
        IReadOnlyList<Plan> plans)
    {
        if (plans.Count == 0)
        {
            container
                .AlignCenter()
                .AlignMiddle()
                .Text("No active plans available.")
                .FontSize(12)
                .FontColor("#606979");

            return;
        }

        var info = new List<(string Label, Func<Plan, string> Value)>
        {
            ("Suited to", p =>
                string.IsNullOrWhiteSpace(p.Description)
                    ? "-"
                    : p.Description),

            ("Monthly price", p =>
                $"PHP {p.MonthlyPrice:N0}"),

            ("Yearly price", p =>
                $"PHP {p.YearlyPrice:N0}"),

            ("Employees", p =>
                $"Up to {p.MaxEmployees}"),

            ("Branches", p =>
                p.MaxBranches > 0
                    ? $"Up to {p.MaxBranches}"
                    : "Not included")
        };

        var modulesByPlan = plans
            .Select(p =>
                new HashSet<string>(
                    p.Modules?.Split(
                        ',',
                        StringSplitOptions.RemoveEmptyEntries |
                        StringSplitOptions.TrimEntries)
                    ?? Array.Empty<string>(),
                    StringComparer.OrdinalIgnoreCase))
            .ToList();

        container.Table(table =>
        {
            table.ColumnsDefinition(columns =>
            {
                columns.ConstantColumn(190);

                foreach (var _ in plans)
                    columns.RelativeColumn();
            });

            table.Header(header =>
            {
                header.Cell()
                    .Background("#1C283F")
                    .Padding(6)
                    .Text("");

                foreach (var plan in plans)
                {
                    header.Cell()
                        .Background("#1C283F")
                        .Padding(6)
                        .AlignCenter()
                        .AlignMiddle()
                        .Text(plan.Name)
                        .FontSize(13)
                        .Bold()
                        .FontColor("#FFFFFF");
                }
            });

            for (int r = 0; r < info.Count; r++)
            {
                var row = info[r];
                var shaded = r % 2 == 1;

                AddRow(
                    table,
                    row.Label,
                    plans,
                    i => row.Value(plans[i]),
                    r == 1 || r == 2,
                    "#1C283F",
                    shaded);
            }

            table.Cell()
                .ColumnSpan((uint)(plans.Count + 1))
                .Background("#E8E9EC")
                .Padding(6)
                .Text("Included features")
                .FontSize(9.5f)
                .Bold()
                .FontColor("#1C283F");

            for (int m = 0; m < PlanModules.All.Length; m++)
            {
                var (key, label) = PlanModules.All[m];
                var shaded = m % 2 == 1;

                AddRow(
                    table,
                    label,
                    plans,
                    i =>
                        modulesByPlan[i].Contains(key)
                            ? "Yes"
                            : "-",
                    true,
                    modulesByPlan.Count == 0
                        ? "#606979"
                        : "#2E6B45",
                    shaded,
                    i => modulesByPlan[i].Contains(key));
            }
        });
    }

    private static void AddRow(
        TableDescriptor table,
        string label,
        IReadOnlyList<Plan> plans,
        Func<int, string> value,
        bool strong,
        string color,
        bool shaded,
        Func<int, bool>? useGreenForYes = null)
    {
        var background = shaded
            ? "#F5F6F8"
            : "#FFFFFF";

        table.Cell()
            .Background(background)
            .BorderBottom(0.5f)
            .BorderColor("#DCDFE4")
            .PaddingVertical(4)
            .PaddingHorizontal(8)
            .AlignMiddle()
            .Text(label)
            .FontColor("#1C283F")
            .FontSize(9.5f);

        for (int i = 0; i < plans.Count; i++)
        {
            var text = value(i);

            var textColor =
                useGreenForYes != null && useGreenForYes(i)
                    ? "#2E6B45"
                    : color;

            var cell = table.Cell()
                .Background(background)
                .BorderBottom(0.5f)
                .BorderColor("#DCDFE4")
                .PaddingVertical(4)
                .PaddingHorizontal(4)
                .AlignCenter()
                .AlignMiddle();

            if (strong)
            {
                cell.Text(text)
                    .Bold()
                    .FontSize(9.5f)
                    .FontColor(textColor);
            }
            else
            {
                cell.Text(text)
                    .FontSize(9.5f)
                    .FontColor(textColor);
            }
        }
    }
}