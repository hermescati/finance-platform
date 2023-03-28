using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Expensier.Domain.Models
{
    public class PortfolioReturn : DomainObject
    {
        public Account AccountHolder { get; set; }
        public DateTime RecordedDate { get; set; }
        public double ReturnPercentage { get; set; }
    }
}
