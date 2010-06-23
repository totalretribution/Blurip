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

        public string title = "BluRip v0.5.0 © _hawk_";

        private LogWindow logWindow = null;
        private DemuxedStreamsWindow demuxedStreamsWindow = null;
        private bool silent = false;
        private bool abort = false;

        public MainWindow()
        {
            InitializeComponent();
            try
            {
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

                /*
                comboBoxX264Priority.Items.Clear();
                foreach (string s in Enum.GetNames(typeof(ProcessPriorityClass)))
                {
                    comboBoxX264Priority.Items.Add(s);
                }
                */
            }
            catch (Exception)
            {
            }
        }
        
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                //CheckForIllegalCrossThreadCalls = false;
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

                demuxedStreamsWindow = new DemuxedStreamsWindow(this);
                demuxedStreamsWindow.Owner = this;

                UpdateFromSettings();

                UpdateStatus((string)App.Current.Resources["StatusBar"] + " " + (string)App.Current.Resources["StatusBarReady"]);
            }
            catch (Exception)
            {
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
        
        private void UpdateStatus(string msg)
        {
            try
            {
                statusBarItemMain.Content = msg;
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
                settings.showDemuxedStream = menuItemViewDemuxedStreams.IsChecked;
                settings.demuxedStreamsX = demuxedStreamsWindow.Left;
                settings.demuxedStreamsY = demuxedStreamsWindow.Top;

                settings.showLog = menuItemViewLog.IsChecked;
                settings.logX = logWindow.Left;
                settings.logY = logWindow.Top;

                settings.bluripX = this.Left;
                settings.bluripY = this.Top;

                UserSettings.SaveSettingsFile(settings, settingsPath);
                if (!silent)
                {
                    if (System.Windows.MessageBox.Show((string)App.Current.Resources["MessageExit"], (string)App.Current.Resources["MessageExitHeader"], MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
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

        private void UpdateFromSettings()
        {
            try
            {
                textBoxBlurayPath.Text = settings.lastBluRayPath;

                this.Left = settings.bluripX;
                this.Top = settings.bluripY;

                logWindow.Left = settings.logX;
                logWindow.Top = settings.logY;
                menuItemViewDemuxedStreams.IsChecked = settings.showDemuxedStream;

                demuxedStreamsWindow.Left = settings.demuxedStreamsX;
                demuxedStreamsWindow.Top = settings.demuxedStreamsY;
                menuItemViewLog.IsChecked = settings.showLog;

                menuItemViewSnap.IsChecked = settings.snap;

                UpdateDiff();

                menuItemViewLog_Click(null, null);
                menuItemViewDemuxedStreams_Click(null, null);
            }
            catch (Exception)
            {
            }
        }
        
        private void ErrorMsg(string msg)
        {
            try
            {
                System.Windows.MessageBox.Show(msg, (string)App.Current.Resources["ErrorHeader"], MessageBoxButton.OK, MessageBoxImage.Error);
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
                /*
                if (dt != null)
                {
                    dt.Stop();
                    dt = null;
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
                */
            }
            catch (Exception)
            {
            }
        }

        private void menuItemViewReset_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                settings.snap = false;
                settings.showLog = false;
                settings.logX = 80;
                settings.logY = 80;

                settings.showDemuxedStream = false;
                settings.demuxedStreamsX = 100;
                settings.demuxedStreamsY = 100;

                settings.bluripX = 120;
                settings.bluripY = 120;

                this.Left = settings.bluripX;
                this.Top = settings.bluripY;

                logWindow.Left = settings.logX;
                logWindow.Top = settings.logY;
                menuItemViewDemuxedStreams.IsChecked = settings.showDemuxedStream;

                demuxedStreamsWindow.Left = settings.demuxedStreamsX;
                demuxedStreamsWindow.Top = settings.demuxedStreamsY;
                menuItemViewLog.IsChecked = settings.showLog;

                UpdateDiff();

                menuItemViewLog_Click(null, null);
                menuItemViewDemuxedStreams_Click(null, null);
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
    }
}
