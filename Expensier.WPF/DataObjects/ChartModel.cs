using System.Windows.Media;


namespace Expensier.WPF.ViewModels.Charts
{
    public class ChartModel : ViewModelBase
    {
        public string SeriesLabel { get; set; }
        public double SeriesValue { get; set; }
        public SolidColorBrush? SeriesColor { get; set; }


        public ChartModel( string label, double totalAmount )
        {
            SeriesLabel = label;
            SeriesValue = totalAmount;
        }


        public ChartModel( string label, double percentage, SolidColorBrush color )
        {
            SeriesLabel = label;
            SeriesValue = percentage;
            SeriesColor = color;
        }
    }
}
