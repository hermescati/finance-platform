using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Expensier.Domain.Models
{
    public class Transaction : DomainObject
    {
        public enum TransactionCategory
        {
            Income,
            Rent,
            Utilities,
            Food,
            Travel,
            Subscription,
            Shopping,
        }

        public required Account User { get; set; }
        public required string Name { get; set; }
        public required string Category { get; set; }
        public required double Amount { get; set; }
        public bool IsCredit { get; set; }
        public required DateTime ProcessedDate { get; set; }
    }
}
