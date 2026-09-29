using System;

namespace BlossomTales2.Randomizer.mm
{
    public class JungleNorthEast : Region
    {
        public override Predicate<Inventory> CanAccess => inventory => true;

        public JungleNorthEast(World world) : base("Jungle North East", world)
        {
        }
    }
}
