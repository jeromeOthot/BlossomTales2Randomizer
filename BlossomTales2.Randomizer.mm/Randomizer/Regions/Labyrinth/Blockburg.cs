using System;

namespace BlossomTales2.Randomizer.mm
{
    public class Blockburg : Region
    {
        public override Predicate<Inventory> CanAccess => inventory => true;

        public Blockburg(World world) : base("Blockburg", world)
        {
        }
    }
}
