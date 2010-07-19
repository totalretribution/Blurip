using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using System.Windows.Forms;

namespace BluRip
{
    public partial class MainWindow : Window
    {
        private DemuxTool dt = null;

        private bool DoDemux()
        {
            try
            {
                if (!settings.doDemux) return true;

                if (!Directory.Exists(settings.workingDir))
                {
                    logWindow.MessageDemux(Global.Res("ErrorWorkingDirectory"));
                    if (!silent) Global.ErrorMsg(Global.Res("ErrorWorkingDirectory"));
                    return false;
                }
                if (comboBoxTitle.SelectedIndex == -1)
                {
                    logWindow.MessageDemux(Global.Res("ErrorNoTitle"));
                    if (!silent) Global.ErrorMsg(Global.Res("ErrorNoTitle"));
                    return false;
                }
                int videoCount = 0;
                int audioCount = 0;
                int unknown = 0;
                foreach (StreamInfo si in titleList[comboBoxTitle.SelectedIndex].streams)
                {
                    if (si.streamType == StreamType.Audio && si.selected)
                    {
                        audioCount++;
                    }
                    if (si.streamType == StreamType.Video && si.selected)
                    {
                        videoCount++;
                    }
                    if (si.streamType == StreamType.Unknown && si.selected)
                    {
                        unknown++;
                    }
                }
                if (audioCount < 1)
                {
                    logWindow.MessageDemux(Global.Res("ErrorNoAudio"));
                    if (!silent) Global.ErrorMsg(Global.Res("ErrorNoAudio"));
                    return false;
                }
                if (videoCount != 1)
                {
                    logWindow.MessageDemux(Global.Res("ErrorNoVideo"));
                    if (!silent) Global.ErrorMsg(Global.Res("ErrorNoVideo"));
                    return false;
                }
                if (unknown > 0)
                {
                    logWindow.MessageDemux(Global.Res("ErrorUnknownTracks"));
                    if (!silent) Global.ErrorMsg(Global.Res("ErrorUnknownTracks"));
                    return false;
                }

                UpdateStatus(Global.Res("StatusBar") + " " + Global.Res("StatusBarDemux"));
                DisableControls();
                
                string dtsBitrate = "1536";
                string ac3Bitrate = "640";
                if(settings.downmixDTSIndex >= 0 && settings.downmixDTSIndex < dtsBitrates.Count) dtsBitrate = dtsBitrates[settings.downmixDTSIndex];
                if(settings.downmixAc3Index >= 0 && settings.downmixAc3Index < ac3Bitrates.Count) ac3Bitrate = ac3Bitrates[settings.downmixAc3Index];

                dt = new DemuxTool(settings, m2tsList, videoTypes, ac3AudioTypes, dtsAudioTypes,
                    titleList[comboBoxTitle.SelectedIndex], ref demuxedStreamList, ac3Bitrate, dtsBitrate);

                dt.OnInfoMsg += new ExternalTool.InfoEventHandler(DemuxMsg);
                dt.OnLogMsg += new ExternalTool.LogEventHandler(DemuxMsg);

                dt.Start();
                dt.WaitForExit();

                demuxedStreamsWindow.UpdateDemuxedStreams();

                if (dt == null) return false;
                else return dt.Successfull;
            }
            catch (Exception ex)
            {
                logWindow.MessageDemux(Global.Res("ErrorException") + " " + ex.Message);
                return false;
            }
            finally
            {
                EnableControls();

                UpdateStatus(Global.Res("StatusBar") + " " + Global.Res("StatusBarReady"));
            }
        }

        private void buttonOnlyDemux_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!checkEac3to()) return;
                DoDemux();
            }
            catch (Exception ex)
            {
                logWindow.MessageDemux(Global.Res("ErrorException") + " " + ex.Message);
            }
            finally
            {
            }
        }
    }
}