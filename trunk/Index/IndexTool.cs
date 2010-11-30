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
using System.Text;
using System.IO;

namespace BluRip
{
    public enum IndexType
    {
        ffmsindex,
        dgindex,
        unknown
    }

    public class IndexTool : ExternalTool
    {
        private UserSettings settings = null;
        private string filename = "";
        private IndexType indexType = IndexType.unknown;

        public IndexTool(UserSettings settings, string filename, IndexType indexType)
            : base()
        {
            try
            {
                this.settings = settings;
                this.filename = filename;
                this.indexType = indexType;

                if (indexType == IndexType.ffmsindex)
                {
                    if (settings.deleteIndex || isOlder(filename + ".ffindex", filename))
                    {
                        if (File.Exists(filename + ".ffindex"))
                        {
                            try
                            {
                                File.Delete(filename + ".ffindex");
                            }
                            catch (Exception)
                            {
                            }
                        }
                    }
                    Path = settings.ffmsindexPath;
                    Parameter = "\"" + filename + "\"";
                }
                else if (indexType == IndexType.dgindex)
                {
                    string output = System.IO.Path.ChangeExtension(filename, "dgi");
                    if (settings.deleteIndex || isOlder(output, filename))
                    {
                        if (File.Exists(output))
                        {
                            try
                            {
                                File.Delete(output);
                            }
                            catch (Exception)
                            {
                            }
                        }
                    }                    
                    Path = settings.dgindexnvPath;
                    Parameter = "-i \"" + filename + "\" -o \"" + output + "\" -e";
                }
            }
            catch (Exception)
            {
            }
        }

        private bool isOlder(string indexFile, string videoFile)
        {
            try
            {
                DateTime indexInfo = new DateTime(0,0,0);
                DateTime videoInfo = new DateTime(0,0,0);

                if (File.Exists(indexFile))
                {
                    indexInfo = File.GetCreationTime(indexFile);
                }
                else
                {
                    return false;
                }

                if (File.Exists(videoFile))
                {
                    videoInfo = File.GetCreationTime(videoFile);
                }
                else
                {
                    return false;
                }

                TimeSpan ts = indexInfo - videoInfo;
                if (ts.TotalSeconds > 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        protected override void StartInfo()
        {
            Info("Starting to index");
            Info("");
        }

        protected override void EndInfo()
        {
            Info("Done.");
        }

        protected override bool StartProcessing()
        {
            if (Ok && ExitCode == 0) return true;
            else return false;
        }

        protected override void Process()
        {
            try
            {
                if (indexType == IndexType.ffmsindex)
                {
                    if (File.Exists(filename + ".ffindex"))
                    {
                        successfull = true;
                    }
                    else
                    {
                        Info(filename + ".ffindex not found!");
                    }
                }
                else if (indexType == IndexType.dgindex)
                {
                    string output = System.IO.Path.ChangeExtension(filename, "dgi");
                    if (File.Exists(output))
                    {
                        successfull = true;
                    }
                    else
                    {
                        Info(output + " not found!");
                    }
                }
            }
            catch (Exception)
            {
            }
        }
    }
}
