using System;
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
                {
                    new Location(new LocationId("darklands-house4.tmx", "sickZombie", new Vector3(352f, 0f, 268f)),
                        "sick zombie", (inventory) => inventory.HasBottle && World.CanyonNorth.CanAccess(inventory), ItemType.HeartQ_1)
                }, //accès monsterton && bouteille && accès canyon

                //NPC
                {
                    new Location(new LocationId("darklands-house2-floor2.tmx", "bard_song", new Vector3(0f, 0f, 0f)),
                        "bard song", (inventory) => true,ItemType.WakeUp)
                }, //accès monsterton

                //Darklands village shop
                {
                    new Location(new LocationId("darklands-house2-shop.tmx", "left", Vector3.Zero),
                        "shop left", (inventory) => true, ItemType.Jar_Empty)
                },
                {
                    new Location(new LocationId("darklands-house2-shop.tmx", "center", Vector3.Zero),
                        "shop center", (inventory) => true,ItemType.Crystal)
                },
                {
                    new Location(new LocationId("darklands-house2-shop.tmx", "right", Vector3.Zero),
                        "shop right", (inventory) => true,ItemType.HeartQ_1)
                },

                //Chest
                {
                    new Location(new LocationId("overworld-23x17.tmx", "Chest_Small", new Vector3(2176f, 0f, 704f)),
                        "left village chest", (inventory) => true,ItemType.GoldCoin)
                }, //accès monsterton
                {
                    new Location(new LocationId("overworld-24x17.tmx", "Chest_Small", new Vector3(352f, 0f, 244f)),
                        "burried chest", (inventory) => inventory.HasShovel && inventory.HasBottle, ItemType.Crystal)
                }, //accès monsterton && bouteille && pelle  pelle id=3
                {
                    new Location(
                        new LocationId("darklands-house2-floor2.tmx", "Chest_Small", new Vector3(284f, 0f, 496f)),
                        "shop 2nd floor chest", (inventory) => true,ItemType.GoldCoin)
                }, //accès monsterton
                {
                    new Location(new LocationId("darklands-house6.tmx", "Chest_Small", new Vector3(264f, 0f, 428f)),
                        "house north push table chest", (inventory) => true,ItemType.GoldCoin)
                }, //accès monsterton
                {
                    new Location(new LocationId("darklands-house7.tmx", "Chest_Small", new Vector3(320f, 0f, 380f)),
                        "house south chest", (inventory) => true, ItemType.GoldCoin)
                }, //accès monsterton
            };
        }
    }
}
