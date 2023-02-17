using Expensier.WPF.State.Navigators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Expensier.WPF.ViewModels.Factories
{
    public class ExpensierViewModelFactory : IExpensierViewModelFactory
    {
        private readonly CreateViewModel<DashboardViewModel> _createDashboardViewModel;
        private readonly CreateViewModel<ExpensesViewModel> _createExpenseViewModel;
        private readonly CreateViewModel<WalletViewModel> _createWalletViewModel;
        private readonly CreateViewModel<LoginViewModel> _createLoginViewModel;

        public ExpensierViewModelFactory(
            CreateViewModel<DashboardViewModel> createDashboardViewModel, 
            CreateViewModel<ExpensesViewModel> createExpenseViewModel, 
            CreateViewModel<WalletViewModel> createWalletViewModel, 
            CreateViewModel<LoginViewModel> createLoginViewModel)
        {
            _createDashboardViewModel = createDashboardViewModel;
            _createExpenseViewModel = createExpenseViewModel;
            _createWalletViewModel = createWalletViewModel;
            _createLoginViewModel = createLoginViewModel;
        }

        public ViewModelBase CreateViewModel(ViewType viewType)
        {
            switch (viewType)
            {
                case ViewType.Dashboard:
                    return _createDashboardViewModel();
                case ViewType.Expenses:
                    return _createExpenseViewModel();
                case ViewType.Wallet:
                    return _createWalletViewModel();
                case ViewType.Login:
                    return _createLoginViewModel();
                default:
                    throw new ArgumentException("The View Type does not have a ViewModel.", "viewType");
            }
        }
    }
}
