using Expensier.WPF.State.Expenses;
using Expensier.WPF.ViewModels.Expenses;
using LiveCharts;
using LiveCharts.Wpf;
using Microsoft.IdentityModel.Tokens;
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
        private readonly IEnumerable<TransactionDataModel> _transactions;
        public TransactionViewModel TransactionViewModel { get; }
        public IEnumerable<TransactionDataModel> Transactions => _transactions;

        private bool _listEmpty;
        public  bool ListEmpty
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

        public ObservableCollection<string> xAxis { get; }

        public MonthlyExpensesViewModel(TransactionStore transactionStore)
        {
            _transactions = new ObservableCollection<TransactionDataModel>();
            xAxis = new ObservableCollection<string>();

            _transactionStore = transactionStore;
            TransactionViewModel = new TransactionViewModel(transactionStore,
                transactions => transactions
                .OrderBy(t => t.ProcessDate)
                .Where(t => t.IsCredit == true));

            _transactions = TransactionViewModel.Transactions;

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

            ConstructChart(_transactions);
        }

        private void ConstructChart(IEnumerable<TransactionDataModel> transactions)
        {
            transactions = transactions
                .Where(t => t.ProcessDate.Year == DateTime.Now.Year)
                .GroupBy(t => t.ProcessDate.Month)
                .Select(g => new TransactionDataModel(g.Key.ToString(CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedMonthName(g.Key)), g.Sum(t => t.Amount)));

            ChartSeries = new ChartValues<double>(transactions.Select(t => t.Amount));

            xAxis.Clear();
            foreach (string record in transactions.Select(t => t.DateFormat))
            {
                xAxis.Add(record);
            }
        }
    }
}
    