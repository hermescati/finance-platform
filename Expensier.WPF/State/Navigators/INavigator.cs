using Expensier.WPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Expensier.WPF.State.Navigators
{
    public enum ViewType
    {
        Dashboard,
        Expenses,
        Wallet,
        Login, 
        Register
    }

    public interface INavigator
    {
        ViewModelBase CurrentViewModel { get; set; } 
    }
}
