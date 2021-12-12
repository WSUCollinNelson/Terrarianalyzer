using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Terrarianalyzer.ByteReaderUtilities;

namespace Terrarianalyzer
{
    /// <summary>
    /// An enum of different liquids in Terraria
    /// </summary>
    public enum LiquidType 
    {
        None,
        Water,
        Lava,
        Honey
    }

    /// <summary>
    /// An object that encodes the data within a Terraria world save 
    /// </summary>
    public class WorldObject
    {
        //This section contains a lot of values loaded in from the header of the Terraria world save. Not all are used.
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
        public double HellHeight { get; set; }
        #endregion

        public List<TileObject> Tiles = new List<TileObject>();
        public List<ChestObject> Chests = new List<ChestObject>();

        public WorldObject(MemoryStream bytes)
        {
            //Loads lots of general world data
            LoadHeader(bytes);

            //Loads the world tiles
            LoadTiles(bytes);

            //Loads the world's chest collection
            LoadChests(bytes);

            bytes.Dispose();
        }

        /// <summary>
        /// Loads the file header and section 1. Contains data about general world information, what bosses have been killed, 
        /// the state of certain other milestones, etc.
        /// </summary>
        /// <param name="bytes"></param>
        private void LoadHeader(MemoryStream bytes)
        {
            //Load version info
            Version = LoadInt32(bytes);
            Discard(bytes, 20);
            Int16 sectionCount = LoadInt16(bytes);
            Discard(bytes, 4 * sectionCount);

            //Load tile masks for later
            Int16 tileMasks = LoadInt16(bytes);
            TileMasks = LoadBitArray(bytes, (int)Math.Ceiling((decimal)tileMasks / 8));

            //Load world name/seed
            WorldName = LoadString(bytes);
            WorldSeed = LoadString(bytes);
            Discard(bytes, 44);

            //Height and width
            WorldHeight = LoadInt32(bytes);
            WorldWidth = LoadInt32(bytes);

            //Optional world states
            Gamemode = LoadInt32(bytes);
            DrunkWorld = LoadBool(bytes);
            GetGood = LoadBool(bytes);
            TenthAnniversary = LoadBool(bytes);
            DontStarve = LoadBool(bytes);
            NotTheBees = LoadBool(bytes);
            Discard(bytes, 77);

            //Heights and spawn location
            Spawn = (LoadInt32(bytes), LoadInt32(bytes));
            SurfaceHeight = LoadDouble(bytes);
            RockHeight = LoadDouble(bytes);
            GameTime = LoadDouble(bytes);
            Discard(bytes, 15);

            //Bosses killed/milestones
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

            //Fishing quests
            AnglersCount = LoadInt32(bytes);
            for (int i = 0; i < AnglersCount; i++)
            {
                Anglers.Add(LoadString(bytes));
            }
            SavedAngler = LoadBool(bytes);
            AnglerQuests = LoadInt32(bytes);
            Discard(bytes, 11);

            //Kill counts
            KillCount = LoadInt16(bytes);
            for (int i = 0; i < KillCount; i++)
            {
                Kills.Add(LoadInt32(bytes));
            }
            Discard(bytes, 25);

            //Party Counts
            PartyCount = LoadInt32(bytes);
            for (int i = 0; i < PartyCount; i++)
            {
                PartyMembers.Add(LoadInt32(bytes));
            }
            Discard(bytes, 30);

            //Tree tops?
            TreeTopsCount = LoadInt32(bytes);
            for (int i = 0; i < TreeTopsCount; i++)
            {
                TreeTops.Add(LoadInt32(bytes));
            }
            Discard(bytes, 23);

            //Hell height
            double hellLevel = (((WorldHeight - 230) - SurfaceHeight) / 6);
            hellLevel = hellLevel * 6 + SurfaceHeight - 5;
            HellHeight = hellLevel;
        }

        /// <summary>
        /// Loads all 20+ million tiles into a 2 dimensional array for fast access.
        /// </summary>
        /// <param name="bytes"></param>
        private void LoadTiles(MemoryStream bytes)
        {
            for (int x = 0; x < WorldWidth; x++)
            {
                for (int y = 0; y < WorldHeight; y++)
                {
                    //Init variables
                    byte activeFlags = LoadByteRaw(bytes);
                    byte tileFlags;
                    byte tileFlagsHighByte = default(byte);
                    int wallType = 0;
                    int tileType = 0;
                    int liquidAmount = 0;
                    LiquidType liquidType = LiquidType.None;

                    //Gets if tile flags have a "high byte"
                    if (GetBit(activeFlags, 0))
                    {
                        tileFlags = LoadByteRaw(bytes);
                        if (GetBit(tileFlags, 0))
                        {
                            tileFlagsHighByte = LoadByteRaw(bytes);
                        }
                    }

                    //If tile is an actual solid block
                    if (GetBit(activeFlags, 1))
                    {
                        //If tiletype has extra byte
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

                        //If tile is "important"
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

                        //load tile color
                        if (GetBit(tileFlagsHighByte, 3))
                        {
                            int color = LoadByte(bytes);
                        }
                    }

                    //If tile has wall
                    if (GetBit(activeFlags, 2))
                    {
                        wallType = LoadByteRaw(bytes);
                        //If wall is colored
                        if (GetBit(tileFlagsHighByte, 4))
                        {
                            int color = LoadByte(bytes);
                        }
                    }

                    //Amount of liquid on the block
                    int liquidBits = (activeFlags & 0x18) >> 3;
                    
                    if (liquidBits != 0)
                    {
                        //Load liquid info
                        liquidAmount = LoadByte(bytes);
                        liquidType = (LiquidType)liquidBits;
                    }

                    //If tile flags has certain bit
                    if ((tileFlagsHighByte & 0x40) == 64)
                    {
                        int wallIDHighByte = LoadByte(bytes);
                        wallType = wallIDHighByte << 8 | wallType;
                    }

                    //Get length of RLE compression
                    int k = 0;
                    if (GetBit(activeFlags, 6))
                    {
                        k = LoadByte(bytes);
                    }
                    if (GetBit(activeFlags, 7))
                    {
                        k = LoadInt16(bytes);
                    }

                    //Add tile to tileArray
                    Tiles.Add(new TileObject(tileType, liquidAmount, liquidType));

                    //Repeat addition for RLE compression count.
                    for (int i = 0; i < k && y < WorldHeight; i++)
                    {
                        y++;
                        Tiles.Add(new TileObject(tileType, liquidAmount, liquidType));
                    }
                }
            }
        }

        /// <summary>
        /// Loads all Chests from memory
        /// </summary>
        /// <param name="bytes">The MemoryStream to load</param>
        private void LoadChests(MemoryStream bytes)
        {
            int numberOfChests = LoadInt16(bytes);
            int NumberOfSlots = LoadInt16(bytes);

            //For each chest
            for (int i = 0; i < numberOfChests; i++)
            {
                //Get chest position
                int chestX = LoadInt32(bytes);
                int chestY = LoadInt32(bytes);
                string chestName = LoadString(bytes);

                //For each item slot in the chest
                List<ItemObject> items = new List<ItemObject>();
                for (int j = 0; j < NumberOfSlots; j++)
                {
                    int stackSize = LoadInt16(bytes);
                    if (stackSize != 0)
                    {
                        int itemID = LoadInt32(bytes);
                        int itemPrefix = LoadByte(bytes);
                        items.Add(new ItemObject(itemID, itemPrefix));
                    }
                }

                //Add new chest
                Chests.Add(new ChestObject(items, (chestX, chestY)));
            }
        }
    }
}
