using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace BlossomTales2.Randomizer.mm
{
    public class GlobalRegion : Region
    {
        public override bool CanAccess(Inventory inventory) => true;

        private bool CanReachTrader(Inventory inventory) => World.JungleBack.CanAccess(inventory) || World.CanyonSouthWest.CanAccess(inventory) || World.DarkWoodsBack.CanAccess(inventory);
        private bool CanDoTrade1(Inventory inventory) => CanReachTrader(inventory)
                                                         && inventory.CanCollectIngredient(EquipableItem.IngredientList.Apple)
                                                         && inventory.CanCollectIngredient(EquipableItem.IngredientList.Mushroom)
                                                         && inventory.CanCollectIngredient(EquipableItem.IngredientList.Clover);
        private bool CanDoTrade2(Inventory inventory) => CanDoTrade1(inventory)
                                                         && inventory.CanCollectIngredient(EquipableItem.IngredientList.Chrysanthemum)
                                                         && inventory.CanCollectIngredient(EquipableItem.IngredientList.Orange)
                                                         && inventory.CanCollectIngredient(EquipableItem.IngredientList.Willow);
        private bool CanDoTrade3(Inventory inventory) => CanDoTrade2(inventory)
                                                         && inventory.CanCollectIngredient(EquipableItem.IngredientList.Skyblossom)
                                                         && inventory.CanCollectIngredient(EquipableItem.IngredientList.Clam)
                                                         && inventory.CanCollectIngredient(EquipableItem.IngredientList.Snailshell);
        private bool CanDoTrade4(Inventory inventory) => CanDoTrade3(inventory)
                                                         && inventory.CanCollectIngredient(EquipableItem.IngredientList.CanyonWisp)
                                                         && inventory.CanCollectIngredient(EquipableItem.IngredientList.Jojoba)
                                                         && inventory.CanCollectIngredient(EquipableItem.IngredientList.Sunkiss);
        private bool CanDoTrade5(Inventory inventory) => CanDoTrade4(inventory)
                                                         && inventory.CanCollectIngredient(EquipableItem.IngredientList.DesertPuff)
                                                         && inventory.CanCollectIngredient(EquipableItem.IngredientList.WaterDrop)
                                                         && inventory.CanCollectIngredient(EquipableItem.IngredientList.FlameTongue);
        private bool CanDoTrade6(Inventory inventory) => CanDoTrade5(inventory)
                                                         && inventory.CanCollectIngredient(EquipableItem.IngredientList.Aster)
                                                         && inventory.CanCollectIngredient(EquipableItem.IngredientList.Lily)
                                                         && inventory.CanCollectIngredient(EquipableItem.IngredientList.RootWeed);
        private bool CanDoTrade7(Inventory inventory) => CanDoTrade6(inventory)
                                                         && inventory.CanCollectIngredient(EquipableItem.IngredientList.RedMushroom)
                                                         && inventory.CanCollectIngredient(EquipableItem.IngredientList.GreenMushroom)
                                                         && inventory.CanCollectIngredient(EquipableItem.IngredientList.PurpleMushroom);
        private bool CanDoTrade8(Inventory inventory) => CanDoTrade7(inventory)
                                                         && inventory.CanCollectIngredient(EquipableItem.IngredientList.Poinsettia)
                                                         && inventory.CanCollectIngredient(EquipableItem.IngredientList.Bellflower)
                                                         && inventory.CanCollectIngredient(EquipableItem.IngredientList.Daisy);

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
                    inventory => CanDoTrade1(inventory),
                    ItemType.HeartQ_1),
                new Location(new LocationId(string.Empty, "traderStan1", Vector3.Zero),
                    "Trader Stan Item 2",
                    inventory => CanDoTrade2(inventory),
                    ItemType.Jar_ArmorOrbs),
                new Location(new LocationId(string.Empty, "traderStan2", Vector3.Zero),
                    "Trader Stan Item 3",
                    inventory => CanDoTrade3(inventory),
                    ItemType.Crystal),
                new Location(new LocationId(string.Empty, "traderStan3", Vector3.Zero),
                    "Trader Stan Item 4",
                    inventory => CanDoTrade4(inventory),
                    ItemType.Five_Gems),
                new Location(new LocationId(string.Empty, "traderStan4", Vector3.Zero),
                    "Trader Stan Item 5",
                    inventory => CanDoTrade5(inventory),
                    ItemType.Crystal),
                new Location(new LocationId(string.Empty, "traderStan5", Vector3.Zero),
                    "Trader Stan Item 6",
                    inventory => CanDoTrade6(inventory),
                    ItemType.Five_Gems),
                new Location(new LocationId(string.Empty, "traderStan6", Vector3.Zero),
                    "Trader Stan Item 7",
                    inventory => CanDoTrade7(inventory),
                    ItemType.HeartQ_1),
                new Location(new LocationId(string.Empty, "traderStan7", Vector3.Zero),
                    "Trader Stan Item 8",
                    inventory => CanDoTrade8(inventory),
                    ItemType.Five_Gems),
            };
        }
    }
}
