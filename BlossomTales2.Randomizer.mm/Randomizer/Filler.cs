using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

namespace BlossomTales2.Randomizer.mm
{
    public class Filler
    {
        private Random _random;

        public Filler(Random random)
        {
            _random = random;
        }

        public bool TryFillWorld(out World world)
        {
            world = new World();
            List<Location> locations = world.CollectLocations();
            ShuffleList(locations);

            PlaceKeyPieces(locations);

            Inventory assumedInventory = new Inventory(world);
            List<ItemType> progressionPool = new List<ItemType>(ItemPools.CreateProgressionItemsPool());
            List<ItemType> usefulItemsPool = new List<ItemType>(ItemPools.CreateUsefulItemsPool());
            assumedInventory.AddItem(progressionPool);
            assumedInventory.AddItem(usefulItemsPool);

            if (!AssumedFill(progressionPool, assumedInventory, locations))
                return false;

            if (!AssumedFill(usefulItemsPool, assumedInventory, locations))
                return false;

            foreach (Location remainingLocation in locations)
                remainingLocation.Item = ItemType.GoldCoin;

            return true;
        }

        private bool AssumedFill(List<ItemType> itemPool, Inventory assumedInventory, List<Location> locations)
        {
            ShuffleList(itemPool);
            foreach (ItemType itemToPlace in itemPool)
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
            }

            return true;
        }

        private void PlaceKeyPieces(List<Location> locations)
        {
            //TODO: Handle key pieces correctly
            LocationId key1Location = new LocationId("morkla-octopus.tmx", "Chest", new Vector3(896f, 0f, 624f));
            LocationId key2Location = new LocationId("temple-genieBoss.tmx", "Chest", new Vector3(832f, 0f, 640f));
            LocationId key3Location = new LocationId("mansion-bossScientist.tmx", "Chest", new Vector3(704f, 0f, 448f));

            List<ItemType> keyPool = new List<ItemType>(ItemPools.CreateKeyRewardsPool());
            ShuffleList(keyPool);

            for (int i = locations.Count - 1; i >= 0; i--)
            {
                if (keyPool.Count == 0)
                    break;

                Location location = locations[i];
                if (location.Item != ItemType.None)
                    continue;

                if (location.Id.Equals(key1Location)
                    || location.Id.Equals(key2Location)
                    || location.Id.Equals(key3Location))
                {
                    location.Item = keyPool[0];
                    keyPool.RemoveAt(0);
                    locations.RemoveAt(i);
                }
            }
        }


        public bool ValidateWorld(World world)
        {
            Inventory inventory = new Inventory(world);

            //Validate seed
            int sphere = 0;
            StringBuilder spheres = new StringBuilder();

            bool hasCollectedItem;
            do
            {
                spheres.AppendLine($"---- Sphere: {sphere} ----");
                List<ItemType> newItems = world.CollectItems(inventory, ref spheres);
                hasCollectedItem = newItems.Count > 0;

                foreach (ItemType newItem in newItems)
                    inventory.AddItem(newItem, 1);

                if (world.MinotaurCastle.CanAccessMinotaurBoss(inventory))
                {
                    spheres.AppendLine("Defeat Minotaur King: Victory!!!");
                    GameLogger.LogInfo(spheres.ToString());
                    return true;
                }

                sphere++;
            } while (hasCollectedItem);

            return false;
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
