using Expensier.Domain.Services.Transactions;
using Expensier.WPF.State.Expenses;
using Expensier.WPF.State.Subscriptions;
using Expensier.WPF.ViewModels.Expenses;
using Expensier.WPF.ViewModels.Subscriptions;
using LiveCharts;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using static Expensier.WPF.ViewModels.Charts.ChartDropdownValues;
using LiveChartsCore.SkiaSharpView.Painting;
using SkiaSharp;
using System.Windows.Media;
using LiveChartsCore.Drawing;
using LiveChartsCore.SkiaSharpView.Painting.Effects;
using Expensier.Domain.Models;

namespace Expensier.WPF.ViewModels.Charts
{
    public class SpendingSummaryViewModel : ViewModelBase
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


        private ChartFrequency _selectedItem;
        public ChartFrequency SelectedItem
        {
            get
            {
                return _selectedItem;
            }
            set
            {
                _selectedItem = value;
                OnPropertyChanged( nameof( SelectedItem ) );
            }
        }
        public IEnumerable<ChartFrequency> Frequency => Enum.GetValues( typeof( ChartFrequency ) ).Cast<ChartFrequency>();

        public ISeries[] Series { get; set; } = new ISeries[]
        {
            new LineSeries<double>
            {
                Values = new ObservableCollection<double> { },
                Stroke = new SolidColorPaint(SKColor.Parse("#64927C")) { StrokeThickness = 2 },
                GeometryFill = new SolidColorPaint(SKColor.Parse("#64927C")),
                GeometryStroke = new SolidColorPaint(SKColor.Parse("#64927C")) {StrokeThickness = 2 },
                Fill = new LinearGradientPaint(
                    new[] { new SKColor( 100, 146, 124, 75 ), new SKColor( 100, 146, 124, 0 ) },
                    new SKPoint( 0.25f, 0 ),
                    new SKPoint( 0.25f, 1 )),
                GeometrySize = 4,
                LineSmoothness = 1,
                DataPadding = new LvcPoint(0, 0.5f),
                EnableNullSplitting = false,
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
                Labeler = Labelers.Currency,
                TextSize = 13,
                LabelsPaint = new SolidColorPaint( SKColors.LightGray),
                SeparatorsPaint = new SolidColorPaint( SKColors.White.WithAlpha(100))
                {
                    StrokeThickness = 0.5f,
                    PathEffect = new DashEffect(new float[] {4, 4})
                }
            }
        };

        public SpendingSummaryViewModel( TransactionStore transactionStore )
        {
            _transactions = new ObservableCollection<TransactionDataModel>();

            _transactionStore = transactionStore;
            TransactionViewModel = new TransactionViewModel( transactionStore,
                transactions => transactions
                .OrderBy( t => t.ProcessDate )
                .Where( t => t.IsCredit == true ) );

            _transactions = TransactionViewModel.Transactions;
            GetMonthlyTransactions( _transactions );

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

            PropertyChanged += ( sender, e ) =>
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
            };

        }


        private void GetMonthlyTransactions( IEnumerable<TransactionDataModel> transactions )
        {
            var filteredTransactions = transactions
                .Where( t => t.ProcessDate.Month == DateTime.Now.Month && t.ProcessDate.Year == DateTime.Now.Year )
                .GroupBy( t => t.ProcessDate.Date )
                .Select( g => new ChartDataModel( g.Key.ToString( "ddd, d" ), g.Sum( t => t.Amount ) ) );

            ConstructSeries( filteredTransactions );
        }

        private void GetAnnualTransactions( IEnumerable<TransactionDataModel> transactions )
        {
            var filteredTransactions = transactions
                .Where( t => t.ProcessDate.Year == DateTime.Now.Year )
                .GroupBy( t => t.ProcessDate.Month )
                .Select( g => new ChartDataModel( CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedMonthName( g.Key ), g.Sum( t => t.Amount ) ) );

            ConstructSeries( filteredTransactions );
        }

        private void ConstructSeries( IEnumerable<ChartDataModel> transactions )
        {
            Series[0].Values = new ObservableCollection<double>( transactions.Select( t => t.TotalAmount ) );
            ConstructXAxis( transactions );
        }

        private void ConstructXAxis( IEnumerable<ChartDataModel> transactions )
        {
            XAxis[0].Labels = transactions.Select( t => t.DateLabel ).ToArray();
        }
    }
}
