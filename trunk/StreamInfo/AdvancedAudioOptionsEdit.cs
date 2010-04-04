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
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace BluRip
{
    public partial class AdvancedAudioOptionsEdit : AdvancedOptionsEdit
    {
        public AdvancedAudioOptionsEdit()
        {
            InitializeComponent();
        }

        public AdvancedAudioOptionsEdit(AdvancedOptions ao)
            : base(ao)
        {
            InitializeComponent();
            try
            {
                this.ao = new AdvancedAudioOptions(ao);
                textBoxBitrate.Text = ((AdvancedAudioOptions)ao).bitrate;
                textBoxExtension.Text = ((AdvancedAudioOptions)ao).extension;
                textBoxParameter.Text = ((AdvancedAudioOptions)ao).parameter;
                checkBoxAdditionalAc3Track.Checked = ((AdvancedAudioOptions)ao).additionalAc3Track;
            }
            catch (Exception)
            {
            }
        }

        private void comboBoxExtension_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (comboBoxExtension.SelectedIndex > -1)
                {
                    textBoxExtension.Text = comboBoxExtension.Text;
                }
            }
            catch (Exception)
            {
            }
        }

        private void comboBoxBitrate_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (comboBoxBitrate.SelectedIndex > -1)
                {
                    textBoxBitrate.Text = comboBoxBitrate.Text;
                }
            }
            catch (Exception)
            {
            }
        }

        private void textBoxExtension_TextChanged(object sender, EventArgs e)
        {
            try
            {
                ((AdvancedAudioOptions)ao).extension = textBoxExtension.Text;
            }
            catch (Exception)
            {
            }
        }

        private void textBoxBitrate_TextChanged(object sender, EventArgs e)
        {
            try
            {
                ((AdvancedAudioOptions)ao).bitrate = textBoxBitrate.Text;
            }
            catch (Exception)
            {
            }
        }

        private void textBoxParameter_TextChanged(object sender, EventArgs e)
        {
            try
            {
                ((AdvancedAudioOptions)ao).parameter = textBoxParameter.Text;
            }
            catch (Exception)
            {
            }
        }

        private void checkBoxAdditionalAc3Track_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                ((AdvancedAudioOptions)ao).additionalAc3Track = checkBoxAdditionalAc3Track.Checked;
            }
            catch (Exception)
            {
            }
        }
    }
}
