using Expensier.Domain.Models;
using Expensier.Domain.Services.Authentication;
using Expensier.WPF.State.Accounts;
using System;
using System.Threading.Tasks;


namespace Expensier.WPF.State.Authenticators
{
    public class Authenticator : IAuthenticator
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly AccountStore _accountStore;


        public Authenticator( IAuthenticationService authenticationService, AccountStore accountStore )
        {
            _authenticationService = authenticationService;
            _accountStore = accountStore;
        }


        public Account CurrentAccount
        {
            get => _accountStore.CurrentAccount;
            private set
            {
                _accountStore.CurrentAccount = value;
                StateChanged?.Invoke();
            }
        }

        public bool Authenticated => CurrentAccount != null;

        public event Action StateChanged;


        public async Task UserLogin( string email, string password )
        {
            CurrentAccount = await _authenticationService.UserLogin( email, password );
        }


        public async Task UserLogout()
        {
            try
            {
                CurrentAccount = null;
            }
            catch ( Exception )
            {
                throw;
            }
        }


        public async Task<RegistrationResult> UserRegister( string firstName, string lastName, string email, string password, string confirmPassword )
        {
            return await _authenticationService.UserRegister( firstName, lastName, email, password, confirmPassword );
        }


        public async Task<PasswordResetResult> ResetPassword( string email, string newPassword, string confirmNewPassword )
        {
            return await _authenticationService.ResetPassword( email, newPassword, confirmNewPassword );
        }
    }
}
