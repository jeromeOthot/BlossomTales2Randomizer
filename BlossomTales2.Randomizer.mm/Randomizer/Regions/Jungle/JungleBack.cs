using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace BlossomTales2.Randomizer.mm
{
    public class JungleBack : Region
    {
        public override bool CanAccess(Inventory inventory) => World.Anchortown.CanAccess(inventory);

        public JungleBack(World world) : base("Jungle Back", world)
        {
            Locations = new List<Location>()
            {
                //note caves
                new Location(new LocationId("jungles-25x21-noteCave.tmx", "Chest_Small", new Vector3(288f, 0f, 288f)),
                    "Note Cave Left Chest",
                    inventory => (inventory.HasBombs || inventory.HasFlipper) && inventory.CanOpenNoteDoor && inventory.HasBombs && inventory.HasGrappleHook,
                    ItemType.GoldCoin),
                new Location(new LocationId("jungles-25x21-noteCave.tmx", "Chest_Small", new Vector3(416f, 0f, 288f)),
                    "Note Cave Middle Left Chest",
                    inventory => (inventory.HasBombs || inventory.HasFlipper) && inventory.CanOpenNoteDoor && inventory.HasBombs && inventory.HasGrappleHook,
                    ItemType.GoldCoin),
                new Location(new LocationId("jungles-25x21-noteCave.tmx", "Chest_Small", new Vector3(1056f, 0f, 288f)),
                    "Note Cave Middle Right Chest",
                    inventory => (inventory.HasBombs || inventory.HasFlipper) && inventory.CanOpenNoteDoor && inventory.HasBombs && inventory.HasGrappleHook,
                    ItemType.GoldCoin),
                new Location(new LocationId("jungles-25x21-noteCave.tmx", "Chest_Small", new Vector3(1184f, 0f, 288f)),
                    "Note Cave Right Chest",
                    inventory => (inventory.HasBombs || inventory.HasFlipper) && inventory.CanOpenNoteDoor && inventory.HasBombs && inventory.HasGrappleHook,
                    ItemType.GoldCoin),
                //Caves
                new Location(new LocationId("jungles-25x22-cave.tmx", "Chest_Small", new Vector3(288f, 0f, 488f)),
                    "Pirate Cave Left Chest",
                    inventory => inventory.HasBombs,
                    ItemType.GoldCoin),
                new Location(new LocationId("jungles-25x22-cave.tmx", "Chest_Small", new Vector3(288f, 0f, 488f)),
                    "Pirate Cave Right Chest",
                    inventory => inventory.HasBombs,
                    ItemType.GoldCoin),
                //Short sidequests
                new Location(new LocationId("jungles-22x21.tmx", "ghostJungle", Vector3.Zero),
                    "Jungle Ghost",
                    inventory => inventory.CanCraftPotion(ItemType.Jar_Ghost) && inventory.HasHeartNecklace,
                    ItemType.HeartQ_1),
                new Location(new LocationId("jungles-firstPrimate.tmx", "Chest", new Vector3(480f, 0f, 256f)),
                    "First Primate Reward Chest",
                    _ => true,
                    ItemType.Bombs),
                //Shops
                new Location(new LocationId("pirateShip-shop.tmx", "left", Vector3.Zero),
                    "Pirate Ship Shop Left Item",
                    inventory => inventory.HasFlipper,
                    ItemType.Jar_DoubleDamage),
                new Location(new LocationId("pirateShip-shop.tmx", "center", Vector3.Zero),
                    "Pirate Ship Shop Middle Item",
                    inventory => inventory.HasFlipper,
                    ItemType.Crystal),
                new Location(new LocationId("pirateShip-shop.tmx", "right", Vector3.Zero),
                    "Pirate Ship Shop Right Item",
                    inventory => inventory.HasFlipper,
                    ItemType.HeartQ_1),
                //Bardes
                new Location(new LocationId("jungles-23x22.tmx", "bard_song", Vector3.Zero),
                    "Horse Bard",
                    inventory => inventory.HasGrappleHook,
                    ItemType.CallHorse),
                //Traders
                new Location(new LocationId("sandCastle.tmx", "Chest", new Vector3(384f, 0f, 256f)),
                    "Sand Castle Trader Chest",
                    _ => true,
                    ItemType.HeartQ_1),
                //Chests
                new Location(new LocationId("jungles-22x20.tmx", "Chest_Small", new Vector3(1764f, 0f, 1896f)),
                    "West Honeycomb Chest",
                    inventory => inventory.HasFlipper || inventory.HasBombs || inventory.HasBoomerang,
                    ItemType.Honeycomb),
                new Location(new LocationId("jungles-22x22.tmx", "Chest_Small", new Vector3(1668f, 0f, 512f)),
                    "Southwest Chest",
                    _ => true,
                    ItemType.GoldCoin),
                new Location(new LocationId("jungles-22x22.tmx", "necklaceFish", Vector3.Zero),
                    "Necklace Fishing Spot",
                    inventory => inventory.CanCraftPotion(ItemType.Jar_Ghost) && inventory.HasFishingRod,
                    ItemType.HeartNecklace),
                new Location(new LocationId("jungles-23x22.tmx", "Chest_Small", new Vector3(740f, 0f, 1252f)),
                    "Bridge Chest",
                    _ => true,
                    ItemType.GoldCoin),
                new Location(new LocationId("jungles-24x20.tmx", "Chest_Small", new Vector3(2088f, 0f, 2200f)),
                    "Morkla Bomb Chest",
                    inventory => inventory.HasBombs,
                    ItemType.GoldCoin),
                new Location(new LocationId("jungles-24x22.tmx", "Chest_Small", new Vector3(2336f, 0f, 496f)),
                    "Ledge Chest",
                    _ => true,
                    ItemType.GoldCoin),
                new Location(new LocationId("jungles-25x21.tmx", "Chest_Small", new Vector3(232f, 0f, 272f)),
                    "Morkla Trees Chest",
                    inventory => inventory.HasBombs,
                    ItemType.GoldCoin),
                new Location(new LocationId("jungles-25x22.tmx", "Chest_Small", new Vector3(160f, 0f, 836f)),
                    "Tree Trunk Chest",
                    _ => true,
                    ItemType.GoldCoin),
                new Location(new LocationId("jungles-25x22.tmx", "Chest_Small", new Vector3(1760f, 0f, 728f)),
                    "Pirate Camp Chest",
                    inventory => inventory.HasBombs,
                    ItemType.GoldCoin),

            };
        }
    }
}
