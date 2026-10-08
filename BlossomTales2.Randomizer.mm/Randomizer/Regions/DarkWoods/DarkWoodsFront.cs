using System;
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
                //NPC
                { new Location(new LocationId("owlMap.tmx", "owl", new Vector3(0f, 0f, 0f)), "owl gift", (inventory) => inventory.CanWakeUpPeople, ItemType.Boomerang) }, //accès dark && instrument && chanson wakeup
                { new Location(new LocationId("overworld-23x17-farm.tmx", "farmer", new Vector3(0f, 0f, 0f)), "farmer award", (inventory) => inventory.CanDoDamage,ItemType.HeartQ_1) }, //accès dark && damage

                //UFO
                { new Location(new LocationId("ufo.tmx", "aliens", new Vector3(0f, 0f, 0f)), "ufo award", (inventory) => true,ItemType.Crystal) }, //accès dark

                //Minigame
                { new Location(new LocationId("overworld-24x18.tmx", "campCups", new Vector3(0f, 0f, 0f)), "camp cups minigame", (inventory) => true,ItemType.Crystal) }, //accès dark
                { new Location(new LocationId("overworld-23x18.tmx", "Chest_Small", new Vector3(932f, 0f, 752f)), "light ghost mini-game", (inventory) => inventory.CanSwitchLevers, ItemType.HeartQ_1) }, //accès dark && leviers

                //Cave
                { new Location(new LocationId("jungles-23x19-cave.tmx", "Chest_Small", new Vector3(416f, 0f, 896f)), "note cave south chest", (inventory) => inventory.CanOpenNoteDoor && inventory.CanSwitchLevers, ItemType.Crystal) }, //accès dark && ouvrir portes notes && leviers
                { new Location(new LocationId("overworld-23x17-noteCave.tmx", "Chest_Small", new Vector3(576f, 0f, 260f)), "note cave north chest left", (inventory) => inventory.CanOpenNoteDoor && inventory.HasBombs && inventory.HasSword, ItemType.GoldCoin) }, //accès dark && ouvrir portes notes && (bombes && épée)
                { new Location(new LocationId("overworld-23x17-noteCave.tmx", "Chest_Small", new Vector3(768f, 0f, 260f)), "note cave north chest right", (inventory) => inventory.CanOpenNoteDoor && inventory.HasBombs && inventory.HasSword, ItemType.Five_Gems) }, //accès dark && ouvrir portes notes && (bombes && épée)


                //Chest
                { new Location(new LocationId("overworld-23x16.tmx", "Chest_Small", new Vector3(2272f, 0f, 1364f)), "honeycomb left statue", (inventory) => true, ItemType.Honeycomb) }, //accès dark
                { new Location(new LocationId("overworld-23x16-cave.tmx", "Chest_Small", new Vector3(736f, 0f, 224f)), "north cave chest", (inventory) => inventory.HasBow,  ItemType.HeartQ_1) }, //accès dark && arc
                { new Location(new LocationId("overworld-23x17-farm.tmx", "Chest_Small", new Vector3(752f, 0f, 140f)), "farm chest", (inventory) => true, ItemType.GoldCoin) }, //accès dark
                { new Location(new LocationId("overworld-24x18-blueTent.tmx", "Chest_Small", new Vector3(776f, 0f, 160f)), "blue tent chest", (inventory) => true, ItemType.GoldCoin) }, //accès dark
                { new Location(new LocationId("overworld-24x18-greenTent.tmx", "Chest_Small", new Vector3(576f, 0f, 148f)), "green tent left", (inventory) => true, ItemType.GoldCoin) }, //accès dark
                { new Location(new LocationId("overworld-24x18-greenTent.tmx", "Chest_Small", new Vector3(680f, 0f, 148f)), "green tent right", (inventory) => true, ItemType.GoldCoin) }, //accès dark

                { new Location(new LocationId("jungles-24x19-cave.tmx", "Chest_Small", new Vector3(288f, 0f, 272f)), "cave south chest down left", (inventory) => inventory.HasBombs, ItemType.GoldCoin) }, //accès dark && bombes
                { new Location(new LocationId("jungles-24x19-cave.tmx", "Chest_Small", new Vector3(416f, 0f, 272f)), "cave south chest down right",(inventory) => inventory.HasBombs,ItemType.GoldCoin) }, //accès dark && bombes
                { new Location(new LocationId("jungles-24x19-cave.tmx", "Chest_Small", new Vector3(288f, 0f, 416f)), "cave north chest up left",(inventory) => inventory.HasBombs, ItemType.GoldCoin) }, //accès dark && bombes
                { new Location(new LocationId("jungles-24x19-cave.tmx", "Chest_Small", new Vector3(416f, 0f, 416f)), "cave north chest up right",(inventory) => inventory.HasBombs, ItemType.GoldCoin) }, //accès dark && bombes
            };
        }
    }
}
