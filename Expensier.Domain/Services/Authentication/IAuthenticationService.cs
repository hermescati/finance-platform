using Expensier.Domain.Models;
using Expensier.Domain.Exceptions;


namespace Expensier.Domain.Services.Authentication
{
    /// <summary>
    /// Represents the result of a user's registration attempt.
    /// </summary>
    public enum RegistrationResult
    {
        Success,
        PasswordTooShort,
        PasswordsDoNotMatch,
        EmailInUse,
    }

    /// <summary>
    /// Represents the result of a password reset attempt.
    /// </summary>
    public enum PasswordResetResult
    {
        Success,
        UserNotAuthenticated,
        PasswordsDoNotMatch,
        SameOldPassword
    }


    /// <summary>
    /// Defines operations for authentication and user account management.
    /// </summary>
    public interface IAuthenticationService
    {
        /// <summary>
        /// Retrieves an account based on the provided email address.
        /// </summary>
        /// <param name="email">Provided email address associated with the account.</param>
        /// <returns>The account corresponding to the provided email address.</returns>
        /// <exception cref="UserNotFoundException">Thrown when the user does not exist.</exception>
        /// <exception cref="Exception">Thrown when the retrieval process fails.</exception>
        Task<Account> GetAccount(string email);


        /// <summary>
        /// Authenticates a user with the provided credentials.
        /// </summary>
        /// <params name="email">The user's email address.</param>
        /// <params name="password">The user's password.</param>
        /// <returns>The authenticated user's account.</returns>
        /// <exception cref="UserNotFoundException">Thrown when the user does not exist.</exception>
        /// <exception cref="InvalidPasswordException">Thrown when the provided password does not match the user's account password.</exception>
        /// <exception cref="Exception">Thrown when the authentication fails.</exception>
        Task<Account> UserLogin(string email, string password);


        /// <summary>
        /// Creates a new user account.
        /// </summary>
        /// <param name="firstName">The user's first name.</param>
        /// <param name="lastName">The user's last name.</param>
        /// <param name="email">The user's email address.</param>
        /// <param name="password">The user's password.</param>
        /// <param name="confirmPassword">Confirmation of the user's password.</param>
        /// <returns>The result of the registration process.</returns>
        Task<RegistrationResult> UserRegister(string firstName, string lastName, string email, string password, string confirmPassword);


        /// <summary>
        /// Resets the password of a user account.
        /// </summary>
        /// <param name="email">The user's email address.</param>
        /// <param name="newPassword">The new password.</param>
        /// <param name="confirmNewPassword">Confirmation of the new password.</param>
        /// <returns>The result of the password reset process.</returns>
        /// <exception cref="UserNotFoundException">Thrown when the user does not exist.</exception>
        /// <exception cref="Exception">Thrown when the password reset fails.</exception>
        Task<PasswordResetResult> ResetPassword(string email, string newPassword, string confirmNewPassword);
    }
}
