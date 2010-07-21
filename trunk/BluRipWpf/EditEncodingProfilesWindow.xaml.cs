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
    /// Interaktionslogik für EditProfilesWindow.xaml
    /// </summary>
    public partial class EditEncodingProfilesWindow : Window
    {
        private UserSettings settings = null;

        public EditEncodingProfilesWindow(UserSettings settings)
        {            
            try
            {
                InitializeComponent();
                this.settings = new UserSettings(settings);
                UpdateEncodingProfile();
            }
            catch (Exception ex)
            {
                Global.ErrorMsg(ex);
            }
        }

        private void UpdateEncodingProfile()
        {
            try
            {
                listBoxEncodingProfiles.Items.Clear();
                foreach (EncodingSettings es in settings.encodingSettings)
                {
                    listBoxEncodingProfiles.Items.Add(es.desc);
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

        private void listBoxEncodingProfiles_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                int index = listBoxEncodingProfiles.SelectedIndex;
                if (index > -1)
                {
                    EditEncodingProfileWindow eepw = new EditEncodingProfileWindow(settings.encodingSettings[index]);
                    eepw.ShowDialog();
                    if (eepw.DialogResult == true)
                    {
                        settings.encodingSettings[index] = new EncodingSettings(eepw.encodingSettings);
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        private void buttonProfileUp_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int index = listBoxEncodingProfiles.SelectedIndex;
                if (index > 0)
                {
                    EncodingSettings es = new EncodingSettings(settings.encodingSettings[index]);
                    settings.encodingSettings.RemoveAt(index);
                    settings.encodingSettings.Insert(index - 1, es);
                    UpdateEncodingProfile();
                    listBoxEncodingProfiles.SelectedIndex = index - 1;
                }
            }
            catch (Exception)
            {
            }
        }

        private void buttonProfileDown_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int index = listBoxEncodingProfiles.SelectedIndex;
                if (index < settings.encodingSettings.Count - 1)
                {
                    EncodingSettings es = new EncodingSettings(settings.encodingSettings[index]);
                    settings.encodingSettings.RemoveAt(index);
                    settings.encodingSettings.Insert(index + 1, es);
                    UpdateEncodingProfile();
                    listBoxEncodingProfiles.SelectedIndex = index + 1;
                }
            }
            catch (Exception)
            {
            }
        }

        private void buttonProfileDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int index = listBoxEncodingProfiles.SelectedIndex;
                if (index > -1)
                {
                    settings.encodingSettings.RemoveAt(index);
                    UpdateEncodingProfile();
                }
            }
            catch (Exception)
            {
            }
        }
                
        private void buttonProfileAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                EncodingSettings es = new EncodingSettings();
                es.desc = Global.Res("NewEncodingProfileDesc");
                EditEncodingProfileWindow eepw = new EditEncodingProfileWindow(es);
                eepw.ShowDialog();
                if (eepw.DialogResult == true)
                {
                    settings.encodingSettings.Add(es);
                    UpdateEncodingProfile();
                }
            }
            catch (Exception)
            {
            }
        }
    }
}
