using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace BlossomTales2.Randomizer.mm.Canyon
{
    public class CanyonNorthRegion: Region
    {
        public override bool CanAccess(Inventory inventory) => World.OverworldWest.CanAccess(inventory) && (inventory.HasBow || inventory.HasGrappleHook);

        public CanyonNorthRegion(World world) : base("Canyon North", world)
        {
            Locations = new List<Location>
            {
                //Caves
                new Location(new LocationId("overworld-15x19-cave.tmx", "Chest_Small", new Vector3(544f, 0f, 368f)),
                    "Bomb Cave Top Left Chest",
                    inventory => inventory.HasBombs,
                    ItemType.GoldCoin),
                new Location(new LocationId("overworld-15x19-cave.tmx", "Chest_Small", new Vector3(800f, 0f, 364f)),
                    "Bomb Cave Top Right Chest",
                    inventory => inventory.HasBombs,
                    ItemType.GoldCoin),
                new Location(new LocationId("overworld-15x19-cave.tmx", "Chest_Small", new Vector3(480f, 0f, 492f)),
                    "Bomb Cave Middle Left Chest",
                    inventory => inventory.HasBombs,
                    ItemType.GoldCoin),
                new Location(new LocationId("overworld-15x19-cave.tmx", "Chest_Small", new Vector3(864f, 0f, 488f)),
                    "Bomb Cave Middle Right Chest",
                    inventory => inventory.HasBombs,
                    ItemType.GoldCoin),
                new Location(new LocationId("overworld-15x19-cave.tmx", "Chest_Small", new Vector3(576f, 0f, 616f)),
                    "Bomb Cave Bottom Left Chest",
                    inventory => inventory.HasBombs,
                    ItemType.GoldCoin),
                new Location(new LocationId("overworld-15x19-cave.tmx", "Chest_Small", new Vector3(768f, 0f, 616f)),
                    "Bomb Cave Bottom Right Chest",
                    inventory => inventory.HasBombs,
                    ItemType.GoldCoin),
                //Os
                new Location(new LocationId("overworld-15x19.tmx", "PickUpItem", new Vector3(820f, 0f, 952f)),
                    "Lizard Statue Bone",
                    _ => true,
                    ItemType.CanyonBone),
                new Location(new LocationId("overworld-15x19.tmx", "PickUpItem", new Vector3(1804f, 0f, 2292f)),
                    "Three Rocks Bone",
                    _ => true,
                    ItemType.CanyonBone),
                new Location(new LocationId("overworld-15x20.tmx", "PickUpItem", new Vector3(192f, 0f, 416f)),
                    "Archaeological Site Bone",
                    _ => true,
                    ItemType.CanyonBone),
                new Location(new LocationId("overworld-16x19.tmx", "PickUpItem", new Vector3(992f, 0f, 1636f)),
                    "Sword Master Dojo Bone",
                    _ => true,
                    ItemType.CanyonBone),
                //NPC
                new Location(new LocationId("overworld-15x20.tmx", "archCanyon", Vector3.Zero),
                    "Archaeologist Reward",
                    inventory => inventory.CanyonBonesCount >= 20,
                    ItemType.Crystal),
            };
        }
    }
}
