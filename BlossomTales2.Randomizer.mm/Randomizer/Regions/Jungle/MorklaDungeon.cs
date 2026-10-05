using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace BlossomTales2.Randomizer.mm
{
    public class MorklaDungeon : Region
    {
        public override bool CanAccess(Inventory inventory) => World.JungleBack.CanAccess(inventory)
                                                               && inventory.HasFishingRod
                                                               && (inventory.HasBombs || inventory.HasFlipper);

        private bool CanAccessMorklaBoss(Inventory inventory) => inventory.CanDoDamage && inventory.HasFlipper && inventory.CanHitWaterLevers && inventory.HasBlueGem &&  inventory.HasGreenGem;

        public MorklaDungeon(World world) : base("Morkla", world)
        {
            Locations = new List<Location>()
            {
                new Location(new LocationId("morkla-3.tmx", "Chest_Small", new Vector3(704f, 0f, 1732f)),
                    "Room 3 Chest",
                    inventory => inventory.CanHitWaterLevers && (inventory.HasTorch && inventory.CanDoDamage || inventory.HasFlipper),
                    ItemType.GoldCoin),
                new Location(new LocationId("morkla-4.tmx", "Chest_Small", new Vector3(1216f, 0f, 384f)),
                    "Room 4 Left Chest",
                    inventory => inventory.CanHitWaterLevers && inventory.HasFlipper && inventory.CanSwitchLevers,
                    ItemType.GoldCoin),
                new Location(new LocationId("morkla-4.tmx", "Chest_Small", new Vector3(448f, 0f, 768f)),
                    "Room 4 Right Chest",
                    inventory => inventory.CanHitWaterLevers && inventory.HasFlipper && inventory.CanSwitchLevers,
                    ItemType.GoldCoin),
                new Location(new LocationId("morkla-8.tmx", "Chest_Small", new Vector3(736f, 0f, 556f)),
                    "Room 8 Chest",
                    inventory => inventory.HasTorch && inventory.CanHitWaterLevers && (inventory.CanDoDamage || inventory.HasFlipper),
                    ItemType.Gold_Key),
                new Location(new LocationId("morkla-17.tmx", "Chest_Small", new Vector3(1732f, 0f, 384f)),
                    "Room 17 Chest",
                    inventory => inventory.CanHitWaterLevers && inventory.HasFlipper && inventory.CanSwitchLevers,
                    ItemType.BlueGem),
                new Location(new LocationId("morkla-18.tmx", "Chest_Small", new Vector3(736f, 0f, 288f)),
                    "Room 18 Top Left Chest",
                    inventory => inventory.HasFlipper && (inventory.CanHitWaterLevers && inventory.CanSwitchLevers || inventory.HasTorch),
                    ItemType.GoldCoin),
                new Location(new LocationId("morkla-18.tmx", "Chest_Small", new Vector3(928f, 0f, 288f)),
                    "Room 18 Top Right Chest",
                    inventory => inventory.HasFlipper && (inventory.CanHitWaterLevers && inventory.CanSwitchLevers || inventory.HasTorch),
                    ItemType.GoldCoin),
                new Location(new LocationId("morkla-18.tmx", "Chest_Small", new Vector3(736f, 0f, 608f)),
                    "Room 18 Bottom Left Chest",
                    inventory => inventory.HasFlipper && (inventory.CanHitWaterLevers && inventory.CanSwitchLevers || inventory.HasTorch),
                    ItemType.GoldCoin),
                new Location(new LocationId("morkla-18.tmx", "Chest_Small", new Vector3(928f, 0f, 608)),
                    "Room 18 Bottom Right Chest",
                    inventory => inventory.HasFlipper && (inventory.CanHitWaterLevers && inventory.CanSwitchLevers || inventory.HasTorch),
                    ItemType.GoldCoin),
                new Location(new LocationId("morkla-19.tmx", "Chest_Small", new Vector3(640f, 0f, 312f)),
                    "Room 19 Chest",
                    inventory => inventory.HasFlipper && inventory.CanHitWaterLevers && inventory.HasBombs,
                    ItemType.HeartQ_1),
                new Location(new LocationId("morkla-20.tmx", "Chest_Small", new Vector3(640f, 0f, 384f)),
                    "Room 20 Chest",
                    inventory => inventory.HasBombs && (inventory.HasTorch || inventory.HasFlipper),
                    ItemType.HeartQ_1),
                new Location(new LocationId("morkla-21.tmx", "Chest_Small", new Vector3(896f, 0f, 776f)),
                    "Room 21 Chest",
                    inventory => inventory.CanHitWaterLevers && inventory.HasFlipper && inventory.CanSwitchLevers,
                    ItemType.GreenGem),
                new Location(new LocationId("morkla-pirateBoss.tmx", "Chest", new Vector3(768f, 0f, 640f)),
                    "Pirate Boss Chest",
                    inventory => inventory.CanDoDamage && (inventory.HasTorch && inventory.CanHitWaterLevers && inventory.HasKeys || inventory.HasFlipper),
                    ItemType.KeyPiece1),
                new Location(new LocationId("morkla-octopus.tmx", "BossOctopus", Vector3.Zero),
                    "Octopus Boss Reward",
                    inventory => CanAccessMorklaBoss(inventory),
                    ItemType.HeartQ_4),
                new Location(new LocationId("morkla-octopus.tmx", "Chest", new Vector3(896f, 0f, 624f)),
                    "Octopus Boss Chest",
                    inventory => CanAccessMorklaBoss(inventory),
                    ItemType.KeyPiece1),
            };
        }
    }
}
