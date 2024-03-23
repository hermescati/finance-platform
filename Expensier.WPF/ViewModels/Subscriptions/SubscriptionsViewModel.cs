using Expensier.Domain.Services.Subscriptions;
using Expensier.WPF.DataObjects;
using Expensier.WPF.Enums;
using Expensier.WPF.State.Accounts;
using Expensier.WPF.State.Navigators;
using Expensier.WPF.State.Subscriptions;
using Expensier.WPF.Utils;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
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

        private SortOptions _selectedOption;
        public SortOptions SelectedOption
        {
            get => _selectedOption;
            set
            {
                _selectedOption = value;
                OnPropertyChanged( nameof( SelectedOption ) );
                SortSubscriptions( SelectedOption );
            }
        }
        public IEnumerable<SortOptions> SortItems { get; } = SortOptions.SubscriptionOptions();


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
            _subscriptionStore.StateChanged += FetchSubscriptions;

            FetchSubscriptions();
        }


        public SubscriptionsViewModel(
            SubscriptionStore subscriptionStore,
            Func<IEnumerable<SubscriptionModel>, IEnumerable<SubscriptionModel>> filteredSubscriptions )
        {
            _subscriptionStore = subscriptionStore;
            _filteredSubscriptions = filteredSubscriptions;

            _subscriptions = new ObservableCollection<SubscriptionModel>();
            _subscriptionStore.StateChanged += FetchSubscriptions;

            FetchSubscriptions();
        }


        private void FetchSubscriptions()
        {
            IEnumerable<SubscriptionModel> subscriptions = _subscriptionStore.SubscriptionList
                .Select( s => new SubscriptionModel( s.ID, s.Name, s.Plan, s.Amount, s.Frequency, s.Status, s.DueDate, _accountStore, _subscriptionService, _renavigator ) )
                .Where( s => _showCancelled ? s.Status == SubscriptionStatus.Cancelled : s.Status == SubscriptionStatus.Active )
                .OrderBy( s => s.DueDate );

            _subscriptions.Clear();
            foreach ( SubscriptionModel subscription in subscriptions )
            {
                _subscriptions.Add( subscription );
            }

            ListEmpty = _subscriptions.IsNullOrEmpty();

            PropertyChanged += PropertyChangedEventHandler;
        }


        private void SortSubscriptions( SortOptions selectedOption )
        {
            Type type = typeof( SubscriptionModel );
            PropertyInfo? property = type.GetProperty( selectedOption.Property );

            if ( property == null )
                return;

            IEnumerable<SubscriptionModel> sortedSubscriptions = new ObservableCollection<SubscriptionModel>( _subscriptions );

            sortedSubscriptions = selectedOption.Direction == SortDirection.Ascending
                ? sortedSubscriptions.OrderBy( s => property.GetValue( s ) )
                : sortedSubscriptions.OrderByDescending( s => property.GetValue( s ) );

            _subscriptions.Clear();
            foreach ( var subscription in sortedSubscriptions )
            {
                _subscriptions.Add( subscription );
            }
        }


        private void PropertyChangedEventHandler( object sender, PropertyChangedEventArgs e )
        {
            if ( e.PropertyName == nameof( SelectedOption ) )
                SortSubscriptions( SelectedOption );


            if ( e.PropertyName == nameof( ShowCancelled ) )
                FetchSubscriptions();
        }
    }
}
