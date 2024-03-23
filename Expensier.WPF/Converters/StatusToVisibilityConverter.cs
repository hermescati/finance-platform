using Expensier.Domain.Models;
using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using static Expensier.Domain.Models.Subscription;


namespace Expensier.WPF.Converters
{
    public class StatusToVisibilityConverter : IValueConverter
    {
        public object Convert( object value, Type targetType, object parameter, CultureInfo culture )
        {
            if ( !(value is SubscriptionStatus status) )
                return Visibility.Collapsed;

            if ( (status == SubscriptionStatus.Active && (string) parameter == "Active") ||
                (status == SubscriptionStatus.Canceled && (string) parameter == "Canceled") )
            {
                return Visibility.Visible;
            }

            return Visibility.Collapsed;
        }


        public object ConvertBack( object value, Type targetType, object parameter, CultureInfo culture )
        {
            throw new NotImplementedException();
        }
    }
}
