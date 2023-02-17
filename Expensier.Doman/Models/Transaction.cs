using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Expensier.Domain.Models
{
    public class Transaction : DomainObject
    {
        public Account Account_ { get; set; } 
        public string? Transaction_Name { get; set; }
        public DateTime Process_Date { get; set; }
        public double Amount { get; set; }
        public string? Transaction_Type { get; set; }
        public bool Is_Credit { get; set; }
    }
}
