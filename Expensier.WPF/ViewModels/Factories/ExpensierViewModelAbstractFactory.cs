using Expensier.WPF.State.Navigators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Expensier.WPF.ViewModels.Factories
{
    public class ExpensierViewModelAbstractFactory : IExpensierViewModelAbstractFactory
    {
        private readonly IExpensierViewModelFactory<DashboardViewModel> _dashboardViewModelFactory;
        private readonly IExpensierViewModelFactory<ExpensesViewModel> _expensesViewModelFactory;
        private readonly IExpensierViewModelFactory<WalletViewModel> _walletViewModelFactory;
        private readonly IExpensierViewModelFactory<LoginViewModel> _loginViewModelFactory;

        public ExpensierViewModelAbstractFactory(
            IExpensierViewModelFactory<DashboardViewModel> dashboardViewModelFactory,
            IExpensierViewModelFactory<ExpensesViewModel> expensesViewModelFactory,
            IExpensierViewModelFactory<WalletViewModel> walletViewModelFactory,
            IExpensierViewModelFactory<LoginViewModel> loginViewModelFactory)
        {
            _dashboardViewModelFactory = dashboardViewModelFactory;
            _expensesViewModelFactory = expensesViewModelFactory;
            _walletViewModelFactory = walletViewModelFactory;
            _loginViewModelFactory = loginViewModelFactory;
        }

        public ViewModelBase CreateViewModel(ViewType viewType)
        {
            switch (viewType)
            {
                case ViewType.Dashboard:
                    return _dashboardViewModelFactory.CreateViewModel();
                case ViewType.Expenses:
                    return _expensesViewModelFactory.CreateViewModel();
                case ViewType.Wallet:
                    return _walletViewModelFactory.CreateViewModel();
                case ViewType.Login:
                    return _loginViewModelFactory.CreateViewModel();
                default:
                    throw new ArgumentException("The View Type does not have a ViewModel.", "viewType");
            }
        }
    }
}
