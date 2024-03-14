using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Expensier.Domain.Models
{
    public class Crypto
    {
        public string Symbol { get; set; }
        public string? Name { get; set; }
        public double? Price { get; set; }
        public double? ChangesPercentage { get; set; }
    }
}
