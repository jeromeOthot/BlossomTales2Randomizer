using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace BlossomTales2.Randomizer.mm
{
    public class LabyrinthBack : Region
    {
        public override bool CanAccess(Inventory inventory) => true;

        public LabyrinthBack(World world) : base("Labyrinth Back", world)
        {
            Locations = new List<Location>
            {
                {
                    new Location(
                    new LocationId("overworld-15x16.tmx", "Chest_Small", new Vector3(1308f, 0f, 2332f)),
                    "north far west chest", (inventory) => true,  ItemType.GoldCoin)
                },
                {
                    new Location(
                    new LocationId("overworld-15x17.tmx", "Chest_Small", new Vector3(2392f, 0f, 2348f)),
                    "center far west chest", (inventory) => true,  ItemType.GoldCoin)
                },
                {
                    new Location(
                    new LocationId("overworld-15x18.tmx", "Chest_Small", new Vector3(1948f, 0f, 292f)),
                    "south far west chest", (inventory) => true,  ItemType.GoldCoin)
                },

                {
                    new Location(
                    new LocationId("overworld-15x18-cave.tmx", "Chest_Small", new Vector3(384f, 0f, 268f)),
                    "Bombable Cave south west chest up left", (inventory) => true,  ItemType.GoldCoin)
                },
                {
                    new Location(
                    new LocationId("overworld-15x18-cave.tmx", "Chest_Small", new Vector3(576f, 0f, 268f)),
                    "Bombable Cave south west chest up right", (inventory) => true,  ItemType.GoldCoin)
                },
                {
                    new Location(
                    new LocationId("overworld-15x18-cave.tmx", "Chest_Small", new Vector3(384f, 0f, 456f)),
                    "Bombable Cave south west chest down left", (inventory) => true,  ItemType.GoldCoin)
                },
                {
                    new Location(
                    new LocationId("overworld-15x18-cave.tmx", "Chest_Small", new Vector3(572f, 0f, 456f)),
                    "Bombable Cave south west chest down right", (inventory) => true,  ItemType.GoldCoin)
                },
            };
        }
    }
}
