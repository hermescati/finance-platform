using Expensier.Domain.Models;
using Expensier.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Expensier.Domain.Services.Authentication
{
    public enum RegistrationResult
    {
        Success,
        PasswordTooShort,
        PasswordsDoNotMatch,
        EmailInUse,
    }

    public interface IAuthenticationService
    {
        Task<RegistrationResult> userRegister(string firstName, string lastName, string email, string password, string confirmPassword);
        
        /// <summary>
        /// Get an account for a user's credentials.
        /// </summary>
        /// <param name="email">The user's email.</param>
        /// <param name="password">The user's password.</param>
        /// <returns>The account for the user</returns>
        /// <exception cref="UserNotFoundException">Thrown if the user does not exist.</exception>
        /// <exception cref="InvalidPasswordException">Thrown if the password is invalid.</exception>
        /// <exception cref="Exception">Thrown if the login fails.</exception>
        Task<Account> userLogin(string email, string password);
    }
}
