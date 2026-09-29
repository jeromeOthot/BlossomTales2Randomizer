using System;

namespace BlossomTales2.Randomizer.mm
{
    public class LabyrinthBack : Region
    {
        public override Predicate<Inventory> CanAccess => inventory => true;

        public LabyrinthBack(World world) : base("Labyrinth Back", world)
        {
        }
    }
}
