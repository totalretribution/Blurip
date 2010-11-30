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
        private bool lowRes = false;
        private bool pgs = false;

        public SubtitleTool(UserSettings settings, string fps, ref StreamInfo si, bool onlyForced, bool lowRes, bool pgs)
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
                this.lowRes = lowRes;
                this.pgs = pgs;

                if (this.fps == "23.00") this.fps = "23";
                if (this.fps == "24.00") this.fps = "24";
                if (this.fps == "25.00") this.fps = "25";

                string format = ".sub";
                if (pgs)
                {
                    format = ".sup";
                }

                if (!lowRes)
                {
                    if (!onlyForced)
                    {
                        output = settings.workingDir + "\\" + System.IO.Path.GetFileNameWithoutExtension(si.filename) +
                                "_complete" + format;

                        outputIdx = settings.workingDir + "\\" + System.IO.Path.GetFileNameWithoutExtension(si.filename) +
                            "_complete.idx";
                    }
                    else
                    {
                        output = settings.workingDir + "\\" + System.IO.Path.GetFileNameWithoutExtension(si.filename) +
                                "_onlyforced" + format;

                        outputIdx = settings.workingDir + "\\" + System.IO.Path.GetFileNameWithoutExtension(si.filename) +
                            "_onlyforced.idx";
                    }
                }
                else
                {
                    if (!onlyForced)
                    {
                        output = settings.workingDir + "\\" + System.IO.Path.GetFileNameWithoutExtension(si.filename) +
                                "_complete_lowres.sub";

                        outputIdx = settings.workingDir + "\\" + System.IO.Path.GetFileNameWithoutExtension(si.filename) +
                            "_complete_lowres.idx";
                    }
                    else
                    {
                        output = settings.workingDir + "\\" + System.IO.Path.GetFileNameWithoutExtension(si.filename) +
                                "_onlyforced_lowres.sub";

                        outputIdx = settings.workingDir + "\\" + System.IO.Path.GetFileNameWithoutExtension(si.filename) +
                            "_onlyforced_lowres.idx";
                    }
                }

                this.Parameter = "-jar \"" + settings.sup2subPath + "\" \"" +
                            si.filename + "\" \"" + output + "\" /fps:" + this.fps;

                if (!onlyForced)
                {
                }
                else
                {
                    this.Parameter += " /forced";
                }

                if (!lowRes)
                {
                    if (!settings.resize720p)
                    {
                        this.Parameter += " /res:1080";
                    }
                    else
                    {
                        this.Parameter += " /res:720";
                    }
                }
                else
                {
                    this.Parameter += " /res:576";
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

                if (!lowRes)
                {
                    if (!onlyForced)
                    {
                        if (File.Exists(output))
                        {
                            if (!pgs)
                            {
                                sfi.normalSub = output;
                            }
                            else
                            {
                                sfi.normalSup = output;
                            }
                        }
                        if (File.Exists(outputIdx))
                        {
                            if (!pgs)
                            {
                                sfi.normalIdx = outputIdx;
                            }
                        }
                    }
                    else
                    {
                        if (File.Exists(output))
                        {
                            if (!pgs)
                            {
                                sfi.forcedSub = output;
                            }
                            else
                            {
                                sfi.forcedSup = output;
                            }
                        }
                        if (File.Exists(outputIdx))
                        {
                            if (!pgs)
                            {
                                sfi.forcedIdx = outputIdx;
                            }
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
                        if (sfi.normalSup != "" && sfi.forcedSup != "")
                        {
                            FileInfo f1 = new FileInfo(sfi.normalSup);
                            FileInfo f2 = new FileInfo(sfi.forcedSup);
                            if (f1.Length == f2.Length)
                            {
                                File.Delete(sfi.normalSup);
                                sfi.normalSup = "";
                            }
                        }
                        successfull = true;
                    }
                    catch (Exception ex)
                    {
                        Info("Exception: " + ex.Message);
                    }
                }
                else
                {
                    if (!onlyForced)
                    {
                        if (File.Exists(output))
                        {
                            sfi.normalSubLowRes = output;
                        }
                        if (File.Exists(outputIdx))
                        {
                            sfi.normalIdxLowRes = outputIdx;
                        }
                    }
                    else
                    {
                        if (File.Exists(output))
                        {
                            sfi.forcedSubLowRes = output;
                        }
                        if (File.Exists(outputIdx))
                        {
                            sfi.forcedIdxLowRes = outputIdx;
                        }
                    }

                    try
                    {
                        if (sfi.normalIdxLowRes != "" && sfi.normalSubLowRes != "" && sfi.forcedIdxLowRes != "" && sfi.forcedSubLowRes != "")
                        {
                            FileInfo f1 = new FileInfo(sfi.normalSubLowRes);
                            FileInfo f2 = new FileInfo(sfi.forcedSubLowRes);
                            if (f1.Length == f2.Length)
                            {
                                File.Delete(sfi.normalSubLowRes);
                                File.Delete(sfi.normalIdxLowRes);
                                sfi.normalSubLowRes = "";
                                sfi.normalIdxLowRes = "";
                            }
                        }
                        successfull = true;
                    }
                    catch (Exception ex)
                    {
                        Info("Exception: " + ex.Message);
                    }
                }
            }
            catch (Exception)
            {
            }
        }
    }
}