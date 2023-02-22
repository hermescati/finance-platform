using Expensier.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Expensier.Domain.Services.Transactions
{
    public enum TransactionType
    {
        Salary,
        Rent, 
        Utilities,
        Food, 
        Travel,
        Subscriptions,
        Shopping
    }

    public interface ITransactionService
    {
        Task<Account> AddTransaction(
            Account currentAccount,
            string transactionName,
            DateTime processDate,
            double amount,
            TransactionType transactionType);
    }
}
