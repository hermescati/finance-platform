using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Expensier.Domain.Models
{
    public class DomainObject
    {
        [Column(Order = 0)]
        public Guid ID { get; set; }
    }
}
