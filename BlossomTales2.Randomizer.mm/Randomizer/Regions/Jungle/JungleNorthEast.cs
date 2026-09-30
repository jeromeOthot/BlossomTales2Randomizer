using System;

namespace BlossomTales2.Randomizer.mm
{
    public class JungleNorthEast : Region
    {
        public override Predicate<Inventory> CanAccess => inventory => World.JungleBack.CanAccess(inventory) && inventory.HasFlipper;

        public JungleNorthEast(World world) : base("Jungle North East", world)
        {
        }
    }
}
