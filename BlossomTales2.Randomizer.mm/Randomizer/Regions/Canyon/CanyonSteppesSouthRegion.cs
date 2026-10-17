using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace BlossomTales2.Randomizer.mm.Canyon
{
    public class CanyonSteppesSouthRegion : Region
    {
        public override bool CanAccess(Inventory inventory) => World.CanyonSouthEast.CanAccess(inventory) && (inventory.HasGrappleHook || inventory.HasBoomerang);

        public CanyonSteppesSouthRegion(World world) : base("Canyon Steppes South", world)
        {
            Locations = new List<Location>
            {
                //Note Caves
                new Location(new LocationId("overworld-17x21-cave.tmx", "Chest_Small", new Vector3(416f, 0f, 480f)),
                    "Note Cave Chest",
                    inventory => inventory.HasBoomerang || inventory.CanOpenNoteDoor && inventory.CanSwitchLevers,
                    ItemType.HeartQ_1),
                //Os
                new Location(new LocationId("overworld-18x21.tmx", "PickUpItem", new Vector3(160f, 0f, 736f)),
                    "Ledge Bone",
                    _ => true,
                    ItemType.CanyonBone),
                //Chest
                new Location(new LocationId("overworld-17x20.tmx", "Chest_Small", new Vector3(1056f, 0f, 2008f)),
                    "North Ledge Left Chest",
                    _ => true,
                    ItemType.GoldCoin),
                new Location(new LocationId("overworld-17x20.tmx", "Chest_Small", new Vector3(1184f, 0f, 2008f)),
                    "North Ledge Right Chest",
                    _ => true,
                    ItemType.GoldCoin),
                new Location(new LocationId("overworld-18x21.tmx", "Chest_Small", new Vector3(224f, 0f, 592f)),
                    "Honeycomb Chest",
                    _ => true,
                    ItemType.Honeycomb),
                new Location(new LocationId("overworld-18x21.tmx", "Chest_Small", new Vector3(784, 0f, 156)),
                    "Tree Chest",
                    _ => true,
                    ItemType.GoldCoin),
            };
        }
    }
}
