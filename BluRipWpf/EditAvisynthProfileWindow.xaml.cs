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
    /// Interaktionslogik für EditAvisynthProfileWindow.xaml
    /// </summary>
    public partial class EditAvisynthProfileWindow : Window
    {
        AvisynthSettings avs = null;

        public EditAvisynthProfileWindow(AvisynthSettings avs)
        {
            InitializeComponent();
            try
            {
                this.avs = new AvisynthSettings(avs);
                textBoxDescription.Text = avs.desc;
                richTextBoxCommands.AppendText(avs.commands);
            }
            catch (Exception)
            {
            }
        }

        public AvisynthSettings avisynthSettings
        {
            get { return avs; }
        }

        private void buttonOk_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        private void textBoxDescription_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                avs.desc = textBoxDescription.Text;
            }
            catch (Exception)
            {
            }
        }

        private void richTextBoxCommands_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                TextRange textRange = new TextRange(richTextBoxCommands.Document.ContentStart, richTextBoxCommands.Document.ContentEnd);
                avs.commands = textRange.Text;
            }
            catch (Exception)
            {
            }
        }
    }
}
