using Expensier.WPF.State.Expenses;
using Expensier.WPF.ViewModels.Expenses;
using LiveCharts;
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using Microsoft.IdentityModel.Tokens;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Expensier.WPF.ViewModels.Charts
{
    public class MonthlyExpensesViewModel : ViewModelBase
    {
        private readonly TransactionStore _transactionStore;
        public TransactionViewModel TransactionViewModel { get; }
        private readonly IEnumerable<TransactionDataModel> _transactions;
        public IEnumerable<TransactionDataModel> Transactions => _transactions;


        private bool _listEmpty;
        public bool ListEmpty
        {
            get
            {
                return _listEmpty;
            }
            set
            {
                _listEmpty = value;
                OnPropertyChanged( nameof( ListEmpty ) );
            }
        }

        private bool _listNotEmpty;
        public bool ListNotEmpty
        {
            get
            {
                return _listNotEmpty;
            }
            set
            {
                _listNotEmpty = value;
                OnPropertyChanged( nameof( ListNotEmpty ) );
            }
        }


        public ISeries[] Series { get; set; } = new ISeries[]
        {
            new ColumnSeries<double>
            {
                IsHoverable = false,
                Values = new ObservableCollection<double> {1500, 1500, 1500, 1500, 1500, 1500},
                Stroke = null,
                Fill = new SolidColorPaint(SKColor.Parse("#1F1D1F")),
                Padding = 18,
                Rx = 8,
                Ry = 8,
                IgnoresBarPosition = true,
            },
            new ColumnSeries<double>
            {
                Values = new ObservableCollection<double>(),
                Stroke = new SolidColorPaint(SKColor.Parse("#64927C")) { StrokeThickness = 2 },
                Fill = new SolidColorPaint(SKColor.Parse("#64927C")),
                Padding = 18,
                Rx = 8,
                Ry = 8,
                IgnoresBarPosition = true,
                YToolTipLabelFormatter = (chartPoint) => $"{chartPoint.Coordinate.PrimaryValue:C2}"
            }
        };
        public Axis[] XAxis { get; set; } = new Axis[]
        {
            new Axis
            {
                TextSize = 13,
                LabelsPaint = new SolidColorPaint(SKColors.LightGray)
            }
        };
        public Axis[] YAxis { get; set; } = new Axis[]
        {
            new Axis
            {
                ShowSeparatorLines = false,
                LabelsPaint = new SolidColorPaint(SKColor.Parse("#1B191B"))
            }
        };
        public LiveChartsCore.Measure.Margin Margin { get; set; } = new LiveChartsCore.Measure.Margin( 0, -40, 0, 0 );


        public MonthlyExpensesViewModel( TransactionStore transactionStore )
        {
            _transactions = new ObservableCollection<TransactionDataModel>();

            _transactionStore = transactionStore;
            TransactionViewModel = new TransactionViewModel( transactionStore,
                transactions => transactions
                .OrderBy( t => t.ProcessDate )
                .Where( t => t.IsCredit == true ) );

            _transactions = TransactionViewModel.Transactions;

            if ( _transactions.IsNullOrEmpty() )
            {
                _listEmpty = true;
                _listNotEmpty = false;
            }
            else
            {
                _listEmpty = false;
                _listNotEmpty = true;
            }

            GetLastSixMonthsExpenses( _transactions );
        }


        private void GetLastSixMonthsExpenses( IEnumerable<TransactionDataModel> transactions )
        {
            var sixMonthsAgo = DateTime.Now.AddMonths( -6 );

            var lastSixMonths = Enumerable.Range( 0, 6 )
                .Select( i => DateTime.Now.AddMonths( -i ) )
                .OrderBy( date => date );


            var filteredTransactions = lastSixMonths
                .GroupJoin( transactions,
                    date => new { date.Year, date.Month },
                    transaction => new { transaction.ProcessDate.Year, transaction.ProcessDate.Month },
                    ( date, transactionGroup ) => new
                    {
                        MonthName = CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedMonthName(date.Month),
                        Amount = transactionGroup.Any() ? transactionGroup.Sum( t => t.Amount ) : 0
                    } )
                .Select( data => new ChartDataModel( data.MonthName, data.Amount ) );

            ConstructSeries( filteredTransactions );
        }

        private void ConstructSeries( IEnumerable<ChartDataModel> transactions )
        {
            Series[1].Values = new ObservableCollection<double>( transactions.Select( t => t.TotalAmount ));
            ConstructXAxis( transactions );
        }

        private void ConstructXAxis( IEnumerable<ChartDataModel> transactions )
        {
            XAxis[0].Labels = transactions.Select( t => t.Label ).ToArray();
        }
    }
}
