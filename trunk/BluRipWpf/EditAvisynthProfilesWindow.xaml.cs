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
    /// Interaktionslogik für EditAvisynthProfilesWindow.xaml
    /// </summary>
    public partial class EditAvisynthProfilesWindow : Window
    {
        private UserSettings settings = null;

        public EditAvisynthProfilesWindow(UserSettings settings)
        {
            InitializeComponent();
            try
            {
                this.settings = new UserSettings(settings);
                UpdateAvisynthProfiles();
            }
            catch (Exception)
            {
            }
        }

        private void UpdateAvisynthProfiles()
        {
            try
            {
                listBoxAvisynthProfiles.Items.Clear();
                foreach(AvisynthSettings avs in settings.avisynthSettings)
                {
                    listBoxAvisynthProfiles.Items.Add(avs.desc);
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

        private string Res(string key)
        {
            try
            {
                return (string)App.Current.Resources[key];
            }
            catch (Exception)
            {
                return "Unknown resource";
            }
        }

        private void listBoxAvisynthProfiles_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                int index = listBoxAvisynthProfiles.SelectedIndex;
                if (index > -1)
                {
                    EditAvisynthProfileWindow eapw = new EditAvisynthProfileWindow(settings.avisynthSettings[index]);
                    eapw.ShowDialog();
                    if (eapw.DialogResult == true)
                    {
                        settings.avisynthSettings[index] = new AvisynthSettings(eapw.avisynthSettings);
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        private void buttonAviProfileAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                AvisynthSettings avs = new AvisynthSettings();
                avs.desc = Res("NewAvisynthProfileDesc");
                settings.avisynthSettings.Add(avs);
                UpdateAvisynthProfiles();
                listBoxAvisynthProfiles.SelectedIndex = settings.avisynthSettings.Count - 1;
                listBoxAvisynthProfiles_MouseDoubleClick(null, null);
            }
            catch (Exception)
            {
            }
        }

        private void buttonAviProfileUp_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int index = listBoxAvisynthProfiles.SelectedIndex;
                if (index > 0)
                {
                    AvisynthSettings avs = new AvisynthSettings(settings.avisynthSettings[index]);
                    settings.avisynthSettings.RemoveAt(index);
                    settings.avisynthSettings.Insert(index - 1, avs);
                    UpdateAvisynthProfiles();
                    listBoxAvisynthProfiles.SelectedIndex = index - 1;
                }
            }
            catch (Exception)
            {
            }
        }

        private void buttonAviProfileDown_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int index = listBoxAvisynthProfiles.SelectedIndex;
                if (index < settings.avisynthSettings.Count - 1)
                {
                    AvisynthSettings avs = new AvisynthSettings(settings.avisynthSettings[index]);
                    settings.avisynthSettings.RemoveAt(index);
                    settings.avisynthSettings.Insert(index + 1, avs);
                    UpdateAvisynthProfiles();
                    listBoxAvisynthProfiles.SelectedIndex = index + 1;
                }
            }
            catch (Exception)
            {
            }
        }

        private void buttonAviProfileDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int index = listBoxAvisynthProfiles.SelectedIndex;
                if (index > -1)
                {
                    settings.avisynthSettings.RemoveAt(index);
                    UpdateAvisynthProfiles();
                }
            }
            catch (Exception)
            {
            }
        }
    }
}
