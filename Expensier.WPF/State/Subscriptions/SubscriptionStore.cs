using Expensier.Domain.Models;
using Expensier.WPF.State.Accounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Expensier.WPF.State.Subscriptions
{
    public class SubscriptionStore
    {
        private readonly AccountStore _accountStore;

        public IEnumerable<Subscription> SubscriptionList => _accountStore.CurrentAccount?.SubscriptionList ?? new List<Subscription>();

        public event Action StateChanged;

        public SubscriptionStore(AccountStore accountStore)
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
