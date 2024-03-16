using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Expensier.Domain.Models
{
    public class Account : DomainObject
    {
        [Column(Order = 1)]
        public required User User { get; set; }


        public virtual ICollection<Transaction>? TransactionList { get; set; }
        public virtual ICollection<Subscription>? SubscriptionList { get; set; }
        public virtual ICollection<CryptoAsset>? CryptoAssetList { get; set; }
        public virtual ICollection<PortfolioReturn>? PortfolioReturn { get; set; }
    }
}
