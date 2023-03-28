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
                AccountHolder = currentAccount,
                CompanyName = companyName,
                SubscriptionPlan = subscriptionPlan,
                DueDate = dueDate,
                Amount = amount,
                SubscriptionType = subscriptionCycle.ToString()
            };

            currentAccount.SubscriptionList.Add(newSubscription);

            await _accountService.Update(currentAccount.ID, currentAccount);

            return currentAccount;
        }

        public async Task<Account> DeleteSubscription(Account currentAccount, int subscriptionID)
        {
            currentAccount.SubscriptionList
                .Remove(currentAccount.SubscriptionList
                .FirstOrDefault((subscription) => subscription.ID == subscriptionID));

            await _accountService.Update(currentAccount.ID, currentAccount);

            await _subscriptionService.Delete(subscriptionID);

            return currentAccount;
        }
    }
}
