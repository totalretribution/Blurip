using System.Collections.Generic;
using System.Security.Cryptography;
using System;
using System.IO;
using System.Xml.Serialization;
using System.Text;
namespace BluRip
{

    public class LanguagInfo
    {
        public LanguagInfo() { }

        public LanguagInfo(string language, string translation, string languageShort)
        {
            this.language = language;
            this.translation = translation;
            this.languageShort = languageShort;
        }

        public LanguagInfo(LanguagInfo orig)
        {
            this.language = orig.language;
            this.translation = orig.translation;
            this.languageShort = orig.languageShort;
        }

        public string language = "";
        public string translation = "";
        public string languageShort = "";
    }

    public class EncodingSettings
    {
        public EncodingSettings() { }

        public EncodingSettings(EncodingSettings orig)
        {
            this.desc = orig.desc;
            this.settings = orig.settings;
        }

        public EncodingSettings(string desc, string parameter)
        {
            this.desc = desc;
            this.settings = parameter;
        }

        public string desc = "";
        public string settings = "";
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
            this.cropDirectshow = orig.cropDirectshow;
            this.encodeDirectshow = orig.encodeDirectshow;
            this.cropMode = orig.cropMode;
            this.mkvmergePath = orig.mkvmergePath;
            this.x264Priority = orig.x264Priority;
            this.targetFolder = orig.targetFolder;
            this.targetFilename = orig.targetFilename;
            this.movieTitle = orig.movieTitle;
            this.defaultAudio = orig.defaultAudio;
            this.defaultSubtitle = orig.defaultSubtitle;
            this.defaultSubtitleForced = orig.defaultSubtitleForced;
            this.commandsAfterResize = orig.commandsAfterResize;
            this.lastProfile = orig.lastProfile;
            this.dtsHdCore = orig.dtsHdCore;

            foreach (LanguagInfo li in orig.preferedLanguages)
            {
                this.preferedLanguages.Add(li);
            }

            foreach (EncodingSettings es in orig.encodingSettings)
            {
                this.encodingSettings.Add(es);
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
                    settings.preferedLanguages.Add(new LanguagInfo("German", "Deutsch", "de"));
                    settings.preferedLanguages.Add(new LanguagInfo("English","Englisch","en"));
                }

                if(settings.encodingSettings.Count == 0)
                {
                    settings.encodingSettings.Add(new EncodingSettings("Slow - film - crf 19.0 - level 4.1", "--preset slow --tune film --crf 19.0 --level 4.1"));
                    settings.encodingSettings.Add(new EncodingSettings("Slow - animation - crf 19.0 - level 4.1", "--preset slow --tune animation --crf 19.0 --level 4.1"));

                    settings.encodingSettings.Add(new EncodingSettings("Slow - film - crf 18.0 - level 4.1", "--preset slow --tune film --crf 18.0 --level 4.1"));
                    settings.encodingSettings.Add(new EncodingSettings("Slow - animation - crf 18.0 - level 4.1", "--preset slow --tune animation --crf 18.0 --level 4.1"));
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
        public List<LanguagInfo> preferedLanguages = new List<LanguagInfo>();
        public string workingDir = "";
        public string ffmsindexPath = "";
        public string x264Path = "";
        public string sup2subPath = "";
        public int nrFrames = 10;
        public int blackValue = 50000;
        public string filePrefix = "";
        public string javaPath = "";        
        public List<EncodingSettings> encodingSettings = new List<EncodingSettings>();
        public bool cropDirectshow = true;
        public bool encodeDirectshow = false;
        public int cropMode = 0;
        public string mkvmergePath = "";
        public System.Diagnostics.ProcessPriorityClass x264Priority = System.Diagnostics.ProcessPriorityClass.Normal;
        public string targetFolder = "";
        public string targetFilename = "";
        public string movieTitle = "";
        public bool defaultAudio = true;
        public bool defaultSubtitle = true;
        public bool defaultSubtitleForced = true;
        public string commandsAfterResize = "";
        public int lastProfile = 0;
        public bool deleteAfterEncode = false;
        public bool dtsHdCore = true;
    }
}