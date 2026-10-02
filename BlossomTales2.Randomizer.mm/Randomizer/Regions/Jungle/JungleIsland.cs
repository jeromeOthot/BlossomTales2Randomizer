using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace BlossomTales2.Randomizer.mm
{
    public class JungleIsland : Region
    {
        public override Predicate<Inventory> CanAccess => inventory => World.OverworldEast.CanAccess(inventory) && inventory.HasFlipper
        || World.CanyonIsland.CanAccess(inventory) &&  inventory.HasGrappleHook;

        public JungleIsland(World world) : base("Jungle Island", world)
        {
            Locations = new List<Location>()
            {
                //Note caves
                new Location(new LocationId("jungles-22x21-noteCave.tmx", "Chest_Small", new Vector3(480f, 0f, 224f)),
                    "Note Cave Top Left Chest",
                    inventory => inventory.CanOpenNoteDoor,
                    ItemType.GoldCoin),
                new Location(new LocationId("jungles-22x21-noteCave.tmx", "Chest_Small", new Vector3(608f, 0f, 224f)),
                    "Note Cave Top Right Chest",
                    inventory => inventory.CanOpenNoteDoor,
                    ItemType.GoldCoin),
                new Location(new LocationId("jungles-22x21-noteCave.tmx", "Chest_Small", new Vector3(480f, 0f, 352f)),
                    "Note Cave Bottom Left Chest",
                    inventory => inventory.CanOpenNoteDoor,
                    ItemType.GoldCoin),
                new Location(new LocationId("jungles-22x21-noteCave.tmx", "Chest_Small", new Vector3(608f, 0f, 352f)),
                    "Note Cave Bottom Right Chest",
                    inventory => inventory.CanOpenNoteDoor,
                    ItemType.GoldCoin),
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
                //short sidequests
                new Location(new LocationId("jungles-21x22.tmx", "hunter", Vector3.Zero),
                    "Troll Hunter",
                    inventory => inventory.HasGrappleHook && inventory.HasBow,
                    ItemType.Bow),
                //Chests
                new Location(new LocationId("jungles-21x22.tmx", "Chest_Small", new Vector3(852f, 0f, 684f)),
                    "Hunter Camp Chest",
                    inventory => inventory.HasGrappleHook,
                    ItemType.GoldCoin),
                new Location(new LocationId("jungles-21x22.tmx", "Chest_Small", new Vector3(1336f, 0f, 896f)),
                    "Bomb Rock Chest",
                    inventory => inventory.HasBombs,
                    ItemType.GoldCoin),
                new Location(new LocationId("jungles-21x22-island.tmx", "Chest_Small", new Vector3(552f, 0f, 688f)),
                    "Secret Island Top Left Buried Chest",
                    inventory => inventory.HasFlipper && inventory.HasShovel,
                    ItemType.GoldCoin),
                new Location(new LocationId("jungles-21x22-island.tmx", "Chest_Small", new Vector3(864f, 0f, 684f)),
                    "Secret Island Top Right Buried Chest",
                    inventory => inventory.HasFlipper && inventory.HasShovel,
                    ItemType.GoldCoin),
                new Location(new LocationId("jungles-21x22-island.tmx", "Chest_Small", new Vector3(524f, 0f, 808f)),
                    "Secret Island Middle Left Buried Chest",
                    inventory => inventory.HasFlipper && inventory.HasShovel,
                    ItemType.GoldCoin),
                new Location(new LocationId("jungles-21x22-island.tmx", "Chest_Small", new Vector3(924f, 0f, 788f)),
                    "Secret Island Middle Right Buried Chest",
                    inventory => inventory.HasFlipper && inventory.HasShovel,
                    ItemType.GoldCoin),
                new Location(new LocationId("jungles-21x22-island.tmx", "Chest_Small", new Vector3(600f, 0f, 888f)),
                    "Secret Island Bottom Left Buried Chest",
                    inventory => inventory.HasFlipper && inventory.HasShovel,
                    ItemType.GoldCoin),
                new Location(new LocationId("jungles-21x22-island.tmx", "Chest_Small", new Vector3(804f, 0f, 892f)),
                    "Secret Island Bottom Right Buried Chest",
                    inventory => inventory.HasFlipper && inventory.HasShovel,
                    ItemType.GoldCoin),
                new Location(new LocationId("jungles-22x22.tmx", "Chest_Small", new Vector3(1440f, 0f, 2016f)),
                    "Lighthouse Cape Chest",
                    _ => true,
                    ItemType.GoldCoin),
                new Location(new LocationId("jungles-22x22-lighthouse.tmx", "Chest_Small", new Vector3(556f, 0f, 144f)),
                    "Lighthouse Chest",
                    _ => true,
                    ItemType.GoldCoin),
            };
        }
    }
}
