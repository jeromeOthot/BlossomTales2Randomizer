using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace BlossomTales2.Randomizer.mm
{
    public class DarkWoodsBack : Region
    {
        public override bool CanAccess(Inventory inventory) => World.Monsterton.CanAccess(inventory);

        public DarkWoodsBack(World world) : base("Periwinkle Woods Back", world)
        {
            Locations = new List<Location>
            {
                //mausoleum
                { new Location(new LocationId("overworld-24x16-mausoleum.tmx", "Chest_Small", new Vector3(736f, 0f, 200f)), "mausoleum chest", (inventory) => true,  ItemType.HeartQ_1) }, //accès monsterton
                { new Location(new LocationId("overworld-25x17-combat.tmx", "Chest_Small", new Vector3(1280f, 0f, 516f)), "combat area chest", (inventory) => inventory.HasRexTeleporter, ItemType.CombatScroll) }, //accès monsterton && (bombes || teleporter)

                //Chest
                { new Location(new LocationId("overworld-25x16-cave.tmx", "Chest_Small", new Vector3(416f, 0f, 304f)), "cave beside mansion chest south left", (inventory) => inventory.HasBombs, ItemType.GoldCoin) }, //accès mansion && bombes
                { new Location(new LocationId("overworld-25x16-cave.tmx", "Chest_Small", new Vector3(544f, 0f, 304f)), "cave beside mansion chest south right", (inventory) => inventory.HasBombs, ItemType.GoldCoin) }, //accès mansion && bombes
                { new Location(new LocationId("overworld-25x16-cave.tmx", "Chest_Small", new Vector3(352f, 0f, 432f)), "cave beside mansion chest north left", (inventory) => inventory.HasBombs, ItemType.GoldCoin) }, //accès mansion && bombes
                { new Location(new LocationId("overworld-25x16-cave.tmx", "Chest_Small", new Vector3(608f, 0f, 432f)), "cave beside mansion chest north right", (inventory) => inventory.HasBombs, ItemType.GoldCoin) }, //accès mansion && bombes

                { new Location(new LocationId("overworld-25x17-cave.tmx", "Chest_Small", new Vector3(320f, 0f, 352f)), "south purple merchant cave chest left", (inventory) => true, ItemType.GoldCoin) }, //accès monsterton
                { new Location(new LocationId("overworld-25x17-cave.tmx", "Chest_Small", new Vector3(544f, 0f, 352f)), "south purple merchant cave chest center", (inventory) => true, ItemType.GoldCoin) }, //accès monsterton
                { new Location(new LocationId("overworld-25x17-cave.tmx", "Chest_Small", new Vector3(772f, 0f, 352f)), "south purple merchant cave chest right", (inventory) => true, ItemType.GoldCoin) }, //accès monsterton

                { new Location(new LocationId("overworld-25x18-cave.tmx", "Chest_Small", new Vector3(640f, 0f, 256f)), "cave river chest left", (inventory) => inventory.HasFlipper && inventory.HasRexTeleporter, ItemType.GoldCoin) }, //(accès dark || accès monsterton) && flippers && teleporter
                { new Location(new LocationId("overworld-25x18-cave.tmx", "Chest_Small", new Vector3(732f, 0f, 292f)), "cave river chest center", (inventory) => inventory.HasFlipper && inventory.HasRexTeleporter, ItemType.Honeycomb) }, //(accès dark || accès monsterton) && flippers && teleporter
                { new Location(new LocationId("overworld-25x18-cave.tmx", "Chest_Small", new Vector3(832f, 0f, 256f)), "cave river chest right", (inventory) => inventory.HasFlipper && inventory.HasRexTeleporter, ItemType.GoldCoin) }, //(accès dark || accès monsterton) && flippers && teleporter

            };
        }
    }
}
