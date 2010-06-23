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
    /// Interaktionslogik für OpenM2tsWindow.xaml
    /// </summary>
    public partial class FileListWindow : Window
    {
        public List<string> fileList = new List<string>();

        private void UpdateList()
        {
            try
            {
                listBoxFileList.Items.Clear();
                foreach (string s in fileList)
                {
                    listBoxFileList.Items.Add(System.IO.Path.GetFileName(s));
                }
            }
            catch (Exception)
            {
            }
        }

        public FileListWindow()
        {
            InitializeComponent();
            try
            {
                listBoxFileList.Items.Clear();
            }
            catch (Exception)
            {
            }
        }

        private void buttonFileListUp_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int index = listBoxFileList.SelectedIndex;
                if (index > 0)
                {
                    string tmp = fileList[index];
                    fileList.RemoveAt(index);
                    fileList.Insert(index - 1, tmp);
                    UpdateList();
                    listBoxFileList.SelectedIndex = index - 1;
                }
            }
            catch (Exception)
            {
            }
        }

        private void buttonFileListDown_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int index = listBoxFileList.SelectedIndex;
                if (index < fileList.Count - 1)
                {
                    string tmp = fileList[index];
                    fileList.RemoveAt(index);
                    fileList.Insert(index + 1, tmp);
                    UpdateList();
                    listBoxFileList.SelectedIndex = index + 1;
                }
            }
            catch (Exception)
            {
            }
        }

        private void buttonFileListDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int index = listBoxFileList.SelectedIndex;
                if (index > -1)
                {
                    fileList.RemoveAt(index);
                    UpdateList();
                }
            }
            catch (Exception)
            {
            }
        }

        private void buttonFileListAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                OpenFileDialog ofd = new OpenFileDialog();
                ofd.Filter = "m2ts files|*.m2ts";
                ofd.Multiselect = true;
                if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    foreach (string s in ofd.FileNames)
                    {
                        fileList.Add(s);
                    }
                    UpdateList();
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
