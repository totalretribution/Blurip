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
                checkBoxSupTitle.IsChecked = aso.supTitle;
                checkBoxSupTitleOnlyForced.IsChecked = aso.supTitleOnlyForced;
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

        private void checkBoxSubTitle_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                aso.supTitle = (bool)checkBoxSupTitle.IsChecked;
            }
            catch (Exception)
            {
            }
        }

        private void checkBoxSupTitleOnlyForced_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                aso.supTitleOnlyForced = (bool)checkBoxSupTitleOnlyForced.IsChecked;
            }
            catch (Exception)
            {
            }
        }
    }
}
