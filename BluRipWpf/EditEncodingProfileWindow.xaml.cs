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
    /// Interaktionslogik für EditEncodingProfileWindow.xaml
    /// </summary>
    public partial class EditEncodingProfileWindow : Window
    {
        EncodingSettings es = null;

        public EditEncodingProfileWindow(EncodingSettings es)
        {            
            try
            {
                InitializeComponent();
                this.es = new EncodingSettings(es);
                textBoxDescription.Text = es.desc;
                textBoxSettings.Text = es.settings;
                textBoxSettings2.Text = es.settings2;
                textBoxSizeValue.Text = es.sizeValue.ToString();
                comboBoxSizeType.SelectedIndex = (int)es.sizeType;
                checkBox2pass.IsChecked = es.pass2;
                
                comboBoxProfile.ItemsSource = GlobalVars.profile;
                comboBoxPreset.ItemsSource = GlobalVars.preset;
                comboBoxTune.ItemsSource = GlobalVars.tune;
                comboBoxLevel.ItemsSource = GlobalVars.level;
                comboBoxAqmode.ItemsSource = GlobalVars.aqmode;
                comboBoxBadapt.ItemsSource = GlobalVars.badapt;

                textBoxCrf.Text = es.crf.ToString("f1");
                textBoxBframes.Text = es.bframes.ToString();
                textBoxRef.Text = es.refvalue.ToString();

                checkBoxFastdecode.IsChecked = es.fastdecode;
                checkBoxNofastpskip.IsChecked = es.nofastpskip;
                checkBoxZerolatency.IsChecked = es.zerolatency;
                checkBoxSlowfirstpass.IsChecked = es.slowfirstpass;

                if (es.profile > -1 && es.profile < GlobalVars.profile.Count) comboBoxProfile.SelectedIndex = es.profile;
                if (es.preset > -1 && es.preset < GlobalVars.preset.Count) comboBoxPreset.SelectedIndex = es.preset;
                if (es.tune > -1 && es.tune < GlobalVars.tune.Count) comboBoxTune.SelectedIndex = es.tune;
                if (es.level > -1 && es.level < GlobalVars.level.Count) comboBoxLevel.SelectedIndex = es.level;
                if (es.aqmode > -1 && es.aqmode < GlobalVars.aqmode.Count) comboBoxAqmode.SelectedIndex = es.aqmode;
                if (es.badapt > -1 && es.badapt < GlobalVars.badapt.Count) comboBoxBadapt.SelectedIndex = es.badapt;

                checkBox2pass_Checked(null, null);

                UpdateParams();
            }
            catch (Exception ex)
            {
                Global.ErrorMsg(ex);
            }
        }

        private void UpdateParams()
        {
            try
            {
                textBlockParam.Text = "Parameter: " + es.GetParam;
                textBlockSecondParam.Text = "Parameter: " + es.GetSecondParam;
            }
            catch (Exception)
            {
            }
        }

        public EncodingSettings encodingSettings
        {
            get { return es; }
        }

        private void checkBox2pass_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                es.pass2 = (bool)checkBox2pass.IsChecked;
                if (es.pass2)
                {
                    groupBox2pass.IsEnabled = true;
                }
                else
                {
                    groupBox2pass.IsEnabled = false;
                }
                UpdateParams();
            }
            catch (Exception)
            {
            }
        }

        private void textBoxDescription_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                es.desc = textBoxDescription.Text;
            }
            catch (Exception)
            {
            }
        }

        private void textBoxSettings_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                es.settings = textBoxSettings.Text;
                UpdateParams();
            }
            catch (Exception)
            {
            }
        }

        private void textBoxSettings2_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                es.settings2 = textBoxSettings2.Text;
                UpdateParams();
            }
            catch (Exception)
            {
            }
        }

        private void textBoxSizeValue_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                es.sizeValue = Convert.ToDouble(textBoxSizeValue.Text);
                UpdateParams();
            }
            catch (Exception)
            {
            }
        }

        private void buttonOk_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        private void comboBoxSizeType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                es.sizeType = (SizeType)Enum.ToObject(typeof(SizeType), comboBoxSizeType.SelectedIndex);
                UpdateParams();
            }
            catch (Exception)
            {
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                textBoxDescription.Focus();
                textBoxDescription.SelectAll();
            }
            catch (Exception)
            {
            }
        }

        private void comboBoxProfile_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                int index = comboBoxProfile.SelectedIndex;
                if (index > -1 && index < GlobalVars.profile.Count)
                {
                    es.profile = index;
                    UpdateParams();
                }
            }
            catch (Exception)
            {
            }
        }

        private void comboBoxPreset_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                int index = comboBoxPreset.SelectedIndex;
                if (index > -1 && index < GlobalVars.preset.Count)
                {
                    es.preset = index;
                    UpdateParams();
                }
            }
            catch (Exception)
            {
            }
        }

        private void comboBoxTune_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                int index = comboBoxTune.SelectedIndex;
                if (index > -1 && index < GlobalVars.tune.Count)
                {
                    es.tune = index;
                    UpdateParams();
                }
            }
            catch (Exception)
            {
            }
        }

        private void comboBoxBadapt_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                int index = comboBoxBadapt.SelectedIndex;
                if (index > -1 && index < GlobalVars.badapt.Count)
                {
                    es.badapt = index;                    
                    UpdateParams();
                }
            }
            catch (Exception)
            {
            }
        }

        private void comboBoxAqmode_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                int index = comboBoxAqmode.SelectedIndex;
                if (index > -1 && index < GlobalVars.aqmode.Count)
                {
                    es.aqmode = index;
                    UpdateParams();
                }
            }
            catch (Exception)
            {
            }
        }

        private void comboBoxLevel_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                int index = comboBoxLevel.SelectedIndex;
                if (index > -1 && index < GlobalVars.level.Count)
                {
                    es.level = index;
                    UpdateParams();
                }
            }
            catch (Exception)
            {
            }
        }

        private void textBoxCrf_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                es.crf = Convert.ToDouble(textBoxCrf.Text);
                UpdateParams();
            }
            catch (Exception)
            {
            }
        }

        private void textBoxBframes_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                es.bframes = Convert.ToInt32(textBoxBframes.Text);
                UpdateParams();
            }
            catch (Exception)
            {
            }
        }

        private void textBoxRef_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                es.refvalue = Convert.ToInt32(textBoxRef.Text);
                UpdateParams();
            }
            catch (Exception)
            {
            }
        }

        private void checkBoxNofastpskip_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                es.nofastpskip = (bool)checkBoxNofastpskip.IsChecked;
                UpdateParams();
            }
            catch (Exception)
            {
            }
        }

        private void checkBoxFastdecode_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                es.fastdecode = (bool)checkBoxFastdecode.IsChecked;
                UpdateParams();
            }
            catch (Exception)
            {
            }
        }

        private void checkBoxZerolatency_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                es.zerolatency = (bool)checkBoxZerolatency.IsChecked;
                UpdateParams();
            }
            catch (Exception)
            {
            }
        }

        private void checkBoxSlowfirstpass_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                es.slowfirstpass = (bool)checkBoxSlowfirstpass.IsChecked;
                UpdateParams();
            }
            catch (Exception)
            {
            }
        }
    }
}
