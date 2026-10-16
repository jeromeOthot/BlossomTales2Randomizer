using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Newtonsoft.Json;

namespace BlossomTales2.Randomizer.mm
{
    public class RandomizerSingleton
    {
        public static RandomizerSingleton Instance { get; private set; }
        private Dictionary<LocationId, ItemData> _locationsVanilla;
        private Dictionary<LocationId, ItemData> _randomizedLocations;
        private Random _random;

        public static void Initialize()
        {
            Instance = new RandomizerSingleton();
            Instance.InitializeSetting();
            Instance.InitializeLocations();
        }

        public void RandomizeLocations()
        {
            RandomizeItems();
        }

        public string SaveRandomizedLocations()
        {
            return JsonConvert.SerializeObject(_randomizedLocations);
        }

        public void LoadRandomizedLocations(string json)
        {
            if(json == null)
            {
                _randomizedLocations = _locationsVanilla;
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

        private void InitializeLocations()
        {
            World world = new World();
            List<Location> locations = world.CollectLocations();
            _locationsVanilla = new Dictionary<LocationId, ItemData>();
            foreach (Location location in locations)
                _locationsVanilla.Add(location.Id, new ItemData(location.VanillaItem));

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

        private void RandomizeItems()
        {
            int seed = RamdomizerSettings.SeedNumber.HasValue ? RamdomizerSettings.SeedNumber.Value : Environment.TickCount;
            _random = new Random(seed);

            GameLogger.LogInfo($"Seed = {seed}");

            if(seed == 0)
                _randomizedLocations = _locationsVanilla;
            else
                PlaceItems();
        }

        private void PlaceItems()
        {


            bool isValid;
            int tries = 0;
            do
            {
                tries++;
                isValid = FillLocations();
                if (!isValid)
                {
                    continue;
                }

                isValid = Validator.ValidateSeed(_randomizedLocations);
            } while (!isValid && tries <= 1000);

            if (isValid)
            {
                GameLogger.LogInfo($"Total tries {tries}");
                GameLogger.LogInfo("Spoiler log begin:");
                foreach (KeyValuePair<LocationId, ItemData> kvp in _randomizedLocations)
                {
                    GameLogger.LogInfo($"{kvp.Key}: {kvp.Value}");
                }
                GameLogger.LogInfo("Spoiler log end");
            }
            else
            {
                GameLogger.LogInfo("Too many tries. Setting game as vanilla.");
                _randomizedLocations = _locationsVanilla;
            }
        }

        private bool FillLocations()
        {
            // List<ItemType> itemPool = new List<ItemType>();
            // itemPool.AddRange(ItemPools.CreateProgressionItemsPool());
            // itemPool.AddRange(ItemPools.CreateUsefulItemsPool());

            //TODO: Handle key pieces correctly
            LocationId key1Location = new LocationId("morkla-octopus.tmx", "Chest", new Vector3(896f, 0f, 624f));
            LocationId key2Location = new LocationId("temple-genieBoss.tmx", "Chest", new Vector3(832f, 0f, 640f));
            LocationId key3Location = new LocationId("mansion-bossScientist.tmx", "Chest", new Vector3(704f, 0f, 448f));

            _randomizedLocations = new Dictionary<LocationId, ItemData>();
            //Place key pieces
            List<ItemType> keyPool = new List<ItemType>(ItemPools.CreateKeyRewardsPool());
            ShuffleList(keyPool);
            _randomizedLocations.Add(key1Location, new ItemData(keyPool[0]));
            _randomizedLocations.Add(key2Location, new ItemData(keyPool[1]));
            _randomizedLocations.Add(key3Location, new ItemData(keyPool[2]));

            World world = new World();
            List<Location> locations = world.CollectLocations();
            for (int i = locations.Count - 1; i >= 0; i--)
            {
                Location locationId = locations[i];
                //TODO: Handle key pieces
                if (locationId.Id.Equals(key1Location) ||  locationId.Id.Equals(key2Location) || locationId.Id.Equals(key3Location))
                    locations.RemoveAt(i);
            }

            ShuffleList(locations);

            Inventory assumedInventory = new Inventory(world);
            List<ItemType> progressionPool = new List<ItemType>(ItemPools.CreateProgressionItemsPool());
            List<ItemType> usefulItemsPool = new List<ItemType>(ItemPools.CreateUsefulItemsPool());
            assumedInventory.AddItem(progressionPool);
            assumedInventory.AddItem(usefulItemsPool);
            ShuffleList(progressionPool);
            ShuffleList(usefulItemsPool);
            //Place progression item

            foreach (ItemType itemToPlace in progressionPool)
            {
                assumedInventory.RemoveItem(itemToPlace, 1);
                Location assignableLocation = locations.Find(loc => loc.CanAccess(assumedInventory));
                if (assignableLocation == null)
                {
                    //Pas trouvé location
                    GameLogger.LogInfo($"Cannot place {itemToPlace}, seed not valid");
                    return false;
                }
                assignableLocation.Item = itemToPlace;
                locations.Remove(assignableLocation);
                _randomizedLocations.Add(assignableLocation.Id, new ItemData(itemToPlace));
            }

            foreach (ItemType itemToPlace in usefulItemsPool)
            {
                assumedInventory.RemoveItem(itemToPlace, 1);
                Location assignableLocation = locations.Find(loc => loc.CanAccess(assumedInventory));
                if (assignableLocation == null)
                {
                    //Pas trouvé location
                    GameLogger.LogInfo($"Cannot place {itemToPlace}, seed not valid");
                    return false;
                }
                assignableLocation.Item = itemToPlace;
                locations.Remove(assignableLocation);
                _randomizedLocations.Add(assignableLocation.Id, new ItemData(itemToPlace));
            }

            foreach (Location remainingLocation in locations)
                _randomizedLocations.Add(remainingLocation.Id, new ItemData(ItemType.GoldCoin));

            return true;
        }

        private void ShuffleList<T>(List<T> list)
        {
            //Fisher-yates shuffle
            for (int i = list.Count - 1; i > 0; i--)
            {
                int j = _random.Next(0, i+1);
                T temp = list[i];
                list[i] = list[j];
                list[j] = temp;
            }
        }
    }
}
