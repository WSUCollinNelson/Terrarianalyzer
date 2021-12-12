using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Terrarianalyzer
{
    /// <summary>
    /// Interface for processing world saves into WorldObjects
    /// </summary>
    public interface IProcessWorldSaves
    {
        /// <summary>
        /// Will Prompt the user for a filepath and load a new WorldObject
        /// </summary>
        /// <returns>A WorldObject populated with relevant data</returns>
        WorldObject GetWorldObject();
    }
}
