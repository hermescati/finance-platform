﻿using Expensier.Domain.Services.Subscriptions;
using Expensier.WPF.State.Accounts;
using Expensier.WPF.State.Expenses;
using Expensier.WPF.State.Navigators;
using Expensier.WPF.State.Subscriptions;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Expensier.WPF.ViewModels.Charts.ChartDropdownValues;

namespace Expensier.WPF.ViewModels.Subscriptions
{
    public class SubscriptionViewModel : ViewModelBase
    {
        private readonly ISubscriptionService _subscriptionService;
        private readonly AccountStore _accountStore;
        private readonly IRenavigator _renavigator;
        private readonly SubscriptionStore _subscriptionStore;
        private readonly Func<IEnumerable<SubscriptionDataModel>, IEnumerable<SubscriptionDataModel>> _filterSubscriptions;
        private readonly ObservableCollection<SubscriptionDataModel> _subscriptions;

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
                OnPropertyChanged( nameof( ListEmpty ) );
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
                OnPropertyChanged( nameof( ListNotEmpty ) );
            }
        }

        private SortingFunctions _selectedItem;
        public SortingFunctions SelectedItem
        {
            get
            {
                return _selectedItem;
            }
            set
            {
                _selectedItem = value;
                OnPropertyChanged( nameof( SelectedItem ) );
            }
        }

        public IEnumerable<SortingFunctions> Sort => Enum.GetValues( typeof( SortingFunctions ) )
            .Cast<SortingFunctions>();

        public IEnumerable<SubscriptionDataModel> Subscriptions => _subscriptions;

        public SubscriptionViewModel(
            SubscriptionStore subscriptionStore,
            ISubscriptionService subscriptionService,
            AccountStore accountStore,
            IRenavigator renavigator ) : this( subscriptionStore, subscriptions => subscriptions, subscriptionService, accountStore, renavigator ) { }

        public SubscriptionViewModel(
            SubscriptionStore subscriptionStore,
            Func<IEnumerable<SubscriptionDataModel>, IEnumerable<SubscriptionDataModel>> filterSubscriptions,
            ISubscriptionService subscriptionService,
            AccountStore accountStore,
            IRenavigator renavigator )
        {
            _subscriptionStore = subscriptionStore;
            _subscriptionService = subscriptionService;
            _accountStore = accountStore;
            _renavigator = renavigator;
            _filterSubscriptions = filterSubscriptions;
            _subscriptions = new ObservableCollection<SubscriptionDataModel>();

            _subscriptionStore.StateChanged += Subscription_StateChanged;

            ResetSubscriptions();
        }

        public SubscriptionViewModel( SubscriptionStore subscriptionStore, Func<IEnumerable<SubscriptionDataModel>, IEnumerable<SubscriptionDataModel>> filterSubscriptions )
        {
            _subscriptionStore = subscriptionStore;
            _filterSubscriptions = filterSubscriptions;
            _subscriptions = new ObservableCollection<SubscriptionDataModel>();

            _subscriptionStore.StateChanged += Subscription_StateChanged;

            ResetSubscriptions();
        }

        private void ResetSubscriptions()
        {
            IEnumerable<SubscriptionDataModel> subscriptionDataModel = _subscriptionStore.SubscriptionList
                .Select( s => new SubscriptionDataModel( s.ID, s.Name, s.Plan, s.Amount, s.Frequency, s.IsActive, s.DueDate, _subscriptionService, _renavigator, _accountStore ) )
                .OrderBy( s => s.DueDate );

            _subscriptions.Clear();
            foreach ( SubscriptionDataModel dataModel in subscriptionDataModel )
            {
                _subscriptions.Add( dataModel );
            }

            if ( _subscriptions.IsNullOrEmpty() )
            {
                _listEmpty = true;
                _listNotEmpty = false;
            }
            else
            {
                _listEmpty = false;
                _listNotEmpty = true;
            }

            PropertyChanged += ( sender, e ) =>
            {
                if ( e.PropertyName == nameof( SelectedItem ) )
                {
                    SortSubscriptions();
                }
            };
        }

        public void SortSubscriptions()
        {
            IEnumerable<SubscriptionDataModel> subscriptionDataModel = _subscriptionStore.SubscriptionList
                .Select( s => new SubscriptionDataModel( s.ID, s.Name, s.Plan, s.Amount, s.Frequency, s.IsActive, s.DueDate, _subscriptionService, _renavigator, _accountStore ) );

            if ( _selectedItem == SortingFunctions.Asceding )
            {
                subscriptionDataModel = subscriptionDataModel.OrderBy( t => t.Name );
            }
            else if ( _selectedItem == SortingFunctions.Descending )
            {
                subscriptionDataModel = subscriptionDataModel.OrderByDescending( t => t.Name );
            }
            else if ( _selectedItem == SortingFunctions.Amount )
            {
                subscriptionDataModel = subscriptionDataModel.OrderByDescending( t => t.Amount );
            }
            else
            {
                subscriptionDataModel = subscriptionDataModel.OrderByDescending( t => t.DueDate );
            }

            _subscriptions.Clear();
            foreach ( SubscriptionDataModel dataModel in subscriptionDataModel )
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
