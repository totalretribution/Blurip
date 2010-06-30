﻿using System;
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

        private string Res(string key)
        {
            try
            {
                return (string)App.Current.Resources[key];
            }
            catch (Exception)
            {
                return "Unknown resource";
            }
        }

        public void UpdateQueue()
        {
            try
            {
                listBoxQueue.Items.Clear();
                foreach (Project p in mainWindow.ProjectQueue)
                {
                    listBoxQueue.Items.Add(Res("InfoQueueTitle") + " " + p.settings.movieTitle);
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
