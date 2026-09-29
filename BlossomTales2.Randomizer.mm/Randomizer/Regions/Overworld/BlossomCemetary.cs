using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace BlossomTales2.Randomizer.mm
{
    public class BlossomCemetary : Region
    {
        public override Predicate<Inventory> CanAccess => _ => true;

        public BlossomCemetary(World world) : base("BlossomCemetary", world)
        {
            Locations = new List<Location>()
            {
                new Location(new LocationId("overworld-20x18-cave1.tmx", "Chest_Small", new Vector3(1184f, 0f, 2208f)),
                    "Cave Chest",
                    inventory => inventory.CanCutPegs && inventory.HasTorch,
                    ItemType.HeartQ_1),
                new Location(new LocationId("overworld-20x18.tmx", "lanternGuy", new Vector3(1036f, 0f, 660f)),
                    "Lantern Guy",
                    _ => true,
                    ItemType.Torch),
                new Location(new LocationId("overworld-20x18.tmx", "ghostDrink", Vector3.Zero),
                    "Ghost",
                    inventory => inventory.CanCraftPotion(ItemType.Jar_Ghost) && inventory.CanCraftPotion(ItemType.Jar_Resurrection),
                    ItemType.Crystal)
            };
        }
    }
}
