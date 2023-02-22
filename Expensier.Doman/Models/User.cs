using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Expensier.Domain.Models
{
    public class User : DomainObject
    {
        public string? First_Name { get; set; }
        public string? Last_Name { get; set;}
        public string? Email { get; set; }
        public string? Password_Hash { get; set; }
        public DateTime Date_Joined { get; set; }
    }
}
