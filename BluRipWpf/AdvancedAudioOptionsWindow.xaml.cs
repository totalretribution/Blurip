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
    /// Interaktionslogik für AdvancedAudioOptionsWindow.xaml
    /// </summary>
    public partial class AdvancedAudioOptionsWindow : Window
    {
        private AdvancedAudioOptions aao = null;

        public AdvancedAudioOptions advancedAudioOptions
        {
            get { return aao; }
        }

        public AdvancedAudioOptionsWindow(AdvancedAudioOptions aao)
        {
            try
            {
                InitializeComponent();
                this.aao = new AdvancedAudioOptions(aao);
                textBoxExtension.Text = aao.extension;
                textBoxBitrate.Text = aao.bitrate;
                textBoxExtraParameter.Text = aao.parameter;
                checkBoxAddAc3Track.IsChecked = aao.additionalAc3Track;
            }
            catch (Exception ex)
            {
                Global.ErrorMsg(ex);
            }
        }

        private void comboBoxExtension_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (comboBoxExtension.SelectedIndex > -1)
                {
                    textBoxExtension.Text = comboBoxExtension.SelectedItem.ToString();
                }
            }
            catch (Exception)
            {
            }
        }

        private void comboBoxBitrate_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (comboBoxBitrate.SelectedIndex > -1)
                {
                    textBoxBitrate.Text = comboBoxBitrate.SelectedItem.ToString();
                }
            }
            catch (Exception)
            {
            }
        }

        private void textBoxExtension_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                aao.extension = textBoxExtension.Text;
            }
            catch (Exception)
            {
            }
        }

        private void textBoxBitrate_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                aao.bitrate = textBoxBitrate.Text;
            }
            catch (Exception)
            {
            }
        }

        private void textBoxExtraParameter_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                aao.parameter = textBoxExtraParameter.Text;
            }
            catch (Exception)
            {
            }
        }

        private void checkBoxAddAc3Track_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                aao.additionalAc3Track = (bool)checkBoxAddAc3Track.IsChecked;
            }
            catch (Exception)
            {
            }
        }

        private void buttonOk_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }
    }
}
