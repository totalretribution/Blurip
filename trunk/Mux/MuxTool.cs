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
    public class MuxTool : ExternalTool
    {
        private UserSettings settings = null;
        private TitleInfo titleInfo = null;

        public MuxTool(UserSettings settings, TitleInfo titleInfo)
            : base()
        {
            try
            {
                this.settings = settings;
                this.titleInfo = titleInfo;
                this.Path = settings.mkvmergePath;
                this.Parameter += "--title \"" + settings.movieTitle + "\" -o \"" + settings.targetFolder + "\\" + settings.targetFilename + ".mkv\" ";

                // video + chapter
                foreach (StreamInfo si in titleInfo.streams)
                {
                    string lan = "";
                    if (settings.preferedLanguages.Count > 0) lan = settings.preferedLanguages[0].languageShort;

                    if (si.streamType == StreamType.Chapter)
                    {
                        if (lan != "") this.Parameter += "--chapter-language " + lan + " ";
                        this.Parameter += "--chapters \"" + si.filename + "\" ";
                    }
                    else if (si.streamType == StreamType.Video)
                    {
                        if (si.extraFileInfo != null && si.extraFileInfo.GetType() == typeof(VideoFileInfo))
                        {
                            this.Parameter += "\"" + ((VideoFileInfo)si.extraFileInfo).encodedFile + "\" ";
                        }
                    }
                }
                // audio
                bool defaultSet = false;
                foreach (StreamInfo si in titleInfo.streams)
                {
                    if (si.streamType == StreamType.Audio)
                    {
                        string st = "";
                        st = getShortLanguage(si.language);
                        if (st != "") this.Parameter += "--language 0" + ":" + st + " ";
                        if (settings.preferedLanguages.Count > 0 && settings.preferedLanguages[0].language == si.language)
                        {
                            if (!defaultSet)
                            {
                                if (settings.defaultAudio)
                                {
                                    this.Parameter += "--default-track 0 ";
                                }
                                defaultSet = true;
                            }
                        }
                        this.Parameter += "\"" + si.filename + "\" ";

                        // add additional ac3 track
                        if (si.advancedOptions != null && si.advancedOptions.GetType() == typeof(AdvancedAudioOptions))
                        {
                            AdvancedAudioOptions aao = (AdvancedAudioOptions)si.advancedOptions;
                            if (st != "") this.Parameter += "--language 0" + ":" + st + " ";
                            this.Parameter += "\"" + aao.additionalFilename + "\" ";
                        }
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
                    foreach (StreamInfo si in titleInfo.streams)
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
                                            if (st != "") this.Parameter += "--language 0" + ":" + st + " ";

                                            if (!settings.defaultSubtitleForced)
                                            {
                                                this.Parameter += "--default-track 0 ";
                                                defaultSet = true;
                                            }
                                            else
                                            {
                                                if (hasForcedSub(si.language))
                                                {
                                                    if (sfi.forcedIdx != "")
                                                    {
                                                        this.Parameter += "--default-track 0 ";
                                                        defaultSet = true;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                                if (!settings.defaultSubtitle)
                                {
                                    this.Parameter += "--default-track 0:0 ";
                                }
                                if (sfi.normalIdx != "")
                                {
                                    this.Parameter += "\"" + sfi.normalIdx + "\" ";
                                }
                                else if (sfi.forcedIdx != "")
                                {
                                    this.Parameter += "\"" + sfi.forcedIdx + "\" ";
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Info("Exception: " + ex.Message); 
            }
        }

        protected override void StartInfo()
        {   
            Info("Starting to mux...");
            Info("");
        }

        protected override void EndInfo()
        {
            Info("Done.");
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
                foreach (StreamInfo si in titleInfo.streams)
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

        protected override bool StartProcessing()
        {
            if (Ok && ExitCode == 0) return true;
            else return false;
        }

        protected override void Process()
        {
            try
            {
                List<int> subsCount = new List<int>();
                List<int> forcedSubsCount = new List<int>();
                for (int i = 0; i < settings.preferedLanguages.Count; i++)
                {
                    subsCount.Add(0);
                    forcedSubsCount.Add(0);
                }

                for (int i = 0; i < settings.preferedLanguages.Count; i++)
                {
                    subsCount[i] = 0;
                    forcedSubsCount[i] = 0;
                }

                if (settings.copySubs > 0)
                {
                    Info("Trying to copy subtitles...");
                    try
                    {
                        if (!Directory.Exists(settings.targetFolder + "\\Subs"))
                        {
                            Directory.CreateDirectory(settings.targetFolder + "\\Subs");
                        }
                    }
                    catch (Exception ex)
                    {
                        Info("Exception: " + ex.Message);
                    }
                    int sub = 1;
                    foreach (StreamInfo si in titleInfo.streams)
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
                                        Info("Exception: " + ex.Message);
                                    }
                                    sub++;
                                }
                            }
                        }
                    }
                    Info("Done.");
                }
                if (settings.deleteAfterEncode)
                {
                    Info("Deleting source files...");
                    string filename = "";
                    foreach (StreamInfo si in titleInfo.streams)
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
                            Info("Exception: " + ex.Message);
                        }
                    }
                    try
                    {
                        File.Delete(settings.workingDir + "\\" + settings.filePrefix + "_streamInfo.xml");
                        File.Delete(settings.workingDir + "\\" + settings.filePrefix + "_cropTemp.avs");
                    }
                    catch (Exception ex)
                    {
                        Info("Exception: " + ex.Message);
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
                        string output = System.IO.Path.ChangeExtension(filename, "dgi");
                        File.Delete(output);
                    }
                    catch (Exception)
                    {
                    }
                    Info("Done.");
                }
            }
            catch (Exception)
            {
            }
        }
    }
}