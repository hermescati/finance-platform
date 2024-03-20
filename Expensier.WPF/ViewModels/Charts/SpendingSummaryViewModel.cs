using Expensier.WPF.State.Expenses;
using Expensier.WPF.ViewModels.Expenses;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using Expensier.WPF.Utils;
using Expensier.WPF.DataObjects;
using Expensier.WPF.Enums;
using System.ComponentModel;


namespace Expensier.WPF.ViewModels.Charts
{
    public class SpendingSummaryViewModel : ViewModelBase
    {
        private readonly TransactionStore _transactionStore;
        public TransactionViewModel TransactionViewModel { get; }


        private readonly IEnumerable<TransactionModel> _transactions;
        public IEnumerable<TransactionModel> Transactions => _transactions;


        private ChartFrequency _selectedItem;
        public ChartFrequency SelectedItem
        {
            get => _selectedItem;
            set
            {
                _selectedItem = value;
                OnPropertyChanged( nameof( SelectedItem ) );
            }
        }
        public IEnumerable<ChartFrequency> Frequency => Enum.GetValues( typeof( ChartFrequency ) )
            .Cast<ChartFrequency>();


        public ISeries[] Series { get; set; } = new ISeries[] { ChartSettings.DefaultLineSeries() };
        public Axis[] XAxis { get; set; } = new Axis[] { ChartSettings.DefaultXAxis() };
        public Axis[] YAxis { get; set; } = new Axis[] { ChartSettings.DefaultYAxis() };


        public SpendingSummaryViewModel( TransactionStore transactionStore )
        {
            _transactionStore = transactionStore;
            _transactions = new ObservableCollection<TransactionModel>();

            TransactionViewModel = new TransactionViewModel( transactionStore, transactions => transactions );

            _transactions = TransactionViewModel.Transactions
                .Where( t => t.IsCredit )
                .OrderBy( t => t.ProcessedDate );

            GetMonthlyTransactions( _transactions );

            PropertyChanged += PropertyChangedEventHandler;
        }


        private void GetMonthlyTransactions( IEnumerable<TransactionModel> transactions )
        {
            IEnumerable<ChartModel> grouppedTransactions = transactions
                .Where( t => t.ProcessedDate.Month == DateTime.Now.Month && t.ProcessedDate.Year == DateTime.Now.Year )
                .GroupBy( t => t.ProcessedDate.Date )
                .Select( g => new ChartModel( g.Key.ToString( "ddd, d" ), g.Sum( t => t.Amount ) ) );

            ConstructSeries( grouppedTransactions );
            ConstructXAxis( grouppedTransactions );
        }


        private void GetAnnualTransactions( IEnumerable<TransactionModel> transactions )
        {
            IEnumerable<ChartModel> grouppedTransactions = transactions
                .Where( t => t.ProcessedDate.Year == DateTime.Now.Year )
                .GroupBy( t => t.ProcessedDate.Month )
                .Select( g => new ChartModel( CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedMonthName( g.Key ), g.Sum( t => t.Amount ) ) );

            ConstructSeries( grouppedTransactions );
            ConstructXAxis( grouppedTransactions );
        }


        private void ConstructSeries( IEnumerable<ChartModel> transactions )
        {
            Series[0].Values = transactions.Select( t => t.SeriesValue );
        }


        private void ConstructXAxis( IEnumerable<ChartModel> transactions )
        {
            XAxis[0].Labels = transactions.Select( t => t.SeriesLabel ).ToArray();
        }


        private void PropertyChangedEventHandler( object sender, PropertyChangedEventArgs e )
        {
            if ( e.PropertyName == nameof( SelectedItem ) )
            {
                if ( SelectedItem == ChartFrequency.Yearly )
                {
                    GetAnnualTransactions( _transactions );
                }
                else
                {
                    GetMonthlyTransactions( _transactions );
                }
            }
        }
    }
}
