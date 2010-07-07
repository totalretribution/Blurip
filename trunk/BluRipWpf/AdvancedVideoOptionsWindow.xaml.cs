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

                checkBoxManualFramerate.IsChecked = avo.disableFps;
                textBoxFramerate.Text = avo.fps;
                textBoxLength.Text = avo.length;
                textBoxFrameCount.Text = avo.frames;
                checkBoxManualFramerate_Checked(null, null);


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

        private void checkBoxManualFramerate_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                avo.disableFps = (bool)checkBoxManualFramerate.IsChecked;
                if (avo.disableFps)
                {
                    groupBoxFramerate.IsEnabled = true;
                }
                else
                {
                    groupBoxFramerate.IsEnabled = false;
                }
            }
            catch (Exception)
            {
            }
        }

        private void textBoxFramerate_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                avo.fps = textBoxFramerate.Text;
            }
            catch (Exception)
            {
            }
        }

        private void textBoxLength_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                avo.length = textBoxLength.Text;
            }
            catch (Exception)
            {
            }
        }

        private void textBoxFrameCount_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                avo.frames = textBoxFrameCount.Text;
            }
            catch (Exception)
            {
            }
        }

        private void comboBoxFramerate_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (comboBoxFramerate.SelectedIndex > -1)
                {
                    textBoxFramerate.Text = (string)comboBoxFramerate.SelectedItem;
                }
            }
            catch (Exception)
            {
            }
        }
    }
}
