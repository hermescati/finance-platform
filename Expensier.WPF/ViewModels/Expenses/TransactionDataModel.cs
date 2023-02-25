using Expensier.Domain.Services.Transactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Expensier.WPF.ViewModels
{
    public class TransactionDataModel : ViewModelBase
    {
        public string TransactionName { get; set; }
        public DateTime ProcessDate { get; set; }
        public double Amount { get; set; }
        public string TransactionType { get; set; }
        public bool IsCredit { get; }

        public TransactionDataModel(string transactionName, DateTime processDate, double amount, string transactionType, bool isCredit)
        {
            TransactionName = transactionName;
            ProcessDate = processDate;
            Amount = amount;
            TransactionType = transactionType;
            IsCredit = isCredit;
        }
    }
}
