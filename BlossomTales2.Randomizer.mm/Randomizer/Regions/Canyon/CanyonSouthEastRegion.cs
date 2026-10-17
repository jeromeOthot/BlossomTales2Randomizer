using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace BlossomTales2.Randomizer.mm.Canyon
{
    public class CanyonSouthEastRegion : Region
    {
        public override bool CanAccess(Inventory inventory) => World.Dustwood.CanAccess(inventory);


        public CanyonSouthEastRegion(World world) : base("Canyon Southeast", world)
        {
            Locations = new List<Location>
            {
                //Npc
                new Location(new LocationId("overworld-17x22.tmx", "ghostCanyon", Vector3.Zero),
                    "Ghost",
                    inventory => inventory.CanCraftPotion(ItemType.Jar_Ghost) && (inventory.IngredGemCount >= 10 || inventory.HasShovel),
                    ItemType.Crystal),
                //0S
                new Location(new LocationId("overworld-17x21.tmx", "PickUpItem", new Vector3(412f, 0f, 2164f)),
                    "Witch Area Bone",
                    _ => true,
                    ItemType.CanyonBone),
                new Location(new LocationId("overworld-17x22.tmx", "PickUpItem", new Vector3(1952f, 0f, 864f)),
                    "Cemetery Bone",
                    _ => true,
                    ItemType.CanyonBone),
                new Location(new LocationId("overworld-18x22.tmx", "PickUpItem", new Vector3(1864f, 0f, 1940f)),
                    "Lizard Statue Mountain Bone",
                    _ => true,
                    ItemType.CanyonBone),
            };
        }
    }
}
