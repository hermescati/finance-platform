using Expensier.Domain.Models;
using Expensier.Domain.Services.Authentication;
using Expensier.WPF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Expensier.WPF.State.Authenticators
{
    public class Authenticator : ObservableObject, IAuthenticator
    {
        private readonly IAuthenticationService _authenticationService;

        public Authenticator(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        private Account _currentUser;
        public Account CurrentUser 
        { 
            get
            {
                return _currentUser;
            }
            private set
            {
                _currentUser = value;
                OnPropertyChanged(nameof(CurrentUser));
                OnPropertyChanged(nameof(Authenticated));
            }
        }

        public bool Authenticated => CurrentUser != null;

        public async Task<bool> userLogin(string email, string password)
        {
            bool success = true;

            try
            {
                CurrentUser = await _authenticationService.userLogin(email, password);
            }
            catch (Exception)
            {

                success = false;
            }

            return success;
        }

        public void userLogout()
        {
            CurrentUser = null;
        }

        public async Task<RegistrationResult> userRegister(string firstName, string lastName, string email, string password, string confirmPassword)
        {
            return await _authenticationService.userRegister(firstName, lastName, email, password, confirmPassword);
        }
    }
}
