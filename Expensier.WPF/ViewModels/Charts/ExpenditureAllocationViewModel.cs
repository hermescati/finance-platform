using Expensier.WPF.State.Expenses;
using Expensier.WPF.ViewModels.Expenses;
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView.Painting;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using LiveChartsCore.SkiaSharpView;
using System.Windows.Media;
using Expensier.WPF.Utils;
using Expensier.WPF.DataObjects;
using Expensier.WPF.Enums;
using System.ComponentModel;


namespace Expensier.WPF.ViewModels.Charts
{
    public class ExpenditureAllocationViewModel : ViewModelBase
    {
        private readonly TransactionStore _transactionStore;
        public TransactionViewModel TransactionViewModel { get; }


        private readonly IEnumerable<TransactionModel> _transactions;
        public IEnumerable<TransactionModel> Transactions => _transactions;

        private readonly ObservableCollection<ISeries> _series;
        public IEnumerable<ISeries> Series => _series;

        private readonly ObservableCollection<ChartModel> _legend;
        public IEnumerable<ChartModel> Legend => _legend;


        private double _totalExpenditure;
        public double TotalExpenditure
        {
            get => _totalExpenditure;
            set
            {
                _totalExpenditure = value;
                OnPropertyChanged( nameof( TotalExpenditure ) );
            }
        }


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


        public ExpenditureAllocationViewModel( TransactionStore transactionStore )
        {
            _transactionStore = transactionStore;
            _transactions = new ObservableCollection<TransactionModel>();
            _series = new ObservableCollection<ISeries>();
            _legend = new ObservableCollection<ChartModel>();

            TransactionViewModel = new TransactionViewModel( transactionStore, transactions => transactions );

            _transactions = TransactionViewModel.Transactions
                .Where( t => t.IsCredit )
                .OrderBy( t => t.ProcessedDate );

            GetMonthlyExpenditures( _transactions );

            PropertyChanged += PropertyChangedEventHandler;
        }


        private void GetMonthlyExpenditures( IEnumerable<TransactionModel> transactions )
        {
            transactions = transactions
                .Where( t => t.ProcessedDate.Month == DateTime.Now.Month && t.ProcessedDate.Year == DateTime.Now.Year );

            ConstructChart( transactions );
        }


        private void GetAnnualExpenditures( IEnumerable<TransactionModel> transactions )
        {
            transactions = transactions.Where( t => t.ProcessedDate.Year == DateTime.Now.Year );

            ConstructChart( transactions );
        }


        private void ConstructChart( IEnumerable<TransactionModel> transactions )
        {
            _series.Clear();

            List<ChartModel> grouppedTransactions = transactions
                .GroupBy( t => t.Category )
                .Select( g => new ChartModel( g.Key.ToString(), g.Sum( t => t.Amount ) ) ).ToList();

            TotalExpenditure = grouppedTransactions.Sum( t => t.SeriesValue );

            foreach ( ChartModel group in grouppedTransactions )
            {
                int index = grouppedTransactions.IndexOf( group );

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

            foreach ( ISeries series in seriesCollection )
            {
                int index = seriesCollection.IndexOf( series );

                double totalValue = series.Values.Cast<double>().ToList()[0];
                SKColor seriesColor = ChartSettings.ApplyPieChartColor( index );

                ChartModel legendData = new ChartModel(
                    label: series.Name,
                    percentage: totalValue / TotalExpenditure,
                    color: new SolidColorBrush( Color.FromArgb( seriesColor.Alpha, seriesColor.Red, seriesColor.Green, seriesColor.Blue ) ) );

                _legend.Add( legendData );
            }
        }


        private void PropertyChangedEventHandler( object sender, PropertyChangedEventArgs e )
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
        }
    }
}
