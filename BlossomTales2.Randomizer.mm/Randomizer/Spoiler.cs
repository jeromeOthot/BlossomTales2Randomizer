using System.Collections.Generic;
using System.IO;
using System.Text;

namespace BlossomTales2.Randomizer.mm
{
    public class Spoiler
    {
        private const string SpoilerLogFileName = "SpoilerLog_slot_{0}_{1}.txt";

        private StringBuilder _spoilerLog = new StringBuilder();
        private StringBuilder _spheres = new StringBuilder();
        private uint _seed;
        private List<ItemType> _progressionItems = new List<ItemType>();

        public Spoiler(uint seed)
        {
            _seed = seed;
            _spoilerLog.AppendLine("Spoiler log:");
            _spheres.AppendLine("Playthough:");
            CreateProgressionItemList();
        }

        private void CreateProgressionItemList()
        {
            _progressionItems.Clear();
            _progressionItems.AddRange(ItemPools.CreateProgressionItemsPool());
            _progressionItems.AddRange(ItemPools.CreateKeyRewardsPool());
        }

        public void ResetPlaythrough()
        {
            CreateProgressionItemList();
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
            ItemType item = location.Item;

            //Reverse search bottles in progression
            if(item >= ItemType.Jar_Empty && item <= ItemType.Jar_Speedster)
                item = ItemType.Jar_Empty;

            if (!_progressionItems.Remove(item))
                return;

            AddToLog(_spheres, region, location);
        }

        private void AddToLog(StringBuilder log, Region region, Location location)
        {
            log.AppendLine($"{region.Name} - {location.Name}: {location.Item.GetPrettyName()}");
        }

        public void CompletePlaythrough()
        {
            _spheres.AppendLine("Defeat Minotaur King: Victory!!!");
        }

        public void ExportToFile()
        {
            StringBuilder log = new StringBuilder();
            log.AppendLine($"Seed = {_seed}");
            log.AppendLine(_spoilerLog.ToString());
            log.AppendLine(_spheres.ToString());

            string path = Path.Combine(Game1.OSDir, string.Format(SpoilerLogFileName, Game1.SaveSlotSelected + 1, _seed));
            File.WriteAllText(path, log.ToString());
        }
    }
}
