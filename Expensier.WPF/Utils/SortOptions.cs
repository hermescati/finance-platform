using Expensier.WPF.Enums;
using System;
using System.Collections.Generic;
using System.Windows.Media;


namespace Expensier.WPF.Utils
{
    public class SortOptions
    {
        public string Property { get; }
        public string Name { get; }
        public SortDirection Direction { get; }
        public ImageSource[] Icon { get; }


        private readonly static ImageSource[] _dateIcons =
        {
            App.Current.FindResource("DateIcon") as ImageSource ?? throw new InvalidOperationException("DateIcon resource not found"),
            App.Current.FindResource("DateSelectedIcon") as ImageSource ?? throw new InvalidOperationException("DateIcon resource not found")
        };

        private readonly static ImageSource[] _amountIcons =
        {
            App.Current.FindResource("AmountIcon") as ImageSource ?? throw new InvalidOperationException("DateIcon resource not found"),
            App.Current.FindResource("AmountSelectedIcon") as ImageSource ?? throw new InvalidOperationException("DateIcon resource not found")
        };

        private readonly static ImageSource[] _alphAscIcons =
        {
            App.Current.FindResource("AlphAscIcon") as ImageSource ?? throw new InvalidOperationException("DateIcon resource not found"),
            App.Current.FindResource("AlphAscSelectedIcon") as ImageSource ?? throw new InvalidOperationException("DateIcon resource not found")
        };

        private readonly static ImageSource[] _alphDescIcons =
        {
            App.Current.FindResource("AlphDescIcon") as ImageSource ?? throw new InvalidOperationException("DateIcon resource not found"),
            App.Current.FindResource("AlphDescSelectedIcon") as ImageSource ?? throw new InvalidOperationException("DateIcon resource not found")
        };


        public SortOptions(
            string property,
            string name,
            SortDirection direction,
            ImageSource[] icon )
        {
            Property = property;
            Name = name;
            Direction = direction;
            Icon = icon;
        }


        public static List<SortOptions> TransactionOptions()
        {
            return new List<SortOptions>
            {
                new(property: "ProcessedDate", name: "Oldest First", direction: SortDirection.Ascending, _dateIcons),
                new(property: "ProcessedDate", name: "Newest First", direction: SortDirection.Descending, _dateIcons),
                new(property: "Amount", name: "Amount (Low)", direction: SortDirection.Ascending, _amountIcons),
                new(property: "Amount", name: "Amount (High)", direction: SortDirection.Descending, _amountIcons),
                new(property: "Name", name: "Name (A-Z)", direction: SortDirection.Ascending, _alphAscIcons),
                new(property: "Name", name: "Name (Z-A)", direction: SortDirection.Descending, _alphDescIcons),
            };
        }


        public static List<SortOptions> SubscriptionOptions()
        {
            return new List<SortOptions>
            {
                new(property: "DueDate", name: "Oldest First", direction: SortDirection.Ascending, _dateIcons),
                new(property: "DueDate", name: "Newest First", direction: SortDirection.Descending, _dateIcons),
                new(property: "Amount", name: "Amount (Low)", direction: SortDirection.Ascending, _amountIcons),
                new(property: "Amount", name: "Amount (High)", direction: SortDirection.Descending, _amountIcons),
                new(property: "Plan", name: "Name (A-Z)", direction: SortDirection.Ascending, _alphAscIcons),
                new(property: "Plan", name: "Name (Z-A)", direction: SortDirection.Descending, _alphDescIcons),
            };
        }
    }
}
