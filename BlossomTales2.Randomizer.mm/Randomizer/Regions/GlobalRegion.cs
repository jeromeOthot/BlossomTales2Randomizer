// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace BlossomTales2.Randomizer.mm
{
    public class GlobalRegion : Region
    {
        public override bool CanAccess(Inventory inventory) => true;

        private bool CanReachTrader(Inventory inventory) => World.JungleBack.CanAccess(inventory) || World.CanyonSouthWest.CanAccess(inventory) || World.DarkWoodsBack.CanAccess(inventory);

        public GlobalRegion(World world) : base("Global", world)
        {
            Locations = new List<Location>
            {
                //long side quests
                new Location(new LocationId(string.Empty, "chipmunk_statue_award", Vector3.Zero),
                    "Chipmunk Statues Reward",
                    inventory => World.OverworldEast.CanAccess(inventory) && World.OverworldNorth.CanAccess(inventory) && World.OverworldWest.CanAccess(inventory) && inventory.HasFlipper,
                    ItemType.HeartQ_1),
                new Location(new LocationId(string.Empty, "frog_statue_award", Vector3.Zero),
                    "Frog Statues Reward",
                    inventory => World.JungleFront.CanAccess(inventory) && World.JungleBack.CanAccess(inventory) && World.JungleNorthEast.CanAccess(inventory) && World.JungleIsland.CanAccess(inventory),
                    ItemType.HeartQ_1),
                new Location(new LocationId(string.Empty, "lizard_statue_award", Vector3.Zero),
                    "Lizard Statues Reward",
                    inventory => World.CanyonNorth.CanAccess(inventory) && World.CanyonSouthWest.CanAccess(inventory) && World.CanyonSouthEast.CanAccess(inventory)
                                 && World.CanyonIsland.CanAccess(inventory) && World.CanyonSteppesSouth.CanAccess(inventory),
                    ItemType.HeartQ_1),
                new Location(new LocationId(string.Empty, "bunny_statue_award", Vector3.Zero),
                    "Bunny Statues Reward",
                    inventory => World.DarkWoodsFront.CanAccess(inventory) && World.Monsterton.CanAccess(inventory) && World.DarkWoodsBack.CanAccess(inventory),
                    ItemType.HeartQ_1),
                //Traders
                new Location(new LocationId(string.Empty, "traderStan0", Vector3.Zero),
                    "Trader Stan Item 1",
                    inventory => CanReachTrader(inventory),
                    ItemType.HeartQ_1),
                new Location(new LocationId(string.Empty, "traderStan1", Vector3.Zero),
                    "Trader Stan Item 2",
                    inventory => CanReachTrader(inventory),
                    ItemType.Jar_ArmorOrbs),
                new Location(new LocationId(string.Empty, "traderStan2", Vector3.Zero),
                    "Trader Stan Item 3",
                    inventory => CanReachTrader(inventory),
                    ItemType.Crystal),
                new Location(new LocationId(string.Empty, "traderStan3", Vector3.Zero),
                    "Trader Stan Item 4",
                    inventory => CanReachTrader(inventory),
                    ItemType.Five_Gems),
                new Location(new LocationId(string.Empty, "traderStan4", Vector3.Zero),
                    "Trader Stan Item 5",
                    inventory => CanReachTrader(inventory),
                    ItemType.Crystal),
                new Location(new LocationId(string.Empty, "traderStan5", Vector3.Zero),
                    "Trader Stan Item 6",
                    inventory => CanReachTrader(inventory),
                    ItemType.Five_Gems),
                new Location(new LocationId(string.Empty, "traderStan6", Vector3.Zero),
                    "Trader Stan Item 7",
                    inventory => CanReachTrader(inventory),
                    ItemType.HeartQ_1),
                new Location(new LocationId(string.Empty, "traderStan7", Vector3.Zero),
                    "Trader Stan Item 8",
                    inventory => CanReachTrader(inventory),
                    ItemType.Five_Gems),
            };
        }
    }
}
