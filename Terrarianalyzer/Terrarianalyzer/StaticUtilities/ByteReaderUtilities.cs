using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Terrarianalyzer
{
    /// <summary>
    /// Provides static utilities for processing bytes from a memorystream as readable data
    /// </summary>
    public static class ByteReaderUtilities
    {
        /// <summary>
        /// Loads one byte as an int
        /// </summary>
        /// <param name="bytes">The Memory stream to load from</param>
        /// <returns>An integer</returns>
        public static int LoadByte(MemoryStream bytes)
        {
            return bytes.ReadByte();
        }

        /// <summary>
        /// Loads one byte as a byte
        /// </summary>
        /// <param name="bytes">The Memory stream to load from</param>
        /// <returns>A byte</returns>
        public static byte LoadByteRaw(MemoryStream bytes)
        {
            byte[] readByte = new byte[1];
            bytes.Read(readByte, 0, 1);
            return readByte[0];
        }

        /// <summary>
        /// Determines the state of a specific bit within a byte
        /// </summary>
        /// <param name="inputByte">The byte to check</param>
        /// /// <param name="bitIndex">The bit to check</param>
        /// <returns>The boolean state of the selected bit</returns>
        public static bool GetBit(byte inputByte, int bitIndex)
        {
            return (inputByte & (1 << bitIndex)) != 0;
        }

        /// <summary>
        /// Loads 2 bytes as a short int
        /// </summary>
        /// <param name="bytes">The Memory stream to load from</param>
        /// <returns>A short integer</returns>
        public static Int16 LoadInt16(MemoryStream bytes)
        {
            byte[] result = new byte[2];
            bytes.Read(result, 0, 2);
            return BitConverter.ToInt16(result, 0);
        }

        /// <summary>
        /// Loads 4 bytes as an int
        /// </summary>
        /// <param name="bytes">The Memory stream to load from</param>
        /// <returns>An int</returns>
        public static Int32 LoadInt32(MemoryStream bytes)
        {
            byte[] result = new byte[4];
            bytes.Read(result, 0, 4);
            return BitConverter.ToInt32(result, 0);
        }

        /// <summary>
        /// Loads 8 bytes as a long int
        /// </summary>
        /// <param name="bytes">The Memory stream to load from</param>
        /// <returns>A long int</returns>
        public static Int64 LoadInt64(MemoryStream bytes)
        {
            byte[] result = new byte[8];
            bytes.Read(result, 0, 8);
            return BitConverter.ToInt64(result, 0);
        }

        /// <summary>
        /// Loads 8 bytes as a double precision floating point number
        /// </summary>
        /// <param name="bytes">The Memory stream to load from</param>
        /// <returns>A double</returns>
        public static double LoadDouble(MemoryStream bytes)
        {
            byte[] result = new byte[8];
            bytes.Read(result, 0, 8);
            return BitConverter.ToDouble(result, 0);
        }

        /// <summary>
        /// Discards some number of bytes, reading them in and disposing of them
        /// </summary>
        /// <param name="bytes">The Memory stream to load from</param>
        /// <param name="byteCount">The number of bytes to discard</param>
        public static void Discard(MemoryStream bytes, int byteCount)
        {
            byte[] discardBuffer = new byte[byteCount];
            bytes.Read(discardBuffer, 0, byteCount);
        }

        /// <summary>
        /// Loads a pascal string from the MemoryStream
        /// </summary>
        /// <param name="bytes">The Memory stream to load from</param>
        /// <returns>A string</returns>
        public static string LoadString(MemoryStream bytes)
        {
            int stringLength = LoadByte(bytes);
            byte[] stringBuffer = new byte[stringLength];
            bytes.Read(stringBuffer, 0, stringLength);
            return Encoding.Default.GetString(stringBuffer);
        }

        /// <summary>
        /// Loads 1 byte as a bool
        /// </summary>
        /// <param name="bytes">The Memory stream to load from</param>
        /// <returns>A bool</returns>
        public static bool LoadBool(MemoryStream bytes)
        {
            int inputByte = bytes.ReadByte();
            return inputByte == 1;
        }

        /// <summary>
        /// Loads some number of bytes into a byte array, then breaks them up to form a long array of boolean bits.
        /// </summary>
        /// <param name="bytes">The Memory stream to load from</param>
        /// /// <param name="numberOfBytes">The number of bytes to load in</param>
        /// <returns>An array of boolean bits</returns>
        public static List<bool> LoadBitArray(MemoryStream bytes, int numberOfBytes)
        {
            List<bool> output = new List<bool>();
            for (int i = 0; i < numberOfBytes; i++)
            {
                byte readByte = LoadByteRaw(bytes);
                for (int j = 0; j < 8; j++)
                {
                    //foreach byte, add the value of each bit
                    output.Add(GetBit(readByte, j));
                }
            }

            return output;
        }
    }
}
