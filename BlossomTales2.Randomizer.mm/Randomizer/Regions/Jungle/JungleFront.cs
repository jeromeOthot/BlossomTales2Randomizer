using System;

namespace BlossomTales2.Randomizer.mm
{
    public class JungleFront : Region
    {
        public override Predicate<Inventory> CanAccess => inventory => true;

        public JungleFront(World world) : base("Jungle Front", world)
        {

        }
    }
}
