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

using System.Collections.Generic;
using System;
using System.IO;
using System.Xml.Serialization;

namespace BluRip
{
    public class LanguageInfo
    {
        public LanguageInfo() { }

        public LanguageInfo(string language, string translation, string languageShort)
        {
            this.language = language;
            this.translation = translation;
            this.languageShort = languageShort;
        }

        public LanguageInfo(LanguageInfo orig)
        {
            this.language = orig.language;
            this.translation = orig.translation;
            this.languageShort = orig.languageShort;
        }

        public string language = "";
        public string translation = "";
        public string languageShort = "";
    }

    public enum SizeType
    {
        None, // when using crf
        Bitrate, // specify bitrate
        Size // specify target size
    }

    public class EncodingSettings
    {
        public EncodingSettings() { }

        public EncodingSettings(EncodingSettings orig)
        {
            this.desc = orig.desc;
            this.settings = orig.settings;
            this.settings2 = orig.settings2;
            this.pass2 = orig.pass2;
            this.sizeValue = orig.sizeValue;
            this.sizeType = orig.sizeType;
        }

        public EncodingSettings(string desc, string parameter)
        {
            this.desc = desc;
            this.settings = parameter;
        }

        public EncodingSettings(string desc, string parameter, string parameter2, bool pass2)
        {
            this.desc = desc;
            this.settings = parameter;
            this.settings2 = parameter2;
            this.pass2 = pass2;
        }

        public string desc = "";
        public string settings = "";
        public bool pass2 = false;
        public string settings2 = "";
        public double sizeValue = 0;
        public SizeType sizeType = SizeType.None;
    }

    public class AvisynthSettings
    {
        public AvisynthSettings() { }

        public AvisynthSettings(string desc, string commands)
        {
            this.desc = desc;
            this.commands = commands;
        }

        public AvisynthSettings(AvisynthSettings orig)
        {
            this.desc = orig.desc;
            this.commands = orig.commands;
        }

        public string desc = "";

        public string commands = "";
    }

    public class UserSettings
    {
        public UserSettings() { }

        public UserSettings(UserSettings orig)
        {
            this.eac3toPath = orig.eac3toPath;
            this.lastBluRayPath = orig.lastBluRayPath;
            this.useAutoSelect = orig.useAutoSelect;
            this.includeChapter = orig.includeChapter;
            this.includeSubtitle = orig.includeSubtitle;
            this.preferDTS = orig.preferDTS;
            this.preferedLanguages.Clear();
            this.workingDir = orig.workingDir;
            this.ffmsindexPath = orig.ffmsindexPath;
            this.x264Path = orig.x264Path;
            this.sup2subPath = orig.sup2subPath;
            this.nrFrames = orig.nrFrames;
            this.blackValue = orig.blackValue;
            this.filePrefix = orig.filePrefix;
            this.javaPath = orig.javaPath;            
            this.cropMode = orig.cropMode;
            this.mkvmergePath = orig.mkvmergePath;
            this.x264Priority = orig.x264Priority;
            this.targetFolder = orig.targetFolder;
            this.targetFilename = orig.targetFilename;
            this.movieTitle = orig.movieTitle;
            this.defaultAudio = orig.defaultAudio;
            this.defaultSubtitle = orig.defaultSubtitle;
            this.defaultSubtitleForced = orig.defaultSubtitleForced;
            this.lastProfile = orig.lastProfile;
            this.dtsHdCore = orig.dtsHdCore;
            this.untouchedVideo = orig.untouchedVideo;
            this.lastAvisynthProfile = orig.lastAvisynthProfile;
            this.resize720p = orig.resize720p;
            this.downmixAc3 = orig.downmixAc3;
            this.downmixAc3Index = orig.downmixAc3Index;
            this.downmixDTS = orig.downmixDTS;
            this.downmixDTSIndex = orig.downmixDTSIndex;
            this.minimizeAutocrop = orig.minimizeAutocrop;
            this.cropInput = orig.cropInput;
            this.encodeInput = orig.encodeInput;
            this.untouchedAudio = orig.untouchedAudio;
            this.muxSubs = orig.muxSubs;
            this.copySubs = orig.copySubs;
            this.dgindexnvPath = orig.dgindexnvPath;
            this.convertDtsToAc3 = orig.convertDtsToAc3;
            this.x264x64Path = orig.x264x64Path;
            this.avs2yuvPath = orig.avs2yuvPath;
            this.use64bit = orig.use64bit;

            foreach (LanguageInfo li in orig.preferedLanguages)
            {
                this.preferedLanguages.Add(new LanguageInfo(li));
            }

            foreach (EncodingSettings es in orig.encodingSettings)
            {
                this.encodingSettings.Add(new EncodingSettings(es));
            }

            foreach(AvisynthSettings avs in orig.avisynthSettings)
            {
                this.avisynthSettings.Add(new AvisynthSettings(avs));
            }
        }
             

        public static bool SaveSettingsFile(UserSettings settings, string filename)
        {
            MemoryStream ms = null;
            FileStream fs = null;
            XmlSerializer xs = null;
            
            try
            {
                ms = new MemoryStream();
                fs = new FileStream(filename, FileMode.Create, FileAccess.Write);

                xs = new XmlSerializer(typeof(UserSettings));
                xs.Serialize(ms, settings);
                ms.Seek(0, SeekOrigin.Begin);
                
                fs.Write(ms.ToArray(), 0, (int)ms.Length);
                ms.Close();
                fs.Close();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
            finally
            {                
                if (ms != null) ms.Close();
                if (fs != null) fs.Close();
            }
        }

        public static bool LoadSettingsFile(ref UserSettings settings, string filename)
        {
            MemoryStream ms = null;
            
            try
            {
                if (!File.Exists(filename)) return false;
                byte[] data = File.ReadAllBytes(filename);
                XmlSerializer xs = new XmlSerializer(typeof(UserSettings));
                ms = new MemoryStream(data);
                ms.Seek(0, SeekOrigin.Begin);
                
                settings = (UserSettings)xs.Deserialize(ms);
                ms.Close();

                if (settings.preferedLanguages.Count == 0)
                {
                    settings.preferedLanguages.Add(new LanguageInfo("German", "Deutsch", "de"));
                    settings.preferedLanguages.Add(new LanguageInfo("English","Englisch","en"));
                }

                if(settings.encodingSettings.Count == 0)
                {
                    settings.encodingSettings.Add(new EncodingSettings("Slow - film - crf 19.0 - level 4.1", "--preset slow --tune film --crf 19.0 --level 4.1"));
                    settings.encodingSettings.Add(new EncodingSettings("Slow - animation - crf 19.0 - level 4.1", "--preset slow --tune animation --crf 19.0 --level 4.1"));

                    settings.encodingSettings.Add(new EncodingSettings("Slow - film - crf 18.0 - level 4.1", "--preset slow --tune film --crf 18.0 --level 4.1"));
                    settings.encodingSettings.Add(new EncodingSettings("Slow - animation - crf 18.0 - level 4.1", "--preset slow --tune animation --crf 18.0 --level 4.1"));
                }

                if (settings.avisynthSettings.Count == 0)
                {
                    settings.avisynthSettings.Add(new AvisynthSettings("Empty", ""));
                    settings.avisynthSettings.Add(new AvisynthSettings("Undot", "# undot - remove minimal noise\r\nUndot()\r\n"));
                }

                return true;
            }
            catch (Exception)
            {
                return false;
            }
            finally
            {
                if (ms != null) ms.Close();
            }
        }

        public string eac3toPath = "";
        public string lastBluRayPath = "";
        public bool useAutoSelect = true;
        public bool includeChapter = true;
        public bool includeSubtitle = true;
        public bool preferDTS = true;        
        public List<LanguageInfo> preferedLanguages = new List<LanguageInfo>();
        public string workingDir = "";
        public string ffmsindexPath = "";
        public string x264Path = "";
        public string sup2subPath = "";
        public int nrFrames = 10;
        public int blackValue = 50000;
        public string filePrefix = "";
        public string javaPath = "";        
        public List<EncodingSettings> encodingSettings = new List<EncodingSettings>();        
        public int cropMode = 0;
        public string mkvmergePath = "";
        public System.Diagnostics.ProcessPriorityClass x264Priority = System.Diagnostics.ProcessPriorityClass.Normal;
        public string targetFolder = "";
        public string targetFilename = "";
        public string movieTitle = "";
        public bool defaultAudio = true;
        public bool defaultSubtitle = true;
        public bool defaultSubtitleForced = true;
        public int lastProfile = 0;
        public bool deleteAfterEncode = false;
        public bool dtsHdCore = true;        
        public bool untouchedVideo = false;
        public List<AvisynthSettings> avisynthSettings = new List<AvisynthSettings>();
        public int lastAvisynthProfile = 0;
        public bool resize720p = false;
        public bool downmixDTS= false;
        public int downmixDTSIndex = 0;
        public bool downmixAc3 = false;
        public int downmixAc3Index = 0;
        public bool minimizeAutocrop = false;
        public int cropInput = 1;
        public int encodeInput = 1;
        public bool untouchedAudio = false;
        public int muxSubs = 1;
        public int copySubs = 1;
        public string dgindexnvPath = "";
        public bool convertDtsToAc3 = false;
        public string x264x64Path = "";
        public string avs2yuvPath = "";
        public bool use64bit = false;
    }
}