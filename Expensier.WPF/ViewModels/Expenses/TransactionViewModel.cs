using Expensier.WPF.State.Expenses;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Expensier.WPF.ViewModels.Expenses
{
    public class TransactionViewModel : ViewModelBase
    {
        private readonly TransactionStore _transactionStore;
        private readonly Func<IEnumerable<TransactionDataModel>, IEnumerable<TransactionDataModel>> _filterTransaction;
        private readonly ObservableCollection<TransactionDataModel> _transactions;

        public IEnumerable<TransactionDataModel> Transactions => _transactions;

        public TransactionViewModel(TransactionStore transactionStore) : this(transactionStore, transactions => transactions) { }

        public TransactionViewModel(TransactionStore transactionStore, Func<IEnumerable<TransactionDataModel>, IEnumerable<TransactionDataModel>> filterTransactions)
        {
            _transactionStore = transactionStore;
            _filterTransaction = filterTransactions;
            _transactions = new ObservableCollection<TransactionDataModel>();

            _transactionStore.StateChanged += Transaction_StateChanged;

            ResetTransactions();
        }

        private void ResetTransactions()
        {
            IEnumerable<TransactionDataModel> transactionDataModel = _transactionStore.TransactionList
                .Select(t => new TransactionDataModel(t.Transaction_Name, t.Process_Date, t.Amount, t.Transaction_Type, t.Is_Credit))
                .OrderByDescending(o => o.ProcessDate);

            transactionDataModel = _filterTransaction(transactionDataModel);

            _transactions.Clear();
            foreach (TransactionDataModel dataModel in transactionDataModel)
            {
                _transactions.Add(dataModel);
            }
        }

        private void Transaction_StateChanged()
        {
            ResetTransactions();
        }
    }
}
