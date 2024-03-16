using Expensier.Domain.Services.Transactions;
using Expensier.WPF.State.Accounts;
using Expensier.WPF.State.Navigators;
using Expensier.WPF.ViewModels.Modals;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading.Tasks;


namespace Expensier.WPF.Commands.Transactions
{
    public class ExportTransactionsCommand : AsyncCommandBase
    {
        private readonly ExportModalViewModel _exportModalViewModel;
        private readonly ITransactionService _transactionService;
        private readonly IRenavigator _renavigator;
        private readonly AccountStore _accountStore;

        public ExportTransactionsCommand(
            ExportModalViewModel exportModalViewModel,
            ITransactionService transactionService,
            IRenavigator renavigator,
            AccountStore accountStore )
        {
            _exportModalViewModel = exportModalViewModel;
            _transactionService = transactionService;
            _renavigator = renavigator;
            _accountStore = accountStore;

            _exportModalViewModel.PropertyChanged += ExportModalViewModel_PropertyChanged;
        }


        public override bool CanExecute( object parameter ) =>
            _exportModalViewModel.CanExport &&
            base.CanExecute( parameter );


        public override async Task ExecuteAsync( object parameter )
        {
            _exportModalViewModel.ErrorMessageContent = string.Empty;
            _exportModalViewModel.SuccessMessageContent = string.Empty;

            try
            {
                await _transactionService.ExportTransactionData( _accountStore.CurrentAccount, _exportModalViewModel.FilePath );
                _exportModalViewModel.SuccessMessageContent = "Data successfully exported!";

                _renavigator.Renavigate();
            }
            catch ( Exception e )
            {
                _exportModalViewModel.ErrorMessageContent = "There was an error exporting data. Please try again!";
                Trace.TraceError( e.Message );
                throw;
            }
        }


        private void ExportModalViewModel_PropertyChanged( object? sender, PropertyChangedEventArgs e )
        {
            if ( e.PropertyName == nameof( ExportModalViewModel.CanExport ) )
                OnCallExecuteChanged();
        }
    }
}
