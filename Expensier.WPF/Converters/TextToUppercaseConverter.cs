using System;
using System.Globalization;
using System.Windows.Data;


namespace Expensier.WPF.Converters
{
    public class TextToUppercaseConverter : IValueConverter
    {
        public object Convert( object value, Type targetType, object parameter, CultureInfo culture )
        {
            if (value != null && value is string)
            {
                return ((string)value).ToUpper();
            }

            return value;
        }

        public object ConvertBack( object value, Type targetType, object parameter, CultureInfo culture )
        {
            throw new NotImplementedException();
        }
    }
}
