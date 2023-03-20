using Expensier.Domain.Exceptions;
using Expensier.Domain.Models;
using Expensier.Domain.Services.Authentication;
using Expensier.EntityFramework.Services;
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
    public class ContinueCommand : AsyncCommandBase
    {
        private readonly ResetPasswordViewModel _resetPasswordViewModel;
        private readonly IAuthenticationService _authenticationService;

        public ContinueCommand(ResetPasswordViewModel resetPasswordViewModel, IAuthenticationService authenticationService)
        {
            _resetPasswordViewModel = resetPasswordViewModel;
            _authenticationService = authenticationService;

            _resetPasswordViewModel.PropertyChanged += ForgotPasswordViewModel_PropertyChanged;
        }

        public override bool CanExecute(object parameter)
        {
            return _resetPasswordViewModel.CanContinue && base.CanExecute(parameter);
        }

        public override async Task ExecuteAsync(object parameter)
        {
            _resetPasswordViewModel.ErrorMessage = string.Empty;

            try
            {
                await _authenticationService.getAccount(_resetPasswordViewModel.Email);
                _resetPasswordViewModel.EmailForm = false;
                _resetPasswordViewModel.PasswordForm = true;
            }
            catch (UserNotFoundException)
            {
                _resetPasswordViewModel.ErrorMessage = "User does not exist. Check for typos and try again.";
            }
            catch (Exception)
            {
                _resetPasswordViewModel.ErrorMessage = "Something went wrong!";
            }
        }

        private void ForgotPasswordViewModel_PropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(ResetPasswordViewModel.CanContinue))
            {
                OnCallExecuteChanged();
            }
        }
    }
}
