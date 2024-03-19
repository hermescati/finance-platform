using Expensier.Domain.Models;
using static Expensier.Domain.Models.Transaction;


namespace Expensier.Domain.Services.Transactions
{
    public interface ITransactionService
    {
        Task<Account> AddTransaction(
            Account currentAccount,
            string transactionName,
            TransactionCategory transactionCategory,
            double amount,
            DateTime processedDate );


        Transaction GetTransactionByID(
            Account currentAccount,
            Guid transactionID );


        Task<Account> DeleteTransaction(
            Account currentAccount,
            Guid transactionID );


        Task ExportTransactions(
            Account currentAccount,
            string filePath );
    }
}
