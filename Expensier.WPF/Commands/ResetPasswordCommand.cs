using Expensier.Domain.Services.Authentication;
using Expensier.WPF.State.Authenticators;
using Expensier.WPF.State.Navigators;
using Expensier.WPF.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Expensier.WPF.Commands
{
    public class ResetPasswordCommand : AsyncCommandBase
    {
        private readonly ResetPasswordViewModel _resetPasswordViewModel;
        private readonly IAuthenticator _authenticator;
        private readonly IRenavigator _renavigator;

        public ResetPasswordCommand(ResetPasswordViewModel resetPasswordViewModel, IAuthenticator authenticator, IRenavigator renavigator)
        {
            _resetPasswordViewModel = resetPasswordViewModel;
            _authenticator = authenticator;
            _renavigator = renavigator;

            _resetPasswordViewModel.PropertyChanged += ForgotPasswordViewModel_PropertyChanged;
        }

        public override bool CanExecute(object parameter)
        {
            return _resetPasswordViewModel.CanReset && base.CanExecute(parameter);
        }

        public override async Task ExecuteAsync(object parameter)
        {
            _resetPasswordViewModel.ErrorMessage = string.Empty;

            try
            {
                PasswordResetResult result = await _authenticator.resetPassword(_resetPasswordViewModel.Email, _resetPasswordViewModel.OldPassword, _resetPasswordViewModel.NewPassword, _resetPasswordViewModel.ConfirmPassword);

                switch (result)
                {
                    case PasswordResetResult.Success:
                        _renavigator.Renavigate();
                        break;
                    case PasswordResetResult.UserNotAuthenticated:
                        _resetPasswordViewModel.ErrorMessage = "The old password does not match to the chosen account. Check the password!";
                        break;
                    case PasswordResetResult.PasswordsDoNotMatch:
                        _resetPasswordViewModel.ErrorMessage = "Password does not match the confirm password!";
                        break;
                    case PasswordResetResult.SameOldPassword:
                        _resetPasswordViewModel.ErrorMessage = "The new password cannot be the same as the old one!";
                        break;
                    default:
                        _resetPasswordViewModel.ErrorMessage = "Reset process failed!";
                        break;
                }
            }
            catch (Exception)
            {
                _resetPasswordViewModel.ErrorMessage = "Reset process failed!";
            }
        }

        private void ForgotPasswordViewModel_PropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(ResetPasswordViewModel.CanReset))
            {
                OnCallExecuteChanged();
            }
        }
    }
}
