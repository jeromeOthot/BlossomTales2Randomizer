using System.Collections.Generic;
using System.Text;

namespace BlossomTales2.Randomizer.mm
{
    public class Validator
    {
        public static bool ValidateSeed(Dictionary<LocationId, ItemData> randomizedLocations)
        {
            World world = new World();
            Inventory inventory = new Inventory(world);

            //Populate world
            List<Location> locations = world.CollectLocations();
            foreach (Location location in locations)
            {
                if(randomizedLocations.TryGetValue(location.Id, out ItemData itemData))
                    location.Item = itemData.Item;
            }

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
    }
}
