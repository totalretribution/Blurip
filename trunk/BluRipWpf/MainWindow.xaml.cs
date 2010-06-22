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
                //UpdateFromSettings();
                logWindow = new LogWindow();
                logWindow.Owner = this;

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

        private void buttonBluRayPath_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                FolderBrowserDialog fbd = new FolderBrowserDialog();
                if (fbd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    textBoxBlurayPath.Text = fbd.SelectedPath;
                    settings.lastBluRayPath = fbd.SelectedPath;
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
    }
}
