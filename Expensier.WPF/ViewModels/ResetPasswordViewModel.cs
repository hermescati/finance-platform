using Expensier.Domain.Services.Authentication;
using Expensier.WPF.Commands;
using Expensier.WPF.State.Authenticators;
using Expensier.WPF.State.Navigators;
using Expensier.WPF.ViewModels.Errors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Expensier.WPF.ViewModels
{
    public class ResetPasswordViewModel : ViewModelBase
    {
        private string _email;
        public string Email
        {
            get
            {
                return _email;
            }
            set
            {
                _email = value;
                OnPropertyChanged(nameof(Email));
                OnPropertyChanged(nameof(CanContinue));
            }
        }

        private string _newPassword;
        public string NewPassword
        {
            get
            {
                return _newPassword;
            }
            set
            {
                _newPassword = value;
                OnPropertyChanged(nameof(NewPassword));
                OnPropertyChanged(nameof(CanReset));
            }
        }

        private string _confirmPassword;
        public string ConfirmPassword
        {
            get
            {
                return _confirmPassword;
            }
            set
            {
                _confirmPassword = value;
                OnPropertyChanged(nameof(ConfirmPassword)); 
                OnPropertyChanged(nameof(CanReset));
            }
        }

        private bool _emailForm = true;
        public bool EmailForm
        {
            get
            {
                return _emailForm;
            }
            set
            {
                _emailForm = value;
                OnPropertyChanged(nameof(EmailForm));
            }
        }

        private bool _passwordForm = false;
        public bool PasswordForm
        {
            get
            {
                return _passwordForm;
            }
            set
            {
                _passwordForm = value;
                OnPropertyChanged(nameof(PasswordForm));
            }
        }

        public MessageViewModel ErrorMessageViewModel { get; }

        public string ErrorMessage
        {
            set => ErrorMessageViewModel.Message = value;
        }

        public bool CanContinue => !string.IsNullOrEmpty(Email);
        public bool CanReset => !string.IsNullOrEmpty(NewPassword) && 
            !string.IsNullOrEmpty(ConfirmPassword);

        public ICommand ContinueCommand { get; }
        public ICommand ResetPasswordCommand { get; }
        public ICommand ViewLoginCommand { get; }

        public ResetPasswordViewModel(IAuthenticationService authenticationService, IAuthenticator authenticator, IRenavigator loginRenagivator)
        {
            ErrorMessageViewModel = new MessageViewModel();

            ContinueCommand = new ContinueCommand(this, authenticationService);
            ResetPasswordCommand = new ResetPasswordCommand(this, authenticator, loginRenagivator);
            ViewLoginCommand = new RenavigateCommand(loginRenagivator);
        }
    }
}
