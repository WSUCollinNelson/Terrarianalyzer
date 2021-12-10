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
        public int Version { get; set; }
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

        public WorldObject(MemoryStream bytes)
        {
            //Read through useless pre-header data
            Version = LoadInt32(bytes);
            Discard(bytes, 20);
            Int16 sectionCount = LoadInt16(bytes);
            Discard(bytes, 4 * sectionCount);
            Int16 tileMasks = LoadInt16(bytes);
            Discard(bytes, (int)Math.Ceiling((decimal)tileMasks / 8));
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
            bytes.Dispose();
        }

        private int LoadByte(MemoryStream bytes)
        {
            return bytes.ReadByte();
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
    }
}
