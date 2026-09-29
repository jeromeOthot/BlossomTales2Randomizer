using System;

namespace BlossomTales2.Randomizer.mm
{
    public class DarkWoodsFront : Region
    {
        public override Predicate<Inventory> CanAccess => inventory => true;

        public DarkWoodsFront(World world) : base("Periwinkle Woods Front", world)
        {
        }
    }
}
