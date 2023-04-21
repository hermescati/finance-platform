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

namespace Expensier.WPF.Controls
{
    public partial class SearchBar : UserControl
    {
        private bool _isSearching;

        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Text", typeof(string), typeof(SearchBar),
                new FrameworkPropertyMetadata(string.Empty, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
                    TextPropertyChanged, null, false, UpdateSourceTrigger.PropertyChanged));


        private static void TextPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is SearchBar searchBar)
            {
                searchBar.UpdateContent();
            }
        }

        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        public SearchBar()
        {
            InitializeComponent();
        }

        private void SeachBar_PasswordChanged(object sender, RoutedEventArgs e)
        {
            _isSearching = true;
            Text = searchBar.Text;
            _isSearching = false;
        }

        private void UpdateContent()
        {
            if (!_isSearching)
            {
                searchBar.Text = Text;
            }
        }
    }
}
