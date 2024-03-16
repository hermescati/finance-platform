using Expensier.WPF.State.Navigators;
using Expensier.WPF.ViewModels.Factories;
using System;
using System.Windows.Input;


namespace Expensier.WPF.Commands
{
    public class UpdateCurrentViewModelCommand : ICommand
    {
        public event EventHandler? CanExecuteChanged;


        private readonly INavigator _navigatior;
        private readonly IExpensierViewModelFactory _viewModelFactory;


        public UpdateCurrentViewModelCommand( INavigator navigatior, IExpensierViewModelFactory viewModelFactory )
        {
            _navigatior = navigatior;
            _viewModelFactory = viewModelFactory;
        }


        public bool CanExecute( object? parameter ) => true;


        public void Execute( object? parameter )
        {
            if ( parameter is ViewType )
            {
                ViewType viewType = (ViewType) parameter;
                _navigatior.CurrentViewModel = _viewModelFactory.CreateViewModel( viewType );
            }
        }
    }
}
