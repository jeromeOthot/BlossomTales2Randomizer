using System;

namespace BlossomTales2.Randomizer.mm
{
    public class LabyrinthFront : Region
    {
        public override Predicate<Inventory> CanAccess => inventory => true;

        public LabyrinthFront(World world) : base("Labyrinth Front", world)
        {
        }
    }
}
