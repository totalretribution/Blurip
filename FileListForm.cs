using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace BluRip
{
    public partial class FileListForm : Form
    {
        public List<string> fileList = new List<string>();

        private void UpdateList()
        {
            try
            {
                listBoxFileList.Items.Clear();
                foreach (string s in fileList)
                {
                    listBoxFileList.Items.Add(Path.GetFileName(s));
                }
            }
            catch (Exception)
            {
            }
        }

        public FileListForm()
        {
            InitializeComponent();
            try
            {
                fileList.Clear();
            }
            catch (Exception)
            {
            }
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog ofd = new OpenFileDialog();
                ofd.Filter = "m2ts files|*.m2ts";
                ofd.Multiselect = true;
                if (ofd.ShowDialog() == DialogResult.OK)
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

        private void buttonDelete_Click(object sender, EventArgs e)
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

        private void buttonMoveUp_Click(object sender, EventArgs e)
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

        private void buttonMoveDown_Click(object sender, EventArgs e)
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
    }
}
