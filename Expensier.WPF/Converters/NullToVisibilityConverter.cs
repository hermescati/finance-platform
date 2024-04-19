using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;


namespace Expensier.WPF.Converters
{
    public class NullToVisibilityConverter : IValueConverter
    {
        public object Convert( object value, Type targetType, object parameter, CultureInfo culture )
        {
            if ( value == null )
                return Visibility.Collapsed;

            if ( value is bool boolValue && !boolValue )
                return Visibility.Collapsed;

            if ( value is string stringValue && string.IsNullOrEmpty(stringValue) )
                return Visibility.Collapsed;

            if ( IsNumericType( value ) && ( double ) value == 0 )
                return Visibility.Collapsed;

            return Visibility.Visible;
        }


        public object ConvertBack( object value, Type targetType, object parameter, CultureInfo culture )
        {
            throw new NotImplementedException();
        }


        private bool IsNumericType( object value )
        {
            return value is int ||
                   value is double ||
                   value is decimal ||
                   value is float;
        }
    }
}
