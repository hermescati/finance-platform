using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;


namespace Expensier.WPF.Controls.Portfolio
{
    public partial class PortfolioCard : UserControl
    {
        public static readonly DependencyProperty IconProperty =
            DependencyProperty.Register(
                name: "Icon",
                propertyType: typeof( ImageSource ),
                ownerType: typeof( PortfolioCard ),
                typeMetadata: new PropertyMetadata( null ) );

        public ImageSource Icon
        {
            get => ( ImageSource ) GetValue( IconProperty );
            set => SetValue( IconProperty, value );
        }


        public static readonly DependencyProperty HeaderProperty =
            DependencyProperty.Register(
                name: "Header",
                propertyType: typeof( string ),
                ownerType: typeof( PortfolioCard ),
                typeMetadata: new PropertyMetadata( null ) );

        public string Header
        {
            get => ( string ) GetValue( HeaderProperty );
            set => SetValue( HeaderProperty, value );
        }


        public static readonly DependencyProperty TotalValueProperty =
            DependencyProperty.Register(
                name: "TotalValue",
                propertyType: typeof( double ),
                ownerType: typeof( PortfolioCard ),
                typeMetadata: new PropertyMetadata( null ) );

        public double TotalValue
        {
            get => ( double ) GetValue( TotalValueProperty );
            set => SetValue( TotalValueProperty, value );
        }


        public static readonly DependencyProperty InitialInvestmentProperty =
            DependencyProperty.Register(
                name: "InitialInvestment",
                propertyType: typeof( double ),
                ownerType: typeof( PortfolioCard ),
                typeMetadata: new PropertyMetadata( null ) );

        public double InitialInvestment
        {
            get => ( double ) GetValue( InitialInvestmentProperty );
            set => SetValue( InitialInvestmentProperty, value );
        }


        public static readonly DependencyProperty InvestmentReturnProperty =
            DependencyProperty.Register(
                name: "InvestmentReturn",
                propertyType: typeof( double ),
                ownerType: typeof( PortfolioCard ),
                typeMetadata: new PropertyMetadata( null ) );

        public double InvestmentReturn
        {
            get => ( double ) GetValue( InvestmentReturnProperty );
            set => SetValue( InvestmentReturnProperty, value );
        }


        public PortfolioCard()
        {
            InitializeComponent();
        }
    }
}
