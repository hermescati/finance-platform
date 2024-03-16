using Expensier.Domain.Exceptions;
using Expensier.Domain.Models;
using Microsoft.AspNet.Identity;


namespace Expensier.Domain.Services.Authentication
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IAccountService _accountService;
        private readonly IPasswordHasher _passwordHasher;


        public AuthenticationService( IAccountService accountService, IPasswordHasher passwordHasher )
        {
            _accountService = accountService;
            _passwordHasher = passwordHasher;
        }


        public async Task<Account> GetAccount( string email )
        {
            Account userAccount = await _accountService.GetByEmail( email );
            if ( userAccount == null )
            {
                throw new UserNotFoundException( email );
            }

            return userAccount;
        }


        public async Task<Account> UserLogin( string email, string password )
        {
            Account storedAccount = await GetAccount( email );
            PasswordVerificationResult passwordResult = _passwordHasher.VerifyHashedPassword( storedAccount.User.PasswordHash, password );

            if ( passwordResult != PasswordVerificationResult.Success )
            {
                throw new InvalidPasswordException( email, password );
            }

            return storedAccount;
        }


        public async Task<RegistrationResult> UserRegister( string firstName, string lastName, string email, string password, string confirmPassword )
        {
            if ( password.Length < 8 )
                return RegistrationResult.PasswordTooShort;

            if ( password != confirmPassword )
                return RegistrationResult.PasswordsDoNotMatch;

            Account existingUser = await _accountService.GetByEmail( email );
            if ( existingUser != null )
                return RegistrationResult.EmailInUse;


            User newUser = new User()
            {
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                PasswordHash = _passwordHasher.HashPassword( password ),
                JoinDate = DateTime.Now,
            };

            Account newAccount = new Account()
            {
                User = newUser
            };

            await _accountService.Create( newAccount );

            return RegistrationResult.Success;
        }


        public async Task<PasswordResetResult> ResetPassword( string email, string newPassword, string confirmPassword )
        {
            if ( newPassword != confirmPassword )
                return PasswordResetResult.PasswordsDoNotMatch;

            Account userAccount = await GetAccount( email );

            PasswordVerificationResult passwordResult = _passwordHasher.VerifyHashedPassword( userAccount.User.PasswordHash, newPassword );
            if ( passwordResult == PasswordVerificationResult.Success )
                return PasswordResetResult.SameOldPassword;


            userAccount.User.PasswordHash = _passwordHasher.HashPassword( newPassword );

            Account account = new Account()
            {
                User = userAccount.User
            };

            await _accountService.Update( userAccount.ID, account );

            return PasswordResetResult.Success;
        }
    }
}
