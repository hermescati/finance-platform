using Microsoft.IdentityModel.Tokens;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;


namespace Expensier.WPF.Controls
{
    public partial class SearchBar : UserControl
    {
        private bool _isSearching;


        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register( "Text", typeof( string ), typeof( SearchBar ),
                new FrameworkPropertyMetadata( string.Empty, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
                    TextPropertyChanged, null, false, UpdateSourceTrigger.PropertyChanged ) );


        private static void TextPropertyChanged( DependencyObject d, DependencyPropertyChangedEventArgs e )
        {
            if ( d is SearchBar searchBar )
            {
                searchBar.UpdateContent();
            }
        }


        public string Text
        {
            get { return (string) GetValue( TextProperty ); }
            set { SetValue( TextProperty, value ); }
        }


        public SearchBar()
        {
            InitializeComponent();
        }


        private void UpdateText()
        {
            _isSearching = true;
            Text = searchBar.Text;
            _isSearching = false;
        }


        private void UpdateContent()
        {
            if ( !_isSearching )
            {
                searchBar.Text = Text;
            }
        }


        private void searchBar_KeyUp( object sender, KeyEventArgs e )
        {
            if ( e.Key == Key.Enter )
            {
                UpdateText();
            }
        }


        private void searchBar_TextChanged( object sender, TextChangedEventArgs e )
        {
            if ( searchBar.Text.IsNullOrEmpty() )
            {
                UpdateText();
            }
        }
    }
}
