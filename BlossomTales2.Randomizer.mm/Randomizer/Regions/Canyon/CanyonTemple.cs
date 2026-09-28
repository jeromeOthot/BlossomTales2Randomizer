// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace BlossomTales2.Randomizer.mm.Canyon
{
    public class CanyonVillageRegion : Region
    {
        public override Predicate<Inventory> CanAccess => inventory => (World.CanyonNorthWest.CanAccess(inventory) && inventory.CanOpenNoteDoor);

        private Predicate<Inventory> CanAccessTemple2 => inventory => (this.CanAccess() && ((inventory.HasBow && inventory.CanActivateBlueSwitch) || inventory.HasGrappleHook));
        private Predicate<Inventory> CanAccessTemple3 => inventory => CanAccessTemple2 && inventory.CanActivateBlueSwitch;
        private Predicate<Inventory> CanAccessTemple4 => inventory => (CanAccessTemple3 && inventory.HasGrappleHook);

        public CanyonVillageRegion(World world) : base("CanyonTemple", world)
        {
            Locations = new List<Location>
            {
                //NPC
                { new Location(new LocationId("temple-1.tmx", "Chest_Small", new Vector3(1376f, 0f, 1184f)), "Entrance chest",(inventory) => CanAccessTemple2, ItemType.Gold_Key) }, //accès temple && (arc && leviers || grappin)
                { new Location(new LocationId("temple-4.tmx", "Chest_Small", new Vector3(3232f, 0f, 1392f)), "Beggar",(inventory) => CanAccessTemple3, ItemType.GoldCoin) }, //accès temple 2 && leviers
                { new Location(new LocationId("temple-5.tmx", "Chest_Small", new Vector3(624f, 0f, 1728f)),  "Beggar",(inventory) => CanAccessTemple2, ItemType.Gold_Key) }, //accès temple 2 && (leviers || grappin)
                { new Location(new LocationId("temple-5-secret.tmx", "Chest_Small", new Vector3(640f, 0f, 292f)), "Hidden Cristal chest",(inventory) => CanAccessTemple3 && inventory.HasBomb, ItemType.Crystal) }, //accès temple 2 && leviers && bombes
                { new Location(new LocationId("temple-6.tmx", "Chest_Small", new Vector3(388f, 0f, 756f)), "Beggar",(inventory) => CanAccessTemple3 && inventory.HasBomb, ItemType.GoldCoin) }, //accès temple 2 && leviers && bombes
                { new Location(new LocationId("temple-8.tmx", "Chest_Small", new Vector3(896f, 0f, 1792f)), "Beggar",(inventory) => CanAccessTemple4, ItemType.GoldCoin) }, //accès temple 3 && grappin
                { new Location(new LocationId("temple-8.tmx", "Chest_Small", new Vector3(1728f, 0f, 1792f)), "Beggar",(inventory) => CanAccessTemple4, ItemType.GoldCoin) }, //accès temple 3 && grappin
                { new Location(new LocationId("temple-11.tmx", "Chest_Small", new Vector3(440f, 0f, 452f)), "Beggar",(inventory) => CanAccessTemple3, ItemType.GoldCoin) }, //accès temple 3 && leviers
                { new Location(new LocationId("temple-11.tmx", "Chest_Small", new Vector3(448f, 0f, 1920f)), "Beggar",(inventory) => CanAccessTemple3 && inventory.HasBomb, ItemType.Gold_Key) }, //accès temple 3 && leviers && bombes
                { new Location(new LocationId("temple-15-secret.tmx", "Chest_Small",new Vector3(640f, 0f, 292f)), "Hidden Heart Piece chest",(inventory) => CanAccessTemple4 && inventory.HasBomb, ItemType.HeartQ_1) }, //accès temple 4 && bombes
                { new Location(new LocationId("temple-17.tmx", "Chest_Small", new Vector3(276f, 0f, 220f)), "Beggar",(inventory) => CanAccessTemple4, ItemType.GoldCoin) }, //accès temple 4
                { new Location(new LocationId("temple-18.tmx", "Chest_Small", new Vector3(1152f, 0f, 1724f)), "Beggar",(inventory) => CanAccessTemple4, ItemType.GoldCoin) }, //accès temple 4
                { new Location(new LocationId("temple-18-secret.tmx", "Chest_Small", new Vector3(640f, 0f, 284f)), "Hidden Heart Piece chest",(inventory) => CanAccessTemple4 && inventory.HasBomb, ItemType.HeartQ_1) }, //accès temple 4 && bombes
                { new Location(new LocationId("temple-genieBoss.tmx", "BossGenie", new Vector3(0f, 0f, 0f)), "Boss heart",(inventory) => CanAccessTemple4 && inventory.HasLantern, ItemType.HeartQ_4) }, //accès temple 4 && lanterne
                { new Location(new LocationId("temple-genieBoss.tmx", "Chest", new Vector3(832f, 0f, 640f)), "Boss key",(inventory) => CanAccessTemple4 && inventory.HasLantern, ItemType.KeyPiece2) }, //accès temple 4 && lanterne
                { new Location(new LocationId("temple-vultureBoss.tmx", "Chest", new Vector3(768f, 0f, 640f)), "Mini-boss",(inventory) => CanAccessTemple3 && (inventory.HasKeys || inventory.CanDoDamage) && inventory.CanDoDamage, ItemType.GrappleHook) }, //accès temple 3 && (clé || grappin) && damage
            };

        }
    }
}
