using Expensier.WPF.Commands;
using Expensier.WPF.State.Authenticators;
using Expensier.WPF.State.Navigators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Expensier.WPF.ViewModels
{
    public class LoginViewModel : ViewModelBase
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
            }
        }

        public MessageViewModel ErrorMessageViewModel { get; }

        public string ErrorMessage
        {
            set => ErrorMessageViewModel.Message = value;
        }

        public ICommand LoginCommand { get; }

        public LoginViewModel(IAuthenticator authenticator, IRenavigator renavigator)
        {
            ErrorMessageViewModel = new MessageViewModel();
            LoginCommand = new LoginCommand(this, authenticator, renavigator);
        }
    }
}
