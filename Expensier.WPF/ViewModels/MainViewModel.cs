using Expensier.WPF.Commands;
using Expensier.WPF.State.Accounts;
using Expensier.WPF.State.Authenticators;
using Expensier.WPF.State.Navigators;
using Expensier.WPF.ViewModels.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Expensier.WPF.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private readonly IExpensierViewModelFactory _viewModelFactory;
        private readonly INavigator _navigator;
        private readonly IAuthenticator _authenticator;
        private readonly IRenavigator _loginRenavigator;

        public bool IsAuthenticated => _authenticator.Authenticated;
        public ViewModelBase CurrentViewModel => _navigator.CurrentViewModel;

        public ICommand UpdateCurrentViewModelCommand { get; }
        public ICommand LogoutCommand { get; }


        public MainViewModel(INavigator navigator, IAuthenticator authenticator, IExpensierViewModelFactory viewModelFactory, IRenavigator loginRenavigator)
        {
            _navigator = navigator;
            _authenticator = authenticator;
            _viewModelFactory = viewModelFactory;
            _loginRenavigator = loginRenavigator;

            _navigator.StateChanged += Navigator_StateChanged;
            _authenticator.StateChanged += Authenticator_StateChanged;

            UpdateCurrentViewModelCommand = new UpdateCurrentViewModelCommand(navigator, _viewModelFactory);
            UpdateCurrentViewModelCommand.Execute(ViewType.Login);

            LogoutCommand = new LogoutCommand(authenticator, loginRenavigator);
        }

        private void Authenticator_StateChanged()
        {
            OnPropertyChanged(nameof(IsAuthenticated));
        }

        private void Navigator_StateChanged()
        {
            OnPropertyChanged(nameof(CurrentViewModel));
        }
    }
}
