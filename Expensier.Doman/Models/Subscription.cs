using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Expensier.Domain.Models
{
    public class Subscription : DomainObject
    {
        public Account AccountHolder { get; set; }
        public string? CompanyName { get; set; }
        public string? SubscriptionPlan { get; set; }
        public DateTime DueDate { get; set; }
        public double Amount { get; set; }
        public string? SubscriptionType { get; set; }
    }
}
