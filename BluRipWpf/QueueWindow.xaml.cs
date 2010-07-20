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

namespace BluRip
{
    /// <summary>
    /// Interaktionslogik für QueueWindow.xaml
    /// </summary>
    public partial class QueueWindow : Window
    {
        private MainWindow mainWindow = null;

        public QueueWindow(MainWindow mainWindow)
        {            
            try
            {
                InitializeComponent();
                this.mainWindow = mainWindow;
            }
            catch (Exception ex)
            {
                Global.ErrorMsg(ex.Message);
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

        public void EnableControls()
        {
            try
            {
                groupBoxQueue.IsEnabled = true;
            }
            catch (Exception)
            {
            }
        }

        public void DisableControls()
        {
            try
            {
                groupBoxQueue.IsEnabled = false;
            }
            catch (Exception)
            {
            }
        }

        private void Window_LocationChanged(object sender, EventArgs e)
        {
            try
            {
                if (IsActive)
                    mainWindow.UpdateDiffQueue();
            }
            catch (Exception)
            {
            }
        }

        public void UpdateQueue()
        {
            try
            {
                listBoxQueue.Items.Clear();
                foreach (Project p in mainWindow.ProjectQueue)
                {
                    listBoxQueue.Items.Add(Global.Res("InfoQueueTitle") + " " + p.settings.movieTitle);
                }
            }
            catch (Exception)
            {
            }
        }

        private void buttonQueueStart_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                mainWindow.ProcessQueue();
            }
            catch (Exception)
            {
            }
        }

        private void buttonQueueUp_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int index = listBoxQueue.SelectedIndex;
                if (index > 0)
                {
                    Project p = mainWindow.ProjectQueue[index];
                    mainWindow.ProjectQueue.RemoveAt(index);
                    mainWindow.ProjectQueue.Insert(index - 1, p);
                    UpdateQueue();
                    listBoxQueue.SelectedIndex = index - 1;
                }
            }
            catch (Exception)
            {
            }
        }

        private void buttonQueueDown_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int index = listBoxQueue.SelectedIndex;
                if (index < mainWindow.ProjectQueue.Count - 1)
                {
                    Project p = mainWindow.ProjectQueue[index];
                    mainWindow.ProjectQueue.RemoveAt(index);
                    mainWindow.ProjectQueue.Insert(index + 1, p);
                    UpdateQueue();
                    listBoxQueue.SelectedIndex = index + 1;
                }
            }
            catch (Exception)
            {
            }
        }

        private void buttonQueueDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int index = listBoxQueue.SelectedIndex;
                if (index > -1)
                {
                    mainWindow.ProjectQueue.RemoveAt(index);
                    UpdateQueue();
                }
            }
            catch (Exception)
            {
            }
        }

    }
}
