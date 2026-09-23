// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace BlossomTales2.Randomizer.mm.Canyon
{
    public class CanyonSteppesNorthRegion : Region
    {
        public override Predicate<Inventory> CanAccess => inventory => inventory.CanAccessCanyonSteppe;

        public CanyonSteppesNorthRegion(World world) : base("Canyon Steppes North Region", world)
        {
            Locations = new List<Location>
            {
                //Os
                { new Location(new LocationId("overworld-16x19.tmx", "PickUpItem", new Vector3(1616f, 0f, 988f)), "Canyon Bone North Steppes far west",_ => true, ItemType.CanyonBone) }, //accès canyon
                { new Location(new LocationId("overworld-17x19.tmx", "PickUpItem", new Vector3(796f, 0f, 1076f)), "Canyon Bone North Steppes far west",_ => true, ItemType.CanyonBone) }, //accès canyon
                { new Location(new LocationId("overworld-17x19.tmx", "PickUpItem", new Vector3(844f, 0f, 2180f)), "Canyon Bone North Steppes far west",_ => true, ItemType.CanyonBone) }, //accès canyon
                { new Location(new LocationId("overworld-18x19.tmx", "PickUpItem", new Vector3(256f, 0f, 1168f)), "Canyon Bone North Steppes far west",_ => true, ItemType.CanyonBone) }, //accès canyon
                { new Location(new LocationId("overworld-18x20.tmx", "PickUpItem", new Vector3(304f, 0f, 520f)), "Canyon Bone North Steppes far west",_ => true, ItemType.CanyonBone) }, //accès canyon

                //Chest
                { new Location(new LocationId("overworld-17x20-cave.tmx", "Chest_Small", new Vector3(736f, 0f, 224f)), "Canyon falling tiles cave chest",(inventory) => inventory.CanAccessCanyonSteppe && inventory.HasGrappleHook, ItemType.HeartQ_1) }, //accès canyon plateau && grappin
                { new Location(new LocationId("overworld-17x20.tmx", "Chest_Small", new Vector3(476f, 0f, 208f)), "Honeycomb Canyon Bone North Steppes",(inventory) => inventory.CanAccessCanyonSteppe && inventory.HasGrappleHook, ItemType.Honeycomb) }, //accès canyon plateau
                { new Location(new LocationId("overworld-18x19.tmx", "Chest_Small", new Vector3(792f, 0f, 1348f)), "Honeycomb on canyon river",(inventory) => inventory.CanAccessCanyonSteppe && inventory.HasGrappleHook, ItemType.Honeycomb) }, //accès canyon plateau && grappin
            };
        }
    }
}
