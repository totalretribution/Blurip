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
                if (!Directory.Exists(settings.workingDir))
                {
                    logWindow.MessageDemux("Working dir not set");
                    if (!silent) ErrorMsg("Working dir not set");
                    return false;
                }
                if (comboBoxTitle.SelectedIndex == -1)
                {
                    logWindow.MessageDemux("No title selected");
                    if (!silent) ErrorMsg("No title selected");
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
                    logWindow.MessageDemux("No audio streams selected");
                    if (!silent) ErrorMsg("No audio streams selected");
                    return false;
                }
                if (videoCount != 1)
                {
                    logWindow.MessageDemux("No video stream or more then one selected");
                    if (!silent) ErrorMsg("No video stream or more then one selected");
                    return false;
                }
                if (unknown > 0)
                {
                    logWindow.MessageDemux("Unknown tracks selected - please report log to developer");
                    if (!silent) ErrorMsg("Unknown tracks selected - please report log to developer");
                    return false;
                }

                UpdateStatus((string)App.Current.Resources["StatusBar"] + " " + (string)App.Current.Resources["StatusBarDemux"]);
                DisableControls();

                progressBarMain.Visibility = Visibility.Visible;
                buttonAbort.Visibility = Visibility.Visible;

                dt = new DemuxTool(settings, m2tsList, videoTypes, ac3AudioTypes, dtsAudioTypes,
                    titleList[comboBoxTitle.SelectedIndex], ref demuxedStreamList, "", "");

                dt.OnInfoMsg += new ExternalTool.InfoEventHandler(DemuxMsg);
                dt.OnLogMsg += new ExternalTool.LogEventHandler(DemuxMsg);

                dt.Start();
                dt.WaitForExit();

                demuxedStreamsWindow.UpdateDemuxedStreams();

                return dt.Successfull;
            }
            catch (Exception ex)
            {
                logWindow.MessageDemux("Exception: " + ex.Message);
                return false;
            }
            finally
            {
                EnableControls();
                progressBarMain.Visibility = Visibility.Hidden;
                buttonAbort.Visibility = Visibility.Hidden;
                UpdateStatus((string)App.Current.Resources["StatusBar"] + " " + (string)App.Current.Resources["StatusBarReady"]);
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
                logWindow.MessageDemux("Exception: " + ex.Message);
            }
            finally
            {
            }
        }
    }
}