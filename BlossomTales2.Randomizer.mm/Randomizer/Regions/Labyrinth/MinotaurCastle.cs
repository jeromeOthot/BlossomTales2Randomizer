using System;

namespace BlossomTales2.Randomizer.mm
{
    public class MinotaurCastle : Region
    {
        public override Predicate<Inventory> CanAccess => inventory => true;

        public MinotaurCastle(World world) : base("Minotaur King's Castle", world)
        {
        }
    }
}
