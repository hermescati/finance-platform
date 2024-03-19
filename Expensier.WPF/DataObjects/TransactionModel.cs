using Expensier.Domain.Services.Transactions;
using Expensier.WPF.Commands.Transactions;
using Expensier.WPF.State.Accounts;
using Expensier.WPF.State.Navigators;
using Expensier.WPF.ViewModels;
using System;
using System.Windows.Input;
using static Expensier.Domain.Models.Transaction;


namespace Expensier.WPF.DataObjects
{
    public class TransactionModel : ViewModelBase
    {
        private readonly AccountStore _accountStore;
        private readonly ITransactionService _transactionService;
        private readonly IRenavigator _renavigator;


        public Guid ID { get; set; }
        public string Name { get; set; }
        public TransactionCategory Category { get; set; }
        public double Amount { get; set; }
        public bool IsCredit { get; }
        public DateTime ProcessedDate { get; set; }
        public ICommand DeleteCommand { get; }


        public TransactionModel(
            Guid id,
            string name,
            TransactionCategory category,
            double amount,
            bool isCredit,
            DateTime processedDate,
            AccountStore accountStore,
            ITransactionService transactionService,
            IRenavigator renavigator )
        {
            ID = id;
            Name = name;
            Category = category;
            Amount = amount;
            IsCredit = isCredit;
            ProcessedDate = processedDate;

            _accountStore = accountStore;
            _transactionService = transactionService;
            _renavigator = renavigator;

            DeleteCommand = new DeleteTransactionCommand( this, _transactionService, _renavigator, _accountStore );
        }
    }
}
