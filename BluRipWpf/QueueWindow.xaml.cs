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
    }
}
