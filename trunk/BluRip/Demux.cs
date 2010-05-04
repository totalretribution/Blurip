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
using System.Diagnostics;
using System.Threading;
using System.Text.RegularExpressions;
using System.IO;
using MediaInfoLib;

namespace BluRip
{
    public partial class MainForm : Form
    {
        private DemuxTool dt = null;
        private TitleInfo demuxedStreamList = new TitleInfo();

        private bool DoDemux()
        {
            try
            {
                this.Text = title + " [Demuxing...]";
                notifyIconMain.Text = this.Text;

                if (!Directory.Exists(settings.workingDir))
                {
                    MessageDemux("Working dir not set");
                    if (!silent) MessageBox.Show("Working dir not set", "Error");
                    return false;
                }
                if (comboBoxTitle.SelectedIndex == -1)
                {
                    MessageDemux("No title selected");
                    if (!silent) MessageBox.Show("No title selected", "Error");
                    return false;
                }
                int videoCount = 0;
                int audioCount = 0;
                int unknown = 0;
                foreach (StreamInfo si in titleList[comboBoxTitle.SelectedIndex].streams)
                {
                    if (si.streamType == StreamType.Audio && si.selected)
                    {
                        audioCount++;
                    }
                    if (si.streamType == StreamType.Video && si.selected)
                    {
                        videoCount++;
                    }
                    if (si.streamType == StreamType.Unknown && si.selected)
                    {
                        unknown++;
                    }
                }
                if (audioCount < 1)
                {
                    MessageDemux("No audio streams selected");
                    if (!silent) MessageBox.Show("No audio streams selected", "Error");
                    return false;
                }
                if (videoCount != 1)
                {
                    MessageDemux("No video stream or more then one selected");
                    if (!silent) MessageBox.Show("No video stream or more then one selected", "Error");
                    return false;
                }
                if (unknown > 0)
                {
                    MessageDemux("Unknown tracks selected - please report log to developer");
                    if (!silent) MessageBox.Show("Unknown tracks selected - please report log to developer", "Error");
                    return false;
                }

                progressBarMain.Visible = true;
                buttonAbort.Visible = true;

                dt = new DemuxTool(settings, m2tsList, videoTypes, ac3AudioTypes, dtsAudioTypes,
                    titleList[comboBoxTitle.SelectedIndex], ref demuxedStreamList, comboBoxDownmixAc3.Text, comboBoxDownmixDts.Text);

                dt.OnInfoMsg += new ExternalTool.InfoEventHandler(DemuxMsg);
                dt.OnLogMsg += new ExternalTool.LogEventHandler(DemuxMsg);

                dt.Start();
                dt.WaitForExit();

                UpdateDemuxedStreams();

                return dt.Successfull;
            }
            catch (Exception ex)
            {
                MessageDemux("Exception: " + ex.Message);
                return false;
            }
            finally
            {
                this.Text = title;
                notifyIconMain.Text = this.Text;
            }
        }

        private void buttonDoDemux_Click(object sender, EventArgs e)
        {
            try
            {
                if (!checkEac3to()) return;
                progressBarMain.Visible = true;
                buttonAbort.Visible = true;
                tabControlMain.Enabled = false;

                DoDemux();
            }
            catch (Exception ex)
            {
                MessageDemux("Exception: " + ex.Message);
            }
            finally
            {
                tabControlMain.Enabled = true;
                progressBarMain.Visible = false;
                buttonAbort.Visible = false;
            }
        }

        private void UpdateDemuxedStreams()
        {
            try
            {
                listBoxDemuxedStreams.Items.Clear();
                foreach (StreamInfo si in demuxedStreamList.streams)
                {
                    listBoxDemuxedStreams.Items.Add(si.typeDesc + " - " + si.filename);
                }
            }
            catch (Exception)
            {
            }
        }
    }
}