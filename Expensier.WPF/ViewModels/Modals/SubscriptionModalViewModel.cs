﻿using Expensier.Domain.Models;
using Expensier.Domain.Services.Subscriptions;
using Expensier.WPF.Commands;
using Expensier.WPF.State.Accounts;
using Expensier.WPF.State.Navigators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using static Expensier.Domain.Models.Subscription;

namespace Expensier.WPF.ViewModels.Modals
{
    public class SubscriptionModalViewModel : ViewModelBase
    {
        private string _companyName;
        public string CompanyName
        {
            get
            {
                return _companyName;
            }
            set
            {
                _companyName = value;
                OnPropertyChanged(nameof(CompanyName));
                OnPropertyChanged(nameof(CanAdd));
            }
        }

        private string _subscriptionPlan;
        public string SubscriptionPlan
        {
            get
            {
                return _subscriptionPlan;
            }
            set
            {
                _subscriptionPlan = value;
                OnPropertyChanged(nameof(SubscriptionPlan));
                OnPropertyChanged(nameof(CanAdd));
            }
        }

        private double _amount;
        public double Amount
        {
            get
            {
                return _amount;
            }
            set
            {
                _amount = value;
                OnPropertyChanged(nameof(Amount));
                OnPropertyChanged(nameof(CanAdd));
            }
        }

        private SubscriptionFrequency _subscriptionCycle;
        public SubscriptionFrequency SubscriptionCycle
        {
            get
            {
                return _subscriptionCycle;
            }
            set
            {
                _subscriptionCycle = value;
                OnPropertyChanged(nameof(SubscriptionCycle));
            }
        }

        private DateTime _dueDate = DateTime.Now;
        public DateTime DueDate
        {
            get
            {
                return _dueDate;
            }
            set
            {
                _dueDate = value;
                OnPropertyChanged(nameof(DueDate));
            }
        }

        public bool CanAdd => !string.IsNullOrEmpty(CompanyName) && 
            !string.IsNullOrEmpty(SubscriptionPlan) && 
            Amount > 0.0;

        public IEnumerable<SubscriptionFrequency> SubscriptionCycles => Enum.GetValues(typeof(SubscriptionFrequency )).Cast<SubscriptionFrequency>();

        public ICommand AddSubscriptionCommand { get; }

        public SubscriptionModalViewModel(ISubscriptionService subscriptionService, AccountStore accountStore, IRenavigator renavigator)
        {
            AddSubscriptionCommand = new AddSubscriptionCommand(this, subscriptionService, accountStore, renavigator);
        }
    }
}
