using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;


namespace Expensier.WPF.Converters
{
    public class NullToVisibilityConverter : IValueConverter
    {
        private bool IsNumericType( object value )
        {
            return value is int ||
                   value is double ||
                   value is decimal ||
                   value is float;
        }


        public object Convert( object value, Type targetType, object parameter, CultureInfo culture )
        {
            bool isInverse = parameter as string == "Inverse";

            if ( value is null )
                return isInverse ? Visibility.Visible : Visibility.Collapsed;

            if ( value is bool boolValue )
                return isInverse ? Visibility.Visible : Visibility.Collapsed;

            if ( value is string stringValue && string.IsNullOrEmpty( stringValue ) )
                return isInverse ? Visibility.Visible : Visibility.Collapsed;

            if ( IsNumericType( value ) && (double) value == 0 )
                return isInverse ? Visibility.Visible : Visibility.Collapsed;

            return isInverse ? Visibility.Collapsed : Visibility.Visible;
        }


        public object ConvertBack( object value, Type targetType, object parameter, CultureInfo culture )
        {
            throw new NotImplementedException();
        }
    }
}
