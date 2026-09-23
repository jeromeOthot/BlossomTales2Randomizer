using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace BlossomTales2.Randomizer.mm
{
    public class OverworldEast : Region
    {
        public override Predicate<Inventory> CanAccess => inventory => inventory.CanCutPegs || inventory.HasBoomerang || inventory.HasFlipper ||
                                                                       World.OverworldNorth.CanAccess(inventory) && inventory.HasGrappleHook;

        public OverworldEast(World world) : base("Eastern Overworld", world)
        {
            Locations = new List<Location>
            {
                //Caves
                new Location(new LocationId("overworld-20x21-cave.tmx", "Chest_Small", new Vector3(296f, 0f, 224f)),
                    "Bomb Cave Top Left Chest",
                    inventory => inventory.HasBombs,
                    ItemType.GoldCoin),
                new Location(new LocationId("overworld-20x21-cave.tmx", "Chest_Small", new Vector3(424f, 0f, 224f)),
                    "Bomb Cave Top Right Chest",
                    inventory => inventory.HasBombs,
                    ItemType.GoldCoin),
                new Location(new LocationId("overworld-20x21-cave.tmx", "Chest_Small", new Vector3(424f, 0f, 320f)),
                    "Bomb Cave Lower Left Chest",
                    inventory => inventory.HasBombs,
                    ItemType.GoldCoin),
                new Location(new LocationId("overworld-20x21-cave.tmx", "Chest_Small", new Vector3(296f, 0f, 320f)),
                    "Bomb Cave Lower Right Chest",
                    inventory => inventory.HasBombs,
                    ItemType.GoldCoin),
                //Note Caves
                new Location(new LocationId("overworld-22x19-cave.tmx", "Chest_Small", new Vector3(992f, 0f, 1648f)),
                    "Note Cave Chest",
                    inventory => inventory.CanOpenNoteDoor,
                    ItemType.HeartQ_1),
                //Long sidequests
                new Location(new LocationId("chipmunkKing.tmx", "chipmunk", Vector3.Zero),
                    "Chipmunk King",
                    inventory => inventory.CanCollectIngredient(EquipableItem.IngredientList.Apple) && (inventory.HasFlipper || inventory.HasBombs || inventory.HasBoomerang) ,
                    ItemType.HeartQ_1),
                //Minigames
                new Location(new LocationId("overworld-21x20-gabby.tmx", "Chest_price_1", Vector3.Zero),
                    "Treasure Mini-game Prize 1",
                    _ => true,
                    ItemType.HeartQ_1),
                new Location(new LocationId("overworld-21x20-gabby.tmx", "Chest_price_2", Vector3.Zero),
                    "Treasure Mini-game Prize 2",
                    _ => true,
                    ItemType.Crystal),
                new Location(new LocationId("overworld-21x20-gabby.tmx", "Chest_price_3", Vector3.Zero),
                    "Treasure Mini-game Prize 3",
                    _ => true,
                    ItemType.Five_Gems),
                //Chests
                new Location(new LocationId("blossom-house5.tmx", "Chest_Small", new Vector3(480f, 0f, 172f)),
                    "Abandoned House Block Chest",
                    inventory => inventory.HasTorch,
                    ItemType.GoldCoin),
                new Location(new LocationId("blossom-house5.tmx", "Chest_Small", new Vector3(660f, 0f, 348f)),
                    "Abandoned House Table Chest",
                    inventory => inventory.HasTorch,
                    ItemType.GoldCoin),
                new Location(new LocationId("overworld-22x20.tmx", "Chest_Small", new Vector3(264f, 0f, 472f)),
                    "Ledge Chest",
                    _ => true,
                    ItemType.GoldCoin),
                //Tracking only
                new Location(new LocationId("overworld-21x21.tmx", "chipmunkStatue", Vector3.Zero), //need an ID?
                    "Chipmunk Statue",
                    inventory => inventory.HasFlipper,
                    ItemType.ChipmunkStatue),
                new Location(new LocationId("overworld-21x20.tmx", "seedling", Vector3.Zero), //need an ID?
                    "Seed soil",
                    inventory => inventory.HasTreeSeeds,
                    ItemType.PlantedTrees),
            };
        }
    }
}
