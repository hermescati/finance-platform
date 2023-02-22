using Expensier.WPF.State;
using Expensier.WPF.State.Expenses;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Expensier.WPF.ViewModels
{
    public class SubscriptionViewModel : ViewModelBase
    {
        private readonly SubscriptionStore _subscriptionStore;
        private readonly ObservableCollection<SubscriptionModel> _subscriptions;

        public IEnumerable<SubscriptionModel> Subscriptions => _subscriptions;


        public SubscriptionViewModel(SubscriptionStore subscriptionStore)
        {
            _subscriptionStore = subscriptionStore;

            _subscriptions = new ObservableCollection<SubscriptionModel>();
            _subscriptionStore.StateChanged += Subscription_StateChanged;

            ResetSubscriptions();
        }

        private void ResetSubscriptions()
        {
            IEnumerable<SubscriptionModel> subscriptionViewModels = _subscriptionStore.SubscriptionList
                .Select(s => new SubscriptionModel(s.Subscription_Plan, s.Due_Date, s.Amount));

            _subscriptions.Clear();
            foreach (SubscriptionModel dataModel in subscriptionViewModels)
            {
                _subscriptions.Add(dataModel);
            }
        }

        private void Subscription_StateChanged()
        {
            ResetSubscriptions();
        }
    }
}
