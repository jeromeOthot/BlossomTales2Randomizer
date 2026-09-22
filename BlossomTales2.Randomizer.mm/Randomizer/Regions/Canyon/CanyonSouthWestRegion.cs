// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace BlossomTales2.Randomizer.mm.Canyon
{
    public class CanyonSouthWestRegion: Region
    {
        public override Predicate<Inventory> CanAccess => inventory => inventory.CanAccessCanyon;

        public CanyonSouthWestRegion(World world) : base("Canyon South West Region", world)
        {
            Locations = new List<Location>
            {
                //Os
                {
                    new Location(new LocationId("overworld-15x21.tmx", "PickUpItem", new Vector3(288f, 0f, 1952f)),
                        "Canyon bone north east village", _ => true, ItemType.CanyonBone)
                }, //accès canyon
                {
                    new Location(new LocationId("overworld-15x22.tmx", "PickUpItem", new Vector3(1568f, 0f, 2400f)),
                        "Canyon bone east village", _ => true, ItemType.CanyonBone)
                }, //accès canyon
                {
                    new Location(new LocationId("overworld-16x21.tmx", "PickUpItem", new Vector3(288f, 0f, 296f)),
                        "Canyon bone north  village", _ => true, ItemType.CanyonBone)
                }, //accès canyon

                //Chest
                {
                    new Location(new LocationId("overworld-15x21-cave.tmx", "Chest_Small", new Vector3(352f, 0f, 412f)),
                        "Canyon village north west cave chest #1", (inventory) => inventory.HasBombs, ItemType.GoldCoin)
                }, //accès canyon && bombes
                {
                    new Location(new LocationId("overworld-15x21-cave.tmx", "Chest_Small", new Vector3(608f, 0f, 408f)),
                        "Canyon village north west cave chest #2", (inventory) => inventory.HasBombs, ItemType.GoldCoin)
                }, //accès canyon && bombes
                {
                    new Location(new LocationId("overworld-15x21-cave.tmx", "Chest_Small", new Vector3(352f, 0f, 604f)),
                        "Canyon village north west cave chest #3", (inventory) => inventory.HasBombs, ItemType.GoldCoin)
                }, //accès canyon && bombes
                {
                    new Location(new LocationId("overworld-15x21-cave.tmx", "Chest_Small", new Vector3(608f, 0f, 604f)),
                        "Canyon village north west cave chest #4", (inventory) => inventory.HasBombs, ItemType.GoldCoin)
                }, //accès canyon && bombes

                {
                    new Location(
                        new LocationId("overworld-15x22-cave.tmx", "Chest_Small", new Vector3(1216f, 0f, 448f)),
                        "Canyon village  west cave chest #1",
                        (inventory) => inventory.CanOpenDoorNote && inventory.CanActivateBlueSwitch, ItemType.GoldCoin)
                }, //accès canyon && ouvrir portes note && levier
                {
                    new Location(
                        new LocationId("overworld-15x22-cave.tmx", "Chest_Small", new Vector3(1408f, 0f, 448f)),
                        "Canyon village  west cave chest #2",
                        (inventory) => inventory.CanOpenDoorNote && inventory.CanActivateBlueSwitch, ItemType.GoldCoin)
                }, //accès canyon && ouvrir portes note && levier
                {
                    new Location(
                        new LocationId("overworld-15x22-cave.tmx", "Chest_Small", new Vector3(1312f, 0f, 312f)),
                        "Canyon village  west cave chest #3",
                        (inventory) => inventory.CanOpenDoorNote && inventory.CanActivateBlueSwitch, ItemType.GoldCoin)
                }, //accès canyon && ouvrir portes note && levier

                {
                    new Location(new LocationId("overworld-16x21-cave.tmx", "Chest_Small", new Vector3(544f, 0f, 228f)),
                        "Canyon village  north cave chest", (inventory) => inventory.CanActivateBlueSwitch,
                        ItemType.GoldCoin)
                }, //accès canyon && levier

                //short sidequests
                {
                    new Location(new LocationId("colosseum.tmx", "price_heart", Vector3.Zero),
                        "Canyon colosseum price heart", (inventory) => inventory.CanDoDamage && inventory.HasBombs,
                        ItemType.HeartQ_1)
                },
                {
                    new Location(new LocationId("colosseum.tmx", "price_crystal", Vector3.Zero),
                        "Canyon colosseum price crystal", (inventory) => inventory.CanDoDamage && inventory.HasBombs,
                        ItemType.Crystal)
                },
            };
        }
    }
}
