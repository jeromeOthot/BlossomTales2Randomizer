using System;

namespace BlossomTales2.Randomizer.mm
{
    public class JungleBack : Region
    {
        public override Predicate<Inventory> CanAccess => inventory => true;

        public JungleBack(World world) : base("Jungle Back", world)
        {
        }
    }
}
