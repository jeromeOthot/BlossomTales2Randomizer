using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace BlossomTales2.Randomizer.mm
{
    public class MansionDungeon : Region
    {
        public override bool CanAccess(Inventory inventory) => World.DarkWoodsBack.CanAccess(inventory) && inventory.HasBoomerang;
        public override int Locks => 4;
        private bool CanAccessMansion2(Inventory inventory) => CanAccess(inventory) && inventory.GoldKeyCount >= RequiredKeyCount(inventory) + 1 && inventory.CanDoDamage;
        private bool CanAccessMansion3(Inventory inventory) => CanAccessMansion2(inventory) && inventory.HasRexTeleporter;
        private bool CanAccessMansionBoss(Inventory inventory) => CanAccessMansion3(inventory) && inventory.GoldKeyCount >= RequiredKeyCount(inventory) + 4  && inventory.CanDoDamage && inventory.HasRexTeleporter;

        public MansionDungeon(World world) : base("Mansion", world)
        {
            Locations = new List<Location>()
            {
                new Location(new LocationId("mansion-4.tmx", "Chest_Small", new Vector3(640f, 0f, 1280f)),
                    "Memory Room Falling Chest",
                    inventory => inventory.CanDoDamage,
                    ItemType.Gold_Key),
                new Location(new LocationId("mansion-12.tmx", "Chest_Small", new Vector3(1920f, 0f, 892f)),
                    "Roller Room Chest",
                    inventory => CanAccessMansion2(inventory) && inventory.HasGrappleHook || CanAccessMansion3(inventory) && inventory.HasRexTeleporter,
                    ItemType.Gold_Key),
                new Location(new LocationId("mansion-12-secret.tmx", "Chest_Small", new Vector3(640f, 0f, 384f)),
                    "Roller Secret Room Chest",
                    inventory => (CanAccessMansion2(inventory) && inventory.HasGrappleHook || CanAccessMansion3(inventory) && inventory.HasRexTeleporter) && inventory.HasBombs,
                    ItemType.HeartQ_1),
                new Location(new LocationId("mansion-15-secret.tmx", "Chest_Small", new Vector3(640f, 0f, 384f)),
                    "Laser Secret Room Top Chest",
                    inventory => CanAccessMansion3(inventory) && inventory.HasBombs,
                    ItemType.HeartQ_1),
                new Location(new LocationId("mansion-15-secret.tmx", "Chest_Small", new Vector3(512f, 0f, 456f)),
                    "Laser Secret Room Bottom Left Chest",
                    inventory => CanAccessMansion3(inventory) && inventory.HasBombs,
                    ItemType.GoldCoin),
                new Location(new LocationId("mansion-15-secret.tmx", "Chest_Small", new Vector3(768f, 0f, 456f)),
                    "Laser Secret Room Bottom Right Chest",
                    inventory => CanAccessMansion3(inventory) && inventory.HasBombs,
                    ItemType.GoldCoin),
                new Location(new LocationId("mansion-16.tmx", "Chest_Small", new Vector3(1832f, 0f, 636f)),
                    "Laser Memory Puzzle Room Falling Chest",
                    inventory => CanAccessMansion3(inventory),
                    ItemType.Gold_Key),
                new Location(new LocationId("mansion-20.tmx", "Chest_Small", new Vector3(504f, 0f, 896f)),
                    "Path Puzzle Room Falling Chest",
                    inventory => CanAccessMansion3(inventory) && inventory.CanDoDamage && inventory.HasRexTeleporter,
                    ItemType.Gold_Key),
                new Location(new LocationId("mansion-bossVampire.tmx", "Chest", new Vector3(704f, 0f, 448f)),
                    "Vampire Boss Chest",
                    inventory => CanAccessMansion2(inventory) && inventory.GoldKeyCount >= RequiredKeyCount(inventory) + 4 && inventory.CanDoDamage,
                    ItemType.RexTeleporter),
                new Location(new LocationId("mansion-bossScientist.tmx", "BossScientist", Vector3.Zero),
                    "Scientist Boss Reward",
                    inventory => CanAccessMansionBoss(inventory),
                    ItemType.HeartQ_4),
                new Location(new LocationId("mansion-bossScientist.tmx", "Chest", new Vector3(704f, 0f, 448f)),
                    "Scientist Boss Chest",
                    inventory => CanAccessMansionBoss(inventory),
                    ItemType.HeartQ_4)
            };
        }

        private int RequiredKeyCount(Inventory inventory)
        {
            int keyCount = 0;
            keyCount += World.OrchidTomb.CanAccess(inventory) ? World.OrchidTomb.Locks : 0;
            keyCount += World.MorklaDungeon.CanAccess(inventory) ? World.MorklaDungeon.Locks : 0;
            keyCount += World.CanyonTemple.CanAccess(inventory) ? World.CanyonTemple.Locks : 0;
            keyCount += World.MinotaurCastle.CanAccess(inventory) ? World.MinotaurCastle.Locks : 0;
            return keyCount;
        }
    }
}
