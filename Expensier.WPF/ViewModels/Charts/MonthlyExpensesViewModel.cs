using Expensier.WPF.DataObjects;
using Expensier.WPF.State.Expenses;
using Expensier.WPF.Utils;
using Expensier.WPF.ViewModels.Expenses;
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;


namespace Expensier.WPF.ViewModels.Charts
{
    public class MonthlyExpensesViewModel : ViewModelBase
    {
        private readonly TransactionStore _transactionStore;
        public TransactionViewModel TransactionViewModel { get; }


        private readonly IEnumerable<TransactionModel> _transactions;
        public IEnumerable<TransactionModel> Transactions => _transactions;


        public ISeries[] Series { get; set; } = ChartSettings.DefaultColumnSeries();
        public Axis[] XAxis { get; set; } = new Axis[] { ChartSettings.DefaultXAxis() };
        public Axis[] YAxis { get; set; } = new Axis[] { ChartSettings.DefaultYAxis( showAxis: false ) };
        public LiveChartsCore.Measure.Margin Margin { get; set; } = ChartSettings.DrawMargin;


        public double TotalExpenditure { get; set; }


        public MonthlyExpensesViewModel( TransactionStore transactionStore )
        {
            _transactions = new ObservableCollection<TransactionModel>();

            _transactionStore = transactionStore;
            TransactionViewModel = new TransactionViewModel( transactionStore, transactions => transactions );

            double highestIncome = GetHighestIncomeValue();

            _transactions = TransactionViewModel.Transactions
                .Where( t => t.IsCredit )
                .OrderBy( t => t.ProcessedDate );

            TotalExpenditure = _transactions.Sum( t => t.Amount );

            GetLastSixMonthsExpenses( _transactions, highestIncome );
        }


        private double GetHighestIncomeValue()
        {
            IEnumerable<ChartDataModel> incomeTransactions = TransactionViewModel.Transactions
                .Where( t => !t.IsCredit )
                .OrderBy( t => t.ProcessedDate )
                .GroupBy( t => t.ProcessedDate.Month )
                .Select( g => new ChartDataModel( g.Key.ToString(), g.Sum( t => t.Amount ) ) );

            return incomeTransactions.Max( t => t.SeriesValue );
        }


        private void GetLastSixMonthsExpenses( IEnumerable<TransactionModel> transactions, double defaultIncome )
        {
            IEnumerable<DateTime> lastSixMonths = Enumerable.Range( 0, 6 )
                .Select( i => DateTime.Now.AddMonths( -i ) )
                .OrderBy( date => date );

            IEnumerable<ChartDataModel> filteredTransactions = lastSixMonths
                .GroupJoin( transactions,
                    date => new { date.Year, date.Month },
                    transaction => new { transaction.ProcessedDate.Year, transaction.ProcessedDate.Month },
                    ( date, transactionGroup ) => new
                    {
                        MonthName = CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedMonthName( date.Month ),
                        Amount = transactionGroup.Any() ? transactionGroup.Sum( t => t.Amount ) : 0
                    } )
                .Select( data => new ChartDataModel( data.MonthName, data.Amount ) );

            ConstructSeries( filteredTransactions, defaultIncome );
            ConstructXAxis( filteredTransactions );
        }


        private void ConstructSeries( IEnumerable<ChartDataModel> transactions, double defaultIncome )
        {
            Series[0].Values = Enumerable.Repeat( defaultIncome, 6 );
            Series[1].Values = transactions.Select( t => t.SeriesValue );
        }


        private void ConstructXAxis( IEnumerable<ChartDataModel> transactions )
        {
            XAxis[0].Labels = transactions.Select( t => t.SeriesLabel ).ToArray();
        }
    }
}
