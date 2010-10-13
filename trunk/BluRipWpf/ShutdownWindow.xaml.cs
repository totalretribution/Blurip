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
using System.Windows.Threading;

namespace BluRip
{
    /// <summary>
    /// Interaktionslogik für ShutdownWindow.xaml
    /// </summary>
    public partial class ShutdownWindow : Window
    {
        private DispatcherTimer timer;
        private int countdown = 120;
        private string msgTextResource = "";

        public ShutdownWindow()
        {            
            try
            {
                InitializeComponent();
                labelShutdownCounter.Content = Global.ResFormat("LabelShutdownCounter", countdown);
                timer = new DispatcherTimer();
                timer.Interval = new TimeSpan(0, 0, 1);
                timer.Tick += new EventHandler(TimerTick);
                timer.Start();
            }
            catch (Exception ex)
            {
                Global.ErrorMsg(ex);
            }
        }

        public ShutdownWindow(string captionResource, string msgTextResource, int countdown)
        {
            try
            {
                InitializeComponent();
                this.msgTextResource = msgTextResource;
                this.countdown = countdown;
                this.Title = Global.Res(captionResource);
                labelShutdownCounter.Content = Global.ResFormat(msgTextResource, countdown);
                timer = new DispatcherTimer();
                timer.Interval = new TimeSpan(0, 0, 1);
                timer.Tick += new EventHandler(TimerTick);
                timer.Start();
            }
            catch (Exception ex)
            {
                Global.ErrorMsg(ex);
            }
        }

        private void TimerTick(object sender, EventArgs e)
        {
            try
            {
                if (msgTextResource == "")
                {
                    labelShutdownCounter.Content = Global.ResFormat("LabelShutdownCounter", countdown);
                }
                else
                {
                    labelShutdownCounter.Content = Global.ResFormat(msgTextResource, countdown);
                }
                countdown--;
                if (countdown == 0)
                {
                    DialogResult = true;
                }
            }
            catch (Exception)
            {
            }
        }

        private void buttonCancel_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                timer.Stop();
            }
            catch (Exception)
            {
            }
        }

        private void buttonOk_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                timer.Stop();
                DialogResult = true;
            }
            catch (Exception)
            {
            }
        }
    }
}
