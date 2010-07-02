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
    /// Interaktionslogik für ExternalTools.xaml
    /// </summary>
    public partial class ExternalTools : Window
    {
        private UserSettings settings = null;

        public ExternalTools(UserSettings settings)
        {            
            try
            {
                InitializeComponent();
                this.settings = new UserSettings(settings);
                
                textBoxEac3toPath.Text = settings.eac3toPath;
                textBoxBDSup2subPath.Text = settings.sup2subPath;
                textBoxJavaPath.Text = settings.javaPath;
                textBoxX264Path.Text = settings.x264Path;
                textBoxMkvmergePath.Text = settings.mkvmergePath;
                textBoxFfmsindexPath.Text = settings.ffmsindexPath;
                textBoxDgindexnvPath.Text = settings.dgindexnvPath;
                textBoxX264x64Path.Text = settings.x264x64Path;
                textBoxAvs2yuvPath.Text = settings.avs2yuvPath;
            }
            catch (Exception ex)
            {
                Global.ErrorMsg(ex);
            }
        }

        private void buttonOk_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DialogResult = true;
            }
            catch (Exception)
            {
            }
        }

        public UserSettings userSettings
        {
            get { return settings; }
        }

        private void buttonEac3toPath_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                OpenFileDialog ofd = new OpenFileDialog();
                ofd.Filter = "eac3to.exe|eac3to.exe";
                if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    textBoxEac3toPath.Text = ofd.FileName;
                    settings.eac3toPath = ofd.FileName;
                }
            }
            catch (Exception)
            {
            }
        }

        private void buttonBDSup2subPath_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                OpenFileDialog ofd = new OpenFileDialog();
                ofd.Filter = "BDSup2Sub.jar|BDSup2Sub.jar";
                if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    textBoxBDSup2subPath.Text = ofd.FileName;
                    settings.sup2subPath = ofd.FileName;
                }
            }
            catch (Exception)
            {
            }
        }

        private void buttonX264Path_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                OpenFileDialog ofd = new OpenFileDialog();
                ofd.Filter = "x264.exe|x264.exe";
                if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    textBoxX264Path.Text = ofd.FileName;
                    settings.x264Path = ofd.FileName;
                }
            }
            catch (Exception)
            {
            }
        }

        private void buttonJavaPath_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                OpenFileDialog ofd = new OpenFileDialog();
                ofd.Filter = "java.exe|java.exe";
                if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    textBoxJavaPath.Text = ofd.FileName;
                    settings.javaPath = ofd.FileName;
                }
            }
            catch (Exception)
            {
            }
        }

        private void buttonMkvmergePath_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                OpenFileDialog ofd = new OpenFileDialog();
                ofd.Filter = "mkvmerge.exe|mkvmerge.exe";
                if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    textBoxMkvmergePath.Text = ofd.FileName;
                    settings.mkvmergePath = ofd.FileName;
                }
            }
            catch (Exception)
            {
            }
        }

        private void buttonFfmsindexPath_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "ffmsindex.exe|ffmsindex.exe";
            if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                textBoxFfmsindexPath.Text = ofd.FileName;
                settings.ffmsindexPath = ofd.FileName;
            }
        }

        private void buttonDgindexnvPath_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "DGIndexNv.exe|DGIndexNv.exe";
            if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                textBoxDgindexnvPath.Text = ofd.FileName;
                settings.dgindexnvPath = ofd.FileName;
            }
        }

        private void buttonX264x64Path_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "x264.exe|x264*.exe";
            if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                textBoxX264x64Path.Text = ofd.FileName;
                settings.x264x64Path = ofd.FileName;
            }
        }

        private void buttonAvs2yuvPath_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "avs2yuv.exe|avs2yuv.exe";
            if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                textBoxAvs2yuvPath.Text = ofd.FileName;
                settings.avs2yuvPath = ofd.FileName;
            }
        }
    }
}
