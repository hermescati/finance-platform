using Expensier.Domain.Services.Transactions;
using Expensier.WPF.State.Expenses;
using Expensier.WPF.State.Subscriptions;
using Expensier.WPF.ViewModels.Expenses;
using Expensier.WPF.ViewModels.Subscriptions;
using LiveCharts;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Expensier.WPF.ViewModels.Charts
{
    public class SpendingSummaryViewModel : ViewModelBase
    {
        private readonly TransactionStore _transactionStore;
        private readonly IEnumerable<TransactionDataModel> _transactions;
        public TransactionViewModel TransactionViewModel { get; }
        public IEnumerable<TransactionDataModel> Transactions => _transactions;

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

        public SpendingSummaryViewModel(TransactionStore transactionStore)
        {
            _transactions = new ObservableCollection<TransactionDataModel>();
            xAxis = new ObservableCollection<string>();

            _transactionStore = transactionStore;
            TransactionViewModel = new TransactionViewModel(transactionStore,
                transactions => transactions
                .OrderBy(t => t.ProcessDate)
                .Where(t => t.IsCredit == true)
                .GroupBy(t => t.ProcessDate.Date)
                .Select(g => new TransactionDataModel(g.Key, g.Sum(t => t.Amount))));

            _transactions = TransactionViewModel.Transactions;
            string dateFormat = "ddd";

            ChartSeries = new ChartValues<double>(_transactions.Select(a => a.Amount));

            xAxis.Clear();
            ConstructAxis(_transactions, dateFormat);
        }

        public void ConstructSeries(IEnumerable<TransactionDataModel> transactions)
        {
            ChartSeries = new ChartValues<double>(transactions.Select(t => t.Amount));
        }

        public void ConstructAxis(IEnumerable<TransactionDataModel> transactions, string dateFormat)
        {
            foreach (DateTime date in transactions.Select(t => t.ProcessDate))
            {
                xAxis.Add(date.ToString(dateFormat));
            }
        }

    }
}
