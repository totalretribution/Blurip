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
    /// Interaktionslogik für EditAvisynthProfilesWindow.xaml
    /// </summary>
    public partial class EditAvisynthProfilesWindow : Window
    {
        private UserSettings settings = null;

        public EditAvisynthProfilesWindow(UserSettings settings)
        {            
            try
            {
                InitializeComponent();
                this.settings = new UserSettings(settings);
                UpdateAvisynthProfiles();
            }
            catch (Exception ex)
            {
                Global.ErrorMsg(ex);
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
                avs.desc = Global.Res("NewAvisynthProfileDesc");
                EditAvisynthProfileWindow eapw = new EditAvisynthProfileWindow(avs);
                eapw.ShowDialog();
                if (eapw.DialogResult == true)
                {
                    settings.avisynthSettings.Add(avs);
                    UpdateAvisynthProfiles();
                }
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
