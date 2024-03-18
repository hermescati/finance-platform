using System.ComponentModel.DataAnnotations.Schema;


namespace Expensier.Domain.Models
{
    public class Subscription : DomainObject
    {
        public enum SubscriptionStatus
        {
            Active,
            Cancelled
        }

        public enum SubscriptionFrequency
        {
            Monthly,
            Annual
        }

        public enum CompanyName
        {
            Figma,
            Adobe,
            Netflix,
            Disney,
            Spotify,
            Apple
        }


        [Column( Order = 1 )]
        public required Account User { get; set; }

        [Column( Order = 2 )]
        public required string Name { get; set; }

        [Column( Order = 3 )]
        public required string Plan { get; set; }

        [Column( Order = 4 )]
        public required double Amount { get; set; }

        [Column( Order = 5 )]
        public SubscriptionFrequency Frequency { get; set; }

        [Column( Order = 6 )]
        public SubscriptionStatus Status { get; set; }

        [Column( Order = 7 )]
        public DateTime? DueDate { get; set; }
    }
}
