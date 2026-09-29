using System;

namespace BlossomTales2.Randomizer.mm
{
    public class MansionDungeon : Region
    {
        public override Predicate<Inventory> CanAccess => inventory => true;

        public MansionDungeon(World world) : base("Mansion", world)
        {
        }
    }
}
