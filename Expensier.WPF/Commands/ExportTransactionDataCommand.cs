﻿using Expensier.Domain.Services.Transactions;
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
        private readonly ITransactionService _transactionService;
        private readonly AccountStore _accountStore;
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
            return _exportModalViewModel.CanExport && base.CanExecute( parameter );
        }


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
            catch ( Exception )
            {
                _exportModalViewModel.ErrorMessageContent = "There was an error exporting data. Please try again!";
                throw;
            }
        }


        private void ExportModalViewModel_PropertyChanged( object? sender, PropertyChangedEventArgs e )
        {
            if ( e.PropertyName == nameof( ExportModalViewModel.CanExport ) )
            {
                OnCallExecuteChanged();
            }
        }
    }
}
