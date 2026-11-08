using System.Drawing;
using System.Windows.Forms.DataVisualization.Charting;

namespace Larp.winforms
{
    public static class ChartTheme
    {
        public static void Apply(Chart chart)
        {
            chart.BackColor = AppTheme.Surface;
            chart.Palette = ChartColorPalette.None;
            chart.PaletteCustomColors = AppTheme.ChartPalette;

            foreach (ChartArea area in chart.ChartAreas)
            {
                area.BackColor = AppTheme.Surface;

                foreach (Axis axis in new[] { area.AxisX, area.AxisY })
                {
                    axis.LineColor = AppTheme.Border;
                    axis.MajorTickMark.LineColor = AppTheme.Border;
                    axis.LabelStyle.ForeColor = AppTheme.TextMuted;
                    axis.MajorGrid.LineColor = AppTheme.Border;
                }
            }

            foreach (Legend legend in chart.Legends)
            {
                legend.BackColor = AppTheme.Surface;
                legend.ForeColor = AppTheme.TextMuted;
            }

            foreach (Title title in chart.Titles)
            {
                title.ForeColor = AppTheme.TextPrimary;
            }

            foreach (Series series in chart.Series)
            {
                if (series.ChartType != SeriesChartType.Doughnut &&
                    series.ChartType != SeriesChartType.Pie)
                {
                    series.LabelForeColor = AppTheme.TextPrimary;
                }
            }
        }
    }
}