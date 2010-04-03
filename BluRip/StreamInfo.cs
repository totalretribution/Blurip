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
        private StreamInfoTool sit = null;
        private M2tsInfoTool mit = null;

        private List<TitleInfo> titleList = new List<TitleInfo>();
        private List<string> m2tsList = new List<string>();

        private void DemuxMsg(object sender, ExternalTool.MsgEventArgs e)
        {
            MessageDemux(e.Message.Replace("\b", "").Trim());
        }

        private void UpdateTitleList()
        {
            try
            {
                comboBoxTitle.Items.Clear();
                checkedListBoxStreams.Items.Clear();

                foreach (TitleInfo ti in titleList)
                {
                    comboBoxTitle.Items.Add(ti.desc);
                }
                if (titleList.Count > 0)
                {
                    comboBoxTitle.SelectedIndex = 0;
                }
            }
            catch (Exception)
            {
            }
        }

        private void buttonGetStreamInfo_Click(object sender, EventArgs e)
        {
            try
            {
                if (!checkEac3to()) return;
                this.Text = title + " [Getting stream info...]";

                progressBarMain.Visible = true;
                buttonAbort.Visible = true;

                comboBoxTitle.Items.Clear();
                checkedListBoxStreams.Items.Clear();
                m2tsList.Clear();

                sit = new StreamInfoTool(settings, ref titleList, textBoxPath.Text, videoTypes, ac3AudioTypes, dtsAudioTypes);
                sit.OnInfoMsg += new ExternalTool.InfoEventHandler(DemuxMsg);
                sit.OnLogMsg += new ExternalTool.LogEventHandler(DemuxMsg);
                sit.Start();
                sit.WaitForExit();
                if (!sit.Successfull)
                {
                    titleList.Clear();
                }

                UpdateTitleList();

                demuxedStreamList = new TitleInfo();
                UpdateDemuxedStreams();
            }
            catch (Exception ex)
            {
                MessageDemux("Exception: " + ex.Message);
            }
            finally
            {
                progressBarMain.Visible = false;
                buttonAbort.Visible = false;
                this.Text = title;
            }
        }

        private void buttonOpenM2ts_Click(object sender, EventArgs e)
        {
            try
            {
                if (!checkEac3to()) return;
                FileListForm flf = new FileListForm();
                if (flf.ShowDialog() == DialogResult.OK)
                {
                    comboBoxTitle.Items.Clear();
                    checkedListBoxStreams.Items.Clear();

                    titleList.Clear();
                    m2tsList.Clear();
                    foreach (string s in flf.fileList)
                    {
                        m2tsList.Add(s);
                    }
                    DoM2tsInfo();
                }
            }
            catch (Exception)
            {
            }
        }

        private void DoM2tsInfo()
        {
            try
            {
                this.Text = title + " [Getting m2ts stream info...]";

                progressBarMain.Visible = true;
                buttonAbort.Visible = true;

                comboBoxTitle.Items.Clear();
                checkedListBoxStreams.Items.Clear();

                mit = new M2tsInfoTool(settings, ref titleList, m2tsList, videoTypes, ac3AudioTypes, dtsAudioTypes);
                mit.OnInfoMsg += new ExternalTool.InfoEventHandler(DemuxMsg);
                mit.OnLogMsg += new ExternalTool.LogEventHandler(DemuxMsg);
                mit.Start();
                mit.WaitForExit();
                if (!mit.Successfull)
                {
                    titleList.Clear();
                }

                UpdateTitleList();

                demuxedStreamList = new TitleInfo();
                UpdateDemuxedStreams();
            }
            catch (Exception ex)
            {
                MessageDemux("Exception: " + ex.Message);
            }
            finally
            {
                progressBarMain.Visible = false;
                buttonAbort.Visible = false;
                this.Text = title;
            }
        }
    }
}