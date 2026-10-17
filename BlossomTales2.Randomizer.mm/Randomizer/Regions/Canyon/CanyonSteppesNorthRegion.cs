using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace BlossomTales2.Randomizer.mm.Canyon
{
    public class CanyonSteppesNorthRegion : Region
    {
        public override bool CanAccess(Inventory inventory) => World.OverworldWest.CanAccess(inventory) && inventory.HasGrappleHook
                                                               || World.CanyonNorth.CanAccess(inventory) && inventory.HasBoomerang;

        public CanyonSteppesNorthRegion(World world) : base("Canyon Steppes North", world)
        {
            Locations = new List<Location>
            {
                //Caves
                new Location(new LocationId("overworld-17x20-cave.tmx", "Chest_Small", new Vector3(736f, 0f, 224f)),
                    "Falling Tiles Cave Chest",
                    inventory => inventory.HasGrappleHook,
                    ItemType.HeartQ_1),
                //Os
                new Location(new LocationId("overworld-16x19.tmx", "PickUpItem", new Vector3(1616f, 0f, 988f)),
                    "Master Dojo Peg Bone",
                    _ => true,
                    ItemType.CanyonBone),
                new Location(new LocationId("overworld-17x19.tmx", "PickUpItem", new Vector3(796f, 0f, 1076f)),
                    "Wooden Dock Bone",
                    _ => true,
                    ItemType.CanyonBone),
                new Location(new LocationId("overworld-17x19.tmx", "PickUpItem", new Vector3(844f, 0f, 2180f)),
                    "Twin Mounts Bone",
                    _ => true,
                    ItemType.CanyonBone),
                new Location(new LocationId("overworld-18x19.tmx", "PickUpItem", new Vector3(256f, 0f, 1168f)),
                    "Grapplehook Isles Bone",
                    _ => true,
                    ItemType.CanyonBone),
                new Location(new LocationId("overworld-18x20.tmx", "PickUpItem", new Vector3(304f, 0f, 520f)),
                    "Ledge Bone",
                    _ => true,
                    ItemType.CanyonBone),
                //Chest
                new Location(new LocationId("overworld-17x20.tmx", "Chest_Small", new Vector3(476f, 0f, 208f)),
                    "Honeycomb Chest",
                    _ => true,
                    ItemType.Honeycomb),
                new Location(new LocationId("overworld-18x19.tmx", "Chest_Small", new Vector3(792f, 0f, 1348f)),
                    "Grapplehook Isles Honeycomb Chest",
                    inventory => inventory.HasGrappleHook,
                    ItemType.Honeycomb),
            };
        }
    }
}
