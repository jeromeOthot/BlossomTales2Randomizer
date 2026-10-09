using System;
using System.Collections.Generic;
using System.Text;

namespace BlossomTales2.Randomizer.mm
{
    public abstract class Region
    {
        public string Name { get; private set; }
        //public List<Region> Regions { get; private set; } = new List<Region>();
        public List<Location> Locations { get; set; } = new List<Location>();
        public abstract bool CanAccess(Inventory inventory);

        protected World World { get ; private set; }

        public Region(string name, World world)
        {
            Name = name;
            World = world;
        }


        public List<ItemType> CollectItems(Inventory inventory, ref StringBuilder spheres)
        {
            List<ItemType> newItems = new List<ItemType>();

            if (!CanAccess(inventory))
                return newItems;

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

                newItems.Add(location.Item);
                if(location.Item != ItemType.GoldCoin)
                    spheres.AppendLine($"{Name} {location.Name} : {location.Item}");
                location.HasCollectedItem = true;
            }

            return newItems;
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
