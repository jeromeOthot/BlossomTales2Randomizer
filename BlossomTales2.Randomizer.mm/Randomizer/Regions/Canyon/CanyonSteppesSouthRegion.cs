// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace BlossomTales2.Randomizer.mm.Canyon
{
    public class CanyonSteppesSouthRegion : Region
    {
        public override Predicate<Inventory> CanAccess =>
            inventory => inventory.HasGrappleHook &&  World.CanyonSouthEast.CanAccess(inventory) ;

        public CanyonSteppesSouthRegion(World world) : base("Steppes South Region", world)
        {
            Locations = new List<Location>
            {
                //Os
                {
                 new Location(new LocationId("overworld-18x21.tmx", "PickUpItem", new Vector3(160f, 0f, 736f)),
                            "bone corner edge", _ => true, ItemType.CanyonBone)
                }, //accès canyon steppe

                //Honeycomb
                //TODO: Manque le chest du Honeycomb a la carte 18x21

                //Chest
                {
                    new Location(new LocationId("overworld-17x20.tmx", "Chest_Small", new Vector3(476f, 0f, 208f)),
                        "chest north ledge left", _ => true, ItemType.GoldCoin)
                }, //accès canyon
                {
                    new Location(new LocationId("overworld-17x20-cave.tmx", "Chest_Small", new Vector3(736f, 0f, 224f)),
                        "chest north ledge right", _ => true, ItemType.GoldCoin)
                }, //accès canyon
                {
                    new Location(new LocationId("overworld-17x21-cave.tmx", "Chest_Small", new Vector3(416f, 0f, 480f)),
                        "cave chest", inventory => inventory.CanSwitchLevers, ItemType.HeartQ_1)
                } //accès canyon steppe && leviers
            };
        }
    }
}
