using System;
using System.Collections.Generic;
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

        public required Account User { get; set; }
        public required string Name { get; set; }
        public required string Plan { get; set; }
        public required double Amount { get; set; }
        public required string Frequency { get; set; }
        public bool IsActive { get; set; }
        public DateTime DueDate { get; set; }
    }
}
