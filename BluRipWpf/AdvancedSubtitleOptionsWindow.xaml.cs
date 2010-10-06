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
    /// Interaktionslogik für AdvancedSubtitleOptionsWindow.xaml
    /// </summary>
    public partial class AdvancedSubtitleOptionsWindow : Window
    {
        private AdvancedSubtitleOptions aso = null;

        public AdvancedSubtitleOptions advancedSubtitleOptions
        {
            get { return aso; }
        }

        public AdvancedSubtitleOptionsWindow(AdvancedSubtitleOptions aso)
        {
            try
            {
                InitializeComponent();
                this.aso = new AdvancedSubtitleOptions(aso);

                checkBoxIsForced.IsChecked = aso.isForced;
                checkBoxVobSub.IsChecked = aso.vobSub;
                checkBoxVobSubOnlyForced.IsChecked = aso.vobSubOnlyForced;
            }
            catch (Exception)
            {
            }            
        }

        private void buttonOk_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        private void checkBoxIsForced_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                aso.isForced = (bool)checkBoxIsForced.IsChecked;
            }
            catch (Exception)
            {
            }
        }

        private void checkBoxVobSub_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                aso.vobSub = (bool)checkBoxVobSub.IsChecked;
            }
            catch (Exception)
            {
            }
        }

        private void checkBoxVobSubOnlyForced_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                aso.vobSubOnlyForced = (bool)checkBoxVobSubOnlyForced.IsChecked;
            }
            catch (Exception)
            {
            }
        }
    }
}
