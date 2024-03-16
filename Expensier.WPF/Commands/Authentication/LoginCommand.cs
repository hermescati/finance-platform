using Expensier.Domain.Exceptions;
using Expensier.WPF.State.Authenticators;
using Expensier.WPF.State.Navigators;
using Expensier.WPF.ViewModels;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading.Tasks;


namespace Expensier.WPF.Commands.Authentication
{
    public class LoginCommand : AsyncCommandBase
    {
        private readonly LoginViewModel _loginViewModel;
        private readonly IAuthenticator _authenticator;
        private readonly IRenavigator _renavigator;
        private readonly SidePanelViewModel _sidePanelViewModel;


        public LoginCommand(
            LoginViewModel loginViewModel,
            IAuthenticator authenticator,
            IRenavigator renavigator,
            SidePanelViewModel sidePanelViewModel )
        {
            _loginViewModel = loginViewModel;
            _authenticator = authenticator;
            _renavigator = renavigator;
            _sidePanelViewModel = sidePanelViewModel;

            _loginViewModel.PropertyChanged += LoginViewModel_PropertyChanged;
        }


        public override bool CanExecute( object parameter ) => 
            _loginViewModel.CanLogin 
            && base.CanExecute( parameter );


        public override async Task ExecuteAsync( object parameter )
        {
            _loginViewModel.ErrorMessage = string.Empty;

            try
            {
                await _authenticator.UserLogin( _loginViewModel.Email, _loginViewModel.Password );
                _sidePanelViewModel.GreetingMessage = $"Hello, {_authenticator.CurrentAccount.User.FirstName}";
                
                _renavigator.Renavigate();
            }
            catch ( UserNotFoundException )
            {
                _loginViewModel.ErrorMessage = "User not found. Please check your email and try again!";
            }
            catch ( InvalidPasswordException )
            {
                _loginViewModel.ErrorMessage = "Incorrect password. Please try again!";
            }
            catch ( Exception e )
            {
                Trace.TraceError( e.Message );
                _loginViewModel.ErrorMessage = "An unexpected error occurred during login. Please try again later!";
            }
        }


        private void LoginViewModel_PropertyChanged( object? sender, PropertyChangedEventArgs e )
        {
            if ( e.PropertyName == nameof( LoginViewModel.CanLogin ) )
                OnCallExecuteChanged();
        }
    }
}
