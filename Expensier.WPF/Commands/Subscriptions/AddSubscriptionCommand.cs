using Expensier.Domain.Models;
using Expensier.Domain.Services.Subscriptions;
using Expensier.WPF.State.Accounts;
using Expensier.WPF.State.Navigators;
using Expensier.WPF.ViewModels.Modals;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using static Expensier.Domain.Models.Subscription;


namespace Expensier.WPF.Commands.Subscriptions
{
    public class AddSubscriptionCommand : AsyncCommandBase
    {
        private readonly SubscriptionModalViewModel _subscriptionModalViewModel;
        private readonly ISubscriptionService _subscriptionService;
        private readonly IRenavigator _renavigator;
        private readonly AccountStore _accountStore;


        public AddSubscriptionCommand(
            SubscriptionModalViewModel modalViewModel,
            ISubscriptionService subscriptionService,
            IRenavigator renavigator,
            AccountStore accountStore )
        {
            _subscriptionModalViewModel = modalViewModel;
            _subscriptionService = subscriptionService;
            _renavigator = renavigator;
            _accountStore = accountStore;

            _subscriptionModalViewModel.PropertyChanged += SubscriptionModalViewModel_PropertyChanged;
        }



        public override bool CanExecute( object parameter ) =>
            _subscriptionModalViewModel.CanAdd &&
            base.CanExecute( parameter );


        public override async Task ExecuteAsync( object parameter )
        {
            try
            {
                Account updatedAccount = await _subscriptionService.AddSubscription(
                    _accountStore.CurrentAccount,
                    _subscriptionModalViewModel.CompanyName,
                    _subscriptionModalViewModel.SubscriptionPlan,
                    _subscriptionModalViewModel.Amount,
                    _subscriptionModalViewModel.SubscriptionCycle,
                    _subscriptionModalViewModel.DueDate );

                _accountStore.CurrentAccount = updatedAccount;
                _subscriptionModalViewModel.CompanyName = string.Empty;
                _subscriptionModalViewModel.SubscriptionPlan = string.Empty;
                _subscriptionModalViewModel.Amount = 0.0;
                _subscriptionModalViewModel.SubscriptionCycle = SubscriptionFrequency.Monthly;
                _subscriptionModalViewModel.DueDate = DateTime.Now;
                _renavigator.Renavigate();
            }
            catch ( Exception e )
            {
                Trace.TraceError( e.Message );
                throw;
            }
        }


        private void SubscriptionModalViewModel_PropertyChanged( object? sender, System.ComponentModel.PropertyChangedEventArgs e )
        {
            if ( e.PropertyName == nameof( SubscriptionModalViewModel.CanAdd ) )
                OnCallExecuteChanged();
        }
    }
}
