//BluRip - one click BluRay/m2ts to mkv converter
//Copyright (C) 2009-2010 _hawk_

//This program is free software; you can redistribute it and/or
//modify it under the terms of the GNU General Public License
//as published by the Free Software Foundation; either version 2
//of the License, or (at your option) any later version.

//This program is distributed in the hope that it will be useful,
//but WITHOUT ANY WARRANTY; without even the implied warranty of
//MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//GNU General Public License for more details.

//You should have received a copy of the GNU General Public License
//along with this program; if not, write to the Free Software
//Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA  02110-1301, USA.

//Contact: hawk.ac@gmx.net

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
            try
            {
                InitializeComponent();
                listBoxFileList.Items.Clear();
            }
            catch (Exception ex)
            {
                Global.ErrorMsg(ex);
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
                ofd.Filter = Global.Res("FileFilter") + "|*.m2ts";
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
