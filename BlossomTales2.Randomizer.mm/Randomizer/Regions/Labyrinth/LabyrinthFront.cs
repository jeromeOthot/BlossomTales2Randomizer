using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace BlossomTales2.Randomizer.mm
{
    public class LabyrinthFront : Region
    {
        public override bool CanAccess(Inventory inventory) => World.OverworldWest.CanAccess(inventory) && inventory.Has3DongeonKeys;

        //accès labyrinthe 17x16: accès labyrinthe 18x16 && teleporter && leviers
        public Predicate<Inventory> CanAccessLabyrinthe17x16 => inventory => CanAccess(inventory) && inventory.HasRexTeleporter && inventory.CanSwitchLevers;

        //accès labyrinthe 18x16: accès labyrinthe && grappin && lanterne
        public Predicate<Inventory> CanAccessLabyrinthe18x16 => inventory => CanAccess(inventory) && inventory.HasGrappleHook && inventory.HasTorch;

        //accès labyrinthe 17x18: ...

        public LabyrinthFront(World world) : base("Labyrinth Front", world)
        {
            Locations = new List<Location>
            {
                //chests
                { new Location(new LocationId("overworld-15x18.tmx", "Chest", new Vector3(1856f, 0f, 1408f)),  "mini-boss mirror shield", (inventory) => true,ItemType.Shield) }, //accès labyrinthe

                { new Location(new LocationId("labyrinth-forge.tmx", "golemHead", new Vector3(0f, 0f, 0f)), "golem head forge", (inventory) => true && inventory.NbBlueGem >= 50, ItemType.Sword) }, //accès labyrinthe && blue gem == 50

                { new Location(new LocationId("overworld-15x18-cave.tmx", "Chest_Small", new Vector3(384f, 0f, 268f)), "bombable south cave chest down left", (inventory) => inventory.HasBombs, ItemType.GoldCoin) }, //accès labyrinthe back && bombes
                { new Location(new LocationId("overworld-15x18-cave.tmx", "Chest_Small", new Vector3(576f, 0f, 268f)), "bombable south cave chest down right", (inventory) => inventory.HasBombs, ItemType.GoldCoin) }, //accès labyrinthe back && bombes
                { new Location(new LocationId("overworld-15x18-cave.tmx", "Chest_Small", new Vector3(384f, 0f, 456f)), "bombable south cave chest up left", (inventory) => inventory.HasBombs, ItemType.GoldCoin) }, //accès labyrinthe back && bombes
                { new Location(new LocationId("overworld-15x18-cave.tmx", "Chest_Small", new Vector3(572f, 0f, 456f)), "bombable south cave chest down right", (inventory) => inventory.HasBombs, ItemType.GoldCoin) }, //accès labyrinthe back && bombes

                { new Location(new LocationId("overworld-16x17.tmx", "Chest_Small", new Vector3(1576f, 0f, 280f)), "village other side wall chest", (inventory) => CanAccessLabyrinthe17x16(inventory), ItemType.GoldCoin) }, //accès labyrinthe 17x16

                { new Location(new LocationId("overworld-16x18.tmx", "Chest_Small", new Vector3(176f, 0f, 1452f)), "south village chest", (inventory) => true, ItemType.GoldCoin) }, //accès labyrinthe

                { new Location(new LocationId("overworld-17x16.tmx", "Chest_Small", new Vector3(2376f, 0f, 868f)), "blue tree right chest", (inventory) => true, ItemType.GoldCoin) }, //accès labyrinthe 17x16
                { new Location(new LocationId("overworld-17x16-cave.tmx", "Chest_Small", new Vector3(512f, 0f, 268f)), "bombable north cave chest down left", (inventory) => CanAccessLabyrinthe17x16(inventory) && inventory.HasBombs, ItemType.GoldCoin) }, //accès labyrinthe 17x16 && bombes
                { new Location(new LocationId("overworld-17x16-cave.tmx", "Chest_Small", new Vector3(672f, 0f, 268f)), "bombable north cave chest down center", (inventory) => CanAccessLabyrinthe17x16(inventory) && inventory.HasBombs, ItemType.GoldCoin) }, //accès labyrinthe 17x16 && bombes
                { new Location(new LocationId("overworld-17x16-cave.tmx", "Chest_Small", new Vector3(832f, 0f, 268f)), "bombable north cave chest down right", (inventory) => CanAccessLabyrinthe17x16(inventory) && inventory.HasBombs, ItemType.GoldCoin) }, //accès labyrinthe 17x16 && bombes
                { new Location(new LocationId("overworld-17x16-cave.tmx", "Chest_Small", new Vector3(512f, 0f, 456f)), "bombable north cave chest up left", (inventory) => CanAccessLabyrinthe17x16(inventory) && inventory.HasBombs, ItemType.GoldCoin) }, //accès labyrinthe 17x16 && bombes
                { new Location(new LocationId("overworld-17x16-cave.tmx", "Chest_Small", new Vector3(672f, 0f, 456f)), "bombable north cave chest up center", (inventory) => CanAccessLabyrinthe17x16(inventory) && inventory.HasBombs, ItemType.GoldCoin) }, //accès labyrinthe 17x16 && bombes
                { new Location(new LocationId("overworld-17x16-cave.tmx", "Chest_Small", new Vector3(828f, 0f, 456f)), "bombable north cave chest up right", (inventory) => CanAccessLabyrinthe17x16(inventory) && inventory.HasBombs, ItemType.GoldCoin) }, //accès labyrinthe 17x16 && bombes

                { new Location(new LocationId("overworld-17x18.tmx", "Chest_Small", new Vector3(1308f, 0f, 224f)), "left blue tree chest", (inventory) => true, ItemType.GoldCoin) }, //accès labyrinthe 17x18

                { new Location(new LocationId("overworld-18x17-cave.tmx", "Chest_Small", new Vector3(384f, 0f, 268f)), "entrance bombable cave chest left", (inventory) => inventory.HasBombs, ItemType.GoldCoin) }, //accès labyrinthe && bombes
                { new Location(new LocationId("overworld-18x17-cave.tmx", "Chest_Small", new Vector3(480f, 0f, 268f)), "entrance bombable cave chest center", (inventory) => inventory.HasBombs, ItemType.GoldCoin) }, //accès labyrinthe && bombes
                { new Location(new LocationId("overworld-18x17-cave.tmx", "Chest_Small", new Vector3(576f, 0f, 268f)), "entrance bombable cave chest right", (inventory) => inventory.HasBombs, ItemType.GoldCoin) }, //accès labyrinthe && bombes
            };
        }
    }
}
