using Expensier.Domain.Services.Authentication;
using Expensier.WPF.Commands;
using Expensier.WPF.Commands.Authentication;
using Expensier.WPF.State.Authenticators;
using Expensier.WPF.State.Navigators;
using Expensier.WPF.ViewModels.Errors;
using System.Windows.Input;


namespace Expensier.WPF.ViewModels
{
    public class ResetPasswordViewModel : ViewModelBase
    {
        private string _email;
        public string Email
        {
            get => _email;
            set
            {
                _email = value;
                OnPropertyChanged( nameof( Email ) );
                OnPropertyChanged( nameof( CanContinue ) );
            }
        }


        private string _newPassword;
        public string NewPassword
        {
            get => _newPassword;
            set
            {
                _newPassword = value;
                OnPropertyChanged( nameof( NewPassword ) );
                OnPropertyChanged( nameof( CanReset ) );
            }
        }


        private string _confirmPassword;
        public string ConfirmPassword
        {
            get => _confirmPassword;
            set
            {
                _confirmPassword = value;
                OnPropertyChanged( nameof( ConfirmPassword ) );
                OnPropertyChanged( nameof( CanReset ) );
            }
        }


        private bool _showEmailForm = true;
        public bool ShowEmailForm
        {
            get => _showEmailForm;
            set
            {
                _showEmailForm = value;
                OnPropertyChanged( nameof( ShowEmailForm ) );
            }
        }


        private bool _showPasswordForm = false;
        public bool ShowPasswordForm
        {
            get => _showPasswordForm;
            set
            {
                _showPasswordForm = value;
                OnPropertyChanged( nameof( ShowPasswordForm ) );
            }
        }


        public MessageViewModel ErrorMessageViewModel { get; }
        public string ErrorMessage
        {
            set => ErrorMessageViewModel.Message = value;
        }


        public bool CanContinue => !string.IsNullOrEmpty( Email );
        public bool CanReset => 
            !string.IsNullOrEmpty( NewPassword ) &&
            !string.IsNullOrEmpty( ConfirmPassword );


        public ICommand ContinueCommand { get; }
        public ICommand ResetPasswordCommand { get; }
        public ICommand ViewLoginCommand { get; }


        public ResetPasswordViewModel( IAuthenticationService authenticationService, IAuthenticator authenticator, IRenavigator loginRenagivator )
        {
            ErrorMessageViewModel = new MessageViewModel();

            ContinueCommand = new ContinueCommand( this, authenticationService );
            ResetPasswordCommand = new ResetPasswordCommand( this, authenticator, loginRenagivator );
            ViewLoginCommand = new RenavigateCommand( loginRenagivator );
        }
    }
}
