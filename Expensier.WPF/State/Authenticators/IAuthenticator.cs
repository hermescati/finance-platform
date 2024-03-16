using Expensier.Domain.Models;
using Expensier.Domain.Exceptions;
using Expensier.Domain.Services.Authentication;
using System;
using System.Threading.Tasks;


namespace Expensier.WPF.State.Authenticators
{
    /// <summary>
    /// Defines methods for authenticating users and managing their authentication state.
    /// </summary>
    public interface IAuthenticator
    {
        /// <summary>
        /// Gets the currently logged-in user's account.
        /// </summary>
        Account CurrentAccount { get; }

        /// <summary>
        /// Indicates whether the user is authenticated or not.
        /// </summary>
        bool Authenticated { get; }

        /// <summary>
        /// Event that is triggered when the authentication state changes.
        /// </summary>
        event Action StateChanged;


        /// <summary>
        /// Logs in a user with the provided credentials and stores the returned account as 
        /// the currently logged-in account for the session.
        /// </summary>
        /// <param name="email">The user's email.</param>
        /// <param name="password">The user's password.</param>
        /// <exception cref="UserNotFoundException">Thrown when the user does not exist.</exception>
        /// <exception cref="InvalidPasswordException">Thrown when the provided password does not match the user's account password.</exception>
        /// <exception cref="Exception">Thrown when the authentication fails.</exception>
        Task UserLogin( string email, string password );


        /// <summary>
        /// Logs out the currently authenticated user.
        /// </summary>
        Task UserLogout();


        /// <summary>
        /// Creates a new user account.
        /// </summary>
        /// <param name="firstName">The user's first name.</param>
        /// <param name="lastName">The user's last name.</param>
        /// <param name="email">The user's email address.</param>
        /// <param name="password">The user's password.</param>
        /// <param name="confirmPassword">Confirmation of the user's password.</param>
        /// <returns>The result of the registration process.</returns>
        Task<RegistrationResult> UserRegister( string firstName, string lastName, string email, string password, string confirmPassword );


        /// <summary>
        /// Resets the password of a user account.
        /// </summary>
        /// <param name="email">The user's email address.</param>
        /// <param name="newPassword">The new password.</param>
        /// <param name="confirmNewPassword">Confirmation of the new password.</param>
        /// <returns>The result of the password reset process.</returns>
        /// <exception cref="UserNotFoundException">Thrown when the user does not exist.</exception>
        /// <exception cref="Exception">Thrown when the password reset fails.</exception>
        Task<PasswordResetResult> ResetPassword( string email, string newPassword, string confirmNewPassword );
    }
}
