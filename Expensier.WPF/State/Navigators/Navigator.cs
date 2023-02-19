using Expensier.WPF.Commands;
using Expensier.WPF.ViewModels;
using Expensier.WPF.ViewModels.Factories;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Expensier.WPF.State.Navigators
{    
    public class Navigator : INavigator
    {
        private ViewModelBase _currentViewModel;
        
        public ViewModelBase CurrentViewModel 
        {
            get
            {
                return _currentViewModel;
            }
            set
            {
                _currentViewModel = value;
                StateChanged?.Invoke();
            }
        }

        public event Action StateChanged;
    }
}
