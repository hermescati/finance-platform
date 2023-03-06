using Expensier.WPF.Controls.Modals;
using Expensier.WPF.ViewModels;
using Expensier.WPF.ViewModels.Modals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Expensier.WPF.Views
{
    /// <summary>
    /// Interaction logic for TransactionsView.xaml
    /// </summary>
    public partial class ExpensesView : UserControl
    {
        public ExpensesView()
        {
            InitializeComponent();
        }

        private void AddTransactionButton_Click(object sender, RoutedEventArgs e)
        {
            AddTransaction.IsOpen = true;
        }
    }
}
