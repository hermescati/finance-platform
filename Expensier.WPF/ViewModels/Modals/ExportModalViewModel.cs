using Expensier.Domain.Services.Transactions;
using Expensier.WPF.Commands.Transactions;
using Expensier.WPF.State.Accounts;
using Expensier.WPF.State.Navigators;
using Expensier.WPF.ViewModels.Errors;
using System;
using System.IO;
using System.Windows.Input;


namespace Expensier.WPF.ViewModels.Modals
{
    public class ExportModalViewModel : ViewModelBase
    {
        private static string _fileName = $"transactions_{DateTime.Now.ToString( "yyyy-MM-dd" )}.csv";
        public string FileName
        {
            get => _fileName;
            set
            {
                _fileName = value;
                OnPropertyChanged( nameof( FileName ) );
            }
        }


        private static string _filePath = Path.Combine( Environment.GetFolderPath( Environment.SpecialFolder.UserProfile ) + "\\Downloads", _fileName );
        public string FilePath
        {
            get => _filePath;
            set
            {
                _filePath = value;
                OnPropertyChanged( nameof( FilePath ) );
                OnPropertyChanged( nameof( CanExport ) );
            }
        }


        public MessageViewModel ErrorMessage { get; }
        public MessageViewModel SuccessMessage { get; }
        public string ErrorMessageContent { set => ErrorMessage.Message = value; }
        public string SuccessMessageContent { set => SuccessMessage.Message = value; }


        public bool CanExport => !string.IsNullOrEmpty( FilePath );


        public ICommand ExportTransactionData { get; }


        public ExportModalViewModel(
            ITransactionService transactionService,
            AccountStore accountStore,
            IRenavigator renavigator )
        {
            ErrorMessage = new MessageViewModel();
            SuccessMessage = new MessageViewModel();

            ExportTransactionData = new ExportTransactionsCommand( this, transactionService, renavigator, accountStore );
        }
    }
}
