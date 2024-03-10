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

namespace Expensier.WPF.ViewModels.Charts
{
    public class ExpenditureAllocationViewModel : ViewModelBase
    {
        private readonly TransactionStore _transactionStore;
        private readonly IEnumerable<TransactionDataModel> _transactions;
        private readonly ObservableCollection<ISeries> _series;


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


        public IEnumerable<ISeries> Series => _series;
        public string[] ChartColors = new string[]
        {
            "#FFA7DDBC",
            "#FF64927C",
            "#FF497F76",
            "#FF255F5B",
            "#FF3C5549",
            "#FF2C3E35"
        };
        public SolidColorPaint LegendTextPaint { get; set; } = new SolidColorPaint( SKColors.WhiteSmoke );


        public TransactionViewModel TransactionViewModel { get; }
        public IEnumerable<TransactionDataModel> Transactions => _transactions;


        public ExpenditureAllocationViewModel( TransactionStore transactionStore )
        {
            _transactions = new ObservableCollection<TransactionDataModel>();
            _series = new ObservableCollection<ISeries>();

            _transactionStore = transactionStore;
            TransactionViewModel = new TransactionViewModel( transactionStore,
                transactions => transactions
                .OrderBy( t => t.ProcessDate )
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
            transactions = transactions.Where( t => t.ProcessDate.Month == DateTime.Now.Month && t.ProcessDate.Year == DateTime.Now.Year );
            ConstructChart( transactions );
        }

        private void GetAnnualExpenditures( IEnumerable<TransactionDataModel> transactions )
        {
            transactions = transactions.Where( t => t.ProcessDate.Year == DateTime.Now.Year );
            ConstructChart( transactions );
        }

        private void ConstructChart( IEnumerable<TransactionDataModel> transactions )
        {
            _series.Clear();

            var grouppedTransactions = transactions
                .GroupBy( t => t.TransactionType )
                .Select( g => new ChartDataModel( g.Key, g.Sum( t => t.Amount ) ) ).ToList();

            foreach ( var group in grouppedTransactions )
            {
                var index = grouppedTransactions.IndexOf(group );

                ISeries pieSeries = new PieSeries<double>
                {
                    Name = group.Label,
                    Values = new ObservableCollection<double> { group.TotalAmount },
                    Fill = new SolidColorPaint( SKColor.Parse( ChartColors[index] ) ),
                    OuterRadiusOffset = 0,
                    MaxRadialColumnWidth = 56,
                    ToolTipLabelFormatter = ( chartPoint ) => $"{group.TotalAmount:C2}",
                };

                _series.Add( pieSeries );
            }
        }
    }
}
