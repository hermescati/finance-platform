using Expensier.Domain.Services.Transactions;
using Expensier.WPF.State.Accounts;
using Expensier.WPF.State.Navigators;
using Expensier.WPF.ViewModels.Modals;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Expensier.WPF.Commands
{
    public class ExportTransactionDataCommand : AsyncCommandBase
    {
        private readonly ExportModalViewModel _exportModalViewModel;
        private readonly AccountStore _accountStore;
        private readonly ITransactionService _transactionService;
        private readonly IRenavigator _renavigator;


        public ExportTransactionDataCommand( ExportModalViewModel exportModalViewModel, ITransactionService transactionService, AccountStore accountStore, IRenavigator renavigator )
        {
            _exportModalViewModel = exportModalViewModel;
            _transactionService = transactionService;
            _accountStore = accountStore;
            _renavigator = renavigator;

            _exportModalViewModel.PropertyChanged += ExportModalViewModel_PropertyChanged;
        }


        public override bool CanExecute( object parameter )
        {
            return _exportModalViewModel.CanExport && base.CanExecute(parameter);
        }


        public override async Task ExecuteAsync(object parameter )
        {
            try
            {
                string filePath = Path.Combine(_exportModalViewModel.FileLocation, _exportModalViewModel.FileName);
                await _transactionService.ExportTransactionData(_accountStore.CurrentAccount, filePath);

                _renavigator.Renavigate();
            }
            catch ( Exception )
            {
                throw;
            }
        }


        private void ExportModalViewModel_PropertyChanged(object? sender, PropertyChangedEventArgs e )
        {
            if (e.PropertyName == nameof( ExportModalViewModel.CanExport) )
            {
                OnCallExecuteChanged();
            }
        }
    }
}
