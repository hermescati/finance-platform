using System;
using System.Globalization;
using System.Windows.Data;


namespace Expensier.WPF.Converters
{
    public class AssetChangePercentageConverter : IValueConverter
    {
        private readonly object _secondaryColor = App.Current.FindResource( "Secondary" );
        private readonly object _accentColor = App.Current.FindResource( "Accent" );


        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double percentage = (double)value;
            return percentage < 0 ? _accentColor : _secondaryColor;
        }


        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
