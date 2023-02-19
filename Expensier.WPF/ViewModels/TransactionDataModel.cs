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
        public bool IsCredit { get; set; }

        public TransactionDataModel(string transactionName, DateTime processDate, double amount, bool isCredit)
        {
            TransactionName = transactionName;
            ProcessDate = processDate;
            Amount = amount;
            IsCredit = isCredit;
        }
    }
}
