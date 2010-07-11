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
    /// Interaktionslogik für AboutWindow.xaml
    /// </summary>
    public partial class AboutWindow : Window
    {
        public AboutWindow()
        {
            try
            {
                InitializeComponent();
            }
            catch (Exception ex)
            {
                Global.ErrorMsg(ex);
            }            
        }

        private void buttonOk_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        private string germanForumLink = "http://forum.gleitz.info/showthread.php?t=41747";
        private string englishForumLink = "http://forum.doom9.org/showthread.php?t=152294";
        private string homeLink = "http://code.google.com/p/blurip/";
        private string emailLink = "mailto:hawk.ac@gmx.net";

        private void labelGermanForum_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start(germanForumLink);
            }
            catch (Exception)
            {
            }
        }

        private void labelEnglishForum_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start(englishForumLink);
            }
            catch (Exception)
            {
            }
        }

        private void labelEmail_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start(emailLink);
            }
            catch (Exception)
            {
            }
        }

        private void labelHome_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start(homeLink);
            }
            catch (Exception)
            {
            }
        }
    }
}
