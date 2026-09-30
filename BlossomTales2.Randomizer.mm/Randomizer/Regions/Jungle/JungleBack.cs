using System;

namespace BlossomTales2.Randomizer.mm
{
    public class JungleBack : Region
    {
        public override Predicate<Inventory> CanAccess => inventory => World.Anchortown.CanAccess(inventory);

        public JungleBack(World world) : base("Jungle Back", world)
        {
        }
    }
}
