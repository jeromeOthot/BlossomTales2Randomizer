using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace BlossomTales2.Randomizer.mm.Canyon
{
    public class CanyonSouthWestRegion: Region
    {
        public override bool CanAccess(Inventory inventory) => World.CanyonNorth.CanAccess(inventory);

        public CanyonSouthWestRegion(World world) : base("Canyon Southwest", world)
        {
            Locations = new List<Location>
            {
                //Note Caves
                new Location(new LocationId("overworld-15x22-cave.tmx", "Chest_Small", new Vector3(1312f, 0f, 312f)),
                    "Note Cave Top Chest",
                    inventory => inventory.CanOpenNoteDoor && inventory.CanSwitchLevers,
                    ItemType.GoldCoin),
                new Location(new LocationId("overworld-15x22-cave.tmx", "Chest_Small", new Vector3(1216f, 0f, 448f)),
                    "Note Cave Bottom Left Chest",
                    inventory => inventory.CanOpenNoteDoor && inventory.CanSwitchLevers,
                    ItemType.GoldCoin),
                new Location(new LocationId("overworld-15x22-cave.tmx", "Chest_Small", new Vector3(1408f, 0f, 448f)),
                    "Note Cave Bottom Right Chest",
                    inventory => inventory.CanOpenNoteDoor && inventory.CanSwitchLevers,
                    ItemType.GoldCoin),
                //Caves
                new Location(new LocationId("overworld-15x21-cave.tmx", "Chest_Small", new Vector3(352f, 0f, 412f)),
                    "Bomb Cave Top Left Chest",
                    inventory => inventory.HasBombs,
                    ItemType.GoldCoin),
                new Location(new LocationId("overworld-15x21-cave.tmx", "Chest_Small", new Vector3(608f, 0f, 408f)),
                    "Bomb Cave Top Right Chest",
                    inventory => inventory.HasBombs,
                    ItemType.GoldCoin),
                new Location(new LocationId("overworld-15x21-cave.tmx", "Chest_Small", new Vector3(352f, 0f, 604f)),
                    "Bomb Cave Bottom Left Chest",
                    inventory => inventory.HasBombs,
                    ItemType.GoldCoin),
                new Location(new LocationId("overworld-15x21-cave.tmx", "Chest_Small", new Vector3(608f, 0f, 604f)),
                    "Bomb Cave Bottom Right Chest",
                    inventory => inventory.HasBombs,
                    ItemType.GoldCoin),
                new Location(new LocationId("overworld-16x21-cave.tmx", "Chest_Small", new Vector3(544f, 0f, 228f)),
                    "Random Puzzle Cave Chest",
                    inventory => inventory.CanSwitchLevers,
                    ItemType.HeartQ_1),
                //short sidequests
                new Location(new LocationId("colosseum.tmx", "prize_heart", Vector3.Zero),
                    "Colosseum Prize 1",
                    inventory => inventory.CanDoDamage && (inventory.HasBombs || inventory.HasGrappleHook || inventory.HasFalcon),
                    ItemType.HeartQ_1,
                    LocationPool.Sidequests),
                new Location(new LocationId("colosseum.tmx", "prize_crystal", Vector3.Zero),
                    "Colosseum Prize 2",
                    inventory => inventory.CanDoDamage && (inventory.HasBombs || inventory.HasGrappleHook || inventory.HasFalcon),
                    ItemType.Crystal,
                    LocationPool.Sidequests),
                //Os
                new Location(new LocationId("overworld-15x21.tmx", "PickUpItem", new Vector3(288f, 0f, 1952f)),
                    "Colosseum Bone",
                    _ => true,
                    ItemType.CanyonBone),
                new Location(new LocationId("overworld-15x22.tmx", "PickUpItem", new Vector3(1568f, 0f, 2400f)),
                    "Trader Area Bone",
                    _ => true,
                    ItemType.CanyonBone),
                new Location(new LocationId("overworld-16x21.tmx", "PickUpItem", new Vector3(288f, 0f, 296f)),
                    "North Village Bone",
                    _ => true,
                    ItemType.CanyonBone),
            };
        }
    }
}
