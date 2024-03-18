using Expensier.WPF.DataObjects;
using Expensier.WPF.State.Subscriptions;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using static Expensier.Domain.Models.Subscription;


namespace Expensier.WPF.ViewModels.Subscriptions
{
    public class TopSubscriptionsViewModel : ViewModelBase
    {
        private readonly SubscriptionStore _subscriptionStore;
        public SubscriptionsViewModel SubscriptionViewModel { get; }


        private readonly ObservableCollection<SubscriptionModel> _subscriptions;
        public IEnumerable<SubscriptionModel> Subscriptions => _subscriptions;


        public TopSubscriptionsViewModel( SubscriptionStore subscriptionStore )
        {
            _subscriptionStore = subscriptionStore;
            _subscriptions = new ObservableCollection<SubscriptionModel>();

            SubscriptionViewModel = new SubscriptionsViewModel( 
                subscriptionStore,
                subscriptions => subscriptions.Where( s => s.Status == SubscriptionStatus.Active )
                .OrderBy( s => s.DueDate )
                .Take( 3 ) );

            _subscriptions = (ObservableCollection<SubscriptionModel>) SubscriptionViewModel.Subscriptions;
        }
    }
}
