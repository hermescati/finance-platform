using System.ComponentModel.DataAnnotations.Schema;


namespace Expensier.Domain.Models
{
    public class Account : DomainObject
    {
        [Column( Order = 1 )]
        public required User User { get; set; }


        public virtual ICollection<Transaction>? TransactionList { get; set; }
        public virtual ICollection<Subscription>? SubscriptionList { get; set; }
        public virtual ICollection<AssetTransaction>? AssetList { get; set; }
        public virtual ICollection<PortfolioReturn>? PortfolioReturn { get; set; }
    }
}
