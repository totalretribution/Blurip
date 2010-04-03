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
    public partial class EncoderSettingsForm : Form
    {
        public EncodingSettings es = null;

        public EncoderSettingsForm()
        {
            InitializeComponent();
        }

        public EncoderSettingsForm(EncodingSettings es)
        {
            InitializeComponent();
            try
            {
                this.es = new EncodingSettings(es);
                textBoxDesc.Text = this.es.desc;
                textBoxSettings.Text = this.es.settings;
                textBoxSettings2.Text = this.es.settings2;
                checkBox2pass.Checked = this.es.pass2;
                checkBox2pass_CheckedChanged(null, null);
            }
            catch (Exception)
            {
            }
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            try
            {
                es.desc = textBoxDesc.Text;
                es.settings = textBoxSettings.Text;
                es.pass2 = checkBox2pass.Checked;
                es.settings2 = textBoxSettings2.Text;
            }
            catch (Exception)
            {
            }
        }

        private void checkBox2pass_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (checkBox2pass.Checked)
                {
                    labelSettings2.Visible = true;
                    textBoxSettings2.Visible = true;
                    labelSettings.Text = "Parameter (first pass)";
                }
                else
                {
                    labelSettings2.Visible = false;
                    textBoxSettings2.Visible = false;
                    labelSettings.Text = "Parameter";
                }
            }
            catch (Exception)
            {
            }
        }
    }
}
