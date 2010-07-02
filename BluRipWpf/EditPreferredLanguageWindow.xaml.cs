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
    /// Interaktionslogik für EditPreferedLanguageWindow.xaml
    /// </summary>
    public partial class EditPreferredLanguageWindow : Window
    {
        private LanguageInfo li = null;

        public EditPreferredLanguageWindow(LanguageInfo li)
        {            
            try
            {
                InitializeComponent();
                this.li = new LanguageInfo(li);

                textBoxLanguage.Text = li.language;
                textBoxTranslation.Text = li.translation;
                textBoxLanguageShort.Text = li.languageShort;
            }
            catch (Exception ex)
            {
                Global.ErrorMsg(ex);
            }
        }

        public LanguageInfo languageInfo
        {
            get { return li; }
        }

        private void buttonOk_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        private void textBoxLanguage_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                li.language = textBoxLanguage.Text;
            }
            catch (Exception)
            {
            }
        }

        private void textBoxTranslation_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                li.translation = textBoxTranslation.Text;
            }
            catch (Exception)
            {
            }
        }

        private void textBoxLanguageShort_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                li.languageShort = textBoxLanguageShort.Text;
            }
            catch (Exception)
            {
            }
        }
    }
}
