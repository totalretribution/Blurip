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

        private void comboBoxStreamType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                si.streamType = (StreamType)comboBoxStreamType.SelectedItem;
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
    }
}
