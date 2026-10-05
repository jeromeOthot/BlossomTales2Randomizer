using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace BlossomTales2.Randomizer.mm
{
    public class Anchortown : Region
    {
        public override bool CanAccess(Inventory inventory) => World.JungleFront.CanAccess(inventory);

        public Anchortown(World world) : base("Anchortown", world)
        {
            Locations = new List<Location>()
            {
                //Npcs
                new Location(new LocationId("jungles-23x21.tmx", "fisherman", Vector3.Zero),
                    "Fisherman",
                    _ => true,
                    ItemType.FishingRod),
                //Long sidequests
                new Location(new LocationId("anchor-shop.tmx", "fisherman", Vector3.Zero),
                    "Fish Collector",
                    inventory => inventory.CanCollectAllFishes,
                    ItemType.FishingRod),
                //Shops
                new Location(new LocationId("anchor-shop.tmx", "left", Vector3.Zero),
                    "Shop Left Item",
                    _ => true,
                    ItemType.Jar_Empty),
                new Location(new LocationId("anchor-shop.tmx", "center", Vector3.Zero),
                    "Shop Middle Item",
                    _ => true,
                    ItemType.Crystal),
                new Location(new LocationId("anchor-shop.tmx", "right", Vector3.Zero),
                    "Shop Right Item",
                    _ => true,
                    ItemType.HeartQ_1),
                //Chests
                new Location(new LocationId("anchor-house4.tmx", "Chest_Small", new Vector3(348f, 0f, 436f)),
                    "Southwest House Chest",
                    _ => true,
                    ItemType.GoldCoin),
                new Location(new LocationId("jungles-23x20.tmx", "Chest_Small", new Vector3(2264f, 0f, 1928f)),
                    "Anchortown Back Chest",
                    _ => true,
                    ItemType.GoldCoin),
            };
        }
    }
}
