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
using MediaInfoLib;

namespace BluRip
{
    public partial class MainWindow : Window
    {
        private IndexTool it = null;

        private void IndexMsg(object sender, ExternalTool.MsgEventArgs e)
        {
            logWindow.MessageCrop(e.Message.Replace("\b", "").Trim());
        }

        private bool IndexCrop()
        {
            try
            {
                string filename = "";
                AdvancedVideoOptions avo = null;
                foreach (StreamInfo si in demuxedStreamList.streams)
                {
                    if (si.streamType == StreamType.Video)
                    {
                        filename = si.filename;
                        if (si.advancedOptions != null && si.advancedOptions.GetType() == typeof(AdvancedVideoOptions)) avo = (AdvancedVideoOptions)si.advancedOptions;
                        break;
                    }
                }

                string fps = "";
                string resX = "";
                string resY = "";
                string length = "";
                string frames = "";

                try
                {
                    MediaInfoLib.MediaInfo mi2 = new MediaInfoLib.MediaInfo();
                    mi2.Open(filename);
                    mi2.Option("Complete", "1");
                    string[] tmpstr = mi2.Inform().Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
                    foreach (string s in tmpstr)
                    {
                        logWindow.MessageCrop(s.Trim());
                    }
                    if (mi2.Count_Get(StreamKind.Video) > 0)
                    {
                        fps = mi2.Get(StreamKind.Video, 0, "FrameRate");
                        resX = mi2.Get(StreamKind.Video, 0, "Width");
                        resY = mi2.Get(StreamKind.Video, 0, "Height");
                        length = mi2.Get(StreamKind.Video, 0, "Duration");
                        frames = mi2.Get(StreamKind.Video, 0, "FrameCount");
                    }
                    mi2.Close();
                }
                catch (Exception ex)
                {
                    logWindow.MessageCrop(Global.Res("ErrorMediaInfo") + " " + ex.Message);
                    return false;
                }

                if (avo != null && avo.disableFps)
                {
                    logWindow.MessageCrop(Global.Res("InfoManualFps"));
                    fps = avo.fps;
                    length = avo.length;
                    frames = avo.frames;
                }

                if (fps == "")
                {
                    logWindow.MessageCrop(Global.Res("ErrorFramerate"));
                    foreach (StreamInfo si in demuxedStreamList.streams)
                    {
                        if (si.streamType == StreamType.Video)
                        {
                            if (si.desc.Contains("24 /1.001"))
                            {
                                logWindow.MessageCrop(Global.ResFormat("InfoFps", " 23.976"));
                                fps = "23.976";
                                break;
                            }
                            else if (si.desc.Contains("1080p24 (16:9)"))
                            {
                                logWindow.MessageCrop(Global.ResFormat("InfoFps", " 24"));
                                fps = "24";
                                break;
                            }
                            // add other framerates here
                        }
                    }
                    if (fps == "")
                    {
                        logWindow.MessageCrop(Global.Res("ErrorNoFramerate"));
                        return false;
                    }
                }

                if (frames == "" || length == "")
                {
                    logWindow.MessageCrop(Global.Res("InfoFrames"));
                }

                CropInfo cropInfo = new CropInfo();
                if (!settings.untouchedVideo)
                {
                    if (settings.cropInput == 1 || settings.encodeInput == 1)
                    {
                        bool skip = false;
                        if (File.Exists(filename + ".ffindex") && !settings.deleteIndex)
                        {
                            skip = true;
                        }
                        if (!skip)
                        {
                            it = new IndexTool(settings, filename, IndexType.ffmsindex);
                            it.OnInfoMsg += new ExternalTool.InfoEventHandler(IndexMsg);
                            it.OnLogMsg += new ExternalTool.LogEventHandler(IndexMsg);
                            it.Start();
                            it.WaitForExit();
                            if (!it.Successfull)
                            {
                                logWindow.MessageCrop(Global.Res("ErrorIndex"));
                                return false;
                            }
                        }
                    }

                    if (settings.cropInput == 2 || settings.encodeInput == 2)
                    {
                        string output = System.IO.Path.ChangeExtension(filename, "dgi");

                        bool skip = false;
                        if (File.Exists(output) && !settings.deleteIndex)
                        {
                            skip = true;
                        }
                        if (!skip)
                        {
                            it = new IndexTool(settings, filename, IndexType.dgindex);
                            it.OnInfoMsg += new ExternalTool.InfoEventHandler(IndexMsg);
                            it.OnLogMsg += new ExternalTool.LogEventHandler(IndexMsg);
                            it.Start();
                            it.WaitForExit();
                            if (!it.Successfull)
                            {
                                logWindow.MessageCrop(Global.Res("ErrorIndex"));
                                return false;
                            }
                        }
                    }

                    if (avo == null || !avo.disableAutocrop)
                    {
                        if (settings.cropInput == 0)
                        {
                            File.WriteAllText(settings.workingDir + "\\" + settings.filePrefix + "_cropTemp.avs",
                                "DirectShowSource(\"" + filename + "\")");
                        }
                        else if (settings.cropInput == 1)
                        {
                            string data = "";
                            string dlldir = System.IO.Path.GetDirectoryName(settings.ffmsindexPath);
                            if (File.Exists(dlldir + "\\ffms2.dll"))
                            {
                                data = "LoadPlugin(\"" + dlldir + "\\ffms2.dll" + "\")\r\n";
                            }
                            data += "FFVideoSource(\"" + filename + "\")";
                            File.WriteAllText(settings.workingDir + "\\" + settings.filePrefix + "_cropTemp.avs", data);
                        }
                        else if (settings.cropInput == 2)
                        {
                            string output = System.IO.Path.ChangeExtension(filename, "dgi");
                            string data = "";
                            string dlldir = System.IO.Path.GetDirectoryName(settings.dgindexnvPath);
                            if (File.Exists(dlldir + "\\DGMultiDecodeNV.dll"))
                            {
                                data = "LoadPlugin(\"" + dlldir + "\\DGMultiDecodeNV.dll" + "\")\r\n";
                            }
                            data += "DGMultiSource(\"" + output + "\")";
                            File.WriteAllText(settings.workingDir + "\\" + settings.filePrefix + "_cropTemp.avs", data);
                        }
                        logWindow.MessageCrop(Global.Res("InfoStartCrop"));

                        AutoCrop ac = new AutoCrop(settings.workingDir + "\\" + settings.filePrefix + "_cropTemp.avs", settings, ref cropInfo);
                        
                        if (cropInfo.error)
                        {
                            logWindow.MessageCrop(Global.Res("ErrorException") + " " + cropInfo.errorStr);
                            return false;
                        }

                        if (settings.minimizeAutocrop)
                        {
                            ac.WindowState = FormWindowState.Minimized;
                        }

                        ac.NrFrames = settings.nrFrames;
                        ac.BlackValue = settings.blackValue;
                        ac.ShowDialog();
                    }
                    else
                    {
                        cropInfo.border = avo.manualBorders;
                        cropInfo.borderBottom = avo.borderBottom;
                        cropInfo.borderTop = avo.borderTop;
                        cropInfo.resize = avo.manualResize;
                        cropInfo.resizeX = avo.resizeX;
                        cropInfo.resizeY = avo.resizeY;
                        cropInfo.error = false;
                        if (avo.manualCrop)
                        {
                            cropInfo.cropBottom = avo.cropBottom;
                            cropInfo.cropTop = avo.cropTop;
                        }
                        else
                        {
                            cropInfo.cropBottom = 0;
                            cropInfo.cropTop = 0;
                        }
                    }

                    logWindow.MessageCrop("");
                    logWindow.MessageCrop(Global.ResFormat("InfoCropTop", cropInfo.cropTop));
                    logWindow.MessageCrop(Global.ResFormat("InfoCropBottom", cropInfo.cropBottom));
                    if (cropInfo.border)
                    {
                        logWindow.MessageCrop(Global.ResFormat("InfoBorderTop", cropInfo.borderTop));
                        logWindow.MessageCrop(Global.ResFormat("InfoBorderBottom", cropInfo.borderBottom));
                    }
                    if (cropInfo.resize)
                    {
                        logWindow.MessageCrop(Global.ResFormat("InfoResize", cropInfo.resizeX, cropInfo.resizeY));
                    }

                    string encode = "";
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
                        if (File.Exists(dlldir + "\\DGMultiDecodeNV.dll"))
                        {
                            encode += "LoadPlugin(\"" + dlldir + "\\DGMultiDecodeNV.dll" + "\")\r\n";
                        }
                        encode += "DGMultiSource(\"" + output + "\")\r\n";
                    }
                    if (cropInfo.cropTop != 0 || cropInfo.cropBottom != 0)
                    {
                        encode += "Crop(0," + cropInfo.cropTop.ToString() + ",-0,-" + cropInfo.cropBottom.ToString() + ")\r\n";
                        if (cropInfo.border)
                        {
                            encode += "AddBorders(0," + cropInfo.borderTop + ",0," + cropInfo.borderBottom + ")\r\n";
                        }
                        else
                        {
                            logWindow.MessageCrop(Global.Res("InfoNoBorder"));
                        }
                        if (cropInfo.resize)
                        {
                            encode += "LanczosResize(" + cropInfo.resizeX.ToString() + "," + cropInfo.resizeY.ToString() + ")\r\n";
                        }
                        else
                        {
                            logWindow.MessageCrop(Global.Res("InfoNoResize"));
                        }
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

                    logWindow.MessageCrop("");
                    logWindow.MessageCrop(Global.Res("InfoAvsContent"));
                    logWindow.MessageCrop("");
                    string[] tmpstr2 = encode.Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
                    foreach (string s in tmpstr2)
                    {
                        logWindow.MessageCrop(s);
                    }
                }
                foreach (StreamInfo si in demuxedStreamList.streams)
                {
                    if (si.streamType == StreamType.Video)
                    {
                        if (si.extraFileInfo.GetType() != typeof(VideoFileInfo))
                        {
                            si.extraFileInfo = new VideoFileInfo();
                        }
                        if (!settings.untouchedVideo)
                        {
                            ((VideoFileInfo)si.extraFileInfo).encodeAvs = settings.workingDir + "\\" + settings.filePrefix + "_encode.avs";
                        }
                        ((VideoFileInfo)si.extraFileInfo).fps = fps;
                        ((VideoFileInfo)si.extraFileInfo).length = length;
                        ((VideoFileInfo)si.extraFileInfo).frames = frames;
                        if (cropInfo.resize)
                        {
                            ((VideoFileInfo)si.extraFileInfo).resX = cropInfo.resizeX.ToString();
                            ((VideoFileInfo)si.extraFileInfo).resY = cropInfo.resizeY.ToString();
                        }
                        else
                        {
                            int tmp = cropInfo.cropBottom + cropInfo.cropTop;
                            int y = Convert.ToInt32(resY) - tmp;
                            ((VideoFileInfo)si.extraFileInfo).resX = resX;
                            ((VideoFileInfo)si.extraFileInfo).resY = y.ToString();
                        }
                    }
                }
                TitleInfo.SaveStreamInfoFile(demuxedStreamList, settings.workingDir + "\\" + settings.filePrefix + "_streamInfo.xml");
                demuxedStreamsWindow.UpdateDemuxedStreams();
                return true;
            }
            catch (Exception ex)
            {
                logWindow.MessageCrop(Global.Res("ErrorException") + " " + ex.Message);
                return false;
            }
            finally
            {

            }
        }

        private bool DoIndex()
        {
            try
            {
                if (!settings.doIndex) return true;

                if (demuxedStreamList.streams.Count == 0)
                {
                    logWindow.MessageMain(Global.Res("ErrorNoDemuxedStreams"));
                    if (!silent) Global.ErrorMsg(Global.Res("ErrorNoDemuxedStreams"));
                    return false;
                }

                UpdateStatus(Global.Res("StatusBar") + " " + Global.Res("StatusBarCrop"));
                DisableControls();

                return IndexCrop();
            }
            catch (Exception ex)
            {
                logWindow.MessageCrop(Global.Res("ErrorException") + " " + ex.Message);
                return false;
            }
            finally
            {
                EnableControls();

                UpdateStatus(Global.Res("StatusBar") + " " + Global.Res("StatusBarReady"));
            }
        }

        private bool checkIndex()
        {
            try
            {
                if (settings.untouchedVideo)
                {
                    return true;
                }
                else if (settings.cropInput == 1 || settings.encodeInput == 1)
                {
                    if (!File.Exists(settings.ffmsindexPath))
                    {
                        logWindow.MessageMain(Global.Res("ErrorFfmsindexPath"));
                        if (!silent) Global.ErrorMsg(Global.Res("ErrorFfmsindexPath"));
                        return false;
                    }
                }
                else if (settings.cropInput == 2 || settings.encodeInput == 2)
                {
                    if (!File.Exists(settings.dgindexnvPath))
                    {
                        logWindow.MessageMain(Global.Res("ErrorDgindexPath"));
                        if (!silent) Global.ErrorMsg(Global.Res("ErrorDgindexPath"));
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

        private void buttonOnlyCrop_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!checkIndex()) return;

                DoIndex();
            }
            catch (Exception ex)
            {
                logWindow.MessageCrop(Global.Res("ErrorException") + " " + ex.Message);
            }
            finally
            {
            }
        }
    }
}