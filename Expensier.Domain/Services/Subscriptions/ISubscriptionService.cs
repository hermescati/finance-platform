using Expensier.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Expensier.Domain.Models.Subscription;

namespace Expensier.Domain.Services.Subscriptions
{
    public interface ISubscriptionService
    {
        Task<Account> AddSubscription(
            Account currentAccount,
            string companyName,
            string subscriptionPlan,
            double amount,
            SubscriptionFrequency subscriptionFrequency,
            DateTime dueDate );


        Subscription GetSubscriptionByID(
            Account currentAccount,
            Guid subscriptionID );


        Task<Account> DeleteSubscription(
            Account currentAccount,
            Guid subscriptionID );


        Task<Account> AcivateSubscription(
            Account currentAccount,
            Guid subscriptionID );


        Task<Account> CancelSubscription(
            Account currentAccount,
            Guid subscriptionID );
    }
}
