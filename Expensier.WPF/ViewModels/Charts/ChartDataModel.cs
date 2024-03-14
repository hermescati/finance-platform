using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Expensier.WPF.ViewModels.Charts
{
    public class ChartDataModel : ViewModelBase
    {
        public string SeriesLabel { get; set; }
        public double SeriesValue { get; set; }
        public SolidColorBrush? SeriesColor { get; set; }


        public ChartDataModel(string label, double totalAmount )
        {
            SeriesLabel = label;
            SeriesValue = totalAmount;
        }


        public ChartDataModel(string label, double percentage, SolidColorBrush color )
        {
            SeriesLabel = label;
            SeriesValue = percentage;
            SeriesColor = color;
        }
    }
}
