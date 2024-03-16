using Expensier.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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


        Task<Account> DeleteTransaction(
            Account currentAccount,
            Guid transactionID );


        Task ExportTransactionData(
            Account currentAccount,
            string filePath );
    }
}
