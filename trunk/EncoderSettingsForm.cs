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
            }
            catch (Exception)
            {
            }
        }
    }
}
