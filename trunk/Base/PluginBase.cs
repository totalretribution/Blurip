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

using System.Collections.Generic;
using System.Security.Cryptography;
using System;
using System.IO;
using System.Xml.Serialization;
using System.Text;
using System.Diagnostics;
using System.Threading;
using System.Windows.Forms;

namespace BluRip
{
    public enum PluginType
    {
        BeforeDemux,
        AfterDemux,
        BeforeAutoCrop,
        AfterAutoCrop,
        BeforeSubtitle,
        AfterSubtitle,
        BeforeEncode,
        AfterEncode,
        BeforeMux,
        AfterMux,
        None
    }

    public abstract class PluginBase
    {
        protected UserSettings settings = null;
        protected List<Project> projectList = null;
        protected TitleInfo demuxedStreamList = null;
        protected TitleInfo streamList = null;

        public PluginBase()
        {
        }

        public virtual PluginType getPluginType()
        {
            return PluginType.None;
        }
    }
}