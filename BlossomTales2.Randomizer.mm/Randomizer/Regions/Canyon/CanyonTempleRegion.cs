using System;
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

        private Predicate<Inventory> CanAccessTemple2 =>  inventory => CanAccess(inventory) && inventory.HasKeys;
        private Predicate<Inventory> CanAccessTemple3 => inventory => CanAccessTemple2(inventory) && inventory.CanSwitchLevers && inventory.HasKeys && inventory.CanDoDamage;
        private Predicate<Inventory> CanAccessTemple4 => inventory => CanAccessTemple3(inventory) && inventory.HasGrappleHook;

        public CanyonTempleRegion(World world) : base("CanyonTemple", world)
        {
            Locations = new List<Location>
            {
                //Chest
                new Location(new LocationId("temple-1.tmx", "Chest_Small", new Vector3(1376f, 0f, 1184f)),
                    "Entrance chest",
                    inventory => inventory.HasBow && inventory.CanSwitchLevers || inventory.HasGrappleHook,
                    ItemType.Gold_Key), //accès temple && (arc && leviers || grappin)
                new Location(new LocationId("temple-4.tmx", "Chest_Small", new Vector3(3232f, 0f, 1392f)),
                    "Chest isolated down-right corner",
                    inventory => CanAccessTemple2(inventory) && inventory.CanSwitchLevers,
                    ItemType.GoldCoin), //accès temple 2 && leviers
                new Location(new LocationId("temple-5.tmx", "Chest_Small", new Vector3(624f, 0f, 1728f)),
                    "Chest middle hole",
                    inventory => CanAccessTemple2(inventory) && (inventory.CanSwitchLevers || inventory.HasGrappleHook),
                    ItemType.Gold_Key), //accès temple 2 && (leviers || grappin)
                new Location(new LocationId("temple-5-secret.tmx", "Chest_Small", new Vector3(640f, 0f, 292f)),
                    "Hidden Cristal chest",
                    inventory => CanAccessTemple2(inventory) && inventory.CanSwitchLevers && inventory.HasBombs,
                    ItemType.Crystal), //accès temple 2 && leviers && bombes
                new Location(new LocationId("temple-6.tmx", "Chest_Small", new Vector3(388f, 0f, 756f)),
                    "Chest under rock",
                    inventory => CanAccessTemple2(inventory) && inventory.CanSwitchLevers && inventory.HasBombs,
                    ItemType.GoldCoin), //accès temple 2 && leviers && bombes
                new Location(new LocationId("temple-8.tmx", "Chest_Small", new Vector3(896f, 0f, 1792f)),
                    "Chest left hole island",
                    inventory => CanAccessTemple3(inventory) && inventory.HasGrappleHook,
                    ItemType.GoldCoin), //accès temple 3 && grappin
                new Location(new LocationId("temple-8.tmx", "Chest_Small", new Vector3(1728f, 0f, 1792f)),
                    "Chest right hole island",
                    inventory => CanAccessTemple3(inventory) && inventory.HasGrappleHook,
                    ItemType.GoldCoin), //accès temple 3 && grappin
                new Location(new LocationId("temple-11.tmx", "Chest_Small", new Vector3(440f, 0f, 452f)),
                    "Chest north east room 11 down",
                    inventory => CanAccessTemple3(inventory) && inventory.CanSwitchLevers,
                    ItemType.GoldCoin), //accès temple 3 && leviers
                new Location(new LocationId("temple-11.tmx", "Chest_Small", new Vector3(448f, 0f, 1920f)),
                    "Chest north east room 11 up",
                    inventory => CanAccessTemple3(inventory) && inventory.CanSwitchLevers && inventory.HasBombs,
                    ItemType.Gold_Key), //accès temple 3 && leviers && bombes
                new Location(new LocationId("temple-15-secret.tmx", "Chest_Small",new Vector3(640f, 0f, 292f)),
                    "Hidden Heart Piece chest North",
                    inventory => CanAccessTemple4(inventory) && inventory.HasBombs,
                    ItemType.HeartQ_1), //accès temple 4 && bombes
                new Location(new LocationId("temple-17.tmx", "Chest_Small", new Vector3(276f, 0f, 220f)),
                    "Chest far linear pegs",
                    inventory => CanAccessTemple4(inventory),
                    ItemType.GoldCoin), //accès temple 4
                new Location(new LocationId("temple-18.tmx", "Chest_Small", new Vector3(1152f, 0f, 1724f)),
                    "Chest down entrance Heart Piece chest East",
                    inventory => CanAccessTemple4(inventory),
                    ItemType.GoldCoin), //accès temple 4
                new Location(new LocationId("temple-18-secret.tmx", "Chest_Small", new Vector3(640f, 0f, 284f)),
                    "Hidden Heart Piece chest East",
                    inventory => CanAccessTemple4(inventory) && inventory.HasBombs,
                    ItemType.HeartQ_1), //accès temple 4 && bombes
                new Location(new LocationId("temple-vultureBoss.tmx", "Chest", new Vector3(768f, 0f, 640f)),
                    "Vulture Boss Chest",
                    inventory => CanAccessTemple3(inventory) && (inventory.HasKeys || inventory.HasGrappleHook) && inventory.CanDoDamage,
                    ItemType.GrappleHook), //accès temple 3 && (clé || grappin) && damage
                new Location(new LocationId("temple-genieBoss.tmx", "BossGenie", Vector3.Zero),
                    "Genie Boss Reward",inventory => CanAccessTemple4(inventory) && inventory.HasTorch,
                    ItemType.HeartQ_4), //accès temple 4 && lanterne
                new Location(new LocationId("temple-genieBoss.tmx", "Chest", new Vector3(832f, 0f, 640f)),
                    "Genie Boss Chest",
                    inventory => CanAccessTemple4(inventory) && inventory.HasTorch,
                    ItemType.KeyPiece2), //accès temple 4 && lanterne
            };

        }
    }
}
