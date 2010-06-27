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
                    if (!silent) ErrorMsg("No videostream or encoded video filename not set");
                    logWindow.MessageMux("No videostream or encoded video filename not set");
                    return false;
                }
                if (audioStream == 0)
                {
                    if (!silent) ErrorMsg("No audiostream or audio filename not set");
                    logWindow.MessageMux("No audiostream or audio filename not set");
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
                        if (!silent) ErrorMsg("Chapter file not found");
                        logWindow.MessageMux("Chapter file not found");
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
                        if (!silent) ErrorMsg("Subtitle file(s) not found");
                        logWindow.MessageMux("Subtitle file(s) not found");
                        return false;
                    }
                }

                DisableControls();

                mt = new MuxTool(settings, demuxedStreamList);
                mt.OnInfoMsg += new ExternalTool.InfoEventHandler(MuxMsg);
                mt.OnLogMsg += new ExternalTool.LogEventHandler(MuxMsg);
                mt.Start();
                mt.WaitForExit();
                return mt.Successfull;
            }
            catch (Exception ex)
            {
                logWindow.MessageMux("Exception: " + ex.Message);
                return false;
            }
            finally
            {
                EnableControls();
                UpdateStatus(Res("StatusBar") + " " + Res("StatusBarReady"));
            }
        }

        private bool checkMkvmerge()
        {
            try
            {
                if (!File.Exists(settings.mkvmergePath))
                {
                    logWindow.MessageMain("mkvmerge path not set");
                    if (!silent) ErrorMsg("mkvmerge path not set");
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
                logWindow.MessageMain(Res("ErrorException") + " " + ex.Message);
            }
            finally
            {
            }
        }
    }
}