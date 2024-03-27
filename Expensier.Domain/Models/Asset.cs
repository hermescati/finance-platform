using System.ComponentModel.DataAnnotations.Schema;


namespace Expensier.Domain.Models
{
    public class Asset
    {
        [Column( Order = 2 )]
        public required string ID { get; set; }

        [Column( Order = 3 )]
        public required string Symbol { get; set; }

        [Column( Order = 4 )]
        public required string Name { get; set; }

        [Column( Order = 5 )]
        public required double CurrentPrice { get; set; }

        [Column( Order = 8 )]
        public double? PercentageChange { get; set; }

        [Column( Order = 10 )]
        public string? Image { get; set; }
    }
}
