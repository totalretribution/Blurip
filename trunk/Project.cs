//BluRip - one click BluRay/m2ts to mkv converter
//Copyright (C) 2009-2010  _hawk_

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

// Contact: hawk.ac@gmx.net

using System.Collections.Generic;
using System.Security.Cryptography;
using System;
using System.IO;
using System.Xml.Serialization;
using System.Text;

namespace BluRip
{
    public class Project
    {
        public Project() { }

        public Project(UserSettings settings, TitleInfo streamList, List<TitleInfo> titleList, int titleIndex, List<string> m2tsList)
        {
            try
            {
                this.settings = new UserSettings(settings);
                this.demuxedStreamList = new TitleInfo(streamList);
                this.titleList.Clear();
                foreach (TitleInfo ti in titleList)
                {
                    this.titleList.Add(new TitleInfo(ti));
                }
                this.titleIndex = titleIndex;
                this.m2tsList.Clear();
                foreach (string s in m2tsList)
                {
                    this.m2tsList.Add(s);
                }
            }
            catch (Exception)
            {
            }
        }

        public Project(Project orig)
        {
            try
            {
                this.demuxedStreamList = new TitleInfo(orig.demuxedStreamList);
                this.m2tsList.Clear();
                foreach (string s in orig.m2tsList)
                {
                    this.m2tsList.Add(s);
                }
                this.settings = new UserSettings(orig.settings);
                this.titleIndex = orig.titleIndex;
                this.titleList.Clear();
                foreach (TitleInfo ti in orig.titleList)
                {
                    this.titleList.Add(new TitleInfo(ti));
                }
            }
            catch (Exception)
            {
            }
        }

        public UserSettings settings = new UserSettings();

        public TitleInfo demuxedStreamList = new TitleInfo();

        public List<TitleInfo> titleList = new List<TitleInfo>();

        public List<string> m2tsList = new List<string>();

        public int titleIndex = -1;

        public static bool SaveProjectFile(Project project, string filename)
        {
            MemoryStream ms = null;
            FileStream fs = null;
            XmlSerializer xs = null;

            try
            {
                ms = new MemoryStream();
                fs = new FileStream(filename, FileMode.Create, FileAccess.Write);

                xs = new XmlSerializer(typeof(Project));
                xs.Serialize(ms, project);
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

        public static bool LoadProjectFile(ref Project project, string filename)
        {
            MemoryStream ms = null;

            try
            {
                if (!File.Exists(filename)) return false;
                byte[] data = File.ReadAllBytes(filename);
                XmlSerializer xs = new XmlSerializer(typeof(Project));
                ms = new MemoryStream(data);
                ms.Seek(0, SeekOrigin.Begin);

                project = (Project)xs.Deserialize(ms);
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
    }
}