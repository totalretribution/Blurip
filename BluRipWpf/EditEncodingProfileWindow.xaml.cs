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
    /// Interaktionslogik für EditEncodingProfileWindow.xaml
    /// </summary>
    public partial class EditEncodingProfileWindow : Window
    {
        EncodingSettings es = null;

        public EditEncodingProfileWindow(EncodingSettings es)
        {
            InitializeComponent();
            try
            {
                this.es = new EncodingSettings(es);
                textBoxDescription.Text = es.desc;
                textBoxSettings.Text = es.settings;
                textBoxSettings2.Text = es.settings2;
                textBoxSizeValue.Text = es.sizeValue.ToString();
                comboBoxSizeType.SelectedIndex = (int)es.sizeType;
                checkBox2pass.IsChecked = es.pass2;

                checkBox2pass_Checked(null, null);
            }
            catch (Exception)
            {
            }
        }

        public EncodingSettings encodingSettings
        {
            get { return es; }
        }

        private void checkBox2pass_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                es.pass2 = (bool)checkBox2pass.IsChecked;
                if (es.pass2)
                {
                    textBoxSettings2.IsEnabled = true;
                    textBoxSizeValue.IsEnabled = true;
                    comboBoxSizeType.IsEnabled = true;
                }
                else
                {
                    textBoxSettings2.IsEnabled = false;
                    textBoxSizeValue.IsEnabled = false;
                    comboBoxSizeType.IsEnabled = false;
                }
            }
            catch (Exception)
            {
            }
        }

        private void textBoxDescription_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                es.desc = textBoxDescription.Text;
            }
            catch (Exception)
            {
            }
        }

        private void textBoxSettings_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                es.settings = textBoxSettings.Text;
            }
            catch (Exception)
            {
            }
        }

        private void textBoxSettings2_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                es.settings2 = textBoxSettings2.Text;
            }
            catch (Exception)
            {
            }
        }

        private void textBoxSizeValue_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                es.sizeValue = Convert.ToDouble(textBoxSizeValue.Text);
            }
            catch (Exception)
            {
            }
        }

        private void buttonOk_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        private void comboBoxSizeType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                es.sizeType = (SizeType)Enum.ToObject(typeof(SizeType), comboBoxSizeType.SelectedIndex);
            }
            catch (Exception)
            {
            }
        }
    }
}
