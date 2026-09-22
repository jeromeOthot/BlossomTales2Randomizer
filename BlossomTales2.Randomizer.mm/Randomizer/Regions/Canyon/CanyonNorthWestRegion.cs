// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace BlossomTales2.Randomizer.mm.Canyon
{
    public class CanyonNorthWestRegion: Region
    {
        public override Predicate<Inventory> CanAccess => inventory => inventory.CanAccessCanyon;

        public CanyonNorthWestRegion() : base("Canyon North West Region")
        {
            Locations = new List<Location>
            {
                //Os
                { new Location(new LocationId("overworld-15x19.tmx", "PickUpItem", new Vector3(1804f, 0f, 2292f)), "Canyon Bone North Archaeological site",_ => true, ItemType.CanyonBone) }, //accès canyon
                { new Location(new LocationId("overworld-15x19.tmx", "PickUpItem", new Vector3(820f, 0f, 952f)), "Canyon Bone North West Archaeological site",_ => true, ItemType.CanyonBone) }, //accès canyon
                { new Location(new LocationId("overworld-15x20.tmx", "PickUpItem", new Vector3(192f, 0f, 416f)), "Canyon Bone North East Archaeological site",_ => true, ItemType.CanyonBone) }, //accès canyon
                { new Location(new LocationId("overworld-16x19.tmx", "PickUpItem", new Vector3(992f, 0f, 1636f)), "Canyon Bone South Sword Master Dojo",_ => true, ItemType.CanyonBone) }, //accès canyon

                //NPC
                { new Location(new LocationId("overworld-15x20.tmx", "archCanyon", new Vector3(0f, 0f, 0f)), "Canyon archaeologist", (inventory) => inventory.CanAccessCanyon && inventory.CanAccessCanyonSteppe, ItemType.Crystal) },  //accès canyon && CanyonBone == 20

                //ChestCanAccessCanyonSteppe
                { new Location(new LocationId("overworld-15x19-cave.tmx", "Chest_Small", new Vector3(544f, 0f, 368f)), "Canyon 6 chest cave chest #1",(inventory) => inventory.CanAccessCanyon && inventory.HasBombs, ItemType.GoldCoin) }, //accès canyon && bombes
                { new Location(new LocationId("overworld-15x19-cave.tmx", "Chest_Small", new Vector3(480f, 0f, 492f)), "Canyon 6 chest cave chest #2",(inventory) => inventory.CanAccessCanyon && inventory.HasBombs, ItemType.GoldCoin) }, //accès canyon && bombes
                { new Location(new LocationId("overworld-15x19-cave.tmx", "Chest_Small", new Vector3(576f, 0f, 616f)), "Canyon 6 chest cave chest #3",(inventory) => inventory.CanAccessCanyon && inventory.HasBombs, ItemType.GoldCoin) }, //accès canyon && bombes
                { new Location(new LocationId("overworld-15x19-cave.tmx", "Chest_Small", new Vector3(768f, 0f, 616f)), "Canyon 6 chest cave chest #4",(inventory) => inventory.CanAccessCanyon && inventory.HasBombs, ItemType.GoldCoin) }, //accès canyon && bombes
                { new Location(new LocationId("overworld-15x19-cave.tmx", "Chest_Small", new Vector3(864f, 0f, 488f)), "Canyon 6 chest cave chest #5",(inventory) => inventory.CanAccessCanyon && inventory.HasBombs, ItemType.GoldCoin) }, //accès canyon && bombes
                { new Location(new LocationId("overworld-15x19-cave.tmx", "Chest_Small", new Vector3(800f, 0f, 364f)), "Canyon 6 chest cave chest #6",(inventory) => inventory.CanAccessCanyon && inventory.HasBombs, ItemType.GoldCoin) }, //accès canyon && bombes
            };
        }
    }
}
