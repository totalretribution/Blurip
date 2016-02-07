//BluRip - one click BluRay/m2ts to mkv converter

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


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace BluRip
{
    class BRMetaData
    {
        private String _discname = "";

        public BRMetaData(String blurayPath)
            : base()
        {
            try
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(blurayPath + "\\BDMV\\META\\DL\\bdmt_eng.xml");
                XmlNamespaceManager xmlnsManager = new XmlNamespaceManager(xmlDoc.NameTable);
                xmlnsManager.AddNamespace("di", "urn:BDA:bdmv;discinfo");

                XmlNode titleNode = xmlDoc.SelectSingleNode("//di:discinfo/di:title/di:name", xmlnsManager);

                if (titleNode != null) _discname = titleNode.InnerText;
            }
            catch (Exception)
            {
                _discname = "";
            }
        }

        public String discname
        {
            get { return _discname; }
        }
    }
}
