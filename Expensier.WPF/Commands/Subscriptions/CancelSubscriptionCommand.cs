using Expensier.Domain.Models;
using Expensier.Domain.Services.Subscriptions;
using Expensier.WPF.State.Accounts;
using Expensier.WPF.State.Navigators;
using Expensier.WPF.ViewModels.Subscriptions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Expensier.WPF.Commands.Subscriptions
{
    public class CancelSubscriptionCommand : AsyncCommandBase
    {
        private readonly SubscriptionDataModel _subscriptionDataModel;
        private readonly ISubscriptionService _subscriptionService;
        private readonly IRenavigator _renavigator;
        private readonly AccountStore _accountStore;


        public CancelSubscriptionCommand(
            SubscriptionDataModel subscriptionDataModel,
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
                Account updatedAccount = await _subscriptionService.CancelSubscription(
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
