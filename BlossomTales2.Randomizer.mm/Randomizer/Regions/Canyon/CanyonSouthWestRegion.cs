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

        public CanyonSouthWestRegion(World world) : base("South West Region", world)
        {
            Locations = new List<Location>
            {
                //Os
                {
                    new Location(new LocationId("overworld-15x21.tmx", "PickUpItem", new Vector3(288f, 0f, 1952f)),
                        "bone north east village", _ => true, ItemType.CanyonBone)
                }, //accès canyon
                {
                    new Location(new LocationId("overworld-15x22.tmx", "PickUpItem", new Vector3(1568f, 0f, 2400f)),
                        "bone east village", _ => true, ItemType.CanyonBone)
                }, //accès canyon
                {
                    new Location(new LocationId("overworld-16x21.tmx", "PickUpItem", new Vector3(288f, 0f, 296f)),
                        "bone north  village", _ => true, ItemType.CanyonBone)
                }, //accès canyon

                //Chest
                {
                    new Location(new LocationId("overworld-15x21-cave.tmx", "Chest_Small", new Vector3(352f, 0f, 412f)),
                        "village north west cave chest left up", (inventory) => inventory.HasBombs, ItemType.GoldCoin)
                }, //accès canyon && bombes
                {
                    new Location(new LocationId("overworld-15x21-cave.tmx", "Chest_Small", new Vector3(608f, 0f, 408f)),
                        "village north west cave chest right up", (inventory) => inventory.HasBombs, ItemType.GoldCoin)
                }, //accès canyon && bombes
                {
                    new Location(new LocationId("overworld-15x21-cave.tmx", "Chest_Small", new Vector3(352f, 0f, 604f)),
                        "village north west cave chest left down", (inventory) => inventory.HasBombs, ItemType.GoldCoin)
                }, //accès canyon && bombes
                {
                    new Location(new LocationId("overworld-15x21-cave.tmx", "Chest_Small", new Vector3(608f, 0f, 604f)),
                        "village north west cave chest right down", (inventory) => inventory.HasBombs, ItemType.GoldCoin)
                }, //accès canyon && bombes

                {
                    new Location(
                        new LocationId("overworld-15x22-cave.tmx", "Chest_Small", new Vector3(1216f, 0f, 448f)),
                        "village  west cave chest left",
                        (inventory) => inventory.CanOpenNoteDoor && inventory.CanActivateBlueSwitch, ItemType.GoldCoin)
                }, //accès canyon && ouvrir portes note && levier
                {
                    new Location(
                        new LocationId("overworld-15x22-cave.tmx", "Chest_Small", new Vector3(1408f, 0f, 448f)),
                        "village  west cave chest right",
                        (inventory) => inventory.CanOpenNoteDoor && inventory.CanActivateBlueSwitch, ItemType.GoldCoin)
                }, //accès canyon && ouvrir portes note && levier
                {
                    new Location(
                        new LocationId("overworld-15x22-cave.tmx", "Chest_Small", new Vector3(1312f, 0f, 312f)),
                        "village  west cave chest center",
                        (inventory) => inventory.CanOpenNoteDoor && inventory.CanActivateBlueSwitch, ItemType.GoldCoin)
                }, //accès canyon && ouvrir portes note && levier

                {
                    new Location(new LocationId("overworld-16x21-cave.tmx", "Chest_Small", new Vector3(544f, 0f, 228f)),
                        "village  north cave chest", (inventory) => inventory.CanActivateBlueSwitch,
                        ItemType.GoldCoin)
                }, //accès canyon && levier

                //short sidequests
                {
                    new Location(new LocationId("colosseum.tmx", "price_heart", Vector3.Zero),
                        "colosseum price heart", (inventory) => inventory.CanDoDamage && inventory.HasBombs,
                        ItemType.HeartQ_1)
                },
                {
                    new Location(new LocationId("colosseum.tmx", "price_crystal", Vector3.Zero),
                        "colosseum price crystal", (inventory) => inventory.CanDoDamage && inventory.HasBombs,
                        ItemType.Crystal)
                },
            };
        }
    }
}
