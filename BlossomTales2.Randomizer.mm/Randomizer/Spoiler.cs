// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.Text;
using Microsoft.Xna.Framework;

namespace BlossomTales2.Randomizer.mm
{
    public class Spoiler
    {
        private StringBuilder _spoilerLog = new StringBuilder();
        private StringBuilder _spheres = new StringBuilder();
        private int _seed;

        public Spoiler(int seed)
        {
            _seed = seed;
            _spoilerLog.AppendLine("Spoiler log:");
            _spheres.AppendLine("Playthough:");
        }

        public void ResetPlaythrough()
        {
            _spheres.Clear();
            _spheres.AppendLine("Playthough:");
        }

        public void SetSphere(int sphere)
        {
            _spheres.AppendLine($"---- Sphere: {sphere} ----");
        }

        public void AddLocationToSpoiler(Region region, Location location)
        {
            AddToLog(_spoilerLog, region, location);
        }

        public void AddLocationToPlaythrough(Region region, Location location)
        {
            if (location.Item == ItemType.GoldCoin)
                return;

            AddToLog(_spheres, region, location);
        }

        private void AddToLog(StringBuilder log, Region region, Location location)
        {
            log.AppendLine($"{region.Name} - {location.Name} : {location.Item}");
        }

        public void CompletePlaythrough()
        {
            _spheres.AppendLine("Defeat Minotaur King: Victory!!!");
        }

        public void ExportToFile()
        {
            GameLogger.LogInfo($"Seed = {_seed}");
            GameLogger.LogInfo(_spoilerLog.ToString());
            GameLogger.LogInfo(string.Empty);
            GameLogger.LogInfo(_spheres.ToString());
        }
    }
}
