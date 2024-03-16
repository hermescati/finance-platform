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
    public class ResetPasswordCommand : AsyncCommandBase
    {
        private readonly ResetPasswordViewModel _resetPasswordViewModel;
        private readonly IAuthenticator _authenticator;
        private readonly IRenavigator _renavigator;


        public ResetPasswordCommand(
            ResetPasswordViewModel resetPasswordViewModel,
            IAuthenticator authenticator,
            IRenavigator renavigator )
        {
            _resetPasswordViewModel = resetPasswordViewModel;
            _authenticator = authenticator;
            _renavigator = renavigator;

            _resetPasswordViewModel.PropertyChanged += ForgotPasswordViewModel_PropertyChanged;
        }


        public override bool CanExecute( object parameter ) =>
            _resetPasswordViewModel.CanReset &&
            base.CanExecute( parameter );


        public override async Task ExecuteAsync( object parameter )
        {
            _resetPasswordViewModel.ErrorMessage = string.Empty;

            try
            {
                PasswordResetResult result = await _authenticator.ResetPassword(
                    _resetPasswordViewModel.Email,
                    _resetPasswordViewModel.NewPassword,
                    _resetPasswordViewModel.ConfirmPassword );

                HandlePasswordResetResult( result );
            }
            catch ( Exception e )
            {
                Trace.TraceError( e.Message );
                _resetPasswordViewModel.ErrorMessage = "An unexpected error occurred. Please try again later!";
            }
        }


        private void HandlePasswordResetResult( PasswordResetResult result )
        {
            switch ( result )
            {
                case PasswordResetResult.Success:
                    _renavigator.Renavigate();
                    break;
                case PasswordResetResult.UserNotAuthenticated:
                    _resetPasswordViewModel.ErrorMessage = "The old password you entered does not match the password associated with this account!";
                    break;
                case PasswordResetResult.PasswordsDoNotMatch:
                    _resetPasswordViewModel.ErrorMessage = "The password does not match the confirm password!";
                    break;
                case PasswordResetResult.SameOldPassword:
                    _resetPasswordViewModel.ErrorMessage = "The new password cannot be the same as the old one!";
                    break;
                default:
                    _resetPasswordViewModel.ErrorMessage = "An unexpected error occurred. Please try again later!";
                    break;
            }
        }


        private void ForgotPasswordViewModel_PropertyChanged( object? sender, PropertyChangedEventArgs e )
        {
            if ( e.PropertyName == nameof( ResetPasswordViewModel.CanReset ) )
                OnCallExecuteChanged();
        }
    }
}
