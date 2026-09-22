// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace BlossomTales2.Randomizer.mm.Canyon
{
    public class CanyonVillageRegion : Region
    {
        public override Predicate<Inventory> CanAccess => inventory => inventory.CanAccessCanyon;

        public CanyonVillageRegion(World world) : base("CanyonVillage", world)
        {
            Locations = new List<Location>
            {
                //NPC
                {  new Location(new LocationId("overworld-16x22.tmx", "beggar", new Vector3(980f, 0f, 1616f)), "Canyon Beggar",(inventory) => true, ItemType.HeartQ_1) }, //accès canyon

                //Canyon village
                { new Location( new LocationId("canyon-shop.tmx", "canyon-shop left", Vector3.Zero), "canyon-shop  left",(inventory) => true, ItemType.Jar_Empty) },
                { new Location(new LocationId("canyon-shop.tmx", "canyon-shop center", Vector3.Zero), "canyon-shop center ",(inventory) => true, ItemType.Crystal) },
                { new Location(new LocationId("canyon-shop.tmx", "canyon-shop right", Vector3.Zero), "canyon-shop right",(inventory) => true, ItemType.HeartQ_1) },

                //House chest
                 { new Location(new LocationId("canyon-house1.tmx", "Chest_Small", new Vector3(576f, 0f, 208f)), "canyon village house 1 chest #1",(inventory) => true, ItemType.GoldCoin) }, //accès canyon
                 { new Location(new LocationId("canyon-house3.tmx", "Chest_Small", new Vector3(772f, 0f, 580f)), "canyon village house 3 chest #1",(inventory) => true, ItemType.GoldCoin) }, //accès canyon
                 { new Location(new LocationId("canyon-house3.tmx", "Chest_Small", new Vector3(344f, 0f, 496f)), "canyon village house 3 chest #2",(inventory) => true, ItemType.GoldCoin) }, //accès canyon
            };

        }
    }
}
