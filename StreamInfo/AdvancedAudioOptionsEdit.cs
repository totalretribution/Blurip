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
            }
            catch (Exception)
            {
            }
        }
    }
}
