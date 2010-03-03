//BluRip - one click BluRay/m2ts to mkv converter
//Copyright (C) 2009-2010  _hawk_

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

// Contact: hawk.ac@gmx.net

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BluRip
{
    public partial class LanguageForm : Form
    {
        public LanguageInfo li = null;

        public LanguageForm()
        {
            InitializeComponent();
        }

        public LanguageForm(LanguageInfo li)
        {
            InitializeComponent();
            try
            {
                this.li = new LanguageInfo(li);
                textBoxLanguage.Text = this.li.language;
                textBoxLanguageShort.Text = this.li.languageShort;
                textBoxTranslation.Text = this.li.translation;
            }
            catch (Exception)
            {
            }
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            try
            {
                li.language = textBoxLanguage.Text;
                li.languageShort = textBoxLanguageShort.Text;
                li.translation = textBoxTranslation.Text;
            }
            catch (Exception)
            {
            }
        }
    }
}
