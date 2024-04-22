using System.Windows;
using System.Windows.Controls;


namespace Expensier.WPF.Controls.Charts
{
    public partial class PieChart : UserControl
    {
        public static readonly DependencyProperty TotalValueProperty = 
            DependencyProperty.Register(
                "TotalValue", 
                typeof( double ), 
                typeof( PieChart ), 
                new PropertyMetadata( default( double ) ) );

        public double TotalValue
        {
            get => ( double ) GetValue( TotalValueProperty );
            set => SetValue( TotalValueProperty, value );
        }


        public PieChart()
        {
            InitializeComponent();
        }
    }
}
