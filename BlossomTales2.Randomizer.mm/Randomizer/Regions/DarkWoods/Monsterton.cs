using System;

namespace BlossomTales2.Randomizer.mm
{
    public class Monsterton : Region
    {
        public override Predicate<Inventory> CanAccess => inventory => true;

        public Monsterton(World world) : base("Monsterton", world)
        {
        }
    }
}
