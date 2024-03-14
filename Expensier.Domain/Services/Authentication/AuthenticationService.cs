using Expensier.Domain.Exceptions;
using Expensier.Domain.Models;
using Expensier.Domain.Services;
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

        public async Task<Account> getAccount(string email)
        {
            Account userAccount = await _accountService.GetByEmail(email);

            if (userAccount == null)
            {
                throw new UserNotFoundException(email);
            }

            return userAccount;
        }

        public async Task<Account> userLogin(string email, string password)
        {
            Account storedAccount = await getAccount(email);
            PasswordVerificationResult passwordResult = verifyPassword(storedAccount.AccountHolder.PasswordHash, password);
            
            if (passwordResult != PasswordVerificationResult.Success)
            {
                throw new InvalidPasswordException(email, password);
            }

            return storedAccount;
        }

        public async Task<RegistrationResult> userRegister(string firstName, string lastName, string email, string password, string confirmPassword)
        {
            RegistrationResult result = RegistrationResult.Success;

            if (password.Length < 8)
            {
                result = RegistrationResult.PasswordTooShort;
            }

            if (password != confirmPassword)
            {
                result = RegistrationResult.PasswordsDoNotMatch;
            }

            Account existingUser = await _accountService.GetByEmail(email);

            if (existingUser != null)
            {
                result = RegistrationResult.EmailInUse;
            }

            if (result == RegistrationResult.Success)
            {
                string hashedPassword = hashPassword(password);

                User user = new User()
                {
                    FirstName = firstName,
                    LastName = lastName,
                    Email = email,
                    PasswordHash = hashedPassword,
                    JoinDate = DateTime.Now,
                };

                Account account = new Account()
                {
                    AccountHolder = user
                };

                await _accountService.Create(account);
            }

            return result;
        }

        public async Task<PasswordResetResult> resetPassword(string email, string newPassword, string confirmPassword)
        {
            Account userAccount = await getAccount(email);
            PasswordResetResult result = PasswordResetResult.Success;

            if (newPassword != confirmPassword)
            {
                result = PasswordResetResult.PasswordsDoNotMatch;
            }

            PasswordVerificationResult passwordResult = verifyPassword(userAccount.AccountHolder.PasswordHash, newPassword);
            if (passwordResult == PasswordVerificationResult.Success)
            {
                result = PasswordResetResult.SameOldPassword;
            }

            if (result == PasswordResetResult.Success)
            {
                string newHashedPassword = hashPassword(newPassword);

                User user = userAccount.AccountHolder;
                user.PasswordHash = newHashedPassword;

                Account account = new Account()
                {
                    AccountHolder = user
                };

                await _accountService.Update(userAccount.ID, account);
            }

            return result;
        }

        public string hashPassword(string password)
        {
            return _passwordHasher.HashPassword(password);
        }

        public PasswordVerificationResult verifyPassword(string userPassword, string enteredPassword)
        {
            return _passwordHasher.VerifyHashedPassword(userPassword, enteredPassword);
        }
    }
}
