using System.Collections.Generic;

namespace BlossomTales2.Randomizer.mm
{
    public class Validator
    {
        public static bool ValidateSeed(Dictionary<LocationId, ItemData> randomizedLocations)
        {
            World world = new World();//WorldFactory.Create();
            Inventory inventory = new Inventory();

            //Populate world
            List<Location> locations = world.CollectLocations();
            foreach (Location location in locations)
            {
                if(randomizedLocations.TryGetValue(location.Id, out ItemData itemData))
                    location.Item = itemData.Item;
            }

            //Validate seed
            bool hasCollectedItem;
            do
            {
                hasCollectedItem = world.TryCollectItems(inventory);
                if (inventory.HasBeatenMinotaurKing)
                    return true;

            } while (hasCollectedItem);

            return false;
        }
    }
}
