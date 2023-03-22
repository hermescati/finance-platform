using Expensier.Domain.Services.Transactions;
using Expensier.WPF.State.Expenses;
using Expensier.WPF.State.Subscriptions;
using Expensier.WPF.ViewModels.Expenses;
using Expensier.WPF.ViewModels.Subscriptions;
using LiveCharts;
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

namespace Expensier.WPF.ViewModels.Charts
{
    public class SpendingSummaryViewModel : ViewModelBase
    {
        private readonly TransactionStore _transactionStore;
        private readonly IEnumerable<TransactionDataModel> _transactions;
        public TransactionViewModel TransactionViewModel { get; }
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
                OnPropertyChanged(nameof(ListEmpty));
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
                OnPropertyChanged(nameof(ListNotEmpty));
            }
        }

        private ChartValues<double> _chartSeries;
        public ChartValues<double> ChartSeries
        {
            get
            {
                return _chartSeries;
            }
            set
            {
                _chartSeries = value;
                OnPropertyChanged(nameof(ChartSeries));
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
                OnPropertyChanged(nameof(SelectedItem));
            }
        }

        public IEnumerable<ChartFrequency> Frequency => Enum.GetValues(typeof(ChartFrequency)).Cast<ChartFrequency>();

        public ObservableCollection<string> xAxis { get; }

        public SpendingSummaryViewModel(TransactionStore transactionStore)
        {
            _transactions = new ObservableCollection<TransactionDataModel>();
            xAxis = new ObservableCollection<string>();

            _transactionStore = transactionStore;
            TransactionViewModel = new TransactionViewModel(transactionStore,
                transactions => transactions
                .OrderBy(t => t.ProcessDate)
                .Where(t => t.IsCredit == true));

            _transactions = TransactionViewModel.Transactions;
            GetMonthlyExpenses(_transactions);

            if (_transactions.IsNullOrEmpty())
            {
                _listEmpty = true;
                _listNotEmpty = false;
            }
            else
            {
                _listEmpty = false;
                _listNotEmpty = true;
            }

            PropertyChanged += (sender, e) =>
            {
                if (e.PropertyName == nameof(SelectedItem))
                {
                    if (SelectedItem == ChartFrequency.Yearly)
                    {
                        GetAnnualExpenses(_transactions);
                    }
                    else
                    {
                        GetMonthlyExpenses(_transactions);
                    }
                }
            };

        }

        private void GetMonthlyExpenses(IEnumerable<TransactionDataModel> transactions)
        {
            transactions = transactions
                .Where(t => t.ProcessDate.Month == DateTime.Now.Month && t.ProcessDate.Year == DateTime.Now.Year)
                .GroupBy(t => t.ProcessDate.Date)
                .Select(g => new TransactionDataModel(g.Key.ToString("ddd, d"), g.Sum(t => t.Amount)));
            ConstructSeries(transactions);
        }

        private void GetAnnualExpenses(IEnumerable<TransactionDataModel> transactions)
        {
            transactions = transactions
                .Where(t => t.ProcessDate.Year == DateTime.Now.Year)
                .GroupBy(t => t.ProcessDate.Month)
                .Select(g => new TransactionDataModel(g.Key.ToString(CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedMonthName(g.Key)), g.Sum(t => t.Amount)));
            ConstructSeries(transactions);
        }

        private void ConstructSeries(IEnumerable<TransactionDataModel> transactions)
        {
            ChartSeries = new ChartValues<double>(transactions.Select(t => t.Amount));
            ConstructAxis(transactions);
        }

        public void ConstructAxis(IEnumerable<TransactionDataModel> transactions)
        {
            xAxis.Clear();
            foreach (string record in transactions.Select(t => t.DateFormat))
            {
                xAxis.Add(record);
            }
        }

    }
}
