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

    public enum PasswordResetResult
    {
        Success,
        UserNotAuthenticated,
        PasswordsDoNotMatch,
        SameOldPassword
    }

    public interface IAuthenticationService
    {
        /// <summary>
        /// Retrieve user account.
        /// </summary>
        /// <param name="email">The user's email.</param>
        /// <returns>The account of the user.</returns>
        /// <exception cref="UserNotFoundException">Thrown if the user does not exist.</exception>
        /// <exception cref="Exception">Thrown if the login fails.</exception>
        Task<Account> getAccount(string email);

        /// <summary>
        /// Accesses user account.
        /// </summary>
        /// <param name="email">User's email.</param>
        /// <param name="password">User's password.</param>
        /// <returns>The current account that the user accessed.</returns>
        /// <exception cref="UserNotFoundException">Thrown if the user does not exist.</exception>
        /// <exception cref="InvalidPasswordException">Thrown if the entered password does not match the one stored in the account.</exception>
        /// <exception cref="Exception">Thrown if the login fails.</exception>
        Task<Account> userLogin(string email, string password);

        /// <summary>
        /// Creates a new account.
        /// </summary>
        /// <param name="firstName">User's first name.</param>
        /// <param name="lastName">User's last name.</param>
        /// <param name="email">User's email.</param>
        /// <param name="password">User's password.</param>
        /// <param name="confirmPassword">User's confirm password.</param>
        /// <returns>The result of the registration process.</returns>
        Task<RegistrationResult> userRegister(string firstName, string lastName, string email, string password, string confirmPassword);

        /// <summary>
        /// Resets the password of an account.
        /// </summary>
        /// <param name="email">User's email.</param>
        /// <param name="password">User's new password.</param>
        /// <param name="confirmPassword">User's new confirm password.</param>
        /// <returns>The result of the reseting process.</returns>
        /// <exception cref="UserNotFoundException">Thrown if the entered email does not exist.</exception>
        /// <exception cref="Exception">Thrown if the login fails.</exception>
        Task<PasswordResetResult> resetPassword(string email, string newPassword, string confirmNewPassword);
    }
}
