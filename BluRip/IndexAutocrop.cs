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
        private IndexTool it = null;

        private void IndexMsg(object sender, ExternalTool.MsgEventArgs e)
        {
            MessageCrop(e.Message.Replace("\b", "").Trim());
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
                        MessageCrop(s.Trim());
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
                    MessageCrop("Error getting MediaInfo: " + ex.Message);
                    return false;
                }

                if (avo != null && avo.disableFps)
                {
                    MessageCrop("Using manual fps - override MediaInfo value");
                    fps = avo.fps;
                    length = avo.length;
                    frames = avo.frames;
                }

                if (fps == "")
                {
                    MessageCrop("Error getting framerate");
                    foreach (StreamInfo si in demuxedStreamList.streams)
                    {
                        if (si.streamType == StreamType.Video)
                        {
                            if (si.desc.Contains("24 /1.001"))
                            {
                                MessageCrop("Assume fps is 23.976");
                                fps = "23.976";
                                break;
                            }
                            else if (si.desc.Contains("1080p24 (16:9)"))
                            {
                                MessageCrop("Assume fps is 24");
                                fps = "24";
                                break;
                            }
                            // add other framerates here
                        }
                    }
                    if (fps == "")
                    {
                        MessageCrop("Could not get framerate - please report log to developer");
                        return false;
                    }
                }

                if (frames == "" || length == "")
                {
                    MessageCrop("WARNING: frames or length not set - bitrate calculation not possible");
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
                                MessageCrop("Error while indexing!");
                                return false;
                            }
                        }
                    }
                    
                    if (settings.cropInput == 2 || settings.encodeInput == 2)
                    {
                        string output = Path.ChangeExtension(filename, "dgi");

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
                                MessageCrop("Error while indexing!");
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
                            string dlldir = Path.GetDirectoryName(settings.ffmsindexPath);
                            if (File.Exists(dlldir + "\\ffms2.dll"))
                            {
                                data = "LoadPlugin(\"" + dlldir + "\\ffms2.dll" + "\")\r\n";
                            }
                            data += "FFVideoSource(\"" + filename + "\")";
                            File.WriteAllText(settings.workingDir + "\\" + settings.filePrefix + "_cropTemp.avs", data);
                        }
                        else if (settings.cropInput == 2)
                        {
                            string output = Path.ChangeExtension(filename, "dgi");
                            string data = "";
                            string dlldir = Path.GetDirectoryName(settings.dgindexnvPath);
                            if (File.Exists(dlldir + "\\DGMultiDecodeNV.dll"))
                            {
                                data = "LoadPlugin(\"" + dlldir + "\\DGMultiDecodeNV.dll" + "\")\r\n";
                            }
                            data += "DGMultiSource(\"" + output + "\")";
                            File.WriteAllText(settings.workingDir + "\\" + settings.filePrefix + "_cropTemp.avs", data);
                        }
                        MessageCrop("Starting AutoCrop...");

                        AutoCrop ac = new AutoCrop(settings.workingDir + "\\" + settings.filePrefix + "_cropTemp.avs", settings, ref cropInfo);
                        if (cropInfo.error)
                        {
                            MessageCrop("Exception: " + cropInfo.errorStr);
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
                        cropInfo.resizeX = avo.sizeX;
                        cropInfo.resizeY = avo.sizeY;
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

                    MessageCrop("");
                    MessageCrop("Crop top: " + cropInfo.cropTop.ToString());
                    MessageCrop("Crop bottom: " + cropInfo.cropBottom.ToString());
                    if (cropInfo.border)
                    {
                        MessageCrop("Border top: " + cropInfo.borderTop.ToString());
                        MessageCrop("Border bottom: " + cropInfo.borderBottom.ToString());
                    }
                    if (cropInfo.resize)
                    {
                        MessageCrop("Resize to: " + cropInfo.resizeX.ToString() + " x " + cropInfo.resizeY.ToString());
                    }

                    string encode = "";
                    if (settings.encodeInput == 0)
                    {
                        encode = "DirectShowSource(\"" + filename + "\")\r\n";
                    }
                    else if (settings.encodeInput == 1)
                    {
                        string dlldir = Path.GetDirectoryName(settings.ffmsindexPath);
                        if (File.Exists(dlldir + "\\ffms2.dll"))
                        {
                            encode += "LoadPlugin(\"" + dlldir + "\\ffms2.dll" + "\")\r\n";
                        }
                        encode += "FFVideoSource(\"" + filename + "\")\r\n";
                    }
                    else if (settings.encodeInput == 2)
                    {
                        string output = Path.ChangeExtension(filename, "dgi");
                        string dlldir = Path.GetDirectoryName(settings.dgindexnvPath);
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
                            MessageCrop("Did not add AddBorders command");
                        }
                        if (cropInfo.resize)
                        {
                            encode += "LanczosResize(" + cropInfo.resizeX.ToString() + "," + cropInfo.resizeY.ToString() + ")\r\n";
                        }
                        else
                        {
                            MessageCrop("Did not add resize command");
                        }
                    }
                    int index = comboBoxAvisynthProfile.SelectedIndex;
                    if (index > -1 && index < settings.avisynthSettings.Count)
                    {
                        string[] tmp = settings.avisynthSettings[index].commands.Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
                        foreach (string s in tmp)
                        {
                            encode += s.Trim() + "\r\n";
                        }
                    }

                    File.WriteAllText(settings.workingDir + "\\" + settings.filePrefix + "_encode.avs", encode);

                    MessageCrop("");
                    MessageCrop("Encode avs:");
                    MessageCrop("");
                    string[] tmpstr2 = encode.Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
                    foreach (string s in tmpstr2)
                    {
                        MessageCrop(s);
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
                UpdateDemuxedStreams();
                return true;
            }
            catch (Exception ex)
            {
                MessageCrop("Exception: " + ex.Message);
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
                this.Text = title + " [Indexing & AutoCrop...]";
                notifyIconMain.Text = this.Text;

                if (demuxedStreamList.streams.Count == 0)
                {
                    MessageMain("No demuxed streams available");
                    if (!silent) MessageBox.Show("No demuxed streams available", "Error");
                    return false;
                }

                return IndexCrop();
            }
            catch (Exception ex)
            {
                MessageCrop("Exception: " + ex.Message);
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