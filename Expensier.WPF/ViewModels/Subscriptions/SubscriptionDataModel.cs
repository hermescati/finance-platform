using Expensier.Domain.Services.Subscriptions;
using Expensier.Domain.Services.Transactions;
using Expensier.WPF.Commands.Subscriptions;
using Expensier.WPF.State.Accounts;
using Expensier.WPF.State.Navigators;
using System;
using System.Windows.Input;
using static Expensier.Domain.Models.Subscription;


namespace Expensier.WPF.ViewModels.Subscriptions
{
    public class SubscriptionDataModel : ViewModelBase
    {
        private readonly ISubscriptionService _subscriptionService;
        private readonly IRenavigator _renavigator;
        private readonly AccountStore _accountStore;


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


        public SubscriptionDataModel(
            Guid id,
            string name,
            string plan,
            double amount,
            SubscriptionFrequency frequency,
            SubscriptionStatus status,
            DateTime? dueDate,
            ISubscriptionService subscriptionService,
            IRenavigator renavigator,
            AccountStore accountStore )
        {
            ID = id;
            Name = name;
            Plan = plan;
            Amount = amount;
            Frequency = frequency;
            Status = status;
            DueDate = dueDate;

            _subscriptionService = subscriptionService;
            _renavigator = renavigator;
            _accountStore = accountStore;

            ActivateCommand = new RenewSubscriptionCommand( this, _subscriptionService, _renavigator, _accountStore );
            CancelCommand = new CancelSubscriptionCommand( this, _subscriptionService, _renavigator, _accountStore );
            DeleteCommand = new DeleteSubscriptionCommand( this, _subscriptionService, _renavigator, _accountStore );
        }
    }
}
