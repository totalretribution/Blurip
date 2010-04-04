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
using System.IO;
using System.Xml.Serialization;
using System.Text.RegularExpressions;

namespace BluRip
{
    public class DemuxTool : ExternalTool
    {
        private UserSettings settings = null;
        private List<string> m2tsFiles = null;
        private List<string> videoTypes = null;
        private List<string> ac3AudioTypes = null;
        private List<string> dtsAudioTypes = null;
        private TitleInfo streamList = null;
        private TitleInfo demuxedStreamList = null;
        private string ac3Bitrate = "";
        private string dtsBitrate = "";

        public DemuxTool(UserSettings settings, List<string> m2tsFiles, List<string> videoTypes, List<string> ac3AudioTypes, 
            List<string> dtsAudioTypes, TitleInfo streamList, ref TitleInfo demuxedStreamList, string ac3Bitrate, string dtsBitrate)
            : base()
        {
            try
            {
                this.settings = settings;
                this.m2tsFiles = m2tsFiles;
                this.videoTypes = videoTypes;
                this.ac3AudioTypes = ac3AudioTypes;
                this.dtsAudioTypes = dtsAudioTypes;
                this.streamList = streamList;
                demuxedStreamList = new TitleInfo();
                this.demuxedStreamList = demuxedStreamList;
                this.ac3Bitrate = ac3Bitrate;
                this.dtsBitrate = dtsBitrate;
                this.Path = settings.eac3toPath;

                Init();
            }
            catch (Exception)
            {
            }
        }

        protected override void StartInfo()
        {
            Info("Starting to demux");            
            Info("Processing " + streamList.desc);
            Info("");
        }

        protected override void EndInfo()
        {
            Info("Done.");
        }

        protected override bool StartProcessing()
        {
            if (Ok && ExitCode == 0) return true;
            else return false;
        }

        private void Init()
        {
            try
            {   
                if (m2tsFiles.Count == 0)
                {
                    Parameter = "\"" + settings.lastBluRayPath + "\" " + streamList.streamNumber + " ";
                }
                else
                {
                    string tmpstr = "";
                    foreach (string s in m2tsFiles)
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
                    Parameter = tmpstr + " ";
                }

                string prefix = settings.filePrefix;

                foreach (StreamInfo si in streamList.streams)
                {
                    if (si.selected && si.streamType != StreamType.Unknown)
                    {
                        Parameter += si.number.ToString() + ": \"" + settings.workingDir + "\\" + prefix + "_" + si.number.ToString("d3") + "_";
                        if (si.streamType == StreamType.Chapter)
                        {
                            Parameter += "chapter.txt\" ";                            
                            si.filename = settings.workingDir + "\\" + prefix + "_" + si.number.ToString("d3") + "_chapter.txt";
                        }
                        else if (si.streamType == StreamType.Audio)
                        {
                            if (si.advancedOptions != null && si.advancedOptions.GetType() == typeof(AdvancedAudioOptions))
                            {
                                Parameter += "audio_custom_" + si.language + ((AdvancedAudioOptions)si.advancedOptions).extension + "\" ";
                                if (((AdvancedAudioOptions)si.advancedOptions).parameter != "")
                                {
                                    Parameter += ((AdvancedAudioOptions)si.advancedOptions).parameter + " ";
                                }
                                if (((AdvancedAudioOptions)si.advancedOptions).bitrate != "")
                                {
                                    Parameter += "-" + ((AdvancedAudioOptions)si.advancedOptions).bitrate + " ";
                                }
                                si.filename = settings.workingDir + "\\" + prefix + "_" + si.number.ToString("d3") + "_audio_custom_" + si.language + ((AdvancedAudioOptions)si.advancedOptions).extension;
                                if (((AdvancedAudioOptions)si.advancedOptions).additionalAc3Track)
                                {
                                    Parameter += si.number.ToString() + ": \"" + settings.workingDir + "\\" + prefix + "_" + si.number.ToString("d3") + "_";
                                    Parameter += "audio_additionalAc3_" + si.language + ".ac3\" ";
                                    ((AdvancedAudioOptions)si.advancedOptions).additionalFilename = settings.workingDir + "\\" + prefix + "_" + si.number.ToString("d3") + "_";
                                    ((AdvancedAudioOptions)si.advancedOptions).additionalFilename += "audio_additionalAc3_" + si.language + ".ac3";
                                }
                            }
                            else if (ac3AudioTypes.Contains(si.typeDesc))
                            {
                                if (settings.untouchedAudio && si.typeDesc == "TrueHD/AC3")
                                {
                                    Parameter += "audio_thd_" + si.language + ".thd\" ";
                                    si.filename = settings.workingDir + "\\" + prefix + "_" + si.number.ToString("d3") + "_audio_thd_" + si.language + ".thd";
                                }
                                else if (settings.untouchedAudio && si.typeDesc == "E-AC3")
                                {
                                    Parameter += "audio_eac3_" + si.language + ".eac3\" ";
                                    si.filename = settings.workingDir + "\\" + prefix + "_" + si.number.ToString("d3") + "_audio_eac3_" + si.language + ".eac3";
                                }
                                else if (settings.untouchedAudio && si.typeDesc == "RAW/PCM")
                                {
                                    Parameter += "audio_pcm_" + si.language + ".pcm\" ";
                                    si.filename = settings.workingDir + "\\" + prefix + "_" + si.number.ToString("d3") + "_audio_pcm_" + si.language + ".pcm";
                                }
                                else
                                {
                                    Parameter += "audio_ac3_" + si.language + ".ac3\" ";
                                    if (settings.downmixAc3)
                                    {
                                        Parameter += "-" + ac3Bitrate + " ";
                                    }
                                    si.filename = settings.workingDir + "\\" + prefix + "_" + si.number.ToString("d3") + "_audio_ac3_" + si.language + ".ac3";
                                }
                            }
                            else if (dtsAudioTypes.Contains(si.typeDesc))
                            {
                                if (settings.untouchedAudio && (si.typeDesc == "DTS Master Audio" || si.typeDesc == "DTS Hi-Res"))
                                {
                                    Parameter += "audio_dtsHD_" + si.language + ".dtshd\" ";
                                    si.filename = settings.workingDir + "\\" + prefix + "_" + si.number.ToString("d3") + "_audio_dtsHD_" + si.language + ".dtshd";
                                }
                                else if (settings.convertDtsToAc3)
                                {
                                    Parameter += "audio_ac3_" + si.language + ".ac3\" ";
                                    if (settings.downmixAc3)
                                    {
                                        Parameter += "-" + ac3Bitrate + " ";
                                    }
                                    si.filename = settings.workingDir + "\\" + prefix + "_" + si.number.ToString("d3") + "_audio_ac3_" + si.language + ".ac3";
                                }
                                else
                                {
                                    Parameter += "audio_dts_" + si.language + ".dts\" ";
                                    if (si.addInfo.Contains("core") && settings.dtsHdCore)
                                    {
                                        Parameter += "-core ";
                                    }
                                    if (settings.downmixDTS)
                                    {
                                        Parameter += "-" + dtsBitrate + " ";
                                    }
                                    si.filename = settings.workingDir + "\\" + prefix + "_" + si.number.ToString("d3") + "_audio_dts_" + si.language + ".dts";
                                }
                            }
                        }
                        else if (si.streamType == StreamType.Video)
                        {
                            Parameter += "video.mkv\" ";
                            si.filename = settings.workingDir + "\\" + prefix + "_" + si.number.ToString("d3") + "_video.mkv";
                        }
                        else if (si.streamType == StreamType.Subtitle)
                        {
                            Parameter += "subtitle_" + si.language + ".sup\" ";
                            si.filename = settings.workingDir + "\\" + prefix + "_" + si.number.ToString("d3") + "_subtitle_" + si.language + ".sup";
                        }
                    }
                }
            }
            catch (Exception)
            {
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

        protected override void Process()
        {
            try
            {
                TitleInfo tmpList2 = new TitleInfo();
                tmpList2.desc = streamList.desc;

                foreach (StreamInfo si in streamList.streams)
                {
                    if (si.selected)
                    {
                        tmpList2.streams.Add(new StreamInfo(si));
                    }
                }
                //TitleInfo.SaveStreamInfoFile(demuxedStreamList, settings.workingDir + "\\" + settings.filePrefix + "_streamInfo.xml");


                // sort streamlist
                TitleInfo tmpList = new TitleInfo();
                tmpList.desc = tmpList2.desc;

                // chapter first
                foreach (StreamInfo si in tmpList2.streams)
                {
                    if (si.streamType == StreamType.Chapter)
                    {
                        tmpList.streams.Add(new StreamInfo(si));
                    }
                }
                // video
                foreach (StreamInfo si in tmpList2.streams)
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
                    foreach (StreamInfo si in tmpList2.streams)
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
                foreach (StreamInfo si in tmpList2.streams)
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
                    foreach (StreamInfo si in tmpList2.streams)
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
                foreach (StreamInfo si in tmpList2.streams)
                {
                    if (si.streamType == StreamType.Subtitle)
                    {
                        if (!HasLanguage(si.language))
                        {
                            tmpList.streams.Add(new StreamInfo(si));
                        }
                    }
                }
                TitleInfo.SaveStreamInfoFile(tmpList, settings.workingDir + "\\" + settings.filePrefix + "_streamInfo.xml");

                foreach (StreamInfo si in tmpList.streams) demuxedStreamList.streams.Add(si);
                demuxedStreamList.desc = tmpList.desc;

                successfull = true;
            }
            catch (Exception)
            {
            }
        }
    }
}