using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

#nullable disable
namespace BlossomTales2.Randomizer.mm
{
    public class RandomizerSingleton
    {
        public static RandomizerSingleton Instance { get; private set; }
        private Dictionary<string, EquipableItem.ItemList> _lootSpots;
        private Dictionary<LocationId, EquipableItem.ItemList> _locations;

        public static void Initialize()
        {
            Instance = new RandomizerSingleton();
            Instance.InitializeLootTables();
            Instance.InitializeLocations();
        }

        public EquipableItem.ItemList GetItemAtLocation(string location)
        {
            TryGetItemAtLocation(location, out EquipableItem.ItemList item);
            return item;
        }

        public EquipableItem.ItemList GetItemAtLocation(LocationId location)
        {
            return _locations[location];
        }

        public bool TryGetItemAtLocation(string location, out EquipableItem.ItemList item)
        {
            return _lootSpots.TryGetValue(location, out item);
        }

        private void InitializeLootTables()
        {
            _lootSpots = new Dictionary<string, EquipableItem.ItemList>
                {
                    { "blossom-house2.tmx:11:5", EquipableItem.ItemList.Guitar},
                    { "grandma_1", EquipableItem.ItemList.Guitar},
                };
        }

        private void InitializeLocations()
        {
            _locations = new Dictionary<LocationId, EquipableItem.ItemList>
            {
                { new LocationId("blossom-lilyHouse.tmx", "npc7", new Vector3(480f, 0f, 328f)), EquipableItem.ItemList.Guitar }
            };
        }
    }
}
