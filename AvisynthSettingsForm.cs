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
