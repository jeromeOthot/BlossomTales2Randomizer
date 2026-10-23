using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace BlossomTales2.Randomizer.mm
{
    public class DarkWoodsFront : Region
    {
        public override bool CanAccess(Inventory inventory) => World.OverworldNorth.CanAccess(inventory) && inventory.CanDoDamage;

        public DarkWoodsFront(World world) : base("Periwinkle Woods Front", world)
        {
            Locations = new List<Location>
            {
                //Note Caves
                new Location(new LocationId("overworld-23x17-noteCave.tmx", "Chest_Small", new Vector3(576f, 0f, 260f)),
                    "Northern Note Cave Left Chest",
                    inventory => inventory.CanOpenNoteDoor && inventory.HasBombs && inventory.HasSword,
                    ItemType.GoldCoin),
                new Location(new LocationId("overworld-23x17-noteCave.tmx", "Chest_Small", new Vector3(768f, 0f, 260f)),
                    "Northern Note Cave Right Chest",
                    inventory => inventory.CanOpenNoteDoor && inventory.HasBombs && inventory.HasSword,
                    ItemType.Five_Gems),
                new Location(new LocationId("jungles-23x19-cave.tmx", "Chest_Small", new Vector3(416f, 0f, 896f)),
                    "Southern Note Cave Chest",
                    inventory => inventory.CanOpenNoteDoor && inventory.CanSwitchLevers,
                    ItemType.Crystal),
                //Caves
                new Location(new LocationId("overworld-23x16-cave.tmx", "Chest_Small", new Vector3(736f, 0f, 224f)),
                    "Owl Tree Area Cave Chest",
                    inventory => inventory.HasBow,
                    ItemType.HeartQ_1),
                new Location(new LocationId(
                    "jungles-24x19-cave.tmx", "Chest_Small", new Vector3(288f, 0f, 416f)),
                    "Bomb Cave Top Left Chest",
                    inventory => inventory.HasBombs,
                    ItemType.GoldCoin),
                new Location(new LocationId("jungles-24x19-cave.tmx", "Chest_Small", new Vector3(416f, 0f, 416f)),
                    "Bomb Cave Top Right Chest",
                    inventory => inventory.HasBombs,
                    ItemType.GoldCoin),
                new Location(new LocationId("jungles-24x19-cave.tmx", "Chest_Small", new Vector3(288f, 0f, 272f)),
                    "Bomb Cave Bottom Left Chest",
                    inventory => inventory.HasBombs,
                    ItemType.GoldCoin),
                new Location(new LocationId("jungles-24x19-cave.tmx", "Chest_Small", new Vector3(416f, 0f, 272f)),
                    "Bomb Cave Bottom Right Chest",
                    inventory => inventory.HasBombs,
                    ItemType.GoldCoin),
                new Location(new LocationId("overworld-25x18-cave.tmx", "Chest_Small", new Vector3(640f, 0f, 256f)),
                    "River Cave Left Chest",
                    inventory => (inventory.HasBombs && inventory.HasGrappleHook || inventory.HasBoomerang || World.DarkWoodsBack.CanAccess(inventory)) &&inventory.HasFlipper && inventory.HasRexTeleporter,
                    ItemType.GoldCoin),
                new Location(new LocationId("overworld-25x18-cave.tmx", "Chest_Small", new Vector3(732f, 0f, 292f)),
                    "River Cave Middle Chest",
                    inventory => (inventory.HasBombs && inventory.HasGrappleHook || inventory.HasBoomerang || World.DarkWoodsBack.CanAccess(inventory)) &&inventory.HasFlipper && inventory.HasRexTeleporter,
                    ItemType.Honeycomb),
                new Location(new LocationId("overworld-25x18-cave.tmx", "Chest_Small", new Vector3(832f, 0f, 256f)),
                    "River Cave Right Chest",
                    inventory =>(inventory.HasBombs && inventory.HasGrappleHook || inventory.HasBoomerang || World.DarkWoodsBack.CanAccess(inventory)) &&inventory.HasFlipper && inventory.HasRexTeleporter,
                    ItemType.GoldCoin),
                //NPC
                new Location(new LocationId("owlMap.tmx", "owl", Vector3.Zero),
                    "Owl",
                    inventory => inventory.CanWakeUpPeople,
                    ItemType.Boomerang,
                    LocationPool.FreeGifts),
                new Location(new LocationId("overworld-23x17-farm.tmx", "farmer", Vector3.Zero),
                    "Farmer Reward",
                    inventory => inventory.CanDoDamage,
                    ItemType.HeartQ_1,
                    LocationPool.Sidequests),
                //UFO
                new Location(new LocationId("ufo.tmx", "aliens", new Vector3(0f, 0f, 0f)),
                    "Ufo Reward",
                    _ => true,
                    ItemType.Crystal,
                    LocationPool.Sidequests),
                //Minigame
                new Location(new LocationId("overworld-24x18.tmx", "campCups", Vector3.Zero),
                    "Minigame Camp Cups Prize",
                    _ => true,
                    ItemType.Crystal),
                new Location(new LocationId("overworld-23x18.tmx", "Chest_Small", new Vector3(932f, 0f, 752f)),
                    "Singing Ghosts Minigame",
                    inventory => inventory.CanSwitchLevers,
                    ItemType.HeartQ_1),
                //Chest
                new Location(new LocationId("overworld-23x16.tmx", "Chest_Small", new Vector3(2272f, 0f, 1364f)),
                    "Honeycomb Chest",
                    _ => true,
                    ItemType.Honeycomb),
                new Location(new LocationId("overworld-23x17-farm.tmx", "Chest_Small", new Vector3(752f, 0f, 140f)),
                    "Farm House Chest",
                    _ => true,
                    ItemType.GoldCoin),
                new Location(new LocationId("overworld-24x18-blueTent.tmx", "Chest_Small", new Vector3(776f, 0f, 160f)),
                    "Blue Tent Chest",
                    inventory => true,
                    ItemType.GoldCoin),
                new Location(new LocationId("overworld-24x18-greenTent.tmx", "Chest_Small", new Vector3(576f, 0f, 148f)),
                    "Green Tent Left Chest",
                    inventory => true,
                    ItemType.GoldCoin),
                new Location(new LocationId("overworld-24x18-greenTent.tmx", "Chest_Small", new Vector3(680f, 0f, 148f)),
                    "Green Tent Right Chest",
                    inventory => true,
                    ItemType.GoldCoin),
            };
        }
    }
}
