using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Expensier.WPF.Converters
{
    public class CryptoNameConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string cryptoName = value.ToString();
            string substring = parameter?.ToString() ?? "";
            
            if (cryptoName.Contains(substring))
            {
                return cryptoName.Replace(substring, "");
            }
            else
            {
                return cryptoName;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
