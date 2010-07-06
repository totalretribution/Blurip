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
    /// Interaktionslogik für AdvancedVideoOptionsWindow.xaml
    /// </summary>
    public partial class AdvancedVideoOptionsWindow : Window
    {
        private AdvancedVideoOptions avo = null;

        public AdvancedVideoOptionsWindow(AdvancedVideoOptions avo)
        {
            try
            {
                InitializeComponent();
                this.avo = new AdvancedVideoOptions(avo);
            }
            catch (Exception ex)
            {
                Global.ErrorMsg(ex);
            }
        }

        public AdvancedVideoOptions advancedVideoOptions
        {
            get { return avo; }
        }

        private void buttonOk_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }
    }
}
