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
using System.IO;
using System.Xml.Serialization;
using System.Text.RegularExpressions;

namespace BluRip
{
    public class EncodeTool : ExternalTool
    {
        private UserSettings settings = null;
        private bool secondPass = false;

        public EncodeTool(UserSettings settings, int profile, bool secondPass)
            : base()
        {
            try
            {
                this.settings = settings;
                this.secondPass = secondPass;
                this.Priority = settings.x264Priority;

                if (!settings.use64bit)
                {
                    this.Path = settings.x264Path;
                }
                else
                {
                    this.Path = "cmd.exe";
                }
            }
            catch (Exception)
            {
            }
        }

        protected override void StartInfo()
        {
            if (!secondPass)
            {
                Info("Starting to encode...");
            }
            else
            {
                Info("Starting to encode 2. pass...");
            }
            Info("");
        }

        protected override void EndInfo()
        {
            Info("Done.");
        }
    }
}