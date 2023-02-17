using Expensier.Domain.Exceptions;
using Expensier.Domain.Models;
using Expensier.Doman.Services;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Expensier.Domain.Services.Authentication
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IAccountService _accountService;
        private readonly IPasswordHasher _passwordHasher;

        public AuthenticationService(IAccountService accountService, IPasswordHasher passwordHasher)
        {
            _accountService = accountService;
            _passwordHasher = passwordHasher;
        }

        public async Task<Account> userLogin(string email, string password)
        {
            Account storedAccount = await _accountService.GetByEmail(email);

            PasswordVerificationResult passwordResult = _passwordHasher.VerifyHashedPassword(storedAccount.Account_Holder_.Password_Hash, password);
            
            if (passwordResult != PasswordVerificationResult.Success)
            {
                throw new InvalidPasswordException(email, password);
            }

            return storedAccount;
        }

        public async Task<RegistrationResult> userRegister(string firstName, string lastName, string email, string password, string confirmPassword)
        {
            RegistrationResult result = RegistrationResult.Success;

            if(password != confirmPassword)
            {
                result = RegistrationResult.PasswordsDoNotMatch;
            }

            Account existingUser = await _accountService.GetByEmail(email); 
            if(existingUser != null)
            {
                result = RegistrationResult.EmailInUse;
            }

            if(result == RegistrationResult.Success)
            {
                string hashedPassword = _passwordHasher.HashPassword(password);

                User user = new User()
                {
                    First_Name = firstName,
                    Last_Name = lastName,
                    Email = email,
                    Password_Hash = hashedPassword,
                    Date_Joined = DateTime.Now,
                };

                Account account = new Account()
                {
                    Account_Holder_ = user
                };

                await _accountService.Create(account);
            }

            return result;
        }
    }
}
