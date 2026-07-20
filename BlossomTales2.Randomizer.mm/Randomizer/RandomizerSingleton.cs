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
                { new LocationId("blossom-house1.tmx", "Chest_Small", new Vector3(672f, 0f, 308f)), EquipableItem.ItemList.GoldCoin }, //Random small
                { new LocationId("blossom-house2.tmx", "Chest_Small", new Vector3(708f, 0f, 356f)), EquipableItem.ItemList.GoldCoin }, //Random small
                { new LocationId("blossom-house3.tmx", "Chest_Small", new Vector3(368f, 0f, 148f)), EquipableItem.ItemList.GoldCoin }, //Random small
                { new LocationId("blossom-house4.tmx", "Chest_Small", new Vector3(696f, 0f, 416f)), EquipableItem.ItemList.GoldCoin }, //Random small
                { new LocationId("blossom-house4.tmx", "Chest_Small", new Vector3(780f, 0f, 156f)), EquipableItem.ItemList.GoldCoin }, //Random small
                //{ new LocationId("overworld-20x20.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), EquipableItem.ItemList.GoldCoin }, //Random small, besoin gant force
                { new LocationId("overworld-21x20-cave.tmx", "Chest_Small", new Vector3(1192f, 0f, 612f)), EquipableItem.ItemList.GoldCoin }, //Random small
                { new LocationId("overworld-20x18.tmx", "lanternGuy", new Vector3(1036f, 0f, 660f)), EquipableItem.ItemList.Torch },
            };
        }
    }
}
