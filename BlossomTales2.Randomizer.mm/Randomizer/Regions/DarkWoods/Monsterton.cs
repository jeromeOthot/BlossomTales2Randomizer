using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace BlossomTales2.Randomizer.mm
{
    public class Monsterton : Region
    {
        public override bool CanAccess(Inventory inventory) => World.DarkWoodsFront.CanAccess(inventory)
                                                               && (inventory.CanCraftPotion(ItemType.Jar_Ghost) || inventory.HasBoomerang);

        public Monsterton(World world) : base("Monsterton", world)
        {
            Locations = new List<Location>
            {
                //Side quest
                new Location(new LocationId("darklands-house4.tmx", "sickZombie", new Vector3(352f, 0f, 268f)),
                    "Sick Zombie Reward",
                    inventory => inventory.CanCraftPotion(ItemType.Jar_Fire),
                    ItemType.HeartQ_1,
                    LocationPool.Sidequests),
                //NPC
                new Location(new LocationId("darklands-house2-floor2.tmx", "bard_song", Vector3.Zero),
                    "Shop Bard Song",
                    _ => true,
                    ItemType.WakeUp,
                    LocationPool.FreeGifts),
                //Shops
                new Location(new LocationId("darklands-house2-shop.tmx", "left", Vector3.Zero),
                    "Shop Left Item",
                    _ => true,
                    ItemType.Jar_Empty,
                    LocationPool.Shops),
                new Location(new LocationId("darklands-house2-shop.tmx", "center", Vector3.Zero),
                    "Shop Middle Item",
                    _ => true,
                    ItemType.Crystal,
                    LocationPool.Shops),
                new Location(new LocationId("darklands-house2-shop.tmx", "right", Vector3.Zero),
                    "Shop Right Item",
                    _ => true,
                    ItemType.HeartQ_1,
                    LocationPool.Shops),
                //Chest
                new Location(new LocationId("overworld-23x17.tmx", "Chest_Small", new Vector3(2176f, 0f, 704f)),
                    "Ledge Chest",
                    _ => true,
                    ItemType.GoldCoin),
                new Location(new LocationId("overworld-24x17.tmx", "Chest_Small", new Vector3(352f, 0f, 244f)),
                    "Ghost Buried Chest",
                    inventory => inventory.CanCraftPotion(ItemType.Jar_Ghost) && inventory.HasShovel,
                    ItemType.Crystal,
                    LocationPool.Sidequests),
                new Location(new LocationId("darklands-house2-floor2.tmx", "Chest_Small", new Vector3(284f, 0f, 496f)),
                    "Shop 2nd Floor Chest",
                    _ => true,
                    ItemType.GoldCoin),
                new Location(new LocationId("darklands-house6.tmx", "Chest_Small", new Vector3(264f, 0f, 428f)),
                    "South House Chest",
                    _ => true,
                    ItemType.GoldCoin),
                new Location(new LocationId("darklands-house7.tmx", "Chest_Small", new Vector3(320f, 0f, 380f)),
                    "North House Table Chest",
                    _ => true,
                    ItemType.GoldCoin),
            };
        }
    }
}
