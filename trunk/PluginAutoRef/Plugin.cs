using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace BluRip
{
    public class PluginAutoRef : PluginBase
    {
        public PluginAutoRef()
            : base()
        {
        }

        public override string GetDescription()
        {
            return "Set --ref parameter for selected encoding profile based on resolution";
        }

        public override string GetName()
        {
            return "Autoref";
        }

        public override string GetVersion()
        {
            return "v0.1";
        }

        // adjust when plugin is called
        public override PluginType getPluginType()
        {
            return PluginType.BeforeEncode;
        }

        public override Type GetSettingsType()
        {
            return typeof(AutoRefSettings);
        }

        protected override string FileName
        {
            get { return "autoref.xml"; }
        }

        public override PluginSettingsBase GetNewSettings()
        {
            return new AutoRefSettings();
        }

        public override bool EditSettings()
        {
            try
            {
                SettingsWindow sw = new SettingsWindow(Settings);
                sw.ShowDialog();
                if (sw.DialogResult == true)
                {
                    this.settings = new AutoRefSettings(sw.Settings);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public override void Process()
        {
            try
            {
                int index = -1;
                for (int i = 0; i < this.project.demuxedStreamList.streams.Count; i++)
                {
                    if (project.demuxedStreamList.streams[i].streamType == StreamType.Video)
                    {
                        index = i;
                        break;
                    }
                }
                if (index > 0)
                {
                    if (project.demuxedStreamList.streams[index].extraFileInfo != null && project.demuxedStreamList.streams[index].extraFileInfo.GetType() == typeof(VideoFileInfo))
                    {
                        VideoFileInfo vfi = (VideoFileInfo)project.demuxedStreamList.streams[index].extraFileInfo;
                        if (vfi.cropInfo.resizeX == 1920)
                        {
                            if (vfi.cropInfo.resizeY > 864 && vfi.cropInfo.resizeY <= 1088)
                            {
                                if (project.settings.lastProfile >= 0 && project.settings.lastProfile < project.settings.encodingSettings.Count)
                                {
                                    project.settings.encodingSettings[project.settings.lastProfile].refvalue = 4;
                                }
                            }
                            else if (vfi.cropInfo.resizeY > 720 && vfi.cropInfo.resizeY <= 864)
                            {
                                if (project.settings.lastProfile >= 0 && project.settings.lastProfile < project.settings.encodingSettings.Count)
                                {
                                    project.settings.encodingSettings[project.settings.lastProfile].refvalue = 5;
                                }
                            }
                            else if (vfi.cropInfo.resizeY <= 720)
                            {
                                if (project.settings.lastProfile >= 0 && project.settings.lastProfile < project.settings.encodingSettings.Count)
                                {
                                    project.settings.encodingSettings[project.settings.lastProfile].refvalue = 6;
                                }
                            }
                        }
                        else if (vfi.cropInfo.resizeX == 1280)
                        {
                            if (vfi.cropInfo.resizeY > 648 && vfi.cropInfo.resizeY <= 720)
                            {
                                if (project.settings.lastProfile >= 0 && project.settings.lastProfile < project.settings.encodingSettings.Count)
                                {
                                    project.settings.encodingSettings[project.settings.lastProfile].refvalue = 9;
                                }
                            }
                            else if (vfi.cropInfo.resizeY > 588 && vfi.cropInfo.resizeY <= 648)
                            {
                                if (project.settings.lastProfile >= 0 && project.settings.lastProfile < project.settings.encodingSettings.Count)
                                {
                                    project.settings.encodingSettings[project.settings.lastProfile].refvalue = 10;
                                }
                            }
                            else if (vfi.cropInfo.resizeY > 540 && vfi.cropInfo.resizeY <= 588)
                            {
                                if (project.settings.lastProfile >= 0 && project.settings.lastProfile < project.settings.encodingSettings.Count)
                                {
                                    project.settings.encodingSettings[project.settings.lastProfile].refvalue = 11;
                                }
                            }
                            else if (vfi.cropInfo.resizeY <= 540)
                            {
                                if (project.settings.lastProfile >= 0 && project.settings.lastProfile < project.settings.encodingSettings.Count)
                                {
                                    project.settings.encodingSettings[project.settings.lastProfile].refvalue = 12;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
            }
        }
    }

    public class AutoRefSettings : PluginSettingsBase
    {
        public AutoRefSettings(PluginSettingsBase orig)
            : base(orig)
        {
            try
            {

            }
            catch (Exception)
            {
            }
        }

        public AutoRefSettings()
            : base()
        {
        }
    }
}
