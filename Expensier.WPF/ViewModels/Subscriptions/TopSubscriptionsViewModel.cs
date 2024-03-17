﻿using Expensier.WPF.DataObjects;
using Expensier.WPF.State.Subscriptions;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.DirectoryServices.ActiveDirectory;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Expensier.Domain.Models.Subscription;

namespace Expensier.WPF.ViewModels.Subscriptions
{
    public class TopSubscriptionsViewModel : ViewModelBase
    {
        private readonly SubscriptionStore _subscriptionStore;
        public SubscriptionsViewModel SubscriptionViewModel { get; }
        private readonly ObservableCollection<SubscriptionModel> _subscriptions;

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
                OnPropertyChanged(nameof(ListEmpty));
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
                OnPropertyChanged(nameof(ListNotEmpty));
            }
        }

        public IEnumerable<SubscriptionModel> Subscriptions => _subscriptions;

        public TopSubscriptionsViewModel(SubscriptionStore subscriptionStore)
        {
            _subscriptions = new ObservableCollection<SubscriptionModel>();
            _subscriptionStore = subscriptionStore;

            SubscriptionViewModel = new SubscriptionsViewModel(subscriptionStore, 
                subscriptions => subscriptions
                .Where(s => s.Status == SubscriptionStatus.Active)
                .OrderByDescending(s => s.DueDate)
                .Take(3));

            _subscriptions = (ObservableCollection<SubscriptionModel>)SubscriptionViewModel.Subscriptions;

            if (_subscriptions.IsNullOrEmpty())
            {
                _listEmpty = true;
                _listNotEmpty = false;
            }
            else
            {
                _listEmpty = false;
                _listNotEmpty = true;
            }
        }
    }
}
