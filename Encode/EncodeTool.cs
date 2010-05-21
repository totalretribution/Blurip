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
    public class EncodeTool : ExternalTool
    {
        private UserSettings settings = null;
        private bool secondPass = false;
        private VideoFileInfo vfi = null;
        private TitleInfo titleInfo = null;
        private int profile = -1;

        public EncodeTool(UserSettings settings, TitleInfo titleInfo, int profile, bool secondPass, VideoFileInfo vfi)
            : base()
        {
            try
            {
                this.settings = settings;
                this.secondPass = secondPass;
                this.Priority = settings.x264Priority;
                this.vfi = vfi;
                this.titleInfo = titleInfo;
                this.profile = profile;

                bool is2pass = settings.encodingSettings[profile].pass2;
                int bitrate = 0;

                if (settings.encodingSettings[profile].pass2)
                {
                    if (settings.encodingSettings[profile].sizeType == SizeType.Bitrate)
                    {
                        bitrate = (int)settings.encodingSettings[profile].sizeValue;
                    }
                    else if (settings.encodingSettings[profile].sizeType == SizeType.Size)
                    {
                        int length = 0;
                        int frames = 0;
                        try
                        {
                            length = Convert.ToInt32(vfi.length);
                            length = (int)(length / 1000);
                        }
                        catch (Exception)
                        {
                        }

                        try
                        {
                            frames = Convert.ToInt32(vfi.frames);
                        }
                        catch (Exception)
                        {
                        }

                        // use frame count to calculate overhead
                        // to be done...

                        long totalSize = GetSize();
                        long targetSize = Convert.ToInt64(settings.encodingSettings[profile].sizeValue * 1024.0 * 1024.0);
                        bitrate = (int)((targetSize - totalSize) / 1024 / length);
                        // no mkv overhead used yet
                    }
                }

                string statsFile = settings.workingDir + "\\" + settings.filePrefix + "_stats";

                if (!secondPass)
                {
                    if (!settings.encodingSettings[profile].pass2)
                    {
                        if (!settings.use64bit)
                        {
                            this.Path = settings.x264Path;
                            this.Parameter = settings.encodingSettings[profile].settings + " \"" + vfi.encodeAvs + "\" -o \"" + settings.workingDir +
                                    "\\" + settings.filePrefix + "_video.mkv\"";
                        }
                        else
                        {
                            this.Path = "cmd.exe";
                            this.Parameter = "/c \"\"" + settings.avs2yuvPath + "\" -raw \"" + vfi.encodeAvs + "\" -o - | \"" +
                                settings.x264x64Path + "\" " + settings.encodingSettings[profile].settings + " --fps " + vfi.fps + " " +
                                " -o \"" + settings.workingDir + "\\" + 
                                settings.filePrefix + "_video.mkv\"" + " - " + vfi.resX + "x" + vfi.resY + "\"";
                        }
                    }
                    else
                    {
                        if (!settings.use64bit)
                        {
                            this.Path = settings.x264Path;
                            this.Parameter = settings.encodingSettings[profile].settings + " \"" + vfi.encodeAvs + "\" " + "--stats \"" + statsFile + "\" " +
                                "--pass 1 --bitrate " + bitrate.ToString() +
                                " -o NUL";
                        }
                        else
                        {
                            this.Path = "cmd.exe";
                            this.Parameter = "/c \"\"" + settings.avs2yuvPath + "\" -raw \"" + vfi.encodeAvs + "\" -o - | \"" +
                                settings.x264x64Path + "\" " + settings.encodingSettings[profile].settings + " --fps " + vfi.fps + " " + "--stats \"" + statsFile + "\" " +
                                "--pass 1 --bitrate " + bitrate.ToString() +
                                " -o NUL" + " - " + vfi.resX + "x" + vfi.resY + "\"";
                        }
                    }
                }
                else
                {
                    if (!settings.encodingSettings[profile].pass2)
                    {
                        if (!settings.use64bit)
                        {
                            this.Path = settings.x264Path;
                            this.Parameter = settings.encodingSettings[profile].settings2 + " \"" + vfi.encodeAvs + "\" -o \"" + settings.workingDir +
                                    "\\" + settings.filePrefix + "_video.mkv\"";
                        }
                        else
                        {
                            this.Path = "cmd.exe";
                            this.Parameter = "/c \"\"" + settings.avs2yuvPath + "\" -raw \"" + vfi.encodeAvs + "\" -o - | \"" +
                                settings.x264x64Path + "\" " + settings.encodingSettings[profile].settings2 + " --fps " + vfi.fps + " -o \"" + settings.workingDir + "\\" + 
                                settings.filePrefix + "_video.mkv\"" + " - " + vfi.resX + "x" + vfi.resY + "\"";
                        }
                    }
                    else
                    {
                        if (!settings.use64bit)
                        {
                            this.Path = settings.x264Path;
                            this.Parameter = settings.encodingSettings[profile].settings2 + " \"" + vfi.encodeAvs + "\" " + "--stats \"" + statsFile + "\" " +
                                "--pass 2 --bitrate " + bitrate.ToString() +
                                " -o \"" + settings.workingDir +
                                "\\" + settings.filePrefix + "_video.mkv\"";
                        }
                        else
                        {
                            this.Path = "cmd.exe";
                            this.Parameter = "/c \"\"" + settings.avs2yuvPath + "\" -raw \"" + vfi.encodeAvs + "\" -o - | \"" +
                                settings.x264x64Path + "\" " + settings.encodingSettings[profile].settings2 + " --fps " + vfi.fps + " " + "--stats \"" + statsFile + "\" " +
                                "--pass 2 --bitrate " + bitrate.ToString() +
                                " -o \"" + settings.workingDir + "\\" +
                                settings.filePrefix + "_video.mkv\"" + " - " + vfi.resX + "x" + vfi.resY + "\"";
                        }
                    }
                }
            }
            catch (Exception)
            {   
            }
        }

        private long GetSize()
        {
            try
            {
                List<int> subsCount = new List<int>();
                List<int> forcedSubsCount = new List<int>();
                for (int i = 0; i < settings.preferredLanguages.Count; i++)
                {
                    subsCount.Add(0);
                    forcedSubsCount.Add(0);
                }

                for (int i = 0; i < settings.preferredLanguages.Count; i++)
                {
                    subsCount[i] = 0;
                    forcedSubsCount[i] = 0;
                }

                long totalSize = 0;
                foreach (StreamInfo si in titleInfo.streams)
                {
                    if (si.streamType == StreamType.Audio)
                    {
                        try
                        {
                            FileInfo fi = new FileInfo(si.filename);
                            totalSize += fi.Length;

                            if (si.advancedOptions != null &&  si.advancedOptions.GetType() == typeof(AdvancedAudioOptions))
                            {
                                if (((AdvancedAudioOptions)si.advancedOptions).additionalAc3Track && ((AdvancedAudioOptions)si.advancedOptions).additionalFilename != "")
                                {
                                    try
                                    {
                                        FileInfo fi2 = new FileInfo(((AdvancedAudioOptions)si.advancedOptions).additionalFilename);
                                        totalSize += fi2.Length;
                                    }
                                    catch (Exception)
                                    {
                                    }
                                }
                            }
                        }
                        catch (Exception)
                        {
                        }
                    }
                    else if (si.streamType == StreamType.Subtitle)
                    {
                        if (si.extraFileInfo != null && si.extraFileInfo.GetType() == typeof(SubtitleFileInfo))
                        {
                            SubtitleFileInfo sfi = (SubtitleFileInfo)si.extraFileInfo;
                            long normalSize = 0;
                            long forcedSize = 0;
                            if (sfi.normalIdx != "" && sfi.normalSub != "")
                            {
                                try
                                {
                                    FileInfo fi = new FileInfo(sfi.normalIdx);
                                    normalSize += fi.Length;
                                    fi = new FileInfo(sfi.normalSub);
                                    normalSize += fi.Length;
                                }
                                catch (Exception)
                                {
                                }
                            }
                            if (sfi.forcedIdx != "" && sfi.forcedSub != "")
                            {
                                try
                                {
                                    FileInfo fi = new FileInfo(sfi.forcedIdx);
                                    forcedSize += fi.Length;
                                    fi = new FileInfo(sfi.forcedSub);
                                    forcedSize += fi.Length;
                                }
                                catch (Exception)
                                {
                                }
                            }
                            // mux all subs
                            if (settings.muxSubs == 1)
                            {
                                totalSize += normalSize;
                                totalSize += forcedSize;
                            }
                            // mux only forced
                            else if (settings.muxSubs == 2)
                            {
                                totalSize += forcedSize;
                            }
                            // only first normal/forced sub
                            else if (settings.muxSubs == 3)
                            {
                                int lang = -1;
                                for (int i = 0; i < settings.preferredLanguages.Count; i++)
                                {
                                    if (settings.preferredLanguages[i].language == si.language) lang = i;
                                }
                                if (lang > -1)
                                {
                                    if (sfi.normalIdx != "")
                                    {
                                        if (subsCount[lang] == 0)
                                        {                                            
                                            subsCount[lang]++;
                                            totalSize += normalSize;
                                        }
                                    }
                                    else if (sfi.forcedIdx != "")
                                    {
                                        if (forcedSubsCount[lang] == 0)
                                        {                                            
                                            forcedSubsCount[lang]++;
                                            totalSize += forcedSize;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                return totalSize;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        protected override void StartInfo()
        {
            if (!secondPass)
            {
                Info("Starting to encode...");
            }
            else
            {
                Info("Starting to encode 2. pass...");
            }
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

        protected override void Process()
        {
            try
            {
                if (secondPass || !settings.encodingSettings[profile].pass2)
                {
                    if (File.Exists(settings.workingDir + "\\" + settings.filePrefix + "_video.mkv"))
                    {
                        foreach (StreamInfo si in titleInfo.streams)
                        {
                            if (si.streamType == StreamType.Video)
                            {
                                if (si.extraFileInfo != null && si.extraFileInfo.GetType() == typeof(VideoFileInfo))
                                {
                                    ((VideoFileInfo)si.extraFileInfo).encodedFile = settings.workingDir + "\\" + settings.filePrefix + "_video.mkv";
                                    successfull = true;
                                    break;
                                }
                            }
                        }
                    }
                }
                else
                {
                    if (File.Exists(settings.workingDir + "\\" + settings.filePrefix + "_stats"))
                    {
                        successfull = true;
                    }
                }
            }
            catch (Exception)
            {
            }
        }
    }
}