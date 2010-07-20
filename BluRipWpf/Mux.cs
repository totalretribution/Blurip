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
        private MuxTool mt = null;

        private void MuxMsg(object sender, ExternalTool.MsgEventArgs e)
        {
            logWindow.MessageMux(e.Message.Replace("\b", "").Trim());
        }

        private bool DoMux()
        {
            try
            {
                if (!settings.doMux) return true;

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
                    if (!silent) Global.ErrorMsg(Global.Res("ErrorVideoFile"));
                    logWindow.MessageMux(Global.Res("ErrorVideoFile"));
                    return false;
                }
                if (audioStream == 0)
                {
                    if (!silent) Global.ErrorMsg(Global.Res("ErrorAudioFile"));
                    logWindow.MessageMux(Global.Res("ErrorAudioFile"));
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
                        if (!silent) Global.ErrorMsg(Global.Res("ErrorChapterFile"));
                        logWindow.MessageMux(Global.Res("ErrorChapterFile"));
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
                                if (sfi.forcedSup != "")
                                {
                                    if (!File.Exists(sfi.forcedSup)) error = true;
                                }

                                if (sfi.normalIdx != "")
                                {
                                    if (!File.Exists(sfi.normalIdx)) error = true;
                                }
                                if (sfi.normalSub != "")
                                {
                                    if (!File.Exists(sfi.normalSub)) error = true;
                                }
                                if (sfi.normalSup != "")
                                {
                                    if (!File.Exists(sfi.normalSup)) error = true;
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
                        if (!silent) Global.ErrorMsg(Global.Res("ErrorSubtitleFile"));
                        logWindow.MessageMux(Global.Res("ErrorSubtitleFile"));
                        return false;
                    }
                }

                DisableControls();
                UpdateStatus(Global.Res("StatusBar") + " " + Global.Res("StatusBarMux"));

                mt = new MuxTool(settings, demuxedStreamList);
                mt.OnInfoMsg += new ExternalTool.InfoEventHandler(MuxMsg);
                mt.OnLogMsg += new ExternalTool.LogEventHandler(MuxMsg);
                mt.Start();
                mt.WaitForExit();
                if (mt == null) return false;
                else return mt.Successfull;
            }
            catch (Exception ex)
            {
                logWindow.MessageMux(Global.Res("ErrorException") + " " + ex.Message);
                return false;
            }
            finally
            {
                EnableControls();
                UpdateStatus(Global.Res("StatusBar") + " " + Global.Res("StatusBarReady"));
            }
        }

        private bool checkMkvmerge()
        {
            try
            {
                if (!File.Exists(settings.mkvmergePath))
                {
                    logWindow.MessageMain(Global.Res("ErrorMkvmergePath"));
                    if (!silent) Global.ErrorMsg(Global.Res("ErrorMkvmergePath"));
                    return false;
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private void buttonOnlyMux_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!checkMkvmerge()) return;

                DoMux();
            }
            catch (Exception ex)
            {
                logWindow.MessageMain(Global.Res("ErrorException") + " " + ex.Message);
            }
            finally
            {
            }
        }
    }
}