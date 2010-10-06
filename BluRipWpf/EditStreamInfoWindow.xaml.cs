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
using System.Windows.Forms;

namespace BluRip
{
    /// <summary>
    /// Interaktionslogik für EditStreamInfoWindow.xaml
    /// </summary>
    public partial class EditStreamInfoWindow : Window
    {
        private StreamInfo si = null;

        public StreamInfo streamInfo
        {
            get { return si; }
        }

        public EditStreamInfoWindow(StreamInfo si)
        {
            try
            {
                InitializeComponent();
                this.si = new StreamInfo(si);
                comboBoxStreamType.Items.Clear();
                foreach (StreamType s in Enum.GetValues(typeof(StreamType))) comboBoxStreamType.Items.Add(s);
                comboBoxStreamType.SelectedItem = si.streamType;

                textBoxDescription.Text = si.desc;
                textBoxFilename.Text = si.filename;
                textBoxLanguage.Text = si.language;
                textBoxNumber.Text = si.number.ToString();
                textBoxTypeDesc.Text = si.typeDesc;
                textBoxAddInfo.Text = si.addInfo;

                checkBoxSelected.IsChecked = si.selected;
            }
            catch (Exception)
            {
            }
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        private void textBoxNumber_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                si.number = Convert.ToInt32(textBoxNumber.Text);
            }
            catch (Exception)
            {
            }
        }

        private void textBoxDescription_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                si.desc = textBoxDescription.Text;
            }
            catch (Exception)
            {
            }
        }

        private void textBoxTypeDesc_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                si.typeDesc = textBoxTypeDesc.Text;
            }
            catch (Exception)
            {
            }
        }

        private void textBoxLanguage_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                si.language = textBoxLanguage.Text;
            }
            catch (Exception)
            {
            }
        }

        private void textBoxFilename_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                si.filename = textBoxFilename.Text;
            }
            catch (Exception)
            {
            }
        }

        private void checkBoxSelected_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                si.selected = (bool)checkBoxSelected.IsChecked;
            }
            catch (Exception)
            {
            }
        }

        private void UpdateStatusInfo()
        {
            try
            {
                if (si.extraFileInfo == null || si.extraFileInfo.GetType() == typeof(ExtraFileInfo))
                {
                    labelExtraFileInfo.Content = Global.ResFormat("LabelStreamInfoExtraFileInfo", Global.Res("MsgEmptyOptions"));
                }
                else
                {
                    bool ok = false;
                    if (si.streamType == StreamType.Video && si.extraFileInfo.GetType() == typeof(VideoFileInfo)) ok = true;
                    if(si.streamType == StreamType.Subtitle && si.extraFileInfo.GetType() == typeof(SubtitleFileInfo)) ok = true;

                    if (ok)
                    {
                        labelExtraFileInfo.Content = Global.ResFormat("LabelStreamInfoExtraFileInfo", Global.Res("MsgFilledOptions"));
                    }
                    else
                    {
                        si.extraFileInfo = new ExtraFileInfo();
                        labelExtraFileInfo.Content = Global.ResFormat("LabelStreamInfoExtraFileInfo", Global.Res("MsgEmptyOptions"));
                    }
                }

                if (si.advancedOptions == null || si.advancedOptions.GetType() == typeof(AdvancedOptions))
                {
                    labelAdvancedOptions.Content = Global.ResFormat("LabelStreamInfoAdvancedOptions", Global.Res("MsgEmptyOptions"));
                }
                else
                {
                    bool ok = false;
                    if (si.streamType == StreamType.Video && si.advancedOptions.GetType() == typeof(AdvancedVideoOptions)) ok = true;
                    if (si.streamType == StreamType.Audio && si.advancedOptions.GetType() == typeof(AdvancedAudioOptions)) ok = true;
                    if (si.streamType == StreamType.Subtitle && si.advancedOptions.GetType() == typeof(AdvancedSubtitleOptions)) ok = true;

                    if (ok)
                    {
                        labelAdvancedOptions.Content = Global.ResFormat("LabelStreamInfoAdvancedOptions", Global.Res("MsgFilledOptions"));
                    }
                    else
                    {
                        si.advancedOptions = new AdvancedOptions();
                        labelAdvancedOptions.Content = Global.ResFormat("LabelStreamInfoAdvancedOptions", Global.Res("MsgEmptyOptions"));
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        private void comboBoxStreamType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                si.streamType = (StreamType)comboBoxStreamType.SelectedItem;
                UpdateStatusInfo();
            }
            catch (Exception)
            {
            }
        }

        private void textBoxAddInfo_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                si.addInfo = textBoxAddInfo.Text;
            }
            catch (Exception)
            {
            }
        }

        private void buttonFilename_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                OpenFileDialog ofd = new OpenFileDialog();
                ofd.Filter = "*.*|*.*";
                if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    textBoxFilename.Text = ofd.FileName;
                }
            }
            catch (Exception)
            {
            }
        }

        private void buttonDelExtraInfo_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                si.extraFileInfo = new ExtraFileInfo();
                UpdateStatusInfo();
            }
            catch (Exception)
            {
            }
        }

        private void buttonDelAdvancedOptions_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                si.advancedOptions = new AdvancedOptions();
                UpdateStatusInfo();
            }
            catch (Exception)
            {
            }
        }

        private void buttonEditAdvancedOptions_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (si.streamType == StreamType.Audio)
                {
                    AdvancedAudioOptions aao = new AdvancedAudioOptions();
                    if (si.advancedOptions != null && si.advancedOptions.GetType() == typeof(AdvancedAudioOptions)) aao = new AdvancedAudioOptions(si.advancedOptions);
                    AdvancedAudioOptionsWindow aaow = new AdvancedAudioOptionsWindow(aao);                    
                    aaow.ShowDialog();
                    if (aaow.DialogResult == true)
                    {
                        si.advancedOptions = new AdvancedAudioOptions(aaow.advancedAudioOptions);
                        UpdateStatusInfo();
                    }
                }
                else if (si.streamType == StreamType.Video)
                {
                    AdvancedVideoOptions avo = new AdvancedVideoOptions();                    
                    if (si.advancedOptions != null && si.advancedOptions.GetType() == typeof(AdvancedVideoOptions)) avo = new AdvancedVideoOptions(si.advancedOptions);
                    AdvancedVideoOptionsWindow avow = new AdvancedVideoOptionsWindow(avo);
                    avow.ShowDialog();
                    if (avow.DialogResult == true)
                    {
                        si.advancedOptions = new AdvancedVideoOptions(avow.advancedVideoOptions);
                        UpdateStatusInfo();
                    }
                }
                else if (si.streamType == StreamType.Subtitle)
                {
                    AdvancedSubtitleOptions aso = new AdvancedSubtitleOptions();
                    if (si.advancedOptions != null && si.advancedOptions.GetType() == typeof(AdvancedSubtitleOptions)) aso = new AdvancedSubtitleOptions(si.advancedOptions);
                    AdvancedSubtitleOptionsWindow avow = new AdvancedSubtitleOptionsWindow(aso);
                    avow.ShowDialog();
                    if (avow.DialogResult == true)
                    {
                        si.advancedOptions = new AdvancedSubtitleOptions(avow.advancedSubtitleOptions);
                        UpdateStatusInfo();
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        private void buttonEditExtraInfo_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (si.streamType == StreamType.Video)
                {
                    VideoFileInfo vfi = new VideoFileInfo();
                    if (si.extraFileInfo != null && si.extraFileInfo.GetType() == typeof(VideoFileInfo)) vfi = new VideoFileInfo(si.extraFileInfo);
                    EditVideoFileInfoWindow evfiw = new EditVideoFileInfoWindow(vfi);
                    evfiw.ShowDialog();
                    if (evfiw.DialogResult == true)
                    {
                        si.extraFileInfo = new VideoFileInfo(evfiw.videoFileInfo);
                        UpdateStatusInfo();
                    }
                }
                else if (si.streamType == StreamType.Subtitle)
                {
                    SubtitleFileInfo sfi = new SubtitleFileInfo();
                    if (si.extraFileInfo != null && si.extraFileInfo.GetType() == typeof(SubtitleFileInfo)) sfi = new SubtitleFileInfo(si.extraFileInfo);
                    EditSubtitleFileInfoWindow esfiw = new EditSubtitleFileInfoWindow(sfi);
                    esfiw.ShowDialog();
                    if (esfiw.DialogResult == true)
                    {
                        si.extraFileInfo = new SubtitleFileInfo(esfiw.subtitleFileInfo);
                        UpdateStatusInfo();
                    }
                }
            }
            catch (Exception)
            {
            }
        }
    }
}
