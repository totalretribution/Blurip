using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace BluRip
{
    public partial class AdvancedVideoOptionsEdit : AdvancedOptionsEdit
    {
        public AdvancedVideoOptionsEdit()
        {
            InitializeComponent();
        }

        public AdvancedVideoOptionsEdit(AdvancedOptions ao) : base(ao)
        {
            InitializeComponent();
            try
            {
                this.ao = new AdvancedVideoOptions(ao);
                checkBoxManualFps.Checked = ((AdvancedVideoOptions)ao).disableFps;
                checkBoxManualAutoCrop.Checked = ((AdvancedVideoOptions)ao).disableAutocrop;
                textBoxFps.Text = ((AdvancedVideoOptions)ao).fps;
                checkBoxManualAutoCrop.Checked = ((AdvancedVideoOptions)ao).disableAutocrop;
                checkBoxManualCrop.Checked = ((AdvancedVideoOptions)ao).manualCrop;
                checkBoxManualBorders.Checked = ((AdvancedVideoOptions)ao).manualBorders;
                checkBoxManualResize.Checked = ((AdvancedVideoOptions)ao).manualResize;

                textBoxCropBottom.Text = ((AdvancedVideoOptions)ao).cropBottom.ToString();
                textBoxCropLeft.Text = ((AdvancedVideoOptions)ao).cropLeft.ToString();
                textBoxCropRight.Text = ((AdvancedVideoOptions)ao).cropRight.ToString();
                textBoxCropTop.Text = ((AdvancedVideoOptions)ao).cropTop.ToString();

                textBoxSizeX.Text = ((AdvancedVideoOptions)ao).sizeX.ToString();
                textBoxSizeY.Text = ((AdvancedVideoOptions)ao).sizeY.ToString();

                textBoxBorderBottom.Text = ((AdvancedVideoOptions)ao).borderBottom.ToString();
                textBoxBorderLeft.Text = ((AdvancedVideoOptions)ao).borderLeft.ToString();
                textBoxBorderRight.Text = ((AdvancedVideoOptions)ao).borderRight.ToString();
                textBoxBorderTop.Text = ((AdvancedVideoOptions)ao).borderTop.ToString();
            }
            catch (Exception)
            {
            }
        }

        private void checkBoxManualFps_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxManualFps.Checked)
            {
                groupBoxFps.Enabled = true;
            }
            else
            {
                groupBoxFps.Enabled = false;
            }
            ((AdvancedVideoOptions)ao).disableFps = checkBoxManualFps.Checked;
        }

        private void AdvancedVideoOptionsEdit_Shown(object sender, EventArgs e)
        {
            try
            {
                checkBoxManualFps_CheckedChanged(null, null);
                checkBoxManualAutoCrop_CheckedChanged(null, null);
                checkBoxManualCrop_CheckedChanged(null, null);
                checkBoxManualBorders_CheckedChanged(null, null);
                checkBoxManualResize_CheckedChanged(null, null);
            }
            catch (Exception)
            {
            }
        }

        private void textBoxFps_TextChanged(object sender, EventArgs e)
        {
            try
            {
                ((AdvancedVideoOptions)ao).fps = textBoxFps.Text;
            }
            catch (Exception)
            {
            }
        }

        private void checkBoxManualAutoCrop_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxManualAutoCrop.Checked)
            {
                groupBoxAutocrop.Enabled = true;
            }
            else
            {
                groupBoxAutocrop.Enabled = false;
            }
            ((AdvancedVideoOptions)ao).disableAutocrop = checkBoxManualAutoCrop.Checked;
        }

        private void checkBoxManualCrop_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxManualCrop.Checked)
            {
                groupBoxCrop.Enabled = true;
            }
            else
            {
                groupBoxCrop.Enabled = false;
            }
            ((AdvancedVideoOptions)ao).manualCrop = checkBoxManualCrop.Checked;
        }

        private void checkBoxManualBorders_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxManualBorders.Checked)
            {
                groupBoxAddBorders.Enabled = true;
            }
            else
            {
                groupBoxAddBorders.Enabled = false;
            }
            ((AdvancedVideoOptions)ao).manualBorders = checkBoxManualBorders.Checked;
        }

        private void checkBoxManualResize_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxManualResize.Checked)
            {
                groupBoxResize.Enabled = true;
            }
            else
            {
                groupBoxResize.Enabled = false;
            }
            ((AdvancedVideoOptions)ao).manualResize = checkBoxManualResize.Checked;
        }

        private void textBoxCropLeft_TextChanged(object sender, EventArgs e)
        {
            try
            {
                ((AdvancedVideoOptions)ao).cropLeft = Convert.ToInt32(textBoxCropLeft.Text);
            }
            catch (Exception)
            {
            }
        }

        private void textBoxCropRight_TextChanged(object sender, EventArgs e)
        {
            try
            {
                ((AdvancedVideoOptions)ao).cropRight = Convert.ToInt32(textBoxCropRight.Text);
            }
            catch (Exception)
            {
            }
        }

        private void textBoxCropTop_TextChanged(object sender, EventArgs e)
        {
            try
            {
                ((AdvancedVideoOptions)ao).cropTop = Convert.ToInt32(textBoxCropTop.Text);
            }
            catch (Exception)
            {
            }
        }

        private void textBoxCropBottom_TextChanged(object sender, EventArgs e)
        {
            try
            {
                ((AdvancedVideoOptions)ao).cropBottom = Convert.ToInt32(textBoxCropBottom.Text);
            }
            catch (Exception)
            {
            }
        }

        private void textBoxBorderLeft_TextChanged(object sender, EventArgs e)
        {
            try
            {
                ((AdvancedVideoOptions)ao).borderLeft = Convert.ToInt32(textBoxBorderLeft.Text);
            }
            catch (Exception)
            {
            }
        }

        private void textBoxBorderRight_TextChanged(object sender, EventArgs e)
        {
            try
            {
                ((AdvancedVideoOptions)ao).borderRight = Convert.ToInt32(textBoxBorderRight.Text);
            }
            catch (Exception)
            {
            }
        }

        private void textBoxBorderTop_TextChanged(object sender, EventArgs e)
        {
            try
            {
                ((AdvancedVideoOptions)ao).borderTop = Convert.ToInt32(textBoxBorderTop.Text);
            }
            catch (Exception)
            {
            }
        }

        private void textBoxBorderBottom_TextChanged(object sender, EventArgs e)
        {
            try
            {
                ((AdvancedVideoOptions)ao).borderBottom = Convert.ToInt32(textBoxBorderBottom.Text);
            }
            catch (Exception)
            {
            }
        }

        private void textBoxSizeX_TextChanged(object sender, EventArgs e)
        {
            try
            {
                ((AdvancedVideoOptions)ao).sizeX = Convert.ToInt32(textBoxSizeX.Text);
            }
            catch (Exception)
            {
            }
        }

        private void textBoxSizeY_TextChanged(object sender, EventArgs e)
        {
            try
            {
                ((AdvancedVideoOptions)ao).sizeY = Convert.ToInt32(textBoxSizeY.Text);
            }
            catch (Exception)
            {
            }
        }

        
    }
}
