using Expensier.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Expensier.Domain.Services.Subscriptions
{
    public enum SubscriptionCycle
    {
        Monthly, 
        Annual
    }

    public interface ISubscriptionService
    {
        Task<Account> AddSubscription(
            Account currentAccount,
            string companyName,
            string subscriptionPlan,
            DateTime dueDate,
            double amount,
            SubscriptionCycle subscriptionCycle);

        Task<Account> DeleteSubscription(
            Account currentAccount,
            int subscriptionID);
    }
}
