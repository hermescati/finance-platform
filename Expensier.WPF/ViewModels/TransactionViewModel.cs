using Expensier.WPF.State.Expenses;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Expensier.WPF.ViewModels
{
    public class TransactionViewModel : ViewModelBase
    {
        private readonly TransactionStore _transactionStore;
        private readonly ObservableCollection<TransactionDataModel> _transactions;

        public IEnumerable<TransactionDataModel> Transactions => _transactions;


        public TransactionViewModel(TransactionStore transactionStore)
        {
            _transactionStore = transactionStore;

            _transactions = new ObservableCollection<TransactionDataModel>();
            _transactionStore.StateChanged += Transaction_StateChanged;

            ResetTransactions();
        }

        private void ResetTransactions()
        {
            IEnumerable<TransactionDataModel> transactionViewModels = _transactionStore.TransactionList
                .Select(t => new TransactionDataModel(t.Transaction_Name, t.Process_Date, t.Amount, t.Is_Credit))
                .OrderBy(o => o.ProcessDate);

            _transactions.Clear();
            foreach (TransactionDataModel dataModel in transactionViewModels)
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
