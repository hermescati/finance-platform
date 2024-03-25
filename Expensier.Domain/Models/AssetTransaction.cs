using System.ComponentModel.DataAnnotations.Schema;


namespace Expensier.Domain.Models
{
    public class AssetTransaction : DomainObject
    {
        public enum AssetType
        {
            Stock,
            Cryptocurrency
        }


        [Column( Order = 1 )]
        public required Account User { get; set; }

        [Column( Order = 2 )]
        public required Asset Asset { get; set; }

        [Column( Order = 5 )]
        public required double PurchasePrice { get; set; }

        [Column( Order = 6 )]
        public required double QuantityOwned { get; set; }

        [Column( Order = 8 )]
        public AssetType Category { get; set; }

        [Column( Order = 10 )]
        public DateTime? PurchaseDate { get; set; }
    }
}
