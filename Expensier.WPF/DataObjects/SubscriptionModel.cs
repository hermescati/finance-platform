using Expensier.Domain.Services.Subscriptions;
using Expensier.WPF.Commands.Subscriptions;
using Expensier.WPF.State.Accounts;
using Expensier.WPF.State.Navigators;
using Expensier.WPF.ViewModels;
using System;
using System.Windows.Input;
using static Expensier.Domain.Models.Subscription;


namespace Expensier.WPF.DataObjects
{
    public class SubscriptionModel : ViewModelBase
    {
        private readonly AccountStore _accountStore;
        private readonly ISubscriptionService _subscriptionService;
        private readonly IRenavigator _renavigator;


        public Guid ID { get; set; }
        public string Name { get; set; }
        public string Plan { get; set; }
        public double Amount { get; set; }
        public SubscriptionFrequency Frequency { get; set; }
        public SubscriptionStatus Status { get; set; }
        public DateTime? DueDate { get; set; }
        public ICommand ActivateCommand { get; }
        public ICommand CancelCommand { get; }
        public ICommand DeleteCommand { get; }


        public SubscriptionModel(
            Guid id,
            string name,
            string plan,
            double amount,
            SubscriptionFrequency frequency,
            SubscriptionStatus status,
            DateTime? dueDate,
            AccountStore accountStore,
            ISubscriptionService subscriptionService,
            IRenavigator renavigator )
        {
            ID = id;
            Name = name;
            Plan = plan;
            Amount = amount;
            Frequency = frequency;
            Status = status;
            DueDate = dueDate;

            _accountStore = accountStore;
            _subscriptionService = subscriptionService;
            _renavigator = renavigator;

            ActivateCommand = new RenewSubscriptionCommand( this, _subscriptionService, _renavigator, _accountStore );
            CancelCommand = new CancelSubscriptionCommand( this, _subscriptionService, _renavigator, _accountStore );
            DeleteCommand = new DeleteSubscriptionCommand( this, _subscriptionService, _renavigator, _accountStore );
        }
    }
}
