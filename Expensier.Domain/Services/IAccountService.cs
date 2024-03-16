using Expensier.Domain.Models;
using Expensier.Domain.Exceptions;


namespace Expensier.Domain.Services
{
    public interface IAccountService : IDataService<Account>
    {
        /// <summary>
        /// Retrieves an account by its associated email.
        /// </summary>
        /// <param name="email">The email associated with the account.</param>
        /// <returns>The account corresponding to the provided email.</returns>
        /// <exception cref="UserNotFoundException">Thrown when the user does not exist.</exception>
        Task<Account> GetByEmail( string email );
    }
}
