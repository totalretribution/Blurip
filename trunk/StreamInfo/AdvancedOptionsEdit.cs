using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace BluRip
{
    public partial class AdvancedOptionsEdit : Form
    {
        public AdvancedOptionsEdit()
        {
            InitializeComponent();
        }

        public AdvancedOptions ao = null;

        public AdvancedOptionsEdit(AdvancedOptions ao)
        {
            InitializeComponent();
            try
            {
                this.ao = new AdvancedOptions(ao);
            }
            catch (Exception)
            {
            }
        }
    }
}
