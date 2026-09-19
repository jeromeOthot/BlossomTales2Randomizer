using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace BlossomTales2.Randomizer.mm
{
    public static class WorldFactory
    {
        public static World Create()
        {
            //TODO: Add world definition
            World world = new World();
            //world.AddRegion(CreateBlossomdale());
            return world;
        }

        private static Region CreateBlossomdale()
        {
            Region blossomdale = new Region("Blossomdale") { Locations = new List<Location>()
                {
                    new Location(new LocationId("blossom-lilyHouse.tmx", "npc7_1", new Vector3(480f, 0f, 328f)),
                        "Grandma 1", (_) => true, ItemType.Shield),
                    new Location(new LocationId("blossom-lilyHouse.tmx", "npc7_2", new Vector3(480f, 0f, 328f)),
                        "Grandma 2", (_) => true, ItemType.Sword),
                    new Location(new LocationId("blossom-house1.tmx", "Chest_Small", new Vector3(672f, 0f, 308f)),
                        "Blossom House 1", (_) => true, ItemType.Bombs ),
                    new Location(new LocationId("blossom-house2.tmx", "Chest_Small", new Vector3(708f, 0f, 356f)),
                        "Blossom House 2", (_) => true, ItemType.GoldCoin ),
                    new Location(new LocationId("blossom-tavern-basement.tmx", "Chest_Small", new Vector3(256f, 0f, 272f)),
                        "Tavern Top Left", (inventory) => inventory.HasBombs, ItemType.GoldCoin),
                    new Location(new LocationId("blossom-tavern-basement.tmx", "Chest_Small", new Vector3(256f, 0f, 464f)),
                        "Tavern Top Right", (inventory) => inventory.HasBombs, ItemType.GoldCoin),
                    new Location(new LocationId("blossom-tavern-basement.tmx", "Chest_Small", new Vector3(448f, 0f, 272f)),
                        "Tavern Lower Left", (inventory) => inventory.HasBombs, ItemType.GoldCoin),
                    new Location(new LocationId("blossom-tavern-basement.tmx", "Chest_Small", new Vector3(448f, 0f, 464f)),
                        "Tavern Lower Right", (inventory) => inventory.HasBombs, ItemType.HeartQ_1),
                },
                CanAccess = (_) => true
            };
            return blossomdale;
        }
    }
}
