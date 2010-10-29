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
    public class SubStreamInfoTool : ExternalTool
    {
        private UserSettings settings = null;
        private List<TitleInfo> result = null;
        private List<string> videoTypes = null;
        private List<string> ac3AudioTypes = null;
        private List<string> dtsAudioTypes = null;
        private List<string> files = null;
        private string streamNumber = "";

        public SubStreamInfoTool(UserSettings settings, ref List<TitleInfo> result, string path, string streamNumber, List<string> videoTypes, List<string> ac3AudioTypes, List<string> dtsAudioTypes, List<string> files)
            : base()
        {
            try
            {
                this.settings = settings;
                this.result = result;
                this.Path = settings.eac3toPath;
                this.Parameter = "\"" + path + "\" " + streamNumber + ")";
                this.videoTypes = videoTypes;
                this.ac3AudioTypes = ac3AudioTypes;
                this.dtsAudioTypes = dtsAudioTypes;
                this.streamNumber = streamNumber + ")";
                this.files = files;
            }
            catch (Exception)
            {
            }
        }

        protected override void StartInfo()
        {
            Info("Getting title info...");
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
                string res = Output;
                res = res.Replace("\b", "");
                res = res.Replace("\r", "");

                string[] tmp = res.Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
                for (int i = 0; i < tmp.Length; i++)
                {
                    tmp[i] = tmp[i].Trim();
                }

                if (res.Trim() == "")
                {
                    Info("Failed to get stream infos");
                    return;
                }

                TitleInfo ti = new TitleInfo();
                if (files != null)
                {
                    ti.files.Clear();
                    foreach (string s in files) ti.files.Add(s);
                }

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
                    ti.streamNumber = streamNumber;
                }

                for (int i = 0; i < tmp.Length; i++)
                {
                    if (Regex.IsMatch(tmp[i], "^[0-9.*].*:"))
                    {
                        StreamInfo sr = new StreamInfo();
                        sr.desc = tmp[i];
                        if (i < tmp.Length - 1)
                        {
                            if (!Regex.IsMatch(tmp[i + 1], "^[0-9.*].*:"))
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
                                else
                                {
                                    sr.language = "undef";
                                }
                            }
                            else if (dtsAudioTypes.Contains(tmpInfo[0]))
                            {
                                sr.streamType = StreamType.Audio;
                                if (tmpInfo.Length > 1)
                                {
                                    sr.language = tmpInfo[1].Trim();
                                }
                                else
                                {
                                    sr.language = "undef";
                                }
                            }
                            else if (tmpInfo[0] == "Subtitle (PGS)")
                            {
                                sr.streamType = StreamType.Subtitle;
                                if (tmpInfo.Length > 1)
                                {
                                    sr.language = tmpInfo[1].Trim();
                                }
                                else
                                {
                                    sr.language = "undef";
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
                successfull = true;
            }
            catch (Exception)
            {
            }
        }
    }
}