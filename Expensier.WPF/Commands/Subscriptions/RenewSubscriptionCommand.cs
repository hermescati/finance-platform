using Expensier.Domain.Models;
using Expensier.Domain.Services.Subscriptions;
using Expensier.WPF.DataObjects;
using Expensier.WPF.State.Accounts;
using Expensier.WPF.State.Navigators;
using System;
using System.Diagnostics;
using System.Threading.Tasks;


namespace Expensier.WPF.Commands.Subscriptions
{
    public class RenewSubscriptionCommand : AsyncCommandBase
    {
        private readonly SubscriptionModel _subscriptionDataModel;
        private readonly ISubscriptionService _subscriptionService;
        private readonly IRenavigator _renavigator;
        private readonly AccountStore _accountStore;


        public RenewSubscriptionCommand(
            SubscriptionModel subscriptionDataModel,
            ISubscriptionService subscriptionService,
            IRenavigator renavigator,
            AccountStore accountStore )
        {
            _subscriptionDataModel = subscriptionDataModel;
            _subscriptionService = subscriptionService;
            _renavigator = renavigator;
            _accountStore = accountStore;
        }


        public override async Task ExecuteAsync( object parameter )
        {
            try
            {
                Account updatedAccount = await _subscriptionService.RenewSubscription(
                    _accountStore.CurrentAccount,
                    _subscriptionDataModel.ID );

                _accountStore.CurrentAccount = updatedAccount;
                _renavigator.Renavigate();
            }
            catch ( Exception e )
            {
                Trace.TraceError( e.Message );
                throw;
            }
        }
    }
}
