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
    /// Interaktionslogik für AboutWindow.xaml
    /// </summary>
    public partial class SettingsWindow : Window
    {
        private AutoRefSettings settings = null;

        public SettingsWindow(PluginSettingsBase settings)
        {
            try
            {
                InitializeComponent();
                this.settings = new AutoRefSettings(settings);

                checkBoxActivated.IsChecked = this.settings.activated;
            }
            catch (Exception)
            {
            }            
        }

        public AutoRefSettings Settings
        {
            get { return settings; }
        }

        private void buttonOk_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        private void checkBoxActivated_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                settings.activated = (bool)checkBoxActivated.IsChecked;
            }
            catch (Exception)
            {
            }
        }
    }
}
