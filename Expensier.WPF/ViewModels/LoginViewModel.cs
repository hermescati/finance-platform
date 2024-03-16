using Expensier.WPF.Commands;
using Expensier.WPF.Commands.Authentication;
using Expensier.WPF.State.Authenticators;
using Expensier.WPF.State.Navigators;
using Expensier.WPF.ViewModels.Errors;
using System.Windows.Input;


namespace Expensier.WPF.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        private string _email;
        public string Email
        {
            get => _email;
            set
            {
                _email = value;
                OnPropertyChanged( nameof( Email ) );
                OnPropertyChanged( nameof( CanLogin ) );
            }
        }


        private string _password;
        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                OnPropertyChanged( nameof( Password ) );
                OnPropertyChanged( nameof( CanLogin ) );
            }
        }


        public bool CanLogin =>
            !string.IsNullOrEmpty( Email ) &&
            !string.IsNullOrEmpty( Password );


        public MessageViewModel ErrorMessageViewModel { get; }
        public string ErrorMessage
        {
            set => ErrorMessageViewModel.Message = value;
        }


        public ICommand LoginCommand { get; }
        public ICommand ViewRegisterCommand { get; }
        public ICommand ViewForgotPasswordCommand { get; }


        public LoginViewModel(
            IAuthenticator authenticator,
            IRenavigator loginRedirect,
            IRenavigator registerRedirect,
            IRenavigator forgotPasswordRedirect,
            SidePanelViewModel sidePanelViewModel )
        {
            ErrorMessageViewModel = new MessageViewModel();

            LoginCommand = new LoginCommand( this, authenticator, loginRedirect, sidePanelViewModel );
            ViewRegisterCommand = new RenavigateCommand( registerRedirect );
            ViewForgotPasswordCommand = new RenavigateCommand( forgotPasswordRedirect );
        }
    }
}
