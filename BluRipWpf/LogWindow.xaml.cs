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

namespace BluRip
{
    /// <summary>
    /// Interaktionslogik für LogWindow.xaml
    /// </summary>
    public partial class LogWindow : Window
    {
        private delegate void Message(string msg);
        private MainWindow mainWindow = null;

        public LogWindow(MainWindow mainWindow)
        {
            InitializeComponent();
            try
            {
                this.mainWindow = mainWindow;
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

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            
        }

        public void MessageMain(string msg)
        {
            try
            {
                if (richTextBoxMainLog.Dispatcher.CheckAccess())
                {
                    richTextBoxMainLog.AppendText("[" + DateTime.Now.ToString() + "] " + msg + "\r");
                    richTextBoxMainLog.ScrollToEnd();
                }
                else
                {
                    richTextBoxMainLog.Dispatcher.Invoke(new Message(MessageMain), new object[] { msg });
                }                
            }
            catch (Exception)
            {
            }
        }

        public void MessageDemux(string msg)
        {
            try
            {
                if (richTextBoxDemuxLog.Dispatcher.CheckAccess())
                {
                    richTextBoxDemuxLog.AppendText("[" + DateTime.Now.ToString() + "] " + msg + "\r");
                    richTextBoxDemuxLog.ScrollToEnd();
                    MessageMain(msg);
                }
                else
                {
                    richTextBoxDemuxLog.Dispatcher.Invoke(new Message(MessageDemux), new object[] { msg });
                }
                
            }
            catch (Exception)
            {
            }
        }

        public void MessageCrop(string msg)
        {
            try
            {
                if (richTextBoxCropLog.Dispatcher.CheckAccess())
                {
                    richTextBoxCropLog.AppendText("[" + DateTime.Now.ToString() + "] " + msg + "\r");
                    richTextBoxCropLog.ScrollToEnd();
                    MessageMain(msg);
                }
                else
                {
                    richTextBoxCropLog.Dispatcher.Invoke(new Message(MessageCrop), new object[] { msg });
                }

            }
            catch (Exception)
            {
            }
        }

        public void MessageSubtitle(string msg)
        {
            try
            {
                if (richTextBoxSubtitleLog.Dispatcher.CheckAccess())
                {
                    richTextBoxSubtitleLog.AppendText("[" + DateTime.Now.ToString() + "] " + msg + "\r");
                    richTextBoxSubtitleLog.ScrollToEnd();
                    MessageMain(msg);
                }
                else
                {
                    richTextBoxSubtitleLog.Dispatcher.Invoke(new Message(MessageSubtitle), new object[] { msg });
                }

            }
            catch (Exception)
            {
            }
        }

        public void MessageEncode(string msg)
        {
            try
            {
                if (richTextBoxEncodeLog.Dispatcher.CheckAccess())
                {
                    richTextBoxEncodeLog.AppendText("[" + DateTime.Now.ToString() + "] " + msg + "\r");
                    richTextBoxEncodeLog.ScrollToEnd();
                    MessageMain(msg);
                }
                else
                {
                    richTextBoxEncodeLog.Dispatcher.Invoke(new Message(MessageEncode), new object[] { msg });
                }

            }
            catch (Exception)
            {
            }
        }

        public void MessageMux(string msg)
        {
            try
            {
                if (richTextBoxMuxLog.Dispatcher.CheckAccess())
                {
                    richTextBoxMuxLog.AppendText("[" + DateTime.Now.ToString() + "] " + msg + "\r");
                    richTextBoxMuxLog.ScrollToEnd();
                    MessageMain(msg);
                }
                else
                {
                    richTextBoxMuxLog.Dispatcher.Invoke(new Message(MessageMux), new object[] { msg });
                }

            }
            catch (Exception)
            {
            }
        }

        private void menuLogClearAll_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                richTextBoxMainLog.Document.Blocks.Clear();
                richTextBoxDemuxLog.Document.Blocks.Clear();
                richTextBoxCropLog.Document.Blocks.Clear();
                richTextBoxSubtitleLog.Document.Blocks.Clear();
                richTextBoxEncodeLog.Document.Blocks.Clear();
                richTextBoxMuxLog.Document.Blocks.Clear();
            }
            catch (Exception)
            {
            }
        }

        private void menuLogClear_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int index = tabControlLog.SelectedIndex;
                switch (index)
                {
                    case 0:
                        richTextBoxMainLog.Document.Blocks.Clear();
                        break;
                    case 1:
                        richTextBoxDemuxLog.Document.Blocks.Clear();
                        break;
                    case 2:
                        richTextBoxCropLog.Document.Blocks.Clear();
                        break;
                    case 3:
                        richTextBoxSubtitleLog.Document.Blocks.Clear();
                        break;
                    case 4:
                        richTextBoxEncodeLog.Document.Blocks.Clear();
                        break;
                    case 5:
                        richTextBoxMuxLog.Document.Blocks.Clear();
                        break;
                    default:
                        break;
                }
            }
            catch (Exception)
            {
            }
        }

        private void menuLogSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int index = tabControlLog.SelectedIndex;
                switch (index)
                {
                    case 0:
                        break;
                    case 1:
                        break;
                    case 2:
                        break;
                    case 3:
                        break;
                    case 4:
                        break;
                    case 5:
                        break;
                    default:
                        break;
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
                    mainWindow.UpdateDiffLog();
            }
            catch (Exception)
            {
            }
        }

        public void SaveMainLog(string filename)
        {
            try
            {
                //SaveLog(richTextBoxMainLog.Document.Blocks.FirstBlock.
            }
            catch (Exception)
            {
            }
        }

        private void SaveLog(string log, string filename)
        {
            try
            {
                string[] lines = log.Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
                string tmp = "";
                foreach (string s in lines) tmp += s.Trim() + "\r\n";
                System.IO.File.WriteAllText(filename, tmp);
            }
            catch (Exception)
            {
            }
        }
    }
}
