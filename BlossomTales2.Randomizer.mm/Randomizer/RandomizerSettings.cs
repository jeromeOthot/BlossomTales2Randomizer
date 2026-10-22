// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
using System.IO;
using Newtonsoft.Json;

namespace BlossomTales2.Randomizer.mm
{
    public static class RandomizerSettings
    {
        public static uint? SeedNumber = null;
        public static GamePlaySettings GamePlay = new GamePlaySettings();

        public class GamePlaySettings
        {
            public bool SkipCutscenes = true;
            public bool OpenWorldState = true;
            public bool RandomizeColiseumCoins = false;
            public LocationPool LocationPools = LocationPool.None;
        }

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
            public bool Dungeons = false;
            public bool Caves = false;
            public bool NoteCaves = false;
            public bool Gifts = false;
            public bool ShortSideQuest = false;
            public bool LongSideQuest = false;
            public bool MiniGames = false;
            public bool Shops = false;
            public bool Traders = false;
            public bool Mausoleum = false;
        }

        public static void Load(string path)
        {
            string json = File.ReadAllText(path);

            var loaded = JsonConvert.DeserializeObject<RandomizerSettingsFile>(json);

            if (loaded != null)
            {
                SeedNumber = loaded.SeedNumber;
                if (loaded.GamePlay != null)
                {
                    GamePlay = loaded.GamePlay;
                }
            }
        }

        private class RandomizerSettingsFile
        {
            public uint? SeedNumber { get; set; }

            public GamePlaySettings GamePlay { get; set; }
          //  public OtherSetting Other { get; set; }
          //  public ItemLocalisationSetting ItemLocalisation { get; set; }
        }
    }
}
