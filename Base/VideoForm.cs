using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace BluRip
{
    public partial class VideoForm : Form
    {
        private string filename = "";
        private bool update = false;

        public VideoForm()
        {
            InitializeComponent();
        }

        public VideoForm(string filename)
        {
            try
            {
                InitializeComponent();
                this.filename = filename;
            }
            catch (Exception)
            {
            }
        }
        
        private delegate void UpdateTime(object sender, AxAXVLC.DVLCEvents_MediaPlayerTimeChangedEvent e);        

        private void OnTimeChanged(object sender, AxAXVLC.DVLCEvents_MediaPlayerTimeChangedEvent e)
        {
            try
            {
                if (labelPosition.InvokeRequired)
                {
                    labelPosition.Invoke(new UpdateTime(OnTimeChanged), new object[] { sender, e });
                }
                else
                {
                    labelPosition.Text = "Current position: " + e.time.ToString() + " ms - total length: " + axVLCPlugin21.input.Length.ToString("f0") + " ms - position: " + axVLCPlugin21.input.Position.ToString("f2"); ;
                    
                    if (axVLCPlugin21.input.Length > 0)
                    {
                        trackBarPosition.Maximum = 100;
                        trackBarPosition.Value = (int)(axVLCPlugin21.input.Position * 100.0);
                    }
                    update = true;
                }
            }
            catch (Exception)
            {
            }
        }


        private void VideoForm_Shown(object sender, EventArgs e)
        {
            try
            {
                if (filename != "")
                {
                    trackBarPosition.Minimum = 0;
                    trackBarPosition.Maximum = 0;
                    axVLCPlugin21.MediaPlayerTimeChanged += new AxAXVLC.DVLCEvents_MediaPlayerTimeChangedEventHandler(OnTimeChanged);

                    axVLCPlugin21.AutoPlay = false;
                    axVLCPlugin21.playlist.items.clear();
                    axVLCPlugin21.playlist.add(filename);
                    axVLCPlugin21.playlist.play();
                    int count = 0;
                    while (!axVLCPlugin21.playlist.isPlaying && count < 40)
                    {
                        count++;
                        Thread.Sleep(50);
                    }
                    if (count == 40)
                    {
                        axVLCPlugin21.playlist.stop();
                        MessageBox.Show("There was an error while opening the file", "Error");
                        this.Close();
                    }
                    axVLCPlugin21.playlist.togglePause();
                    
                    comboBoxAudio.Items.Clear();
                    comboBoxAudio.Items.Add("Disabled");
                    for (int i = 0; i < axVLCPlugin21.audio.count; i++)
                    {
                        comboBoxAudio.Items.Add(i + 1);
                    }
                    int index = axVLCPlugin21.audio.track;
                    if (index < 0) index = 0;
                    comboBoxAudio.SelectedIndex = index;
                    comboBoxSubs.Items.Clear();
                    comboBoxSubs.Items.Add("Disabled");
                    for (int i = 0; i < axVLCPlugin21.subtitle.count; i++)
                    {
                        comboBoxSubs.Items.Add(i + 1);
                    }
                    index = axVLCPlugin21.subtitle.track;
                    if (index < 0) index = 0;
                    comboBoxSubs.SelectedIndex = index;
                    axVLCPlugin21.playlist.play();
                }
            }
            catch (Exception)
            {
            }
        }

        private void VideoForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                axVLCPlugin21.playlist.stop();
                axVLCPlugin21.MediaPlayerTimeChanged -= new AxAXVLC.DVLCEvents_MediaPlayerTimeChangedEventHandler(OnTimeChanged);                
            }
            catch (Exception)
            {
            }
        }

        private void trackBarPosition_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (!update)
                {
                    axVLCPlugin21.input.Position = trackBarPosition.Value / 100.0;
                }
                else
                {
                    update = false;
                }
            }
            catch (Exception)
            {
            }
        }

        private void buttonStop_Click(object sender, EventArgs e)
        {
            try
            {
                axVLCPlugin21.playlist.stop();
                labelPosition.Text = "Current position:";
                trackBarPosition.Minimum = 0;
                trackBarPosition.Maximum = 0;
                trackBarPosition.Value = 0;
            }
            catch (Exception)
            {
            }
        }

        private void buttonPlay_Click(object sender, EventArgs e)
        {
            try
            {
                axVLCPlugin21.playlist.play();
                int count = 0;
                while (!axVLCPlugin21.playlist.isPlaying && count < 40)
                {
                    count++;
                    Thread.Sleep(50);
                }
                int index = comboBoxAudio.SelectedIndex;
                if (index > -1)
                {
                    axVLCPlugin21.audio.track = index;
                }
                index = comboBoxSubs.SelectedIndex;
                if (index > -1)
                {
                    if (axVLCPlugin21.subtitle.track != -1)
                    {
                        axVLCPlugin21.subtitle.track = index;
                    }
                }
                
            }
            catch (Exception)
            {
            }
        }

        private void buttonPause_Click(object sender, EventArgs e)
        {
            try
            {
                axVLCPlugin21.playlist.togglePause();
            }
            catch (Exception)
            {
            }
        }

        private void comboBoxAudio_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int index = comboBoxAudio.SelectedIndex;
                if (index > -1)
                {
                    axVLCPlugin21.audio.track = index;
                }
            }
            catch (Exception)
            {
            }
        }

        private void comboBoxSubs_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int index = comboBoxSubs.SelectedIndex;
                if (index > -1)
                {
                    if (axVLCPlugin21.subtitle.track != -1)
                    {
                        axVLCPlugin21.subtitle.track = index;
                    }
                }
            }
            catch (Exception)
            {
            }
        }
    }
}
