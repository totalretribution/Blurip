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
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using System.Windows.Forms;

namespace BluRip
{
    public partial class MainWindow : Window
    {
        SubtitleTool st = null;

        private void SubtitleMsg(object sender, ExternalTool.MsgEventArgs e)
        {
            string tmp = e.Message.Replace("\b", "").Trim();
            if (!tmp.StartsWith("#>") && !tmp.StartsWith("Decoding frame") && !tmp.StartsWith("#<"))
            {
                logWindow.MessageSubtitle(tmp);
            }
        }

        private bool DoSubtitle()
        {
            try
            {
                if (!Directory.Exists(settings.workingDir))
                {
                    logWindow.MessageDemux(Global.Res("ErrorWorkingDirectory"));
                    if (!silent) Global.ErrorMsg(Global.Res("ErrorWorkingDirectory"));
                    return false;
                }

                if (demuxedStreamList.streams.Count == 0)
                {
                    logWindow.MessageSubtitle(Global.Res("ErrorNoDemuxedStreams"));
                    if (!silent) Global.ErrorMsg(Global.Res("ErrorNoDemuxedStreams"));
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
                    logWindow.MessageSubtitle(Global.Res("ErrorSetFramerate"));
                    if (!silent) Global.ErrorMsg(Global.Res("ErrorSetFramerate"));
                    return false;
                }

                UpdateStatus(Global.Res("StatusBar") + " " + Global.Res("StatusBarSubtitle"));

                bool vobsub = false;
                int subtitleCount = 0;
                foreach (StreamInfo si in demuxedStreamList.streams)
                {
                    if (si.streamType == StreamType.Subtitle)
                    {
                        subtitleCount++;
                        if (si.extraFileInfo == null || si.extraFileInfo.GetType() != typeof(SubtitleFileInfo)) si.extraFileInfo = new SubtitleFileInfo();
                        if (si.advancedOptions != null && si.advancedOptions.GetType() == typeof(AdvancedSubtitleOptions))
                        {
                            if (((AdvancedSubtitleOptions)si.advancedOptions).vobSub)
                            {
                                vobsub = true;
                            }
                        }
                    }
                }

                if (subtitleCount == 0)
                {
                    logWindow.MessageSubtitle(Global.Res("InfoNoSubtitles"));
                    return true;
                }

                // do not mux and copy subs
                if (settings.muxSubs == 0 && settings.copySubs == 0 && !vobsub)
                {
                    logWindow.MessageSubtitle(Global.Res("InfoNoSubtitlesProcessing"));
                    return true;
                }
                // only untouched subs
                else if (settings.muxUntouchedSubs && settings.copyUntouchedSubs && !vobsub)
                {
                    logWindow.MessageSubtitle(Global.Res("InfoNoSubtitlesProcessing"));
                    return true;
                }
                else if (settings.muxUntouchedSubs && settings.copySubs == 0 && !vobsub)
                {
                    logWindow.MessageSubtitle(Global.Res("InfoNoSubtitlesProcessing"));
                    return true;
                }
                else if (settings.muxSubs == 0 && settings.copyUntouchedSubs && !vobsub)
                {
                    logWindow.MessageSubtitle(Global.Res("InfoNoSubtitlesProcessing"));
                    return true;
                }

                DisableControls();

                bool sub = false;
                bool sup = false;

                if (settings.muxSubs > 0 && settings.muxSubs < 4)
                {
                    sub = true;
                }
                else if (settings.muxSubs >= 4)
                {
                    sup = true;
                }

                if (settings.copySubs > 0 && settings.copySubs < 4)
                {
                    sub = true;
                }
                else if (settings.copySubs >= 4)
                {
                    sup = true;
                }

                // hardcode subs? enable sub/idx processing
                if (vobsub)
                {
                    sub = true;
                }

                bool error = false;
                int subtitle = 0;
                for (int i = 0; i < demuxedStreamList.streams.Count; i++)
                {
                    if (demuxedStreamList.streams[i].streamType == StreamType.Subtitle)
                    {
                        if (demuxedStreamList.streams[i].extraFileInfo != null && demuxedStreamList.streams[i].extraFileInfo.GetType() == typeof(SubtitleFileInfo) &&
                            ((SubtitleFileInfo)demuxedStreamList.streams[i].extraFileInfo).isSecond) continue;

                        subtitle++;
                        StreamInfo si = demuxedStreamList.streams[i];
                        if (sub)
                        {
                            UpdateStatus(Global.Res("StatusBar") + " " + String.Format(Global.Res("StatusBarSubtitleNormal"), subtitle, subtitleCount));                            
                            st = new SubtitleTool(settings, fps, ref si, false, false, false);
                            st.OnInfoMsg += new ExternalTool.InfoEventHandler(SubtitleMsg);
                            st.OnLogMsg += new ExternalTool.LogEventHandler(SubtitleMsg);
                            st.Start();
                            st.WaitForExit();
                            if (st == null || !st.Successfull) error = true;

                            UpdateStatus(Global.Res("StatusBar") + " " + String.Format(Global.Res("StatusBarSubtitleForced"), subtitle, subtitleCount));
                            st = new SubtitleTool(settings, fps, ref si, true, false, false);
                            st.OnInfoMsg += new ExternalTool.InfoEventHandler(SubtitleMsg);
                            st.OnLogMsg += new ExternalTool.LogEventHandler(SubtitleMsg);
                            st.Start();
                            st.WaitForExit();
                            if (st == null || !st.Successfull) error = true;
                        }
                        if (sup)
                        {
                            UpdateStatus(Global.Res("StatusBar") + " " + String.Format(Global.Res("StatusBarSubtitleNormalPgs"), subtitle, subtitleCount));
                            st = new SubtitleTool(settings, fps, ref si, false, false, true);
                            st.OnInfoMsg += new ExternalTool.InfoEventHandler(SubtitleMsg);
                            st.OnLogMsg += new ExternalTool.LogEventHandler(SubtitleMsg);
                            st.Start();
                            st.WaitForExit();
                            if (st == null || !st.Successfull) error = true;

                            UpdateStatus(Global.Res("StatusBar") + " " + String.Format(Global.Res("StatusBarSubtitleForcedPgs"), subtitle, subtitleCount));
                            st = new SubtitleTool(settings, fps, ref si, true, false, true);
                            st.OnInfoMsg += new ExternalTool.InfoEventHandler(SubtitleMsg);
                            st.OnLogMsg += new ExternalTool.LogEventHandler(SubtitleMsg);
                            st.Start();
                            st.WaitForExit();
                            if (st == null || !st.Successfull) error = true;
                        }

                        if (settings.muxLowResSubs && (settings.muxSubs > 0 && settings.muxSubs <4) && !vobsub)
                        {
                            UpdateStatus(Global.Res("StatusBar") + " " + String.Format(Global.Res("StatusBarSubtitleLowresNormal"), subtitle, subtitleCount));
                            si = demuxedStreamList.streams[i];
                            st = new SubtitleTool(settings, fps, ref si, false, true, false);
                            st.OnInfoMsg += new ExternalTool.InfoEventHandler(SubtitleMsg);
                            st.OnLogMsg += new ExternalTool.LogEventHandler(SubtitleMsg);
                            st.Start();
                            st.WaitForExit();
                            if (st == null || !st.Successfull) error = true;

                            UpdateStatus(Global.Res("StatusBar") + " " + String.Format(Global.Res("StatusBarSubtitleLowresForced"), subtitle, subtitleCount));
                            st = new SubtitleTool(settings, fps, ref si, true, true, false);
                            st.OnInfoMsg += new ExternalTool.InfoEventHandler(SubtitleMsg);
                            st.OnLogMsg += new ExternalTool.LogEventHandler(SubtitleMsg);
                            st.Start();
                            st.WaitForExit();
                            if (st == null || !st.Successfull) error = true;
                        }

                        if (!error)
                        {
                            if (si.extraFileInfo != null && si.extraFileInfo.GetType() == typeof(SubtitleFileInfo))
                            {
                                SubtitleFileInfo sfi = (SubtitleFileInfo)si.extraFileInfo;
                                if (((sfi.forcedIdx != "" && sfi.normalIdx != "") || (sfi.forcedSup != "" && sfi.normalSup != "")) && !vobsub)
                                {
                                    StreamInfo si2 = new StreamInfo(demuxedStreamList.streams[i]);
                                    if (demuxedStreamList.streams[i].extraFileInfo != null && demuxedStreamList.streams[i].extraFileInfo.GetType() == typeof(SubtitleFileInfo))
                                    {
                                        ((SubtitleFileInfo)demuxedStreamList.streams[i].extraFileInfo).forcedIdx = "";
                                        ((SubtitleFileInfo)demuxedStreamList.streams[i].extraFileInfo).forcedSub = "";
                                        ((SubtitleFileInfo)demuxedStreamList.streams[i].extraFileInfo).forcedSup = "";
                                        ((SubtitleFileInfo)demuxedStreamList.streams[i].extraFileInfo).forcedIdxLowRes = "";
                                        ((SubtitleFileInfo)demuxedStreamList.streams[i].extraFileInfo).forcedSubLowRes = "";
                                        ((SubtitleFileInfo)demuxedStreamList.streams[i].extraFileInfo).isSecond = false;
                                    }
                                    si2.desc += " (only forced)"; ;
                                    if (si2.extraFileInfo != null && si2.extraFileInfo.GetType() == typeof(SubtitleFileInfo))
                                    {
                                        ((SubtitleFileInfo)si2.extraFileInfo).normalIdx = "";
                                        ((SubtitleFileInfo)si2.extraFileInfo).normalSub = "";
                                        ((SubtitleFileInfo)si2.extraFileInfo).normalSup = "";
                                        ((SubtitleFileInfo)si2.extraFileInfo).normalIdxLowRes = "";
                                        ((SubtitleFileInfo)si2.extraFileInfo).normalSubLowRes = "";
                                        ((SubtitleFileInfo)si2.extraFileInfo).isSecond = true;
                                    }
                                    bool add = true;
                                    if (demuxedStreamList.streams.Count > i + 1)
                                    {
                                        if (demuxedStreamList.streams[i + 1].extraFileInfo != null && demuxedStreamList.streams[i + 1].extraFileInfo.GetType() == typeof(SubtitleFileInfo))
                                        {
                                            SubtitleFileInfo stfi = (SubtitleFileInfo)demuxedStreamList.streams[i + 1].extraFileInfo;
                                            if (stfi.normalIdx == ((SubtitleFileInfo)si2.extraFileInfo).normalIdx &&
                                                stfi.normalSub == ((SubtitleFileInfo)si2.extraFileInfo).normalSub &&
                                                stfi.normalSup == ((SubtitleFileInfo)si2.extraFileInfo).normalSup &&
                                                stfi.normalIdxLowRes == ((SubtitleFileInfo)si2.extraFileInfo).normalIdxLowRes &&
                                                stfi.normalSubLowRes == ((SubtitleFileInfo)si2.extraFileInfo).normalSubLowRes &&
                                                stfi.forcedIdx == ((SubtitleFileInfo)si2.extraFileInfo).forcedIdx &&
                                                stfi.forcedSub == ((SubtitleFileInfo)si2.extraFileInfo).forcedSub &&
                                                stfi.forcedSup == ((SubtitleFileInfo)si2.extraFileInfo).forcedSup &&
                                                stfi.forcedIdxLowRes == ((SubtitleFileInfo)si2.extraFileInfo).forcedIdxLowRes &&
                                                stfi.forcedSubLowRes == ((SubtitleFileInfo)si2.extraFileInfo).forcedSubLowRes &&
                                                demuxedStreamList.streams[i + 1].filename == si2.filename)
                                            {
                                                add = false;
                                            }
                                        }
                                    }
                                    if (add)
                                    {
                                        demuxedStreamList.streams.Insert(i + 1, si2);
                                        i++;
                                    }
                                }
                                // treat track as forced track even if it doesn't contain forced subs
                                else if ((sfi.normalIdx != "" || sfi.normalSup != "") && !vobsub)
                                {
                                    if (si.advancedOptions != null && si.advancedOptions.GetType() == typeof(AdvancedSubtitleOptions))
                                    {
                                        if (((AdvancedSubtitleOptions)si.advancedOptions).isForced)
                                        {
                                            sfi.forcedIdx = sfi.normalIdx;
                                            sfi.forcedSub = sfi.normalSub;
                                            sfi.forcedSup = sfi.normalSup;

                                            sfi.forcedIdxLowRes = sfi.normalIdxLowRes;
                                            sfi.forcedSubLowRes = sfi.normalSubLowRes;

                                            sfi.normalIdx = "";
                                            sfi.normalSub = "";
                                            sfi.normalSup = "";

                                            sfi.normalIdxLowRes = "";
                                            sfi.normalSubLowRes = "";
                                        }
                                    }
                                }
                            }
                        }
                    }
                    demuxedStreamsWindow.UpdateDemuxedStreams();
                }

                if (error)
                {
                    logWindow.MessageSubtitle(Global.Res("ErrorSubtitle"));
                    return false;
                }

                TitleInfo.SaveStreamInfoFile(demuxedStreamList, settings.workingDir + "\\" + settings.filePrefix + "_streamInfo.xml");
                demuxedStreamsWindow.UpdateDemuxedStreams();

                return true;
            }
            catch (Exception ex)
            {
                logWindow.MessageSubtitle(Global.Res("ErrorException") + " " + ex.Message);
                return false;
            }
            finally
            {
                EnableControls();
                UpdateStatus(Global.Res("StatusBar") + " " + Global.Res("StatusBarReady"));
            }
        }

        private void buttonOnlySubtitle_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!checkBdsup2sub()) return;
                
                DoSubtitle();
            }
            catch (Exception ex)
            {
                logWindow.MessageSubtitle(Global.Res("ErrorException") + " " + ex.Message);
            }
            finally
            {
                
            }
        }

        private bool checkBdsup2sub()
        {
            try
            {
                // do not mux and copy subs
                if (settings.muxSubs == 0 && settings.copySubs == 0)
                {                    
                    return true;
                }
                // only untouched subs
                else if (settings.muxUntouchedSubs && settings.copyUntouchedSubs)
                {                    
                    return true;
                }
                else if (settings.muxUntouchedSubs && settings.copySubs == 0)
                {
                    return true;
                }
                else if (settings.muxSubs == 0 && settings.copyUntouchedSubs)
                {
                    return true;
                }
                
                bool sub = false;
                bool sup = false;

                if (settings.muxSubs > 0 && settings.muxSubs < 4)
                {
                    sub = true;
                }
                else if (settings.muxSubs >= 4)
                {
                    sup = true;
                }

                if (settings.copySubs > 0 && settings.copySubs < 4)
                {
                    sub = true;
                }
                else if (settings.copySubs >= 4)
                {
                    sup = true;
                }
                if (sub || sup)
                {
                    if (!File.Exists(settings.javaPath))
                    {
                        logWindow.MessageMain(Global.Res("ErrorJavaPath"));
                        if (!silent) Global.ErrorMsg(Global.Res("ErrorJavaPath"));
                        return false;
                    }
                    if (!File.Exists(settings.sup2subPath))
                    {
                        logWindow.MessageMain(Global.Res("ErrorBdsup2subPath"));
                        if (!silent) Global.ErrorMsg(Global.Res("ErrorBdsup2subPath"));
                        return false;
                    }
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}