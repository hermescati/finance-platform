﻿using Expensier.WPF.ViewModels.Modals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Expensier.WPF.Controls.Modals
{
    /// <summary>
    /// Interaction logic for ExportModal.xaml
    /// </summary>
    public partial class ExportModal : System.Windows.Controls.UserControl
    {
        public ExportModal()
        {
            InitializeComponent();
        }

        private void Rectangle_MouseDown( object sender, MouseButtonEventArgs e )
        {
            this.Visibility = Visibility.Collapsed;
            
        }

        private void Button_Click( object sender, RoutedEventArgs e )
        {
            this.Visibility = Visibility.Collapsed;
        }

        private void Open_File_Dialog(object sender, RoutedEventArgs e )
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog()
            {
                DefaultExt = ".csv",
                Filter = "CSV files (*.csv)|*.csv"
            };

            var result = saveFileDialog.ShowDialog();

            if ( result == DialogResult.OK )
            {
                ((ExportModalViewModel)DataContext).FilePath = saveFileDialog.FileName;
            }
        }
    }
}
