using System;

namespace BlossomTales2.Randomizer.mm
{
    public class Monsterton : Region
    {
        public override Predicate<Inventory> CanAccess => inventory => World.DarkWoodsFront.CanAccess(inventory)
                                                                       && (inventory.CanCraftPotion(ItemType.Jar_Ghost) || inventory.HasBoomerang);

        public Monsterton(World world) : base("Monsterton", world)
        {
            //Chest
          //  { new LocationId("overworld-23x17.tmx", "Chest_Small", new Vector3(2176f, 0f, 704f)), new ItemData(ItemType.GoldCoin) }, //accès monsterton

        }
    }
}
