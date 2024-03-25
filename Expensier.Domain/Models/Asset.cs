using System.ComponentModel.DataAnnotations.Schema;


namespace Expensier.Domain.Models
{
    public class Asset
    {
        [Column( Order = 2 )]
        public required string Symbol { get; set; }

        [Column( Order = 3 )]
        public required string Name { get; set; }

        [Column( Order = 4 )]
        public required double CurrentPrice { get; set; }

        [Column( Order = 7 )]
        public double? PercentageChange { get; set; }

        [Column( Order = 9 )]
        public string? Image { get; set; }
    }
}
