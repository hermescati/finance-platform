using Expensier.Domain.Models;
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
        Task<bool> userLogin(string email, string password);
        void userLogout();
    }
}
