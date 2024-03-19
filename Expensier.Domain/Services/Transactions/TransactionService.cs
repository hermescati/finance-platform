using Expensier.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Expensier.Domain.Models.Transaction;

namespace Expensier.Domain.Services.Transactions
{
    public class TransactionService : ITransactionService
    {
        private readonly IDataService<Account> _accountService;
        private readonly IDataService<Transaction> _transactionService;


        public TransactionService( IDataService<Account> accountService, IDataService<Transaction> transactionService )
        {
            _accountService = accountService;
            _transactionService = transactionService;
        }


        public async Task<Account> AddTransaction( 
            Account currentAccount, 
            string transactionName, 
            TransactionCategory transactionCategory, 
            double amount, 
            DateTime processedDate )
        {
            bool isCredit = transactionCategory != TransactionCategory.Income;

            Transaction newTransaction = new Transaction()
            {
                User = currentAccount,
                Name = transactionName,
                Amount = amount,
                Category = transactionCategory,
                IsCredit = isCredit,
                ProcessedDate = processedDate,
            };

            currentAccount.TransactionList?.Add( newTransaction );

            await _accountService.Update( currentAccount.ID, currentAccount );

            return currentAccount;
        }


        public async Task<Account> DeleteTransaction( Account currentAccount, Guid transactionID )
        {
            currentAccount.TransactionList
                .Remove( currentAccount.TransactionList.FirstOrDefault( ( transaction ) => transaction.ID == transactionID ) );

            await _accountService.Update( currentAccount.ID, currentAccount );
            await _transactionService.Delete( transactionID );

            return currentAccount;
        }


        public async Task ExportTransactionData( Account currentAccount, string filePath )
        {
            IEnumerable<Transaction> transactionList = currentAccount.TransactionList;

            using ( StreamWriter writer = new StreamWriter( filePath ) )
            {
                await writer.WriteLineAsync( "Name, Category, Amount, Processed_Date" );

                foreach ( Transaction transaction in transactionList )
                {
                    string transactionAmount = string.Format( "{0}", transaction.Amount );
                    transactionAmount = transaction.IsCredit ? "- $" + transactionAmount : "+ $" + transactionAmount;

                    writer.WriteLine( string.Format( "{0}, {1}, {2}, {3}",
                        transaction.Name,
                        transaction.Category,
                        transactionAmount,
                        transaction.ProcessedDate.ToString( "dd/MM/yyyy" ) ) );
                }
            }
        }
    }
}
