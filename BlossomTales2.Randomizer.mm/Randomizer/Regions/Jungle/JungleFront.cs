using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace BlossomTales2.Randomizer.mm
{
    public class JungleFront : Region
    {
        public override Predicate<Inventory> CanAccess => inventory => (World.OverworldEast.CanAccess(inventory)
                                                                       && (inventory.CanCraftPotion(ItemType.Jar_Health) || inventory.HasFlipper))
                                                                       || (World.DarkWoodsFront.CanAccess(inventory) && inventory.HasGrappleHook);

        public JungleFront(World world) : base("Jungle Front", world)
        {
            Locations = new List<Location>()
            {
                //Short sidequests
                new Location(new LocationId("jungles-23x19.tmx", "archJungle", Vector3.Zero),
                    "Archeologist",
                    _ => true,
                    ItemType.HeartQ_1),
            };
        }
    }
}
