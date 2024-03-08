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

namespace Expensier.WPF.Controls.Cryptos
{
    /// <summary>
    /// Interaction logic for CryptoWatchlist.xaml
    /// </summary>
    public partial class CryptoWatchlist : UserControl
    {
        public CryptoWatchlist()
        {
            InitializeComponent();
            listView.PreviewMouseWheel += ListView_PreviewMouseWheel;
        }

        private void ListView_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            ScrollViewer scrollViewer = FindVisualChild<ScrollViewer>(listView);
            if (scrollViewer != null)
            {
                double pixelsToScroll = 12;

                if (e.Delta > 0)
                    scrollViewer.ScrollToHorizontalOffset(scrollViewer.HorizontalOffset - pixelsToScroll);
                else
                    scrollViewer.ScrollToHorizontalOffset(scrollViewer.HorizontalOffset + pixelsToScroll);

                e.Handled = true;
            }
        }

        private T FindVisualChild<T>(DependencyObject obj) where T : DependencyObject
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(obj); i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(obj, i);
                if (child != null && child is T)
                    return (T)child;
                else
                {
                    T childOfChild = FindVisualChild<T>(child);
                    if (childOfChild != null)
                        return childOfChild;
                }
            }
            return null;
        }
    }
}
