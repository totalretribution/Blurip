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
        private EncodeTool et = null;
        private string lastMsg = "";
        public bool secondPass = false;

        private bool checkX264()
        {
            try
            {
                if (settings.use64bit)
                {
                    if (!File.Exists(settings.x264x64Path))
                    {
                        logWindow.MessageMain(Global.Res("ErrorX264x64Path"));
                        if (!silent) Global.ErrorMsg(Global.Res("ErrorX264x64Path"));
                        return false;
                    }
                    if (!File.Exists(settings.avs2yuvPath))
                    {
                        logWindow.MessageMain(Global.Res("ErrorAvs2yuvPath"));
                        if (!silent) Global.ErrorMsg(Global.Res("ErrorAvs2yuvPath"));
                        return false;
                    }
                }
                else
                {
                    if (!File.Exists(settings.x264Path))
                    {
                        logWindow.MessageMain(Global.Res("ErrorX264Path"));
                        if (!silent) Global.ErrorMsg(Global.Res("ErrorX264Path"));
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
        
        private void buttonOnlyEncode_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!checkX264()) return;

                DoEncode();
            }
            catch (Exception ex)
            {
                logWindow.MessageEncode(Global.Res("ErrorException") + " " + ex.Message);
            }
            finally
            {
                
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
                    int index = settings.lastProfile;
                    string substr = text.Substring(start + 1, end - start - 2);
                    if (substr != lastMsg)
                    {
                        lastMsg = substr;
                        logWindow.MessageEncode(text);
                        if (index > -1 && settings.encodingSettings[index].pass2)
                        {
                            if (!secondPass)
                            {
                                UpdateStatus(Global.Res("StatusBar") + " " + Global.ResFormat("StatusBarEncodeFirstPass", text));
                            }
                            else
                            {
                                UpdateStatus(Global.Res("StatusBar") + " " + Global.ResFormat("StatusBarEncodeSecondPass", text));
                            }
                        }
                        else
                        {
                            UpdateStatus(Global.Res("StatusBar") + " " + Global.ResFormat("StatusBarEncode", text));
                        }
                    }
                }
                else
                {
                    logWindow.MessageEncode(text);
                }
            }
            else
            {
                logWindow.MessageEncode(text);
            }
        }

        private bool DoEncode()
        {
            try
            {
                if (!settings.doEncode) return true;

                if (!Directory.Exists(settings.workingDir))
                {
                    logWindow.MessageDemux(Global.Res("ErrorWorkingDirectory"));
                    if (!silent) Global.ErrorMsg(Global.Res("ErrorWorkingDirectory"));
                    return false;
                }
                if (settings.encodedMovieDir != "")
                {
                    if (!Directory.Exists(settings.encodedMovieDir))
                    {
                        logWindow.MessageDemux(Global.Res("ErrorEncodedMovieDirectory"));
                        if (!silent) Global.ErrorMsg(Global.Res("ErrorEncodedMovieDirectory"));
                        return false;
                    }
                }
                if (demuxedStreamList.streams.Count == 0)
                {
                    logWindow.MessageSubtitle(Global.Res("ErrorNoDemuxedStreams"));
                    if (!silent) Global.ErrorMsg(Global.Res("ErrorNoDemuxedStreams"));
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
                    logWindow.MessageEncode(Global.Res("ErrorEncodeAvsNotSet"));
                    if (!silent) Global.ErrorMsg(Global.Res("ErrorEncodeAvsNotSet"));
                    return false;
                }

                int profile = settings.lastProfile;
                if (profile < 0)
                {
                    logWindow.MessageEncode(Global.Res("ErrorEncodingProfileNotSet"));
                    if (!silent) Global.ErrorMsg(Global.Res("ErrorEncodingProfileNotSet"));
                    return false;
                }

                UpdateStatus(Global.Res("StatusBar") + " " + Global.ResFormat("StatusBarEncode", ""));
                DisableControls();

                lastMsg = "";
                secondPass = false;

                et = new EncodeTool(settings, demuxedStreamList, profile, false, vfi);
                et.OnInfoMsg += new ExternalTool.InfoEventHandler(EncodeMsg);
                et.OnLogMsg += new ExternalTool.LogEventHandler(EncodeMsg);
                et.Start();
                et.WaitForExit();

                if (et == null || !et.Successfull)
                {
                    logWindow.MessageEncode(Global.Res("ErrorEncodeFailed"));
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
                    if (et == null || !et.Successfull)
                    {
                        logWindow.MessageEncode(Global.Res("ErrorEncode2passFailed"));
                        return false;
                    }
                }

                TitleInfo.SaveStreamInfoFile(demuxedStreamList, settings.workingDir + "\\" + settings.filePrefix + "_streamInfo.xml");
                return true;
            }
            catch (Exception ex)
            {
                logWindow.MessageEncode(Global.Res("ErrorException") + " " + ex.Message);
                return false;
            }
            finally
            {
                EnableControls();

                UpdateStatus(Global.Res("StatusBar") + " " + Global.Res("StatusBarReady"));
            }
        }
    }
}