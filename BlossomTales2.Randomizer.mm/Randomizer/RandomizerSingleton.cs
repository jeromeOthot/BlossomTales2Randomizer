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
        private Dictionary<LocationId, EquipableItem.ItemList> _locations;

        public static void Initialize()
        {
            Instance = new RandomizerSingleton();
            Instance.InitializeLocations();
        }

        public EquipableItem.ItemList GetItemAtLocation(LocationId location)
        {
            return _locations[location];
        }

        public bool TryGetItemAtLocation(LocationId location, out EquipableItem.ItemList item)
        {
            return _locations.TryGetValue(location, out item);
        }

        private void InitializeLocations()
        {
            _locations = new Dictionary<LocationId, EquipableItem.ItemList>
            {
                { new LocationId("blossom-lilyHouse.tmx", "npc7", new Vector3(480f, 0f, 328f)), EquipableItem.ItemList.Guitar },
                { new LocationId("blossom-house2.tmx", "Chest_Small", new Vector3(708f, 0f, 356f)), EquipableItem.ItemList.Bow }
            };
        }
    }
}
