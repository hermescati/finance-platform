using System.Windows.Media;


namespace Expensier.WPF.ViewModels.Charts
{
    public class ChartModel : ViewModelBase
    {
        public string SeriesLabel { get; set; }
        public double SeriesValue { get; set; }
        public SolidColorBrush? SeriesColor { get; set; }
        public string? DetailedLabel { get; set; }


        public ChartModel(
            string label,
            double percentage,
            SolidColorBrush? color = null,
            string? detailedLabel = null )
        {
            SeriesLabel = label;
            SeriesValue = percentage;
            DetailedLabel = detailedLabel;
            SeriesColor = color;
        }
    }
}
