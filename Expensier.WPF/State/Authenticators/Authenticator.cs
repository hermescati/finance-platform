using Expensier.Domain.Models;
using Expensier.Domain.Services.Authentication;
using Expensier.WPF.State.Accounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Expensier.WPF.State.Authenticators
{
    public class Authenticator : IAuthenticator
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly AccountStore _accountStore;

        public Authenticator(IAuthenticationService authenticationService, AccountStore accountStore)
        {
            _authenticationService = authenticationService;
            _accountStore = accountStore;
        }

        public Account CurrentAccount 
        { 
            get
            {
                return _accountStore.CurrentAccount;
            }
            private set
            {
                _accountStore.CurrentAccount = value;
                StateChanged?.Invoke();
            }
        }

        public bool Authenticated => CurrentAccount != null;

        public event Action StateChanged;

        public async Task userLogin(string email, string password)
        {
            CurrentAccount = await _authenticationService.userLogin(email, password);
        }

        public void userLogout()
        {
            CurrentAccount = null;
        }

        public async Task<RegistrationResult> userRegister(string firstName, string lastName, string email, string password, string confirmPassword)
        {
            return await _authenticationService.userRegister(firstName, lastName, email, password, confirmPassword);
        }
    }
}
