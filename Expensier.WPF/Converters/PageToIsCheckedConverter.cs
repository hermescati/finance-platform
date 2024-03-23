using System;
using System.Globalization;
using System.Windows.Data;


namespace Expensier.WPF.Converters
{
    public class PageToIsCheckedConverter : IMultiValueConverter
    {
        public object Convert( object[] values, Type targetType, object parameter, CultureInfo culture )
        {
            if ( values[0] is int currentPage && values[1] is int buttonPage )
            {
                return currentPage == buttonPage;
            }
            return false;
        }

        public object[] ConvertBack( object value, Type[] targetTypes, object parameter, CultureInfo culture )
        {
            throw new NotSupportedException( "ConvertBack not supported." );
        }
    }
}
