using Expensier.Domain.Models;
using Expensier.Domain.Services.Transactions;
using Expensier.WPF.State.Accounts;
using Expensier.WPF.State.Expenses;
using Expensier.WPF.State.Navigators;
using Microsoft.IdentityModel.Tokens;
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
        private readonly ITransactionService _transactionService;
        private readonly AccountStore _accountStore;
        private readonly IRenavigator _renavigator;
        private readonly TransactionStore _transactionStore;
        private readonly Func<IEnumerable<TransactionDataModel>, IEnumerable<TransactionDataModel>> _filterTransaction;
        private readonly ObservableCollection<TransactionDataModel> _transactions;

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

        public IEnumerable<TransactionDataModel> Transactions => _transactions;

        public TransactionViewModel(
            TransactionStore transactionStore, 
            ITransactionService transactionService,
            AccountStore accountStore,
            IRenavigator renavigator) : this(transactionStore, transactions => transactions, transactionService, accountStore, renavigator) { }

        public TransactionViewModel(
            TransactionStore transactionStore, 
            Func<IEnumerable<TransactionDataModel>, IEnumerable<TransactionDataModel>> filterTransactions, 
            ITransactionService transactionService, 
            AccountStore accountStore,
            IRenavigator renavigator)
        {
            _transactionStore = transactionStore;
            _transactionService = transactionService;
            _accountStore = accountStore;
            _renavigator = renavigator;
            _filterTransaction = filterTransactions;
            _transactions = new ObservableCollection<TransactionDataModel>();

            _transactionStore.StateChanged += Transaction_StateChanged;

            ResetTransactions();
        }

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
                .Select(t => new TransactionDataModel(t.ID, t.TransactionName, t.ProcessDate, t.Amount, t.TransactionType, t.IsCredit, _transactionService, _accountStore, _renavigator))
                .OrderByDescending(o => o.ProcessDate);

            transactionDataModel = _filterTransaction(transactionDataModel);

            _transactions.Clear();
            foreach (TransactionDataModel dataModel in transactionDataModel)
            {
                _transactions.Add(dataModel);
            }

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
        }

        private void Transaction_StateChanged()
        {
            ResetTransactions();
        }
    }
}
