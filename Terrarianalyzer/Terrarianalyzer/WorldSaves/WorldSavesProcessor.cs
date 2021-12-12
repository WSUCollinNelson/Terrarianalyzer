using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Terrarianalyzer
{
    /// <summary>
    /// Implementation of the IProcessWorldSaves interface
    /// </summary>
    public class WorldSavesProcessor : IProcessWorldSaves
    {
        /// <summary>
        /// Prompts an OpenFileDialog to select a Terraria world save.
        /// Reads all of the bytes of data directy from a world save, creates a new world object from this binary.
        /// </summary>
        /// <returns>A WorldObject created from the selected file.</returns>
        public WorldObject GetWorldObject()
        {
            string filePath = string.Empty;

            //Create an OpenFileDialog to select the world file
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                //Initialize in the local directory
                openFileDialog.InitialDirectory = "./";

                //Filter to Terraria world files.
                openFileDialog.Filter = "wld files (*.wld)|*.wld";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    filePath = openFileDialog.FileName;

                    //Read the contents of the file into a stream
                    byte[] bytes = File.ReadAllBytes(filePath);

                    // Create the memory stream and load a new WorldObject
                    using (MemoryStream memoryStream = new MemoryStream(bytes))
                    {
                        return new WorldObject(memoryStream);
                    }
                }
            }

            return null;
        }
    }
}
