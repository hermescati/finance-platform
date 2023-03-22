﻿using Expensier.WPF.State.Expenses;
using Expensier.WPF.State.Subscriptions;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Expensier.WPF.ViewModels.Subscriptions
{
    public class SubscriptionViewModel : ViewModelBase
    {
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

        public IEnumerable<SubscriptionDataModel> Subscriptions => _subscriptions;

        public SubscriptionViewModel(SubscriptionStore subscriptionStore) : this(subscriptionStore, subscriptions => subscriptions) { }

        public SubscriptionViewModel(SubscriptionStore subscriptionStore, Func<IEnumerable<SubscriptionDataModel>, IEnumerable<SubscriptionDataModel>> filterSubscriptions)
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
                .Select(s => new SubscriptionDataModel(s.Company_Name, s.Subscription_Plan, s.Due_Date, s.Amount, s.Subscription_Type))
                .OrderBy(s => s.DueDate);

            _subscriptions.Clear();
            foreach (SubscriptionDataModel dataModel in subscriptionDataModel)
            {
                _subscriptions.Add(dataModel);
            }

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

        private void Subscription_StateChanged()
        {
            ResetSubscriptions();
        }
    }
}
