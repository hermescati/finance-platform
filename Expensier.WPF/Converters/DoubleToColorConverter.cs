using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace Expensier.WPF.Converters
{
    public class DoubleToColorConverter : IValueConverter
    {
        public Color NegativeColor { get; set; }
        public Color PositiveColor { get; set; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is double d)
            {
                return d < 0 ? new SolidColorBrush(NegativeColor) : new SolidColorBrush(PositiveColor);
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
