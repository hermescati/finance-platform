using Expensier.Domain.Models;
using static Expensier.Domain.Models.Subscription;


namespace Expensier.Domain.Services.Subscriptions
{
    /// <summary>
    /// Represents a service interface for managing subscriptions within user accounts.
    /// </summary>
    public interface ISubscriptionService
    {
        /// <summary>
        /// Adds a new subscription to the provided user account.
        /// </summary>
        /// <param name="currentAccount">The account to which the subscription will be added.</param>
        /// <param name="companyName">The name of the company providing the subscription.</param>
        /// <param name="subscriptionPlan">The name of the subscription plan.</param>
        /// <param name="amount">The amount of the subscription.</param>
        /// <param name="subscriptionFrequency">The frequency of the subscription (e.g., monthly, annual).</param>
        /// <param name="dueDate">The due date of the subscription.</param>
        /// <returns>The updated account after the subscription has been added.</returns>
        Task<Account> AddSubscription(
            Account currentAccount,
            string companyName,
            string subscriptionPlan,
            double amount,
            SubscriptionFrequency subscriptionFrequency,
            DateTime dueDate );


        /// <summary>
        /// Retrieves a subscription by its ID from the provided user account.
        /// </summary>
        /// <param name="currentAccount">The acocunt under which the subscription belongs to.</param>
        /// <param name="subscriptionID">The ID of the subscription to be retrieved.</param>
        /// <returns>The subscription with the specified ID.</returns>
        Subscription GetSubscriptionByID(
            Account currentAccount,
            Guid subscriptionID );


        /// <summary>
        /// Deletes a subscription from the provided user account.
        /// </summary>
        /// <param name="currentAccount">The account from which the subscription will be deleted.</param>
        /// <param name="subscriptionID">The ID of the subscription to be deleted.</param>
        /// <returns>The updated account after the subscription has been deleted.</returns>
        Task<Account> DeleteSubscription(
            Account currentAccount,
            Guid subscriptionID );


        /// <summary>
        /// Renews a subscription from the provided user account. Renewing a subscription will be followed
        /// by a new transaction based on the assumption that to renew a subscription one needs to pay the fee.
        /// </summary>
        /// <param name="currentAccount">The account for which the subscription will be renewed.</param>
        /// <param name="subscriptionID">The ID of the subscription to be renewed.</param>
        /// <returns>The updated account after the subscription has been renewed.</returns>
        Task<Account> RenewSubscription(
            Account currentAccount,
            Guid subscriptionID );


        /// <summary>
        /// Cancels a subscription for the provided user account without deleting it. This way the user can
        /// renew the subscription without having the need to create it from the beginning.
        /// </summary>
        /// <param name="currentAccount">The account for which the subscription will be canceled.</param>
        /// <param name="subscriptionID">The ID of the subscription to cancel.</param>
        /// <returns>The updated account after the subscription has been canceled.</returns>
        Task<Account> CancelSubscription(
            Account currentAccount,
            Guid subscriptionID );


        /// <summary>
        /// Updates the due date of all subscriptions based on their frequency.
        /// </summary>
        /// <returns></returns>
        Task UpdateSubscriptionDate();
    }
}
