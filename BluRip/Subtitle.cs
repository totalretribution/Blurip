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
        SubtitleTool st = null;

        private void SubtitleMsg(object sender, ExternalTool.MsgEventArgs e)
        {
            string tmp = e.Message.Replace("\b", "").Trim();
            if (!tmp.StartsWith("#>") && !tmp.StartsWith("Decoding frame") && !tmp.StartsWith("#<"))
            {
                MessageSubtitle(tmp);
            }
        }

        private bool DoSubtitle()
        {
            try
            {
                if (demuxedStreamList.streams.Count == 0)
                {
                    MessageSubtitle("No demuxed streams available");
                    if (!silent) MessageBox.Show("No demuxed streams available", "Error");
                    return false;
                }

                string fps = "";
                foreach (StreamInfo si in demuxedStreamList.streams)
                {
                    if (si.streamType == StreamType.Video)
                    {
                        if (si.extraFileInfo.GetType() == typeof(VideoFileInfo))
                        {
                            fps = ((VideoFileInfo)si.extraFileInfo).fps;
                            break;
                        }
                    }
                }
                if (fps == "")
                {
                    MessageSubtitle("Framerate not set - do index + autocrop first");
                    if (!silent) MessageBox.Show("Framerate not set - do index + autocrop first", "Error");
                    return false;
                }
                                
                this.Text = title + " [Processing subtitles...]";
                notifyIconMain.Text = this.Text;

                int subtitleCount = 0;
                foreach (StreamInfo si in demuxedStreamList.streams)
                {
                    if (si.streamType == StreamType.Subtitle)
                    {
                        subtitleCount++;
                    }
                }

                if (subtitleCount == 0)
                {
                    MessageSubtitle("No subtitles to process");
                    return true;
                }

                bool error = false;
                int subtitle = 0;
                for(int i=0; i < demuxedStreamList.streams.Count; i++)
                {
                    if (demuxedStreamList.streams[i].streamType == StreamType.Subtitle)
                    {
                        subtitle++;
                        this.Text = title + " [Processing subtitles (normal) (" + subtitle.ToString() + "/" + subtitleCount.ToString() + ")...]";
                        StreamInfo si = demuxedStreamList.streams[i];
                        st = new SubtitleTool(settings, fps, ref si, false, false);
                        st.OnInfoMsg += new ExternalTool.InfoEventHandler(SubtitleMsg);
                        st.OnLogMsg += new ExternalTool.LogEventHandler(SubtitleMsg);
                        st.Start();
                        st.WaitForExit();
                        if (!st.Successfull) error = true;

                        this.Text = title + " [Processing subtitles (forced) (" + subtitle.ToString() + "/" + subtitleCount.ToString() + ")...]";
                        st = new SubtitleTool(settings, fps, ref si, true, false);
                        st.OnInfoMsg += new ExternalTool.InfoEventHandler(SubtitleMsg);
                        st.OnLogMsg += new ExternalTool.LogEventHandler(SubtitleMsg);
                        st.Start();
                        st.WaitForExit();
                        if (!st.Successfull) error = true;

                        if (settings.muxLowResSubs)
                        {
                            this.Text = title + " [Processing lowres subtitles (normal) (" + subtitle.ToString() + "/" + subtitleCount.ToString() + ")...]";
                            si = demuxedStreamList.streams[i];
                            st = new SubtitleTool(settings, fps, ref si, false, true);
                            st.OnInfoMsg += new ExternalTool.InfoEventHandler(SubtitleMsg);
                            st.OnLogMsg += new ExternalTool.LogEventHandler(SubtitleMsg);
                            st.Start();
                            st.WaitForExit();
                            if (!st.Successfull) error = true;

                            this.Text = title + " [Processing lowres subtitles (forced) (" + subtitle.ToString() + "/" + subtitleCount.ToString() + ")...]";
                            st = new SubtitleTool(settings, fps, ref si, true, true);
                            st.OnInfoMsg += new ExternalTool.InfoEventHandler(SubtitleMsg);
                            st.OnLogMsg += new ExternalTool.LogEventHandler(SubtitleMsg);
                            st.Start();
                            st.WaitForExit();
                            if (!st.Successfull) error = true;
                        }

                        if (!error)
                        {
                            if (si.extraFileInfo != null && si.extraFileInfo.GetType() == typeof(SubtitleFileInfo))
                            {
                                SubtitleFileInfo sfi = (SubtitleFileInfo)si.extraFileInfo;
                                if (sfi.forcedIdx != "" && sfi.normalIdx != "")
                                {
                                    StreamInfo si2 = new StreamInfo(demuxedStreamList.streams[i]);
                                    if (demuxedStreamList.streams[i].extraFileInfo != null && demuxedStreamList.streams[i].extraFileInfo.GetType() == typeof(SubtitleFileInfo))
                                    {
                                        ((SubtitleFileInfo)demuxedStreamList.streams[i].extraFileInfo).forcedIdx = "";
                                        ((SubtitleFileInfo)demuxedStreamList.streams[i].extraFileInfo).forcedSub = "";
                                        ((SubtitleFileInfo)demuxedStreamList.streams[i].extraFileInfo).forcedIdxLowRes = "";
                                        ((SubtitleFileInfo)demuxedStreamList.streams[i].extraFileInfo).forcedSubLowRes = "";
                                    }
                                    si2.desc += " (only forced)"; ;
                                    if (si2.extraFileInfo != null && si2.extraFileInfo.GetType() == typeof(SubtitleFileInfo))
                                    {
                                        ((SubtitleFileInfo)si2.extraFileInfo).normalIdx = "";
                                        ((SubtitleFileInfo)si2.extraFileInfo).normalSub = "";
                                        ((SubtitleFileInfo)si2.extraFileInfo).normalIdxLowRes = "";
                                        ((SubtitleFileInfo)si2.extraFileInfo).normalSubLowRes = "";
                                    }
                                    demuxedStreamList.streams.Insert(i + 1, si2);
                                    i++;
                                }
                            }
                        }
                    }
                }

                if (error)
                {
                    MessageSubtitle("Error processing subtitle");
                    return false;
                }

                TitleInfo.SaveStreamInfoFile(demuxedStreamList, settings.workingDir + "\\" + settings.filePrefix + "_streamInfo.xml");
                UpdateDemuxedStreams();
                
                return true;
            }
            catch (Exception ex)
            {
                MessageSubtitle("Exception: " + ex.Message);
                return false;
            }
            finally
            {
                this.Text = title;
                notifyIconMain.Text = this.Text;
            }
        }

        private void buttonDoSubtitle_Click(object sender, EventArgs e)
        {
            try
            {
                if (!checkBdsup2sub()) return;
                progressBarMain.Visible = true;
                buttonAbort.Visible = true;
                tabControlMain.Enabled = false;

                DoSubtitle();
            }
            catch (Exception ex)
            {
                MessageSubtitle("Exception: " + ex.Message);
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