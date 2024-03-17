using Expensier.Domain.Services.Subscriptions;
using Expensier.WPF.DataObjects;
using Expensier.WPF.Enums;
using Expensier.WPF.State.Accounts;
using Expensier.WPF.State.Navigators;
using Expensier.WPF.State.Subscriptions;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using static Expensier.Domain.Models.Subscription;


namespace Expensier.WPF.ViewModels.Subscriptions
{
    public class SubscriptionsViewModel : ViewModelBase
    {
        private readonly AccountStore _accountStore;
        private readonly SubscriptionStore _subscriptionStore;
        private readonly ISubscriptionService _subscriptionService;
        private readonly IRenavigator _renavigator;
        private readonly Func<IEnumerable<SubscriptionModel>, IEnumerable<SubscriptionModel>> _filteredSubscriptions;


        private readonly ObservableCollection<SubscriptionModel> _subscriptions;
        public IEnumerable<SubscriptionModel> Subscriptions => _subscriptions;


        private bool _listEmpty;
        public bool ListEmpty
        {
            get => _listEmpty;
            set
            {
                _listEmpty = value;
                OnPropertyChanged( nameof( ListEmpty ) );
            }
        }

        private bool _showCancelled;
        public bool ShowCancelled
        {
            get => _showCancelled;
            set
            {
                _showCancelled = value;
                OnPropertyChanged( nameof( ShowCancelled ) );
            }
        }


        private SortOptions _selectedItem;
        public SortOptions SelectedItem
        {
            get => _selectedItem;
            set
            {
                _selectedItem = value;
                OnPropertyChanged( nameof( SelectedItem ) );
            }
        }
        public IEnumerable<SortOptions> Sort => Enum.GetValues( typeof( SortOptions ) )
            .Cast<SortOptions>();


        public SubscriptionsViewModel(
            AccountStore accountStore,
            SubscriptionStore subscriptionStore,
            ISubscriptionService subscriptionService,
            IRenavigator renavigator ) : this(
                accountStore,
                subscriptionStore,
                subscriptionService,
                renavigator,
                subscriptions => subscriptions )
        { }


        public SubscriptionsViewModel(
            AccountStore accountStore,
            SubscriptionStore subscriptionStore,
            ISubscriptionService subscriptionService,
            IRenavigator renavigator,
            Func<IEnumerable<SubscriptionModel>, IEnumerable<SubscriptionModel>> filteredSubscriptions )
        {
            _accountStore = accountStore;
            _subscriptionStore = subscriptionStore;
            _subscriptionService = subscriptionService;
            _renavigator = renavigator;
            _filteredSubscriptions = filteredSubscriptions;

            _subscriptions = new ObservableCollection<SubscriptionModel>();
            _subscriptionStore.StateChanged += Subscription_StateChanged;

            ResetSubscriptions();
        }


        public SubscriptionsViewModel(
            SubscriptionStore subscriptionStore,
            Func<IEnumerable<SubscriptionModel>, IEnumerable<SubscriptionModel>> filteredSubscriptions )
        {
            _subscriptionStore = subscriptionStore;
            _filteredSubscriptions = filteredSubscriptions;

            _subscriptions = new ObservableCollection<SubscriptionModel>();
            _subscriptionStore.StateChanged += Subscription_StateChanged;

            ResetSubscriptions();
        }


        private void ResetSubscriptions()
        {
            _subscriptions.Clear();

            IEnumerable<SubscriptionModel> subscriptions = _subscriptionStore.SubscriptionList
                .Select( s => new SubscriptionModel( s.ID, s.Name, s.Plan, s.Amount, s.Frequency, s.Status, s.DueDate, _accountStore, _subscriptionService, _renavigator ) )
                .OrderBy( s => s.DueDate );

            if ( _showCancelled )
            {
                subscriptions = subscriptions.Where( s => s.Status == SubscriptionStatus.Cancelled );
            }
            else
            {
                subscriptions = subscriptions.Where( s => s.Status == SubscriptionStatus.Active );
            }

            foreach ( SubscriptionModel subscription in subscriptions )
            {
                _subscriptions.Add( subscription );
            }

            ListEmpty = _subscriptions.IsNullOrEmpty();

            PropertyChanged += ( sender, e ) =>
            {
                if ( e.PropertyName == nameof( SelectedItem ) )
                {
                    SortSubscriptions();
                }
                else if ( e.PropertyName == nameof( ShowCancelled ) )
                {
                    ResetSubscriptions();
                }
            };
        }


        private void SortSubscriptions()
        {
            IEnumerable<SubscriptionModel> subscriptionDataModel = _subscriptionStore.SubscriptionList
                .Select( s => new SubscriptionModel( s.ID, s.Name, s.Plan, s.Amount, s.Frequency, s.Status, s.DueDate, _accountStore, _subscriptionService, _renavigator ) );

            if ( _selectedItem == SortOptions.Asceding )
            {
                subscriptionDataModel = subscriptionDataModel.OrderBy( t => t.Name );
            }
            else if ( _selectedItem == SortOptions.Descending )
            {
                subscriptionDataModel = subscriptionDataModel.OrderByDescending( t => t.Name );
            }
            else if ( _selectedItem == SortOptions.Amount )
            {
                subscriptionDataModel = subscriptionDataModel.OrderByDescending( t => t.Amount );
            }
            else
            {
                subscriptionDataModel = subscriptionDataModel.OrderByDescending( t => t.DueDate );
            }

            _subscriptions.Clear();
            foreach ( SubscriptionModel dataModel in subscriptionDataModel )
            {
                _subscriptions.Add( dataModel );
            }
        }

        private void Subscription_StateChanged()
        {
            ResetSubscriptions();
        }
    }
}
