using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Expensier.Domain.Models
{
    public class CryptoAsset : DomainObject
    {
        public Account Account_ { get; set; }
        public Crypto Crypto { get; set; }
        public double Purchase_Price { get; set; }
        public double Amount { get; set; }
    }
}
