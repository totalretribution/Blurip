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
        private MuxTool mt = null;

        private void MuxMsg(object sender, ExternalTool.MsgEventArgs e)
        {
            MessageMux(e.Message.Replace("\b", "").Trim());
        }

        private bool DoMux()
        {
            try
            {
                this.Text = title + " [Muxing...]";
                notifyIconMain.Text = this.Text;

                int videoStream = 0;
                int audioStream = 0;
                int chapterStream = 0;
                int subtitleStream = 0;

                foreach (StreamInfo si in demuxedStreamList.streams)
                {
                    if (si.streamType == StreamType.Video)
                    {
                        if (si.extraFileInfo.GetType() == typeof(VideoFileInfo))
                        {
                            if (((VideoFileInfo)si.extraFileInfo).encodedFile != "")
                            {
                                if (File.Exists(((VideoFileInfo)si.extraFileInfo).encodedFile))
                                {
                                    videoStream++;
                                }
                            }
                        }
                    }
                    else if (si.streamType == StreamType.Audio)
                    {
                        if (File.Exists(si.filename))
                        {
                            audioStream++;
                        }
                    }
                    else if (si.streamType == StreamType.Chapter)
                    {
                        chapterStream++;
                    }
                    else if (si.streamType == StreamType.Subtitle)
                    {
                        subtitleStream++;
                    }
                }
                if (videoStream == 0)
                {
                    if (!silent) MessageBox.Show("No videostream or encoded video filename not set", "Error");
                    MessageMux("No videostream or encoded video filename not set");
                    return false;
                }
                if (audioStream == 0)
                {
                    if (!silent) MessageBox.Show("No audiostream or audio filename not set", "Error");
                    MessageMux("No audiostream or audio filename not set");
                    return false;
                }
                if (chapterStream > 0)
                {
                    bool error = false;
                    foreach (StreamInfo si in demuxedStreamList.streams)
                    {
                        if (si.streamType == StreamType.Chapter)
                        {
                            if (!File.Exists(si.filename))
                            {
                                error = true;
                            }
                        }
                    }
                    if (error)
                    {
                        if (!silent) MessageBox.Show("Chapter file not found", "Error");
                        MessageMux("Chapter file not found");
                        return false;
                    }
                }
                if (subtitleStream > 0)
                {
                    bool error = false;
                    foreach (StreamInfo si in demuxedStreamList.streams)
                    {
                        if (si.streamType == StreamType.Subtitle)
                        {
                            if (si.extraFileInfo.GetType() == typeof(SubtitleFileInfo))
                            {
                                SubtitleFileInfo sfi = (SubtitleFileInfo)si.extraFileInfo;
                                if (sfi.forcedIdx != "")
                                {
                                    if (!File.Exists(sfi.forcedIdx)) error = true;
                                }
                                if (sfi.forcedSub != "")
                                {
                                    if (!File.Exists(sfi.forcedSub)) error = true;
                                }

                                if (sfi.normalIdx != "")
                                {
                                    if (!File.Exists(sfi.normalIdx)) error = true;
                                }
                                if (sfi.normalSub != "")
                                {
                                    if (!File.Exists(sfi.normalSub)) error = true;
                                }
                            }
                            else
                            {
                                error = false;
                            }
                        }
                    }
                    if (error)
                    {
                        if (!silent) MessageBox.Show("Subtitle file(s) not found", "Error");
                        MessageMux("Subtitle file(s) not found");
                        return false;
                    }
                }

                mt = new MuxTool(settings, demuxedStreamList);
                mt.OnInfoMsg += new ExternalTool.InfoEventHandler(MuxMsg);
                mt.OnLogMsg += new ExternalTool.LogEventHandler(MuxMsg);
                mt.Start();
                mt.WaitForExit();
                return mt.Successfull;                
            }
            catch (Exception ex)
            {
                MessageMain("Exception: " + ex.Message);
                return false;
            }
            finally
            {
                this.Text = title;
                notifyIconMain.Text = this.Text;
            }
        }

        private void buttonDoMux_Click(object sender, EventArgs e)
        {
            try
            {
                if (!checkMkvmerge()) return;
                progressBarMain.Visible = true;
                buttonAbort.Visible = true;
                tabControlMain.Enabled = false;

                DoMux();
            }
            catch (Exception ex)
            {
                MessageMain("Exception: " + ex.Message);
            }
            finally
            {
                tabControlMain.Enabled = true;
                progressBarMain.Visible = false;
                buttonAbort.Visible = false;
            }
        }
    }
}