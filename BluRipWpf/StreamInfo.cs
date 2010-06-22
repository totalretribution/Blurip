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
        private StreamInfoTool sit = null;
        private M2tsInfoTool mit = null;

        private List<TitleInfo> titleList = new List<TitleInfo>();
        private List<string> m2tsList = new List<string>();
        private TitleInfo demuxedStreamList = new TitleInfo();

        private void DemuxMsg(object sender, ExternalTool.MsgEventArgs e)
        {
            logWindow.MessageDemux(e.Message.Replace("\b", "").Trim());
        }

        private void UpdateTitleList()
        {
            try
            {
                comboBoxTitle.Items.Clear();
                listBoxStreams.Items.Clear();

                foreach (TitleInfo ti in titleList)
                {
                    comboBoxTitle.Items.Add(ti.desc);
                }
                if (titleList.Count > 0)
                {
                    comboBoxTitle.SelectedIndex = 0;
                }
            }
            catch (Exception)
            {
            }
        }

        private void buttonGetStreamInfo_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //if (!checkEac3to()) return;
                //this.Text = title + " [Getting stream info...]";
                UpdateStatus((string)App.Current.Resources["StatusBar"] + " " + (string)App.Current.Resources["StatusBarStreamInfo"]);
                progressBarMain.Visibility = Visibility.Visible;
                buttonAbort.Visibility = Visibility.Visible;

                comboBoxTitle.Items.Clear();
                listBoxStreams.Items.Clear();
                m2tsList.Clear();

                sit = new StreamInfoTool(settings, ref titleList, settings.lastBluRayPath, videoTypes, ac3AudioTypes, dtsAudioTypes);
                sit.OnInfoMsg += new ExternalTool.InfoEventHandler(DemuxMsg);
                sit.OnLogMsg += new ExternalTool.LogEventHandler(DemuxMsg);
                sit.Start();
                sit.WaitForExit();
                if (!sit.Successfull)
                {
                    titleList.Clear();
                }

                UpdateTitleList();

                demuxedStreamList = new TitleInfo();
                //UpdateDemuxedStreams();
            }
            catch (Exception ex)
            {
                logWindow.MessageDemux("Exception: " + ex.Message);
            }
            finally
            {
                progressBarMain.Visibility = Visibility.Hidden;
                buttonAbort.Visibility = Visibility.Hidden;
                UpdateStatus((string)App.Current.Resources["StatusBar"] + " " +(string)App.Current.Resources["StatusBarReady"]);
                //this.Text = title;
            }
        }

        /*
        private void buttonOpenM2ts_Click(object sender, EventArgs e)
        {
            try
            {
                if (!checkEac3to()) return;
                FileListForm flf = new FileListForm();
                if (flf.ShowDialog() == DialogResult.OK)
                {
                    comboBoxTitle.Items.Clear();
                    checkedListBoxStreams.Items.Clear();

                    titleList.Clear();
                    m2tsList.Clear();
                    foreach (string s in flf.fileList)
                    {
                        m2tsList.Add(s);
                    }
                    DoM2tsInfo();
                }
            }
            catch (Exception)
            {
            }
        }

        private void DoM2tsInfo()
        {
            try
            {
                this.Text = title + " [Getting m2ts stream info...]";

                progressBarMain.Visible = true;
                buttonAbort.Visible = true;

                comboBoxTitle.Items.Clear();
                checkedListBoxStreams.Items.Clear();

                mit = new M2tsInfoTool(settings, ref titleList, m2tsList, videoTypes, ac3AudioTypes, dtsAudioTypes);
                mit.OnInfoMsg += new ExternalTool.InfoEventHandler(DemuxMsg);
                mit.OnLogMsg += new ExternalTool.LogEventHandler(DemuxMsg);
                mit.Start();
                mit.WaitForExit();
                if (!mit.Successfull)
                {
                    titleList.Clear();
                }

                UpdateTitleList();

                demuxedStreamList = new TitleInfo();
                UpdateDemuxedStreams();
            }
            catch (Exception ex)
            {
                MessageDemux("Exception: " + ex.Message);
            }
            finally
            {
                progressBarMain.Visible = false;
                buttonAbort.Visible = false;
                this.Text = title;
            }
        }
       */
    }
}