using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace BlossomTales2.Randomizer.mm
{
    public class Blockburg : Region
    {
        public override bool CanAccess(Inventory inventory) => World.LabyrinthFront.CanAccessLabyrinth17x18(inventory) && inventory.CanSwitchLevers && inventory.CanHitTargets;
        public bool CanAccessCyclops(Inventory inventory) => CanAccess(inventory) && inventory.CanSwitchLevers && inventory.HasGrappleHook;

        public Blockburg(World world) : base("Blockburg", world)
        {
            Locations = new List<Location>
            {
                //Short Sidequests
                new Location(new LocationId("overworld-16x17.tmx", "labSlime", Vector3.Zero),
                    "Sewers",
                    inventory => inventory.CanDoDamage,
                    ItemType.HeartQ_1),
                //Shops
                new Location(new LocationId("labHouse-shop.tmx", "left", Vector3.Zero),
                    "Shop Left Item",
                    _ => true,
                    ItemType.Jar_Empty),
                new Location(new LocationId("labHouse-shop.tmx", "center", Vector3.Zero),
                    "Shop Middle Item",
                    _ => true,
                    ItemType.Crystal),
                new Location(new LocationId("labHouse-shop.tmx", "right", Vector3.Zero),
                    "Shop Right Item",
                    _ => true,
                    ItemType.HeartQ_1),
                //Chests
                new Location(new LocationId("overworld-15x18.tmx", "Chest", new Vector3(1856f, 0f, 1408f)),
                    "Cyclops Reward Chest",
                    inventory => CanAccessCyclops(inventory) && inventory.CanDoDamage,
                    ItemType.Shield),
                new Location(new LocationId("overworld-16x18.tmx", "Chest_Small", new Vector3(176f, 0f, 1452f)),
                    "South of Village Chest",
                    inventory => CanAccessCyclops(inventory),
                    ItemType.GoldCoin),

            };
        }
    }
}
