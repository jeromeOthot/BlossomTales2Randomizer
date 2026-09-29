using System;

namespace BlossomTales2.Randomizer.mm
{
    public class JungleIsland : Region
    {
        public override Predicate<Inventory> CanAccess => inventory => true;

        public JungleIsland(World world) : base("Jungle Island", world)
        {
        }
    }
}
