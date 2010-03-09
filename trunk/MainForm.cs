//BluRip - one click BluRay/m2ts to mkv converter
//Copyright (C) 2009-2010  _hawk_

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

// Contact: hawk.ac@gmx.net

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
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
        private UserSettings settings = new UserSettings();
        private string settingsPath = "";
        private List<TitleInfo> titleList = new List<TitleInfo>();

        private List<string> videoTypes = new List<string>();
        private List<string> ac3AudioTypes = new List<string>();
        private List<string> dtsAudioTypes = new List<string>();

        private Thread titleInfoThread = null;
        private Thread demuxThread = null;
        private Thread indexThread = null;
        private Thread encodeThread = null;
        private Thread subtitleThread = null;
        private Thread muxThread = null;

        private Process pc = new Process();
        private Process pc2 = new Process();

        public string title = "BluRip v0.4.6 © _hawk_/PPX";

        public MainForm()
        {
            InitializeComponent();
            try
            {
                videoTypes.Add("h264/AVC");
                videoTypes.Add("VC-1");
                videoTypes.Add("MPEG2");

                ac3AudioTypes.Add("TrueHD/AC3");
                ac3AudioTypes.Add("AC3");
                ac3AudioTypes.Add("AC3 Surround");
                ac3AudioTypes.Add("AC3 EX");
                ac3AudioTypes.Add("E-AC3");

                dtsAudioTypes.Add("DTS");
                dtsAudioTypes.Add("DTS Master Audio");
                dtsAudioTypes.Add("DTS Express");
                dtsAudioTypes.Add("DTS Hi-Res");
                dtsAudioTypes.Add("DTS ES"); // have to check if needed
                dtsAudioTypes.Add("DTS-ES");

                comboBoxX264Priority.Items.Clear();
                foreach (string s in Enum.GetNames(typeof(ProcessPriorityClass)))
                {
                    comboBoxX264Priority.Items.Add(s);
                }
            }
            catch (Exception)
            {
            }
        }

        public delegate void MsgHandler(string msg);

        private void MessageMain(string msg)
        {
            try
            {
                if (richTextBoxLogMain.Disposing) return;
                if (richTextBoxLogMain.IsDisposed) return;
                if (this.richTextBoxLogMain.InvokeRequired)
                {
                    MsgHandler mh = new MsgHandler(MessageMain);
                    this.Invoke(mh, new object[] { msg });
                }
                else
                {
                    richTextBoxLogMain.AppendText("[" + DateTime.Now.ToString() + "] " + msg + "\n");
                    richTextBoxLogMain.ScrollToCaret();
                }
            }
            catch (Exception)
            {
            }
        }

        private void MessageDemux(string msg)
        {
            try
            {
                if (richTextBoxLogDemux.Disposing) return;
                if (richTextBoxLogDemux.IsDisposed) return;
                if (this.richTextBoxLogDemux.InvokeRequired)
                {
                    MsgHandler mh = new MsgHandler(MessageDemux);
                    this.Invoke(mh, new object[] { msg });
                }
                else
                {
                    richTextBoxLogDemux.AppendText("[" + DateTime.Now.ToString() + "] " + msg + "\n");
                    richTextBoxLogDemux.ScrollToCaret();
                    MessageMain(msg);
                }
            }
            catch (Exception)
            {
            }
        }

        private void MessageCrop(string msg)
        {
            try
            {
                if (richTextBoxLogCrop.Disposing) return;
                if (richTextBoxLogCrop.IsDisposed) return;
                if (this.richTextBoxLogCrop.InvokeRequired)
                {
                    MsgHandler mh = new MsgHandler(MessageCrop);
                    this.Invoke(mh, new object[] { msg });
                }
                else
                {
                    richTextBoxLogCrop.AppendText("[" + DateTime.Now.ToString() + "] " + msg + "\n");
                    richTextBoxLogCrop.ScrollToCaret();
                    MessageMain(msg);
                }
            }
            catch (Exception)
            {
            }
        }

        private void MessageSubtitle(string msg)
        {
            try
            {
                if (richTextBoxLogSubtitle.Disposing) return;
                if (richTextBoxLogSubtitle.IsDisposed) return;
                if (this.richTextBoxLogSubtitle.InvokeRequired)
                {
                    MsgHandler mh = new MsgHandler(MessageSubtitle);
                    this.Invoke(mh, new object[] { msg });
                }
                else
                {
                    richTextBoxLogSubtitle.AppendText("[" + DateTime.Now.ToString() + "] " + msg + "\n");
                    richTextBoxLogSubtitle.ScrollToCaret();
                    MessageMain(msg);
                }
            }
            catch (Exception)
            {
            }
        }

        private void MessageEncode(string msg)
        {
            try
            {
                if (richTextBoxLogEncode.Disposing) return;
                if (richTextBoxLogEncode.IsDisposed) return;
                if (this.richTextBoxLogEncode.InvokeRequired)
                {
                    MsgHandler mh = new MsgHandler(MessageEncode);
                    this.Invoke(mh, new object[] { msg });
                }
                else
                {
                    richTextBoxLogEncode.AppendText("[" + DateTime.Now.ToString() + "] " + msg + "\n");
                    richTextBoxLogEncode.ScrollToCaret();
                    MessageMain(msg);
                }
            }
            catch (Exception)
            {
            }
        }

        private void MessageMux(string msg)
        {
            try
            {
                if (richTextBoxLogMux.Disposing) return;
                if (richTextBoxLogMux.IsDisposed) return;
                if (this.richTextBoxLogMux.InvokeRequired)
                {
                    MsgHandler mh = new MsgHandler(MessageMux);
                    this.Invoke(mh, new object[] { msg });
                }
                else
                {
                    richTextBoxLogMux.AppendText("[" + DateTime.Now.ToString() + "] " + msg + "\n");
                    richTextBoxLogMux.ScrollToCaret();
                    MessageMain(msg);
                }
            }
            catch (Exception)
            {
            }
        }

        private void buttonPath_Click(object sender, EventArgs e)
        {
            try
            {
                FolderBrowserDialog fbd = new FolderBrowserDialog();
                if (fbd.ShowDialog() == DialogResult.OK)
                {
                    textBoxPath.Text = fbd.SelectedPath;
                    settings.lastBluRayPath = fbd.SelectedPath;
                }
            }
            catch (Exception)
            {
            }
        }

        static StringBuilder sb = new StringBuilder();
        void OutputDataReceivedMain(object sender, DataReceivedEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(e.Data))
                {
                    sb.Append(e.Data + "\r\n");
                    MessageMain(e.Data.Replace("\b","").Trim());
                }
            }
            catch (Exception)
            {
            }
        }

        void OutputDataReceivedDemux(object sender, DataReceivedEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(e.Data))
                {
                    sb.Append(e.Data + "\r\n");
                    MessageDemux(e.Data.Replace("\b", "").Trim());
                }
            }
            catch (Exception)
            {
            }
        }

        void OutputDataReceivedCrop(object sender, DataReceivedEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(e.Data))
                {
                    sb.Append(e.Data + "\r\n");
                    MessageCrop(e.Data.Replace("\b", "").Trim());
                }
            }
            catch (Exception)
            {
            }
        }

        void OutputDataReceivedSubtitle(object sender, DataReceivedEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(e.Data))
                {
                    sb.Append(e.Data + "\r\n");
                    MessageSubtitle(e.Data.Replace("\b", "").Trim());
                }
            }
            catch (Exception)
            {
            }
        }

        void OutputDataReceivedEncode(object sender, DataReceivedEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(e.Data))
                {
                    sb.Append(e.Data + "\r\n");
                    MessageEncode(e.Data.Replace("\b", "").Trim());
                }
            }
            catch (Exception)
            {
            }
        }

        void OutputDataReceivedMux(object sender, DataReceivedEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(e.Data))
                {
                    sb.Append(e.Data + "\r\n");
                    MessageMux(e.Data.Replace("\b", "").Trim());
                }
            }
            catch (Exception)
            {
            }
        }

        void ErrorDataReceived(object sender, DataReceivedEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(e.Data))
                {
                    sb.Append(e.Data + "\r\n");
                    MessageMain(e.Data.Replace("\b", "").Trim());
                }
            }
            catch (Exception)
            {
            }
        }

        private void GetSubStreamInfo(string path, string streamNumber, List<TitleInfo> result)
        {
            try
            {
                MessageDemux("");
                MessageDemux("Getting title info...");
                MessageDemux("");
                sb.Remove(0, sb.Length);
                pc2 = new Process();
                pc2.StartInfo.FileName = settings.eac3toPath;
                pc2.StartInfo.Arguments = "\"" + path + "\" " + streamNumber + ")";
                
                pc2.OutputDataReceived += new DataReceivedEventHandler(OutputDataReceivedDemux);
                pc2.ErrorDataReceived += new DataReceivedEventHandler(OutputDataReceivedDemux);
                pc2.StartInfo.UseShellExecute = false;
                pc2.StartInfo.CreateNoWindow = true;
                pc2.StartInfo.RedirectStandardError = true;
                pc2.StartInfo.RedirectStandardOutput = true;

                MessageDemux("Command: " + pc2.StartInfo.FileName + pc2.StartInfo.Arguments);

                if (!pc2.Start())
                {
                    MessageDemux("Error starting eac3to.exe");
                    return;
                }

                string res = "";                
                pc2.BeginOutputReadLine();
                pc2.BeginErrorReadLine();
                pc2.WaitForExit();
                MessageDemux("eac3to return code: " + pc2.ExitCode.ToString());
                pc2.Close();
                res = sb.ToString();
                res = res.Replace("\b", "");

                string[] tmp = res.Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
                for (int i = 0; i < tmp.Length; i++)
                {
                    tmp[i] = tmp[i].Trim();
                }

                if (res.Trim() == "")
                {
                    MessageDemux("Failed to get stream infos");
                    return;
                }

                TitleInfo ti = new TitleInfo();

                if (tmp[0][0] == '-')
                {
                    int length = 0;
                    for (int i = 0; i < tmp[0].Length; i++)
                    {
                        if (tmp[0][i] == '-')
                        {
                            length++;
                        }
                        else
                        {
                            break;
                        }
                    }
                    tmp[0] = tmp[0].Substring(length, tmp[0].Length - length);
                    tmp[0] = tmp[0].Trim();
                    ti.desc = tmp[0];
                }

                for (int i = 0; i < tmp.Length; i++)
                {
                    if (Regex.IsMatch(tmp[i], "^[0-9].*:"))
                    {
                        StreamInfo sr = new StreamInfo();
                        sr.desc = tmp[i];
                        if (i < tmp.Length - 1)
                        {
                            if (!Regex.IsMatch(tmp[i + 1], "^[0-9].*:"))
                            {
                                sr.addInfo = tmp[i + 1];                                

                            }
                        }

                        int pos = tmp[i].IndexOf(':');
                        string substr = tmp[i].Substring(0, pos);
                        sr.number = Convert.ToInt32(substr);

                        substr = tmp[i].Substring(pos + 1).Trim();
                        string[] tmpInfo = substr.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                        if (tmpInfo.Length > 0)
                        {
                            sr.typeDesc = tmpInfo[0];
                            if (tmpInfo[0] == "Chapters")
                            {
                                sr.streamType = StreamType.Chapter;
                            }
                            else if (videoTypes.Contains(tmpInfo[0]))
                            {
                                sr.streamType = StreamType.Video;
                            }
                            else if (ac3AudioTypes.Contains(tmpInfo[0]))
                            {
                                sr.streamType = StreamType.Audio;
                                if (tmpInfo.Length > 1)
                                {
                                    sr.language = tmpInfo[1].Trim();
                                }
                            }
                            else if (dtsAudioTypes.Contains(tmpInfo[0]))
                            {
                                sr.streamType = StreamType.Audio;
                                if (tmpInfo.Length > 1)
                                {
                                    sr.language = tmpInfo[1].Trim();
                                }
                            }
                            else if (tmpInfo[0] == "Subtitle (PGS)")
                            {
                                sr.streamType = StreamType.Subtitle;
                                if (tmpInfo.Length > 1)
                                {
                                    sr.language = tmpInfo[1].Trim();
                                }
                            }
                            else
                            {
                                sr.streamType = StreamType.Unknown;
                            }
                        }
                        else
                        {
                            sr.typeDesc = "Unknown";
                            sr.streamType = StreamType.Unknown;
                        }

                        ti.streams.Add(sr);
                    }
                }

                result.Add(ti);
            }
            catch (Exception ex)
            {
                MessageDemux("Exception: " + ex.Message);
            }
        }

        private bool GetStreamInfo(string path, List<TitleInfo> result)
        {
            try
            {
                sb.Remove(0, sb.Length);
                result.Clear();
                MessageDemux("Getting playlist info...");
                MessageDemux("");
                pc = new Process();
                pc.StartInfo.FileName = settings.eac3toPath;
                pc.StartInfo.Arguments = "\"" + path + "\"";
                
                pc.OutputDataReceived += new DataReceivedEventHandler(OutputDataReceivedDemux);
                pc.ErrorDataReceived += new DataReceivedEventHandler(OutputDataReceivedDemux);

                pc.StartInfo.UseShellExecute = false;
                pc.StartInfo.CreateNoWindow = true;
                pc.StartInfo.RedirectStandardError = true;
                pc.StartInfo.RedirectStandardOutput = true;

                MessageDemux("Command: " + pc.StartInfo.FileName + pc.StartInfo.Arguments);

                if (!pc.Start())
                {
                    MessageDemux("Error starting eac3to.exe");
                    return false;
                }

                pc.BeginOutputReadLine();
                pc.BeginErrorReadLine();
                pc.WaitForExit();
                MessageDemux("eac3to return code: " + pc.ExitCode.ToString());
                pc.Close();

                MessageDemux("");
                MessageDemux("Done.");

                string res = sb.ToString();
                
                res = res.Replace("\b", "");

                string[] tmp = res.Split(new char[] { '\r' }, StringSplitOptions.RemoveEmptyEntries);
                for (int i = 0; i < tmp.Length; i++)
                {
                    tmp[i] = tmp[i].Trim();
                }
                for (int i = 0; i < tmp.Length; i++)
                {
                    if (Regex.IsMatch(tmp[i], "^[0-9].*\\)"))
                    {
                        string[] tmp2 = tmp[i].Split(new char[] { ')' }, StringSplitOptions.RemoveEmptyEntries);
                        GetSubStreamInfo(path, tmp2[0], result);
                    }
                }
                
                return true;
            }
            catch (Exception ex)
            {
                MessageDemux("Exception: " + ex.Message);
                return false;
            }
        }

        private void TitleInfoThread()
        {
            try
            {
                GetStreamInfo(textBoxPath.Text, titleList);
            }
            catch (Exception)
            {
            }
        }

        private void UpdateTitleList()
        {
            try
            {
                comboBoxTitle.Items.Clear();
                listBoxStreams.Items.Clear();

                foreach (TitleInfo ti in titleList)
                {
                    comboBoxTitle.Items.Add(ti.desc);
                }
                if (titleList.Count > 0)
                {
                    comboBoxTitle.SelectedIndex = 0;
                }
            }
            catch (Exception)
            {
            }
        }

        private void buttonGetStreamInfo_Click(object sender, EventArgs e)
        {
            try
            {
                if (!checkEac3to()) return;
                this.Text = title + " [Getting stream info...]";

                progressBarMain.Visible = true;
                buttonAbort.Visible = true;

                m2tsList.Clear();

                titleInfoThread = new Thread(TitleInfoThread);
                titleInfoThread.Start();

                while (titleInfoThread.IsAlive)
                {
                    Application.DoEvents();
                    Thread.Sleep(5);
                }
                titleInfoThread = null;

                UpdateTitleList();

                demuxedStreamList = new TitleInfo();
                UpdateDemuxedStreams();
            }
            catch (Exception ex)
            {
                MessageDemux("Exception: " + ex.Message);
                if (titleInfoThread != null) titleInfoThread = null;                
            }
            finally
            {
                progressBarMain.Visible = false;
                buttonAbort.Visible = false;
                this.Text = title;
            }
        }

        private void buttonEac3toPath_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog ofd = new OpenFileDialog();
                ofd.Filter = "eac3to.exe|eac3to.exe";
                if (ofd.ShowDialog() == DialogResult.OK)
                {                    
                    textBoxEac3toPath.Text = ofd.FileName;
                    settings.eac3toPath = ofd.FileName;
                }
            }
            catch (Exception)
            {
            }
        }

        private void UpdateLanguage()
        {
            try
            {
                listBoxPreferedLanguages.Items.Clear();
                foreach (LanguageInfo li in settings.preferedLanguages)
                {
                    listBoxPreferedLanguages.Items.Add(li.language + " (" + li.translation + " - " + li.languageShort + ")");
                }
            }
            catch (Exception)
            {
            }
        }

        private void UpdateEncodingSettings()
        {
            try
            {
                listBoxX264Profiles.Items.Clear();
                comboBoxEncodeProfile.Items.Clear();
                foreach (EncodingSettings es in settings.encodingSettings)
                {
                    listBoxX264Profiles.Items.Add(es.desc);
                    comboBoxEncodeProfile.Items.Add(es.desc);
                }
                if (settings.lastProfile > -1 && settings.lastProfile < settings.encodingSettings.Count)
                {
                    comboBoxEncodeProfile.SelectedIndex = settings.lastProfile;
                }
            }
            catch (Exception)
            {
            }
        }

        private void UpdateAvisynthSettings()
        {
            try
            {
                listBoxAviSynthProfiles.Items.Clear();
                comboBoxAvisynthProfile.Items.Clear();
                foreach (AvisynthSettings avs in settings.avisynthSettings)
                {
                    listBoxAviSynthProfiles.Items.Add(avs.desc);
                    comboBoxAvisynthProfile.Items.Add(avs.desc);
                }
                if (settings.lastAvisynthProfile > -1 && settings.lastAvisynthProfile < settings.avisynthSettings.Count)
                {
                    comboBoxAvisynthProfile.SelectedIndex = settings.lastAvisynthProfile;
                }
            }
            catch (Exception)
            {
            }
        }

        private void UpdateFromSettings()
        {
            try
            {
                textBoxEac3toPath.Text = settings.eac3toPath;
                textBoxPath.Text = settings.lastBluRayPath;

                checkBoxAutoSelect.Checked = settings.useAutoSelect;
                checkBoxIncludeSubtitle.Checked = settings.includeSubtitle;
                checkBoxPreferDts.Checked = settings.preferDTS;
                checkBoxSelectChapters.Checked = settings.includeChapter;
                listBoxPreferedLanguages.Items.Clear();
                textBoxWorkingDir.Text = settings.workingDir;
                textBoxFfmsindexPath.Text = settings.ffmsindexPath;
                textBoxX264Path.Text = settings.x264Path;
                textBoxSup2subPath.Text = settings.sup2subPath;
                textBoxFilePrefix.Text = settings.filePrefix;
                textBoxJavaPath.Text = settings.javaPath;
                textBoxMkvmergePath.Text = settings.mkvmergePath;

                numericUpDownBlackValue.Value = settings.blackValue;
                numericUpDownNrFrames.Value = settings.nrFrames;

                comboBoxCropMode.SelectedIndex = settings.cropMode;

                comboBoxX264Priority.SelectedItem = Enum.GetName(typeof(ProcessPriorityClass),settings.x264Priority);

                textBoxTargetFolder.Text = settings.targetFolder;
                textBoxTargetfilename.Text = settings.targetFilename;
                textBoxMovieTitle.Text = settings.movieTitle;

                checkBoxDefaultAudioTrack.Checked = settings.defaultAudio;
                checkBoxDefaultSubtitleForced.Checked = settings.defaultSubtitleForced;
                checkBoxDefaultSubtitleTrack.Checked = settings.defaultSubtitle;
                checkBoxDeleteAfterEncode.Checked = settings.deleteAfterEncode;

                checkBoxUseCore.Checked = settings.dtsHdCore;
                checkBoxUntouchedVideo.Checked = settings.untouchedVideo;
                checkBoxResize720p.Checked = settings.resize720p;

                checkBoxUntouchedVideo_CheckedChanged(null, null);

                checkBoxDownmixAc3.Checked = settings.downmixAc3;
                checkBoxDownmixDts.Checked = settings.downmixDTS;

                if (settings.downmixAc3Index > -1 && settings.downmixAc3Index < comboBoxDownmixAc3.Items.Count) comboBoxDownmixAc3.SelectedIndex = settings.downmixAc3Index;
                if (settings.downmixDTSIndex > -1 && settings.downmixDTSIndex < comboBoxDownmixDts.Items.Count) comboBoxDownmixDts.SelectedIndex = settings.downmixDTSIndex;

                checkBoxDownmixAc3_CheckedChanged(null, null);
                checkBoxDownmixDts_CheckedChanged(null, null);

                checkBoxMinimizeCrop.Checked = settings.minimizeAutocrop;
                                     
                
                comboBoxCropInput.SelectedIndex = settings.cropInput;
                comboBoxEncodeInput.SelectedIndex = settings.encodeInput;

                checkBoxUntouchedAudio.Checked = settings.untouchedAudio;

                comboBoxCopySubs.SelectedIndex = settings.copySubs;
                comboBoxMuxSubs.SelectedIndex = settings.muxSubs;

                textBoxDgindexnvPath.Text = settings.dgindexnvPath;

                checkBoxDtsToAc3.Checked = settings.convertDtsToAc3;

                UpdateLanguage();
                UpdateEncodingSettings();
                UpdateAvisynthSettings();
            }
            catch (Exception)
            {
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            try
            {
                CheckForIllegalCrossThreadCalls = false;

                this.Text = title;

                settingsPath = Application.StartupPath + "\\settings.xml";
                if (!File.Exists(settingsPath))
                {
                    UserSettings.SaveSettingsFile(settings, settingsPath);
                    UserSettings.LoadSettingsFile(ref settings, settingsPath);
                }
                else
                {
                    UserSettings.LoadSettingsFile(ref settings, settingsPath);
                }
                UpdateFromSettings();
            }
            catch (Exception)
            {
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                UserSettings.SaveSettingsFile(settings, settingsPath);
                abortThreads();
            }
            catch (Exception)
            {
            }
        }

        private void comboBoxTitle_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (comboBoxTitle.SelectedIndex > -1)
                {
                    listBoxStreams.Items.Clear();
                    UpdateStreamList();
                }
            }
            catch (Exception)
            {
            }
        }

        private string StreamTypeToString(StreamType st)
        {
            if (st == StreamType.Audio)
            {
                return "AUDIO";
            }
            else if (st == StreamType.Chapter)
            {
                return "CHAPTER";
            }
            else if (st == StreamType.Subtitle)
            {
                return "SUBTITLE";
            }
            else if (st == StreamType.Unknown)
            {
                return "UNKNOWN";
            }
            else if (st == StreamType.Video)
            {
                return "VIDEO";
            }
            else
            {
                return "UNKNOWN";
            }
        }

        private bool HasLanguage(string s)
        {
            foreach (LanguageInfo li in settings.preferedLanguages)
            {
                if (li.language == s) return true;
            }
            return false;
        }

        private int LanguagIndex(string s)
        {
            int index = -1;
            for (int i = 0; i < settings.preferedLanguages.Count; i++)
            {
                if (settings.preferedLanguages[i].language == s)
                {
                    index = i;
                    break;
                }
            }
            return index;
        }

        private void UpdateStreamList()
        {
            try
            {
                listBoxStreams.BeginUpdate();
                int maxLength = 0;
                foreach (StreamInfo si in titleList[comboBoxTitle.SelectedIndex].streams)
                {                    
                    maxLength = Math.Max(maxLength, StreamTypeToString(si.streamType).Length);
                }

                foreach (StreamInfo si in titleList[comboBoxTitle.SelectedIndex].streams)
                {                    
                    string desc = "[ " + si.number.ToString("d3") + " ] - [ " + StreamTypeToString(si.streamType);
                    for (int i = 0; i < maxLength - StreamTypeToString(si.streamType).Length; i++) desc += " ";
                    desc += " ] - (" + si.desc + ")";
                    if (si.addInfo != "")
                    {
                        desc += " - (" + si.addInfo + ")";
                    }
                    listBoxStreams.Items.Add(desc);
                }

                List<int> maxac3List = new List<int>();
                List<int> maxdtsList = new List<int>();

                for (int i = 0; i < settings.preferedLanguages.Count; i++)
                {
                    maxac3List.Add(0);
                    maxdtsList.Add(0);
                }
                foreach (StreamInfo si in titleList[comboBoxTitle.SelectedIndex].streams)
                {
                }

                int videoCount = 0;
                int chapterCount = 0;

                if (settings.useAutoSelect)
                {
                    List<int> ac3List = new List<int>();
                    List<int> dtsList = new List<int>();

                    for (int i = 0; i < settings.preferedLanguages.Count; i++)
                    {
                        ac3List.Add(0);
                        dtsList.Add(0);
                    }
                    foreach (StreamInfo si in titleList[comboBoxTitle.SelectedIndex].streams)
                    {
                        if (si.streamType == StreamType.Audio)
                        {
                            if (HasLanguage(si.language))
                            {
                                int index = LanguagIndex(si.language);
                                if (dtsAudioTypes.Contains(si.typeDesc))
                                {
                                    maxdtsList[index]++;
                                }
                                if (ac3AudioTypes.Contains(si.typeDesc))
                                {
                                    maxac3List[index]++;
                                }
                            }
                        }
                    }

                    foreach (StreamInfo si in titleList[comboBoxTitle.SelectedIndex].streams)
                    {
                        if (si.streamType == StreamType.Chapter)
                        {
                            if (settings.includeChapter && chapterCount == 0)
                            {
                                si.selected = true;
                                chapterCount++;
                            }
                        }
                        if (si.streamType == StreamType.Subtitle)
                        {
                            if (settings.includeSubtitle)
                            {
                                if (HasLanguage(si.language))
                                {
                                    si.selected = true;
                                }
                            }
                        }
                        if (si.streamType == StreamType.Audio)
                        {
                            if (HasLanguage(si.language))
                            {
                                int index = LanguagIndex(si.language);
                                if (settings.preferDTS)
                                {
                                    if (dtsAudioTypes.Contains(si.typeDesc))
                                    {
                                        if (dtsList[index] == 0)
                                        {
                                            dtsList[index]++;
                                            si.selected = true;
                                        }
                                    }
                                    if (ac3AudioTypes.Contains(si.typeDesc) && maxdtsList[index] == 0)
                                    {
                                        if (ac3List[index] == 0)
                                        {
                                            ac3List[index]++;
                                            si.selected = true;
                                        }
                                    }
                                }
                                else
                                {
                                    if (ac3AudioTypes.Contains(si.typeDesc))
                                    {   
                                        if (ac3List[index] == 0)
                                        {
                                            if (si.typeDesc == "AC3 Surround" && maxac3List.Count > 0)
                                            {
                                            }
                                            else
                                            {
                                                ac3List[index]++;
                                                si.selected = true;
                                            }
                                        }
                                    }
                                    if (dtsAudioTypes.Contains(si.typeDesc) && maxac3List[index] == 0)
                                    {
                                        if (dtsList[index] == 0)
                                        {
                                            dtsList[index]++;
                                            si.selected = true;
                                        }
                                    }
                                }
                            }
                        }
                        if (si.streamType == StreamType.Video)
                        {
                            if (si.desc.Contains("1080") && videoCount == 0)
                            {
                                si.selected = true;
                                videoCount++;
                            }
                        }
                        
                        listBoxStreams.SetSelected(titleList[comboBoxTitle.SelectedIndex].streams.IndexOf(si),si.selected);
                        
                    }
                }
                listBoxStreams.EndUpdate();
            }
            catch (Exception)
            {
            }
        }

        private void buttonDeleteLanguage_Click(object sender, EventArgs e)
        {
            if (listBoxPreferedLanguages.SelectedIndex > -1)
            {
                settings.preferedLanguages.RemoveAt(listBoxPreferedLanguages.SelectedIndex);
                UpdateLanguage();
            }
        }

        private void checkBoxAutoSelect_CheckedChanged(object sender, EventArgs e)
        {
            settings.useAutoSelect = checkBoxAutoSelect.Checked;
        }

        private void checkBoxSelectChapters_CheckedChanged(object sender, EventArgs e)
        {
            settings.includeChapter = checkBoxSelectChapters.Checked;
        }

        private void checkBoxPreferDts_CheckedChanged(object sender, EventArgs e)
        {
            settings.preferDTS = checkBoxPreferDts.Checked;
        }

        private void checkBoxIncludeSubtitle_CheckedChanged(object sender, EventArgs e)
        {
            settings.includeSubtitle = checkBoxIncludeSubtitle.Checked;
        }

        private void abortThreads()
        {
            try
            {
                abort = true;
                if (titleInfoThread != null)
                {
                    titleInfoThread.Abort();
                    titleInfoThread = null;
                }
                if (demuxThread != null)
                {
                    demuxThread.Abort();
                    demuxThread = null;
                }
                if (indexThread != null)
                {
                    indexThread.Abort();
                    indexThread = null;
                }
                if (subtitleThread != null)
                {
                    subtitleThread.Abort();
                    subtitleThread = null;
                }
                if (encodeThread != null)
                {
                    encodeThread.Abort();
                    encodeThread = null;
                }
                if (muxThread != null)
                {
                    muxThread.Abort();
                    muxThread = null;
                }

                try
                {
                    pc.Kill();
                    pc.Close();
                }
                catch (Exception)
                {
                }

                try
                {
                    pc2.Kill();
                    pc.Close();
                }
                catch (Exception)
                {
                }
            }
            catch (Exception)
            {
            }
        }

        private void buttonAbort_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Are you sure?", "Abort all threads", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    abortThreads();
                }
            }
            catch (Exception)
            {
            }
        }

        private void buttonWorkingDir_Click(object sender, EventArgs e)
        {
            try
            {
                FolderBrowserDialog fbd = new FolderBrowserDialog();

                try
                {
                    if (settings.workingDir != "")
                    {
                        DirectoryInfo di = Directory.GetParent(settings.workingDir);
                        fbd.SelectedPath = di.FullName;
                    }
                }
                catch (Exception)
                {
                }

                if (fbd.ShowDialog() == DialogResult.OK)
                {
                    textBoxWorkingDir.Text = fbd.SelectedPath;
                    settings.workingDir = fbd.SelectedPath;
                }
            }
            catch (Exception)
            {
            }
        }

        private void listBoxStreams_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                for (int i = 0; i < titleList[comboBoxTitle.SelectedIndex].streams.Count; i++)
                {
                    if (listBoxStreams.SelectedIndices.Contains(i)) titleList[comboBoxTitle.SelectedIndex].streams[i].selected = true;
                    else titleList[comboBoxTitle.SelectedIndex].streams[i].selected = false;
                }
            }
            catch (Exception)
            {
            }
        }

        private bool demuxThreadStatus = false;
        private void DemuxThread()
        {
            try
            {
                demuxThreadStatus = false;
                sb.Remove(0, sb.Length);                
                MessageDemux("Starting to demux...");
                MessageDemux("");
                pc = new Process();
                pc.StartInfo.FileName = settings.eac3toPath;
                if (m2tsList.Count == 0)
                {
                    pc.StartInfo.Arguments = "\"" + settings.lastBluRayPath + "\" ";
                }
                else
                {
                    string tmpstr = "";
                    foreach (string s in m2tsList)
                    {
                        tmpstr += "\"" + s + "\"+";
                    }
                    if (tmpstr.Length > 0)
                    {
                        if (tmpstr[tmpstr.Length - 1] == '+')
                        {
                            tmpstr = tmpstr.Substring(0, tmpstr.Length - 1);
                        }
                    }
                    pc.StartInfo.Arguments = tmpstr + " ";
                }

                string prefix = textBoxFilePrefix.Text;

                foreach (StreamInfo si in titleList[comboBoxTitle.SelectedIndex].streams)
                {
                    if (si.selected && si.streamType != StreamType.Unknown)
                    {
                        pc.StartInfo.Arguments += si.number.ToString() + ": \"" + settings.workingDir + "\\" + prefix + "_" + si.number.ToString("d3") + "_";
                        if (si.streamType == StreamType.Chapter)
                        {
                            pc.StartInfo.Arguments += "chapter.txt\" ";
                            si.filename = settings.workingDir + "\\" + prefix + "_" + si.number.ToString("d3") + "_chapter.txt";
                        }
                        else if (si.streamType == StreamType.Audio)
                        {
                            if (ac3AudioTypes.Contains(si.typeDesc))
                            {   
                                if (settings.untouchedAudio && si.typeDesc == "TrueHD/AC3")
                                {
                                    pc.StartInfo.Arguments += "audio_thd_" + si.language + ".thd\" ";
                                    si.filename = settings.workingDir + "\\" + prefix + "_" + si.number.ToString("d3") + "_audio_thd_" + si.language + ".thd";
                                }
                                else
                                {
                                    pc.StartInfo.Arguments += "audio_ac3_" + si.language + ".ac3\" ";
                                    if (settings.downmixAc3)
                                    {
                                        pc.StartInfo.Arguments += "-" + comboBoxDownmixAc3.Text + " ";
                                    }
                                    si.filename = settings.workingDir + "\\" + prefix + "_" + si.number.ToString("d3") + "_audio_ac3_" + si.language + ".ac3";
                                }
                            }
                            else if (dtsAudioTypes.Contains(si.typeDesc))
                            {
                                if (settings.untouchedAudio && (si.typeDesc == "DTS Master Audio" || si.typeDesc == "DTS Hi-Res"))
                                {
                                    pc.StartInfo.Arguments += "audio_dtsHD_" + si.language + ".dtshd\" ";
                                    si.filename = settings.workingDir + "\\" + prefix + "_" + si.number.ToString("d3") + "_audio_dtsHD_" + si.language + ".dtshd";
                                }
                                else if (settings.convertDtsToAc3)
                                {
                                    pc.StartInfo.Arguments += "audio_ac3_" + si.language + ".ac3\" ";
                                    if (settings.downmixAc3)
                                    {
                                        pc.StartInfo.Arguments += "-" + comboBoxDownmixAc3.Text + " ";
                                    }
                                    si.filename = settings.workingDir + "\\" + prefix + "_" + si.number.ToString("d3") + "_audio_ac3_" + si.language + ".ac3";
                                }
                                else
                                {
                                    pc.StartInfo.Arguments += "audio_dts_" + si.language + ".dts\" ";
                                    if (si.addInfo.Contains("core") && settings.dtsHdCore)
                                    {
                                        pc.StartInfo.Arguments += "-core ";
                                    }
                                    if (settings.downmixDTS)
                                    {
                                        pc.StartInfo.Arguments += "-" + comboBoxDownmixDts.Text + " ";
                                    }
                                    si.filename = settings.workingDir + "\\" + prefix + "_" + si.number.ToString("d3") + "_audio_dts_" + si.language + ".dts";
                                }
                            }
                        }
                        else if (si.streamType == StreamType.Video)
                        {
                            pc.StartInfo.Arguments += "video.mkv\" ";
                            si.filename = settings.workingDir + "\\" + prefix + "_" + si.number.ToString("d3") + "_video.mkv";
                        }
                        else if (si.streamType == StreamType.Subtitle)
                        {
                            pc.StartInfo.Arguments += "subtitle_"+ si.language +".sup\" ";
                            si.filename = settings.workingDir + "\\" + prefix + "_" + si.number.ToString("d3") + "_subtitle_" + si.language + ".sup";
                        }
                    }
                }
                MessageDemux("Command: " + pc.StartInfo.FileName + pc.StartInfo.Arguments);
                pc.OutputDataReceived += new DataReceivedEventHandler(OutputDataReceivedDemux);
                //pc.ErrorDataReceived += new DataReceivedEventHandler(ErrorDataReceived);

                pc.StartInfo.UseShellExecute = false;
                pc.StartInfo.CreateNoWindow = true;
                pc.StartInfo.RedirectStandardError = true;
                pc.StartInfo.RedirectStandardOutput = true;

                if (!pc.Start())
                {
                    MessageDemux("Error starting eac3to.exe");
                    return;
                }

                //pc.BeginErrorReadLine();
                pc.BeginOutputReadLine();
                
                pc.WaitForExit();
                MessageDemux("eac3to return code: " + pc.ExitCode.ToString());
                pc.Close();                

                demuxedStreamList = new TitleInfo();
                demuxedStreamList.desc = titleList[comboBoxTitle.SelectedIndex].desc;

                foreach (StreamInfo si in titleList[comboBoxTitle.SelectedIndex].streams)
                {
                    if (si.selected)
                    {
                        demuxedStreamList.streams.Add(new StreamInfo(si));
                    }
                }
                TitleInfo.SaveStreamInfoFile(demuxedStreamList, settings.workingDir + "\\" + prefix + "_streamInfo.xml");
                UpdateDemuxedStreams();
                MessageDemux("");
                MessageDemux("Done.");

                // sort streamlist
                TitleInfo tmpList = new TitleInfo();
                tmpList.desc = demuxedStreamList.desc;

                // chapter first
                foreach (StreamInfo si in demuxedStreamList.streams)
                {
                    if (si.streamType == StreamType.Chapter)
                    {
                        tmpList.streams.Add(new StreamInfo(si));
                    }
                }
                // video
                foreach (StreamInfo si in demuxedStreamList.streams)
                {
                    if (si.streamType == StreamType.Video)
                    {
                        if (settings.untouchedVideo)
                        {
                            if (si.extraFileInfo.GetType() != typeof(VideoFileInfo))
                            {
                                si.extraFileInfo = new VideoFileInfo();
                            }
                            ((VideoFileInfo)si.extraFileInfo).encodedFile = si.filename;
                        }
                        tmpList.streams.Add(new StreamInfo(si));
                    }
                }
                // audio
                foreach (LanguageInfo li in settings.preferedLanguages)
                {
                    foreach (StreamInfo si in demuxedStreamList.streams)
                    {
                        if (si.streamType == StreamType.Audio)
                        {
                            if (si.language == li.language)
                            {
                                tmpList.streams.Add(new StreamInfo(si));
                            }
                        }
                    }
                }
                foreach (StreamInfo si in demuxedStreamList.streams)
                {
                    if (si.streamType == StreamType.Audio)
                    {
                        if (!HasLanguage(si.language))
                        {
                            tmpList.streams.Add(new StreamInfo(si));
                        }
                    }
                }
                // subtitle
                foreach (LanguageInfo li in settings.preferedLanguages)
                {
                    foreach (StreamInfo si in demuxedStreamList.streams)
                    {
                        if (si.streamType == StreamType.Subtitle)
                        {
                            if (si.language == li.language)
                            {
                                tmpList.streams.Add(new StreamInfo(si));
                            }
                        }
                    }
                }
                foreach (StreamInfo si in demuxedStreamList.streams)
                {
                    if (si.streamType == StreamType.Subtitle)
                    {
                        if (!HasLanguage(si.language))
                        {
                            tmpList.streams.Add(new StreamInfo(si));
                        }
                    }
                }
                demuxedStreamList = tmpList;
                TitleInfo.SaveStreamInfoFile(demuxedStreamList, settings.workingDir + "\\" + prefix + "_streamInfo.xml");
                UpdateDemuxedStreams();

                demuxThreadStatus = true;
            }
            catch (Exception ex)
            {
                MessageDemux("Exception: " + ex.Message);
            }
        }

        private TitleInfo demuxedStreamList = new TitleInfo();

        private void UpdateDemuxedStreams()
        {
            try
            {
                listBoxDemuxedStreams.Items.Clear();
                foreach (StreamInfo si in demuxedStreamList.streams)
                {
                    listBoxDemuxedStreams.Items.Add(si.typeDesc + " - " + si.filename);
                }
            }
            catch (Exception)
            {
            }
        }

        private bool indexThreadStatus = false;
        private void IndexThread()
        {
            try
            {
                indexThreadStatus = false;
                string filename = "";
                foreach (StreamInfo si in demuxedStreamList.streams)
                {
                    if (si.streamType == StreamType.Video)
                    {
                        filename = si.filename;
                        break;
                    }
                }

                string fps = "";

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
                    }
                    mi2.Close();
                }
                catch (Exception ex)
                {
                    MessageCrop("Error getting MediaInfo: " + ex.Message);
                    return;
                }

                if (fps == "")
                {
                    MessageCrop("Error getting framerate");
                    foreach (StreamInfo si in demuxedStreamList.streams)
                    {
                        if (si.streamType == StreamType.Video)
                        {
                            if (si.addInfo.Contains("24p /1.001"))
                            {
                                MessageCrop("Assume fps is 23.976");
                                fps = "23.976";
                                break;
                            }
                            // add other framerates here
                        }
                    }
                    if (fps == "")
                    {
                        return;
                    }
                }

                sb.Remove(0, sb.Length);
                if (!settings.untouchedVideo)
                {
                    if (settings.cropInput == 1 || settings.encodeInput == 1)
                    {
                        if (!File.Exists(filename + ".ffindex"))
                        {
                            MessageCrop("Starting to index...");
                            MessageCrop("");

                            pc = new Process();
                            pc.StartInfo.FileName = settings.ffmsindexPath;
                            pc.StartInfo.Arguments = "\"" + filename + "\"";

                            MessageCrop("Command: " + pc.StartInfo.FileName + pc.StartInfo.Arguments);
                            pc.OutputDataReceived += new DataReceivedEventHandler(OutputDataReceivedCrop);

                            pc.StartInfo.UseShellExecute = false;
                            pc.StartInfo.CreateNoWindow = true;
                            pc.StartInfo.RedirectStandardError = true;
                            pc.StartInfo.RedirectStandardOutput = true;

                            if (!pc.Start())
                            {
                                MessageCrop("Error starting ffmsindex.exe");
                                return;
                            }

                            pc.BeginOutputReadLine();

                            pc.WaitForExit();
                            MessageCrop("ffmsindex return code: " + pc.ExitCode.ToString());
                            pc.Close();
                            MessageCrop("Indexing done!");
                        }
                        else
                        {
                            MessageCrop(filename + ".ffindex already exits");
                        }
                    }
                    else if (settings.cropInput == 2 || settings.encodeInput == 2)
                    {
                        string output = Path.ChangeExtension(filename, "dgi");

                        if (!File.Exists(output))
                        {
                            MessageCrop("Starting to index...");
                            MessageCrop("");

                            pc = new Process();
                            pc.StartInfo.FileName = settings.dgindexnvPath;
                            pc.StartInfo.Arguments = "-i \"" + filename + "\" -o \"" + output + "\" -e";

                            MessageCrop("Command: " + pc.StartInfo.FileName + pc.StartInfo.Arguments);
                            pc.OutputDataReceived += new DataReceivedEventHandler(OutputDataReceivedCrop);

                            pc.StartInfo.UseShellExecute = false;
                            pc.StartInfo.CreateNoWindow = true;
                            pc.StartInfo.RedirectStandardError = true;
                            pc.StartInfo.RedirectStandardOutput = true;

                            if (!pc.Start())
                            {
                                MessageCrop("Error starting DGIndexNv.exe");
                                return;
                            }

                            pc.BeginOutputReadLine();

                            pc.WaitForExit();
                            MessageCrop("dgindexnv return code: " + pc.ExitCode.ToString());
                            pc.Close();
                            MessageCrop("Indexing done!");
                        }
                        else
                        {
                            MessageCrop(output + " already exists");
                        }
                    }

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
                    AutoCrop ac = new AutoCrop(settings.workingDir + "\\" + settings.filePrefix + "_cropTemp.avs", settings);
                    if (ac.error)
                    {
                        MessageCrop("Exception: " + ac.errorStr);
                        return;
                    }

                    if (settings.minimizeAutocrop)
                    {
                        ac.WindowState = FormWindowState.Minimized;
                    }
                    
                    ac.NrFrames = settings.nrFrames;
                    ac.BlackValue = settings.blackValue;
                    ac.ShowDialog();
                                       

                    MessageCrop("");
                    MessageCrop("Crop top: " + ac.cropTop.ToString());
                    MessageCrop("Crop bottom: " + ac.cropBottom.ToString());
                    if (ac.border)
                    {
                        MessageCrop("Border top: " + ac.borderTop.ToString());
                        MessageCrop("Border bottom: " + ac.borderBottom.ToString());
                    }
                    if (ac.resize)
                    {
                        MessageCrop("Resize to: " + ac.resizeX.ToString() + " x " + ac.resizeY.ToString());
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
                    if (ac.cropTop != 0 || ac.cropBottom != 0)
                    {
                        encode += "Crop(0," + ac.cropTop.ToString() + ",-0,-" + ac.cropBottom.ToString() + ")\r\n";
                        if (ac.border)
                        {
                            encode += "AddBorders(0," + ac.borderTop + ",0," + ac.borderBottom + ")\r\n";
                        }
                        else
                        {
                            MessageCrop("Did not add AddBorders command");
                        }
                        if (ac.resize)
                        {
                            encode += "LanczosResize(" + ac.resizeX.ToString() + "," + ac.resizeY.ToString() + ")\r\n";
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
                    }
                }
                TitleInfo.SaveStreamInfoFile(demuxedStreamList, settings.workingDir + "\\" + settings.filePrefix + "_streamInfo.xml");
                UpdateDemuxedStreams();
                indexThreadStatus = true;
            }
            catch (Exception ex)
            {
                MessageCrop("Exception: " + ex.Message);
            }
            finally
            {
                
            }
        }

        private bool DoDemux()
        {
            try
            {
                this.Text = title + " [Demuxing...]";
                notifyIconMain.Text = this.Text;

                if (settings.workingDir == "")
                {
                    MessageMain("Working dir not set");
                    if(!silent) MessageBox.Show("Working dir not set", "Error");
                    return false;
                }
                if (comboBoxTitle.SelectedIndex == -1)
                {
                    MessageMain("No title selected");
                    if (!silent) MessageBox.Show("No title selected", "Error");
                    return false;
                }
                int videoCount = 0;
                int audioCount = 0;
                int unknown = 0;
                foreach (StreamInfo si in titleList[comboBoxTitle.SelectedIndex].streams)
                {
                    if (si.streamType == StreamType.Audio && si.selected)
                    {
                        audioCount++;
                    }
                    if (si.streamType == StreamType.Video && si.selected)
                    {
                        videoCount++;
                    }
                    if (si.streamType == StreamType.Unknown && si.selected)
                    {
                        unknown++;
                    }
                }
                if (audioCount < 1)
                {
                    MessageMain("No audio streams selected");
                    if (!silent) MessageBox.Show("No audio streams selected", "Error");
                    return false;
                }
                if (videoCount != 1)
                {
                    MessageMain("No video stream or more then one selected");
                    if (!silent) MessageBox.Show("No video stream or more then one selected", "Error");
                    return false;
                }
                if (unknown > 0)
                {
                    MessageMain("Unknown tracks selected - please report log to developer");
                    if (!silent) MessageBox.Show("Unknown tracks selected - please report log to developer", "Error");
                    return false;
                }

                progressBarMain.Visible = true;
                buttonAbort.Visible = true;

                demuxThread = new Thread(DemuxThread);
                demuxThread.Start();

                while (demuxThread.IsAlive)
                {
                    Application.DoEvents();
                    Thread.Sleep(5);
                }
                demuxThread = null;
                return demuxThreadStatus;
            }
            catch (Exception ex)
            {
                MessageDemux("Exception: " + ex.Message);
                return false;
            }
            finally
            {
                this.Text = title;
                notifyIconMain.Text = this.Text;
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


                indexThread = new Thread(IndexThread);
                indexThread.Start();

                while (indexThread.IsAlive)
                {
                    Application.DoEvents();
                    Thread.Sleep(5);
                }
                indexThread = null;
                return indexThreadStatus;
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

        private bool hasFpsValue()
        {
            try
            {
                if (demuxedStreamList.streams.Count == 0)
                {
                    return false;
                }
                foreach (StreamInfo si in demuxedStreamList.streams)
                {
                    if (si.streamType == StreamType.Video)
                    {
                        if (si.extraFileInfo.GetType() == typeof(VideoFileInfo))
                        {
                            if (((VideoFileInfo)si.extraFileInfo).fps != "") return true;
                        }
                        else return false;
                    }
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private bool hasAvsValue()
        {
            try
            {
                if (demuxedStreamList.streams.Count == 0)
                {
                    return false;
                }
                foreach (StreamInfo si in demuxedStreamList.streams)
                {
                    if (si.streamType == StreamType.Video)
                    {
                        if (si.extraFileInfo.GetType() == typeof(VideoFileInfo))
                        {
                            if (((VideoFileInfo)si.extraFileInfo).encodeAvs != "") return true;
                        }
                        else return false;
                    }
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private bool hasOutputVideoValue()
        {
            try
            {
                if (demuxedStreamList.streams.Count == 0)
                {
                    return false;
                }
                foreach (StreamInfo si in demuxedStreamList.streams)
                {
                    if (si.streamType == StreamType.Video)
                    {
                        if (si.extraFileInfo.GetType() == typeof(VideoFileInfo))
                        {
                            if (((VideoFileInfo)si.extraFileInfo).encodedFile != "") return true;
                        }
                        else return false;
                    }
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private void buttonStartConvert_Click(object sender, EventArgs e)
        {
            try
            {
                if (!checkComplete()) return;
                progressBarMain.Visible = true;
                buttonAbort.Visible = true;
                tabControlMain.Enabled = false;

                if (demuxedStreamList.streams.Count == 0)
                {
                    if (!DoDemux()) return;
                    if (!DoIndex()) return;
                    if (!DoSubtitle()) return;
                    if (!settings.untouchedVideo)
                    {
                        if (!DoEncode()) return;
                    }
                    if (!DoMux()) return;
                    SaveLog(richTextBoxLogMain.Text, settings.workingDir + "\\" + settings.filePrefix + "_completeLog.txt");
                }
                else
                {
                    if (!hasOutputVideoValue())
                    {
                        if (!hasAvsValue() || !hasFpsValue())
                        {
                            if (!DoIndex()) return;
                        }
                        if (!DoSubtitle()) return;
                        if (!DoEncode()) return;
                    }
                    else
                    {
                        if (!DoMux()) return;
                    }
                    SaveLog(richTextBoxLogMain.Text, settings.workingDir + "\\" + settings.filePrefix + "_completeLog.txt");
                }
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

        private void buttonFfmsindexPath_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog ofd = new OpenFileDialog();
                ofd.Filter = "ffmsindex.exe|ffmsindex.exe";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    textBoxFfmsindexPath.Text = ofd.FileName;
                    settings.ffmsindexPath = ofd.FileName;
                }
            }
            catch (Exception)
            {
            }
        }

        private void buttonX264Path_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog ofd = new OpenFileDialog();
                ofd.Filter = "x264.exe|x264.exe";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    textBoxX264Path.Text = ofd.FileName;
                    settings.x264Path = ofd.FileName;
                }
            }
            catch (Exception)
            {
            }
        }

        private void buttonSup2subPath_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog ofd = new OpenFileDialog();
                ofd.Filter = "BDSup2Sub.jar|BDSup2Sub.jar";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    textBoxSup2subPath.Text = ofd.FileName;
                    settings.sup2subPath = ofd.FileName;
                }
            }
            catch (Exception)
            {
            }
        }
        
        private void textBoxFilePrefix_TextChanged(object sender, EventArgs e)
        {
            try
            {
                settings.filePrefix = textBoxFilePrefix.Text;
            }
            catch (Exception)
            {
            }
        }

        private void buttonJavaPath_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog ofd = new OpenFileDialog();
                ofd.Filter = "java.exe|java.exe";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    textBoxJavaPath.Text = ofd.FileName;
                    settings.javaPath = ofd.FileName;
                }
            }
            catch (Exception)
            {
            }
        }

        private void numericUpDownNrFrames_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                settings.nrFrames = (int)numericUpDownNrFrames.Value;
            }
            catch (Exception)
            {
            }
        }

        private void numericUpDownBlackValue_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                settings.blackValue = (int)numericUpDownBlackValue.Value;
            }
            catch (Exception)
            {
            }
        }

        private void buttonLoadStreamInfo_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog ofd = new OpenFileDialog();
                ofd.Filter = "*_streamInfo.xml|*.xml";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    TitleInfo.LoadSettingsFile(ref demuxedStreamList, ofd.FileName);
                    UpdateDemuxedStreams();
                }
            }
            catch (Exception)
            {
            }
        }

        private void buttonClearStreamInfoList_Click(object sender, EventArgs e)
        {
            try
            {
                demuxedStreamList = new TitleInfo();
                UpdateDemuxedStreams();
            }
            catch (Exception)
            {
            }
        }
        
        private void buttonAddLanguage_Click(object sender, EventArgs e)
        {
            try
            {
                LanguageInfo li = new LanguageInfo();
                li.language = "Language";
                li.languageShort = "la";
                li.translation = "Language";
                settings.preferedLanguages.Add(li);
                UpdateLanguage();
            }
            catch (Exception)
            {
            }
        }

        private void listBoxPreferedLanguages_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                int index = listBoxPreferedLanguages.SelectedIndex;
                if (index > -1)
                {
                    LanguageForm lf = new LanguageForm(settings.preferedLanguages[index]);
                    if (lf.ShowDialog() == DialogResult.OK)
                    {
                        settings.preferedLanguages[index] = new LanguageInfo(lf.li);
                        UpdateLanguage();
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        private void buttonAddX264_Click(object sender, EventArgs e)
        {
            try
            {
                EncodingSettings es = new EncodingSettings("Description", "Parameter");
                settings.encodingSettings.Add(es);
                UpdateEncodingSettings();
            }
            catch (Exception)
            {
            }
        }

        private void buttonDelX264_Click(object sender, EventArgs e)
        {
            try
            {
                int index = listBoxX264Profiles.SelectedIndex;
                if (index > -1)
                {
                    settings.encodingSettings.RemoveAt(index);
                    UpdateEncodingSettings();
                }
            }
            catch (Exception)
            {
            }
        }

        private void listBoxX264Profiles_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                int index = listBoxX264Profiles.SelectedIndex;
                if (index > -1)
                {
                    EncoderSettingsForm esf = new EncoderSettingsForm(settings.encodingSettings[index]);
                    if (esf.ShowDialog() == DialogResult.OK)
                    {
                        settings.encodingSettings[index] = new EncodingSettings(esf.es);
                        UpdateEncodingSettings();
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        private void buttonDoDemux_Click(object sender, EventArgs e)
        {
            try
            {
                if (!checkEac3to()) return;
                progressBarMain.Visible = true;
                buttonAbort.Visible = true;
                tabControlMain.Enabled = false;

                DoDemux();
            }
            catch (Exception ex)
            {
                MessageDemux("Exception: " + ex.Message);
            }
            finally
            {
                tabControlMain.Enabled = true;
                progressBarMain.Visible = false;
                buttonAbort.Visible = false;
            }
        }

        private void buttonDoIndex_Click(object sender, EventArgs e)
        {
            try
            {
                if (!checkIndex()) return;
                progressBarMain.Visible = true;
                buttonAbort.Visible = true;
                tabControlMain.Enabled = false;

                DoIndex();
            }
            catch (Exception ex)
            {
                MessageCrop("Exception: " + ex.Message);
            }
            finally
            {
                tabControlMain.Enabled = true;
                progressBarMain.Visible = false;
                buttonAbort.Visible = false;
            }
        }

        private void buttonDoEncode_Click(object sender, EventArgs e)
        {
            try
            {
                if (!checkEac3to()) return;
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
                
        private bool DoEncode()
        {
            try
            {
                this.Text = title + " [Encoding...]";
                notifyIconMain.Text = this.Text;

                encodeThread = new Thread(EncodeThread);
                encodeThread.Start();

                while (encodeThread.IsAlive)
                {
                    Application.DoEvents();
                    Thread.Sleep(5);
                }
                encodeThread = null;
                return encodeThreadStatus;
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

        private class OutputReader
        {

            private StreamReader sr = null;
            private string text = "";
            public string Text { get { return text; } }
            private MsgHandler Message = null;
            private string lastMsg = "";
            private UserSettings settings = null;
            private int index = -1;
            private MainForm mf = null;
            private NotifyIcon ni = null;

            public OutputReader(StreamReader sr, MsgHandler Message, UserSettings settings, int index, MainForm mf, NotifyIcon ni)
            {
                try
                {
                    this.sr = sr;
                    this.Message = Message;
                    this.settings = settings;
                    this.index = index;
                    this.mf = mf;
                    this.ni = ni;
                }
                catch (Exception)
                {
                }
            }

            public void Start()
            {
                try
                {
                    while ((text = sr.ReadLine()) != null)
                    {
                        int s = text.IndexOf('[');
                        int e = text.IndexOf(']');
                        if (s == 0 && e > 0)
                        {
                            string substr = text.Substring(s + 1, e - s - 2);
                            if (substr != lastMsg)
                            {
                                Message(text);
                                lastMsg = substr;
                                
                                if (index > -1 && settings.encodingSettings[index].pass2)
                                {
                                    if (!mf.secondPass)
                                    {
                                        mf.Text = mf.title + " [Encoding 1. pass " + text + "]";
                                        ni.Text = "Encoding 1. pass - " + substr + "%";
                                    }
                                    else
                                    {
                                        mf.Text = mf.title + " [Encoding 2. pass " + text + "]";
                                        ni.Text = "Encoding 2. pass - " + substr + "%";
                                    }
                                }
                                else
                                {
                                    mf.Text = mf.title + " [Encoding " + text + "]";
                                    ni.Text = "Encoding - " + substr + "%";
                                }
                            }
                        }
                        else
                        {
                            Message(text);
                        }
                    }
                    text = sr.ReadToEnd();
                }
                catch (Exception)
                {
                }
            }
        }

        public bool secondPass = false;
        private bool encodeThreadStatus = false;
        private void EncodeThread()
        {
            try
            {
                encodeThreadStatus = false;
                if (settings.workingDir == "")
                {
                    MessageMain("Working dir not set");
                    if (!silent) MessageBox.Show("Working dir not set", "Error");
                    return;
                }
                if (demuxedStreamList.streams.Count == 0)
                {
                    MessageMain("No demuxed streams available");
                    if (!silent) MessageBox.Show("No demuxed streams available", "Error");
                    return;
                }

                string filename = "";
                foreach (StreamInfo si in demuxedStreamList.streams)
                {
                    if (si.streamType == StreamType.Video)
                    {
                        if (si.extraFileInfo.GetType() == typeof(VideoFileInfo))
                        {
                            filename = ((VideoFileInfo)si.extraFileInfo).encodeAvs;
                            break;
                        }
                    }
                }

                if (filename == "")
                {
                    MessageMain("Encode avs not set - do index + autocrop first");
                    if (!silent) MessageBox.Show("Encode avs not set - do index + autocrop first", "Error");
                    return;
                }

                int index = comboBoxEncodeProfile.SelectedIndex;
                if (index < 0)
                {
                    MessageMain("Encoding profile not set");
                    if (!silent) MessageBox.Show("Encoding profile not set", "Error");
                    return;
                }

                sb.Remove(0, sb.Length);

                if (!settings.encodingSettings[index].pass2)
                {
                    MessageEncode("Starting to encode...");
                    MessageEncode("");
                }
                else
                {
                    secondPass = false;
                    MessageEncode("Starting to encode 1. pass...");
                    MessageEncode("");
                }

                pc = new Process();
                pc.StartInfo.FileName = settings.x264Path;
                pc.StartInfo.Arguments = settings.encodingSettings[index].settings + " \"" + filename + "\" -o \"" + settings.workingDir +
                    "\\" + settings.filePrefix + "_video.mkv\"";

                MessageEncode("Command: " + pc.StartInfo.FileName + pc.StartInfo.Arguments);
                
                pc.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                pc.StartInfo.UseShellExecute = false;
                pc.StartInfo.CreateNoWindow = true;
                pc.StartInfo.RedirectStandardOutput = true;
                pc.StartInfo.RedirectStandardError = true;

                if (!pc.Start())
                {
                    MessageEncode("Error starting x264.exe");
                    return;
                }

                pc.PriorityClass = settings.x264Priority;
                
                OutputReader or = new OutputReader(pc.StandardOutput, MessageEncode, settings, index, this, notifyIconMain);
                Thread orThread = new Thread(or.Start);

                OutputReader or2 = new OutputReader(pc.StandardError, MessageEncode, settings, index, this, notifyIconMain);
                Thread or2Thread = new Thread(or2.Start);

                orThread.Start();
                or2Thread.Start();

                orThread.Join();
                or2Thread.Join();

                pc.WaitForExit();
                MessageEncode("x264 return code: " + pc.ExitCode.ToString());
                MessageEncode(or.Text);
                MessageEncode(or2.Text);
                                
                pc.Close();
                MessageEncode("Encoding done!");
                
                if (settings.encodingSettings[index].pass2)
                {
                    secondPass = true;
                    MessageEncode("Starting to encode 2. pass...");
                    MessageEncode("");

                    pc = new Process();
                    pc.StartInfo.FileName = settings.x264Path;
                    pc.StartInfo.Arguments = settings.encodingSettings[index].settings2 + " \"" + filename + "\" -o \"" + settings.workingDir +
                        "\\" + settings.filePrefix + "_video.mkv\"";

                    MessageEncode("Command: " + pc.StartInfo.FileName + pc.StartInfo.Arguments);

                    pc.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                    pc.StartInfo.UseShellExecute = false;
                    pc.StartInfo.CreateNoWindow = true;
                    pc.StartInfo.RedirectStandardOutput = true;
                    pc.StartInfo.RedirectStandardError = true;

                    if (!pc.Start())
                    {
                        MessageEncode("Error starting x264.exe");
                        return;
                    }

                    pc.PriorityClass = settings.x264Priority;

                    or = new OutputReader(pc.StandardOutput, MessageEncode, settings, index, this, notifyIconMain);
                    orThread = new Thread(or.Start);

                    or2 = new OutputReader(pc.StandardError, MessageEncode, settings, index, this, notifyIconMain);
                    or2Thread = new Thread(or2.Start);

                    orThread.Start();
                    or2Thread.Start();

                    orThread.Join();
                    or2Thread.Join();

                    pc.WaitForExit();

                    MessageEncode(or.Text);
                    MessageEncode(or2.Text);

                    pc.Close();

                    MessageEncode("Encoding done!");
                }

                foreach (StreamInfo si in demuxedStreamList.streams)
                {
                    if (si.streamType == StreamType.Video)
                    {
                        if (si.extraFileInfo.GetType() == typeof(VideoFileInfo))
                        {
                            ((VideoFileInfo)si.extraFileInfo).encodedFile = settings.workingDir + "\\" + settings.filePrefix + "_video.mkv";
                            break;
                        }
                    }
                }
                TitleInfo.SaveStreamInfoFile(demuxedStreamList, settings.workingDir + "\\" + settings.filePrefix + "_streamInfo.xml");
                encodeThreadStatus = true;
            }
            catch (Exception ex)
            {
                MessageEncode("Exception: " + ex.Message);
            }
            finally
            {
                
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

        private bool subtitleThreadStatus = false;
        private void SubtitleThread()
        {
            try
            {
                subtitleThreadStatus = false;
                
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
                    MessageMain("Framerate not set - do index + autocrop first");
                    if (!silent) MessageBox.Show("Framerate not set - do index + autocrop first", "Error");
                    return;
                }

                int subtitleCount = 0;
                foreach (StreamInfo si in demuxedStreamList.streams)
                {
                    if (si.streamType == StreamType.Subtitle)
                    {
                        subtitleCount++;
                    }
                }

                int subtitle = 0;
                foreach (StreamInfo si in demuxedStreamList.streams)
                {
                    if (si.streamType == StreamType.Subtitle)
                    {
                        subtitle++;
                        this.Text = title + " [Processing subtitles (" + subtitle.ToString() + "/" + subtitleCount.ToString() + ")...]";
                        si.extraFileInfo = new SubtitleFileInfo();
                        SubtitleFileInfo sfi = (SubtitleFileInfo)si.extraFileInfo;

                        string output = settings.workingDir + "\\" + Path.GetFileNameWithoutExtension(si.filename) +
                            "_complete.sub";

                        string outputIdx = settings.workingDir + "\\" + Path.GetFileNameWithoutExtension(si.filename) +
                            "_complete.idx";

                        sb.Remove(0, sb.Length);
                        MessageSubtitle("Starting to process subtitle...");
                        MessageSubtitle("");

                        pc = new Process();
                        pc.StartInfo.FileName = settings.javaPath;
                        pc.StartInfo.Arguments = "-jar \"" + settings.sup2subPath + "\" \"" +
                            si.filename + "\" \"" + output + "\" /fps:" + fps;

                        if (!settings.resize720p)
                        {
                            pc.StartInfo.Arguments += " /res:1080";
                        }
                        else
                        {
                            pc.StartInfo.Arguments += " /res:720";
                        }

                        MessageSubtitle("Command: " + pc.StartInfo.FileName + pc.StartInfo.Arguments);
                        
                        pc.OutputDataReceived += new DataReceivedEventHandler(OutputDataReceivedSubtitle);

                        pc.StartInfo.UseShellExecute = false;
                        pc.StartInfo.CreateNoWindow = true;
                        pc.StartInfo.RedirectStandardError = true;
                        pc.StartInfo.RedirectStandardOutput = true;

                        if (!pc.Start())
                        {
                            MessageSubtitle("Error starting java.exe");
                            return;
                        }

                        pc.BeginOutputReadLine();

                        pc.WaitForExit();
                        MessageSubtitle("bdsup2sub return code: " + pc.ExitCode.ToString());
                        pc.Close();
                        MessageSubtitle("Processing done!");

                        if (File.Exists(output))
                        {
                            sfi.normalSub = output;
                        }
                        if (File.Exists(outputIdx))
                        {
                            sfi.normalIdx = outputIdx;
                        }
                        ////////////////////////////////////////////////////////

                        output = settings.workingDir + "\\" + Path.GetFileNameWithoutExtension(si.filename) +
                            "_onlyforced.sub";

                        outputIdx = settings.workingDir + "\\" + Path.GetFileNameWithoutExtension(si.filename) +
                            "_onlyforced.idx";

                        sb.Remove(0, sb.Length);
                        MessageSubtitle("Starting to process subtitle...");
                        MessageSubtitle("");

                        pc = new Process();
                        pc.StartInfo.FileName = settings.javaPath;
                        pc.StartInfo.Arguments = "-jar \"" + settings.sup2subPath + "\" \"" +
                            si.filename + "\" \"" + output + "\" /forced+ /fps:" + fps;

                        if (!settings.resize720p)
                        {
                            pc.StartInfo.Arguments += " /res:1080";
                        }
                        else
                        {
                            pc.StartInfo.Arguments += " /res:720";
                        }

                        MessageSubtitle("Command: " + pc.StartInfo.FileName + pc.StartInfo.Arguments);

                        pc.OutputDataReceived += new DataReceivedEventHandler(OutputDataReceivedSubtitle);

                        pc.StartInfo.UseShellExecute = false;
                        pc.StartInfo.CreateNoWindow = true;
                        pc.StartInfo.RedirectStandardError = true;
                        pc.StartInfo.RedirectStandardOutput = true;

                        if (!pc.Start())
                        {
                            MessageSubtitle("Error starting java.exe");
                            return;
                        }

                        pc.BeginOutputReadLine();

                        pc.WaitForExit();
                        pc.Close();
                        MessageSubtitle("Processing done!");

                        if (File.Exists(output))
                        {
                            sfi.forcedSub = output;
                        }
                        if (File.Exists(outputIdx))
                        {
                            sfi.forcedIdx = outputIdx;
                        }
                        try
                        {
                            if (sfi.normalIdx != "" && sfi.normalSub != "" && sfi.forcedIdx != "" && sfi.forcedSub != "")
                            {
                                FileInfo f1 = new FileInfo(sfi.normalSub);
                                FileInfo f2 = new FileInfo(sfi.forcedSub);
                                if (f1.Length == f2.Length)
                                {
                                    File.Delete(sfi.normalSub);
                                    File.Delete(sfi.normalIdx);
                                    sfi.normalSub = "";
                                    sfi.normalIdx = "";
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageSubtitle("Exception: " + ex.Message);
                        }
                    }
                }
                TitleInfo.SaveStreamInfoFile(demuxedStreamList, settings.workingDir + "\\" + settings.filePrefix + "_streamInfo.xml");
                subtitleThreadStatus = true;
            }
            catch (Exception ex)
            {
                MessageSubtitle("Exception: " + ex.Message);
            }
        }

        private bool DoSubtitle()
        {
            try
            {
                if (demuxedStreamList.streams.Count == 0)
                {
                    MessageMain("No demuxed streams available");
                    if (!silent) MessageBox.Show("No demuxed streams available", "Error");
                    return false;
                }

                this.Text = title + " [Processing subtitles...]";
                notifyIconMain.Text = this.Text;
                
                subtitleThread = new Thread(SubtitleThread);
                subtitleThread.Start();

                while (subtitleThread.IsAlive)
                {
                    Application.DoEvents();
                    Thread.Sleep(5);
                }
                subtitleThread = null;
                return subtitleThreadStatus;
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

        private string avisynthLink = "http://sourceforge.net/projects/avisynth2/files/";
        private string haaliLink = "http://haali.su/mkv/";
        private string eac3toLink = "http://forum.doom9.org/showthread.php?t=125966";
        private string x264Link = "http://x264.nl/";
        private string bdsup2subLink = "http://forum.doom9.org/showthread.php?t=145277";
        private string ffmpegsrcLink = "http://code.google.com/p/ffmpegsource/downloads/list";
        private string javaLink = "http://java.com/downloads/";
        private string mkvtoolnixLink = "http://www.bunkus.org/videotools/mkvtoolnix/downloads.html";
        private string filterTweakerLink = "http://www.codecguide.com/windows7_preferred_filter_tweaker.htm";
        private string anydvdLink = "http://www.slysoft.com/de/anydvdhd.html";
        private string surcodeLink = "http://www.surcode.com/";
        private string dgdecnvLink = "http://neuron2.net/dgdecnv/dgdecnv.html";

        private void linkLabelAviSynth_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start(avisynthLink);
            }
            catch (Exception)
            {
            }
        }

        private void linkLabelHaali_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start(haaliLink);
            }
            catch (Exception)
            {
            }
        }

        private void linkLabelEac3to_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start(eac3toLink);
            }
            catch (Exception)
            {
            }
        }

        private void linkLabelX264_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start(x264Link);
            }
            catch (Exception)
            {
            }
        }

        private void linkLabelBDSup2sub_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start(bdsup2subLink);
            }
            catch (Exception)
            {
            }
        }

        private void linkLabelFFMpegSrc_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start(ffmpegsrcLink);
            }
            catch (Exception)
            {
            }
        }

        private void linkLabelJava_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start(javaLink);
            }
            catch (Exception)
            {
            }
        }

        private void linkLabelMkvtoolnix_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start(mkvtoolnixLink);
            }
            catch (Exception)
            {
            }
        }

        private void buttonMkvmergePath_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog ofd = new OpenFileDialog();
                ofd.Filter = "mkvmerge.exe|mkvmerge.exe";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    textBoxMkvmergePath.Text = ofd.FileName;
                    settings.mkvmergePath = ofd.FileName;
                }
            }
            catch (Exception)
            {
            }
        }

        private void comboBoxX264Priority_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (comboBoxX264Priority.SelectedIndex > -1)
                {
                    settings.x264Priority = (ProcessPriorityClass)Enum.Parse(typeof(ProcessPriorityClass), comboBoxX264Priority.Text);
                }
            }
            catch (Exception)
            {
            }
        }

        private void buttonMinimize_Click(object sender, EventArgs e)
        {
            try
            {
                notifyIconMain.Visible = true;
                this.Hide();
                if (this.Text.Length < 64)
                {
                    notifyIconMain.Text = this.Text;
                }
            }
            catch (Exception)
            {
            }
        }

        private void notifyIconMain_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                this.Show();
                notifyIconMain.Visible = false;
            }
            catch (Exception)
            {
            }
        }

        private void linkLabelFilterTweaker_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start(filterTweakerLink);
            }
            catch (Exception)
            {
            }
        }

        private void buttonTargetfolder_Click(object sender, EventArgs e)
        {
            try
            {
                FolderBrowserDialog fbd = new FolderBrowserDialog();

                try
                {
                    if (settings.targetFolder != "")
                    {
                        DirectoryInfo di = Directory.GetParent(settings.targetFolder);
                        fbd.SelectedPath = di.FullName;
                    }
                }
                catch (Exception)
                {
                }

                if (fbd.ShowDialog() == DialogResult.OK)
                {
                    textBoxTargetFolder.Text = fbd.SelectedPath;
                    settings.targetFolder = fbd.SelectedPath;
                }
            }
            catch (Exception)
            {
            }
        }

        private void textBoxTargetfilename_TextChanged(object sender, EventArgs e)
        {
            try
            {
                settings.targetFilename = textBoxTargetfilename.Text;
            }
            catch (Exception)
            {
            }
        }

        private void textBoxMovieTitle_TextChanged(object sender, EventArgs e)
        {
            try
            {
                settings.movieTitle = textBoxMovieTitle.Text;
            }
            catch (Exception)
            {
            }
        }

        private string getShortLanguage(string language)
        {
            try
            {
                foreach (LanguageInfo li in settings.preferedLanguages)
                {
                    if (li.language == language) return li.languageShort;
                }
                return "";
            }
            catch (Exception)
            {
                return "";
            }
        }

        private bool hasForcedSub(string language)
        {
            try
            {
                foreach (StreamInfo si in demuxedStreamList.streams)
                {
                    if (si.streamType == StreamType.Subtitle && si.language == language)
                    {
                        if (si.extraFileInfo.GetType() == typeof(SubtitleFileInfo))
                        {
                            if (((SubtitleFileInfo)si.extraFileInfo).forcedIdx != "") return true;
                        }
                    }
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private bool muxThreadStatus = false;
        private void MuxThread()
        {
            try
            {
                muxThreadStatus = false;
                int videoStream = 0;
                int audioStream = 0;
                int chapterStream = 0;
                int suptitleStream = 0;

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
                        suptitleStream++;
                    }
                }
                if (videoStream == 0)
                {
                    if (!silent) MessageBox.Show("No videostream or encoded video filename not set", "Error");
                    MessageMux("No videostream or encoded video filename not set");
                    return;
                }
                if (audioStream == 0)
                {
                    if (!silent) MessageBox.Show("No audiostream or audio filename not set", "Error");
                    MessageMux("No audiostream or audio filename not set");
                    return;
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
                        return;
                    }
                }
                if (suptitleStream > 0)
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
                            }                        }
                    }
                    if (error)
                    {
                        if (!silent) MessageBox.Show("Subtitle file(s) not found", "Error");
                        MessageMux("Subtitle file(s) not found");
                        return;
                    }
                }

                sb.Remove(0, sb.Length);
                MessageMux("Starting to mux...");
                MessageMux("");

                pc = new Process();
                pc.StartInfo.FileName = settings.mkvmergePath;
                pc.StartInfo.Arguments = "--title \"" + settings.movieTitle + "\" -o \"" + settings.targetFolder + "\\" + settings.targetFilename + ".mkv\" ";

                // video + chapter
                foreach (StreamInfo si in demuxedStreamList.streams)
                {
                    string lan = "";
                    if (settings.preferedLanguages.Count > 0) lan = settings.preferedLanguages[0].languageShort;

                    if (si.streamType == StreamType.Chapter)
                    {
                        if (lan != "") pc.StartInfo.Arguments += "--chapter-language " + lan + " ";
                        pc.StartInfo.Arguments += "--chapters \"" + si.filename + "\" ";
                    }
                    else if (si.streamType == StreamType.Video)
                    {                        
                        pc.StartInfo.Arguments += "\"" + ((VideoFileInfo)si.extraFileInfo).encodedFile + "\" ";
                    }                    
                }
                // audio
                bool defaultSet = false;
                foreach (StreamInfo si in demuxedStreamList.streams)
                {
                    if (si.streamType == StreamType.Audio)
                    {
                        string st = "";
                        st = getShortLanguage(si.language);
                        if (st != "") pc.StartInfo.Arguments += "--language 0" + ":" + st + " ";
                        if (settings.preferedLanguages.Count > 0 && settings.preferedLanguages[0].language == si.language)
                        {
                            if (!defaultSet)
                            {
                                if (settings.defaultAudio)
                                {
                                    pc.StartInfo.Arguments += "--default-track 0 ";
                                }
                                defaultSet = true;
                            }
                        }
                        pc.StartInfo.Arguments += "\"" + si.filename + "\" ";
                    }
                }

                List<int> subsCount = new List<int>();
                List<int> forcedSubsCount = new List<int>();
                for (int i = 0; i < settings.preferedLanguages.Count; i++)
                {
                    subsCount.Add(0);
                    forcedSubsCount.Add(0);
                }

                if (settings.muxSubs > 0)
                {
                    // subtitle
                    defaultSet = false;
                    foreach (StreamInfo si in demuxedStreamList.streams)
                    {
                        if (si.streamType == StreamType.Subtitle)
                        {
                            SubtitleFileInfo sfi = (SubtitleFileInfo)si.extraFileInfo;

                            bool mux = false;
                            if (settings.muxSubs == 1)
                            {
                                mux = true;
                            }
                            else if (settings.muxSubs == 2)
                            {
                                if (sfi.forcedIdx != "") mux = true;
                            }
                            else if (settings.muxSubs == 3)
                            {
                                int lang = -1;
                                for (int i = 0; i < settings.preferedLanguages.Count; i++)
                                {
                                    if (settings.preferedLanguages[i].language == si.language) lang = i;
                                }
                                if (lang > -1)
                                {
                                    if (sfi.normalIdx != "")
                                    {
                                        if (subsCount[lang] == 0)
                                        {
                                            mux = true;
                                            subsCount[lang]++;
                                        }
                                    }
                                    else if (sfi.forcedIdx != "")
                                    {
                                        if (forcedSubsCount[lang] == 0)
                                        {
                                            mux = true;
                                            forcedSubsCount[lang]++;
                                        }
                                    }
                                }
                            }

                            if (mux)
                            {
                                if (settings.preferedLanguages.Count > 0 && settings.preferedLanguages[0].language == si.language)
                                {
                                    if (!defaultSet)
                                    {
                                        if (settings.defaultSubtitle)
                                        {
                                            string st = "";
                                            st = getShortLanguage(si.language);
                                            if (st != "") pc.StartInfo.Arguments += "--language 0" + ":" + st + " ";

                                            if (!settings.defaultSubtitleForced)
                                            {
                                                pc.StartInfo.Arguments += "--default-track 0 ";
                                                defaultSet = true;
                                            }
                                            else
                                            {
                                                if (hasForcedSub(si.language))
                                                {
                                                    if (sfi.forcedIdx != "")
                                                    {
                                                        pc.StartInfo.Arguments += "--default-track 0 ";
                                                        defaultSet = true;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                                if (!settings.defaultSubtitle)
                                {
                                    pc.StartInfo.Arguments += "--default-track 0:0 ";
                                }
                                if (sfi.normalIdx != "")
                                {
                                    pc.StartInfo.Arguments += "\"" + sfi.normalIdx + "\" ";
                                }
                                else if (sfi.forcedIdx != "")
                                {
                                    pc.StartInfo.Arguments += "\"" + sfi.forcedIdx + "\" ";
                                }
                            }
                        }
                    }
                }

                MessageMux("Command: " + pc.StartInfo.FileName + pc.StartInfo.Arguments);
                
                pc.OutputDataReceived += new DataReceivedEventHandler(OutputDataReceivedMux);

                pc.StartInfo.UseShellExecute = false;
                pc.StartInfo.CreateNoWindow = true;
                pc.StartInfo.RedirectStandardError = true;
                pc.StartInfo.RedirectStandardOutput = true;

                if (!pc.Start())
                {
                    MessageMux("Error starting mkvmerge.exe");
                    return;
                }

                pc.BeginOutputReadLine();

                pc.WaitForExit();
                MessageMux("mkvmerge return code: " + pc.ExitCode.ToString());
                pc.Close();
                MessageMux("Muxing done!");

                for (int i = 0; i < settings.preferedLanguages.Count; i++)
                {
                    subsCount[i] = 0;
                    forcedSubsCount[i] = 0;
                }

                if (settings.copySubs > 0)
                {
                    MessageMux("Trying to copy subtitles...");
                    try
                    {
                        if (!Directory.Exists(settings.targetFolder + "\\Subs"))
                        {
                            Directory.CreateDirectory(settings.targetFolder + "\\Subs");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageMain("Exception: " + ex.Message);
                    }
                    int sub = 1;
                    foreach (StreamInfo si in demuxedStreamList.streams)
                    {
                        if (si.streamType == StreamType.Subtitle)
                        {
                            if (si.extraFileInfo.GetType() == typeof(SubtitleFileInfo))
                            {
                                SubtitleFileInfo sfi = (SubtitleFileInfo)si.extraFileInfo;

                                bool copy = false;
                                if (settings.copySubs == 1)
                                {
                                    copy = true;
                                }
                                else if (settings.copySubs == 2)
                                {
                                    if (sfi.normalIdx != "") copy = true;
                                }
                                else if (settings.copySubs == 3)
                                {
                                    int lang = -1;
                                    for (int i = 0; i < settings.preferedLanguages.Count; i++)
                                    {
                                        if (settings.preferedLanguages[i].language == si.language) lang = i;
                                    }
                                    if (lang > -1)
                                    {
                                        if (sfi.normalIdx != "")
                                        {
                                            if (subsCount[lang] == 0)
                                            {
                                                copy = true;
                                                subsCount[lang]++;
                                            }
                                        }
                                        else if (sfi.forcedIdx != "")
                                        {
                                            if (forcedSubsCount[lang] == 0)
                                            {
                                                copy = true;
                                                forcedSubsCount[lang]++;
                                            }
                                        }
                                    }
                                }


                                if (copy)
                                {
                                    try
                                    {
                                        string target = settings.targetFolder + "\\Subs\\" + settings.targetFilename;
                                        if (sfi.normalIdx != "")
                                        {
                                            File.Copy(sfi.normalIdx, target + "_" + sub.ToString("d2") + "_" + si.language.ToLower() + ".idx", true);
                                            File.Copy(sfi.normalSub, target + "_" + sub.ToString("d2") + "_" + si.language.ToLower() + ".sub", true);
                                        }
                                        else if (sfi.forcedIdx != "")
                                        {
                                            File.Copy(sfi.forcedIdx, target + "_" + sub.ToString("d2") + "_" + si.language.ToLower() + "_forced.idx", true);
                                            File.Copy(sfi.forcedSub, target + "_" + sub.ToString("d2") + "_" + si.language.ToLower() + "_forced.sub", true);
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        MessageMux("Exception: " + ex.Message);
                                    }
                                    sub++;
                                }
                            }
                        }
                    }
                    MessageMux("Done.");
                }
                if (settings.deleteAfterEncode)
                {
                    MessageMux("Deleting source files...");
                    string filename = "";
                    foreach (StreamInfo si in demuxedStreamList.streams)
                    {
                        try
                        {
                            File.Delete(si.filename);
                            if (si.extraFileInfo.GetType() == typeof(VideoFileInfo))
                            {
                                File.Delete(((VideoFileInfo)si.extraFileInfo).encodedFile);
                                File.Delete(((VideoFileInfo)si.extraFileInfo).encodeAvs);
                                filename = si.filename;
                            }
                            if (si.extraFileInfo.GetType() == typeof(SubtitleFileInfo))
                            {
                                SubtitleFileInfo sfi = (SubtitleFileInfo)si.extraFileInfo;
                                if (sfi.forcedIdx != "") File.Delete(sfi.forcedIdx);
                                if (sfi.forcedSub != "") File.Delete(sfi.forcedSub);
                                if (sfi.normalIdx != "") File.Delete(sfi.normalIdx);
                                if (sfi.normalSub != "") File.Delete(sfi.normalSub);
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageMux("Exception: " + ex.Message);
                        }
                    }
                    try
                    {
                        File.Delete(settings.workingDir + "\\" + settings.filePrefix + "_streamInfo.xml");
                        File.Delete(settings.workingDir + "\\" + settings.filePrefix + "_cropTemp.avs");
                    }
                    catch (Exception ex)
                    {
                        MessageMux("Exception: " + ex.Message);
                    }

                    // delete index files
                    try
                    {
                        File.Delete(filename + ".ffindex");
                    }
                    catch (Exception)
                    {
                    }
                    try
                    {
                        string output = Path.ChangeExtension(filename, "dgi");
                        File.Delete(output);
                    }
                    catch (Exception)
                    {
                    }
                    MessageMux("Done.");
                }
                muxThreadStatus = true;
            }
            catch (Exception ex)
            {
                MessageMux("Exception: " + ex.Message);
            }
        }

        private bool DoMux()
        {
            try
            {
                this.Text = title + " [Muxing...]";
                notifyIconMain.Text = this.Text;

                muxThread = new Thread(MuxThread);
                muxThread.Start();

                while (muxThread.IsAlive)
                {
                    Application.DoEvents();
                    Thread.Sleep(5);
                }
                muxThread = null;
                return muxThreadStatus;
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

        private void buttonStreamUp_Click(object sender, EventArgs e)
        {
            try
            {
                int index = listBoxDemuxedStreams.SelectedIndex;
                if (index > 0)
                {
                    StreamInfo si = demuxedStreamList.streams[index];
                    demuxedStreamList.streams.RemoveAt(index);
                    demuxedStreamList.streams.Insert(index - 1, si);
                    UpdateDemuxedStreams();
                    listBoxDemuxedStreams.SelectedIndex = index - 1;
                    TitleInfo.SaveStreamInfoFile(demuxedStreamList, settings.workingDir + "\\" + settings.filePrefix + "_streamInfo.xml");
                }
            }
            catch (Exception)
            {
            }
        }

        private void buttonStreamDown_Click(object sender, EventArgs e)
        {
            try
            {
                int index = listBoxDemuxedStreams.SelectedIndex;
                if (index < demuxedStreamList.streams.Count - 1)
                {
                    StreamInfo si = demuxedStreamList.streams[index];
                    demuxedStreamList.streams.RemoveAt(index);
                    demuxedStreamList.streams.Insert(index + 1, si);
                    UpdateDemuxedStreams();
                    listBoxDemuxedStreams.SelectedIndex = index + 1;
                    TitleInfo.SaveStreamInfoFile(demuxedStreamList, settings.workingDir + "\\" + settings.filePrefix + "_streamInfo.xml");
                }
            }
            catch (Exception)
            {
            }
        }

        private void buttonLangUp_Click(object sender, EventArgs e)
        {
            try
            {
                int index = listBoxPreferedLanguages.SelectedIndex;
                if (index > 0)
                {
                    LanguageInfo li = settings.preferedLanguages[index];
                    settings.preferedLanguages.RemoveAt(index);
                    settings.preferedLanguages.Insert(index - 1, li);
                    UpdateLanguage();
                    listBoxPreferedLanguages.SelectedIndex = index - 1;
                }
            }
            catch (Exception)
            {
            }
        }

        private void buttonLangDown_Click(object sender, EventArgs e)
        {
            try
            {
                int index = listBoxPreferedLanguages.SelectedIndex;
                if (index < settings.preferedLanguages.Count - 1)
                {
                    LanguageInfo li = settings.preferedLanguages[index];
                    settings.preferedLanguages.RemoveAt(index);
                    settings.preferedLanguages.Insert(index + 1, li);
                    UpdateLanguage();
                    listBoxPreferedLanguages.SelectedIndex = index + 1;
                }
            }
            catch (Exception)
            {
            }
        }

        private void checkBoxDefaultAudioTrack_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                settings.defaultAudio = checkBoxDefaultAudioTrack.Checked;
            }
            catch (Exception)
            {
            }
        }

        private void checkBoxDefaultSubtitleTrack_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                settings.defaultSubtitle = checkBoxDefaultSubtitleTrack.Checked;
                if (settings.defaultSubtitle)
                {
                    checkBoxDefaultSubtitleForced.Enabled = true;
                }
                else
                {
                    checkBoxDefaultSubtitleForced.Enabled = false;
                }
            }
            catch (Exception)
            {
            }
        }

        private void checkBoxDefaultSubtitleForced_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                settings.defaultSubtitleForced = checkBoxDefaultSubtitleForced.Checked;
            }
            catch (Exception)
            {
            }
        }

        private void comboBoxEncodeProfile_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                settings.lastProfile = comboBoxEncodeProfile.SelectedIndex;
            }
            catch (Exception)
            {
            }
        }

        private void checkBoxDeleteAfterEncode_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                settings.deleteAfterEncode = checkBoxDeleteAfterEncode.Checked;
            }
            catch (Exception)
            {
            }
        }

        private void linkLabelAnyDvd_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start(anydvdLink);
            }
            catch (Exception)
            {
            }
        }

        private void comboBoxCropMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (comboBoxCropMode.SelectedIndex > -1)
                {
                    settings.cropMode = comboBoxCropMode.SelectedIndex;
                }
            }
            catch (Exception)
            {
            }
        }

        private void clearLogToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (tabControlLog.SelectedTab == tabPageMainLog)
                {
                    richTextBoxLogMain.Clear();
                }
                else if (tabControlLog.SelectedTab == tabPageDemuxLog)
                {
                    richTextBoxLogDemux.Clear();
                }
                if (tabControlLog.SelectedTab == tabPageCropLog)
                {
                    richTextBoxLogCrop.Clear();
                }
                if (tabControlLog.SelectedTab == tabPageSubtitleLog)
                {
                    richTextBoxLogSubtitle.Clear();
                }
                if (tabControlLog.SelectedTab == tabPageEncodeLog)
                {
                    richTextBoxLogEncode.Clear();
                }
                if (tabControlLog.SelectedTab == tabPageMuxLog)
                {
                    richTextBoxLogMux.Clear();
                }
            }
            catch (Exception)
            {
            }
        }

        private void clearAllLogsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                richTextBoxLogMain.Clear();
                richTextBoxLogDemux.Clear();
                richTextBoxLogCrop.Clear();
                richTextBoxLogSubtitle.Clear();
                richTextBoxLogEncode.Clear();
                richTextBoxLogMux.Clear();
            }
            catch (Exception)
            {
            }
        }

        private void checkBoxUseCore_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                settings.dtsHdCore = checkBoxUseCore.Checked;
            }
            catch (Exception)
            {
            }
        }

        private List<string> m2tsList = new List<string>();

        private void buttonOpenM2ts_Click(object sender, EventArgs e)
        {
            try
            {
                if (!checkEac3to()) return;
                FileListForm flf = new FileListForm();
                if (flf.ShowDialog() == DialogResult.OK)
                {
                    comboBoxTitle.Items.Clear();
                    listBoxStreams.Items.Clear();

                    titleList.Clear();
                    m2tsList.Clear();
                    foreach (string s in flf.fileList)
                    {
                        m2tsList.Add(s);
                    }
                    DoM2tsInfo();
                }
            }
            catch (Exception)
            {
            }
        }

        private void M2tsInfoThread()
        {
            try
            {
                GetM2tsInfo(titleList);
            }
            catch (Exception)
            {
            }
        }

        private void DoM2tsInfo()
        {
            try
            {
                this.Text = title + " [Getting m2ts stream info...]";

                progressBarMain.Visible = true;
                buttonAbort.Visible = true;

                comboBoxTitle.Items.Clear();
                listBoxStreams.Items.Clear();

                titleInfoThread = new Thread(M2tsInfoThread);
                titleInfoThread.Start();

                while (titleInfoThread.IsAlive)
                {
                    Application.DoEvents();
                    Thread.Sleep(5);
                }
                titleInfoThread = null;

                UpdateTitleList();

                demuxedStreamList = new TitleInfo();
                UpdateDemuxedStreams();
            }
            catch (Exception ex)
            {
                MessageDemux("Exception: " + ex.Message);
                if (titleInfoThread != null) titleInfoThread = null;
            }
            finally
            {
                progressBarMain.Visible = false;
                buttonAbort.Visible = false;
                this.Text = title;
            }
        }

        private void GetM2tsInfo(List<TitleInfo> result)
        {
            try
            {                
                sb.Remove(0, sb.Length);
                pc2 = new Process();
                pc2.StartInfo.FileName = settings.eac3toPath;
                string tmpstr = "";
                foreach (string s in m2tsList)
                {
                    tmpstr += "\"" + s + "\"+";
                }
                if (tmpstr.Length > 0)
                {
                    if (tmpstr[tmpstr.Length - 1] == '+')
                    {
                        tmpstr = tmpstr.Substring(0, tmpstr.Length - 1);
                    }
                }
                pc2.StartInfo.Arguments = tmpstr;

                pc2.OutputDataReceived += new DataReceivedEventHandler(OutputDataReceivedDemux);
                pc2.ErrorDataReceived += new DataReceivedEventHandler(OutputDataReceivedDemux);
                pc2.StartInfo.UseShellExecute = false;
                pc2.StartInfo.CreateNoWindow = true;
                pc2.StartInfo.RedirectStandardError = true;
                pc2.StartInfo.RedirectStandardOutput = true;

                MessageDemux("Command: " + pc2.StartInfo.FileName + pc2.StartInfo.Arguments);

                if (!pc2.Start())
                {
                    MessageDemux("Error starting eac3to.exe");
                    return;
                }

                string res = "";
                pc2.BeginOutputReadLine();
                pc2.BeginErrorReadLine();
                pc2.WaitForExit();
                pc2.Close();
                res = sb.ToString();
                res = res.Replace("\b", "");

                string[] tmp = res.Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
                for (int i = 0; i < tmp.Length; i++)
                {
                    tmp[i] = tmp[i].Trim();
                }

                if (res.Trim() == "")
                {
                    MessageDemux("Failed to get stream infos");
                    return;
                }

                TitleInfo ti = new TitleInfo();

                if (tmp[0][0] == '-')
                {
                    int length = 0;
                    for (int i = 0; i < tmp[0].Length; i++)
                    {
                        if (tmp[0][i] == '-')
                        {
                            length++;
                        }
                        else
                        {
                            break;
                        }
                    }
                    tmp[0] = tmp[0].Substring(length, tmp[0].Length - length);
                    tmp[0] = tmp[0].Trim();
                    ti.desc = tmp[0];
                }

                for (int i = 0; i < tmp.Length; i++)
                {
                    if (Regex.IsMatch(tmp[i], "^[0-9].*:"))
                    {
                        StreamInfo sr = new StreamInfo();
                        sr.desc = tmp[i];
                        if (i < tmp.Length - 1)
                        {
                            if (!Regex.IsMatch(tmp[i + 1], "^[0-9].*:"))
                            {
                                sr.addInfo = tmp[i + 1];

                            }
                        }

                        int pos = tmp[i].IndexOf(':');
                        string substr = tmp[i].Substring(0, pos);
                        sr.number = Convert.ToInt32(substr);

                        substr = tmp[i].Substring(pos + 1).Trim();
                        string[] tmpInfo = substr.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                        if (tmpInfo.Length > 0)
                        {
                            sr.typeDesc = tmpInfo[0];
                            if (tmpInfo[0] == "Chapters")
                            {
                                sr.streamType = StreamType.Chapter;
                            }
                            else if (videoTypes.Contains(tmpInfo[0]))
                            {
                                sr.streamType = StreamType.Video;
                            }
                            else if (ac3AudioTypes.Contains(tmpInfo[0]))
                            {
                                sr.streamType = StreamType.Audio;
                                if (tmpInfo.Length > 1)
                                {
                                    sr.language = tmpInfo[1].Trim();
                                }
                            }
                            else if (dtsAudioTypes.Contains(tmpInfo[0]))
                            {
                                sr.streamType = StreamType.Audio;
                                if (tmpInfo.Length > 1)
                                {
                                    sr.language = tmpInfo[1].Trim();
                                }
                            }
                            else if (tmpInfo[0] == "Subtitle (PGS)")
                            {
                                sr.streamType = StreamType.Subtitle;
                                if (tmpInfo.Length > 1)
                                {
                                    sr.language = tmpInfo[1].Trim();
                                }
                            }
                            else
                            {
                                sr.streamType = StreamType.Unknown;
                            }
                        }
                        else
                        {
                            sr.typeDesc = "Unknown";
                            sr.streamType = StreamType.Unknown;
                        }

                        ti.streams.Add(sr);
                    }
                }

                MessageDemux("Done.");

                result.Add(ti);
            }
            catch (Exception ex)
            {
                MessageDemux("Exception: " + ex.Message);
            }
        }        

        private void checkBoxUntouchedVideo_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                settings.untouchedVideo = checkBoxUntouchedVideo.Checked;
                if (settings.untouchedVideo)
                {
                    checkBoxResize720p.Checked = false;
                    checkBoxResize720p.Enabled = false;
                }
                else
                {
                    checkBoxResize720p.Enabled = true;
                }
            }
            catch (Exception)
            {
            }
        }

        private void buttonAddAvisynth_Click(object sender, EventArgs e)
        {
            try
            {
                AvisynthSettings avs = new AvisynthSettings("Empty", "");
                settings.avisynthSettings.Add(avs);
                UpdateAvisynthSettings();
            }
            catch (Exception)
            {
            }
        }

        private void buttonDelAvisynth_Click(object sender, EventArgs e)
        {
            try
            {
                int index = listBoxAviSynthProfiles.SelectedIndex;
                if (index > -1)
                {
                    settings.avisynthSettings.RemoveAt(index);
                    UpdateAvisynthSettings();
                }
            }
            catch (Exception)
            {
            }
        }

        private void listBoxAviSynthProfiles_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                int index = listBoxAviSynthProfiles.SelectedIndex;
                if (index > -1)
                {
                    AvisynthSettingsForm avsf = new AvisynthSettingsForm(settings.avisynthSettings[index]);
                    if (avsf.ShowDialog() == DialogResult.OK)
                    {
                        settings.avisynthSettings[index] = new AvisynthSettings(avsf.avs);
                        UpdateAvisynthSettings();
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        private void checkBoxResize720p_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                settings.resize720p = checkBoxResize720p.Checked;
            }
            catch (Exception)
            {
            }
        }

        private bool checkEac3to()
        {
            try
            {
                if (!File.Exists(settings.eac3toPath))
                {
                    MessageMain("eac3to path not set");
                    if(!silent) MessageBox.Show("eac3to path not set", "Error");
                    return false;
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private bool checkIndex()
        {
            try
            {
                if (settings.cropInput == 1 || settings.encodeInput == 1)
                {
                    if (!File.Exists(settings.ffmsindexPath))
                    {
                        MessageMain("ffmsindex path not set");
                        if (!silent) MessageBox.Show("ffmsindex path not set", "Error");
                        return false;
                    }
                }
                else if (settings.cropInput == 2 || settings.encodeInput == 2)
                {
                    if (!File.Exists(settings.dgindexnvPath))
                    {
                        MessageMain("DGIndexNv path not set");
                        if (!silent) MessageBox.Show("DGIndexNv path not set", "Error");
                        return false;
                    }
                    if (!File.Exists(settings.cuvidserverPath))
                    {
                        MessageMain("CUVIDServer path not set");
                        if (!silent) MessageBox.Show("CUVIDServer path not set", "Error");
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

        private bool checkBdsup2sub()
        {
            try
            {
                if (!File.Exists(settings.javaPath))
                {
                    MessageMain("java path not set");
                    if (!silent) MessageBox.Show("java path not set", "Error");
                    return false;
                }
                if (!File.Exists(settings.sup2subPath))
                {
                    MessageMain("BDsup2sub path not set");
                    if (!silent) MessageBox.Show("BDsup2sub path not set", "Error");
                    return false;
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private bool checkX264()
        {
            try
            {
                if (!File.Exists(settings.x264Path))
                {
                    MessageMain("x264 path not set");
                    if (!silent) MessageBox.Show("x264 path not set", "Error");
                    return false;
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private bool checkMkvmerge()
        {
            try
            {
                if (!File.Exists(settings.mkvmergePath))
                {
                    MessageMain("mkvmerge path not set");
                    if (!silent) MessageBox.Show("mkvmerge path not set", "Error");
                    return false;
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private bool checkComplete()
        {
            try
            {
                if (!checkEac3to()) return false;
                int sup = 0;
                if (comboBoxTitle.SelectedIndex > -1)
                {
                    foreach (StreamInfo si in titleList[comboBoxTitle.SelectedIndex].streams)
                    {
                        if (si.streamType == StreamType.Subtitle)
                        {
                            sup++;
                        }
                    }
                }
                if (!checkIndex()) return false;
                if (sup > 0)
                {
                    if (!checkBdsup2sub()) return false;
                }
                if (!settings.untouchedVideo)
                {
                    if (!checkX264()) return false;
                }
                if (!checkMkvmerge()) return false;
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private void checkBoxDownmixDts_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                settings.downmixDTS = checkBoxDownmixDts.Checked;
                if (settings.downmixDTS)
                {
                    comboBoxDownmixDts.Enabled = true;
                }
                else
                {
                    comboBoxDownmixDts.Enabled = false;
                }
            }
            catch (Exception)
            {
            }
        }

        private void checkBoxDownmixAc3_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                settings.downmixAc3 = checkBoxDownmixAc3.Checked;
                if (settings.downmixAc3)
                {
                    comboBoxDownmixAc3.Enabled = true;
                }
                else
                {
                    comboBoxDownmixAc3.Enabled = false;
                }
            }
            catch (Exception)
            {
            }
        }

        private void comboBoxDownmixDts_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (comboBoxDownmixDts.SelectedIndex > -1) settings.downmixDTSIndex = comboBoxDownmixDts.SelectedIndex;
            }
            catch (Exception)
            {
            }
        }

        private void comboBoxDownmixAc3_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (comboBoxDownmixAc3.SelectedIndex > -1) settings.downmixAc3Index = comboBoxDownmixAc3.SelectedIndex;
            }
            catch (Exception)
            {
            }
        }

        private void linkLabelSurcode_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start(surcodeLink);
            }
            catch (Exception)
            {
            }
        }

        private void SaveLog(string log, string filename)
        {
            try
            {
                string[] lines = log.Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
                string tmp = "";
                foreach (string s in lines) tmp += s.Trim() + "\r\n";
                File.WriteAllText(filename, tmp);
            }
            catch (Exception)
            {
            }
        }

        private void SaveLog(string log)
        {
            try
            {
                string[] lines = log.Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
                string tmp = "";
                foreach(string s in lines) tmp += s.Trim() + "\r\n";
                SaveFileDialog sfd = new SaveFileDialog();                
                sfd.Filter = "Log file|*.txt";
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    File.WriteAllText(sfd.FileName, tmp);
                }
            }
            catch (Exception)
            {
            }
        }

        private void saveLogToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (tabControlLog.SelectedTab == tabPageMainLog)
                {   
                    SaveLog(richTextBoxLogMain.Text);
                }
                else if (tabControlLog.SelectedTab == tabPageDemuxLog)
                {
                    SaveLog(richTextBoxLogDemux.Text);
                }
                if (tabControlLog.SelectedTab == tabPageCropLog)
                {
                    SaveLog(richTextBoxLogCrop.Text);
                }
                if (tabControlLog.SelectedTab == tabPageSubtitleLog)
                {
                    SaveLog(richTextBoxLogSubtitle.Text);
                }
                if (tabControlLog.SelectedTab == tabPageEncodeLog)
                {
                    SaveLog(richTextBoxLogEncode.Text);
                }
                if (tabControlLog.SelectedTab == tabPageMuxLog)
                {
                    SaveLog(richTextBoxLogMux.Text);
                }
            }
            catch (Exception)
            {
            }
        }

        private void checkBoxMinimizeCrop_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                settings.minimizeAutocrop = checkBoxMinimizeCrop.Checked;
            }
            catch (Exception)
            {
            }
        }

        private void buttonSaveProject_Click(object sender, EventArgs e)
        {
            try
            {
                if (demuxedStreamList.streams.Count == 0 && titleList.Count == 0)
                {
                    if (MessageBox.Show("Demuxed stream list/title list empty - continue anyway?", "Stream list empty", MessageBoxButtons.YesNo) == DialogResult.No)
                    {
                        return;
                    }
                }

                Project project = new Project(settings, demuxedStreamList, titleList, comboBoxTitle.SelectedIndex, m2tsList);
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = "BluRip project (*.brp)|*.brp";
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    Project.SaveProjectFile(project, sfd.FileName);
                }
            }
            catch (Exception)
            {
            }
        }

        private void buttonLoadProject_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog ofd = new OpenFileDialog();
                ofd.Filter = "BluRip project (*.brp)|*.brp";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    Project project = new Project();
                    if (Project.LoadProjectFile(ref project, ofd.FileName))
                    {
                        settings = new UserSettings(project.settings);                        
                        titleList.Clear();
                        foreach (TitleInfo ti in project.titleList)
                        {
                            titleList.Add(new TitleInfo(ti));
                        }
                        UpdateTitleList();
                        demuxedStreamList = new TitleInfo(project.demuxedStreamList);

                        m2tsList.Clear();
                        foreach (string s in project.m2tsList)
                        {
                            m2tsList.Add(s);
                        }

                        UpdateFromSettings();
                        UpdateDemuxedStreams();                        
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }

        private List<Project> projectQueue = new List<Project>();

        private void UpdateQueue()
        {
            try
            {
                listBoxQueue.Items.Clear();
                foreach (Project p in projectQueue)
                {
                    listBoxQueue.Items.Add("Title: " + p.settings.movieTitle);
                }
            }
            catch (Exception)
            {
            }
        }

        private void buttonQueueCurrent_Click(object sender, EventArgs e)
        {
            try
            {
                Project p = new Project(settings, demuxedStreamList, titleList, comboBoxTitle.SelectedIndex, m2tsList);
                projectQueue.Add(p);
                UpdateQueue();
            }
            catch (Exception)
            {
            }
        }

        private void buttonQueueDel_Click(object sender, EventArgs e)
        {
            try
            {
                int index = listBoxQueue.SelectedIndex;
                if (index > -1)
                {
                    projectQueue.RemoveAt(index);
                    UpdateQueue();
                }
            }
            catch (Exception)
            {
            }
        }

        private void buttonQueueUp_Click(object sender, EventArgs e)
        {
            try
            {
                int index = listBoxQueue.SelectedIndex;
                if (index > 0)
                {
                    Project p = projectQueue[index];
                    projectQueue.RemoveAt(index);
                    projectQueue.Insert(index - 1, p);
                    UpdateQueue();
                    listBoxQueue.SelectedIndex = index - 1;
                }
            }
            catch (Exception)
            {
            }
        }

        private void buttonQueueDown_Click(object sender, EventArgs e)
        {
            try
            {
                int index = listBoxQueue.SelectedIndex;
                if (index > -1 && index < projectQueue.Count - 1)
                {
                    Project p = projectQueue[index];
                    projectQueue.RemoveAt(index);
                    projectQueue.Insert(index + 1, p);
                    UpdateQueue();
                    listBoxQueue.SelectedIndex = index + 1;
                }
            }
            catch (Exception)
            {
            }
        }

        private void buttonQueueExisting_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog ofd = new OpenFileDialog();
                ofd.Filter = "BluRip project (*.brp)|*.brp";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    Project project = new Project();
                    if (Project.LoadProjectFile(ref project, ofd.FileName))
                    {
                        projectQueue.Add(project);
                        UpdateQueue();
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }

        private bool silent = false;
        private bool abort = false;

        private void buttonProcessQueue_Click(object sender, EventArgs e)
        {
            try
            {
                silent = true;
                abort = false;
                
                foreach (Project project in projectQueue)
                {
                    richTextBoxLogCrop.Clear();
                    richTextBoxLogDemux.Clear();
                    richTextBoxLogEncode.Clear();
                    richTextBoxLogMain.Clear();
                    richTextBoxLogMux.Clear();
                    richTextBoxLogSubtitle.Clear();

                    MessageMain("Starting to process job " + (projectQueue.IndexOf(project) + 1).ToString() + "/" + projectQueue.Count.ToString());

                    if (!abort)
                    {
                        MessageMain("Processing project " + project.settings.movieTitle);
                        settings = new UserSettings(project.settings);
                        titleList.Clear();
                        foreach (TitleInfo ti in project.titleList)
                        {
                            titleList.Add(new TitleInfo(ti));
                        }
                        UpdateTitleList();
                        demuxedStreamList = new TitleInfo(project.demuxedStreamList);

                        m2tsList.Clear();
                        foreach (string s in project.m2tsList)
                        {
                            m2tsList.Add(s);
                        }

                        UpdateFromSettings();
                        UpdateDemuxedStreams();

                        buttonStartConvert_Click(null, null);
                    }
                    else
                    {
                        MessageMain("Queue canceled");
                    }
                    MessageMain("Job done.");
                }
                if (checkBoxShutDown.Checked)
                {
                    System.Diagnostics.Process.Start("ShutDown", "-s -f");
                }
            }
            catch (Exception)
            {
            }
            finally
            {
                silent = false;
            }
        }

        private void comboBoxCropInput_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                settings.cropInput = comboBoxCropInput.SelectedIndex;
            }
            catch (Exception)
            {
            }
        }

        private void comboBoxEncodeInput_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                settings.encodeInput = comboBoxEncodeInput.SelectedIndex;
            }
            catch (Exception)
            {
            }
        }

        private void checkBoxUntouchedAudio_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                settings.untouchedAudio = checkBoxUntouchedAudio.Checked;
                if (settings.untouchedAudio)
                {
                    checkBoxDownmixAc3.Checked = false;
                    checkBoxDownmixDts.Checked = false;
                    checkBoxUseCore.Checked = false;
                    checkBoxDtsToAc3.Checked = false;

                    checkBoxUseCore.Enabled = false;
                    checkBoxDownmixAc3.Enabled = false;
                    checkBoxDownmixDts.Enabled = false;
                    checkBoxDtsToAc3.Enabled = false;
                }
                else
                {
                    checkBoxUseCore.Enabled = true;
                    checkBoxDownmixAc3.Enabled = true;
                    checkBoxDownmixDts.Enabled = true;
                    checkBoxDtsToAc3.Enabled = true;
                    checkBoxDownmixDts_CheckedChanged(null, null);
                    checkBoxDownmixAc3_CheckedChanged(null, null);
                }
            }
            catch (Exception)
            {
            }
        }

        private void comboBoxMuxSubs_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                settings.muxSubs = comboBoxMuxSubs.SelectedIndex;
            }
            catch (Exception)
            {
            }
        }

        private void comboBoxCopySubs_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                settings.copySubs = comboBoxCopySubs.SelectedIndex;
            }
            catch (Exception)
            {
            }
        }

        private void linkLabelDGDecNv_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start(dgdecnvLink);
            }
            catch (Exception)
            {
            }
        }

        private void buttonDgindexnvPath_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog ofd = new OpenFileDialog();
                ofd.Filter = "DGIndexNv.exe|DGIndexNv.exe";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    textBoxDgindexnvPath.Text = ofd.FileName;
                    settings.dgindexnvPath = ofd.FileName;
                }
            }
            catch (Exception)
            {
            }
        }

        private void checkBoxDtsToAc3_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                settings.convertDtsToAc3 = checkBoxDtsToAc3.Checked;
            }
            catch (Exception)
            {
            }
        }
    }
}
