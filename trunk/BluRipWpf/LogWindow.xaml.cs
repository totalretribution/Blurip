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

        public LogWindow()
        {
            InitializeComponent();
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
                    richTextBoxMainLog.AppendText(msg + "\r");
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
                    richTextBoxDemuxLog.AppendText(msg + "\r");
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
    }
}
