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

        Task<RegistrationResult> userRegister(string firstName, string lastName, string email, string password, string confirmPassword);

        /// <summary>
        /// Login to the application.
        /// </summary>
        /// <param name="email">The user's name.</param>
        /// <param name="password">The user's password.</param>
        /// <exception cref="UserNotFoundException">Thrown if the user does not exist.</exception>
        /// <exception cref="InvalidPasswordException">Thrown if the password is invalid.</exception>
        /// <exception cref="Exception">Thrown if the login fails.</exception>
        Task userLogin(string email, string password);

        void userLogout();
    }
}
