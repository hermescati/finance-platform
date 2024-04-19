using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;


namespace Expensier.WPF.Converters.Assets
{
    public class QuantityOwnedFormatter : IValueConverter
    {
        public object Convert( object value, Type targetType, object parameter, CultureInfo culture )
        {
            if ( value == null )
                return "";

            double quantity = ( double ) value;

            if ( quantity >= 1 )
                return quantity.ToString( "#,0", CultureInfo.CurrentCulture );

            double roundedValue = Math.Round( quantity, 6 );
            return roundedValue.ToString( "0.######", CultureInfo.CurrentCulture );

        }

        public object ConvertBack( object value, Type targetType, object parameter, CultureInfo culture )
        {
            throw new NotImplementedException();
        }
    }
}
