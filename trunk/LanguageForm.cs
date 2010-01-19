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
        public LanguagInfo li = null;

        public LanguageForm()
        {
            InitializeComponent();
        }

        public LanguageForm(LanguagInfo li)
        {
            InitializeComponent();
            try
            {
                this.li = new LanguagInfo(li);
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
