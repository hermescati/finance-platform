using Expensier.Domain.Models;
using Expensier.Domain.Services.Subscriptions;
using Expensier.WPF.State.Accounts;
using Expensier.WPF.State.Navigators;
using Expensier.WPF.ViewModels.Subscriptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Expensier.WPF.Commands
{
    public class DeleteSubscriptionCommand : AsyncCommandBase
    {
        private readonly SubscriptionDataModel _subscriptionDataModel;
        private readonly ISubscriptionService _subscriptionService;
        private readonly AccountStore _accountStore;
        private readonly IRenavigator _renavigator;

        public DeleteSubscriptionCommand(SubscriptionDataModel subscriptionDataModel, ISubscriptionService subscriptionService, AccountStore accountStore, IRenavigator renavigator)
        {
            _subscriptionDataModel = subscriptionDataModel;
            _subscriptionService = subscriptionService;
            _accountStore = accountStore;
            _renavigator = renavigator;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            try
            {
                Account updatedAccount = await _subscriptionService.DeleteSubscription(
                    _accountStore.CurrentAccount,
                    _subscriptionDataModel.Id);

                _accountStore.CurrentAccount = updatedAccount;
                _renavigator.Renavigate();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
