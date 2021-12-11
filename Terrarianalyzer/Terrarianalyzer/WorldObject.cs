using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Terrarianalyzer
{
    public class WorldObject
    {
        #region SectionOneValues
        public int Version { get; set; }
        public List<bool> TileMasks { get; set; }
        public string WorldName { get; set; }
        public string WorldSeed { get; set; }
        public Int32 WorldHeight { get; set; }
        public Int32 WorldWidth { get; set; }
        public Int32 Gamemode { get; set; }
        public bool DrunkWorld { get; set; }
        public bool GetGood { get; set; }
        public bool TenthAnniversary { get; set; }
        public bool DontStarve { get; set; }
        public bool NotTheBees { get; set; }
        public (Int32, Int32) Spawn { get; set; }
        public double SurfaceHeight { get; set; }
        public double RockHeight { get; set; }
        public double GameTime { get; set; }
        public bool CrimsonWorld { get; set; }
        public bool KilledEye { get; set; }
        public bool KilledEater { get; set; }
        public bool KilledSkeletron { get; set; }
        public bool KilledQueenBee { get; set; }
        public bool KilledDestroyer { get; set; }
        public bool KilledTwins { get; set; }
        public bool KilledPrime { get; set; }
        public bool KilledHardBoss { get; set; }
        public bool KilledPlantera { get; set; }
        public bool KilledGolem { get; set; }
        public bool KilledSlimeKing { get; set; }
        public bool HardMode { get; set; }
        public bool SavedTinkerer { get; set; }
        public bool SavedWizard { get; set; }
        public bool SavedMechanic { get; set; }
        public int ShadowOrbs { get; set; }
        public Int32 Altars { get; set; }
        public Int32 AnglersCount { get; set; }
        public List<string> Anglers { get; set; } = new List<string>();
        public bool SavedAngler { get; set; }
        public Int32 AnglerQuests { get; set; }
        public Int16 KillCount { get; set; }
        public List<Int32> Kills { get; set; } = new List<int>();
        public Int32 PartyCount { get; set; }
        public List<Int32> PartyMembers { get; set; } = new List<int>();
        public Int32 TreeTopsCount { get; set; }
        public List<Int32> TreeTops { get; set; } = new List<int>();
        #endregion

        List<TileObject> Tiles = new List<TileObject>();
        List<ChestObject> Chests = new List<ChestObject>();

        public WorldObject(MemoryStream bytes)
        {
            //Read through useless pre-header data
            Version = LoadInt32(bytes);
            Discard(bytes, 20);
            Int16 sectionCount = LoadInt16(bytes);
            Discard(bytes, 4 * sectionCount);
            Int16 tileMasks = LoadInt16(bytes);
            TileMasks = LoadBitArray(bytes, (int)Math.Ceiling((decimal)tileMasks / 8));
            WorldName = LoadString(bytes);
            WorldSeed = LoadString(bytes);
            Discard(bytes, 44);
            WorldHeight = LoadInt32(bytes);
            WorldWidth = LoadInt32(bytes);
            Gamemode = LoadInt32(bytes);
            DrunkWorld = LoadBool(bytes);
            GetGood = LoadBool(bytes);
            TenthAnniversary = LoadBool(bytes);
            DontStarve = LoadBool(bytes);
            NotTheBees = LoadBool(bytes);
            Discard(bytes, 77);
            Spawn = (LoadInt32(bytes), LoadInt32(bytes));
            SurfaceHeight = LoadDouble(bytes);
            RockHeight = LoadDouble(bytes);
            GameTime = LoadDouble(bytes);
            Discard(bytes, 15);
            CrimsonWorld = LoadBool(bytes);
            KilledEye = LoadBool(bytes);
            KilledEater = LoadBool(bytes);
            KilledSkeletron = LoadBool(bytes);
            KilledQueenBee = LoadBool(bytes);
            KilledDestroyer = LoadBool(bytes);
            KilledTwins = LoadBool(bytes);
            KilledPrime = LoadBool(bytes);
            KilledHardBoss = LoadBool(bytes);
            KilledPlantera = LoadBool(bytes);
            KilledGolem = LoadBool(bytes);
            KilledSlimeKing = LoadBool(bytes);
            SavedTinkerer = LoadBool(bytes);
            SavedWizard = LoadBool(bytes);
            SavedMechanic = LoadBool(bytes);
            Discard(bytes, 6);
            ShadowOrbs = LoadByte(bytes);
            Altars = LoadInt32(bytes);
            HardMode = LoadBool(bytes);
            Discard(bytes, 68);
            AnglersCount = LoadInt32(bytes);
            for(int i =0; i < AnglersCount; i++)
            {
                Anglers.Add(LoadString(bytes));
            }
            SavedAngler = LoadBool(bytes);
            AnglerQuests = LoadInt32(bytes);
            Discard(bytes, 11);
            KillCount = LoadInt16(bytes);
            for(int i = 0; i < KillCount; i++)
            {
                Kills.Add(LoadInt32(bytes));
            }
            Discard(bytes, 25);
            PartyCount = LoadInt32(bytes);
            for (int i = 0; i < PartyCount; i++)
            {
                PartyMembers.Add(LoadInt32(bytes));
            }
            Discard(bytes, 30);
            TreeTopsCount = LoadInt32(bytes);
            for (int i = 0; i < TreeTopsCount; i++)
            {
                TreeTops.Add(LoadInt32(bytes));
            }
            Discard(bytes, 23);

            for (int x = 0; x < WorldWidth; x++)
            {
                for (int y = 0; y < WorldHeight; y++)
                {
                    byte activeFlags = LoadByteRaw(bytes);
                    byte tileFlags;
                    byte tileFlagsHighByte = default(byte);
                    int wallType = 0;
                    int tileType = 0;

                    if (GetBit(activeFlags, 0))
                    {
                        tileFlags = LoadByteRaw(bytes);
                        if (GetBit(tileFlags, 0))
                        {
                            tileFlagsHighByte = LoadByteRaw(bytes);
                        }
                    }

                    if (GetBit(activeFlags, 1))
                    {
                        if (GetBit(activeFlags, 5))
                        {
                            int tileTypeHighByte = LoadByte(bytes);
                            tileType = LoadByte(bytes);
                            tileType = (tileType << 8 | tileTypeHighByte);
                        }
                        else
                        {
                            tileType = LoadByte(bytes);
                        }


                        if (TileMasks[tileType])
                        {
                            int textureU = LoadInt16(bytes);
                            int textureV = (tileType == 144 ? 0 : LoadInt16(bytes));
                        }
                        else
                        {
                            int textureU = -1;
                            int textureV = -1;
                        }

                        if (GetBit(tileFlagsHighByte, 3))
                        {
                            int color = LoadByte(bytes);
                        }
                    }

                    if (GetBit(activeFlags, 2))
                    {
                        wallType = LoadByteRaw(bytes);
                        if (GetBit(tileFlagsHighByte, 4))
                        {
                            int color = LoadByte(bytes);
                        }
                    }

                    int liquidBits = (activeFlags & 0x18) >> 3;
                    if (liquidBits != 0)
                    {
                        int liquidAmount = LoadByte(bytes);
                    }

                    if ((tileFlagsHighByte & 0x40) == 64)
                    {
                        int wallIDHighByte = LoadByte(bytes);
                        wallType = wallIDHighByte << 8 | wallType;
                    }

                    int k = 0;
                    if (GetBit(activeFlags, 6))
                    {
                        k = LoadByte(bytes);
                    }
                    if (GetBit(activeFlags, 7))
                    { 
                        k = LoadInt16(bytes);
                    }

                    Tiles.Add(new TileObject(tileType));

                    for (int i = 0; i < k && y < WorldHeight; i++)
                    {
                        y++;
                        Tiles.Add(new TileObject(tileType));
                    }
                }
            }

            int numberOfChests = LoadInt16(bytes);
            int NumberOfSlots = LoadInt16(bytes);

            for(int i = 0; i < numberOfChests; i++)
            {
                int chestX = LoadInt32(bytes);
                int chestY = LoadInt32(bytes);
                string chestName = LoadString(bytes);

                List<ItemObject> items = new List<ItemObject>();
                for (int j = 0; j < NumberOfSlots; j++)
                {
                    int stackSize = LoadInt16(bytes);
                    if(stackSize != 0)
                    {
                        int itemID = LoadInt32(bytes);
                        int itemPrefix = LoadByte(bytes);
                        items.Add(new ItemObject(itemID, itemPrefix));
                    }
                }

                Chests.Add(new ChestObject(items));
            }

            bytes.Dispose();
        }

        private int LoadByte(MemoryStream bytes)
        {
            return bytes.ReadByte();
        }

        private byte LoadByteRaw(MemoryStream bytes)
        {
            byte[] readByte = new byte[1];
            bytes.Read(readByte, 0, 1);
            return readByte[0];
        }

        private bool GetBit(byte inputByte, int bitIndex)
        {
            return (inputByte & (1 << bitIndex)) != 0;
        }

        private Int16 LoadInt16(MemoryStream bytes) 
        {
            byte[] result = new byte[2];
            bytes.Read(result, 0, 2);
            return BitConverter.ToInt16(result, 0);
        }

        private Int32 LoadInt32(MemoryStream bytes)
        {
            byte[] result = new byte[4];
            bytes.Read(result, 0, 4);
            return BitConverter.ToInt32(result, 0);
        }

        private Int64 LoadInt64(MemoryStream bytes)
        {
            byte[] result = new byte[8];
            bytes.Read(result, 0, 8);
            return BitConverter.ToInt64(result, 0);
        }

        private double LoadDouble(MemoryStream bytes)
        {
            byte[] result = new byte[8];
            bytes.Read(result, 0, 8);
            return BitConverter.ToDouble(result, 0);
        }

        private void Discard(MemoryStream bytes, int byteCount)
        {
            byte[] discardBuffer = new byte[byteCount];
            bytes.Read(discardBuffer, 0, byteCount);
        }

        private string LoadString(MemoryStream bytes)
        {
            int stringLength = LoadByte(bytes);
            byte[] stringBuffer = new byte[stringLength];
            bytes.Read(stringBuffer, 0, stringLength);
            return Encoding.Default.GetString(stringBuffer);
        }

        private bool LoadBool(MemoryStream bytes)
        {
            int inputByte = bytes.ReadByte();
            return inputByte == 1;
        }

        private List<bool> LoadBitArray(MemoryStream bytes, int numberOfBytes)
        {
            List<bool> output = new List<bool>();
            for(int i = 0; i < numberOfBytes; i++)
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
