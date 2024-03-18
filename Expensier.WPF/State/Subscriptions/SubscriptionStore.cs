using Expensier.Domain.Models;
using Expensier.WPF.State.Accounts;
using System;
using System.Collections.Generic;


namespace Expensier.WPF.State.Subscriptions
{
    public class SubscriptionStore
    {
        private readonly AccountStore _accountStore;

        
        public event Action StateChanged;

        
        public IEnumerable<Subscription> SubscriptionList => _accountStore.CurrentAccount?.SubscriptionList ?? new List<Subscription>();


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
