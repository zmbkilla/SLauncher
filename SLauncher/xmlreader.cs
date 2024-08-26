using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace SLauncher
{
    public class xmlreader
    {
        public void Read_XML(string URL)
        {
            XmlDocument patchnotes = new XmlDocument();
            patchnotes.Load(URL);

        }
    }
}
