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
    public partial class SettingsWindow : Window
    {
        private PushoverSettings settings = null;

        public SettingsWindow(PluginSettingsBase settings)
        {
            try
            {
                InitializeComponent();
                this.settings = new PushoverSettings(settings);     //Blanks Custom Settings???

                checkBoxActivated.IsChecked = this.settings.activated;
                AppTokenTxt.Text = this.settings.AppToken;
                UserTokenTxt.Text = this.settings.UserToken;
            }
            catch (Exception)
            {
            }
        }

        public PushoverSettings Settings
        {
            get { return settings; }
        }

        private void buttonOk_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                settings.AppToken = AppTokenTxt.Text;
                settings.UserToken = UserTokenTxt.Text;
            }
            catch (Exception)
            {
            }

            DialogResult = true;
        }

        private void checkBoxActivated_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                settings.activated = (bool)checkBoxActivated.IsChecked;
            }
            catch (Exception)
            {
            }
        }

    }
}
