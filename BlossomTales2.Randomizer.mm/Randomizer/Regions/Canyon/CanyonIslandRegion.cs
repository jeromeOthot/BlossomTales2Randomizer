using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace BlossomTales2.Randomizer.mm.Canyon
{
    public class CanyonIslandRegion : Region
    {
        public override bool CanAccess(Inventory inventory) => World.CanyonSouthEast.CanAccess(inventory);

        public CanyonIslandRegion(World world) : base("Canyon Island", world)
        {
            Locations = new List<Location>
            {
                //Note Cave
                new Location(new LocationId("overworld-19x22-bardCave.tmx", "Chest_Small", new Vector3(416f, 0f, 240f)),
                    "Bard Note Cave",
                    inventory => inventory.CanDoDamage && inventory.CanOpenNoteDoor,
                    ItemType.HeartQ_1),
                //Caves
                new Location(new LocationId("overworld-18x22-cave.tmx", "Chest_Small", new Vector3(544f, 0f, 344f)),
                    "Water Bomb Cave Top Chest",
                    inventory => inventory.HasFlipper && inventory.HasBombs,
                    ItemType.GoldCoin),
                new Location(new LocationId("overworld-18x22-cave.tmx", "Chest_Small", new Vector3(448f, 0f, 448f)),
                    "Water Bomb Cave Bottom Left Chest",
                    inventory => inventory.HasFlipper &&  inventory.HasBombs,
                    ItemType.GoldCoin),
                new Location(new LocationId("overworld-18x22-cave.tmx", "Chest_Small", new Vector3(640f, 0f, 448f)),
                    "Water Bomb Cave Bottom Right Chest",
                    inventory => inventory.HasFlipper && inventory.HasBombs,
                    ItemType.GoldCoin),
                // NPC Bard
                new Location(new LocationId("overworld-19x22.tmx", "bard", new Vector3(852f, 0f, 1348f)),
                    "Prisoner Bard Reward",
                    inventory => inventory.CanDoDamage,
                    ItemType.Guitar),
                new Location(new LocationId("overworld-19x22.tmx", "bard_song", new Vector3(852f, 0f, 1348f)),
                    "Prisoner Bard Song",
                    inventory => inventory.CanDoDamage,
                    ItemType.OpenSesame),
                //mausoleum
                new Location(new LocationId("overworld-20x22-combat.tmx", "Chest_Small", new Vector3(2176f, 0f, 1828f)),
                    "Combat Mausoleum",
                    inventory => inventory.CanSwitchLevers && inventory.CanDoDamage,
                    ItemType.CombatScroll),
                //0S
                new Location(new LocationId("overworld-19x21.tmx", "PickUpItem", new Vector3(536f, 0f, 2072f)),
                    "Camper Bone",
                    _ => true,
                    ItemType.CanyonBone),
                new Location(new LocationId("overworld-19x22.tmx", "PickUpItem", new Vector3(1568f, 0f, 928f)),
                    "River Access Bone",
                    _ => true,
                    ItemType.CanyonBone),
                new Location(new LocationId("overworld-20x21.tmx", "PickUpItem", new Vector3(800f, 0f, 2208)),
                    "Mausoleum Bone",
                    _ => true,
                    ItemType.CanyonBone),
                new Location(new LocationId("overworld-20x22.tmx", "PickUpItem", new Vector3(688f, 0f, 916f)),
                    "Bard Area Bone",
                    _ => true,
                    ItemType.CanyonBone),
                //Chest
                new Location(new LocationId("overworld-19x21.tmx", "Chest_Small", new Vector3(2348f, 0f, 1832f)),
                    "Camper Chest",
                    _ => true,
                    ItemType.GoldCoin),
            };

        }
    }
}
