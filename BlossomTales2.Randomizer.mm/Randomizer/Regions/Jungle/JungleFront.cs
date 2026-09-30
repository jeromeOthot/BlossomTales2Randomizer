using System;

namespace BlossomTales2.Randomizer.mm
{
    public class JungleFront : Region
    {
        public override Predicate<Inventory> CanAccess => inventory => (World.OverworldEast.CanAccess(inventory)
                                                                       && (inventory.CanCraftPotion(ItemType.Jar_Health) || inventory.HasFlipper))
                                                                       || (World.DarkWoodsFront.CanAccess(inventory) && inventory.HasGrappleHook);

        public JungleFront(World world) : base("Jungle Front", world)
        {

        }
    }
}
