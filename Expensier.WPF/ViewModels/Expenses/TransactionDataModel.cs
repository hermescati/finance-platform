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

namespace Expensier.WPF.ViewModels
{
    public class TransactionDataModel : ViewModelBase
    {
        private readonly ITransactionService _transactionService;
        private readonly AccountStore _accountStore;
        private readonly IRenavigator _renavigator;

        public int Id { get; set; }
        public string TransactionName { get; set; }
        public string TransactionType { get; set; }
        public double Amount { get; set; }
        public DateTime ProcessDate { get; set; }
        public string DateFormat { get; set; }
        public bool IsCredit { get; }
        public IEnumerable<string> Type { get; set; }
        public ICommand DeleteCommand { get; }


        public TransactionDataModel(DateTime processDate, double amount)
        {
            ProcessDate = processDate;
            Amount = amount;
        }

        public TransactionDataModel(string dateFormat, double amount)
        {
            DateFormat = dateFormat;
            Amount = amount;
        }

        public TransactionDataModel(string dateFormat, IEnumerable<string> transactionType)
        {
            DateFormat = dateFormat;
            Type = transactionType;
        }

        public TransactionDataModel(
            int id, 
            string transactionName, 
            DateTime processDate, 
            double amount, 
            string transactionType, 
            bool isCredit, 
            ITransactionService transactionService,
            AccountStore accountStore,
            IRenavigator renavigator)
        {
            Id = id;
            TransactionName = transactionName;
            ProcessDate = processDate;
            Amount = amount;
            TransactionType = transactionType;
            IsCredit = isCredit;

            _transactionService = transactionService;
            _accountStore = accountStore;
            _renavigator = renavigator;

            DeleteCommand = new DeleteTransactionCommand(this, _transactionService, _accountStore, _renavigator);
        }
    }
}
