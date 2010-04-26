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
        private EncodeTool et = null;
        private string lastMsg = "";
        public bool secondPass = false;

        private void buttonDoEncode_Click(object sender, EventArgs e)
        {
            try
            {
                if (!checkX264()) return;
                progressBarMain.Visible = true;
                buttonAbort.Visible = true;
                tabControlMain.Enabled = false;

                DoEncode();
            }
            catch (Exception ex)
            {
                MessageEncode("Exception: " + ex.Message);
            }
            finally
            {
                tabControlMain.Enabled = true;
                progressBarMain.Visible = false;
                buttonAbort.Visible = false;
            }
        }

        private void EncodeMsg(object sender, ExternalTool.MsgEventArgs e)
        {
            string text = e.Message;

            if (!settings.use64bit)
            {
                int start = text.IndexOf('[');
                int end = text.IndexOf(']');
                if (start == 0 && end > 0)
                {
                    int index = comboBoxEncodeProfile.SelectedIndex;
                    string substr = text.Substring(start + 1, end - start - 2);
                    if (substr != lastMsg)
                    {
                        lastMsg = substr;
                        MessageEncode(text);
                        if (index > -1 && settings.encodingSettings[index].pass2)
                        {
                            if (!secondPass)
                            {
                                Text = title + " [Encoding 1. pass " + text + "]";
                                notifyIconMain.Text = "Encoding 1. pass - " + substr + "%";
                            }
                            else
                            {
                                Text = title + " [Encoding 2. pass " + text + "]";
                                notifyIconMain.Text = "Encoding 2. pass - " + substr + "%";
                            }
                        }
                        else
                        {
                            Text = title + " [Encoding " + text + "]";
                            notifyIconMain.Text = "Encoding - " + substr + "%";
                        }
                    }
                }
                else
                {
                    MessageEncode(text);
                }
            }
            else
            {
                MessageEncode(text);
            }
        }

        private bool DoEncode()
        {
            try
            {
                this.Text = title + " [Encoding...]";
                notifyIconMain.Text = this.Text;

                if (settings.workingDir == "")
                {
                    MessageMain("Working dir not set");
                    if (!silent) MessageBox.Show("Working dir not set", "Error");
                    return false;
                }
                if (demuxedStreamList.streams.Count == 0)
                {
                    MessageMain("No demuxed streams available");
                    if (!silent) MessageBox.Show("No demuxed streams available", "Error");
                    return false;
                }

                VideoFileInfo vfi = null;

                foreach (StreamInfo si in demuxedStreamList.streams)
                {
                    if (si.streamType == StreamType.Video)
                    {
                        if (si.extraFileInfo.GetType() == typeof(VideoFileInfo))
                        {
                            vfi = (VideoFileInfo)si.extraFileInfo;
                            break;
                        }
                    }
                }

                if (vfi == null || vfi.encodeAvs == "")
                {
                    MessageMain("Encode avs not set - do index + autocrop first");
                    if (!silent) MessageBox.Show("Encode avs not set - do index + autocrop first", "Error");
                    return false;
                }

                int profile = comboBoxEncodeProfile.SelectedIndex;
                if (profile < 0)
                {
                    MessageMain("Encoding profile not set");
                    if (!silent) MessageBox.Show("Encoding profile not set", "Error");
                    return false;
                }

                lastMsg = "";
                secondPass = false;

                et = new EncodeTool(settings, demuxedStreamList, profile, false, vfi);
                et.OnInfoMsg += new ExternalTool.InfoEventHandler(EncodeMsg);
                et.OnLogMsg += new ExternalTool.LogEventHandler(EncodeMsg);
                et.Start();
                et.WaitForExit();

                if (!et.Successfull)
                {
                    MessageMain("Encode failed!");
                    return false;
                }
                if (settings.encodingSettings[profile].pass2)
                {
                    secondPass = true;
                    et = new EncodeTool(settings, demuxedStreamList, profile, true, vfi);
                    et.OnInfoMsg += new ExternalTool.InfoEventHandler(EncodeMsg);
                    et.OnLogMsg += new ExternalTool.LogEventHandler(EncodeMsg);
                    et.Start();
                    et.WaitForExit();
                    return et.Successfull;
                }

                return true;
            }
            catch (Exception ex)
            {
                MessageEncode("Exception: " + ex.Message);
                return false;
            }
            finally
            {
                this.Text = title;
                notifyIconMain.Text = this.Text;
            }
        }
    }
}