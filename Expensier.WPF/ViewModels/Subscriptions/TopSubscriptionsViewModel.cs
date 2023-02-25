using Expensier.WPF.State.Subscriptions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Expensier.WPF.ViewModels.Subscriptions
{
    public class TopSubscriptionsViewModel : ViewModelBase
    {
        private readonly SubscriptionStore _subscriptionStore;
        public SubscriptionViewModel SubscriptionViewModel { get; }
        private readonly ObservableCollection<SubscriptionDataModel> _subscriptions;

        public IEnumerable<SubscriptionDataModel> Subscriptions => _subscriptions;

        public TopSubscriptionsViewModel(SubscriptionStore subscriptionStore)
        {
            _subscriptions= new ObservableCollection<SubscriptionDataModel>();
            _subscriptionStore = subscriptionStore;
            SubscriptionViewModel = new SubscriptionViewModel(subscriptionStore, subscriptions => subscriptions.OrderByDescending(s => s.DueDate).Take(3));

            _subscriptions = (ObservableCollection<SubscriptionDataModel>)SubscriptionViewModel.Subscriptions;
        }
    }
}
