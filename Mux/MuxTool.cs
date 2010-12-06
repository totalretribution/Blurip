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
using System.Globalization;

namespace BluRip
{
    public class MuxTool : ExternalTool
    {
        private UserSettings settings = null;
        private TitleInfo titleInfo = null;
        private AdvancedVideoOptions avo = null;
        private string headerCompression = "--compression -1:none ";

        public MuxTool(UserSettings settings, TitleInfo titleInfo)
            : base()
        {
            try
            {
                this.settings = settings;
                this.titleInfo = titleInfo;
                this.Path = settings.mkvmergePath;
                this.Parameter += "--title \"" + settings.movieTitle + "\" -o \"" + settings.targetFolder + "\\" + settings.targetFilename + ".mkv\" ";
                int trackId = 0;
                // video + chapter
                foreach (StreamInfo si in titleInfo.streams)
                {
                    string lan = "";
                    if (settings.preferredAudioLanguages.Count > 0) lan = settings.preferredAudioLanguages[0].languageShort;

                    if (si.streamType == StreamType.Chapter)
                    {
                        if (lan != "") this.Parameter += "--chapter-language " + lan + " ";
                        this.Parameter += "--chapters \"" + si.filename + "\" ";
                    }
                    else if (si.streamType == StreamType.Video)
                    {
                        trackId++;
                        if (settings.disableVideoHeaderCompression)
                        {
                            this.Parameter += headerCompression;
                        }
                        if (si.advancedOptions != null && si.advancedOptions.GetType() == typeof(AdvancedVideoOptions))
                        {
                            avo = new AdvancedVideoOptions(si.advancedOptions);
                        }
                        if (avo != null && avo.manualAspectRatio)
                        {
                            this.Parameter += "--aspect-ratio 0:" + avo.aspectRatio + " ";
                        }
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
                        trackId++;
                        string st = "";
                        st = getShortAudioLanguage(si.language);
                        if (st != "") this.Parameter += "--language 0" + ":" + st + " ";
                        
                        if (settings.disableAudioHeaderCompression)
                        {
                            this.Parameter += headerCompression;
                        }
                        if (!settings.defaultSubtitle)
                        {
                            this.Parameter += "--default-track 0:no ";
                        }

                        if (settings.preferredAudioLanguages.Count > 0 && settings.preferredAudioLanguages[0].language == si.language)
                        {
                            if (!defaultSet)
                            {
                                if (settings.defaultAudio)
                                {
                                    this.Parameter += "--default-track 0:yes ";
                                }
                                defaultSet = true;
                            }
                        }
                        this.Parameter += "\"" + si.filename + "\" ";

                        // add additional ac3 track
                        if (si.advancedOptions != null && si.advancedOptions.GetType() == typeof(AdvancedAudioOptions))
                        {
                            AdvancedAudioOptions aao = (AdvancedAudioOptions)si.advancedOptions;
                            if (aao.additionalAc3Track && File.Exists(aao.additionalFilename))
                            {
                                trackId++;

                                if (st != "") this.Parameter += "--language 0" + ":" + st + " ";
                                if (settings.disableAudioHeaderCompression)
                                {
                                    this.Parameter += headerCompression;
                                }
                                this.Parameter += "\"" + aao.additionalFilename + "\" ";
                            }
                        }
                    }
                }                

                List<int> subsCount = new List<int>();
                List<int> forcedSubsCount = new List<int>();
                for (int i = 0; i < settings.preferredSubtitleLanguages.Count; i++)
                {
                    subsCount.Add(0);
                    forcedSubsCount.Add(0);
                }

                // hardcode subs? do not mux subtitles even if selected
                bool suptitle = false;

                foreach (StreamInfo si in titleInfo.streams)
                {
                    if (si.streamType == StreamType.Subtitle)
                    {
                        if (si.advancedOptions != null && si.advancedOptions.GetType() == typeof(AdvancedSubtitleOptions))
                        {
                            if (((AdvancedSubtitleOptions)si.advancedOptions).supTitle)
                            {
                                if (!suptitle)
                                {
                                    suptitle = true;                                    
                                }
                            }
                        }
                    }
                }

                if ((settings.muxSubs > 0 || settings.muxUntouchedSubs) && !suptitle)
                {
                    // subtitle
                    defaultSet = false;
                    foreach (StreamInfo si in titleInfo.streams)
                    {
                        if (si.streamType == StreamType.Subtitle)
                        {                            
                            SubtitleFileInfo sfi = (SubtitleFileInfo)si.extraFileInfo;

                            bool isForced = false;
                            if (si.advancedOptions != null && si.advancedOptions.GetType() == typeof(AdvancedSubtitleOptions) && ((AdvancedSubtitleOptions)si.advancedOptions).isForced) isForced = true;

                            bool mux = false;
                            bool pgs = false;
                            bool untouched = false;

                            if (settings.muxUntouchedSubs)
                            {
                                if (!sfi.isSecond)
                                {
                                    mux = true;
                                    untouched = true;
                                }
                            }
                            else if (settings.muxSubs == 1)
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
                                for (int i = 0; i < settings.preferredSubtitleLanguages.Count; i++)
                                {
                                    if (settings.preferredSubtitleLanguages[i].language == si.language) lang = i;
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

                            else if (settings.muxSubs == 4)
                            {
                                mux = true;
                                pgs = true;
                            }
                            else if (settings.muxSubs == 5)
                            {
                                if (sfi.forcedSup != "")
                                {
                                    mux = true;
                                    pgs = true;
                                }
                            }
                            else if (settings.muxSubs == 6)
                            {
                                int lang = -1;
                                for (int i = 0; i < settings.preferredSubtitleLanguages.Count; i++)
                                {
                                    if (settings.preferredSubtitleLanguages[i].language == si.language) lang = i;
                                }
                                if (lang > -1)
                                {
                                    if (sfi.normalSup != "")
                                    {
                                        if (subsCount[lang] == 0)
                                        {
                                            mux = true;
                                            pgs = true;
                                            subsCount[lang]++;
                                        }
                                    }
                                    else if (sfi.forcedSup != "")
                                    {
                                        if (forcedSubsCount[lang] == 0)
                                        {
                                            mux = true;
                                            pgs = true;
                                            forcedSubsCount[lang]++;
                                        }
                                    }
                                }
                            }

                            if (mux)
                            {
                                trackId++;
                                string st = "";
                                st = getShortSubLanguage(si.language);
                                if (st != "") this.Parameter += "--language 0" + ":" + st + " ";
                                
                                if (settings.preferredSubtitleLanguages.Count > 0 && settings.preferredSubtitleLanguages[0].language == si.language)
                                {
                                    if (!defaultSet)
                                    {
                                        if (settings.defaultSubtitle)
                                        {
                                            if (!settings.defaultSubtitleForced)
                                            {
                                                this.Parameter += "--default-track 0:yes ";
                                                defaultSet = true;
                                            }
                                            else
                                            {
                                                if (hasForcedSub(si.language))
                                                {
                                                    if (!untouched && !pgs)
                                                    {
                                                        if (sfi.forcedIdx != "")
                                                        {
                                                            this.Parameter += "--default-track 0 ";
                                                            if (settings.defaultForcedFlag)
                                                            {
                                                                this.Parameter += "--forced-track 0 ";
                                                            }
                                                            defaultSet = true;
                                                        }
                                                    }
                                                    else if (!untouched && pgs)
                                                    {
                                                        if (sfi.forcedSup != "")
                                                        {
                                                            this.Parameter += "--default-track 0 ";
                                                            if (settings.defaultForcedFlag)
                                                            {
                                                                this.Parameter += "--forced-track 0 ";
                                                            }
                                                            defaultSet = true;
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                                if (!settings.defaultSubtitle)
                                {
                                    this.Parameter += "--default-track 0:no ";
                                }
                                if (untouched)
                                {
                                    if (si.filename != "")
                                    {
                                        this.Parameter += "\"" + si.filename + "\" ";
                                    }
                                }
                                else if (!settings.muxLowResSubs && !pgs)
                                {
                                    if (sfi.normalIdx != "" && !isForced)
                                    {
                                        this.Parameter += "\"" + sfi.normalIdx + "\" ";
                                    }
                                    else if (sfi.forcedIdx != "")
                                    {
                                        this.Parameter += " --track-name 0:Forced ";
                                        this.Parameter += "\"" + sfi.forcedIdx + "\" ";
                                    }
                                    else if (sfi.normalIdx != "" && isForced)
                                    {
                                        this.Parameter += " --track-name 0:Forced ";
                                        this.Parameter += "\"" + sfi.normalIdx + "\" ";
                                    }
                                }
                                else if (!settings.muxLowResSubs && pgs)
                                {
                                    if (sfi.normalSup != "" && !isForced)
                                    {
                                        this.Parameter += "\"" + sfi.normalSup + "\" ";
                                    }
                                    else if (sfi.forcedSup != "")
                                    {
                                        this.Parameter += " --track-name 0:Forced ";
                                        this.Parameter += "\"" + sfi.forcedSup + "\" ";
                                    }
                                    else if (sfi.normalSup != "")
                                    {
                                        this.Parameter += " --track-name 0:Forced ";
                                        this.Parameter += "\"" + sfi.normalSup + "\" ";
                                    }
                                }
                                else
                                {
                                    if (sfi.normalIdxLowRes != "" && !isForced)
                                    {
                                        this.Parameter += "\"" + sfi.normalIdxLowRes + "\" ";
                                    }
                                    else if (sfi.forcedIdxLowRes != "")
                                    {
                                        this.Parameter += " --track-name 0:Forced ";
                                        this.Parameter += "\"" + sfi.forcedIdxLowRes + "\" ";
                                    }
                                    else if (sfi.normalIdxLowRes != "" && isForced)
                                    {
                                        this.Parameter += " --track-name 0:Forced ";
                                        this.Parameter += "\"" + sfi.normalIdxLowRes + "\" ";
                                    }
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

        private string getShortAudioLanguage(string language)
        {
            try
            {
                foreach (LanguageInfo li in settings.preferredAudioLanguages)
                {
                    if (li.language == language) return li.languageShort;
                }
                return LanguageTag(language);
            }
            catch (Exception)
            {
                return "";
            }
        }

        private string getShortSubLanguage(string language)
        {
            try
            {
                foreach (LanguageInfo li in settings.preferredSubtitleLanguages)
                {
                    if (li.language == language) return li.languageShort;
                }
                if (language == "Modern Greek")
                {
                    return LanguageTag("Greek");
                }
                else
                {
                    return LanguageTag(language);
                }
            }
            catch (Exception)
            {
                return "";
            }
        }

        private string LanguageTag(string language)
        {
            try
            {
                CultureInfo[] cultures = CultureInfo.GetCultures(CultureTypes.AllCultures);
                foreach (CultureInfo ci in cultures)
                {
                    if (ci.EnglishName == language)
                    {
                        return ci.TwoLetterISOLanguageName;
                    }
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
                            if (((SubtitleFileInfo)si.extraFileInfo).forcedSup != "") return true;
                            if (si.advancedOptions != null && si.advancedOptions.GetType() == typeof(AdvancedSubtitleOptions) && ((AdvancedSubtitleOptions)si.advancedOptions).isForced) return true;
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
                for (int i = 0; i < settings.preferredAudioLanguages.Count; i++)
                {
                    subsCount.Add(0);
                    forcedSubsCount.Add(0);
                }

                for (int i = 0; i < settings.preferredAudioLanguages.Count; i++)
                {
                    subsCount[i] = 0;
                    forcedSubsCount[i] = 0;
                }

                if (settings.copySubs > 0 || settings.copyUntouchedSubs)
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
                    int undefCount = 0;
                    int undefForcedCount = 0;

                    foreach (StreamInfo si in titleInfo.streams)
                    {
                        if (si.streamType == StreamType.Subtitle)
                        {
                            if (si.extraFileInfo.GetType() == typeof(SubtitleFileInfo))
                            {
                                SubtitleFileInfo sfi = (SubtitleFileInfo)si.extraFileInfo;

                                bool copy = false;
                                bool pgs = false;
                                bool untouched = false;
                                bool subidx = false;

                                bool isForced = false;
                                if (si.advancedOptions != null && si.advancedOptions.GetType() == typeof(AdvancedSubtitleOptions) && ((AdvancedSubtitleOptions)si.advancedOptions).isForced) isForced = true;

                                if (settings.copyUntouchedSubs)
                                {
                                    if (!sfi.isSecond)
                                    {
                                        copy = true;
                                        untouched = true;
                                    }
                                }

                                if (settings.copySubs == 1)
                                {
                                    copy = true;
                                    subidx = true;
                                }
                                else if (settings.copySubs == 2)
                                {
                                    if (sfi.normalIdx != "")
                                    {
                                        copy = true;
                                        subidx = true;
                                    }
                                }
                                else if (settings.copySubs == 3)
                                {
                                    int lang = -1;
                                    for (int i = 0; i < settings.preferredAudioLanguages.Count; i++)
                                    {
                                        if (settings.preferredAudioLanguages[i].language == si.language) lang = i;
                                    }
                                    if (lang > -1)
                                    {
                                        if (sfi.normalIdx != "")
                                        {
                                            if (subsCount[lang] == 0)
                                            {
                                                copy = true;
                                                subidx = true;
                                                subsCount[lang]++;
                                            }
                                        }
                                        else if (sfi.forcedIdx != "")
                                        {
                                            if (forcedSubsCount[lang] == 0)
                                            {
                                                copy = true;
                                                subidx = true;
                                                forcedSubsCount[lang]++;
                                            }
                                        }
                                    }
                                    if (si.language == "undef")
                                    {
                                        if (sfi.normalIdx != "")
                                        {
                                            if (undefCount == 0)
                                            {
                                                copy = true;
                                                subidx = true;
                                                undefCount++;
                                            }
                                        }
                                        else if (sfi.forcedIdx != "")
                                        {
                                            if (undefForcedCount == 0)
                                            {
                                                copy = true;
                                                subidx = true;
                                                undefForcedCount++;
                                            }
                                        }
                                    }
                                }
                                else if (settings.copySubs == 4)
                                {
                                    copy = true;
                                    pgs = true;
                                }
                                else if (settings.copySubs == 5)
                                {
                                    if (sfi.normalSup != "")
                                    {
                                        copy = true;
                                        pgs = true;
                                    }
                                }
                                else if (settings.copySubs == 6)
                                {
                                    int lang = -1;
                                    for (int i = 0; i < settings.preferredAudioLanguages.Count; i++)
                                    {
                                        if (settings.preferredAudioLanguages[i].language == si.language) lang = i;
                                    }
                                    if (lang > -1)
                                    {
                                        if (sfi.normalSup != "")
                                        {
                                            if (subsCount[lang] == 0)
                                            {
                                                copy = true;
                                                pgs = true;
                                                subsCount[lang]++;
                                            }
                                        }
                                        else if (sfi.forcedSup != "")
                                        {
                                            if (forcedSubsCount[lang] == 0)
                                            {
                                                copy = true;
                                                pgs = true;
                                                forcedSubsCount[lang]++;
                                            }
                                        }
                                    }
                                    if (si.language == "undef")
                                    {
                                        if (sfi.normalSub != "")
                                        {
                                            if (undefCount == 0)
                                            {
                                                copy = true;
                                                pgs = true;
                                                undefCount++;
                                            }
                                        }
                                        else if (sfi.forcedSup != "")
                                        {
                                            if (undefForcedCount == 0)
                                            {
                                                copy = true;
                                                pgs = true;
                                                undefForcedCount++;
                                            }
                                        }
                                    }
                                }


                                if (copy)
                                {
                                    try
                                    {
                                        string target = settings.targetFolder + "\\Subs\\" + settings.targetFilename;
                                        if (untouched)
                                        {
                                            if (si.filename != "")
                                            {
                                                if (File.Exists(si.filename)) File.Copy(si.filename, target + "_" + sub.ToString("d2") + "_" + si.language.ToLower() + "_original_pgs.sup", true);
                                            }
                                        }
                                        if (!pgs && subidx)
                                        {
                                            if (sfi.normalIdx != "" && !isForced)
                                            {
                                                if (File.Exists(sfi.normalIdx)) File.Copy(sfi.normalIdx, target + "_" + sub.ToString("d2") + "_" + si.language.ToLower() + ".idx", true);
                                                if (File.Exists(sfi.normalSub)) File.Copy(sfi.normalSub, target + "_" + sub.ToString("d2") + "_" + si.language.ToLower() + ".sub", true);
                                            }
                                            else if (sfi.forcedIdx != "")
                                            {
                                                if (File.Exists(sfi.forcedIdx)) File.Copy(sfi.forcedIdx, target + "_" + sub.ToString("d2") + "_" + si.language.ToLower() + "_forced.idx", true);
                                                if (File.Exists(sfi.forcedSub)) File.Copy(sfi.forcedSub, target + "_" + sub.ToString("d2") + "_" + si.language.ToLower() + "_forced.sub", true);
                                            }
                                            else if (sfi.normalIdx != "" && isForced)
                                            {
                                                if (File.Exists(sfi.normalIdx)) File.Copy(sfi.normalIdx, target + "_" + sub.ToString("d2") + "_" + si.language.ToLower() + "_forced.idx", true);
                                                if (File.Exists(sfi.normalSub)) File.Copy(sfi.normalSub, target + "_" + sub.ToString("d2") + "_" + si.language.ToLower() + "_forced.sub", true);
                                            }
                                        }
                                        else if (pgs)
                                        {
                                            if (sfi.normalSup != "" && !isForced)
                                            {
                                                if (File.Exists(sfi.normalSup)) File.Copy(sfi.normalSup, target + "_" + sub.ToString("d2") + "_" + si.language.ToLower() + "_pgs.sup", true);
                                            }
                                            else if (sfi.forcedSup != "")
                                            {
                                                if (File.Exists(sfi.forcedSup)) File.Copy(sfi.forcedSup, target + "_" + sub.ToString("d2") + "_" + si.language.ToLower() + "_forced_pgs.sup", true);
                                            }
                                            else if (sfi.normalSup != "" && isForced)
                                            {
                                                if (File.Exists(sfi.normalSup)) File.Copy(sfi.normalSup, target + "_" + sub.ToString("d2") + "_" + si.language.ToLower() + "_forced_pgs.sup", true);
                                            }
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
                            if(File.Exists(si.filename)) File.Delete(si.filename);
                            if (si.extraFileInfo.GetType() == typeof(VideoFileInfo))
                            {
                                if(File.Exists(((VideoFileInfo)si.extraFileInfo).encodedFile)) File.Delete(((VideoFileInfo)si.extraFileInfo).encodedFile);
                                if(File.Exists(((VideoFileInfo)si.extraFileInfo).encodeAvs)) File.Delete(((VideoFileInfo)si.extraFileInfo).encodeAvs);
                                filename = si.filename;
                            }
                            if (si.extraFileInfo.GetType() == typeof(SubtitleFileInfo))
                            {
                                SubtitleFileInfo sfi = (SubtitleFileInfo)si.extraFileInfo;
                                if (sfi.forcedIdx != "")
                                {
                                    if(File.Exists(sfi.forcedIdx)) File.Delete(sfi.forcedIdx);
                                }
                                if (sfi.forcedSub != "")
                                {
                                    if(File.Exists(sfi.forcedSub)) File.Delete(sfi.forcedSub);
                                }
                                if (sfi.forcedSup != "")
                                {
                                    if(File.Exists(sfi.forcedSup)) File.Delete(sfi.forcedSup);
                                }

                                if (sfi.normalIdx != "")
                                {
                                    if(File.Exists(sfi.normalIdx)) File.Delete(sfi.normalIdx);
                                }
                                if (sfi.normalSub != "")
                                {
                                    if(File.Exists(sfi.normalSub)) File.Delete(sfi.normalSub);
                                }
                                if (sfi.normalSup != "")
                                {
                                    if(File.Exists(sfi.normalSup)) File.Delete(sfi.normalSup);
                                }

                                if (sfi.forcedIdxLowRes != "")
                                {
                                    if(File.Exists(sfi.forcedIdxLowRes)) File.Delete(sfi.forcedIdxLowRes);
                                }
                                if (sfi.forcedSubLowRes != "")
                                {
                                    if(File.Exists(sfi.forcedSubLowRes)) File.Delete(sfi.forcedSubLowRes);
                                }

                                if (sfi.normalIdxLowRes != "")
                                {
                                    if(File.Exists(sfi.normalIdxLowRes)) File.Delete(sfi.normalIdxLowRes);
                                }
                                if (sfi.normalSubLowRes != "")
                                {
                                    if(File.Exists(sfi.normalSubLowRes)) File.Delete(sfi.normalSubLowRes);
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            Info("Exception: " + ex.Message);
                        }
                    }
                    try
                    {
                        if (File.Exists(settings.workingDir + "\\" + settings.filePrefix + "_streamInfo.xml")) File.Delete(settings.workingDir + "\\" + settings.filePrefix + "_streamInfo.xml");
                        if(File.Exists(settings.workingDir + "\\" + settings.filePrefix + "_cropTemp.avs")) File.Delete(settings.workingDir + "\\" + settings.filePrefix + "_cropTemp.avs");
                    }
                    catch (Exception ex)
                    {
                        Info("Exception: " + ex.Message);
                    }

                    // delete index files
                    try
                    {
                        if(File.Exists(filename + ".ffindex")) File.Delete(filename + ".ffindex");
                    }
                    catch (Exception)
                    {
                    }

                    try
                    {
                        string output = System.IO.Path.ChangeExtension(filename, "dgi");
                        if(File.Exists(output)) File.Delete(output);
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