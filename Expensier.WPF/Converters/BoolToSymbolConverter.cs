using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Expensier.WPF.Converters
{
    public class BoolToSymbolConverter : IValueConverter
    {
        public char CreditSymbol { get; set; }
        public char DebitSymbol { get; set; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is bool))
            {
                return null;
            }

            bool isCredit = (bool)value;
            if (isCredit)
            {
                return this.CreditSymbol;
            }
            else
            {
                return this.DebitSymbol;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}