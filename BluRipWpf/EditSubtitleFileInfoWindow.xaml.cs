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
    /// Interaktionslogik für EditSubtitleFileInfoWindow.xaml
    /// </summary>
    public partial class EditSubtitleFileInfoWindow : Window
    {
        private SubtitleFileInfo sfi = null;

        public SubtitleFileInfo subtitleFileInfo
        {
            get { return sfi; }
        }

        public EditSubtitleFileInfoWindow(SubtitleFileInfo sfi)
        {
            try
            {
                InitializeComponent();
                this.sfi = new SubtitleFileInfo(sfi);

                textBoxForcedIdx.Text = sfi.forcedIdx;
                textBoxForcedIdxLowres.Text = sfi.forcedIdxLowRes;
                textBoxForcedSub.Text = sfi.forcedSub;
                textBoxForcedSubLowres.Text = sfi.forcedSubLowRes;
                textBoxForcedSup.Text = sfi.forcedSup;
                textBoxNormalIdx.Text = sfi.normalIdx;
                textBoxNormalIdxLowres.Text = sfi.normalIdxLowRes;
                textBoxNormalSub.Text = sfi.normalSub;
                textBoxNormalSubLowres.Text = sfi.normalSubLowRes;
                textBoxNormalSup.Text = sfi.normalSup;
                checkBoxIsSecond.IsChecked = sfi.isSecond;
            }
            catch (Exception)
            {
            }
        }

        private void buttonOk_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        private void textBoxForcedIdx_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                sfi.forcedIdx = textBoxForcedIdx.Text;
            }
            catch (Exception)
            {
            }
        }

        private void textBoxForcedSub_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                sfi.forcedSub = textBoxForcedSub.Text;
            }
            catch (Exception)
            {
            }
        }

        private void textBoxForcedSup_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                sfi.forcedSup = textBoxForcedSup.Text;
            }
            catch (Exception)
            {
            }
        }

        private void textBoxNormalIdx_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                sfi.normalIdx = textBoxNormalIdx.Text;
            }
            catch (Exception)
            {
            }
        }

        private void textBoxNormalSub_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                sfi.normalSub = textBoxNormalSub.Text;
            }
            catch (Exception)
            {
            }
        }

        private void textBoxNormalSup_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                sfi.normalSup = textBoxNormalSup.Text;
            }
            catch (Exception)
            {
            }
        }

        private void textBoxForcedIdxLowres_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                sfi.forcedIdxLowRes = textBoxForcedIdxLowres.Text;
            }
            catch (Exception)
            {
            }
        }

        private void textBoxForcedSubLowres_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                sfi.forcedSubLowRes = textBoxForcedSubLowres.Text;
            }
            catch (Exception)
            {
            }
        }

        private void textBoxNormalIdxLowres_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                sfi.normalIdxLowRes = textBoxNormalIdxLowres.Text;
            }
            catch (Exception)
            {
            }
        }

        private void textBoxNormalSubLowres_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                sfi.normalSubLowRes = textBoxNormalSubLowres.Text;
            }
            catch (Exception)
            {
            }
        }

        private void checkBoxIsSecond_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                sfi.isSecond = (bool)checkBoxIsSecond.IsChecked;
            }
            catch (Exception)
            {
            }
        }

        private void buttonForcedIdx_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                OpenFileDialog ofd = new OpenFileDialog();
                ofd.Filter = "*.idx|*.idx";
                if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    textBoxForcedIdx.Text = ofd.FileName;
                }
            }
            catch (Exception)
            {
            }
        }

        private void buttonForcedSub_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                OpenFileDialog ofd = new OpenFileDialog();
                ofd.Filter = "*.sub|*.sub";
                if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    textBoxForcedSub.Text = ofd.FileName;
                }
            }
            catch (Exception)
            {
            }
        }

        private void buttonForcedSup_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                OpenFileDialog ofd = new OpenFileDialog();
                ofd.Filter = "*.sup|*.sup";
                if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    textBoxForcedSup.Text = ofd.FileName;
                }
            }
            catch (Exception)
            {
            }
        }

        private void buttonNormalIdx_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                OpenFileDialog ofd = new OpenFileDialog();
                ofd.Filter = "*.idx|*.idx";
                if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    textBoxNormalIdx.Text = ofd.FileName;
                }
            }
            catch (Exception)
            {
            }
        }

        private void buttonNormalSub_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                OpenFileDialog ofd = new OpenFileDialog();
                ofd.Filter = "*.sub|*.sub";
                if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    textBoxNormalSub.Text = ofd.FileName;
                }
            }
            catch (Exception)
            {
            }
        }

        private void buttonNormalSup_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                OpenFileDialog ofd = new OpenFileDialog();
                ofd.Filter = "*.sup|*.sup";
                if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    textBoxNormalSup.Text = ofd.FileName;
                }
            }
            catch (Exception)
            {
            }
        }

        private void buttonForcedIdxLowres_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                OpenFileDialog ofd = new OpenFileDialog();
                ofd.Filter = "*.idx|*.idx";
                if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    textBoxForcedIdxLowres.Text = ofd.FileName;
                }
            }
            catch (Exception)
            {
            }
        }

        private void buttonForcedSubLowres_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                OpenFileDialog ofd = new OpenFileDialog();
                ofd.Filter = "*.sub|*.sub";
                if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    textBoxForcedSubLowres.Text = ofd.FileName;
                }
            }
            catch (Exception)
            {
            }
        }

        private void buttonNormalIdxLowres_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                OpenFileDialog ofd = new OpenFileDialog();
                ofd.Filter = "*.idx|*.idx";
                if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    textBoxNormalIdxLowres.Text = ofd.FileName;
                }
            }
            catch (Exception)
            {
            }
        }

        private void buttonNormalSubLowres_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                OpenFileDialog ofd = new OpenFileDialog();
                ofd.Filter = "*.sub|*.sub";
                if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    textBoxNormalSubLowres.Text = ofd.FileName;
                }
            }
            catch (Exception)
            {
            }
        }
    }
}
