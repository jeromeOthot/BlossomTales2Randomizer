using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace BlossomTales2.Randomizer.mm.Canyon
{
    //accès temple 2: accès temple && clé
    //accès temple 3: accès temple 2 && leviers && clé && damage
    //accès temple 4: accès temple 3 && grappin

    public class CanyonTempleRegion : Region
    {
        public override bool CanAccess(Inventory inventory) => World.CanyonNorth.CanAccess(inventory) && inventory.CanOpenNoteDoor;
        public override int Locks => 3;
        private bool CanAccessTemple2(Inventory inventory) => CanAccess(inventory) && inventory.GoldKeyCount >= RequiredKeyCount(inventory) + 1;
        private bool CanAccessTemple3(Inventory inventory) => CanAccessTemple2(inventory) && inventory.CanSwitchLevers && inventory.GoldKeyCount >= RequiredKeyCount(inventory) + 2 && inventory.CanDoDamage;
        private bool CanAccessTemple4(Inventory inventory) => CanAccessTemple3(inventory) && inventory.HasGrappleHook;

        public CanyonTempleRegion(World world) : base("Canyon Temple", world)
        {
            Locations = new List<Location>
            {
                //Chest
                new Location(new LocationId("temple-1.tmx", "Chest_Small", new Vector3(1376f, 0f, 1184f)),
                    "Entrance Chest",
                    inventory => inventory.HasBow && inventory.CanSwitchLevers || inventory.HasGrappleHook,
                    ItemType.Gold_Key),
                new Location(new LocationId("temple-4.tmx", "Chest_Small", new Vector3(3232f, 0f, 1392f)),
                    "Collapsing Paths Chest",
                    inventory => CanAccessTemple2(inventory) && inventory.CanSwitchLevers,
                    ItemType.GoldCoin),
                new Location(new LocationId("temple-5.tmx", "Chest_Small", new Vector3(624f, 0f, 1728f)),
                    "Cart Platform Chest",
                    inventory => CanAccessTemple2(inventory) && (inventory.CanSwitchLevers || inventory.HasGrappleHook),
                    ItemType.Gold_Key),
                new Location(new LocationId("temple-5-secret.tmx", "Chest_Small", new Vector3(640f, 0f, 292f)),
                    "Secret Room Crystal Chest",
                    inventory => CanAccessTemple2(inventory) && inventory.CanSwitchLevers && inventory.HasBombs,
                    ItemType.Crystal),
                new Location(new LocationId("temple-6.tmx", "Chest_Small", new Vector3(388f, 0f, 756f)),
                    "Bomb Rocks Chest",
                    inventory => CanAccessTemple2(inventory) && inventory.CanSwitchLevers && inventory.HasBombs,
                    ItemType.GoldCoin),
                new Location(new LocationId("temple-8.tmx", "Chest_Small", new Vector3(896f, 0f, 1792f)),
                    "Left Peg Platform Chest",
                    inventory => CanAccessTemple3(inventory) && inventory.HasGrappleHook,
                    ItemType.GoldCoin),
                new Location(new LocationId("temple-8.tmx", "Chest_Small", new Vector3(1728f, 0f, 1792f)),
                    "Right Peg Platform Chest",
                    inventory => CanAccessTemple3(inventory) && inventory.HasGrappleHook,
                    ItemType.GoldCoin),
                new Location(new LocationId("temple-11.tmx", "Chest_Small", new Vector3(440f, 0f, 452f)),
                    "Room 11 Chest",
                    inventory => CanAccessTemple3(inventory) && inventory.CanSwitchLevers,
                    ItemType.GoldCoin),
                new Location(new LocationId("temple-11.tmx", "Chest_Small", new Vector3(448f, 0f, 1920f)),
                    "Room 11 Bomb Chest",
                    inventory => CanAccessTemple3(inventory) && inventory.CanSwitchLevers && inventory.HasBombs,
                    ItemType.Gold_Key),
                new Location(new LocationId("temple-15-secret.tmx", "Chest_Small",new Vector3(640f, 0f, 292f)),
                    "Grapplehook Paths Secret Room Chest",
                    inventory => CanAccessTemple4(inventory) && inventory.HasBombs,
                    ItemType.HeartQ_1),
                new Location(new LocationId("temple-17.tmx", "Chest_Small", new Vector3(276f, 0f, 220f)),
                    "Room 17",
                    inventory => CanAccessTemple4(inventory),
                    ItemType.GoldCoin),
                new Location(new LocationId("temple-18.tmx", "Chest_Small", new Vector3(1152f, 0f, 1724f)),
                    "Room 18 Chest",
                    inventory => CanAccessTemple4(inventory),
                    ItemType.GoldCoin),
                new Location(new LocationId("temple-18-secret.tmx", "Chest_Small", new Vector3(640f, 0f, 284f)),
                    "Secret Room 18 Chest",
                    inventory => CanAccessTemple4(inventory) && inventory.HasBombs,
                    ItemType.HeartQ_1),
                new Location(new LocationId("temple-vultureBoss.tmx", "Chest", new Vector3(768f, 0f, 640f)),
                    "Vulture Boss Chest",
                    inventory => CanAccessTemple3(inventory) && (inventory.GoldKeyCount >= RequiredKeyCount(inventory) + 3 || inventory.HasGrappleHook) && inventory.CanDoDamage,
                    ItemType.GrappleHook),
                new Location(new LocationId("temple-genieBoss.tmx", "BossGenie", Vector3.Zero),
                    "Genie Boss Reward",inventory => CanAccessTemple4(inventory) && inventory.HasTorch,
                    ItemType.HeartQ_4),
                new Location(new LocationId("temple-genieBoss.tmx", "Chest", new Vector3(832f, 0f, 640f)),
                    "Genie Boss Chest",
                    inventory => CanAccessTemple4(inventory) && inventory.HasTorch,
                    ItemType.KeyPiece2,
                    LocationPool.KeyPieces),
            };
        }

        private int RequiredKeyCount(Inventory inventory)
        {
            int keyCount = 0;
            keyCount += World.OrchidTomb.CanAccess(inventory) ? World.OrchidTomb.Locks : 0;
            keyCount += World.MorklaDungeon.CanAccess(inventory) ? World.MorklaDungeon.Locks : 0;
            keyCount += World.MansionDungeon.CanAccess(inventory) ? World.MansionDungeon.Locks : 0;
            keyCount += World.MinotaurCastle.CanAccess(inventory) ? World.MinotaurCastle.Locks : 0;
            return keyCount;
        }
    }
}
