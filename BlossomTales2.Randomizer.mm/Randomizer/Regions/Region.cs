using System;
using System.Collections.Generic;

namespace BlossomTales2.Randomizer.mm
{
    public abstract class Region
    {
        public string Name { get; private set; }
        //public List<Region> Regions { get; private set; } = new List<Region>();
        public List<Location> Locations { get; set; } = new List<Location>();
        public abstract Predicate<Inventory> CanAccess { get; }

        public Region(string name)
        {
            Name = name;
        }

        public bool TryCollectItems(Inventory inventory)
        {
            if (!CanAccess(inventory))
                return false;

            bool hasCollectedItem = false;

            // foreach (Region region in Regions)
            // {
            //     bool hasItem = region.TryCollectItems(inventory);
            //     if(!hasCollectedItem)
            //         hasCollectedItem = hasItem;
            // }

            foreach (Location location in Locations)
            {
                if (!location.CanAccess(inventory) || location.HasCollectedItem)
                    continue;

                inventory.AddItem(location.Item, 1);
                location.HasCollectedItem = true;
            }

            return hasCollectedItem;
        }

        public void CollectLocations(List<Location> locations)
        {
            // if (Regions != null)
            // {
            //     foreach (Region region in Regions)
            //         region.CollectLocations(locations);
            // }
            locations.AddRange(Locations);
        }
    }
}
