using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace BluRip
{
    /// <summary>
    /// Interaktionslogik für LinkWindow.xaml
    /// </summary>
    public partial class LinkWindow : Window
    {
        public LinkWindow()
        {
            try
            {
                InitializeComponent();
            }
            catch (Exception ex)
            {
                Global.ErrorMsg(ex);
            }            
        }

        private void buttonOk_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }
    }
}
