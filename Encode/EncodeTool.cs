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
                    }
                }

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
                            this.Parameter = settings.encodingSettings[profile].settings + " \"" + vfi.encodeAvs + "\" " +
                                "--pass 1 --bitrate " + bitrate.ToString() +
                                " -o \"" + settings.workingDir +
                                "\\" + settings.filePrefix + "_video.mkv\"";
                        }
                        else
                        {
                            this.Path = "cmd.exe";
                            this.Parameter = "/c \"\"" + settings.avs2yuvPath + "\" -raw \"" + vfi.encodeAvs + "\" -o - | \"" +
                                settings.x264x64Path + "\" " + settings.encodingSettings[profile].settings + " --fps " + vfi.fps + " " +
                                "--pass 1 --bitrate " + bitrate.ToString() +
                                " -o \"" + settings.workingDir + "\\" +
                                settings.filePrefix + "_video.mkv\"" + " - " + vfi.resX + "x" + vfi.resY + "\"";
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
                            this.Parameter = settings.encodingSettings[profile].settings2 + " \"" + vfi.encodeAvs + "\" " +
                                "--pass 2 --bitrate " + bitrate.ToString() +
                                " -o \"" + settings.workingDir +
                                "\\" + settings.filePrefix + "_video.mkv\"";
                        }
                        else
                        {
                            this.Path = "cmd.exe";
                            this.Parameter = "/c \"\"" + settings.avs2yuvPath + "\" -raw \"" + vfi.encodeAvs + "\" -o - | \"" +
                                settings.x264x64Path + "\" " + settings.encodingSettings[profile].settings2 + " --fps " + vfi.fps + " " +
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
    }
}