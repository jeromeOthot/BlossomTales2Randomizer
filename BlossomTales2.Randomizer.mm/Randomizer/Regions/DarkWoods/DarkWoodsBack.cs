using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace BlossomTales2.Randomizer.mm
{
    public class DarkWoodsBack : Region
    {
        public override bool CanAccess(Inventory inventory) => World.Monsterton.CanAccess(inventory);

        public DarkWoodsBack(World world) : base("Periwinkle Woods Back", world)
        {
            Locations = new List<Location>
            {
                //Caves
                new Location(new LocationId("overworld-25x16-cave.tmx", "Chest_Small", new Vector3(416f, 0f, 304f)),
                    "Mansion Ledge Bomb Cave Top Left Chest",
                    inventory => inventory.HasBoomerang && inventory.HasBombs,
                    ItemType.GoldCoin),
                new Location(new LocationId("overworld-25x16-cave.tmx", "Chest_Small", new Vector3(544f, 0f, 304f)),
                    "Mansion Ledge Bomb Cave Top Right Chest",
                    inventory => inventory.HasBoomerang && inventory.HasBombs,
                    ItemType.GoldCoin),
                new Location(new LocationId("overworld-25x16-cave.tmx", "Chest_Small", new Vector3(352f, 0f, 432f)),
                    "Mansion Ledge Bomb Cave Bottom Left Chest",
                    inventory => inventory.HasBoomerang && inventory.HasBombs,
                    ItemType.GoldCoin),
                new Location(new LocationId("overworld-25x16-cave.tmx", "Chest_Small", new Vector3(608f, 0f, 432f)),
                    "Mansion Ledge Bomb Cave Bottom Right Chest",
                    inventory =>  inventory.HasBoomerang && inventory.HasBombs,
                    ItemType.GoldCoin),
                new Location(new LocationId("overworld-25x17-cave.tmx", "Chest_Small", new Vector3(320f, 0f, 352f)),
                    "Trader Bomb Cave Left Chest",
                    inventory => inventory.HasBombs,
                    ItemType.GoldCoin),
                new Location(new LocationId("overworld-25x17-cave.tmx", "Chest_Small", new Vector3(544f, 0f, 352f)),
                    "Trader Bomb Cave Middle Chest",
                    inventory => inventory.HasBombs,
                    ItemType.GoldCoin),
                new Location(new LocationId("overworld-25x17-cave.tmx", "Chest_Small", new Vector3(772f, 0f, 352f)),
                    "Trader Bomb Cave Right Chest",
                    inventory => inventory.HasBombs,
                    ItemType.GoldCoin),
                //mausoleum
                new Location(new LocationId("overworld-24x16-mausoleum.tmx", "Chest_Small", new Vector3(736f, 0f, 200f)),
                    "Mausoleum Chest",
                    _ => true,
                    ItemType.HeartQ_1),
                new Location(new LocationId("overworld-25x17-combat.tmx", "Chest_Small", new Vector3(1280f, 0f, 516f)),
                    "Combat Mausoleum Chest",
                    inventory => inventory.HasBombs || inventory.HasRexTeleporter,
                    ItemType.CombatScroll),
            };
        }
    }
}
