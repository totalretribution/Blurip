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
using System.Windows.Shapes;
using System.Windows.Forms;

namespace BluRip
{
    /// <summary>
    /// Interaktionslogik für DemuxedStreamsWindow.xaml
    /// </summary>
    public partial class DemuxedStreamsWindow : Window
    {
        private MainWindow mainWindow = null;

        public DemuxedStreamsWindow(MainWindow mainWindow)
        {            
            try
            {
                InitializeComponent();
                this.mainWindow = mainWindow;
            }
            catch(Exception ex)
            {
                Global.ErrorMsg(ex.Message);
            }
        }

        public void UpdateDemuxedStreams()
        {
            try
            {
                listBoxDemuxedStreams.Items.Clear();
                foreach (StreamInfo si in mainWindow.DemuxedStreams.streams)
                {
                    listBoxDemuxedStreams.Items.Add(si.typeDesc + " - " + si.filename);
                }
            }
            catch (Exception)
            {
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            try
            {
                e.Cancel = true;
                this.Hide();
            }
            catch (Exception)
            {
            }
        }

        private void buttonLoadDemuxedStreams_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                OpenFileDialog ofd = new OpenFileDialog();
                ofd.Filter = "*_streamInfo.xml|*.xml";
                if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    TitleInfo ti = new TitleInfo();
                    TitleInfo.LoadSettingsFile(ref ti, ofd.FileName);
                    mainWindow.DemuxedStreams = new TitleInfo(ti);
                    UpdateDemuxedStreams();
                }
            }
            catch (Exception)
            {
            }
        }

        private void Window_LocationChanged(object sender, EventArgs e)
        {
            try
            {
                if(IsActive)
                    mainWindow.UpdateDiffDemuxedStreams();
            }
            catch (Exception)
            {
            }
        }
    }
}
