// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
using System.IO;
using Newtonsoft.Json;

namespace BlossomTales2.Randomizer.mm
{
    public static class RamdomizerSettings
    {
        public static OtherSetting Other = new OtherSetting();
        public static ItemLocalisationSetting ItemLocalisation = new ItemLocalisationSetting();

        public class OtherSetting
        {
            public bool SkipCutscenes = true;
            public bool SkipFestival = true;
            public bool SkipInvasion = true;
            public bool OpenWorldState = true;
            public bool RandomizeColiseumCoins = false;
        }

        public class ItemLocalisationSetting
        {
            public bool Dongeons = false;
            public bool Caves = false;
            public bool NoteCaves = false;
            public bool NPC = false;
            public bool ShortSideQuest = false;
            public bool LongSideQuest = false;
            public bool MiniGames = false;
            public bool Shops = false;
            public bool Bards = false;
            public bool Traders = false;
            public bool Mausoleum = false;
        }

        public static void Load(string path)
        {
            string json = File.ReadAllText(path);

            var loaded = JsonConvert.DeserializeObject<RamdomizerSettingsFile>(json);

            if (loaded != null)
            {
                if (loaded.Other != null)
                    Other = loaded.Other;

                if (loaded.ItemLocalisation != null)
                    ItemLocalisation = loaded.ItemLocalisation;
            }
        }

        private class RamdomizerSettingsFile
        {
            public OtherSetting Other { get; set; }
            public ItemLocalisationSetting ItemLocalisation { get; set; }
        }
    }
}
