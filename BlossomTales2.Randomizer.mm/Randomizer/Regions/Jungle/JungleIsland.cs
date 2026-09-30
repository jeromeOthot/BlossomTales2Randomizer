using System;

namespace BlossomTales2.Randomizer.mm
{
    public class JungleIsland : Region
    {
        public override Predicate<Inventory> CanAccess => inventory => World.OverworldEast.CanAccess(inventory) && inventory.HasFlipper
        || World.CanyonIsland.CanAccess(inventory) &&  inventory.HasGrappleHook;

        public JungleIsland(World world) : base("Jungle Island", world)
        {
        }
    }
}
