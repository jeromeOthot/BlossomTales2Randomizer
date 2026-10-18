using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Newtonsoft.Json;

namespace BlossomTales2.Randomizer.mm
{
    public class RandomizerSingleton
    {
        public static RandomizerSingleton Instance { get; private set; }

        private Dictionary<LocationId, ItemData> _randomizedLocations;

        public static void Initialize()
        {
            Instance = new RandomizerSingleton();
            Instance.InitializeSetting();
        }

        public string SaveRandomizedLocations()
        {
            return JsonConvert.SerializeObject(_randomizedLocations);
        }

        public void LoadRandomizedLocations(string json)
        {
            if(json == null)
            {
                InitializeAsVanilla();
                return;
            }

            _randomizedLocations = JsonConvert.DeserializeObject<Dictionary<LocationId, ItemData>>(json);
        }

        public void GiveItemAtLocation(string name, Vector3 position)
        {
            ItemData item = GetItemByNameAndLocation(name, position);
            if(item != null)
                GiveItem(item);
        }

        public ItemData GetItemAtLocation(string mapName, string name, Vector3 position)
        {
            return TryGetItemAtLocation(mapName, name, position, out ItemData item) ? item : null;
        }

        public void GiveSideQuestReward(string sideQuestName)
        {
            ItemData item = GetItemAtLocation(string.Empty, sideQuestName, Vector3.Zero);
            if(item != null)
                GiveItem(item);
        }

        public ItemData GetItemByNameAndLocation(string name, Vector3 position)
        {
            if (TryGetItemAtLocation(Game1.CurrentLevel.Name, name, position, out ItemData item))
                return item;
            return null;
        }

        public bool TryGetItemAtLocation(string mapName, string name, Vector3 position, out ItemData item)
        {
            LocationId location = new  LocationId(mapName, name, position);
            return _randomizedLocations.TryGetValue(location, out item);
        }

        public ItemData TryGetItemWithMapNameAndName(string mapName, string name)
        {
            var itemByName = _randomizedLocations.Where(x => x.Key.MapName == mapName).Where(x => x.Key.Name == name).FirstOrDefault().Value;
            if(itemByName  == null)
                Game1.Dialoger.AddLine($"Item not found on {mapName} {name}");
            return itemByName;
        }

        public void GiveItem(ItemData itemData)
        {
            if (itemData.TryConvertToEquipableItem(out EquipableItem.ItemList item))
            {
                Game1.player.GiveItemReflection(item);
                return;
            }

            if (itemData.TryConvertToIngredientItem(out EquipableItem.IngredientList ingredient))
            {
                //Game1.player.GiveIngredientReflection(item);
                return;
            }

            if (itemData.TryConvertToSongItem(out Globaler.Songs song))
            {
                Game1.player.LearnSong(song);
                return;
            }
        }

        private void InitializeSetting()
        {
            RamdomizerSettings.Load("settings.json");
            //Exemple
            bool skip = RamdomizerSettings.Other.SkipInvasion;
            GameLogger.LogInfo("Seed number: " + RamdomizerSettings.SeedNumber);
            GameLogger.LogInfo("Check value SkipInvasion: " + RamdomizerSettings.Other.SkipInvasion);
            GameLogger.LogInfo("Check value: Dongeons" + RamdomizerSettings.ItemLocalisation.Dongeons);
        }

        private void InitializeAsVanilla()
        {
            World world = new World();
            List<Location> locations = world.CollectLocations();
            _randomizedLocations = new Dictionary<LocationId, ItemData>();
            foreach (Location location in locations)
                _randomizedLocations.Add(location.Id, new ItemData(location.VanillaItem));

            // if (ModGlobals.RandomizeColiseumCoins)
            // {
            //     _locationsVanilla.Add(new LocationId("colosseum.tmx", "price1", new Vector3(0f, 0f, 0f)), new ItemData(ItemType.GoldCoin));
            //     _locationsVanilla.Add(new LocationId("colosseum.tmx", "price2", new Vector3(0f, 0f, 0f)), new ItemData(ItemType.GoldCoin));
            //     _locationsVanilla.Add(new LocationId("colosseum.tmx", "price3", new Vector3(0f, 0f, 0f)), new ItemData(ItemType.GoldCoin));
            //     _locationsVanilla.Add(new LocationId("colosseum.tmx", "price4", new Vector3(0f, 0f, 0f)), new ItemData(ItemType.GoldCoin));
            //     _locationsVanilla.Add(new LocationId("colosseum.tmx", "price5", new Vector3(0f, 0f, 0f)), new ItemData(ItemType.GoldCoin));
            //     _locationsVanilla.Add(new LocationId("colosseum.tmx", "price6", new Vector3(0f, 0f, 0f)), new ItemData(ItemType.GoldCoin));
            //     _locationsVanilla.Add(new LocationId("colosseum.tmx", "price7", new Vector3(0f, 0f, 0f)), new ItemData(ItemType.GoldCoin));
            //     _locationsVanilla.Add(new LocationId("colosseum.tmx", "price8", new Vector3(0f, 0f, 0f)), new ItemData(ItemType.GoldCoin));
            //     _locationsVanilla.Add(new LocationId("colosseum.tmx", "price9", new Vector3(0f, 0f, 0f)), new ItemData(ItemType.GoldCoin));
            // }
        }

        public void RandomizeLocations()
        {
            int seed = RamdomizerSettings.SeedNumber.HasValue ? RamdomizerSettings.SeedNumber.Value : Environment.TickCount;
            if (seed == 0)
            {
                InitializeAsVanilla();
                return;
            }

            Spoiler spoilerLog = new Spoiler(seed);
            Random random = new Random(seed);
            World world;
            bool isValid;
            int tries = 0;
            do
            {
                tries++;

                Filler filler = new Filler(random);
                isValid = filler.TryFillWorld(out world);
                if (!isValid)
                    continue;

                isValid = filler.ValidateWorld(world, spoilerLog);
                if (!isValid)
                {
                    spoilerLog.ResetPlaythrough();
                    continue;
                }

                spoilerLog.CompletePlaythrough();

                _randomizedLocations = new Dictionary<LocationId, ItemData>();
                List<Location> locations = world.CollectLocations();
                foreach (Location location in locations)
                    _randomizedLocations.Add(location.Id, new ItemData(location.Item));

                ForceLocations();

            } while (!isValid && tries <= 1000);

            if (isValid)
            {
                GameLogger.LogInfo($"Total tries {tries}");
                OutputSpoilerLog(world, spoilerLog);
            }
            else
            {
                GameLogger.LogInfo("Too many tries. Setting game as vanilla.");
                InitializeAsVanilla();
            }
        }

        private void OutputSpoilerLog(World world, Spoiler spoiler)
        {
            List<Location> locations = new List<Location>();
            foreach (Region region in world.Regions)
            {
                locations.Clear();
                region.CollectLocations(locations);
                foreach (Location location in locations)
                    spoiler.AddLocationToSpoiler(region, location);
            }

            spoiler.ExportToFile();
        }

        private void ForceLocations()
        {
            //Uncomment and set the location id and item to what you need.
            //These one are the most easily accessed locations for a new game.
            // _randomizedLocations[new LocationId("blossom-lilyHouse.tmx", "Grandma 1", Vector3.Zero)] = new ItemData(ItemType.Bombs);
            // _randomizedLocations[new LocationId("blossom-lilyHouse.tmx", "Grandma 2", Vector3.Zero)] = new ItemData(ItemType.Bow);
            // _randomizedLocations[new LocationId("blossom-tavern-basement.tmx", "Chest_Small", new Vector3(256f, 0f, 272f))] = new ItemData(ItemType.Shield);
            // _randomizedLocations[new LocationId("blossom-tavern-basement.tmx", "Chest_Small", new Vector3(256f, 0f, 464f))] = new ItemData(ItemType.Shield);
            // _randomizedLocations[new LocationId("blossom-tavern-basement.tmx", "Chest_Small", new Vector3(448f, 0f, 272f))] = new ItemData(ItemType.Shield);
            // _randomizedLocations[new LocationId("blossom-tavern-basement.tmx", "Chest_Small", new Vector3(448f, 0f, 464f))] = new ItemData(ItemType.Flippers);
        }
    }
}
