using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Expensier.Domain.Models
{
    public class Subscription : DomainObject
    {
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
        public required string Frequency { get; set; }

        [Column( Order = 6 )]
        public bool IsActive { get; set; }

        [Column( Order = 7 )]
        public DateTime DueDate { get; set; }
    }
}
