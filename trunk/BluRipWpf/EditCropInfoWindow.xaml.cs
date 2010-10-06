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
    /// Interaktionslogik für EditCropInfoWindow.xaml
    /// </summary>
    public partial class EditCropInfoWindow : Window
    {
        private CropInfo _cropInfo = null;

        public EditCropInfoWindow(CropInfo cropInfo)
        {
            try
            {
                InitializeComponent();
                this._cropInfo = new CropInfo(cropInfo);

                textBoxBorderBottom.Text = _cropInfo.borderBottom.ToString();
                textBoxBorderTop.Text = _cropInfo.borderTop.ToString();
                textBoxCropBottom.Text = _cropInfo.cropBottom.ToString();
                textBoxCropTop.Text = _cropInfo.cropTop.ToString();
                textBoxResizeX.Text = _cropInfo.resizeX.ToString();
                textBoxResizeY.Text = _cropInfo.resizeY.ToString();

                checkBoxAddBorders.IsChecked = _cropInfo.border;
                checkBoxResize.IsChecked = _cropInfo.resize;

                comboBoxResizeMethod.Items.Clear();
                foreach (string s in Global.resizeMethods) comboBoxResizeMethod.Items.Add(s);
                if (_cropInfo.resizeMethod > -1 && _cropInfo.resizeMethod < Global.resizeMethods.Count) comboBoxResizeMethod.SelectedIndex = _cropInfo.resizeMethod;
            }
            catch (Exception ex)
            {
                Global.ErrorMsg(ex);
            }
            
        }

        public CropInfo cropInfo
        {
            get { return _cropInfo; }
        }

        private void buttonOk_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        private void textBoxCropTop_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                _cropInfo.cropTop = Convert.ToInt32(textBoxCropTop.Text);
            }
            catch (Exception)
            {
            }
        }

        private void textBoxCropBottom_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                _cropInfo.cropBottom = Convert.ToInt32(textBoxCropBottom.Text);
            }
            catch (Exception)
            {
            }
        }

        private void textBoxResizeX_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                _cropInfo.resizeX = Convert.ToInt32(textBoxResizeX.Text);
            }
            catch (Exception)
            {
            }
        }

        private void textBoxResizeY_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                _cropInfo.resizeY = Convert.ToInt32(textBoxResizeY.Text);
            }
            catch (Exception)
            {
            }
        }

        private void textBoxBorderTop_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                _cropInfo.borderTop = Convert.ToInt32(textBoxBorderTop.Text);
            }
            catch (Exception)
            {
            }
        }

        private void textBoxBorderBottom_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                _cropInfo.borderBottom = Convert.ToInt32(textBoxBorderBottom.Text);
            }
            catch (Exception)
            {
            }
        }

        private void comboBoxResizeMethod_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (comboBoxResizeMethod.SelectedIndex > -1 && comboBoxResizeMethod.SelectedIndex < Global.resizeMethods.Count)
                {
                    _cropInfo.resizeMethod = comboBoxResizeMethod.SelectedIndex;
                }
            }
            catch (Exception)
            {
            }
        }

        private void checkBoxResize_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                _cropInfo.resize = (bool)checkBoxResize.IsChecked;
            }
            catch (Exception)
            {
            }
        }

        private void checkBoxAddBorders_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                _cropInfo.border = (bool)checkBoxAddBorders.IsChecked;
            }
            catch (Exception)
            {
            }
        }
    }
}
