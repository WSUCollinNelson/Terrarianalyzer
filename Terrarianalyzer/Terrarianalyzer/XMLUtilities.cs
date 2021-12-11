using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Terrarianalyzer
{
    public static class XMLUtilities
    {  
        public static string GetItemName(int id)
        {
            XDocument manifest = XDocument.Load("tiles.xml");
            return (string)manifest.Root.Elements("item").First(e => (int)e.Attribute("num") == id).Attribute("name");
        }

        public static string GetItemPrefix(int prefix)
        {
            if (prefix == 0) return "";
            XDocument manifest = XDocument.Load("tiles.xml");
            return (string)manifest.Root.Elements("prefix").First(e => (int)e.Attribute("num") == prefix).Attribute("name");
        }

        public static string GetTileName(int id)
        {
            XDocument manifest = XDocument.Load("tiles.xml");
            return (string)manifest.Root.Elements("tile").First(e => (int)e.Attribute("num") == id).Attribute("name");
        }
    }
}
