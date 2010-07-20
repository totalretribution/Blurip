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
            try
            {
                InitializeComponent();
                this.avs = new AvisynthSettings(avs);
                textBoxDescription.Text = avs.desc;
                richTextBoxCommands.AppendText(avs.commands);
            }
            catch (Exception ex)
            {
                Global.ErrorMsg(ex);
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
