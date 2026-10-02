using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace BlossomTales2.Randomizer.mm
{
    public class JungleNorthEast : Region
    {
        public override Predicate<Inventory> CanAccess => inventory => World.JungleBack.CanAccess(inventory) && inventory.HasFlipper;

        public JungleNorthEast(World world) : base("Jungle North East", world)
        {
            Locations = new List<Location>()
            {
                //Caves
                new Location(new LocationId("jungles-25x20-cave.tmx", "Chest_Small", new Vector3(448f, 0f, 200f)),
                    "Ladder Cave Chest",
                    inventory => inventory.HasTorch,
                    ItemType.HeartQ_1),
                //Mausoleum
                new Location(new LocationId("jungles-25x19-combat.tmx", "Chest_Small", new Vector3(2176f, 0f, 1792f)),
                    "Jungle Combat Mausoleum",
                    inventory => inventory.HasTorch && inventory.HasBow,
                    ItemType.CombatScroll),
            };
        }
    }
}
