using Expensier.Domain.Models;
using Expensier.WPF.State.Accounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Expensier.WPF.State.Portfolio
{
    public class PortfolioStore
    {
        private readonly AccountStore _accountStore;

        public IEnumerable<PortfolioReturn> Returns => _accountStore.CurrentAccount?.PortfolioReturn ?? new List<PortfolioReturn>();

        public event Action StateChanged;

        public PortfolioStore(AccountStore accountStore)
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
