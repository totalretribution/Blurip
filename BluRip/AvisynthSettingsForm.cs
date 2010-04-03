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
    public partial class AvisynthSettingsForm : Form
    {
        public AvisynthSettings avs = null;

        public AvisynthSettingsForm()
        {
            InitializeComponent();
        }

        public AvisynthSettingsForm(AvisynthSettings avs)
        {
            InitializeComponent();
            try
            {
                this.avs = new AvisynthSettings(avs);
                textBoxDesc.Text = this.avs.desc;
                string[] lines = this.avs.commands.Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (string s in lines)
                {
                    richTextBoxCommands.Text += s.Trim() + "\r\n";
                }
            }
            catch (Exception)
            {
            }
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            try
            {
                avs.desc = textBoxDesc.Text;
                avs.commands = richTextBoxCommands.Text;
            }
            catch (Exception)
            {
            }
        }
    }
}
