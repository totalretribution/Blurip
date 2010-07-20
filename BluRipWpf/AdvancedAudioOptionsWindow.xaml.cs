//BluRip - one click BluRay/m2ts to mkv converter
//Copyright (C) 2009-2010 _hawk_

//This program is free software; you can redistribute it and/or
//modify it under the terms of the GNU General Public License
//as published by the Free Software Foundation; either version 2
//of the License, or (at your option) any later version.

//This program is distributed in the hope that it will be useful,
//but WITHOUT ANY WARRANTY; without even the implied warranty of
//MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//GNU General Public License for more details.

//You should have received a copy of the GNU General Public License
//along with this program; if not, write to the Free Software
//Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA  02110-1301, USA.

//Contact: hawk.ac@gmx.net

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
