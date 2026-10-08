using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace BlossomTales2.Randomizer.mm
{
    public class OverworldWest : Region
    {
        public override bool CanAccess(Inventory inventory) => inventory.HasBombs || inventory.HasFlipper || inventory.HasBoomerang ||
                         inventory.CanCutPegs && inventory.HasGrappleHook;

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
                    inventory => inventory.CanCollectIngredient(EquipableItem.IngredientList.Lily) && inventory.CanCollectIngredient(EquipableItem.IngredientList.MoonFlower)
                        && inventory.CanCollectIngredient(EquipableItem.IngredientList.Tulip) && inventory.CanCollectIngredient(EquipableItem.IngredientList.Willow)
                        && inventory.CanCollectIngredient(EquipableItem.IngredientList.Skyblossom) && inventory.CanCollectIngredient(EquipableItem.IngredientList.CanyonWisp)
                        && inventory.CanCollectIngredient(EquipableItem.IngredientList.Chrysanthemum) && inventory.CanCollectIngredient(EquipableItem.IngredientList.Aster)
                        && inventory.CanCollectIngredient(EquipableItem.IngredientList.Sunkiss) && inventory.CanCollectIngredient(EquipableItem.IngredientList.DesertPuff)
                        && inventory.CanCollectIngredient(EquipableItem.IngredientList.WaterDrop) && inventory.CanCollectIngredient(EquipableItem.IngredientList.FlameTongue)
                        && inventory.CanCollectIngredient(EquipableItem.IngredientList.CactusRose) && inventory.CanCollectIngredient(EquipableItem.IngredientList.Poinsettia)
                        && inventory.CanCollectIngredient(EquipableItem.IngredientList.Bellflower) && inventory.CanCollectIngredient(EquipableItem.IngredientList.Daisy),
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
            };
        }
    }
}
