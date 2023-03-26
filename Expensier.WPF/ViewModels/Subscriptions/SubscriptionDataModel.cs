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

namespace Expensier.WPF.ViewModels.Subscriptions
{
    public class SubscriptionDataModel : ViewModelBase
    {
        private readonly ISubscriptionService _subscriptionService;
        private readonly AccountStore _accountStore;
        private readonly IRenavigator _renavigator;

        public int Id { get; set; }
        public string CompanyName { get; set; }
        public string SubscriptionPlan { get; set; }
        public DateTime DueDate { get; set; }
        public double Amount { get; set; }
        public string SubscriptionCycle { get; set; } 
        public ICommand DeleteCommand { get; }

        public SubscriptionDataModel(
            int id,
            string companyName, 
            string subscriptionPlan, 
            DateTime dueDate, 
            double amount, 
            string subscriptionCycle, 
            ISubscriptionService subscriptionService, 
            AccountStore accountStore, 
            IRenavigator renavigator)
        {
            Id = id;
            CompanyName = companyName;
            SubscriptionPlan = subscriptionPlan;
            DueDate = dueDate;
            Amount = amount;
            SubscriptionCycle = subscriptionCycle;

            _subscriptionService = subscriptionService;
            _accountStore = accountStore;
            _renavigator = renavigator;

            DeleteCommand = new DeleteSubscriptionCommand(this, _subscriptionService, _accountStore, _renavigator);
        }
    }
}
