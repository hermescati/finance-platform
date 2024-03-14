using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Expensier.Domain.Models
{
    public class CryptoAsset : DomainObject
    {
        public Account AccountHolder { get; set; }
        public Crypto Asset { get; set; }
        public double PurchasePrice { get; set; }
        public double Coins { get; set; }
    }
}
