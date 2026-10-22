using System;

namespace BlossomTales2.Randomizer.mm
{
    public class Location
    {
        public LocationId Id { get; set; }
        public string Name { get; set; }
        public Predicate<Inventory> CanAccess { get; set; }
        public ItemType VanillaItem { get; set; }
        public ItemType Item { get; set; } = ItemType.None;
        public bool HasCollectedItem { get; set; }
        public LocationPool LocationPool { get; set; }

        public Location(LocationId id, string name,  Predicate<Inventory> canAccess, ItemType vanillaItem, LocationPool locationPool = LocationPool.Default)
        {
            Id = id;
            Name = name;
            CanAccess = canAccess;
            VanillaItem = vanillaItem;
            Item = ItemType.None;
            LocationPool = locationPool;
        }
    }
}
