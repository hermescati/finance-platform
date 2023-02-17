using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Expensier.Domain.Models
{
    public class Subscription : DomainObject
    {
        public Account Account_ { get; set; }
        public string? Company_Name { get; set; }
        public string? Subscription_Plan { get; set; }
        public DateTime Due_Date { get; set; }
        public double Amount { get; set; }
        public string? Subscription_Type { get; set; }
    }
}
