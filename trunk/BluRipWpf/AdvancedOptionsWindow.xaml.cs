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

        public AdvancedOptionsWindow(UserSettings settings)
        {
            InitializeComponent();
            try
            {
                this.settings = new UserSettings(settings);

                textBoxNrFrames.Text = settings.nrFrames.ToString();
                textBoxRowSum.Text = settings.blackValue.ToString();
                checkBoxMinimizeAutocrop.IsChecked = settings.minimizeAutocrop;
                comboBoxCropMethod.SelectedIndex = settings.cropMode;

                checkBoxDeleteDemuxedFiles.IsChecked = settings.deleteAfterEncode;
                checkBoxAlwaysDeleteIndex.IsChecked = settings.deleteIndex;

                checkBoxUseAutoSelect.IsChecked = settings.useAutoSelect;
                checkBoxIncludeChapters.IsChecked = settings.includeChapter;
                checkBoxPreferDTS.IsChecked = settings.preferDTS;
                checkBoxIncludeSubtitles.IsChecked = settings.includeSubtitle;
                checkBoxDefaultAudioTrack.IsChecked = settings.defaultAudio;
                checkBoxDefaultSubtitleTrack.IsChecked = settings.defaultSubtitle;
                checkBoxDefaultSubtitleForced.IsChecked = settings.defaultSubtitleForced;

                comboBoxProcessPriority.Items.Clear();
                foreach (string s in Enum.GetNames(typeof(ProcessPriorityClass)))
                {
                    comboBoxProcessPriority.Items.Add(s);
                }

                comboBoxProcessPriority.SelectedItem = Enum.GetName(typeof(ProcessPriorityClass), settings.x264Priority);
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
                    settings.cropInput = comboBoxCropMethod.SelectedIndex;
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
    }
}
