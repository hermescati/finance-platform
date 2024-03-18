using Expensier.Domain.Models;
using static Expensier.Domain.Models.Subscription;
using static Expensier.Domain.Models.Transaction;


namespace Expensier.Domain.Services.Subscriptions
{
    public class SubscriptionService : ISubscriptionService
    {
        private readonly IDataService<Account> _accountService;
        private readonly IDataService<Transaction> _transactionService;
        private readonly IDataService<Subscription> _subscriptionService;


        public SubscriptionService(
            IDataService<Account> accountService,
            IDataService<Transaction> transactionService,
            IDataService<Subscription> subscriptionService )
        {
            _accountService = accountService;
            _transactionService = transactionService;
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
                Frequency = subscriptionFrequency,
                Amount = amount,
                Status = SubscriptionStatus.Active,
                DueDate = dueDate,
            };

            currentAccount.SubscriptionList?.Add( newSubscription );
            await _accountService.Update( currentAccount.ID, currentAccount );

            return currentAccount;
        }


        public Subscription GetSubscriptionByID( Account currentAccount, Guid subscriptionID ) => currentAccount.SubscriptionList
            .Single( ( subscription ) => subscription.ID == subscriptionID );


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


        public async Task<Account> RenewSubscription( Account currentAccount, Guid subscriptionID )
        {
            Subscription subscriptionToRenew = GetSubscriptionByID( currentAccount, subscriptionID );

            if ( subscriptionToRenew == null || subscriptionToRenew.Status == SubscriptionStatus.Active )
            {
                return currentAccount;
            }

            if ( subscriptionToRenew.Frequency == SubscriptionFrequency.Monthly )
            {
                subscriptionToRenew.DueDate = DateTime.Now.AddMonths( 1 );
            }
            else if ( subscriptionToRenew.Frequency == SubscriptionFrequency.Annual )
            {
                subscriptionToRenew.DueDate = DateTime.Now.AddYears( 1 );
            }
            subscriptionToRenew.Status = SubscriptionStatus.Active;

            Transaction subscriptionPayment = new Transaction()
            {
                User = currentAccount,
                Name = $"{subscriptionToRenew.Name} Payment",
                Category = TransactionCategory.Subscription.ToString(),
                Amount = subscriptionToRenew.Amount,
                IsCredit = true,
                ProcessedDate = DateTime.Now,
            };
            currentAccount.TransactionList?.Add( subscriptionPayment );

            await _subscriptionService.Update( subscriptionID, subscriptionToRenew );
            await _accountService.Update( currentAccount.ID, currentAccount );

            return currentAccount;
        }


        public async Task<Account> CancelSubscription( Account currentAccount, Guid subscriptionID )
        {
            Subscription subscriptionToCancel = GetSubscriptionByID( currentAccount, subscriptionID );

            if ( subscriptionToCancel != null )
            {
                subscriptionToCancel.Status = SubscriptionStatus.Cancelled;
                subscriptionToCancel.DueDate = null;

                await _accountService.Update( currentAccount.ID, currentAccount );
            }

            return currentAccount;
        }


        public async Task UpdateSubscriptionDate()
        {
            IEnumerable<Subscription> subscriptionList = await _subscriptionService.GetAll();
            IEnumerable<Subscription> activeSubscriptions = subscriptionList.Where( s => s.Status == SubscriptionStatus.Active );

            foreach ( Subscription subscription in activeSubscriptions )
            {
                if ( subscription.DueDate > DateTime.Now )
                {
                    continue;
                }

                if ( subscription.Frequency == SubscriptionFrequency.Monthly )
                {
                    subscription.DueDate = subscription.DueDate?.AddMonths( 1 );
                }
                else if ( subscription.Frequency == SubscriptionFrequency.Annual )
                {
                    subscription.DueDate = subscription.DueDate?.AddYears( 1 );
                }

                await _subscriptionService.Update(subscription.ID, subscription );
            }
        }
    }
}
