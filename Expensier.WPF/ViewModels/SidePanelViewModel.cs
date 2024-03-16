using Expensier.WPF.Commands.Authentication;
using Expensier.WPF.State.Authenticators;
using Expensier.WPF.State.Navigators;
using System.Windows.Input;


namespace Expensier.WPF.ViewModels
{
    public class SidePanelViewModel : ViewModelBase
    {
        private readonly IAuthenticator _authenticator;
        private readonly IRenavigator _loginRenavigator;


        private string _greetingMessage;
        public string GreetingMessage
        {
            get => _greetingMessage;
            set
            {
                _greetingMessage = value;
                OnPropertyChanged( nameof( GreetingMessage ) );
            }
        }


        public ICommand LogoutCommand { get; }


        public SidePanelViewModel( IAuthenticator authenticator, IRenavigator loginRenavigator )
        {
            _authenticator = authenticator;
            _loginRenavigator = loginRenavigator;

            LogoutCommand = new LogoutCommand( authenticator, loginRenavigator );
        }
    }
}
