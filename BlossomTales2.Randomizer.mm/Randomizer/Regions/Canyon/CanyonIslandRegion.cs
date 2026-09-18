// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace BlossomTales2.Randomizer.mm.Canyon
{
    public class CanyonIslandRegion : Region
    {
        public List<Location>  Locations { get; private set; }

        public CanyonIslandRegion() : base("CanyonIsland")
        {
            Locations = new List<Location>
            {
                //mausoleum
                {  new Location(new LocationId("overworld-20x22-combat.tmx", "Chest_Small", new Vector3(2176f, 0f, 1828f)),"Canyon Mausoleum", (inventory) => inventory.CanAccesCanyon() && inventory.CanDoDommage(), ItemType.CombatScroll) }, //accès canyon && leviers (&& damage)

                //Cave
                { new Location(new LocationId("overworld-19x22-bardCave.tmx", "Chest_Small", new Vector3(416f, 0f, 240f)), "Canyon Bard Cave", (inventory) => inventory.CanAccesCanyon() && inventory.CanDoDommage(), ItemType.HeartQ_1)}, //accès canyon && damage (&& ouvrir portes note)

                // NPC Bard
                { new Location(new LocationId("overworld-19x22.tmx", "bard", new Vector3(852f, 0f, 1348f)), "Canyon Bard",(inventory) => inventory.CanAccesCanyon() && inventory.CanDoDommage(), ItemType.Guitar) }, //accès canyon && damage
                { new Location(new LocationId("overworld-19x22.tmx", "bard_song", new Vector3(852f, 0f, 1348f)), "Canyon Bard Song", (inventory) => inventory.CanAccesCanyon() && inventory.CanDoDommage(), ItemType.OpenSesame) }, //accès canyon && damage

                //0S
                { new Location(new LocationId("overworld-19x21.tmx", "PickUpItem", new Vector3(536f, 0f, 2072f)), "Canyon Island Bone North East",(inventory) => inventory.CanAccesCanyon(), ItemType.CanyonBone) }, //accès canyon
                { new Location(new LocationId("overworld-19x22.tmx", "PickUpItem", new Vector3(1568f, 0f, 928f)), "Canyon Island Bone North West ",(inventory) => inventory.CanAccesCanyon(), ItemType.CanyonBone) }, //accès canyon
                { new Location(new LocationId("overworld-20x21.tmx", "PickUpItem", new Vector3(800f, 0f, 2208)), "Canyon Island Bone South East",(inventory) => inventory.CanAccesCanyon(), ItemType.CanyonBone) }, //accès canyon
                { new Location(new LocationId("overworld-20x22.tmx", "PickUpItem", new Vector3(688f, 0f, 916f)), "Canyon Island Bone South West",(inventory) => inventory.CanAccesCanyon(), ItemType.CanyonBone) }, //accès canyon

                //Chest
                { new Location(new LocationId("overworld-18x22.tmx", "Chest_Small", new Vector3(448f, 0f, 448f)), "Canyon Island cave chest #1", (inventory) => inventory.CanAccesCanyon() && inventory.HasFlipper &&  inventory.HasBombs, ItemType.GoldCoin) }, //accès canyon && flippers && bombes
                { new Location(new LocationId("overworld-18x22.tmx", "Chest_Small", new Vector3(640f, 0f, 448f)),"Canyon Island cave chest #2", (inventory) => inventory.CanAccesCanyon() && inventory.HasFlipper && inventory.HasBombs, ItemType.GoldCoin) }, //accès canyon && flippers && bombes
                { new Location(new LocationId("overworld-18x22.tmx", "Chest_Small", new Vector3(544f, 0f, 344f)), "Canyon Island cave chest #3", (inventory) => inventory.CanAccesCanyon() && inventory.HasFlipper && inventory.HasBombs, ItemType.GoldCoin) }, //accès canyon && flippers && bombe
                { new Location(new LocationId("overworld-19x21.tmx", "Chest_Small", new Vector3(2348f, 0f, 1832f)),  "Canyon Island camper chest", (inventory) => inventory.CanAccesCanyon(), ItemType.GoldCoin) }, //accès canyon
            };

        }
    }
}
