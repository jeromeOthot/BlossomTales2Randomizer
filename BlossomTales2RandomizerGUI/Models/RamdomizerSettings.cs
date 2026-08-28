using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlossomTales2Randomizer.Models
{
    public static class RamdomizerSettings
    {
        public static OtherSetting Other;
        public static ItemLocalisationSetting ItemLocalisation;

        public class OtherSetting
        {
            public static bool SkipCutscenes = true;
            public static bool SkipFestival = true;
            public static bool SkipInvasion = true;
            public static bool OpenWorldState = true;
            public static bool RandomizeColiseumCoins = false;
        }

        public class ItemLocalisationSetting
        {
            public static bool Dongeons = false;
            public static bool Caves = false;
            public static bool NoteCaves = false;
            public static bool NPC = false;
            public static bool ShortSideQuest = false;
            public static bool LongSideQuest = false;
            public static bool MiniGames = false;
            public static bool Shops = false;
            public static bool Bards = false;
            public static bool Traders = false;
            public static bool Mausoleum = false;
        }
    }
}
