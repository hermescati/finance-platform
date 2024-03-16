using Expensier.Domain.Services.Subscriptions;
using Expensier.WPF.Commands.Subscriptions;
using Expensier.WPF.State.Accounts;
using Expensier.WPF.State.Navigators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using static Expensier.Domain.Models.Subscription;


namespace Expensier.WPF.ViewModels.Modals
{
    public class SubscriptionModalViewModel : ViewModelBase
    {
        private string _companyName;
        public string CompanyName
        {
            get => _companyName;
            set
            {
                _companyName = value;
                OnPropertyChanged( nameof( CompanyName ) );
                OnPropertyChanged( nameof( CanAdd ) );
            }
        }


        private string _subscriptionPlan;
        public string SubscriptionPlan
        {
            get => _subscriptionPlan;
            set
            {
                _subscriptionPlan = value;
                OnPropertyChanged( nameof( SubscriptionPlan ) );
                OnPropertyChanged( nameof( CanAdd ) );
            }
        }


        private double _amount;
        public double Amount
        {
            get => _amount;
            set
            {
                _amount = value;
                OnPropertyChanged( nameof( Amount ) );
                OnPropertyChanged( nameof( CanAdd ) );
            }
        }


        private SubscriptionFrequency _subscriptionCycle;
        public SubscriptionFrequency SubscriptionCycle
        {
            get => _subscriptionCycle;
            set
            {
                _subscriptionCycle = value;
                OnPropertyChanged( nameof( SubscriptionCycle ) );
            }
        }


        private DateTime _dueDate = DateTime.Now;
        public DateTime DueDate
        {
            get => _dueDate;
            set
            {
                _dueDate = value;
                OnPropertyChanged( nameof( DueDate ) );
            }
        }


        public bool CanAdd => 
            !string.IsNullOrEmpty(CompanyName) && 
            !string.IsNullOrEmpty(SubscriptionPlan) && 
            Amount > 0.0;


        public IEnumerable<SubscriptionFrequency> SubscriptionCycles => 
            Enum.GetValues(typeof(SubscriptionFrequency )).Cast<SubscriptionFrequency>();


        public ICommand AddSubscriptionCommand { get; }


        public SubscriptionModalViewModel(
            ISubscriptionService subscriptionService, 
            AccountStore accountStore, 
            IRenavigator renavigator)
        {
            AddSubscriptionCommand = new AddSubscriptionCommand(this, subscriptionService, renavigator, accountStore );
        }
    }
}
