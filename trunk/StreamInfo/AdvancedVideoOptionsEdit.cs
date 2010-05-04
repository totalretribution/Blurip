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

                checkBoxManualInputRes.Checked = ((AdvancedVideoOptions)ao).manualInputRes;
                textBoxInputResX.Text = ((AdvancedVideoOptions)ao).inputResX.ToString();
                textBoxInputResY.Text = ((AdvancedVideoOptions)ao).inputresY.ToString();

                checkBoxnoMkvDemux.Checked = ((AdvancedVideoOptions)ao).noMkvDemux;
                textBoxVideoExtension.Text = ((AdvancedVideoOptions)ao).videoExtension;
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
                checkBoxManualInputRes_CheckedChanged(null, null);
                checkBoxnoMkvDemux_CheckedChanged(null, null);
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

        private void comboBoxFramerate_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (comboBoxFramerate.SelectedIndex > -1)
                {
                    textBoxFps.Text = comboBoxFramerate.Text;
                }
            }
            catch (Exception)
            {
            }
        }

        private void checkBoxManualInputRes_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxManualInputRes.Checked)
            {
                groupBoxInputResolution.Enabled = true;
            }
            else
            {
                groupBoxInputResolution.Enabled = false;
            }
            ((AdvancedVideoOptions)ao).manualInputRes = checkBoxManualInputRes.Checked;
        }

        private void textBoxInputResX_TextChanged(object sender, EventArgs e)
        {
            try
            {
                ((AdvancedVideoOptions)ao).inputResX = Convert.ToInt32(textBoxInputResX.Text);
            }
            catch (Exception)
            {
            }
        }

        private void textBoxInputResY_TextChanged(object sender, EventArgs e)
        {
            try
            {
                ((AdvancedVideoOptions)ao).inputresY = Convert.ToInt32(textBoxInputResY.Text);
            }
            catch (Exception)
            {
            }
        }

        private void checkBoxnoMkvDemux_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxnoMkvDemux.Checked)
            {
                groupBoxNoMkvDemux.Enabled = true;
            }
            else
            {
                groupBoxNoMkvDemux.Enabled = false;
            }
            ((AdvancedVideoOptions)ao).noMkvDemux = checkBoxnoMkvDemux.Checked;
        }

        private void comboBoxVideoExtension_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (comboBoxVideoExtension.SelectedIndex > -1)
                {
                    textBoxVideoExtension.Text = comboBoxVideoExtension.Text;
                }
            }
            catch (Exception)
            {
            }
        }

        private void textBoxVideoExtension_TextChanged(object sender, EventArgs e)
        {
            try
            {

                ((AdvancedVideoOptions)ao).videoExtension = textBoxVideoExtension.Text;
            }
            catch (Exception)
            {
            }
        }

        
    }
}
