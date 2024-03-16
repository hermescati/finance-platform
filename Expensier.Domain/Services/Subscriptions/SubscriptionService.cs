using Expensier.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Expensier.Domain.Models.Subscription;

namespace Expensier.Domain.Services.Subscriptions
{
    public class SubscriptionService : ISubscriptionService
    {
        private readonly IDataService<Account> _accountService;
        private readonly IDataService<Subscription> _subscriptionService;


        public SubscriptionService( IDataService<Account> accountService, IDataService<Subscription> subscriptionService )
        {
            _accountService = accountService;
            _subscriptionService = subscriptionService;
        }


        public async Task<Account> AddSubscription(
            Account currentAccount,
            string companyName,
            string subscriptionPlan,
            double amount,
            SubscriptionFrequency subscriptionFrequency,
            DateTime dueDate )
        {
            Subscription newSubscription = new Subscription()
            {
                User = currentAccount,
                Name = companyName,
                Plan = subscriptionPlan,
                Frequency = subscriptionFrequency.ToString(),
                Amount = amount,
                IsActive = true,
                DueDate = dueDate,
            };

            currentAccount.SubscriptionList?.Add( newSubscription );
            await _accountService.Update( currentAccount.ID, currentAccount );

            return currentAccount;
        }


        public Subscription GetSubscriptionByID( Account currentAccount, Guid subscriptionID )
        {
            return currentAccount.SubscriptionList.FirstOrDefault( ( subscription ) => subscription.ID == subscriptionID );
        }


        public async Task<Account> DeleteSubscription( Account currentAccount, Guid subscriptionID )
        {
            Subscription subscriptionToDelete = GetSubscriptionByID( currentAccount, subscriptionID );

            if ( subscriptionToDelete != null )
            {
                currentAccount.SubscriptionList?.Remove( subscriptionToDelete );

                await _accountService.Update( currentAccount.ID, currentAccount );
                await _subscriptionService.Delete( subscriptionID );
            }

            return currentAccount;
        }


        public async Task<Account> AcivateSubscription( Account currentAccount, Guid subscriptionID )
        {
            Subscription subscriptionToActivate = GetSubscriptionByID( currentAccount, subscriptionID );

            if ( subscriptionToActivate != null )
            {
                subscriptionToActivate.IsActive = true;
                await _accountService.Update( currentAccount.ID, currentAccount );
            }

            return currentAccount;
        }


        public async Task<Account> CancelSubscription( Account currentAccount, Guid subscriptionID )
        {
            Subscription subscriptionToCancel = GetSubscriptionByID( currentAccount, subscriptionID );

            if ( subscriptionToCancel != null )
            {
                subscriptionToCancel.IsActive = false;
                await _accountService.Update( currentAccount.ID, currentAccount );
            }

            return currentAccount;
        }
    }
}
