using System;

namespace BlossomTales2.Randomizer.mm
{
    public class DarkWoodsBack : Region
    {
        public override Predicate<Inventory> CanAccess => inventory => true;

        public DarkWoodsBack(World world) : base("Periwinkle Woods Back", world)
        {
        }
    }
}
