using Expensier.WPF.State.Authenticators;
using Expensier.WPF.State.Navigators;
using System.Threading.Tasks;


namespace Expensier.WPF.Commands.Authentication
{
    public class LogoutCommand : AsyncCommandBase
    {
        private readonly IAuthenticator _authenticator;
        private readonly IRenavigator _renavigator;


        public LogoutCommand(IAuthenticator authenticator, IRenavigator renavigator)
        {
            _authenticator = authenticator;
            _renavigator = renavigator;
        }


        public override async Task ExecuteAsync(object parameter)
        {
            await _authenticator.UserLogout();
            _renavigator.Renavigate();
        }
    }
}
