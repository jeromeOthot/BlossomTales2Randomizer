using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace BlossomTales2.Randomizer.mm
{
    public class LabyrinthFront : Region
    {
        public override bool CanAccess(Inventory inventory) => World.OverworldWest.CanAccess(inventory) && inventory.HasAllKeyPieces && (inventory.HasBombs || inventory.HasBoomerang);

        private bool CanAccessLabyrinth18x16(Inventory inventory) => CanAccess(inventory) && inventory.CanSwitchLevers && inventory.HasGrappleHook && inventory.HasTorch && inventory.CanHitTargets;
        private bool CanAccessLabyrinth17x16(Inventory inventory) => CanAccessLabyrinth18x16(inventory) && inventory.CanSwitchLevers;
        public bool CanAccessLabyrinth17x18(Inventory inventory) => CanAccessLabyrinth17x16(inventory) && inventory.CanSwitchLevers && inventory.CanHitTargets;

        //accès labyrinthe 17x18: ...

        public LabyrinthFront(World world) : base("Labyrinth Front", world)
        {
            Locations = new List<Location>
            {
                //Caves
                new Location(new LocationId("overworld-17x16-cave.tmx", "Chest_Small", new Vector3(512f, 0f, 268f)),
                    "North Bomb Cave Top Left Chest",
                    inventory => CanAccessLabyrinth17x16(inventory) && inventory.HasBombs,
                    ItemType.GoldCoin), //accès labyrinthe 17x16 && bombes
                new Location(new LocationId("overworld-17x16-cave.tmx", "Chest_Small", new Vector3(672f, 0f, 268f)),
                    "North Bomb Cave Top Center Chest",
                    inventory => CanAccessLabyrinth17x16(inventory) && inventory.HasBombs,
                    ItemType.GoldCoin),
                new Location(new LocationId("overworld-17x16-cave.tmx", "Chest_Small", new Vector3(832f, 0f, 268f)),
                    "North Bomb Cave Top Right Chest",
                    inventory => CanAccessLabyrinth17x16(inventory) && inventory.HasBombs,
                    ItemType.GoldCoin),
                new Location(new LocationId("overworld-17x16-cave.tmx", "Chest_Small", new Vector3(512f, 0f, 456f)),
                    "North Bomb Cave Bottom Left Chest",
                    inventory => CanAccessLabyrinth17x16(inventory) && inventory.HasBombs,
                    ItemType.GoldCoin),
                new Location(new LocationId("overworld-17x16-cave.tmx", "Chest_Small", new Vector3(672f, 0f, 456f)),
                    "North Bomb Cave Bottom Center Chest",
                    inventory => CanAccessLabyrinth17x16(inventory) && inventory.HasBombs,
                    ItemType.GoldCoin),
                new Location(new LocationId("overworld-17x16-cave.tmx", "Chest_Small", new Vector3(828f, 0f, 456f)),
                    "North Bomb Cave Bottom Right Chest",
                    inventory => CanAccessLabyrinth17x16(inventory) && inventory.HasBombs,
                    ItemType.GoldCoin),
                new Location(new LocationId("overworld-18x17-cave.tmx", "Chest_Small", new Vector3(384f, 0f, 268f)),
                    "East Bomb Cave Left Chest",
                    inventory => inventory.HasBombs && inventory.HasGrappleHook,
                    ItemType.GoldCoin),
                new Location(new LocationId("overworld-18x17-cave.tmx", "Chest_Small", new Vector3(480f, 0f, 268f)),
                    "East Bomb Cave Middle Chest",
                    inventory => inventory.HasBombs && inventory.HasGrappleHook,
                    ItemType.GoldCoin),
                new Location(new LocationId("overworld-18x17-cave.tmx", "Chest_Small", new Vector3(576f, 0f, 268f)),
                    "East Bomb Cave Right Chest",
                    inventory => inventory.HasBombs && inventory.HasGrappleHook,
                    ItemType.GoldCoin),
                //Npcs
                new Location(new LocationId("labyrinth-forge.tmx", "golemHead", Vector3.Zero),
                    "Golem Forge",
                    inventory => inventory.IngredGemCount >= 50 || inventory.HasShovel,
                    ItemType.Sword),
                //Chests
                new Location(new LocationId("overworld-16x17.tmx", "Chest_Small", new Vector3(1576f, 0f, 280f)),
                    "Near Blockburg Chest",
                    inventory => CanAccessLabyrinth17x16(inventory),
                    ItemType.GoldCoin),
                new Location(new LocationId("overworld-17x16.tmx", "Chest_Small", new Vector3(2376f, 0f, 868f)),
                    "North Blue Tree Chest",
                    inventory => CanAccessLabyrinth17x16(inventory),
                    ItemType.GoldCoin),
                new Location(new LocationId("overworld-17x18.tmx", "Chest_Small", new Vector3(1308f, 0f, 224f)),
                    "South blue Tree Chest",
                    inventory => CanAccessLabyrinth17x18(inventory),
                    ItemType.GoldCoin),

            };
        }
    }
}
