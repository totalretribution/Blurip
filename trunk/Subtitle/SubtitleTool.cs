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
    public class SubtitleTool : ExternalTool
    {
        private UserSettings settings = null;
        private string fps = "";
        private StreamInfo si = null;
        private bool onlyForced = false;
        private string output = "";
        private string outputIdx = "";

        public SubtitleTool(UserSettings settings, string fps, ref StreamInfo si, bool onlyForced)
            : base()
        {
            try
            {
                this.settings = settings;
                this.fps = fps;
                this.Path = settings.javaPath;
                this.si = new StreamInfo();
                this.si = si;
                this.onlyForced = onlyForced;

                if (!onlyForced)
                {
                    output = settings.workingDir + "\\" + System.IO.Path.GetFileNameWithoutExtension(si.filename) +
                            "_complete.sub";

                    outputIdx = settings.workingDir + "\\" + System.IO.Path.GetFileNameWithoutExtension(si.filename) +
                        "_complete.idx";
                }
                else
                {
                    output = settings.workingDir + "\\" + System.IO.Path.GetFileNameWithoutExtension(si.filename) +
                            "_onlyforced.sub";

                    outputIdx = settings.workingDir + "\\" + System.IO.Path.GetFileNameWithoutExtension(si.filename) +
                        "_onlyforced.idx";
                }

                this.Parameter = "-jar \"" + settings.sup2subPath + "\" \"" +
                            si.filename + "\" \"" + output + "\" /fps:" + this.fps;

                if (!settings.resize720p)
                {
                    this.Parameter += " /res:1080";
                }
                else
                {
                    this.Parameter += " /res:720";
                }
            }
            catch (Exception)
            {
            }
        }

        protected override void StartInfo()
        {
            Info("Starting to process subtitle...");
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
                if (si.extraFileInfo == null || si.extraFileInfo.GetType() != typeof(SubtitleFileInfo)) si.extraFileInfo = new SubtitleFileInfo();
                SubtitleFileInfo sfi = null;
                sfi = (SubtitleFileInfo)si.extraFileInfo;

                if (!onlyForced)
                {
                    if (File.Exists(output))
                    {
                        sfi.normalSub = output;
                    }
                    if (File.Exists(outputIdx))
                    {
                        sfi.normalIdx = outputIdx;
                    }
                }
                else
                {
                    if (File.Exists(output))
                    {
                        sfi.forcedSub = output;
                    }
                    if (File.Exists(outputIdx))
                    {
                        sfi.forcedIdx = outputIdx;
                    }
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
                    Info("Exception: " + ex.Message);
                }
            }
            catch (Exception)
            {
            }
        }
    }
}