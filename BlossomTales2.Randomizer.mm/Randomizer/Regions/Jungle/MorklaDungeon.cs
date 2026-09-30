using System;

namespace BlossomTales2.Randomizer.mm
{
    public class MorklaDungeon : Region
    {
        public override Predicate<Inventory> CanAccess => inventory => World.JungleBack.CanAccess(inventory)
                                                                       && inventory.HasFishingRod
                                                                       && (inventory.HasBombs || inventory.HasFlipper);

        public MorklaDungeon(World world) : base("Morkla", world)
        {
        }
    }
}
