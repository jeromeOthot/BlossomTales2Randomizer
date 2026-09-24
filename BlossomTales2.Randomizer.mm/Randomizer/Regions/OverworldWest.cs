using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace BlossomTales2.Randomizer.mm
{
    public class OverworldWest : Region
    {
        public override Predicate<Inventory> CanAccess =>
            inventory => inventory.HasBombs || inventory.HasFlipper || inventory.HasBoomerang;/* ||
                                                                           World.OverworldNorth.CanAccess(inventory) && inventory.HasGrappleHook;*/

        public OverworldWest(World world) : base("Western Overworld", world)
        {
            Locations = new List<Location>
            {
                //Caves
                new Location(new LocationId("overworld-18x19-cave.tmx", "Chest_Small", new Vector3(512f, 0f, 308f)),
                    "Bomb Cave Top Left Chest",
                    inventory => inventory.HasBombs,
                    ItemType.GoldCoin),
                new Location(new LocationId("overworld-18x19-cave.tmx", "Chest_Small", new Vector3(704f, 0f, 308f)),
                    "Bomb Cave Top Right Chest",
                    inventory => inventory.HasBombs,
                    ItemType.GoldCoin),
                new Location(new LocationId("overworld-18x19-cave.tmx", "Chest_Small", new Vector3(512f, 0f, 544f)),
                    "Bomb Cave Lower Left Chest",
                    inventory => inventory.HasBombs,
                    ItemType.GoldCoin),
                new Location(new LocationId("overworld-18x19-cave.tmx", "Chest_Small", new Vector3(704f, 0f, 544f)),
                    "Bomb Cave Lower Right Chest",
                    inventory => inventory.HasBombs,
                    ItemType.GoldCoin),
                new Location(new LocationId("overworld-19x19-cave.tmx", "Chest_Small", new Vector3(608f, 0f, 224f)),
                    "Pond Cave Chest",
                    inventory => inventory.HasBow,
                    ItemType.HeartQ_1),
                new Location(new LocationId("overworld-19x18-flowerShop.tmx", "flowerShop", Vector3.Zero),
                    "Flower Collection",
                    _ => true, //TODO
                    ItemType.HeartQ_1),
                //Mingames
                new Location(new LocationId("overworld-19x19.tmx", "raceGame", Vector3.Zero),
                    "Racing Minigame",
                    _ => true,
                    ItemType.HeartQ_1),
                //Chests
                new Location(new LocationId("overworld-18x18.tmx", "Chest_Small", new Vector3(596f, 0f, 2064f)),
                    "Bomb Rock Chest",
                    inventory => inventory.HasBombs,
                    ItemType.GoldCoin),
                new Location(new LocationId("overworld-18x18.tmx", "Chest_Small", new Vector3(596f, 0f, 2064f)),
                    "Bomb Rock Chest",
                    inventory => inventory.HasBombs,
                    ItemType.GoldCoin),
                new Location(new LocationId("overworld-19x18.tmx", "Chest_Small", new Vector3(184f, 0f, 296f)),
                    "Hidden Trees Chest",
                    _ => true,
                    ItemType.Honeycomb),
                //Tracking only
                new Location(new LocationId("overworld-19x20.tmx", "chipmunkStatue", Vector3.Zero),
                    "Chipmunk Statue",
                    _ => true,
                    ItemType.ChipmunkStatue),
                new Location(new LocationId("overworld-20x19.tmx", "chipmunkStatue", Vector3.Zero),
                    "Chipmunk Statue",
                    _ => true,
                    ItemType.ChipmunkStatue),
                new Location(new LocationId("overworld-18x18.tmx", "seedling", Vector3.Zero),
                    "Minotaur Gate Seed soil",
                    _ => true,
                    ItemType.PlantedTrees),
                new Location(new LocationId("overworld-18x20.tmx", "seedling", Vector3.Zero),
                    "Southwest Seed soil",
                    _ => true,
                    ItemType.PlantedTrees),
            };
        }
    }
}
