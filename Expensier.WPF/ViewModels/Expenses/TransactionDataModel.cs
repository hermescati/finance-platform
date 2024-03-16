using Expensier.Domain.Services.Transactions;
using Expensier.WPF.Commands;
using Expensier.WPF.Enums;
using Expensier.WPF.State.Accounts;
using Expensier.WPF.State.Navigators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using static Expensier.Domain.Models.Transaction;


namespace Expensier.WPF.ViewModels.Expenses
{
    public class TransactionDataModel : ViewModelBase
    {
        private readonly ITransactionService _transactionService;
        private readonly AccountStore _accountStore;
        private readonly IRenavigator _renavigator;


        public Guid ID { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public double Amount { get; set; }
        public bool IsCredit { get; }
        public DateTime ProcessedDate { get; set; }
        public ICommand DeleteCommand { get; }


        public TransactionDataModel( DateTime processDate, double amount )
        {
            ProcessedDate = processDate;
            Amount = amount;
        }

        public TransactionDataModel(
            Guid id,
            string name,
            string category,
            double amount,
            bool isCredit,
            DateTime processedDate,
            ITransactionService transactionService,
            AccountStore accountStore,
            IRenavigator renavigator )
        {
            ID = id;
            Name = name;
            Category = category;
            Amount = amount;
            IsCredit = isCredit;
            ProcessedDate = processedDate;

            _transactionService = transactionService;
            _accountStore = accountStore;
            _renavigator = renavigator;

            DeleteCommand = new DeleteTransactionCommand( this, _transactionService, _accountStore, _renavigator );
        }
    }
}
