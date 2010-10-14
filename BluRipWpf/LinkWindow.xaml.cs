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
    /// Interaktionslogik für LinkWindow.xaml
    /// </summary>
    public partial class LinkWindow : Window
    {
        private string avisynthLink = "http://sourceforge.net/projects/avisynth2/files/";
        private string haaliLink = "http://haali.su/mkv/";
        private string eac3toLink = "http://forum.doom9.org/showthread.php?t=125966";
        private string x264Link = "http://x264.nl/";
        private string bdsup2subLink = "http://forum.doom9.org/showthread.php?t=145277";
        private string ffmpegsrcLink = "http://code.google.com/p/ffmpegsource/downloads/list";
        private string javaLink = "http://java.com/downloads/";
        private string mkvtoolnixLink = "http://www.bunkus.org/videotools/mkvtoolnix/downloads.html";
        private string filterTweakerLink = "http://www.codecguide.com/windows7_preferred_filter_tweaker.htm";
        private string anydvdLink = "http://www.slysoft.com/de/anydvdhd.html";
        private string surcodeLink = "http://www.surcode.com/";
        private string dgdecnvLink = "http://neuron2.net/dgdecnv/dgdecnv.html";
        private string x264InfoLink = "http://mewiki.project357.com/wiki/X264_Settings";
        private string avs2yuvLink = "http://akuvian.org/src/avisynth/avs2yuv/";
        private string suptitleLink = "http://www.zachsaw.co.cc/?pg=suptitle_pgs_avisynth_plugin";

        public LinkWindow()
        {
            try
            {
                InitializeComponent();
            }
            catch (Exception ex)
            {
                Global.ErrorMsg(ex);
            }            
        }

        private void buttonOk_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        private void labelAvisynth_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start(avisynthLink);
            }
            catch (Exception)
            {
            }
        }

        private void labelHaali_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start(haaliLink);
            }
            catch (Exception)
            {
            }
        }

        private void labelEac3to_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start(eac3toLink);
            }
            catch (Exception)
            {
            }
        }

        private void labelBdsup2sub_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start(bdsup2subLink);
            }
            catch (Exception)
            {
            }
        }

        private void labelX264_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start(x264Link);
            }
            catch (Exception)
            {
            }
        }

        private void labelAnydvd_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start(anydvdLink);
            }
            catch (Exception)
            {
            }
        }

        private void labelJava_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start(javaLink);
            }
            catch (Exception)
            {
            }
        }

        private void labelMkvtoolnix_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start(mkvtoolnixLink);
            }
            catch (Exception)
            {
            }
        }

        private void labelFilterTweaker_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start(filterTweakerLink);
            }
            catch (Exception)
            {
            }
        }

        private void labelSurcode_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start(surcodeLink);
            }
            catch (Exception)
            {
            }
        }

        private void labelDgdecnv_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start(dgdecnvLink);
            }
            catch (Exception)
            {
            }
        }

        private void labelAvs2yuv_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start(avs2yuvLink);
            }
            catch (Exception)
            {
            }
        }

        private void labelFfmepgsrc_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start(ffmpegsrcLink);
            }
            catch (Exception)
            {
            }
        }

        private void labelX264Info_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start(x264InfoLink);
            }
            catch (Exception)
            {
            }
        }
    }
}
