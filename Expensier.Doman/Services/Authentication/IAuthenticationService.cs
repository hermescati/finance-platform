using Expensier.Domain.Models;
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
        PasswordsDoNotMatch,
        EmailInUse,
    }

    public interface IAuthenticationService
    {
        Task<RegistrationResult> userRegister(string firstName, string lastName, string email, string password, string confirmPassword);
        Task<Account> userLogin(string email, string password);
    }
}
