using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Expensier.Domain.Models
{
    public class Account : DomainObject
    {
        public User AccountHolder { get; set; }
        public ICollection<Transaction>? TransactionList { get; set; }
        public ICollection<Subscription>? SubscriptionList { get; set; }
        public ICollection<CryptoAsset>? CryptoAssetList { get; set; }
        public ICollection<PortfolioReturn>? PortfolioReturn { get; set; }
    }
}
