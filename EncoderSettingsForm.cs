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
