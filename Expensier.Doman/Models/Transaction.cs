using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Expensier.Domain.Models
{
    public class Transaction : DomainObject
    {
        public Account AccountHolder { get; set; } 
        public string? TransactionName { get; set; }
        public DateTime ProcessDate { get; set; }
        public double Amount { get; set; }
        public string? TransactionType { get; set; }
        public bool IsCredit { get; set; }
    }
}
