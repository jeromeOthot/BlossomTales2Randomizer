using System;

namespace BlossomTales2.Randomizer.mm
{
    public class Anchortown : Region
    {
        public override Predicate<Inventory> CanAccess => inventory => true;

        public Anchortown(World world) : base("Anchortown", world)
        {
        }
    }
}
