using Expensier.Domain.Services.Subscriptions;
using Expensier.WPF.Commands.Subscriptions;
using Expensier.WPF.State.Accounts;
using Expensier.WPF.State.Navigators;
using System;
using System.Windows.Input;


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
        public string Frequency { get; set; }
        public bool IsActive { get; set; }
        public DateTime DueDate { get; set; }
        public ICommand DeleteCommand { get; }


        public SubscriptionDataModel(
            Guid id,
            string name,
            string plan,
            double amount,
            string frequency,
            bool isActive,
            DateTime dueDate,
            ISubscriptionService subscriptionService,
            IRenavigator renavigator,
            AccountStore accountStore )
        {
            ID = id;
            Name = name;
            Plan = plan;
            Amount = amount;
            Frequency = frequency;
            IsActive = isActive;
            DueDate = dueDate;

            _subscriptionService = subscriptionService;
            _renavigator = renavigator;
            _accountStore = accountStore;

            DeleteCommand = new DeleteSubscriptionCommand( this, _subscriptionService, _renavigator, _accountStore );
        }
    }
}
