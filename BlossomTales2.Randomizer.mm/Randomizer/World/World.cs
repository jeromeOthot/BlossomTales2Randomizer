using System.Collections.Generic;

namespace BlossomTales2.Randomizer.mm
{
    public class World
    {
        public List<Region> Regions { get; set; }
        public Inventory Inventory { get; set; }

        public bool TryCollectItems(Inventory inventory)
        {
            bool hasCollectedItem = false;

            foreach (Region region in Regions)
            {
                bool hasItem = region.TryCollectItems(inventory);
                if(!hasCollectedItem)
                    hasCollectedItem = hasItem;
            }
            return hasCollectedItem;
        }

        public List<Location> CollectLocations()
        {
            List<Location> locations = new List<Location>();
            foreach (Region region in Regions)
                region.CollectLocations(locations);

            return locations;
        }
    }
}
