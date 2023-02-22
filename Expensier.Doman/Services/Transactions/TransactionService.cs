using Expensier.Domain.Models;
using Expensier.Doman.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Expensier.Domain.Services.Transactions
{
    public class TransactionService : ITransactionService
    {
        private readonly IDataService<Account> _accountService;

        public TransactionService(IDataService<Account> accountService)
        {
            _accountService = accountService;
        }

        public async Task<Account> AddTransaction(Account currentAccount, string transactionName, DateTime processDate, double amount, TransactionType transactionType)
        {
            bool IsCredit = true;

            if (transactionType == TransactionType.Salary)
            {
                IsCredit = false;
            }

            Transaction newTransaction = new Transaction()
            {
                Account_ = currentAccount,
                Transaction_Name = transactionName,
                Process_Date = processDate,
                Amount = amount,
                Transaction_Type = transactionType.ToString(),
                Is_Credit = IsCredit
            };

            currentAccount.TransactionList.Add(newTransaction);

            await _accountService.Update(currentAccount.Id, currentAccount);

            return currentAccount;
        }
    }
}
