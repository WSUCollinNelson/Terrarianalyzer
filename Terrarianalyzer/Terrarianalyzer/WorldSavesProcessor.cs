using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Terrarianalyzer
{
    public class WorldSavesProcessor : IProcessWorldSaves
    {
        public WorldObject GetWorldObject()
        {
            var filePath = string.Empty;

            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = "./";
                openFileDialog.Filter = "wld files (*.wld)|*.wld";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    //Get the path of specified file
                    filePath = openFileDialog.FileName;

                    //Read the contents of the file into a stream
                    byte[] bytes = File.ReadAllBytes(filePath);

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
