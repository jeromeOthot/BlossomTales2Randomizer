using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace BlossomTales2.Randomizer.mm
{
    public class Blossomdale : Region
    {
        public override Predicate<Inventory> CanAccess => _ => true;

        public Blossomdale() : base("Blossomdale")
        {
            Locations = new List<Location>
            {
                //caves
                new Location(new LocationId("overworld-21x20-cave.tmx", "Chest_Small", new Vector3(1192f, 0f, 612f)),
                    "Blossomdale Cave Left Chest",
                    _ => true,
                    ItemType.GoldCoin),
                new Location(new LocationId("overworld-21x20-cave.tmx", "Chest_Small", new Vector3(1268f, 0f, 612f)),
                    "Blossomdale Cave Right Chest",
                    _ => true,
                    ItemType.GoldCoin),

                //items npc
                new Location(new LocationId("blossom-blacksmith.tmx", "npc21", Vector3.Zero),
                    "Blacksmith",
                    inventory => inventory.HasBeatenMorklaBoss,
                    ItemType.Bow),
                new Location(new LocationId("blossom-lilyHouse.tmx", "npc7_1", new Vector3(480f, 0f, 328f)),
                    "Grandma 1",
                    _ => true,
                    ItemType.Shield),
                new Location(new LocationId("blossom-lilyHouse.tmx", "npc7_2", new Vector3(480f, 0f, 328f)),
                    "Grandma 2",
                    _ => true,
                    ItemType.Sword),

                //minigames
                new Location(new LocationId("tent-arrow.tmx", "arrowGame", Vector3.Zero),
                    "Bow Minigame",
                    inventory => inventory.HasBow,
                    ItemType.HeartQ_1),

                //mail
                new Location(new LocationId("blossom-postOffice.tmx", "postal_heart", Vector3.Zero),
                    "PostOffice 1",
                    _ => false, //TODO: Add requirements
                    ItemType.HeartQ_1),
                new Location(new LocationId("blossom-postOffice.tmx", "postal_falcon", Vector3.Zero),
                    "PostOffice Final",
                    _ => false, //TODO: Add requirements
                    ItemType.Falcon),

                //shops
                new Location(new LocationId("blossom-shop.tmx", "left", Vector3.Zero),
                    "Blossomdale Shop Left Item",
                    _ => true,
                    ItemType.Jar_Empty),
                new Location(new LocationId("blossom-shop.tmx", "center", Vector3.Zero),
                    "Blossomdale Shop Middle Item",
                    _ => true,
                    ItemType.Crystal),
                new Location(new LocationId("blossom-shop.tmx", "right", Vector3.Zero),
                    "Blossomdale Shop Right Item",
                    _ => true,
                    ItemType.HeartQ_1),

                //bardes
                new Location(new LocationId("blossom-tavern.tmx", "bard_song", Vector3.Zero),
                    "Blossomdale Tavern Bard",
                    inventory => inventory.HasInstrument,
                    ItemType.GrandpaHint),

                //tresors
                new Location(new LocationId("blossom-house1.tmx", "Chest_Small", new Vector3(672f, 0f, 308f)),
                    "Blossomdale House South Left Chest",
                    _ => true,
                    ItemType.GoldCoin),
                new Location(new LocationId("blossom-house2.tmx", "Chest_Small", new Vector3(708f, 0f, 356f)),
                    "Blossomdale House South Right Chest",
                    _ => true,
                    ItemType.GoldCoin),
                new Location(new LocationId("blossom-house3.tmx", "Chest_Small", new Vector3(368f, 0f, 148f)),
                    "Blossomdale Potion House Chest",
                    _ => true,
                    ItemType.GoldCoin),
                new Location(new LocationId("blossom-house4.tmx", "Chest_Small", new Vector3(696f, 0f, 416f)),
                    "Blossomdale North House Table Chest",
                    _ => true,
                    ItemType.GoldCoin),
                new Location(new LocationId("blossom-house4.tmx", "Chest_Small", new Vector3(780f, 0f, 156f)),
                    "Blossomdale North House Fireplace Chest",
                    _ => true,
                    ItemType.GoldCoin),
                new Location(new LocationId("blossom-house4.tmx", "Chest_Small", new Vector3(408f, 0f, 340f)),
                    "Blossomdale North House Torch Chest",
                    inventory => inventory.HasTorch,
                    ItemType.GoldCoin),
                new Location(new LocationId("blossom-tavern-basement.tmx", "Chest_Small", new Vector3(256f, 0f, 272f)),
                    "Blossomdale Tavern Top Left Chest",
                    inventory => inventory.HasBombs,
                    ItemType.GoldCoin),
                new Location(new LocationId("blossom-tavern-basement.tmx", "Chest_Small", new Vector3(256f, 0f, 464f)),
                    "Blossomdale Tavern Top Right Chest",
                    inventory => inventory.HasBombs,
                    ItemType.GoldCoin),
                new Location(new LocationId("blossom-tavern-basement.tmx", "Chest_Small", new Vector3(448f, 0f, 272f)),
                    "Blossomdale Tavern Lower Left Chest",
                    inventory => inventory.HasBombs,
                    ItemType.GoldCoin),
                new Location(new LocationId("blossom-tavern-basement.tmx", "Chest_Small", new Vector3(448f, 0f, 464f)),
                    "Blossomdale Tavern Lower Right Chest",
                    inventory => inventory.HasBombs,
                    ItemType.HeartQ_1),
                new Location(new LocationId("overworld-20x20.tmx", "Chest_Small", new Vector3(2352f, 0f, 2212f)),
                    "Blossomdale Bomb Rock Chest",
                    inventory => inventory.HasBombs,
                    ItemType.GoldCoin)
            };
        }
    }
}
