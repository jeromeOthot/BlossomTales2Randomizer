using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace BlossomTales2.Randomizer.mm
{
    public class LabyrinthBack : Region
    {
        public override bool CanAccess(Inventory inventory) => World.Blockburg.CanAccessCyclops(inventory) && inventory.HasMirrorShield;
        public bool CanAccessLabyrinth15x16(Inventory inventory) => CanAccess(inventory) && inventory.CanSwitchLevers && inventory.HasMirrorShield;

        public LabyrinthBack(World world) : base("Labyrinth Back", world)
        {
            Locations = new List<Location>
            {
                //Caves
                new Location(new LocationId("overworld-15x18-cave.tmx", "Chest_Small", new Vector3(384f, 0f, 268f)),
                    "Bombable Cave Top Left Chest",
                    inventory => inventory.HasBombs,
                    ItemType.GoldCoin),
                new Location(new LocationId("overworld-15x18-cave.tmx", "Chest_Small", new Vector3(576f, 0f, 268f)),
                    "Bombable Cave Top Right Chest",
                    inventory => inventory.HasBombs,
                    ItemType.GoldCoin),
                new Location(new LocationId("overworld-15x18-cave.tmx", "Chest_Small", new Vector3(384f, 0f, 456f)),
                    "Bombable Cave Bottom Left Chest",
                    inventory =>  inventory.HasBombs,
                    ItemType.GoldCoin),
                new Location(new LocationId("overworld-15x18-cave.tmx", "Chest_Small", new Vector3(572f, 0f, 456f)),
                    "Bombable Cave Bottom Right Chest",
                    inventory => inventory.HasBombs,
                    ItemType.GoldCoin),
                //Chests
                new Location(new LocationId("overworld-15x16.tmx", "Chest_Small", new Vector3(1308f, 0f, 2332f)),
                    "Pink Pegs Chest",
                    inventory => CanAccessLabyrinth15x16(inventory),
                    ItemType.GoldCoin),
                new Location(new LocationId("overworld-15x17.tmx", "Chest_Small", new Vector3(2392f, 0f, 2348f)),
                    "center far west chest",
                    inventory => true,
                    ItemType.GoldCoin),
                new Location(new LocationId("overworld-15x18.tmx", "Chest_Small", new Vector3(1948f, 0f, 292f)),
                    "south far west chest",
                    inventory => true,
                    ItemType.GoldCoin)
            };
        }
    }
}
