using Expensier.WPF.ViewModels;
using Expensier.WPF.ViewModels.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Expensier.WPF.State.Navigators
{
    public class DelegateRenavigator<TViewModel> : IRenavigator where TViewModel : ViewModelBase
    {
        private readonly INavigator _navigator;
        private readonly CreateViewModel<TViewModel> _createViewModel;

        public DelegateRenavigator(INavigator navigator, CreateViewModel<TViewModel> createViewModel)
        {
            _navigator = navigator;
            _createViewModel = createViewModel;
        }

        public void Renavigate()
        {
            _navigator.CurrentViewModel = _createViewModel();
        }
    }
}
