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

        [Column( Order = 6 )]
        public required double PurchasePrice { get; set; }

        [Column( Order = 7 )]
        public required double QuantityOwned { get; set; }

        [Column( Order = 9 )]
        public AssetType Category { get; set; }

        [Column( Order = 11 )]
        public DateTime? PurchaseDate { get; set; }
    }
}
