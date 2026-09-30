using System;

namespace BlossomTales2.Randomizer.mm
{
    public class DarkWoodsBack : Region
    {
        public override Predicate<Inventory> CanAccess => inventory => World.Monsterton.CanAccess(inventory);

        public DarkWoodsBack(World world) : base("Periwinkle Woods Back", world)
        {
        }
    }
}
