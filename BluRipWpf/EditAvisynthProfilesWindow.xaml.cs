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
    /// Interaktionslogik für EditAvisynthProfilesWindow.xaml
    /// </summary>
    public partial class EditAvisynthProfilesWindow : Window
    {
        private UserSettings settings = null;

        public EditAvisynthProfilesWindow(UserSettings settings)
        {
            InitializeComponent();
            try
            {
                this.settings = new UserSettings(settings);
            }
            catch (Exception)
            {
            }
        }

        private void buttonOk_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        public UserSettings UserSettings
        {
            get { return settings; }
        }
    }
}
