using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace BlossomTales2.Randomizer.mm
{
    public class MinotaurCastle : Region
    {
        public override bool CanAccess(Inventory inventory) => World.LabyrinthBack.CanAccessLabyrinth15x16(inventory);
        public override int Locks => 2;
        private bool CanAccessCastle2(Inventory inventory) => CanAccess(inventory) && inventory.HasRexTeleporter && inventory.HasGrappleHook && inventory.CanSwitchLevers && inventory.HasBow;
        private bool CanAccessCastle3(Inventory inventory) => CanAccess(inventory) && inventory.GoldKeyCount >= RequiredKeyCount(inventory) + 1;
        private bool CanAccessCastle4(Inventory inventory) => CanAccessCastle3(inventory) && inventory.GoldKeyCount >= RequiredKeyCount(inventory) + 2;
        private bool CanAccessCastle5(Inventory inventory) => CanAccessCastle4(inventory)
                                                                      && inventory.HasGrappleHook && inventory.HasRexTeleporter && inventory.HasTorch && inventory.CanSwitchLevers
                                                                      && (inventory.HasMirrorShield || inventory.CanDoDamage);

        public bool CanAccessMinotaurBoss(Inventory inventory) => CanAccessCastle5(inventory) && inventory.HasMirrorShield
            && (inventory.HasBow || inventory.HasBoomerang || inventory.HasGrappleHook || inventory.HasBombs || (inventory.HasRexTeleporter && inventory.HasSword) || inventory.HasSwordBeams);

        public MinotaurCastle(World world) : base("Minotaur King's Castle", world)
        {
            Locations = new List<Location>()
            {
                new Location(new LocationId("castle-4.tmx", "Chest_Small", new Vector3(832f, 0f, 1204f)),
                    "Boulder Room Chest",
                    inventory => CanAccessCastle2(inventory),
                    ItemType.Gold_Key),
                new Location(new LocationId("castle-6.tmx", "Chest_Small", new Vector3(420f, 0f, 1260f)),
                    "Lower Left Balcony Chest",
                    inventory => CanAccessCastle3(inventory),
                    ItemType.GoldCoin),
                new Location(new LocationId("castle-7.tmx", "Chest_Small", new Vector3(544f, 0f, 472f)),
                    "Laser Eye Bridge Chest",
                    inventory => CanAccessCastle3(inventory) && inventory.HasMirrorShield,
                    ItemType.Gold_Key),
                new Location(new LocationId("castle-9.tmx", "Chest_Small", new Vector3(2436f, 0f, 856f)),
                    "Moving Platform Room Chest",
                    inventory => CanAccessCastle4(inventory) && (inventory.HasGrappleHook || inventory.HasRexTeleporter),
                    ItemType.GoldCoin),
                new Location(new LocationId("castle-12.tmx", "Chest_Small", new Vector3(1728f, 0f, 1120f)),
                    "Upper Balcony Chest",
                    inventory => CanAccessCastle5(inventory),
                    ItemType.GoldCoin),
            };
        }

        private int RequiredKeyCount(Inventory inventory)
        {
            int keyCount = 0;
            keyCount += World.OrchidTomb.CanAccess(inventory) ? World.OrchidTomb.Locks : 0;
            keyCount += World.MorklaDungeon.CanAccess(inventory) ? World.MorklaDungeon.Locks : 0;
            keyCount += World.CanyonTemple.CanAccess(inventory) ? World.CanyonTemple.Locks : 0;
            keyCount += World.MansionDungeon.CanAccess(inventory) ? World.MansionDungeon.Locks : 0;
            return keyCount;
        }
    }
}
