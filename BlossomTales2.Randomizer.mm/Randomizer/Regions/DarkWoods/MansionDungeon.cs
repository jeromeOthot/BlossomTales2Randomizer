using System;

namespace BlossomTales2.Randomizer.mm
{
    public class MansionDungeon : Region
    {
        public override Predicate<Inventory> CanAccess => inventory => World.DarkWoodsBack.CanAccess(inventory) && inventory.HasBoomerang;

        public MansionDungeon(World world) : base("Mansion", world)
        {
        }
    }
}
