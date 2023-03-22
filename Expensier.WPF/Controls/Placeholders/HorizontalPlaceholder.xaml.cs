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

namespace Expensier.WPF.Controls.Placeholders
{
    public partial class HorizontalPlaceholder : UserControl
    {
        public HorizontalPlaceholder()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty IconProperty =
            DependencyProperty.Register("Icon", typeof(ImageSource), typeof(HorizontalPlaceholder), new PropertyMetadata(null));

        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register("Title", typeof(string), typeof(HorizontalPlaceholder), new PropertyMetadata(null));

        public static readonly DependencyProperty BodyContentProperty =
            DependencyProperty.Register("BodyContent", typeof(string), typeof(HorizontalPlaceholder), new PropertyMetadata(null));


        public ImageSource Icon
        {
            get { return (ImageSource)GetValue(IconProperty); }
            set { SetValue(IconProperty, value); }
        }

        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        public string Body
        {
            get { return (string)GetValue(BodyContentProperty); }
            set { SetValue(BodyContentProperty, value); }
        }
    }
}
