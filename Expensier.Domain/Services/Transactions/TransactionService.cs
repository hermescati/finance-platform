using Expensier.Domain.Models;
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
        private readonly IDataService<Transaction> _transactionService;

        public TransactionService(IDataService<Account> accountService, IDataService<Transaction> transactionService)
        {
            _accountService = accountService;
            _transactionService = transactionService;
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
                AccountHolder = currentAccount,
                TransactionName = transactionName,
                ProcessDate = processDate,
                Amount = amount,
                TransactionType = transactionType.ToString(),
                IsCredit = IsCredit
            };

            currentAccount.TransactionList.Add(newTransaction);

            await _accountService.Update(currentAccount.ID, currentAccount);

            return currentAccount;
        }


        public async Task<Account> DeleteTransaction(Account currentAccount, Guid transactionID)
        { 
            currentAccount.TransactionList
                .Remove(currentAccount.TransactionList
                .FirstOrDefault((transaction) => transaction.ID == transactionID));

            await _accountService.Update(currentAccount.ID, currentAccount);

            await _transactionService.Delete(transactionID);

            return currentAccount;
        }
    }
}
