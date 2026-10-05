using System;

namespace BlossomTales2.Randomizer.mm
{
    public class LabyrinthBack : Region
    {
        public override bool CanAccess(Inventory inventory) => true;

        public LabyrinthBack(World world) : base("Labyrinth Back", world)
        {
        }
    }
}
