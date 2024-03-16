using System.ComponentModel.DataAnnotations.Schema;


namespace Expensier.Domain.Models
{
    public class DomainObject
    {
        [Column( Order = 0 )]
        public Guid ID { get; set; }
    }
}
