using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Terrarianalyzer
{
    /// <summary>
    /// Provides static XML Utilities that get data from the XML manifest
    /// </summary>
    public static class XMLUtilities
    {  
        /// <summary>
        /// Gets the user friendly name of an object from its IDs
        /// </summary>
        /// <param name="id">The id of a Terraria item</param>
        /// <returns>The name of the selected item</returns>
        public static string GetItemName(int id)
        {
            //Load tiles.xml and return the name of the entry with the same id
            XDocument manifest = XDocument.Load("tiles.xml");
            return (string)manifest.Root.Elements("item").FirstOrDefault(e => (int)e.Attribute("num") == id).Attribute("name");
        }

        /// <summary>
        /// Gets the id name of an object from its user friendly name
        /// </summary>
        /// <param name="name>The name of a Terraria item</param>
        /// <returns>The id of the selected item</returns>
        public static int GetItemID(string name)
        {
            XDocument manifest = XDocument.Load("tiles.xml");
            return (int)manifest.Root.Elements("item").First(e => (string)e.Attribute("name") == name).Attribute("num");
        }

        /// <summary>
        /// Gets the modifier prefix on an item based on the prefix id
        /// </summary>
        /// <param name="prefix">The id of a Terraria prefix</param>
        /// <returns>The prefix text</returns>
        public static string GetItemPrefix(int prefix)
        {
            if (prefix == 0) return "";
            XDocument manifest = XDocument.Load("tiles.xml");
            return (string)manifest.Root.Elements("prefix").First(e => (int)e.Attribute("num") == prefix).Attribute("name");
        }

        /// <summary>
        /// Gets the user friendly name of a tile from its ID
        /// </summary>
        /// <param name="id">The id of a Terraria tile</param>
        /// <returns>The name of the selected tile</returns>
        public static string GetTileName(int id)
        {
            XDocument manifest = XDocument.Load("tiles.xml");
            return (string)manifest.Root.Elements("tile").First(e => (int)e.Attribute("num") == id).Attribute("name");
        }

        /// <summary>
        /// Gets the name of a tile from its id
        /// </summary>
        /// <param name="name">The name of a Terraria tile</param>
        /// <returns>The id of the selected tile</returns>
        public static int GetTileID(string name)
        {
            XDocument manifest = XDocument.Load("tiles.xml");
            return (int)manifest.Root.Elements("tile").First(e => (string)e.Attribute("name") == name).Attribute("num");
        }

        /// <summary>
        /// Gets an array of all tile names in the XML manifest
        /// </summary>
        /// <returns>An array of all tile names</returns>
        public static string[] GetAllTileNames()
        {
            XDocument manifest = XDocument.Load("tiles.xml");
            return manifest.Root.Elements("tile").Select(e => (string)e.Attribute("name")).ToArray();
        }

        /// <summary>
        /// Gets an array of all item names in the XML manifest
        /// </summary>
        /// <returns>An array of all item names</returns>
        public static string[] GetAllItemNames()
        {
            XDocument manifest = XDocument.Load("tiles.xml");
            return manifest.Root.Elements("item").Select(e => (string)e.Attribute("name")).ToArray();
        }
    }
}
