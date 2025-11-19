using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Xml;

namespace APPHELPPERS
{
    public class Driver
    {
        public static string ConntectionString()
        {
            return (string)HttpContext.Current.Application["driver-connection"];
        }
        public static string CSID()
        {
            return (string)HttpContext.Current.Application["csid-procy"];
        }
    }
    public class XMLReader
    {
        private XmlDocument doc = null;
        public XMLReader()
        {
            string path = HttpContext.Current.Server.MapPath("/bin/") + "data.xml";
            doc = new XmlDocument();
            doc.Load(path);
        }
        public string readConnectionContractor()
        {
            return doc.GetElementsByTagName("connection")[0].InnerXml;
        }
    }
}
