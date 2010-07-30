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
    /// Interaktionslogik für EditVideoFileInfoWindow.xaml
    /// </summary>
    public partial class EditVideoFileInfoWindow : Window
    {
        private VideoFileInfo vfi = null;

        public VideoFileInfo videoFileInfo
        {
            get { return vfi; }
        }

        public EditVideoFileInfoWindow(VideoFileInfo vfi)
        {
            try
            {
                InitializeComponent();
                this.vfi = new VideoFileInfo(vfi);

                textBoxEncodeAvs.Text = vfi.encodeAvs;
                textBoxEncodedFile.Text = vfi.encodedFile;
                textBoxFps.Text = vfi.fps;
                textBoxFrames.Text = vfi.frames;
                textBoxLength.Text = vfi.length;
                textBoxResX.Text = vfi.resX;
                textBoxResY.Text = vfi.resY;
            }
            catch (Exception)
            {
            }
        }

        private void buttonOk_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        private void textBoxFps_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                vfi.fps = textBoxFps.Text;
            }
            catch (Exception)
            {
            }
        }

        private void textBoxLength_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                vfi.length = textBoxLength.Text;
            }
            catch (Exception)
            {
            }
        }

        private void textBoxFrames_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                vfi.frames = textBoxFrames.Text;
            }
            catch (Exception)
            {
            }
        }

        private void textBoxResX_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                vfi.resX = textBoxResX.Text;
            }
            catch (Exception)
            {
            }
        }

        private void textBoxResY_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                vfi.resY = textBoxResY.Text;
            }
            catch (Exception)
            {
            }
        }

        private void textBoxEncodeAvs_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                vfi.encodeAvs = textBoxEncodeAvs.Text;
            }
            catch (Exception)
            {
            }
        }

        private void textBoxEncodedFile_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                vfi.encodedFile = textBoxEncodedFile.Text;
            }
            catch (Exception)
            {
            }
        }

        private void buttonEncodeAvs_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                OpenFileDialog ofd = new OpenFileDialog();
                ofd.Filter = "*.*|*.*";
                if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    textBoxEncodeAvs.Text = ofd.FileName;
                }
            }
            catch (Exception)
            {
            }
        }

        private void buttonEncodedFile_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                OpenFileDialog ofd = new OpenFileDialog();
                ofd.Filter = "*.*|*.*";
                if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    textBoxEncodedFile.Text = ofd.FileName;
                }
            }
            catch (Exception)
            {
            }
        }
    }
}
