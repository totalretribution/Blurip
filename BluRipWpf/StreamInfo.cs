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
        private StreamInfoTool sit = null;
        private M2tsInfoTool mit = null;

        private List<TitleInfo> titleList = new List<TitleInfo>();
        private List<string> m2tsList = new List<string>();
        private TitleInfo demuxedStreamList = new TitleInfo();

        public TitleInfo DemuxedStreams
        {
            get { return demuxedStreamList; }
            set { demuxedStreamList = value; }
        }

        private void DemuxMsg(object sender, ExternalTool.MsgEventArgs e)
        {
            logWindow.MessageDemux(e.Message.Replace("\b", "").Trim());
        }

        private void UpdateTitleList()
        {
            try
            {
                comboBoxTitle.Items.Clear();
                listBoxStreams.ItemsSource = null;

                for(int i=0; i < titleList.Count; i++)
                //foreach (TitleInfo ti in titleList)
                {
                    comboBoxTitle.Items.Add((i+1).ToString() + ") " + titleList[i].desc);
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

        private bool checkEac3to()
        {
            try
            {
                if (!File.Exists(settings.eac3toPath))
                {
                    logWindow.MessageMain(Global.Res("ErrorEac3toPath"));
                    if (!silent) Global.ErrorMsg(Global.Res("ErrorEac3toPath"));
                    return false;
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private void buttonGetStreamInfo_Click(object sender, RoutedEventArgs e)
        {
            try
            {                
                if (!Directory.Exists(settings.lastBluRayPath))
                {
                    logWindow.MessageDemux(Global.Res("ErrorBlurayPath"));
                    if (!silent) Global.ErrorMsg(Global.Res("ErrorBlurayPath"));
                    return;
                }
                if (!checkEac3to()) return;
                DisableControls();
                UpdateStatus(Global.Res("StatusBar") + " " + Global.Res("StatusBarStreamInfo"));
                
                comboBoxTitle.Items.Clear();
                listBoxStreams.ItemsSource = null;
                m2tsList.Clear();

                sit = new StreamInfoTool(settings, ref titleList, settings.lastBluRayPath, GlobalVars.videoTypes, GlobalVars.ac3AudioTypes, GlobalVars.dtsAudioTypes);
                sit.OnInfoMsg += new ExternalTool.InfoEventHandler(DemuxMsg);
                sit.OnLogMsg += new ExternalTool.LogEventHandler(DemuxMsg);
                sit.Start();
                sit.WaitForExit();
                if (sit == null || !sit.Successfull)
                {
                    titleList.Clear();
                }

                UpdateTitleList();

                demuxedStreamList = new TitleInfo();
                demuxedStreamsWindow.UpdateDemuxedStreams();
            }
            catch (Exception ex)
            {
                logWindow.MessageDemux(Global.Res("ErrorException") + " " + ex.Message);
            }
            finally
            {
                EnableControls();

                UpdateStatus(Global.Res("StatusBar") + " " + Global.Res("StatusBarReady"));
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

        private bool HasAudioLanguage(string s)
        {
            foreach (LanguageInfo li in settings.preferredAudioLanguages)
            {
                if (li.language == s) return true;
            }
            return false;
        }

        private bool HasSubLanguage(string s)
        {
            foreach (LanguageInfo li in settings.preferredSubtitleLanguages)
            {
                if (li.language == s) return true;
            }
            return false;
        }

        private int AudioLanguageIndex(string s)
        {
            int index = -1;
            for (int i = 0; i < settings.preferredAudioLanguages.Count; i++)
            {
                if (settings.preferredAudioLanguages[i].language == s)
                {
                    index = i;
                    break;
                }
            }
            return index;
        }

        private int SubLanguageIndex(string s)
        {
            int index = -1;
            for (int i = 0; i < settings.preferredSubtitleLanguages.Count; i++)
            {
                if (settings.preferredSubtitleLanguages[i].language == s)
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
                if (comboBoxTitle.SelectedIndex == -1) return;
                int maxLength = 0;
                int selectionCount = 0;
                foreach (StreamInfo si in titleList[comboBoxTitle.SelectedIndex].streams)
                {
                    maxLength = Math.Max(maxLength, StreamTypeToString(si.streamType).Length);
                    if (si.selected) selectionCount++;
                }

                List<int> maxac3List = new List<int>();
                List<int> maxdtsList = new List<int>();

                for (int i = 0; i < settings.preferredAudioLanguages.Count; i++)
                {
                    maxac3List.Add(0);
                    maxdtsList.Add(0);
                }
                foreach (StreamInfo si in titleList[comboBoxTitle.SelectedIndex].streams)
                {
                    si.maxLength = maxLength;
                }

                int videoCount = 0;
                int chapterCount = 0;

                if (settings.useAutoSelect && selectionCount == 0)
                {
                    List<int> ac3List = new List<int>();
                    List<int> dtsList = new List<int>();

                    for (int i = 0; i < settings.preferredAudioLanguages.Count; i++)
                    {
                        ac3List.Add(0);
                        dtsList.Add(0);
                    }
                    foreach (StreamInfo si in titleList[comboBoxTitle.SelectedIndex].streams)
                    {
                        if (si.streamType == StreamType.Audio)
                        {
                            if (HasAudioLanguage(si.language))
                            {
                                int index = AudioLanguageIndex(si.language);
                                if (GlobalVars.dtsAudioTypes.Contains(si.typeDesc))
                                {
                                    maxdtsList[index]++;
                                }
                                if (GlobalVars.ac3AudioTypes.Contains(si.typeDesc))
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
                                if (HasSubLanguage(si.language))
                                {
                                    si.selected = true;
                                }
                            }
                        }
                        if (si.streamType == StreamType.Audio)
                        {
                            if (HasAudioLanguage(si.language))
                            {
                                int index = AudioLanguageIndex(si.language);
                                if (settings.preferDTS)
                                {
                                    if (GlobalVars.dtsAudioTypes.Contains(si.typeDesc))
                                    {
                                        if (dtsList[index] == 0)
                                        {
                                            dtsList[index]++;
                                            si.selected = true;
                                        }
                                    }
                                    if (GlobalVars.ac3AudioTypes.Contains(si.typeDesc) && maxdtsList[index] == 0)
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
                                    if (GlobalVars.ac3AudioTypes.Contains(si.typeDesc))
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
                                    if (GlobalVars.dtsAudioTypes.Contains(si.typeDesc) && maxac3List[index] == 0)
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
                            // only autoselect first video stream (3d discs!)
                            if (si.desc.Contains("1080") && videoCount < 1)
                            {
                                si.selected = true;
                                videoCount++;
                            }
                        }
                    }
                }

                listBoxStreams.ItemsSource = null;
                listBoxStreams.ItemsSource = titleList[comboBoxTitle.SelectedIndex].streams;
                listBoxStreams.Items.Refresh();                
            }
            catch (Exception)
            {
            }
        }

        private void comboBoxTitle_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (comboBoxTitle.SelectedIndex > -1)
                {
                    listBoxStreams.ItemsSource = null;
                    UpdateStreamList();
                }
            }
            catch (Exception)
            {
            }
        }

        private void buttonOpenM2ts_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!checkEac3to()) return;
                FileListWindow flw = new FileListWindow();
                flw.ShowDialog();
                if (flw.DialogResult == true)
                {
                    comboBoxTitle.Items.Clear();
                    listBoxStreams.ItemsSource = null;

                    titleList.Clear();
                    m2tsList.Clear();
                    foreach (string s in flw.fileList)
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

        private void DoM2tsInfo()
        {
            try
            {
                DisableControls();
                UpdateStatus(Global.Res("StatusBar") + " " + Global.Res("StatusBarM2tsInfo"));
                                
                comboBoxTitle.Items.Clear();
                listBoxStreams.ItemsSource = null;

                mit = new M2tsInfoTool(settings, ref titleList, m2tsList, GlobalVars.videoTypes, GlobalVars.ac3AudioTypes, GlobalVars.dtsAudioTypes);
                mit.OnInfoMsg += new ExternalTool.InfoEventHandler(DemuxMsg);
                mit.OnLogMsg += new ExternalTool.LogEventHandler(DemuxMsg);
                mit.Start();
                mit.WaitForExit();
                if (mit == null || !mit.Successfull)
                {
                    titleList.Clear();
                }

                UpdateTitleList();

                demuxedStreamList = new TitleInfo();
                demuxedStreamsWindow.UpdateDemuxedStreams();
            }
            catch (Exception ex)
            {
                logWindow.MessageDemux(Global.Res("ErrorException") + " " + ex.Message);
            }
            finally
            {
                EnableControls();

                UpdateStatus(Global.Res("StatusBar") + " " + Global.Res("StatusBarReady"));
            }
        }

        private void buttonBluRayPath_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                FolderBrowserDialog fbd = new FolderBrowserDialog();
                if (fbd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    textBoxBlurayPath.Text = fbd.SelectedPath;
                    settings.lastBluRayPath = fbd.SelectedPath;
                }
            }
            catch (Exception)
            {
            }
        }

        private void listBoxStreams_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                int index = listBoxStreams.SelectedIndex;
                if (index > -1)
                {
                    titleList[comboBoxTitle.SelectedIndex].streams[index].selected = !titleList[comboBoxTitle.SelectedIndex].streams[index].selected;
                    listBoxStreams.Items.Refresh();
                }
            }
            catch (Exception)
            {
            }
        }
    }
}