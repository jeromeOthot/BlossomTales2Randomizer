// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace BlossomTales2.Randomizer.mm.Canyon
{
    public class CanyonSteppesSouthRegion : Region
    {
        public override bool CanAccess(Inventory inventory) => World.CanyonSouthEast.CanAccess(inventory) && (inventory.HasGrappleHook || inventory.HasBoomerang);

        public CanyonSteppesSouthRegion(World world) : base("Steppes South Region", world)
        {
            Locations = new List<Location>
            {
                //Caves
                new Location(new LocationId("overworld-17x21-cave.tmx", "Chest_Small", new Vector3(416f, 0f, 480f)),
                    "Cave Chest",
                    inventory => inventory.CanSwitchLevers,
                    ItemType.HeartQ_1),
                //Os
                new Location(new LocationId("overworld-18x21.tmx", "PickUpItem", new Vector3(160f, 0f, 736f)),
                    "bone corner edge",
                    _ => true,
                    ItemType.CanyonBone),
                //Chest
                new Location(new LocationId("overworld-17x20.tmx", "Chest_Small", new Vector3(1056f, 0f, 2008f)),
                    "chest north ledge left",
                    _ => true,
                    ItemType.GoldCoin),
                new Location(new LocationId("overworld-17x20.tmx", "Chest_Small", new Vector3(1184f, 0f, 2008f)),
                    "chest north ledge right",
                    _ => true,
                    ItemType.GoldCoin),
                new Location(new LocationId("overworld-18x21.tmx", "Chest_Small", new Vector3(224f, 0f, 592f)),
                    "chest honeycomb",
                    _ => true,
                    ItemType.Honeycomb),
                new Location(new LocationId("overworld-18x21.tmx", "Chest_Small", new Vector3(784, 0f, 156)),
                    "chest coins",
                    _ => true,
                    ItemType.GoldCoin),
            };
        }
    }
}
