using Expensier.Domain.Models;
using Expensier.WPF.State.Accounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Expensier.WPF.State.Expenses
{
    public class TransactionStore
    {
        private readonly AccountStore _accountStore;

        public IEnumerable<Transaction> TransactionList => _accountStore.CurrentAccount?.TransactionList ?? new List<Transaction>();

        public event Action StateChanged;

        public TransactionStore(AccountStore accountStore)
        {
            _accountStore = accountStore;

            _accountStore.StateChanged += OnStateChanged;
        }

        private void OnStateChanged()
        {
            StateChanged?.Invoke();
        }
    }
}
