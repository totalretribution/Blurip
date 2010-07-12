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
using System.Collections.ObjectModel;
using System.Runtime.InteropServices;

namespace BluRip
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private UserSettings settings = new UserSettings();
        private string settingsPath = "";

        private List<string> videoTypes = new List<string>();
        private List<string> ac3AudioTypes = new List<string>();
        private List<string> dtsAudioTypes = new List<string>();
        private List<string> ac3Bitrates = new List<string>();
        private List<string> dtsBitrates = new List<string>();

        public string title = "BluRip v0.5.0 © _hawk_";

        private LogWindow logWindow = null;
        private DemuxedStreamsWindow demuxedStreamsWindow = null;
        private QueueWindow queueWindow = null;
        private bool silent = false;
        private bool abort = false;
        private List<Project> projectQueue = new List<Project>();

        public MainWindow()
        {            
            try
            {
                InitializeComponent();

                videoTypes.Add("h264/AVC");
                videoTypes.Add("VC-1");
                videoTypes.Add("MPEG2");

                ac3AudioTypes.Add("TrueHD/AC3");
                ac3AudioTypes.Add("AC3");
                ac3AudioTypes.Add("AC3 Surround");
                ac3AudioTypes.Add("AC3 EX");
                ac3AudioTypes.Add("E-AC3");
                ac3AudioTypes.Add("RAW/PCM"); // convert to ac3 by default

                dtsAudioTypes.Add("DTS");
                dtsAudioTypes.Add("DTS Master Audio");
                dtsAudioTypes.Add("DTS Express");
                dtsAudioTypes.Add("DTS Hi-Res");
                dtsAudioTypes.Add("DTS ES"); // have to check if needed
                dtsAudioTypes.Add("DTS-ES");

                dtsBitrates.Add("768");
                dtsBitrates.Add("1536");

                ac3Bitrates.Add("192");
                ac3Bitrates.Add("448");
                ac3Bitrates.Add("640");
            }
            catch (Exception ex)
            {
                Global.ErrorMsg(ex);
            }
        }
        
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                this.Title = title;
                settingsPath = AppDomain.CurrentDomain.BaseDirectory + "\\settings.xml";

                if (!File.Exists(settingsPath))
                {
                    UserSettings.SaveSettingsFile(settings, settingsPath);
                    UserSettings.LoadSettingsFile(ref settings, settingsPath);
                }
                else
                {
                    UserSettings.LoadSettingsFile(ref settings, settingsPath);
                }
                                
                logWindow = new LogWindow(this);
                logWindow.Owner = this;
                                
                queueWindow = new QueueWindow(this);
                queueWindow.Owner = this;

                demuxedStreamsWindow = new DemuxedStreamsWindow(this);
                demuxedStreamsWindow.Owner = this;

                UpdateFromSettings(true);

                UpdateStatus(Global.Res("StatusBar") + " " + Global.Res("StatusBarReady"));
            }
            catch (Exception ex)
            {
                Global.ErrorMsg(ex);
            }
        }

        private void menuItemViewLog_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (menuItemViewLog.IsChecked)
                {
                    logWindow.Show();
                }
                else
                {
                    logWindow.Hide();
                }
            }
            catch (Exception)
            {
            }
        }

        private delegate void UpdateDelegate(string msg);

        private void UpdateStatus(string msg)
        {
            try
            {
                if (this.Dispatcher.CheckAccess())
                {
                    statusBarItemMain.Content = msg;
                }
                else
                {
                    this.Dispatcher.Invoke(new UpdateDelegate(UpdateStatus), msg);
                }
            }
            catch (Exception)
            {
            }
        }

        private void menuItemSettingsExternalTools_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ExternalTools et = new ExternalTools(settings);
                et.ShowDialog();
                if (et.DialogResult == true)
                {
                    settings = new UserSettings(et.userSettings);
                }
            }
            catch (Exception)
            {
            }
        }

        private void windowMain_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            try
            {
                settings.snap = menuItemViewSnap.IsChecked;
                settings.expertMode = menuItemViewExpertMode.IsChecked;
                
                settings.showDemuxedStream = menuItemViewDemuxedStreams.IsChecked;
                settings.demuxedStreamsX = demuxedStreamsWindow.Left;
                settings.demuxedStreamsY = demuxedStreamsWindow.Top;
                settings.demuxedStreamsHeight = demuxedStreamsWindow.Height;
                settings.demuxedStreamsWidth = demuxedStreamsWindow.Width;

                settings.showLog = menuItemViewLog.IsChecked;
                settings.logX = logWindow.Left;
                settings.logY = logWindow.Top;
                settings.logHeight = logWindow.Height;
                settings.logWidth = logWindow.Width;

                settings.showQueue = menuItemViewQueue.IsChecked;
                settings.queueX = queueWindow.Left;
                settings.queueY = queueWindow.Top;
                settings.queueHeight = queueWindow.Height;
                settings.queueWidth = queueWindow.Width;

                settings.bluripX = this.Left;
                settings.bluripY = this.Top;
                settings.bluripHeight = this.Height;
                settings.bluripWidth = this.Width;

                UserSettings.SaveSettingsFile(settings, settingsPath);
                if (!silent)
                {
                    if (System.Windows.MessageBox.Show(Global.Res("MessageExit"), Global.Res("MessageExitHeader"), MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                    {
                        abortThreads();
                    }
                    else
                    {
                        e.Cancel = true;
                    }
                }
                else
                {
                    abortThreads();
                }
            }
            catch (Exception)
            {
            }
        }

        private void UpdateFromSettings(bool updateDicts)
        {
            try
            {
                textBoxBlurayPath.Text = settings.lastBluRayPath;
                textBoxWorkingDirectory.Text = settings.workingDir;
                textBoxFilePrefix.Text = settings.filePrefix;
                textBoxTargetDirectory.Text = settings.targetFolder;
                textBoxTargetFilename.Text = settings.targetFilename;
                textBoxMovieTitle.Text = settings.movieTitle;
                textBoxEncodedMovieDir.Text = settings.encodedMovieDir;

                this.Left = settings.bluripX;
                this.Top = settings.bluripY;
                this.Height = settings.bluripHeight;
                this.Width = settings.bluripWidth;

                logWindow.Left = settings.logX;
                logWindow.Top = settings.logY;
                logWindow.Height = settings.logHeight;
                logWindow.Width = settings.logWidth;
                menuItemViewLog.IsChecked = settings.showLog;
                
                queueWindow.Left = settings.queueX;
                queueWindow.Top = settings.queueY;
                queueWindow.Height = settings.queueHeight;
                queueWindow.Width = settings.queueWidth;
                menuItemViewQueue.IsChecked = settings.showQueue;

                demuxedStreamsWindow.Left = settings.demuxedStreamsX;
                demuxedStreamsWindow.Top = settings.demuxedStreamsY;
                demuxedStreamsWindow.Height = settings.demuxedStreamsHeight;
                demuxedStreamsWindow.Width = settings.demuxedStreamsWidth;
                menuItemViewDemuxedStreams.IsChecked = settings.showDemuxedStream;

                menuItemViewSnap.IsChecked = settings.snap;
                menuItemViewExpertMode.IsChecked = settings.expertMode;

                UpdateDiff();

                menuItemViewLog_Click(null, null);
                menuItemViewDemuxedStreams_Click(null, null);
                menuItemViewQueue_Click(null, null);
                menuItemViewExpertMode_Click(null, null);

                UpdateAvisynthProfiles();
                UpdateEncodingProfiles();
                UpdateMuxSettings();

                if (updateDicts)
                {
                    if (settings.cropInput > -1 && settings.cropInput < 3) comboBoxCropInput.SelectedIndex = settings.cropInput;
                    else
                    {
                        comboBoxCropInput.SelectedIndex = 0;
                    }

                    if (settings.encodeInput > -1 && settings.encodeInput < 3) comboBoxEncodeInput.SelectedIndex = settings.encodeInput;
                    else
                    {
                        comboBoxEncodeInput.SelectedIndex = 0;
                    }

                    if (settings.language == "en")
                    {
                        menuItemLanguageEnglish.IsChecked = true;
                        menuItemLanguageEnglish_Click(null, null);
                    }
                    else if (settings.language == "de")
                    {
                        menuItemLanguageGerman.IsChecked = true;
                        menuItemLanguageGerman_Click(null, null);
                    }
                    else
                    {
                        menuItemLanguageEnglish.IsChecked = true;
                        menuItemLanguageEnglish_Click(null, null);
                    }

                    if (settings.skin == "blu")
                    {
                        menuItemViewSkinBlu.IsChecked = true;
                        menuItemViewSkinBlu_Click(null, null);
                    }
                    else if (settings.skin == "classic")
                    {
                        menuItemViewSkinClassic.IsChecked = true;
                        menuItemViewSkinClassic_Click(null, null);
                    }
                    else
                    {
                        menuItemViewSkinBlu.IsChecked = true;
                        menuItemViewSkinBlu_Click(null, null);
                    }
                }

                checkBoxUntouchedAudio.IsChecked = settings.untouchedAudio;
                checkBoxUntouchedVideo.IsChecked = settings.untouchedVideo;
                checkBoxConvertDtsToAc3.IsChecked = settings.convertDtsToAc3;
                checkBoxMuxLowResSubs.IsChecked = settings.muxLowResSubs;
                checkBoxResize720p.IsChecked = settings.resize720p;
                checkBoxUse64BitX264.IsChecked = settings.use64bit;
                checkBoxUseDTSCore.IsChecked = settings.dtsHdCore;
                checkBoxMuxUntouchedSubs.IsChecked = settings.muxUntouchedSubs;
                checkBoxCopyUntouchedSubs.IsChecked = settings.copyUntouchedSubs;
            }
            catch (Exception)
            {
            }
        }

        private void UpdateAvisynthProfiles()
        {
            try
            {                
                comboBoxAvisynthProfile.Items.Clear();
                foreach (AvisynthSettings avs in settings.avisynthSettings)
                {
                    comboBoxAvisynthProfile.Items.Add(avs.desc);
                }
                if (settings.lastAvisynthProfile > -1 && settings.lastAvisynthProfile < settings.avisynthSettings.Count)
                {
                    comboBoxAvisynthProfile.SelectedIndex = settings.lastAvisynthProfile;
                }
                else
                {
                    settings.lastAvisynthProfile = -1;
                }
            }
            catch (Exception)
            {
            }
        }

        private void UpdateEncodingProfiles()
        {
            try
            {
                comboBoxEncodingProfile.Items.Clear();
                foreach(EncodingSettings es in settings.encodingSettings)
                {
                    comboBoxEncodingProfile.Items.Add(es.desc);
                }
                if(settings.lastProfile > -1 && settings.lastProfile < settings.encodingSettings.Count)
                {
                    comboBoxEncodingProfile.SelectedIndex = settings.lastProfile;
                }
                else
                {
                    settings.lastProfile = -1;
                }
            }
            catch (Exception)
            {
            }
        }

        private void UpdateMuxSettings()
        {
            try
            {
                if (settings.muxSubs > -1 && settings.muxSubs < comboBoxMuxSubtitles.Items.Count)
                {
                    comboBoxMuxSubtitles.SelectedIndex = settings.muxSubs;
                }
                else
                {
                    comboBoxMuxSubtitles.SelectedIndex = 0;
                }
                if (settings.copySubs > -1 && settings.copySubs < comboBoxCopySubtitles.Items.Count)
                {
                    comboBoxCopySubtitles.SelectedIndex = settings.copySubs;
                }
                else
                {
                    comboBoxCopySubtitles.SelectedIndex = 0;
                }
            }
            catch (Exception)
            {
            }
        }
        
        private void menuItemViewDemuxedStreams_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (menuItemViewDemuxedStreams.IsChecked)
                {
                    demuxedStreamsWindow.Show();
                }
                else
                {
                    demuxedStreamsWindow.Hide();
                }
            }
            catch (Exception)
            {
            }
        }

        private void menuItemFileExit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.Close();
            }
            catch (Exception)
            {
            }
        }

        private void abortThreads()
        {
            try
            {
                abort = true;
                if (sit != null)
                {
                    sit.Stop();
                    sit = null;
                }
                if (mit != null)
                {
                    mit.Stop();
                    mit = null;
                }                
                if (dt != null)
                {
                    dt.Stop();
                    dt = null;
                }
                if (st != null)
                {
                    st.Stop();
                    st = null;
                }
                if (it != null)
                {
                    it.Stop();
                    it = null;
                }
                if (et != null)
                {
                    et.Stop();
                    et = null;
                }
                if (mt != null)
                {
                    mt.Stop();
                    mt = null;
                }
            }
            catch (Exception)
            {
            }
        }

        private void menuItemViewReset_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                UserSettings settings = new UserSettings();

                this.Left = settings.bluripX;
                this.Top = settings.bluripY;
                this.Height = settings.bluripHeight;
                this.Width = settings.bluripWidth;

                logWindow.Left = settings.logX;
                logWindow.Top = settings.logY;
                logWindow.Width = settings.logWidth;
                logWindow.Height = settings.logHeight;
                menuItemViewLog.IsChecked = settings.showLog;
                
                demuxedStreamsWindow.Left = settings.demuxedStreamsX;
                demuxedStreamsWindow.Top = settings.demuxedStreamsY;
                demuxedStreamsWindow.Height = settings.demuxedStreamsHeight;
                demuxedStreamsWindow.Width = settings.demuxedStreamsWidth;
                menuItemViewDemuxedStreams.IsChecked = settings.showDemuxedStream;

                queueWindow.Left = settings.queueX;
                queueWindow.Top = settings.queueY;
                queueWindow.Height = settings.queueHeight;
                queueWindow.Width = settings.queueWidth;
                menuItemViewQueue.IsChecked = settings.showQueue;

                UpdateDiff();

                menuItemViewLog_Click(null, null);
                menuItemViewDemuxedStreams_Click(null, null);
                menuItemViewQueue_Click(null, null);
            }
            catch (Exception)
            {
            }
        }

        public void UpdateDiff()
        {
            try
            {
                diffLogX = logWindow.Left - this.Left;
                diffLogY = logWindow.Top - this.Top;
                diffDemuxedStreamsX = demuxedStreamsWindow.Left - this.Left;
                diffDemuxedStreamsY = demuxedStreamsWindow.Top - this.Top;
                diffQueueX = queueWindow.Left - this.Left;
                diffQueueY = queueWindow.Top - this.Top;
            }
            catch (Exception)
            {
            }
        }

        public void UpdateDiffLog()
        {
            try
            {
                diffLogX = logWindow.Left - this.Left;
                diffLogY = logWindow.Top - this.Top;
            }
            catch (Exception)
            {
            }
        }

        public void UpdateDiffQueue()
        {
            try
            {
                diffQueueX = queueWindow.Left - this.Left;
                diffQueueY = queueWindow.Top - this.Top;
            }
            catch (Exception)
            {
            }
        }

        public void UpdateDiffDemuxedStreams()
        {
            try
            {
                diffDemuxedStreamsX = demuxedStreamsWindow.Left - this.Left;
                diffDemuxedStreamsY = demuxedStreamsWindow.Top - this.Top;
            }
            catch (Exception)
            {
            }
        }

        private double diffLogX = 0;
        private double diffLogY = 0;
        private double diffDemuxedStreamsX = 0;
        private double diffDemuxedStreamsY = 0;
        private double diffQueueX = 0;
        private double diffQueueY = 0;

        private void windowMain_LocationChanged(object sender, EventArgs e)
        {
            try
            {
                if (menuItemViewSnap.IsChecked)
                {
                    logWindow.Left = this.Left + diffLogX;
                    logWindow.Top = this.Top + diffLogY;

                    demuxedStreamsWindow.Left = this.Left + diffDemuxedStreamsX;
                    demuxedStreamsWindow.Top = this.Top + diffDemuxedStreamsY;

                    queueWindow.Left = this.Left + diffQueueX;
                    queueWindow.Top = this.Top + diffQueueY;
                }
            }
            catch (Exception)
            {
            }
        }

        private void menuItemViewSnap_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                UpdateDiff();
            }
            catch (Exception)
            {
            }
        }

        private void buttonWorkingDirectory_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                FolderBrowserDialog fbd = new FolderBrowserDialog();

                try
                {
                    if (settings.workingDir != "")
                    {
                        DirectoryInfo di = Directory.GetParent(settings.workingDir);
                        fbd.SelectedPath = di.FullName;
                    }
                }
                catch (Exception)
                {
                }

                if (fbd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    textBoxWorkingDirectory.Text = fbd.SelectedPath;
                    settings.workingDir = fbd.SelectedPath;
                }
            }
            catch (Exception)
            {
            }
        }

        private void buttonTargetDirectory_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                FolderBrowserDialog fbd = new FolderBrowserDialog();

                try
                {
                    if (settings.targetFolder != "")
                    {
                        DirectoryInfo di = Directory.GetParent(settings.targetFolder);
                        fbd.SelectedPath = di.FullName;
                    }
                }
                catch (Exception)
                {
                }

                if (fbd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    textBoxTargetDirectory.Text = fbd.SelectedPath;
                    settings.targetFolder = fbd.SelectedPath;
                }
            }
            catch (Exception)
            {
            }
        }

        public enum EXECUTION_STATE : uint
        {
            ES_SYSTEM_REQUIRED = 0x00000001,
            ES_DISPLAY_REQUIRED = 0x00000002,
            // Legacy flag, should not be used.
            // ES_USER_PRESENT   = 0x00000004,
            ES_CONTINUOUS = 0x80000000,
        }

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        static extern EXECUTION_STATE SetThreadExecutionState(EXECUTION_STATE esFlags);

        public void DisableControls()
        {
            try
            {
                menuItemFile.IsEnabled = false;
                menuItemSettings.IsEnabled = false;
                tabControlMain.IsEnabled = false;
                demuxedStreamsWindow.IsEnabled = false;

                progressBarMain.Visibility = Visibility.Visible;
                buttonAbort.Visibility = Visibility.Visible;
                queueWindow.DisableControls();

                SetThreadExecutionState(EXECUTION_STATE.ES_CONTINUOUS | EXECUTION_STATE.ES_SYSTEM_REQUIRED);
            }
            catch (Exception)
            {
            }
        }

        public void EnableControls()
        {
            try
            {
                menuItemFile.IsEnabled = true;
                menuItemSettings.IsEnabled = true;
                tabControlMain.IsEnabled = true;
                demuxedStreamsWindow.IsEnabled = true;

                progressBarMain.Visibility = Visibility.Hidden;
                buttonAbort.Visibility = Visibility.Hidden;
                queueWindow.EnableControls();

                SetThreadExecutionState(EXECUTION_STATE.ES_CONTINUOUS);
            }
            catch (Exception)
            {
            }
        }

        private void menuItemViewQueue_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (menuItemViewQueue.IsChecked)
                {
                    queueWindow.Show();
                }
                else
                {
                    queueWindow.Hide();
                }
            }
            catch (Exception)
            {
            }
        }

        private void textBoxFilePrefix_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                settings.filePrefix = textBoxFilePrefix.Text;
            }
            catch (Exception)
            {
            }
        }

        private void textBoxTargetFilename_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                settings.targetFilename = textBoxTargetFilename.Text;
            }
            catch (Exception)
            {
            }
        }

        private void textBoxMovieTitle_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                settings.movieTitle = textBoxMovieTitle.Text;
            }
            catch (Exception)
            {
            }
        }

        private void EnableExpert()
        {
            try
            {
                buttonOnlyDemux.Visibility = System.Windows.Visibility.Visible;
                buttonOnlyCrop.Visibility = System.Windows.Visibility.Visible;
                buttonOnlySubtitle.Visibility = System.Windows.Visibility.Visible;
                buttonOnlyEncode.Visibility = System.Windows.Visibility.Visible;
                buttonOnlyMux.Visibility = System.Windows.Visibility.Visible;
                groupBoxExpertSettings.Visibility = System.Windows.Visibility.Visible;
                labelEncodedMovieDir.Visibility = System.Windows.Visibility.Visible;
                textBoxEncodedMovieDir.Visibility = System.Windows.Visibility.Visible;
                buttonEncodedMovieDir.Visibility = System.Windows.Visibility.Visible;
            }
            catch (Exception)
            {
            }
        }

        private void DisableExpert()
        {
            try
            {
                buttonOnlyDemux.Visibility = System.Windows.Visibility.Hidden;
                buttonOnlyCrop.Visibility = System.Windows.Visibility.Hidden;
                buttonOnlySubtitle.Visibility = System.Windows.Visibility.Hidden;
                buttonOnlyEncode.Visibility = System.Windows.Visibility.Hidden;
                buttonOnlyMux.Visibility = System.Windows.Visibility.Hidden;
                groupBoxExpertSettings.Visibility = System.Windows.Visibility.Hidden;
                labelEncodedMovieDir.Visibility = System.Windows.Visibility.Hidden;
                textBoxEncodedMovieDir.Visibility = System.Windows.Visibility.Hidden;
                buttonEncodedMovieDir.Visibility = System.Windows.Visibility.Hidden;
            }
            catch (Exception)
            {
            }
        }

        private void menuItemViewExpertMode_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (menuItemViewExpertMode.IsChecked)
                {
                    EnableExpert();
                }
                else
                {
                    DisableExpert();
                }
            }
            catch (Exception)
            {
            }
        }
        
        private void buttonAbort_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (System.Windows.MessageBox.Show(Global.Res("MessageAbort"), Global.Res("MessageAbortHeader"), MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    abortThreads();
                }
            }
            catch (Exception)
            {
            }
        }

        private void comboBoxEncodingProfile_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (comboBoxEncodingProfile.SelectedIndex > -1) settings.lastProfile = comboBoxEncodingProfile.SelectedIndex;
            }
            catch (Exception)
            {
            }
        }

        private void menuItemLanguageEnglish_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (menuItemLanguageEnglish.IsChecked)
                {
                    menuItemLanguageGerman.IsChecked = false;
                    settings.language = "en";
                    UpdateDictionary("Translation/en.xaml");
                    UpdateFromSettings(false);
                }
            }
            catch (Exception)
            {
            }
        }

        private void menuItemLanguageGerman_Click(object sender, RoutedEventArgs e)
        {
            if (menuItemLanguageGerman.IsChecked)
            {
                menuItemLanguageEnglish.IsChecked = false;
                settings.language = "de";
                UpdateDictionary("Translation/de.xaml");
                UpdateFromSettings(false);
            }
        }

        private bool checkComplete()
        {
            try
            {
                // check path/settings here, too

                if (!checkEac3to()) return false;
                int supCount = 0;
                if (comboBoxTitle.SelectedIndex > -1)
                {
                    foreach (StreamInfo si in titleList[comboBoxTitle.SelectedIndex].streams)
                    {
                        if (si.streamType == StreamType.Subtitle)
                        {
                            supCount++;
                        }
                    }
                }
                if (!checkIndex()) return false;
                if (supCount > 0)
                {
                    if (!checkBdsup2sub()) return false;
                }
                if (!settings.untouchedVideo)
                {
                    if (!checkX264()) return false;
                }
                if (!checkMkvmerge()) return false;
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private bool hasAvsValue()
        {
            try
            {
                if (demuxedStreamList.streams.Count == 0)
                {
                    return false;
                }
                foreach (StreamInfo si in demuxedStreamList.streams)
                {
                    if (si.streamType == StreamType.Video)
                    {
                        if (si.extraFileInfo.GetType() == typeof(VideoFileInfo))
                        {
                            if (((VideoFileInfo)si.extraFileInfo).encodeAvs != "") return true;
                        }
                        else return false;
                    }
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private bool hasOutputVideoValue()
        {
            try
            {
                if (demuxedStreamList.streams.Count == 0)
                {
                    return false;
                }
                foreach (StreamInfo si in demuxedStreamList.streams)
                {
                    if (si.streamType == StreamType.Video)
                    {
                        if (si.extraFileInfo.GetType() == typeof(VideoFileInfo))
                        {
                            if (((VideoFileInfo)si.extraFileInfo).encodedFile != "") return true;
                        }
                        else return false;
                    }
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private bool hasFpsValue()
        {
            try
            {
                if (demuxedStreamList.streams.Count == 0)
                {
                    return false;
                }
                foreach (StreamInfo si in demuxedStreamList.streams)
                {
                    if (si.streamType == StreamType.Video)
                    {
                        if (si.extraFileInfo.GetType() == typeof(VideoFileInfo))
                        {
                            if (((VideoFileInfo)si.extraFileInfo).fps != "") return true;
                        }
                        else return false;
                    }
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private bool StartAll()
        {
            try
            {
                if (!checkComplete()) return false;

                if (demuxedStreamList.streams.Count == 0)
                {
                    if (!DoDemux()) return false;
                    if (!DoIndex()) return false;
                    if (!DoSubtitle()) return false;
                    if (!settings.untouchedVideo)
                    {
                        if (!DoEncode()) return false;
                    }
                    if (!DoMux()) return false;
                }
                else
                {
                    if (!hasOutputVideoValue())
                    {
                        if (!hasAvsValue() || !hasFpsValue())
                        {
                            if (!DoIndex()) return false;
                        }
                        if (!DoSubtitle()) return false;
                        if (!DoEncode()) return false;
                        if (!DoMux()) return false;
                    }
                    else
                    {
                        if (!DoIndex()) return false;
                        if (!DoSubtitle()) return false;
                        if (!DoMux()) return false;
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                logWindow.MessageMain(Global.Res("ErrorException") + " " + ex.Message);
                return false;
            }
            finally
            {
                logWindow.SaveMainLog(settings.workingDir + "\\" + settings.targetFilename + "_completeLog.txt");
            }
        }

        private void buttonStart_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                StartAll();

                if (queueWindow.checkBoxQueueShutdown.IsChecked == true)
                {
                    ShutdownWindow sw = new ShutdownWindow();
                    sw.ShowDialog();
                    if (sw.DialogResult == true)
                    {
                        System.Diagnostics.Process.Start("ShutDown", "-s -f");
                    }
                }
            }
            catch (Exception ex)
            {
                logWindow.MessageMain(Global.Res("ErrorException") + " " + ex.Message);
            }
        }

        private void menuItemSettingsProfiles_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                EditEncodingProfilesWindow epw = new EditEncodingProfilesWindow(settings);
                epw.ShowDialog();
                if (epw.DialogResult == true)
                {
                    settings = new UserSettings(epw.UserSettings);
                    UpdateEncodingProfiles();
                }
            }
            catch (Exception)
            {
            }
        }

        private void menuItemSettingsAdvanced_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bool expert = false;
                if (menuItemViewExpertMode.IsChecked) expert = true;

                AdvancedOptionsWindow aow = new AdvancedOptionsWindow(settings, expert, dtsBitrates, ac3Bitrates);
                aow.ShowDialog();
                if (aow.DialogResult == true)
                {
                    settings = new UserSettings(aow.UserSettings);
                }
            }
            catch (Exception)
            {
            }
        }

        private void menuItemSettingsAvisynthProfiles_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                EditAvisynthProfilesWindow eapw = new EditAvisynthProfilesWindow(settings);
                eapw.ShowDialog();
                if (eapw.DialogResult == true)
                {
                    settings = new UserSettings(eapw.UserSettings);
                    UpdateAvisynthProfiles();
                }
            }
            catch (Exception)
            {
            }
        }

        private void menuItemHelpLinks_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                LinkWindow lw = new LinkWindow();
                lw.ShowDialog();
            }
            catch (Exception)
            {
            }
        }

        private void menuItemHelpAbout_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                AboutWindow aw = new AboutWindow();
                aw.ShowDialog();
            }
            catch (Exception)
            {
            }
        }

        private void comboBoxMuxSubtitles_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (comboBoxMuxSubtitles.SelectedIndex > -1)
                {
                    settings.muxSubs = comboBoxMuxSubtitles.SelectedIndex;
                }
            }
            catch (Exception)
            {
            }
        }

        private void comboBoxCopySubtitles_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (comboBoxCopySubtitles.SelectedIndex > -1)
                {
                    settings.copySubs = comboBoxCopySubtitles.SelectedIndex;
                }
            }
            catch (Exception)
            {
            }
        }

        private void comboBoxCropInput_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (comboBoxCropInput.SelectedIndex > -1)
                {
                    settings.cropInput = comboBoxCropInput.SelectedIndex;
                }
            }
            catch (Exception)
            {
            }
        }

        private void comboBoxEncodeInput_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (comboBoxEncodeInput.SelectedIndex > -1)
                {
                    settings.encodeInput = comboBoxEncodeInput.SelectedIndex;
                }
            }
            catch (Exception)
            {
            }
        }

        private void checkBoxUntouchedVideo_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                settings.untouchedVideo = checkBoxUntouchedVideo.IsChecked.Value;
                if (settings.untouchedVideo)
                {
                    comboBoxEncodingProfile.IsEnabled = false;
                    comboBoxCropInput.IsEnabled = false;
                    comboBoxEncodeInput.IsEnabled = false;
                }
                else
                {
                    comboBoxEncodingProfile.IsEnabled = true;
                    comboBoxCropInput.IsEnabled = true;
                    comboBoxEncodeInput.IsEnabled = true;
                }
            }
            catch (Exception)
            {
            }
        }

        private void checkBoxUntouchedAudio_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                settings.untouchedAudio = checkBoxUntouchedAudio.IsChecked.Value;
            }
            catch (Exception)
            {
            }
        }

        private void checkBoxConvertDtsToAc3_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                settings.convertDtsToAc3 = checkBoxConvertDtsToAc3.IsChecked.Value;
            }
            catch (Exception)
            {
            }
        }

        private void checkBoxResize720p_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                settings.resize720p = checkBoxResize720p.IsChecked.Value;
            }
            catch (Exception)
            {
            }
        }

        private void checkBoxUseDTSCore_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                settings.dtsHdCore = checkBoxUseDTSCore.IsChecked.Value;
            }
            catch (Exception)
            {
            }
        }

        private void checkBoxUse64BitX264_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                settings.use64bit = checkBoxUse64BitX264.IsChecked.Value;
            }
            catch (Exception)
            {
            }
        }

        private void checkBoxMuxLowResSubs_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                settings.muxLowResSubs = checkBoxMuxLowResSubs.IsChecked.Value;
            }
            catch (Exception)
            {
            }
        }

        private void comboBoxAvisynthProfile_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (comboBoxAvisynthProfile.SelectedIndex > -1)
                {
                    settings.lastAvisynthProfile = comboBoxAvisynthProfile.SelectedIndex;
                }
            }
            catch (Exception)
            {
            }
        }

        private void checkBoxMuxUntouchedSubs_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                settings.muxUntouchedSubs = checkBoxMuxUntouchedSubs.IsChecked.Value;
                if (settings.muxUntouchedSubs)
                {
                    comboBoxMuxSubtitles.IsEnabled = false;
                }
                else
                {
                    comboBoxMuxSubtitles.IsEnabled = true;
                }
            }
            catch (Exception)
            {
            }
        }

        private void checkBoxCopyUntouchedSubs_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                settings.copyUntouchedSubs = checkBoxCopyUntouchedSubs.IsChecked.Value;
            }
            catch (Exception)
            {
            }
        }

        private void menuItemFileSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Project project = new Project(settings, demuxedStreamList, titleList, comboBoxTitle.SelectedIndex, m2tsList);
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = Global.Res("ProjectFileFilter");
                if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    Project.SaveProjectFile(project, sfd.FileName);
                }
            }
            catch (Exception)
            {
            }
        }

        private void menuItemFileOpen_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                OpenFileDialog ofd = new OpenFileDialog();
                ofd.Filter = Global.Res("ProjectFileFilter");
                if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    Project project = new Project();
                    if (Project.LoadProjectFile(ref project, ofd.FileName))
                    {
                        settings = new UserSettings(project.settings);
                        titleList.Clear();
                        foreach (TitleInfo ti in project.titleList)
                        {
                            titleList.Add(new TitleInfo(ti));
                        }
                        UpdateTitleList();
                        comboBoxTitle.SelectedIndex = project.titleIndex;
                        demuxedStreamList = new TitleInfo(project.demuxedStreamList);

                        m2tsList.Clear();
                        foreach (string s in project.m2tsList)
                        {
                            m2tsList.Add(s);
                        }

                        UpdateFromSettings(true);
                        demuxedStreamsWindow.UpdateDemuxedStreams();
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        public List<Project> ProjectQueue
        {
            get { return projectQueue; }
            set { projectQueue = value; }
        }

        private void buttonQueue_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Project project = new Project(settings, demuxedStreamList, titleList, comboBoxTitle.SelectedIndex, m2tsList);
                projectQueue.Add(project);
                queueWindow.UpdateQueue();
            }
            catch (Exception)
            {
            }
        }

        public void ProcessQueue()
        {
            try
            {
                silent = true;
                abort = false;

                foreach (Project project in projectQueue)
                {
                    logWindow.ClearAll();

                    logWindow.MessageMain(Global.ResFormat("InfoQueueStart", (projectQueue.IndexOf(project) + 1), projectQueue.Count));

                    if (!abort)
                    {
                        logWindow.MessageMain(Global.ResFormat("InfoQueueProjectStart", project.settings.movieTitle));
                        settings = new UserSettings(project.settings);
                        titleList.Clear();
                        foreach (TitleInfo ti in project.titleList)
                        {
                            titleList.Add(new TitleInfo(ti));
                        }
                        UpdateTitleList();
                        comboBoxTitle.SelectedIndex = project.titleIndex;
                        demuxedStreamList = new TitleInfo(project.demuxedStreamList);

                        m2tsList.Clear();
                        foreach (string s in project.m2tsList)
                        {
                            m2tsList.Add(s);
                        }

                        UpdateFromSettings(true);
                        demuxedStreamsWindow.UpdateDemuxedStreams();

                        StartAll();
                    }
                    else
                    {
                        logWindow.MessageMain(Global.Res("InfoQueueCancel"));
                    }
                    logWindow.MessageMain(Global.Res("InfoQueueDone"));
                }                

                if (queueWindow.checkBoxQueueShutdown.IsChecked == true)
                {
                    ShutdownWindow sw = new ShutdownWindow();
                    sw.ShowDialog();
                    if (sw.DialogResult == true)
                    {
                        System.Diagnostics.Process.Start("ShutDown", "-s -f");
                    }
                }
            }
            catch (Exception ex)
            {
                logWindow.MessageMain(Global.Res("ErrorException") + " " + ex.Message);
            }
            finally
            {
                silent = false;
            }
        }

        private void buttonResetStreams_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                foreach (TitleInfo ti in titleList)
                {
                    foreach (StreamInfo si in ti.streams)
                    {
                        si.selected = false;
                    }
                }
                UpdateStreamList();
            }
            catch (Exception)
            {
            }
        }

        private void menuEditAdvancedOptions_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (comboBoxTitle.SelectedIndex > -1 && listBoxStreams.SelectedIndex > -1)
                {
                    StreamInfo si = titleList[comboBoxTitle.SelectedIndex].streams[listBoxStreams.SelectedIndex];
                    if (si.streamType == StreamType.Audio)
                    {
                        AdvancedAudioOptions aao = null;
                        if (si.advancedOptions.GetType() != typeof(AdvancedAudioOptions))
                        {
                            aao = new AdvancedAudioOptions();
                        }
                        else
                        {
                            aao = new AdvancedAudioOptions(si.advancedOptions);
                        }
                        AdvancedAudioOptionsWindow aaow = new AdvancedAudioOptionsWindow(aao);
                        aaow.ShowDialog();
                        if (aaow.DialogResult == true)
                        {
                            int index = listBoxStreams.SelectedIndex;
                            titleList[comboBoxTitle.SelectedIndex].streams[index].advancedOptions =
                                new AdvancedAudioOptions(aaow.advancedAudioOptions);
                            UpdateStreamList();
                            listBoxStreams.SelectedIndex = index;
                        }
                    }
                    else if (si.streamType == StreamType.Video)
                    {
                        AdvancedVideoOptions avo = null;
                        if (si.advancedOptions.GetType() != typeof(AdvancedVideoOptions))
                        {
                            avo = new AdvancedVideoOptions();
                        }
                        else
                        {
                            avo = new AdvancedVideoOptions(si.advancedOptions);
                        }
                        AdvancedVideoOptionsWindow avow = new AdvancedVideoOptionsWindow(avo);
                        avow.ShowDialog();
                        if (avow.DialogResult == true)
                        {
                            int index = listBoxStreams.SelectedIndex;
                            titleList[comboBoxTitle.SelectedIndex].streams[index].advancedOptions =
                                new AdvancedVideoOptions(avow.advancedVideoOptions);
                            UpdateStreamList();
                            listBoxStreams.SelectedIndex = index;
                        }
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        private void menuDelAdvancedOptions_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (comboBoxTitle.SelectedIndex > -1 && listBoxStreams.SelectedIndex > -1)
                {
                    int index = listBoxStreams.SelectedIndex;
                    titleList[comboBoxTitle.SelectedIndex].streams[index].advancedOptions = new AdvancedOptions();
                    UpdateStreamList();
                    listBoxStreams.SelectedIndex = index;
                }
            }
            catch (Exception)
            {
            }
        }

        private void UpdateDictionary(ResourceDictionary rdTarget)
        {
            try
            {
                foreach (ResourceDictionary rdSrc in System.Windows.Application.Current.Resources.MergedDictionaries)
                {
                    foreach (object oSrc in rdSrc.Keys)
                    {
                        foreach (object oTarget in rdTarget.Keys)
                        {
                            if (oTarget.ToString() == oSrc.ToString())
                            {
                                rdSrc[oSrc] = rdTarget[oSrc];
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        private void UpdateDictionary(string resdict)
        {
            try
            {
                ResourceDictionary rd = new ResourceDictionary();
                rd.Source = new Uri("pack://application:,,,/" + resdict, UriKind.Absolute);
                UpdateDictionary(rd);
            }
            catch (Exception)
            {
            }
        }

        private void menuItemViewSkinBlu_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (menuItemViewSkinBlu.IsChecked)
                {
                    menuItemViewSkinClassic.IsChecked = false;
                    settings.skin = "blu";
                    UpdateDictionary("Style/style.xaml");
                }
            }
            catch (Exception)
            {
            }
        }

        private void menuItemViewSkinClassic_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (menuItemViewSkinClassic.IsChecked)
                {
                    menuItemViewSkinBlu.IsChecked = false;
                    settings.skin = "classic";
                    UpdateDictionary("Style/classic.xaml");
                }
            }
            catch (Exception)
            {
            }
        }

        private void textBoxWorkingDirectory_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                settings.workingDir = textBoxWorkingDirectory.Text;
            }
            catch (Exception)
            {
            }
        }

        private void textBoxTargetDirectory_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                settings.targetFolder = textBoxTargetDirectory.Text;
            }
            catch (Exception)
            {
            }
        }

        private void textBoxEncodedMovieDir_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                settings.encodedMovieDir = textBoxEncodedMovieDir.Text;
            }
            catch (Exception)
            {
            }
        }
    }

    public static class Global
    {
        public static string Res(string key)
        {
            try
            {
                string tmp = (string)App.Current.Resources[key];
                if (tmp != "") return tmp;
                else return "Unknown resource";
            }
            catch (Exception)
            {
                return "Unknown resource";
            }
        }

        public static string ResFormat(string key, params object[] para)
        {
            try
            {
                string tmp = String.Format((string)App.Current.Resources[key], para);
                if (tmp != "") return tmp;
                else return "Unknown resource";
            }
            catch (Exception)
            {
                return "Unknown resource";
            }
        }

        public static void ErrorMsg(string msg)
        {
            try
            {
                System.Windows.MessageBox.Show(msg, Res("ErrorHeader"), MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception)
            {
            }
        }

        public static void ErrorMsg(Exception ex)
        {
            try
            {
                string tmp = " (";
                if (ex.InnerException != null)
                {
                    tmp += ex.InnerException.Message;
                }
                tmp += ")";
                System.Windows.MessageBox.Show(ex.Message + tmp, Res("ErrorHeader"), MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception)
            {
            }
        }        
    }
}
