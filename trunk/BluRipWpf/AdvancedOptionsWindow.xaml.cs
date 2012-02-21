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
using System.Diagnostics;

namespace BluRip
{
    /// <summary>
    /// Interaktionslogik für AdvancedOptionsWindow.xaml
    /// </summary>
    public partial class AdvancedOptionsWindow : Window
    {
        private UserSettings settings = null;
        private List<PluginBase> pluginList = null;

        public AdvancedOptionsWindow(UserSettings settings, bool expert, List<PluginBase> pluginList)
        {            
            try
            {
                InitializeComponent();
                this.settings = new UserSettings(settings);
                this.pluginList = pluginList;

                textBoxNrFrames.Text = settings.nrFrames.ToString();
                textBoxRowSum.Text = settings.blackValue.ToString();
                checkBoxMinimizeAutocrop.IsChecked = settings.minimizeAutocrop;
                if (settings.cropMode > -1 && settings.cropMode < comboBoxCropMethod.Items.Count)
                {
                    comboBoxCropMethod.SelectedIndex = settings.cropMode;
                }
                checkBoxManualCrop.IsChecked = settings.manualCrop;

                checkBoxDeleteDemuxedFiles.IsChecked = settings.deleteAfterEncode;
                checkBoxAlwaysDeleteIndex.IsChecked = settings.deleteIndex;
                checkBoxDisableAudioHeaderCompression.IsChecked = settings.disableAudioHeaderCompression;
                checkBoxDisableVideoHeaderCompression.IsChecked = settings.disableVideoHeaderCompression;
                checkBoxDisableSubtitleHeaderCompression.IsChecked = settings.disableSubtitleHeaderCompression;

                checkBoxUseAutoSelect.IsChecked = settings.useAutoSelect;
                checkBoxIncludeChapters.IsChecked = settings.includeChapter;
                checkBoxPreferDTS.IsChecked = settings.preferDTS;
                checkBoxIncludeSubtitles.IsChecked = settings.includeSubtitle;
                checkBoxDefaultAudioTrack.IsChecked = settings.defaultAudio;
                checkBoxDefaultSubtitleTrack.IsChecked = settings.defaultSubtitle;
                checkBoxDefaultSubtitleForced.IsChecked = settings.defaultSubtitleForced;
                checkBoxForcedSubtitleFlag.IsChecked = settings.defaultForcedFlag;

                checkBoxDefaultSubtitleForced_Checked(null, null);
                checkBoxDefaultSubtitleTrack_Checked(null, null);

                comboBoxProcessPriority.Items.Clear();
                foreach (string s in Enum.GetNames(typeof(ProcessPriorityClass)))
                {
                    comboBoxProcessPriority.Items.Add(s);
                }

                comboBoxDTSBitrate.Items.Clear();
                foreach (string s in GlobalVars.dtsBitrates)
                {
                    comboBoxDTSBitrate.Items.Add(s);
                }

                comboBoxAC3Bitrate.Items.Clear();
                foreach (string s in GlobalVars.ac3Bitrates)
                {
                    comboBoxAC3Bitrate.Items.Add(s);
                }

                checkBoxConvertDTSBitrate.IsChecked = settings.downmixDTS;
                checkBoxConvertAC3Bitrate.IsChecked = settings.downmixAc3;

                checkBoxConvertAC3Bitrate_Checked(null, null);
                checkBoxConvertDTSBitrate_Checked(null, null);

                if (settings.downmixDTSIndex > -1 && settings.downmixDTSIndex < GlobalVars.dtsBitrates.Count) comboBoxDTSBitrate.SelectedIndex = settings.downmixDTSIndex;
                if (settings.downmixAc3Index > -1 && settings.downmixAc3Index < GlobalVars.ac3Bitrates.Count) comboBoxAC3Bitrate.SelectedIndex = settings.downmixAc3Index;

                comboBoxResizeMethod.Items.Clear();
                foreach (string s in GlobalVars.resizeMethods) comboBoxResizeMethod.Items.Add(s);
                if (settings.resizeMethod > -1 && settings.resizeMethod < GlobalVars.resizeMethods.Count) comboBoxResizeMethod.SelectedIndex = settings.resizeMethod;

                comboBoxProcessPriority.SelectedItem = Enum.GetName(typeof(ProcessPriorityClass), settings.x264Priority);

                UpdatePreferredAudio();
                UpdatePreferredSub();

                if (expert)
                {
                    EnableExpert();
                }
                else
                {
                    DisableExpert();
                }


                UpdatePlugins();
            }
            catch (Exception ex)
            {
                Global.ErrorMsg(ex);
            }
        }

        private void UpdatePlugins()
        {
            try
            {
                listBoxPlugIns.Items.Clear();
                foreach (PluginBase plugin in pluginList)
                {
                    string name = "";
                    if (plugin.Settings.activated)
                    {
                        name = "[ " + Global.Res("InfoActivated") + " ] ";
                    }
                    else
                    {
                        name = "[ " + Global.Res("InfoDeactivated") + " ] ";
                    }
                    name += plugin.GetName() + " " + plugin.GetVersion();
                    name += " (" + plugin.GetDescription() + ")";
                    listBoxPlugIns.Items.Add(name);
                }
            }
            catch (Exception)
            {
            }
        }

        public void DisableExpert()
        {
            try
            {
                tabItemAutocrop.Visibility = System.Windows.Visibility.Collapsed;
                tabItemVideo.Visibility = System.Windows.Visibility.Collapsed;
                tabItemAudio.Visibility = System.Windows.Visibility.Collapsed;
            }
            catch (Exception)
            {
            }
        }

        public void EnableExpert()
        {
            try
            {
                tabItemAutocrop.Visibility = System.Windows.Visibility.Visible;
                tabItemVideo.Visibility = System.Windows.Visibility.Visible;
                tabItemAudio.Visibility = System.Windows.Visibility.Visible;
            }
            catch (Exception)
            {
            }
        }

        private void UpdatePreferredAudio()
        {
            try
            {
                listBoxPreferredAudioLanguages.Items.Clear();
                foreach (LanguageInfo li in settings.preferredAudioLanguages)
                {
                    listBoxPreferredAudioLanguages.Items.Add(li.language + " - " + li.translation + " - " + li.languageShort);
                }
            }
            catch (Exception)
            {
            }
        }

        private void UpdatePreferredSub()
        {
            try
            {
                listBoxPreferredSubLanguages.Items.Clear();
                foreach (LanguageInfo li in settings.preferredSubtitleLanguages)
                {
                    listBoxPreferredSubLanguages.Items.Add(li.language + " - " + li.translation + " - " + li.languageShort);
                }
            }
            catch (Exception)
            {
            }
        }

        private void buttonOk_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        public UserSettings UserSettings
        {
            get { return settings; }
        }

        private void textBoxNrFrames_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                settings.nrFrames = Convert.ToInt32(textBoxNrFrames.Text);
            }
            catch (Exception)
            {
            }
        }

        private void textBoxRowSum_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                settings.blackValue = Convert.ToInt32(textBoxRowSum.Text);
            }
            catch (Exception)
            {
            }
        }

        private void comboBoxCropMethod_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (comboBoxCropMethod.SelectedIndex > -1)
                {
                    settings.cropMode = comboBoxCropMethod.SelectedIndex;
                }
            }
            catch (Exception)
            {
            }
        }

        private void checkBoxMinimizeAutocrop_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                settings.minimizeAutocrop = (bool)checkBoxMinimizeAutocrop.IsChecked;
            }
            catch (Exception)
            {
            }
        }

        private void checkBoxDeleteDemuxedFiles_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                settings.deleteAfterEncode = (bool)checkBoxDeleteDemuxedFiles.IsChecked;
            }
            catch (Exception)
            {
            }
        }

        private void checkBoxAlwaysDeleteIndex_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                settings.deleteIndex = (bool)checkBoxAlwaysDeleteIndex.IsChecked;
            }
            catch (Exception)
            {
            }
        }

        private void checkBoxUseAutoSelect_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                settings.useAutoSelect = (bool)checkBoxUseAutoSelect.IsChecked;
            }
            catch (Exception)
            {
            }
        }

        private void checkBoxIncludeChapters_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                settings.includeChapter = (bool)checkBoxIncludeChapters.IsChecked;
            }
            catch (Exception)
            {
            }
        }

        private void checkBoxPreferDTS_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                settings.preferDTS = (bool)checkBoxPreferDTS.IsChecked;
            }
            catch (Exception)
            {
            }
        }

        private void checkBoxIncludeSubtitles_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                settings.includeSubtitle = (bool)checkBoxIncludeSubtitles.IsChecked;
            }
            catch (Exception)
            {
            }
        }

        private void checkBoxDefaultAudioTrack_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                settings.defaultAudio = (bool)checkBoxDefaultAudioTrack.IsChecked;
            }
            catch (Exception)
            {
            }
        }

        private void checkBoxDefaultSubtitleTrack_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                settings.defaultSubtitle = (bool)checkBoxDefaultSubtitleTrack.IsChecked;
                if (settings.defaultSubtitle)
                {
                    checkBoxDefaultSubtitleForced.IsEnabled = true;
                }
                else
                {
                    checkBoxDefaultSubtitleForced.IsChecked = false;
                    checkBoxDefaultSubtitleForced.IsEnabled = false;
                }                
            }
            catch (Exception)
            {
            }
        }

        private void checkBoxDefaultSubtitleForced_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                settings.defaultSubtitleForced = (bool)checkBoxDefaultSubtitleForced.IsChecked;
                if (settings.defaultSubtitleForced)
                {
                    checkBoxForcedSubtitleFlag.IsEnabled = true;
                }
                else
                {
                    checkBoxForcedSubtitleFlag.IsChecked = false;
                    checkBoxForcedSubtitleFlag.IsEnabled = false;
                }
            }
            catch (Exception)
            {
            }
        }

        private void comboBoxProcessPriority_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (comboBoxProcessPriority.SelectedIndex > -1)
                {
                    settings.x264Priority = (ProcessPriorityClass)Enum.Parse(typeof(ProcessPriorityClass), comboBoxProcessPriority.SelectedValue.ToString());
                }
            }
            catch (Exception)
            {
            }
        }

        private void listBoxPreferredAudioLanguages_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                int index = listBoxPreferredAudioLanguages.SelectedIndex;
                if (index > -1)
                {
                    EditPreferredLanguageWindow eplw = new EditPreferredLanguageWindow(settings.preferredAudioLanguages[index]);
                    eplw.ShowDialog();
                    if (eplw.DialogResult == true)
                    {
                        settings.preferredAudioLanguages[index] = new LanguageInfo(eplw.languageInfo);
                        UpdatePreferredAudio();
                    }

                }
            }
            catch (Exception)
            {
            }
        }

        private void listBoxPreferredSubLanguages_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                int index = listBoxPreferredSubLanguages.SelectedIndex;
                if (index > -1)
                {
                    EditPreferredLanguageWindow eplw = new EditPreferredLanguageWindow(settings.preferredSubtitleLanguages[index]);
                    eplw.ShowDialog();
                    if (eplw.DialogResult == true)
                    {
                        settings.preferredSubtitleLanguages[index] = new LanguageInfo(eplw.languageInfo);
                        UpdatePreferredSub();
                    }

                }
            }
            catch (Exception)
            {
            }
        }

        private void buttonPreferredAudioLanguagesUp_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int index = listBoxPreferredAudioLanguages.SelectedIndex;
                if (index > 0)
                {
                    LanguageInfo li = settings.preferredAudioLanguages[index];
                    settings.preferredAudioLanguages.RemoveAt(index);
                    settings.preferredAudioLanguages.Insert(index - 1, li);
                    UpdatePreferredAudio();
                    listBoxPreferredAudioLanguages.SelectedIndex = index - 1;
                }
            }
            catch (Exception)
            {
            }
        }

        private void buttonPreferredAudioLanguagesDown_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int index = listBoxPreferredAudioLanguages.SelectedIndex;
                if (index < settings.preferredAudioLanguages.Count - 1)
                {
                    LanguageInfo li = settings.preferredAudioLanguages[index];
                    settings.preferredAudioLanguages.RemoveAt(index);
                    settings.preferredAudioLanguages.Insert(index + 1, li);
                    UpdatePreferredAudio();
                    listBoxPreferredAudioLanguages.SelectedIndex = index + 1;
                }
            }
            catch (Exception)
            {
            }
        }

        private void buttonPreferredAudioLanguagesDel_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int index = listBoxPreferredAudioLanguages.SelectedIndex;
                if (index > -1)
                {
                    settings.preferredAudioLanguages.RemoveAt(index);
                    UpdatePreferredAudio();
                }
            }
            catch (Exception)
            {
            }
        }
        
        private void buttonPreferredAudioLanguagesAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                LanguageInfo li = new LanguageInfo();
                li.language = Global.Res("NewLanguage");
                EditPreferredLanguageWindow eplw = new EditPreferredLanguageWindow(li);
                eplw.ShowDialog();
                if (eplw.DialogResult == true)
                {
                    li = new LanguageInfo(eplw.languageInfo);
                    settings.preferredAudioLanguages.Add(li);
                    UpdatePreferredAudio();
                }
            }
            catch (Exception)
            {
            }
        }

        private void buttonPreferredSubLanguagesUp_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int index = listBoxPreferredSubLanguages.SelectedIndex;
                if (index > 0)
                {
                    LanguageInfo li = settings.preferredSubtitleLanguages[index];
                    settings.preferredSubtitleLanguages.RemoveAt(index);
                    settings.preferredSubtitleLanguages.Insert(index - 1, li);
                    UpdatePreferredSub();
                    listBoxPreferredSubLanguages.SelectedIndex = index - 1;
                }
            }
            catch (Exception)
            {
            }
        }

        private void buttonPreferredSubLanguagesDown_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int index = listBoxPreferredSubLanguages.SelectedIndex;
                if (index < settings.preferredSubtitleLanguages.Count - 1)
                {
                    LanguageInfo li = settings.preferredSubtitleLanguages[index];
                    settings.preferredSubtitleLanguages.RemoveAt(index);
                    settings.preferredSubtitleLanguages.Insert(index + 1, li);
                    UpdatePreferredSub();
                    listBoxPreferredSubLanguages.SelectedIndex = index + 1;
                }
            }
            catch (Exception)
            {
            }
        }

        private void buttonPreferredSubLanguagesDel_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int index = listBoxPreferredSubLanguages.SelectedIndex;
                if (index > -1)
                {
                    settings.preferredSubtitleLanguages.RemoveAt(index);
                    UpdatePreferredSub();
                }
            }
            catch (Exception)
            {
            }
        }

        private void buttonPreferredSubLanguagesAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                LanguageInfo li = new LanguageInfo();
                li.language = Global.Res("NewLanguage");
                EditPreferredLanguageWindow eplw = new EditPreferredLanguageWindow(li);
                eplw.ShowDialog();
                if (eplw.DialogResult == true)
                {
                    li = new LanguageInfo(eplw.languageInfo);
                    settings.preferredSubtitleLanguages.Add(li);
                    UpdatePreferredSub();
                }
            }
            catch (Exception)
            {
            }
        }

        private void checkBoxConvertDTSBitrate_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                settings.downmixDTS = (bool)checkBoxConvertDTSBitrate.IsChecked;
                if (settings.downmixDTS)
                {
                    comboBoxDTSBitrate.IsEnabled = true;
                }
                else
                {
                    comboBoxDTSBitrate.IsEnabled = false;
                }
            }
            catch (Exception)
            {
            }
        }

        private void checkBoxConvertAC3Bitrate_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                settings.downmixAc3 = (bool)checkBoxConvertAC3Bitrate.IsChecked;
                if (settings.downmixAc3)
                {
                    comboBoxAC3Bitrate.IsEnabled = true;
                }
                else
                {
                    comboBoxAC3Bitrate.IsEnabled = false;
                }
            }
            catch (Exception)
            {
            }
        }

        private void comboBoxDTSBitrate_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (comboBoxDTSBitrate.SelectedIndex > -1)
                {
                    settings.downmixDTSIndex = comboBoxDTSBitrate.SelectedIndex;
                }
            }
            catch (Exception)
            {
            }
        }

        private void comboBoxAC3Bitrate_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (comboBoxAC3Bitrate.SelectedIndex > -1)
                {
                    settings.downmixAc3Index = comboBoxAC3Bitrate.SelectedIndex;
                }
            }
            catch (Exception)
            {
            }
        }

        private void checkBoxForcedSubtitleFLag_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                settings.defaultForcedFlag = (bool)checkBoxForcedSubtitleFlag.IsEnabled;
            }
            catch (Exception)
            {
            }
        }

        private void checkBoxDisableHeaderCompression_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                settings.disableAudioHeaderCompression = (bool)checkBoxDisableAudioHeaderCompression.IsChecked;
            }
            catch (Exception)
            {
            }
        }

        private void comboBoxResizeMethod_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (comboBoxResizeMethod.SelectedIndex > -1) settings.resizeMethod = comboBoxResizeMethod.SelectedIndex;
            }
            catch (Exception)
            {
            }
        }

        private void checkBoxManualCrop_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                settings.manualCrop = (bool)checkBoxManualCrop.IsChecked;
            }
            catch (Exception)
            {
            }
        }

        private void checkBoxDisableVideoHeaderCompression_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                settings.disableVideoHeaderCompression = (bool)checkBoxDisableVideoHeaderCompression.IsChecked;
            }
            catch (Exception)
            {
            }
        }

        private void listBoxPlugIns_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                int index = listBoxPlugIns.SelectedIndex;
                if (index > -1 && index < pluginList.Count)
                {
                    if (pluginList[index].EditSettings())
                    {
                        pluginList[index].SaveSettings();
                        UpdatePlugins();                        
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        private void checkBoxDisableSubtitleHeaderCompression_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                settings.disableSubtitleHeaderCompression = (bool)checkBoxDisableSubtitleHeaderCompression.IsChecked;
            }
            catch (Exception)
            {
            }
        }
    }
}
