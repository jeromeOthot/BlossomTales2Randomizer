using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace BlossomTales2.Randomizer.mm
{
    public class JungleFront : Region
    {
        public override Predicate<Inventory> CanAccess => inventory => (World.OverworldEast.CanAccess(inventory)
                                                                       && (inventory.CanCraftPotion(ItemType.Jar_Health) || inventory.HasFlipper))
                                                                       || (World.DarkWoodsFront.CanAccess(inventory) && inventory.HasGrappleHook);

        public JungleFront(World world) : base("Jungle Front", world)
        {
            Locations = new List<Location>()
            {
                //Caves
                new Location(new LocationId("jungles-23x20-cave.tmx", "Chest_Small", new Vector3(448f, 0f, 492f)),
                    "Bomb Cave Bottom Left Chest",
                    inventory => inventory.HasBombs,
                    ItemType.GoldCoin),
                new Location(new LocationId("jungles-23x20-cave.tmx", "Chest_Small", new Vector3(352f, 0f, 368f)),
                    "Bomb Cave Left Chest",
                    inventory => inventory.HasBombs,
                    ItemType.GoldCoin),
                new Location(new LocationId("jungles-23x20-cave.tmx", "Chest_Small", new Vector3(544f, 0f, 288f)),
                    "Bomb Cave Top Chest",
                    inventory => inventory.HasBombs,
                    ItemType.GoldCoin),
                new Location(new LocationId("jungles-23x20-cave.tmx", "Chest_Small", new Vector3(736f, 0f, 368f)),
                    "Bomb Cave Right Chest",
                    inventory => inventory.HasBombs,
                    ItemType.GoldCoin),
                new Location(new LocationId("jungles-23x20-cave.tmx", "Chest_Small", new Vector3(640f, 0f, 492f)),
                    "Bomb Cave Bottom Right Chest",
                    inventory => inventory.HasBombs,
                    ItemType.GoldCoin),
                //Short sidequests
                new Location(new LocationId("jungles-23x19.tmx", "archJungle", Vector3.Zero),
                    "Archeologist",
                    _ => true,
                    ItemType.Shovel),
                //Chests
                new Location(new LocationId("jungles-24x19.tmx", "Chest_Small", new Vector3(1548f, 0f, 1272f)),
                    "North Honeycomb Chest",
                    inventory => inventory.HasBombs || inventory.HasFlipper,
                    ItemType.Honeycomb),
                new Location(new LocationId("overworld-22x19.tmx", "Chest_Small", new Vector3(2240f, 0f, 1752f)),
                    "Vine Cliff Chest",
                    _ => true,
                    ItemType.GoldCoin),
            };
        }
    }
}
