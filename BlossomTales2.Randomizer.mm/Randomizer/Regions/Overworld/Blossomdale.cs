using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace BlossomTales2.Randomizer.mm
{
    public class Blossomdale : Region
    {
        public override bool CanAccess(Inventory inventory) => true;

        private bool CanDeliverLetter1(Inventory inventory) => true;
        private bool CanDeliverLetter2(Inventory inventory) => CanDeliverLetter1(inventory) && World.Anchortown.CanAccess(inventory);
        private bool CanDeliverLetter3(Inventory inventory) => CanDeliverLetter2(inventory) && World.Dustwood.CanAccess(inventory);
        private bool CanDeliverLetter4(Inventory inventory) => CanDeliverLetter3(inventory) && World.OverworldWest.CanAccess(inventory);
        private bool CanDeliverLetter5(Inventory inventory) => CanDeliverLetter4(inventory) && World.DarkWoodsFront.CanAccess(inventory);
        private bool CanDeliverLetter6(Inventory inventory) => CanDeliverLetter5(inventory) && World.Anchortown.CanAccess(inventory);
        private bool CanDeliverLetter7(Inventory inventory) => CanDeliverLetter6(inventory) && World.CanyonSteppesSouth.CanAccess(inventory);
        private bool CanDeliverLetter8(Inventory inventory) => CanDeliverLetter7(inventory) && World.Monsterton.CanAccess(inventory);
        private bool CanDeliverLetter9(Inventory inventory) => CanDeliverLetter8(inventory) && World.JungleBack.CanAccess(inventory) && inventory.HasFlipper;
        private bool CanDeliverLetter10(Inventory inventory) => CanDeliverLetter9(inventory) && World.Blockburg.CanAccess(inventory);
        private bool CanDeliverLetter11(Inventory inventory) => CanDeliverLetter10(inventory) && World.JungleBack.CanAccess(inventory);
        private bool CanDeliverLetter12(Inventory inventory) => CanDeliverLetter11(inventory) && World.Blockburg.CanAccess(inventory);
        private bool CanDeliverLetter13(Inventory inventory) => CanDeliverLetter12(inventory) && World.OverworldNorth.CanAccess(inventory);
        private bool CanDeliverLetter14(Inventory inventory) => CanDeliverLetter13(inventory) && World.CanyonIsland.CanAccess(inventory);
        private bool CanDeliverLetter15(Inventory inventory) => CanDeliverLetter14(inventory) && World.JungleFront.CanAccess(inventory) && inventory.HasFlipper;
        private bool CanDeliverLetter16(Inventory inventory) => CanDeliverLetter15(inventory) && World.CanyonNorth.CanAccess(inventory);
        private bool CanDeliverLetter17(Inventory inventory) => CanDeliverLetter16(inventory) && World.Monsterton.CanAccess(inventory);


        public Blossomdale(World world) : base("Blossomdale", world)
        {
            Locations = new List<Location>
            {
                //caves
                new Location(new LocationId("overworld-21x20-cave.tmx", "Chest_Small", new Vector3(1192f, 0f, 612f)),
                    "Outskirts Cave Left Chest",
                    _ => true,
                    ItemType.GoldCoin),
                new Location(new LocationId("overworld-21x20-cave.tmx", "Chest_Small", new Vector3(1268f, 0f, 612f)),
                    "Outskirts Cave Right Chest",
                    _ => true,
                    ItemType.GoldCoin),
                //note cave
                new Location(new LocationId("overworld-19x20-noteCave.tmx", "Chest_Small", new Vector3(576f, 0f, 212f)),
                    "Outskirts Note Cave Top Left Chest",
                    inventory => inventory.HasFlipper && inventory.CanOpenNoteDoor && inventory.CanCraftPotion(ItemType.Jar_SlowTime) && inventory.HasTriBow,
                    ItemType.GoldCoin),
                new Location(new LocationId("overworld-19x20-noteCave.tmx", "Chest_Small", new Vector3(768f, 0f, 212f)),
                    "Outskirts Note Cave Top Right Chest",
                    inventory => inventory.HasFlipper && inventory.CanOpenNoteDoor && inventory.CanCraftPotion(ItemType.Jar_SlowTime) && inventory.HasTriBow,
                    ItemType.GoldCoin),
                new Location(new LocationId("overworld-19x20-noteCave.tmx", "Chest_Small", new Vector3(576f, 0f, 340f)),
                    "Outskirts Note Cave Lower Left Chest",
                    inventory => inventory.HasFlipper && inventory.CanOpenNoteDoor && inventory.CanCraftPotion(ItemType.Jar_SlowTime) && inventory.HasTriBow,
                    ItemType.GoldCoin),
                new Location(new LocationId("overworld-19x20-noteCave.tmx", "Chest_Small", new Vector3(768f, 0f, 340f)),
                    "Outskirts Note Cave Lower Right Chest",
                    inventory => inventory.HasFlipper && inventory.CanOpenNoteDoor && inventory.CanCraftPotion(ItemType.Jar_SlowTime) && inventory.HasTriBow,
                    ItemType.GoldCoin),
                //items npc
                new Location(new LocationId("blossom-blacksmith.tmx", "npc21", Vector3.Zero),
                    "Blacksmith",
                    inventory => inventory.HasBeatenMorklaBoss,
                    ItemType.Bow),
                new Location(new LocationId("blossom-lilyHouse.tmx", "Grandma 1", Vector3.Zero),
                    "Grandma 1",
                    _ => true,
                    ItemType.Shield),
                new Location(new LocationId("blossom-lilyHouse.tmx", "Grandma 2", Vector3.Zero),
                    "Grandma 2",
                    _ => true,
                    ItemType.Sword),

                //minigames
                new Location(new LocationId("tent-arrow.tmx", "arrowGame", Vector3.Zero),
                    "Bow Minigame",
                    inventory => inventory.HasBow,
                    ItemType.HeartQ_1),

                //mail
                new Location(new LocationId("blossom-postOffice.tmx", "postal_heart", Vector3.Zero),
                    "PostOffice 1",
                    inventory => CanDeliverLetter11(inventory),
                    ItemType.HeartQ_1),
                new Location(new LocationId("blossom-postOffice.tmx", "postal_falcon", Vector3.Zero),
                    "PostOffice Final",
                    inventory => CanDeliverLetter17(inventory),
                    ItemType.Falcon),

                //shops
                new Location(new LocationId("blossom-shop.tmx", "left", Vector3.Zero),
                    "Shop Left Item",
                    _ => true,
                    ItemType.Jar_Empty),
                new Location(new LocationId("blossom-shop.tmx", "center", Vector3.Zero),
                    "Shop Middle Item",
                    _ => true,
                    ItemType.Crystal),
                new Location(new LocationId("blossom-shop.tmx", "right", Vector3.Zero),
                    "Shop Right Item",
                    _ => true,
                    ItemType.HeartQ_1),

                //bardes
                new Location(new LocationId("blossom-tavern.tmx", "bard_song", Vector3.Zero),
                    "Tavern Bard",
                    inventory => inventory.HasInstrument,
                    ItemType.GrandpaHint),

                //tresors
                new Location(new LocationId("blossom-house1.tmx", "Chest_Small", new Vector3(672f, 0f, 308f)),
                    "South Left House Chest",
                    _ => true,
                    ItemType.GoldCoin),
                new Location(new LocationId("blossom-house2.tmx", "Chest_Small", new Vector3(708f, 0f, 356f)),
                    "South Right House Chest",
                    _ => true,
                    ItemType.GoldCoin),
                new Location(new LocationId("blossom-house3.tmx", "Chest_Small", new Vector3(368f, 0f, 148f)),
                    "Potion House Chest",
                    _ => true,
                    ItemType.GoldCoin),
                new Location(new LocationId("blossom-house4.tmx", "Chest_Small", new Vector3(696f, 0f, 416f)),
                    "North House Table Chest",
                    _ => true,
                    ItemType.GoldCoin),
                new Location(new LocationId("blossom-house4.tmx", "Chest_Small", new Vector3(780f, 0f, 156f)),
                    "North House Fireplace Chest",
                    _ => true,
                    ItemType.GoldCoin),
                new Location(new LocationId("blossom-house4.tmx", "Chest_Small", new Vector3(408f, 0f, 340f)),
                    "North House Torch Chest",
                    inventory => inventory.HasTorch,
                    ItemType.GoldCoin),
                new Location(new LocationId("blossom-tavern-basement.tmx", "Chest_Small", new Vector3(256f, 0f, 272f)),
                    "Tavern Top Left Chest",
                    inventory => inventory.HasBombs,
                    ItemType.GoldCoin),
                new Location(new LocationId("blossom-tavern-basement.tmx", "Chest_Small", new Vector3(256f, 0f, 464f)),
                    "Tavern Top Right Chest",
                    inventory => inventory.HasBombs,
                    ItemType.GoldCoin),
                new Location(new LocationId("blossom-tavern-basement.tmx", "Chest_Small", new Vector3(448f, 0f, 272f)),
                    "Tavern Lower Left Chest",
                    inventory => inventory.HasBombs,
                    ItemType.GoldCoin),
                new Location(new LocationId("blossom-tavern-basement.tmx", "Chest_Small", new Vector3(448f, 0f, 464f)),
                    "Tavern Lower Right Chest",
                    inventory => inventory.HasBombs,
                    ItemType.HeartQ_1),
                new Location(new LocationId("overworld-20x20.tmx", "Chest_Small", new Vector3(2352f, 0f, 2212f)),
                    "Bomb Rock Chest",
                    inventory => inventory.HasBombs,
                    ItemType.GoldCoin),
                new Location(new LocationId("overworld-21x19.tmx", "Chest_Small", new Vector3(340f, 0f, 792f)),
                    "Buried Treasure Chest",
                    inventory => inventory.CanWakeUpPeople && inventory.HasShovel,
                    ItemType.GoldCoin)
            };
        }
    }
}
