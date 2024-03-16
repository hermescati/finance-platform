using Expensier.Domain.Services.Authentication;
using Expensier.WPF.State.Authenticators;
using Expensier.WPF.State.Navigators;
using Expensier.WPF.ViewModels;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading.Tasks;


namespace Expensier.WPF.Commands.Authentication
{
    public class RegisterCommand : AsyncCommandBase
    {
        private readonly RegisterViewModel _registerViewModel;
        private readonly IAuthenticator _authenticator;
        private readonly IRenavigator _registerRenavigator;


        public RegisterCommand(
            RegisterViewModel registerViewModel,
            IAuthenticator authenticator,
            IRenavigator renavigator )
        {
            _registerViewModel = registerViewModel;
            _authenticator = authenticator;
            _registerRenavigator = renavigator;

            _registerViewModel.PropertyChanged += RegisterViewModel_PropertyChanged;
        }


        public override bool CanExecute( object parameter ) =>
            _registerViewModel.CanRegister &&
            base.CanExecute( parameter );


        public override async Task ExecuteAsync( object parameter )
        {
            _registerViewModel.ErrorMessage = string.Empty;

            try
            {
                RegistrationResult result = await _authenticator.UserRegister(
                        _registerViewModel.FirstName,
                        _registerViewModel.LastName,
                        _registerViewModel.Email,
                        _registerViewModel.Password,
                        _registerViewModel.ConfirmPassword );

                HandleRegistrationResult( result );
            }
            catch ( Exception e )
            {
                Trace.TraceError( e.Message );
                _registerViewModel.ErrorMessage = "An unexpected error occurred during registration. Please try again later!";
            }
        }


        private void HandleRegistrationResult( RegistrationResult result )
        {
            switch ( result )
            {
                case RegistrationResult.Success:
                    _registerRenavigator.Renavigate();
                    break;
                case RegistrationResult.PasswordsDoNotMatch:
                    _registerViewModel.ErrorMessage = "The password does not match the confirm password!";
                    break;
                case RegistrationResult.PasswordTooShort:
                    _registerViewModel.ErrorMessage = "The entered password is too short. Password should be at least 8 characters long!";
                    break;
                case RegistrationResult.EmailInUse:
                    _registerViewModel.ErrorMessage = "The email is already associated with an account. Please use a different email or log in instead!";
                    break;
                default:
                    _registerViewModel.ErrorMessage = "An unexpected error occurred during registration. Please try again later!";
                    break;
            }
        }


        private void RegisterViewModel_PropertyChanged( object? sender, PropertyChangedEventArgs e )
        {
            if ( e.PropertyName == nameof( RegisterViewModel.CanRegister ) )
                OnCallExecuteChanged();
        }
    }
}
