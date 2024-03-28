using System;
using System.Globalization;
using System.Windows.Data;


namespace Expensier.WPF.Converters
{
    public class AssetCurrentPriceConverter : IValueConverter
    {
        public object Convert( object value, Type targetType, object parameter, CultureInfo culture )
        {
            double price = (double)value;

            if ( price >= 1 )
                return price.ToString( "C2", CultureInfo.CurrentCulture );

            return price.ToString("C6", CultureInfo.CurrentCulture );
        }

        public object ConvertBack( object value, Type targetType, object parameter, CultureInfo culture )
        {
            throw new NotImplementedException();
        }
    }
}
