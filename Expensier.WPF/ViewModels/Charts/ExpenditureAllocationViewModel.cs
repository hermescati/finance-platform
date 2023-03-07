using Expensier.WPF.State.Expenses;
using Expensier.WPF.ViewModels.Expenses;
using LiveCharts;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using System.Windows.Media;
using static Expensier.WPF.ViewModels.Charts.ChartDropdownValues;

namespace Expensier.WPF.ViewModels.Charts
{
    public class ExpenditureAllocationViewModel : ViewModelBase
    {
        private readonly TransactionStore _transactionStore;
        private readonly IEnumerable<TransactionDataModel> _transactions;
        public TransactionViewModel TransactionViewModel { get; }
        public IEnumerable<TransactionDataModel> Transactions => _transactions;

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
                OnPropertyChanged(nameof(SelectedItem));
            }
        }

        public IEnumerable<ChartFrequency> Frequency => Enum.GetValues(typeof(ChartFrequency)).Cast<ChartFrequency>();
        public SeriesCollection Series { get; }
        public ColorsCollection SeriesColors { get; }

        public ExpenditureAllocationViewModel(TransactionStore transactionStore)
        {
            _transactions = new ObservableCollection<TransactionDataModel>();

            Series = new SeriesCollection();
            SeriesColors = new ColorsCollection();

            _transactionStore = transactionStore;
            TransactionViewModel = new TransactionViewModel(transactionStore,
                transactions => transactions
                .OrderBy(t => t.ProcessDate)
                .Where(t => t.IsCredit == true));

            _transactions = TransactionViewModel.Transactions;
            GetMonthlyExpenditures(_transactions);

            PropertyChanged += (sender, e) =>
            {
                if (e.PropertyName == nameof(SelectedItem))
                {
                    if (SelectedItem == ChartFrequency.Yearly)
                    {
                        GetAnnualExpenditures(_transactions);
                    }
                    else
                    {
                        GetMonthlyExpenditures(_transactions);
                    }
                }
            };
        }

        private void GetMonthlyExpenditures(IEnumerable<TransactionDataModel> transactions)
        {
            transactions = transactions.Where(t => t.ProcessDate.Month == DateTime.Now.Month && t.ProcessDate.Year == DateTime.Now.Year);
            ConstructChart(transactions);
        }

        private void GetAnnualExpenditures(IEnumerable<TransactionDataModel> transactions)
        {
            transactions = transactions.Where(t => t.ProcessDate.Year == DateTime.Now.Year);
            ConstructChart(transactions);
        }

        private void ConstructChart(IEnumerable<TransactionDataModel> transactions)
        {
            Series.Clear();
            Series.AddRange(transactions
                .GroupBy(t => t.TransactionType)
                .Select(g => new PieSeries
                {
                    Title = g.Key.ToString(),
                    Values = new ChartValues<double>(new[]
                    {
                        g.Sum(t => t.Amount)
                    }),
                    Stroke = new SolidColorBrush(Color.FromArgb(255, 27, 25, 27)),
                    StrokeThickness = 8
                }));

            AddChartColors();
        }

        private void AddChartColors()
        {
            SeriesColors.Clear();
            SeriesColors.AddRange(new[] { "#FFA7DDBC", "#FF64927C", "#FF497F76", "#FF255F5B", "#FF3C5549", "#FF2C3E35" }
                      .Select(ColorConverter.ConvertFromString)
                      .OfType<Color>()
                      .ToList());
        }
    }
}
