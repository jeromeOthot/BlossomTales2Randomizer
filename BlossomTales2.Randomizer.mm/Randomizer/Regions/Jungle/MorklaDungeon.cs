using System;

namespace BlossomTales2.Randomizer.mm
{
    public class MorklaDungeon : Region
    {
        public override Predicate<Inventory> CanAccess => inventory => true;

        public MorklaDungeon(World world) : base("Morkla", world)
        {
        }
    }
}
