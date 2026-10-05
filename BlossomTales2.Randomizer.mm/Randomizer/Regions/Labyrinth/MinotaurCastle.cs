using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace BlossomTales2.Randomizer.mm
{
    public class MinotaurCastle : Region
    {
        public override Predicate<Inventory> CanAccess => inventory => World.LabyrinthBack.CanAccess(inventory);

        private Predicate<Inventory> CanAccessCastle2 => inventory => CanAccess(inventory) && inventory.HasRexTeleporter && inventory.HasGrappleHook && inventory.CanSwitchLevers && inventory.HasBow;
        private Predicate<Inventory> CanAccessCastle3 => inventory => CanAccess(inventory) && inventory.HasKeys;
        private Predicate<Inventory> CanAccessCastle4 => inventory => CanAccessCastle3(inventory) && inventory.HasKeys;
        private Predicate<Inventory> CanAccessCastle5 => inventory => CanAccessCastle4(inventory)
                                                                      && inventory.HasGrappleHook && inventory.HasRexTeleporter && inventory.HasTorch && inventory.CanSwitchLevers
                                                                      && (inventory.HasMirrorShield || inventory.CanDoDamage);

        public Predicate<Inventory> CanAccessMinotaurBoss => inventory => CanAccessCastle5(inventory) && inventory.HasMirrorShield
            && (inventory.HasBow || inventory.HasBoomerang || inventory.HasGrappleHook || inventory.HasBombs || (inventory.HasRexTeleporter && inventory.HasSword) || inventory.HasSwordBeams);

        public MinotaurCastle(World world) : base("Minotaur King's Castle", world)
        {
            Locations = new List<Location>()
            {
                new Location(new LocationId("castle-4.tmx", "Chest_Small", new Vector3(832f, 0f, 1204f)),
                    "Room 4 Chest",
                    inventory => CanAccessCastle2(inventory),
                    ItemType.Gold_Key),
                new Location(new LocationId("castle-6.tmx", "Chest_Small", new Vector3(420f, 0f, 1260f)),
                    "Room 6 Chest",
                    inventory => CanAccessCastle3(inventory),
                    ItemType.GoldCoin),
                new Location(new LocationId("castle-7.tmx", "Chest_Small", new Vector3(544f, 0f, 472f)),
                    "Room 7 Chest",
                    inventory => CanAccessCastle3(inventory) && inventory.HasMirrorShield,
                    ItemType.Gold_Key),
                new Location(new LocationId("castle-9.tmx", "Chest_Small", new Vector3(2436f, 0f, 856f)),
                    "Room 9 Chest",
                    inventory => CanAccessCastle4(inventory) && (inventory.HasGrappleHook || inventory.HasRexTeleporter),
                    ItemType.GoldCoin),
                new Location(new LocationId("castle-12.tmx", "Chest_Small", new Vector3(1728f, 0f, 1120f)),
                    "Room 12 Chest",
                    inventory => CanAccessCastle5(inventory),
                    ItemType.GoldCoin),
            };
        }
    }
}
