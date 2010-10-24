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
using System.Reflection;
using System.IO;

namespace BluRip
{
    /// <summary>
    /// Interaktionslogik für InfoWindow.xaml
    /// </summary>
    public partial class InfoWindow : Window
    {
        public InfoWindow()
        {
            InitializeComponent();
        }

        public InfoWindow(string resource)
        {
            try
            {
                InitializeComponent();                
                Stream s = Assembly.GetExecutingAssembly().GetManifestResourceStream("BluRip." + resource);
                if (s != null)
                {
                    s.Seek(0, SeekOrigin.Begin);
                    StreamReader sr = new StreamReader(s);
                    string data = sr.ReadToEnd();
                    sr.Close();
                    string[] lines = data.Split(new char[] { '\n' }, StringSplitOptions.None);
                    string text = "";
                    foreach (string line in lines)
                    {
                        text += line + "\n";
                    }
                    richTextBoxInfo.AppendText(text);
                }
            }
            catch (Exception)
            {
            }
        }

        private void buttonOk_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }
    }
}
