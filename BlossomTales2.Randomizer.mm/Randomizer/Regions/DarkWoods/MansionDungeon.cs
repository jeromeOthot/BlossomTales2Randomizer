using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace BlossomTales2.Randomizer.mm
{
    public class MansionDungeon : Region
    {
        public override bool CanAccess(Inventory inventory) => World.DarkWoodsBack.CanAccess(inventory) && inventory.HasBoomerang;

        private Predicate<Inventory> CanAccessMansion2 => inventory => CanAccess(inventory) && inventory.HasKeys && inventory.CanDoDamage;
        private Predicate<Inventory> CanAccessMansion3 => inventory => CanAccessMansion2(inventory) && inventory.HasRexTeleporter;
        private Predicate<Inventory> CanAccessMansionBoss => inventory => CanAccessMansion3(inventory) && inventory.HasKeys /* x2 */  && inventory.CanDoDamage && inventory.HasRexTeleporter;

        public MansionDungeon(World world) : base("Mansion", world)
        {
            Locations = new List<Location>()
            {
                new Location(new LocationId("mansion-4.tmx", "Chest_Small", new Vector3(640f, 0f, 1280f)),
                    "Room 4 Chest",
                    inventory => inventory.CanDoDamage,
                    ItemType.Gold_Key),
                new Location(new LocationId("mansion-12.tmx", "Chest_Small", new Vector3(1920f, 0f, 892f)),
                    "Room 12 Chest",
                    inventory => CanAccessMansion2(inventory) && inventory.HasGrappleHook,
                    ItemType.Gold_Key),
                new Location(new LocationId("mansion-12-secret.tmx", "Chest_Small", new Vector3(640f, 0f, 384f)),
                    "Room 12 Secret Room Chest",
                    inventory => CanAccessMansion2(inventory) && inventory.HasGrappleHook && inventory.HasBombs,
                    ItemType.HeartQ_1),
                new Location(new LocationId("mansion-15-secret.tmx", "Chest_Small", new Vector3(640f, 0f, 384f)),
                    "Room 15 Secret Room Top Chest",
                    inventory => CanAccessMansion3(inventory) && inventory.HasBombs,
                    ItemType.HeartQ_1),
                new Location(new LocationId("mansion-15-secret.tmx", "Chest_Small", new Vector3(512f, 0f, 456f)),
                    "Room 15 Secret Room Bottom Left Chest",
                    inventory => CanAccessMansion3(inventory) && inventory.HasBombs,
                    ItemType.GoldCoin),
                new Location(new LocationId("mansion-15-secret.tmx", "Chest_Small", new Vector3(768f, 0f, 456f)),
                    "Room 15 Secret Room Bottom Right Chest",
                    inventory => CanAccessMansion3(inventory) && inventory.HasBombs,
                    ItemType.GoldCoin),
                new Location(new LocationId("mansion-16.tmx", "Chest_Small", new Vector3(1832f, 0f, 636f)),
                    "Room 16 Chest",
                    inventory => CanAccessMansion3(inventory),
                    ItemType.Gold_Key),
                new Location(new LocationId("mansion-20.tmx", "Chest_Small", new Vector3(504f, 0f, 896f)),
                    "Room 20 Chest",
                    inventory => CanAccessMansion3(inventory) && inventory.CanDoDamage && inventory.HasRexTeleporter,
                    ItemType.Gold_Key),
                new Location(new LocationId("mansion-bossVampire.tmx", "Chest", new Vector3(704f, 0f, 448f)),
                    "Vampire Boss Chest",
                    inventory => CanAccessMansion2(inventory) && inventory.HasKeys && inventory.CanDoDamage,
                    ItemType.RexTeleporter),
                new Location(new LocationId("mansion-bossVampire.tmx", "Chest", new Vector3(704f, 0f, 448f)),
                    "Vampire Boss Chest",
                    inventory => CanAccessMansion2(inventory) && inventory.HasKeys && inventory.CanDoDamage,
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
    }
}
