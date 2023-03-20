using Expensier.Domain.Models;
using Expensier.Domain.Exceptions;
using Expensier.Domain.Services.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Expensier.WPF.State.Authenticators
{
    public interface IAuthenticator
    {
        Account CurrentAccount { get; }
        bool Authenticated { get; }
        event Action StateChanged;


        /// <summary>
        /// Accesses user account.
        /// </summary>
        /// <param name="email">User's email.</param>
        /// <param name="password">User's password.</param>
        /// <returns>The current account that the user accessed.</returns>
        /// <exception cref="UserNotFoundException">Thrown if the user does not exist.</exception>
        /// <exception cref="InvalidPasswordException">Thrown if the entered password does not match the one stored in the account.</exception>
        /// <exception cref="Exception">Thrown if the login fails.</exception>
        Task userLogin(string email, string password);

        /// <summary>
        /// Logs out the current user.
        /// </summary>
        /// <returns>Sets the current account of the user to null.</returns>
        Task userLogout();

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
        Task<PasswordResetResult> resetPassword(string email, string oldPassword, string newPassword, string confirmNewPassword);
    }
}
