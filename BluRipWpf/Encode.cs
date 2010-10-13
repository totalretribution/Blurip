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
using Windows7.DesktopIntegration.WindowsForms;
using Windows7.DesktopIntegration;

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
                if (settings.encodeInput == 1)
                {
                    if (!File.Exists(settings.ffmsindexPath))
                    {
                        logWindow.MessageMain(Global.Res("ErrorFfmsindexPath"));
                        if (!silent) Global.ErrorMsg(Global.Res("ErrorFfmsindexPath"));
                        return false;
                    }
                }
                if (settings.encodeInput == 2)
                {
                    if (!File.Exists(settings.dgindexnvPath))
                    {
                        logWindow.MessageMain(Global.Res("ErrorDgindexPath"));
                        if (!silent) Global.ErrorMsg(Global.Res("ErrorDgindexPath"));
                        return false;
                    }
                }

                bool vobsub = false;
                bool vobsubForced = false;
                SubtitleFileInfo sfi = null;

                foreach (StreamInfo si in demuxedStreamList.streams)
                {
                    if (si.streamType == StreamType.Subtitle)
                    {
                        if (si.advancedOptions != null && si.advancedOptions.GetType() == typeof(AdvancedSubtitleOptions))
                        {
                            if (((AdvancedSubtitleOptions)si.advancedOptions).vobSub)
                            {
                                // take first vobsob track
                                if (!vobsub)
                                {
                                    vobsub = true;
                                    vobsubForced = ((AdvancedSubtitleOptions)si.advancedOptions).vobSubOnlyForced;
                                    if (si.extraFileInfo != null && si.extraFileInfo.GetType() == typeof(SubtitleFileInfo))
                                    {
                                        sfi = (SubtitleFileInfo)si.extraFileInfo;
                                    }
                                }
                            }
                        }
                    }
                }

                if (vobsub && sfi != null && (File.Exists(sfi.forcedIdx) || File.Exists(sfi.normalIdx)))
                {
                    if (!File.Exists(settings.vobsubPath))
                    {
                        logWindow.MessageMain(Global.Res("ErrorVobsubPath"));
                        if (!silent) Global.ErrorMsg(Global.Res("ErrorVobsubPath"));
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
                        double percent = 0.0;
                        double add = 0.0;
                        if (secondPass) add = 100.0;
                        try
                        {
                            percent = Convert.ToDouble(substr.Replace(".",","));
                        }
                        catch (Exception)
                        {
                        }
                        if (percent > 0.0)
                        {
                            UpdateStatusBar(percent + add);
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
                string[] tmp = text.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                if(tmp.Length > 2 && tmp[1].ToUpper() == "FRAMES:")
                {
                    int frame = 0;
                    int maxFrames = 0;
                    maxFrames = GetMaxFrames();
                    try
                    {
                        frame = Convert.ToInt32(tmp[0]);
                    }
                    catch (Exception)
                    {
                    }
                    if (frame > 0 && maxFrames > 0)
                    {
                        double add = 0;
                        if (secondPass) add = maxFrames;
                        UpdateStatusBar(frame + add);
                    }
                }
            }
        }

        private int GetMaxFrames()
        {
            try
            {
                foreach (StreamInfo si in demuxedStreamList.streams)
                {
                    if (si.streamType == StreamType.Video)
                    {
                        if (si.extraFileInfo != null && si.extraFileInfo.GetType() == typeof(VideoFileInfo))
                        {
                            VideoFileInfo vfi = (VideoFileInfo)si.extraFileInfo;
                            return Convert.ToInt32(vfi.frames);
                        }
                    }
                }
                return 0;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        private bool DoEncode()
        {
            try
            {
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
                StreamInfo vsi = null;
                bool vobsub = false;
                bool vobsubForced = false;
                SubtitleFileInfo sfi = null;

                foreach (StreamInfo si in demuxedStreamList.streams)
                {
                    if (si.streamType == StreamType.Video)
                    {
                        vsi = si;
                        if (si.extraFileInfo.GetType() == typeof(VideoFileInfo))
                        {
                            vfi = (VideoFileInfo)si.extraFileInfo;
                            break;
                        }
                    }
                    else if (si.streamType == StreamType.Subtitle)
                    {
                        if (si.advancedOptions != null && si.advancedOptions.GetType() == typeof(AdvancedSubtitleOptions))
                        {
                            if (((AdvancedSubtitleOptions)si.advancedOptions).vobSub)
                            {
                                // take first vobsob track
                                if (!vobsub)
                                {
                                    vobsub = true;
                                    vobsubForced = ((AdvancedSubtitleOptions)si.advancedOptions).vobSubOnlyForced;
                                    if (si.extraFileInfo != null && si.extraFileInfo.GetType() == typeof(SubtitleFileInfo))
                                    {
                                        sfi = (SubtitleFileInfo)si.extraFileInfo;
                                    }
                                }
                            }
                        }
                    }
                }

                if (vfi == null || vfi.cropInfo == null)
                {
                    logWindow.MessageEncode(Global.Res("ErrorCropInfoNotSet"));
                    if (!silent) Global.ErrorMsg(Global.Res("ErrorCropInfoNotSet"));
                    return false;
                }
                else
                {   
                    vfi.encodeAvs = settings.workingDir + "\\" + settings.filePrefix + "_encode.avs";
                    
                    string filename = vsi.filename;

                    CropInfo cropInfo = vfi.cropInfo;

                    logWindow.MessageEncode("");
                    logWindow.MessageEncode(Global.ResFormat("InfoCropTop", cropInfo.cropTop));
                    logWindow.MessageEncode(Global.ResFormat("InfoCropBottom", cropInfo.cropBottom));
                    if (cropInfo.border)
                    {
                        logWindow.MessageEncode(Global.ResFormat("InfoBorderTop", cropInfo.borderTop));
                        logWindow.MessageEncode(Global.ResFormat("InfoBorderBottom", cropInfo.borderBottom));
                    }
                    if (cropInfo.resize)
                    {
                        logWindow.MessageEncode(Global.ResFormat("InfoResize", cropInfo.resizeX, cropInfo.resizeY));
                    }

                    string encode = "";
                    if (vobsub)
                    {
                        if (File.Exists(settings.vobsubPath))
                        {
                            encode += "LoadPlugin(\"" + settings.vobsubPath + "\")\r\n";
                        }
                    }
                    if (settings.encodeInput == 0)
                    {
                        encode = "DirectShowSource(\"" + filename + "\")\r\n";
                    }
                    else if (settings.encodeInput == 1)
                    {
                        string dlldir = System.IO.Path.GetDirectoryName(settings.ffmsindexPath);
                        if (File.Exists(dlldir + "\\ffms2.dll"))
                        {
                            encode += "LoadPlugin(\"" + dlldir + "\\ffms2.dll" + "\")\r\n";
                        }
                        encode += "FFVideoSource(\"" + filename + "\")\r\n";
                    }
                    else if (settings.encodeInput == 2)
                    {
                        string output = System.IO.Path.ChangeExtension(filename, "dgi");
                        string dlldir = System.IO.Path.GetDirectoryName(settings.dgindexnvPath);
                        if (File.Exists(dlldir + "\\DGDecodeNV.dll"))
                        {
                            encode += "LoadPlugin(\"" + dlldir + "\\DGDecodeNV.dll" + "\")\r\n";
                        }
                        encode += "DGSource(\"" + output + "\")\r\n";
                    }
                    if (cropInfo.cropTop != 0 || cropInfo.cropBottom != 0)
                    {
                        encode += "Crop(0," + cropInfo.cropTop.ToString() + ",-0,-" + cropInfo.cropBottom.ToString() + ")\r\n";                                        
                    }
                    if (cropInfo.border)
                    {
                        encode += "AddBorders(0," + cropInfo.borderTop + ",0," + cropInfo.borderBottom + ")\r\n";
                    }
                    else
                    {
                        logWindow.MessageEncode(Global.Res("InfoNoBorder"));
                    }
                    if (vobsub && sfi != null)
                    {
                        if (!vobsubForced)
                        {
                            if (File.Exists(sfi.normalIdx) && File.Exists(sfi.normalSub))
                            {
                                encode += "VobSub(\"" + sfi.normalIdx + "\"\r\n";
                            }
                        }
                        else
                        {
                            if (File.Exists(sfi.forcedIdx) && File.Exists(sfi.forcedSub))
                            {
                                encode += "VobSub(\"" + sfi.forcedIdx + "\"\r\n";
                            }
                            else if (File.Exists(sfi.normalIdx) && File.Exists(sfi.normalSub))
                            {
                                encode += "VobSub(\"" + sfi.normalIdx + "\"\r\n";
                            }
                        }
                    }
                    if (cropInfo.resize)
                    {
                        if (cropInfo.resizeMethod > -1 && cropInfo.resizeMethod < Global.resizeMethods.Count)
                        {
                            encode += Global.resizeMethods[cropInfo.resizeMethod] + "(" + cropInfo.resizeX.ToString() + "," + cropInfo.resizeY.ToString() + ")\r\n";
                        }
                        else
                        {
                            encode += "LanczosResize(" + cropInfo.resizeX.ToString() + "," + cropInfo.resizeY.ToString() + ")\r\n";
                        }
                    }
                    else
                    {
                        logWindow.MessageEncode(Global.Res("InfoNoResize"));
                    }

                    int index = settings.lastAvisynthProfile;
                    if (index > -1 && index < settings.avisynthSettings.Count)
                    {
                        string[] tmp = settings.avisynthSettings[index].commands.Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
                        foreach (string s in tmp)
                        {
                            encode += s.Trim() + "\r\n";
                        }
                    }

                    File.WriteAllText(settings.workingDir + "\\" + settings.filePrefix + "_encode.avs", encode);

                    logWindow.MessageEncode("");
                    logWindow.MessageEncode(Global.Res("InfoAvsContent"));
                    logWindow.MessageEncode("");
                    string[] tmpstr2 = encode.Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
                    foreach (string s in tmpstr2)
                    {
                        logWindow.MessageEncode(s);
                    }
                }


                if (vfi == null || vfi.encodeAvs == "")
                {
                    logWindow.MessageEncode(Global.Res("ErrorEncodeAvsNotSet"));
                    if (!silent) Global.ErrorMsg(Global.Res("ErrorEncodeAvsNotSet"));
                    //return false;
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

                if (settings.use64bit && GetMaxFrames() > 0)
                {
                    if (!settings.encodingSettings[profile].pass2)
                    {
                        maxProgressValue = GetMaxFrames();
                    }
                    else
                    {
                        maxProgressValue = 2 * GetMaxFrames();
                    }
                    progressBarMain.IsIndeterminate = false;
                    progressBarMain.Maximum = 100;
                    progressBarMain.Minimum = 0;
                }
                else if (!settings.use64bit)
                {
                    if (!settings.encodingSettings[profile].pass2)
                    {
                        maxProgressValue = 100;
                    }
                    else
                    {
                        maxProgressValue = 200;
                    }
                    progressBarMain.IsIndeterminate = false;
                    progressBarMain.Maximum = 100;
                    progressBarMain.Minimum = 0;
                }

                et = new EncodeTool(settings, demuxedStreamList, profile, false, vfi);
                et.OnInfoMsg += new ExternalTool.InfoEventHandler(EncodeMsg);
                et.OnLogMsg += new ExternalTool.LogEventHandler(EncodeMsg);
                et.Start();
                et.WaitForExit();

                if (et == null || !et.Successfull)
                {
                    vfi.encodedFile = "";
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
                        vfi.encodedFile = "";
                        logWindow.MessageEncode(Global.Res("ErrorEncode2passFailed"));
                        return false;
                    }
                }
                
                return true;
            }
            catch (Exception ex)
            {
                logWindow.MessageEncode(Global.Res("ErrorException") + " " + ex.Message);
                return false;
            }
            finally
            {
                progressBarMain.IsIndeterminate = true;
                if (isWindows7)
                {
                    WPFExtensions.SetTaskbarProgressState(this, Windows7Taskbar.ThumbnailProgressState.NoProgress);
                }

                TitleInfo.SaveStreamInfoFile(demuxedStreamList, settings.workingDir + "\\" + settings.filePrefix + "_streamInfo.xml");
                EnableControls();

                UpdateStatus(Global.Res("StatusBar") + " " + Global.Res("StatusBarReady"));
            }
        }
    }
}