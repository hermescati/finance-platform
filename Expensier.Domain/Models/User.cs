using System.ComponentModel.DataAnnotations.Schema;


namespace Expensier.Domain.Models
{
    public class User : DomainObject
    {
        [Column( Order = 1 )]
        public required string FirstName { get; set; }

        [Column( Order = 2 )]
        public required string LastName { get; set; }

        [Column( Order = 3 )]
        public required string Email { get; set; }

        [Column( Order = 4 )]
        public required string PasswordHash { get; set; }

        [Column( Order = 5 )]
        public required DateTime JoinDate { get; set; }
    }
}
