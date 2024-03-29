﻿using System.ComponentModel.DataAnnotations.Schema;


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


        [Column( Order = 1 )]
        public required Account User { get; set; }

        [Column( Order = 2 )]
        public required string Name { get; set; }

        [Column( Order = 3 )]
        public TransactionCategory Category { get; set; }

        [Column( Order = 4 )]
        public required double Amount { get; set; }

        [Column( Order = 5 )]
        public bool IsCredit { get; set; }

        [Column( Order = 6 )]
        public required DateTime ProcessedDate { get; set; }
    }
}
