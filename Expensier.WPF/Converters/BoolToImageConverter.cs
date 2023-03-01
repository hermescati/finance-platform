using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace Expensier.WPF.Converters
{
    public class BoolToImageConverter : IValueConverter
    {
        public ImageSource CreditImage { get; set; }
        public ImageSource DebitImage { get; set; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is bool))
            {
                return null;
            }

            bool isCredit = (bool)value;
            if (isCredit)
            {
                return this.CreditImage;
            }
            else
            {
                return this.DebitImage;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}