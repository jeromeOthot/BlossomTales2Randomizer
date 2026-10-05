using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace BlossomTales2.Randomizer.mm
{
    public class Blockburg : Region
    {
        public override Predicate<Inventory> CanAccess => inventory => true;

        public Blockburg(World world) : base("Blockburg", world)
        {
            Locations = new List<Location>
            {
                { new Location(new LocationId("overworld-16x17.tmx", "labSlime", new Vector3(0f, 0f, 0f)),  "lab Slimes", (inventory) => true, ItemType.HeartQ_1) }, //accès labyrinthe

                //Castle
                { new Location(new LocationId("labHouse-shop.tmx", "left", Vector3.Zero), "shop item left", (inventory) => inventory.HasBombs, ItemType.Jar_Empty) },
                { new Location(new LocationId("labHouse-shop.tmx", "center", Vector3.Zero), "shop item center", (inventory) => inventory.HasBombs, ItemType.Crystal) },
                { new Location(new LocationId("labHouse-shop.tmx", "right", Vector3.Zero), "shop item right", (inventory) => inventory.HasBombs, ItemType.HeartQ_1) },
            };
        }
    }
}
