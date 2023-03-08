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
        private readonly SubscriptionModalViewModel _subscriptionModalViewModel;
        private readonly ISubscriptionService _subscriptionService;
        private readonly AccountStore _accountStore;
        private readonly IRenavigator _renavigator;

        public AddSubscriptionCommand(SubscriptionModalViewModel modalViewModel, ISubscriptionService subscriptionService, AccountStore accountStore, IRenavigator renavigator)
        {
            _subscriptionModalViewModel = modalViewModel;
            _subscriptionService = subscriptionService;
            _accountStore = accountStore;
            _renavigator = renavigator;

            _subscriptionModalViewModel.PropertyChanged += SubscriptionModalViewModel_PropertyChanged;
        }

        public override bool CanExecute(object parameter)
        {
            return _subscriptionModalViewModel.CanAdd && base.CanExecute(parameter);
        }

        public override async Task ExecuteAsync(object parameter)
        {
            try
            {
                Account updatedAccount = await _subscriptionService.AddSubscription(
                    _accountStore.CurrentAccount,
                    _subscriptionModalViewModel.CompanyName,
                    _subscriptionModalViewModel.SubscriptionPlan,
                    _subscriptionModalViewModel.DueDate,
                    _subscriptionModalViewModel.Amount,
                    _subscriptionModalViewModel.SubscriptionCycle);

                _accountStore.CurrentAccount = updatedAccount;
                _renavigator.Renavigate();
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void SubscriptionModalViewModel_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(SubscriptionModalViewModel.CanAdd))
            {
                OnCallExecuteChanged();
            }
        }
    }
}
