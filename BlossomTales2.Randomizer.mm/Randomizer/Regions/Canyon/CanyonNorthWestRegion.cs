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

        public CanyonNorthWestRegion(World world) : base("Canyon North West Region", world)
        {
            Locations = new List<Location>
            {
                //Os
                { new Location(new LocationId("overworld-15x19.tmx", "PickUpItem", new Vector3(1804f, 0f, 2292f)), "Bone North Archaeological site",_ => true, ItemType.CanyonBone) }, //accès canyon
                { new Location(new LocationId("overworld-15x19.tmx", "PickUpItem", new Vector3(820f, 0f, 952f)), "Bone North West Archaeological site",_ => true, ItemType.CanyonBone) }, //accès canyon
                { new Location(new LocationId("overworld-15x20.tmx", "PickUpItem", new Vector3(192f, 0f, 416f)), "Bone North East Archaeological site",_ => true, ItemType.CanyonBone) }, //accès canyon
                { new Location(new LocationId("overworld-16x19.tmx", "PickUpItem", new Vector3(992f, 0f, 1636f)), "Bone South Sword Master Dojo",_ => true, ItemType.CanyonBone) }, //accès canyon

                //NPC
                { new Location(new LocationId("overworld-15x20.tmx", "archCanyon", new Vector3(0f, 0f, 0f)), "archaeologist", (inventory) => inventory.CanAccessCanyon && inventory.CanAccessCanyonSteppe, ItemType.Crystal) },  //accès canyon && CanyonBone == 20

                //ChestCanAccessCanyonSteppe
                { new Location(new LocationId("overworld-15x19-cave.tmx", "Chest_Small", new Vector3(544f, 0f, 368f)), "6 chest cave chest north west",(inventory) => inventory.CanAccessCanyon && inventory.HasBombs, ItemType.GoldCoin) }, //accès canyon && bombes
                { new Location(new LocationId("overworld-15x19-cave.tmx", "Chest_Small", new Vector3(480f, 0f, 492f)), "6 chest cave chest east",(inventory) => inventory.CanAccessCanyon && inventory.HasBombs, ItemType.GoldCoin) }, //accès canyon && bombes
                { new Location(new LocationId("overworld-15x19-cave.tmx", "Chest_Small", new Vector3(576f, 0f, 616f)), "6 chest cave chest south west",(inventory) => inventory.CanAccessCanyon && inventory.HasBombs, ItemType.GoldCoin) }, //accès canyon && bombes
                { new Location(new LocationId("overworld-15x19-cave.tmx", "Chest_Small", new Vector3(768f, 0f, 616f)), "6 chest cave chest south east",(inventory) => inventory.CanAccessCanyon && inventory.HasBombs, ItemType.GoldCoin) }, //accès canyon && bombes
                { new Location(new LocationId("overworld-15x19-cave.tmx", "Chest_Small", new Vector3(864f, 0f, 488f)), "6 chest cave chest north east",(inventory) => inventory.CanAccessCanyon && inventory.HasBombs, ItemType.GoldCoin) }, //accès canyon && bombes
                { new Location(new LocationId("overworld-15x19-cave.tmx", "Chest_Small", new Vector3(800f, 0f, 364f)), "6 chest cave chest south east",(inventory) => inventory.CanAccessCanyon && inventory.HasBombs, ItemType.GoldCoin) }, //accès canyon && bombes
            };
        }
    }
}
