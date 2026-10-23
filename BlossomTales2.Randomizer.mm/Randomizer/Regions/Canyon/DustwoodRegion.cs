using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace BlossomTales2.Randomizer.mm.Canyon
{
    public class DustwoodRegion : Region
    {
        public override bool CanAccess(Inventory inventory) => World.CanyonSouthWest.CanAccess(inventory);

        public DustwoodRegion(World world) : base("Dustwood", world)
        {
            Locations = new List<Location>
            {
                //NPC
                new Location(new LocationId("overworld-16x22.tmx", "beggar", new Vector3(980f, 0f, 1616f)),
                    "Beggar Reward",
                    _ => true,
                    ItemType.HeartQ_1,
                    LocationPool.Sidequests),
                //Shop
                new Location( new LocationId("canyon-shop.tmx", "left", Vector3.Zero),
                    "Shop Left Item",
                    _ => true,
                    ItemType.Jar_Empty,
                    LocationPool.Shops),
                new Location(new LocationId("canyon-shop.tmx", "center", Vector3.Zero),
                    "Shop Middle Item",
                    _ => true,
                    ItemType.Crystal,
                    LocationPool.Shops),
                new Location(new LocationId("canyon-shop.tmx", "right", Vector3.Zero),
                    "Shop Right Item",
                    _ => true,
                    ItemType.HeartQ_1,
                    LocationPool.Shops),
                //House chest
                 new Location(new LocationId("canyon-house1.tmx", "Chest_Small", new Vector3(576f, 0f, 208f)),
                     "Northwest House Chest",
                     _ => true,
                     ItemType.GoldCoin),
                 new Location(new LocationId("canyon-house3.tmx", "Chest_Small", new Vector3(344f, 0f, 496f)),
                     "Southeast House Left Chest",
                     _ => true,
                     ItemType.GoldCoin),
                 new Location(new LocationId("canyon-house3.tmx", "Chest_Small", new Vector3(772f, 0f, 580f)),
                     "Southeast House Right Chest",
                     _ => true,
                     ItemType.GoldCoin),
            };
        }
    }
}
