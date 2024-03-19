using Expensier.Domain.Services.Transactions;
using Expensier.WPF.Commands.Transactions;
using Expensier.WPF.State.Accounts;
using Expensier.WPF.State.Navigators;
using System;
using System.Windows.Input;
using static Expensier.Domain.Models.Transaction;


namespace Expensier.WPF.ViewModels.Expenses
{
    public class TransactionDataModel : ViewModelBase
    {
        private readonly ITransactionService _transactionService;
        private readonly IRenavigator _renavigator;
        private readonly AccountStore _accountStore;


        public Guid ID { get; set; }
        public string Name { get; set; }
        public TransactionCategory Category { get; set; }
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
            TransactionCategory category,
            double amount,
            bool isCredit,
            DateTime processedDate,
            ITransactionService transactionService,
            IRenavigator renavigator,
            AccountStore accountStore )
        {
            ID = id;
            Name = name;
            Category = category;
            Amount = amount;
            IsCredit = isCredit;
            ProcessedDate = processedDate;

            _transactionService = transactionService;
            _renavigator = renavigator;
            _accountStore = accountStore;

            DeleteCommand = new DeleteTransactionCommand( this, _transactionService, _renavigator, _accountStore );
        }
    }
}
