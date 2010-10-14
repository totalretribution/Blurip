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
    public class PluginSettingsBase
    {
        public static bool SaveSettingsFile(PluginSettingsBase settings, Type type, string filename)
        {
            MemoryStream ms = null;
            FileStream fs = null;
            XmlSerializer xs = null;
            try
            {
                ms = new MemoryStream();
                fs = new FileStream(filename, FileMode.Create, FileAccess.Write);

                xs = new XmlSerializer(type);
                xs.Serialize(ms, settings);
                ms.Seek(0, SeekOrigin.Begin);
                fs.Write(ms.ToArray(), 0, (int)ms.Length);
                ms.Close();
                fs.Close();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
            finally
            {
                if (ms != null) ms.Close();
                if (fs != null) fs.Close();
            }
        }

        public static bool LoadSettingsFile(ref PluginSettingsBase settings, Type type, string filename)
        {
            MemoryStream ms = null;
            try
            {
                if (!File.Exists(filename)) return false;
                byte[] data = File.ReadAllBytes(filename);
                XmlSerializer xs = new XmlSerializer(type);
                ms = new MemoryStream(data);
                ms.Seek(0, SeekOrigin.Begin);
                settings = (PluginSettingsBase)xs.Deserialize(ms);
                ms.Close();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
            finally
            {
                if (ms != null) ms.Close();
            }
        }

        public PluginSettingsBase()
        {
            try
            {
            }
            catch (Exception)
            {
            }
        }

        public PluginSettingsBase(PluginSettingsBase orig)
        {
            try
            {
                this.activated = orig.activated;
            }
            catch (Exception)
            {
            }
        }

        bool activated = false;
    }

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
        protected Project _project = null;

        public PluginBase(Project project)
        {
            try
            {
                this._project = new Project(project);
            }
            catch (Exception)
            {
            }
        }

        public Project project
        {
            get { return _project; }
        }

        // plugin name
        public abstract string GetName();

        // plugin description
        public abstract string GetDescription();

        public virtual PluginType getPluginType()
        {
            return PluginType.None;
        }

        PluginSettingsBase settings = null;

        public abstract Type GetSettingsType();
    }
}