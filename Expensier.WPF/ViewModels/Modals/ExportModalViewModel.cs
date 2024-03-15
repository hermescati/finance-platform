using Expensier.Domain.Services.Transactions;
using Expensier.WPF.Commands;
using Expensier.WPF.State.Accounts;
using Expensier.WPF.State.Navigators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Expensier.WPF.ViewModels.Modals
{
    public class ExportModalViewModel : ViewModelBase
    {
        private string _fileName = $"transactions_{DateTime.Now.ToString( "yyyy-MM-dd" )}.csv";
        public string FileName
        {
            get { return _fileName; }
            set
            {
                _fileName = value;
                OnPropertyChanged( nameof( FileName ) );
                OnPropertyChanged( nameof( CanExport ) );
            }
        }


        private string _fileLocation = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "\\Downloads";
        public string FileLocation
        {
            get { return _fileLocation; }
            set
            {
                _fileLocation = value;
                OnPropertyChanged( nameof( FileLocation ) );
                OnPropertyChanged( nameof( CanExport ) );
            }
        }


        public bool CanExport =>
            !string.IsNullOrEmpty( FileName ) &&
            !string.IsNullOrEmpty( FileLocation );


        public ICommand ExportTransactionData { get; }


        public ExportModalViewModel( ITransactionService transactionService, AccountStore accountStore, IRenavigator renavigator )
        {
            ExportTransactionData = new ExportTransactionDataCommand( this, transactionService, accountStore, renavigator );
        }
    }
}
