using Expensier.WPF.State.Navigators;
using Expensier.WPF.ViewModels;
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
        private readonly CreateViewModel<ResetPasswordViewModel> _createForgotPasswordViewModel;

        public ExpensierViewModelFactory(
            CreateViewModel<DashboardViewModel> createDashboardViewModel,
            CreateViewModel<ExpensesViewModel> createExpenseViewModel,
            CreateViewModel<WalletViewModel> createWalletViewModel,
            CreateViewModel<LoginViewModel> createLoginViewModel,
            CreateViewModel<ResetPasswordViewModel> createForgotPasswordViewModel)
        {
            _createDashboardViewModel = createDashboardViewModel;
            _createExpenseViewModel = createExpenseViewModel;
            _createWalletViewModel = createWalletViewModel;
            _createLoginViewModel = createLoginViewModel;
            _createForgotPasswordViewModel = createForgotPasswordViewModel;
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
                case ViewType.ForgotPassword:
                    return _createForgotPasswordViewModel();
                default:
                    throw new ArgumentException("The View Type does not have a ViewModel.", "viewType");
            }
        }
    }
}
