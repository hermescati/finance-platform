using Expensier.Domain.Exceptions;
using Expensier.Domain.Services.Authentication;
using Expensier.WPF.ViewModels;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading.Tasks;


namespace Expensier.WPF.Commands.Authentication
{
    public class ContinueCommand : AsyncCommandBase
    {
        private readonly ResetPasswordViewModel _resetPasswordViewModel;
        private readonly IAuthenticationService _authenticationService;


        public ContinueCommand( ResetPasswordViewModel resetPasswordViewModel, IAuthenticationService authenticationService )
        {
            _resetPasswordViewModel = resetPasswordViewModel;
            _authenticationService = authenticationService;

            _resetPasswordViewModel.PropertyChanged += ForgotPasswordViewModel_PropertyChanged;
        }


        public override bool CanExecute( object parameter ) =>
            _resetPasswordViewModel.CanContinue &&
            base.CanExecute( parameter );


        public override async Task ExecuteAsync( object parameter )
        {
            _resetPasswordViewModel.ErrorMessage = string.Empty;

            try
            {
                await _authenticationService.GetAccount( _resetPasswordViewModel.Email );

                _resetPasswordViewModel.ShowEmailForm = false;
                _resetPasswordViewModel.ShowPasswordForm = true;
            }
            catch ( UserNotFoundException )
            {
                _resetPasswordViewModel.ErrorMessage = "User not found. Please check your email and try again!";
            }
            catch ( Exception e )
            {
                Trace.TraceError( e.Message );
                _resetPasswordViewModel.ErrorMessage = "An unexpected error occurred. Please try again later!";
            }
        }


        private void ForgotPasswordViewModel_PropertyChanged( object? sender, PropertyChangedEventArgs e )
        {
            if ( e.PropertyName == nameof( ResetPasswordViewModel.CanContinue ) )
                OnCallExecuteChanged();
        }
    }
}
