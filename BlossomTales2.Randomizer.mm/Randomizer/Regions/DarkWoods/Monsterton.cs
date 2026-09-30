using System;

namespace BlossomTales2.Randomizer.mm
{
    public class Monsterton : Region
    {
        public override Predicate<Inventory> CanAccess => inventory => World.DarkWoodsFront.CanAccess(inventory)
                                                                       && (inventory.CanCraftPotion(ItemType.Jar_Ghost) || inventory.HasBoomerang);

        public Monsterton(World world) : base("Monsterton", world)
        {
        }
    }
}
