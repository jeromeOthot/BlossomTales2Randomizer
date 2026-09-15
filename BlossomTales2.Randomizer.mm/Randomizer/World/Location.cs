using System;

namespace BlossomTales2.Randomizer.mm
{
    public class Location
    {
        public LocationId Id { get; set; }
        public string Name { get; set; }
        public Predicate<Inventory> CanAccess { get; set; }
        public ItemType Item { get; set; }
        public bool HasCollectedItem { get; set; }
    }
}
