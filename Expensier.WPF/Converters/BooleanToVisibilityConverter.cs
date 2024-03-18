using System.Globalization;
using System.Windows.Data;
using System.Windows;
using System;


namespace Expensier.WPF.Converters
{
    public class BooleanToVisibilityConverter : IValueConverter
    {
        public object Convert( object value, Type targetType, object parameter, CultureInfo culture )
        {
            if ( value is bool boolValue )
            {
                bool inverted = parameter != null && parameter.ToString() == "Inverse";
                return boolValue ^ inverted ? Visibility.Visible : Visibility.Collapsed;
            }

            return Visibility.Collapsed;
        }


        public object ConvertBack( object value, Type targetType, object parameter, CultureInfo culture )
        {
            throw new NotImplementedException();
        }
    }
}
