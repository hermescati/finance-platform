using Expensier.WPF.Enums;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;
using static Expensier.Domain.Models.Transaction;

namespace Expensier.WPF.Converters
{
    public class CategoryToIconConverter : IValueConverter
    {
        private Dictionary<TransactionCategory, ImageSource> categoryIconMap;

        public CategoryToIconConverter()
        {
            InitializeCategoryIconMap();
        }

        private void InitializeCategoryIconMap()
        {
            categoryIconMap = new Dictionary<TransactionCategory, ImageSource>
            {
                { TransactionCategory.Income, App.Current.FindResource("IncomeIcon") as ImageSource },
                { TransactionCategory.Rent, App.Current.FindResource("RentIcon") as ImageSource },
                { TransactionCategory.Utilities, App.Current.FindResource("UtilitiesIcon") as ImageSource },
                { TransactionCategory.Food, App.Current.FindResource("FoodIcon") as ImageSource },
                { TransactionCategory.Travel, App.Current.FindResource("TravelIcon") as ImageSource },
                { TransactionCategory.Shopping, App.Current.FindResource("ShoppingIcon") as ImageSource },
                { TransactionCategory.Subscription, App.Current.FindResource("SubscriptionIcon") as ImageSource }
            };
        }

        public object Convert( object value, Type targetType, object parameter, CultureInfo culture )
        {
            if ( value == null || !( value is TransactionCategory ) )
            {
                return null;
            }

            TransactionCategory category = ( TransactionCategory ) value;

            if ( categoryIconMap.ContainsKey( category ) )
            {
                return categoryIconMap[category];
            }
            else
            {
                return null;
            }
        }

        public object ConvertBack( object value, Type targetType, object parameter, CultureInfo culture )
        {
            throw new NotImplementedException();
        }
    }
}
