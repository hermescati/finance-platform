using Expensier.Domain.Models;
using Expensier.Doman.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Expensier.Domain.Services.Subscriptions
{
    public class SubscriptionService : ISubscriptionService
    {
        private readonly IDataService<Account> _accountService;
        private readonly IDataService<Subscription> _subscriptionService;

        public SubscriptionService(IDataService<Account> accountService, IDataService<Subscription> subscriptionService)
        {
            _accountService = accountService;
            _subscriptionService = subscriptionService;
        }

        public async Task<Account> AddSubscription(Account currentAccount, string companyName, string subscriptionPlan, DateTime dueDate, double amount, SubscriptionCycle subscriptionCycle)
        {
            Subscription newSubscription = new Subscription()
            {
                Account_ = currentAccount,
                Company_Name = companyName,
                Subscription_Plan = subscriptionPlan,
                Due_Date = dueDate,
                Amount = amount,
                Subscription_Type = subscriptionCycle.ToString()
            };

            currentAccount.SubscriptionList.Add(newSubscription);

            await _accountService.Update(currentAccount.Id, currentAccount);

            return currentAccount;
        }

        public async Task<Account> DeleteSubscription(Account currentAccount, int subscriptionID)
        {
            currentAccount.SubscriptionList
                .Remove(currentAccount.SubscriptionList
                .FirstOrDefault((subscription) => subscription.Id == subscriptionID));

            await _accountService.Update(currentAccount.Id, currentAccount);

            await _subscriptionService.Delete(subscriptionID);

            return currentAccount;
        }
    }
}
