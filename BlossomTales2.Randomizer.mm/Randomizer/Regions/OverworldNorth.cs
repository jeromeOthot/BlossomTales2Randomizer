using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace BlossomTales2.Randomizer.mm
{
    public class OverworldNorth : Region
    {
        public override Predicate<Inventory> CanAccess => inventory => World.OverworldWest.CanAccess(inventory) && inventory.HasGrappleHook ||
                                                                       World.OverworldEast.CanAccess(inventory) && (inventory.HasBoomerang || inventory.HasGrappleHook);

        public OverworldNorth(World world) : base("Northern Overworld", world)
        {
            Locations = new List<Location>
            {
                //Note caves
                new Location(new LocationId("overworld-19x16-noteCave.tmx", "Chest_Small", new Vector3(384f, 0f, 208f)),
                "Note Cave Top Left Chest",
                inventory => inventory.CanOpenNoteDoor && inventory.CanSwitchLevers && (inventory.HasBombs || inventory.CanCraftPotion(ItemType.Jar_Speedster)),
                    ItemType.GoldCoin),
                new Location(new LocationId("overworld-19x16-noteCave.tmx", "Chest_Small", new Vector3(512f, 0f, 208f)),
                    "Note Cave Top Middle Chest",
                    inventory => inventory.CanOpenNoteDoor && inventory.CanSwitchLevers && (inventory.HasBombs || inventory.CanCraftPotion(ItemType.Jar_Speedster)),
                    ItemType.GoldCoin),
                new Location(new LocationId("overworld-19x16-noteCave.tmx", "Chest_Small", new Vector3(640f, 0f, 208f)),
                    "Note Cave Top Right Chest",
                    inventory => inventory.CanOpenNoteDoor && inventory.CanSwitchLevers && (inventory.HasBombs || inventory.CanCraftPotion(ItemType.Jar_Speedster)),
                    ItemType.GoldCoin),
                new Location(new LocationId("overworld-19x16-noteCave.tmx", "Chest_Small", new Vector3(384f, 0f, 336f)),
                    "Note Cave Lower Left Chest",
                    inventory => inventory.CanOpenNoteDoor && inventory.CanSwitchLevers && (inventory.HasBombs || inventory.CanCraftPotion(ItemType.Jar_Speedster)),
                    ItemType.GoldCoin),
                new Location(new LocationId("overworld-19x16-noteCave.tmx", "Chest_Small", new Vector3(512f, 0f, 336f)),
                    "Note Cave Lower Middle Chest",
                    inventory => inventory.CanOpenNoteDoor && inventory.CanSwitchLevers && (inventory.HasBombs || inventory.CanCraftPotion(ItemType.Jar_Speedster)),
                    ItemType.GoldCoin),
                new Location(new LocationId("overworld-19x16-noteCave.tmx", "Chest_Small", new Vector3(640f, 0f, 336f)),
                    "Note Cave Lower Right Chest",
                    inventory => inventory.CanOpenNoteDoor && inventory.CanSwitchLevers && (inventory.HasBombs || inventory.CanCraftPotion(ItemType.Jar_Speedster)),
                    ItemType.GoldCoin),
                //Caves
                new Location(new LocationId("overworld-20x17-cave.tmx", "Chest_Small", new Vector3(608f, 0f, 232f)),
                    "Fishing Hole Cave Chest",
                    _ => true,
                    ItemType.HeartQ_1),
                new Location(new LocationId("overworld-21x17-cave.tmx", "Chest_Small", new Vector3(384f, 0f, 320f)),
                    "Wizard Bomb Cave Top Left Chest",
                    inventory => inventory.HasBombs,
                    ItemType.GoldCoin),
                new Location(new LocationId("overworld-21x17-cave.tmx", "Chest_Small", new Vector3(576f, 0f, 320f)),
                    "Wizard Bomb Cave Top Right Chest",
                    inventory => inventory.HasBombs,
                    ItemType.GoldCoin),
                new Location(new LocationId("overworld-21x17-cave.tmx", "Chest_Small", new Vector3(288f, 0f, 512f)),
                    "Wizard Bomb Cave Lower Left Chest",
                    inventory => inventory.HasBombs,
                    ItemType.GoldCoin),
                new Location(new LocationId("overworld-21x17-cave.tmx", "Chest_Small", new Vector3(480f, 0f, 512f)),
                    "Wizard Bomb Cave Lower Middle Chest",
                    inventory => inventory.HasBombs,
                    ItemType.GoldCoin),
                new Location(new LocationId("overworld-21x17-cave.tmx", "Chest_Small", new Vector3(672f, 0f, 512f)),
                    "Wizard Bomb Cave Lower Right Chest",
                    inventory => inventory.HasBombs,
                    ItemType.GoldCoin),
            };
        }
    }
}
