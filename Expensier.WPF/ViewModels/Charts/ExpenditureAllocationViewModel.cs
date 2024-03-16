using Expensier.WPF.State.Expenses;
using Expensier.WPF.ViewModels.Expenses;
using LiveCharts;
using LiveCharts.Wpf;
using LiveChartsCore;
using LiveChartsCore.Drawing;
using LiveChartsCore.SkiaSharpView.Painting;
using Microsoft.IdentityModel.Tokens;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using System.Threading.Tasks;
using System.Transactions;
using System.Windows.Media;
using static Expensier.WPF.ViewModels.Charts.ChartDropdownValues;
using LiveChartsCore.SkiaSharpView.Extensions;
using Expensier.WPF.Utils;

namespace Expensier.WPF.ViewModels.Charts
{
    public class ExpenditureAllocationViewModel : ViewModelBase
    {
        private readonly TransactionStore _transactionStore;
        private readonly IEnumerable<TransactionDataModel> _transactions;
        private readonly ObservableCollection<ISeries> _series;
        private readonly ObservableCollection<ChartDataModel> _legend;


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


        private double _totalExpenditure;
        public double TotalExpenditure
        {
            get
            {
                return _totalExpenditure;
            }
            set
            {
                _totalExpenditure = value;
                OnPropertyChanged( nameof( TotalExpenditure ) );
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


        public IEnumerable<ISeries> Series => _series;
        public IEnumerable<ChartDataModel> Legend => _legend;
        public SolidColorPaint LegendTextPaint { get; set; } = new SolidColorPaint( SKColors.WhiteSmoke );


        public TransactionViewModel TransactionViewModel { get; }
        public IEnumerable<TransactionDataModel> Transactions => _transactions;


        public ExpenditureAllocationViewModel( TransactionStore transactionStore )
        {
            _transactions = new ObservableCollection<TransactionDataModel>();
            _series = new ObservableCollection<ISeries>();
            _legend = new ObservableCollection<ChartDataModel>();

            _transactionStore = transactionStore;
            TransactionViewModel = new TransactionViewModel( transactionStore,
                transactions => transactions
                .OrderBy( t => t.ProcessedDate )
                .Where( t => t.IsCredit == true ) );

            _transactions = TransactionViewModel.Transactions;
            GetMonthlyExpenditures( _transactions );

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
                        GetAnnualExpenditures( _transactions );
                    }
                    else
                    {
                        GetMonthlyExpenditures( _transactions );
                    }
                }
            };
        }


        private void GetMonthlyExpenditures( IEnumerable<TransactionDataModel> transactions )
        {
            transactions = transactions.Where( t => t.ProcessedDate.Month == DateTime.Now.Month && t.ProcessedDate.Year == DateTime.Now.Year );
            ConstructChart( transactions );
        }

        private void GetAnnualExpenditures( IEnumerable<TransactionDataModel> transactions )
        {
            transactions = transactions.Where( t => t.ProcessedDate.Year == DateTime.Now.Year );
            ConstructChart( transactions );
        }

        private void ConstructChart( IEnumerable<TransactionDataModel> transactions )
        {
            _series.Clear();

            var grouppedTransactions = transactions
                .GroupBy( t => t.Category )
                .Select( g => new ChartDataModel( g.Key.ToString(), g.Sum( t => t.Amount ) ) ).ToList();

            TotalExpenditure = grouppedTransactions.Sum( t => t.SeriesValue );

            foreach ( var group in grouppedTransactions )
            {
                var index = grouppedTransactions.IndexOf( group );

                ISeries pieSeries = new PieSeries<double>
                {
                    Name = group.SeriesLabel,
                    Values = new ObservableCollection<double> { group.SeriesValue },

                    Fill = new SolidColorPaint( ChartSettings.ApplyPieChartColor( index ) ),

                    OuterRadiusOffset = ChartSettings.outerRadiusOffset,
                    MaxRadialColumnWidth = ChartSettings.maxRadialColumnWidth,
                    ToolTipLabelFormatter = ( chartPoint ) => $"{group.SeriesValue:C2}",
                };

                _series.Add( pieSeries );
            }

            ConstructLegend( _series );
        }

        private void ConstructLegend( ObservableCollection<ISeries> seriesCollection )
        {
            _legend.Clear();

            foreach ( var series in seriesCollection )
            {
                var index = seriesCollection.IndexOf( series );

                double totalValue = series.Values.Cast<double>().ToList()[0];
                SKColor seriesColor = ChartSettings.ApplyPieChartColor( index );

                ChartDataModel legendData = new ChartDataModel(
                    label: series.Name,
                    percentage: totalValue / TotalExpenditure,
                    color: new SolidColorBrush( Color.FromArgb( seriesColor.Alpha, seriesColor.Red, seriesColor.Green, seriesColor.Blue ) ) );

                _legend.Add( legendData );
            }
        }
    }
}
