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
    /// Interaktionslogik für AdvancedVideoOptionsWindow.xaml
    /// </summary>
    public partial class AdvancedVideoOptionsWindow : Window
    {
        private AdvancedVideoOptions avo = null;

        public AdvancedVideoOptionsWindow(AdvancedVideoOptions avo)
        {
            try
            {
                InitializeComponent();
                this.avo = new AdvancedVideoOptions(avo);

                checkBoxManualFramerate.IsChecked = avo.disableFps;
                textBoxFramerate.Text = avo.fps;
                textBoxLength.Text = avo.length;
                textBoxFrameCount.Text = avo.frames;
                checkBoxManualFramerate_Checked(null, null);

                checkBoxManualInputResolution.IsChecked = avo.manualInputRes;
                textBoxInputResX.Text = avo.inputResX.ToString();
                textBoxInputResY.Text = avo.inputResY.ToString();
                checkBoxManualInputResolution_Checked(null, null);

                checkBoxManualAspectRatio.IsChecked = avo.manualAspectRatio;
                textBoxAspectRatio.Text = avo.aspectRatio;
                checkBoxManualAspectRatio_Checked(null, null);

                checkBoxDisableAutocrop.IsChecked = avo.disableAutocrop;
                checkBoxDisableAutocrop_Checked(null, null);

                checkBoxManualCrop.IsChecked = avo.manualCrop;
                textBoxCropBottom.Text = avo.cropBottom.ToString();
                textBoxCropLeft.Text = avo.cropLeft.ToString();
                textBoxCropRight.Text = avo.cropRight.ToString();
                textBoxCropTop.Text = avo.cropTop.ToString();
                checkBoxManualCrop_Checked(null, null);

                checkBoxManualBorders.IsChecked = avo.manualBorders;
                textBoxBorderBottom.Text = avo.borderBottom.ToString();
                textBoxBorderLeft.Text = avo.borderLeft.ToString();
                textBoxBorderRight.Text = avo.borderRight.ToString();
                textBoxBorderTop.Text = avo.borderTop.ToString();
                checkBoxManualBorders_Checked(null, null);

                checkBoxManualResize.IsChecked = avo.manualResize;
                textBoxResizeX.Text = avo.resizeX.ToString();
                textBoxResizeY.Text = avo.resizeY.ToString();

                comboBoxResizeMethod.Items.Clear();
                foreach (string s in GlobalVars.resizeMethods) comboBoxResizeMethod.Items.Add(s);
                if (avo.resizeMethod > -1 && avo.resizeMethod < GlobalVars.resizeMethods.Count) comboBoxResizeMethod.SelectedIndex = avo.resizeMethod;

                checkBoxManualResize_Checked(null, null);
            }
            catch (Exception ex)
            {
                Global.ErrorMsg(ex);
            }
        }

        public AdvancedVideoOptions advancedVideoOptions
        {
            get { return avo; }
        }

        private void buttonOk_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        private void checkBoxManualFramerate_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                avo.disableFps = (bool)checkBoxManualFramerate.IsChecked;
                if (avo.disableFps)
                {
                    groupBoxFramerate.IsEnabled = true;
                }
                else
                {
                    groupBoxFramerate.IsEnabled = false;
                }
            }
            catch (Exception)
            {
            }
        }

        private void textBoxFramerate_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                avo.fps = textBoxFramerate.Text;
            }
            catch (Exception)
            {
            }
        }

        private void textBoxLength_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                avo.length = textBoxLength.Text;
            }
            catch (Exception)
            {
            }
        }

        private void textBoxFrameCount_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                avo.frames = textBoxFrameCount.Text;
            }
            catch (Exception)
            {
            }
        }

        private void comboBoxFramerate_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (comboBoxFramerate.SelectedIndex > -1)
                {
                    textBoxFramerate.Text = (string)comboBoxFramerate.SelectedItem;
                }
            }
            catch (Exception)
            {
            }
        }

        private void checkBoxManualInputResolution_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                avo.manualInputRes = (bool)checkBoxManualInputResolution.IsChecked;
                if (avo.manualInputRes)
                {
                    groupBoxInputResolution.IsEnabled = true;
                }
                else
                {
                    groupBoxInputResolution.IsEnabled = false;
                }
            }
            catch (Exception)
            {
            }
        }

        private void textBoxInputResX_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                avo.inputResX = Convert.ToInt32(textBoxInputResX.Text);
            }
            catch (Exception)
            {
            }
        }

        private void textBoxInputResY_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                avo.inputResY = Convert.ToInt32(textBoxInputResY.Text);
            }
            catch (Exception)
            {
            }
        }

        private void checkBoxManualAspectRatio_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                avo.manualAspectRatio = (bool)checkBoxManualAspectRatio.IsChecked;
                if (avo.manualAspectRatio)
                {
                    groupBoxAspectRatio.IsEnabled = true;
                }
                else
                {
                    groupBoxAspectRatio.IsEnabled = false;
                }
            }
            catch (Exception)
            {
            }
        }

        private void textBoxAspectRatio_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                avo.aspectRatio = textBoxAspectRatio.Text;
            }
            catch (Exception)
            {
            }
        }

        private void checkBoxDisableAutocrop_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                avo.disableAutocrop = (bool)checkBoxDisableAutocrop.IsChecked;
                if (avo.disableAutocrop)
                {
                    groupBoxAutocrop.IsEnabled = true;
                }
                else
                {
                    groupBoxAutocrop.IsEnabled = false;
                }
            }
            catch (Exception)
            {
            }
        }

        private void checkBoxManualCrop_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                avo.manualCrop = (bool)checkBoxManualCrop.IsChecked;
                if (avo.manualCrop)
                {
                    groupBoxManualCrop.IsEnabled = true;
                }
                else
                {
                    groupBoxManualCrop.IsEnabled = false;
                }
            }
            catch (Exception)
            {
            }
        }

        private void textBoxCropTop_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                avo.cropTop = Convert.ToInt32(textBoxCropTop.Text);
            }
            catch (Exception)
            {
            }
        }

        private void textBoxCropBottom_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                avo.cropBottom = Convert.ToInt32(textBoxCropBottom.Text);
            }
            catch (Exception)
            {
            }
        }

        private void textBoxCropLeft_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                avo.cropLeft = Convert.ToInt32(textBoxCropLeft.Text);
            }
            catch (Exception)
            {
            }
        }

        private void textBoxCropRight_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                avo.cropRight = Convert.ToInt32(textBoxCropRight.Text);
            }
            catch (Exception)
            {
            }
        }

        private void checkBoxManualBorders_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                avo.manualBorders = (bool)checkBoxManualBorders.IsChecked;
                if (avo.manualBorders)
                {
                    groupBoxManualBorders.IsEnabled = true;
                }
                else
                {
                    groupBoxManualBorders.IsEnabled = false;
                }
            }
            catch (Exception)
            {
            }
        }

        private void textBoxBorderTop_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                avo.borderTop = Convert.ToInt32(textBoxBorderTop.Text);
            }
            catch (Exception)
            {
            }
        }

        private void textBoxBorderBottom_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                avo.borderBottom = Convert.ToInt32(textBoxBorderBottom.Text);
            }
            catch (Exception)
            {
            }
        }

        private void textBoxBorderLeft_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                avo.borderLeft = Convert.ToInt32(textBoxBorderLeft.Text);
            }
            catch (Exception)
            {
            }
        }

        private void textBoxBorderRight_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                avo.borderRight = Convert.ToInt32(textBoxBorderRight.Text);
            }
            catch (Exception)
            {
            }
        }

        private void checkBoxManualResize_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                avo.manualResize = (bool)checkBoxManualResize.IsChecked;
                if (avo.manualResize)
                {
                    groupBoxManualResize.IsEnabled = true;
                }
                else
                {
                    groupBoxManualResize.IsEnabled = false;
                }
            }
            catch (Exception)
            {
            }
        }

        private void textBoxResizeX_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                avo.resizeX = Convert.ToInt32(textBoxResizeX.Text);
            }
            catch (Exception)
            {
            }
        }

        private void textBoxResizeY_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                avo.resizeY = Convert.ToInt32(textBoxResizeY.Text);
            }
            catch (Exception)
            {
            }
        }

        private void comboBoxResizeMethod_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                avo.resizeMethod = comboBoxResizeMethod.SelectedIndex;
            }
            catch (Exception)
            {
            }
        }
    }
}
