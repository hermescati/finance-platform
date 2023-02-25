using Expensier.Domain.Models;
using Expensier.Domain.Services.Subscriptions;
using Expensier.WPF.State.Accounts;
using Expensier.WPF.State.Navigators;
using Expensier.WPF.ViewModels.Modals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Expensier.WPF.Commands
{
    public class AddSubscriptionCommand : AsyncCommandBase
    {
        private readonly SubscriptionModalViewModel _modalViewModel;
        private readonly ISubscriptionService _subscriptionService;
        private readonly AccountStore _accountStore;
        private readonly IRenavigator _renavigator;

        public AddSubscriptionCommand(SubscriptionModalViewModel modalViewModel, ISubscriptionService subscriptionService, AccountStore accountStore, IRenavigator renavigator)
        {
            _modalViewModel = modalViewModel;
            _subscriptionService = subscriptionService;
            _accountStore = accountStore;
            _renavigator = renavigator;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            try
            {
                Account updatedAccount = await _subscriptionService.AddSubscription(
                    _accountStore.CurrentAccount,
                    _modalViewModel.CompanyName,
                    _modalViewModel.SubscriptionPlan,
                    _modalViewModel.DueDate,
                    _modalViewModel.Amount,
                    _modalViewModel.SubscriptionCycle);

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
