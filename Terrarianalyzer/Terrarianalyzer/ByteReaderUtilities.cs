using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Terrarianalyzer
{
    public static class ByteReaderUtilities
    {
        public static int LoadByte(MemoryStream bytes)
        {
            return bytes.ReadByte();
        }

        public static byte LoadByteRaw(MemoryStream bytes)
        {
            byte[] readByte = new byte[1];
            bytes.Read(readByte, 0, 1);
            return readByte[0];
        }

        public static bool GetBit(byte inputByte, int bitIndex)
        {
            return (inputByte & (1 << bitIndex)) != 0;
        }

        public static Int16 LoadInt16(MemoryStream bytes)
        {
            byte[] result = new byte[2];
            bytes.Read(result, 0, 2);
            return BitConverter.ToInt16(result, 0);
        }

        public static Int32 LoadInt32(MemoryStream bytes)
        {
            byte[] result = new byte[4];
            bytes.Read(result, 0, 4);
            return BitConverter.ToInt32(result, 0);
        }

        public static Int64 LoadInt64(MemoryStream bytes)
        {
            byte[] result = new byte[8];
            bytes.Read(result, 0, 8);
            return BitConverter.ToInt64(result, 0);
        }

        public static double LoadDouble(MemoryStream bytes)
        {
            byte[] result = new byte[8];
            bytes.Read(result, 0, 8);
            return BitConverter.ToDouble(result, 0);
        }

        public static void Discard(MemoryStream bytes, int byteCount)
        {
            byte[] discardBuffer = new byte[byteCount];
            bytes.Read(discardBuffer, 0, byteCount);
        }

        public static string LoadString(MemoryStream bytes)
        {
            int stringLength = LoadByte(bytes);
            byte[] stringBuffer = new byte[stringLength];
            bytes.Read(stringBuffer, 0, stringLength);
            return Encoding.Default.GetString(stringBuffer);
        }

        public static bool LoadBool(MemoryStream bytes)
        {
            int inputByte = bytes.ReadByte();
            return inputByte == 1;
        }

        public static List<bool> LoadBitArray(MemoryStream bytes, int numberOfBytes)
        {
            List<bool> output = new List<bool>();
            for (int i = 0; i < numberOfBytes; i++)
            {
                byte readByte = LoadByteRaw(bytes);
                for (int j = 0; j < 8; j++)
                {
                    output.Add(GetBit(readByte, j));
                }
            }

            return output;
        }
    }
}
