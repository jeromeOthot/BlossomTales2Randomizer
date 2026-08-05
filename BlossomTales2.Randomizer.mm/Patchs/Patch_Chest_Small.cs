using System;
using BlossomTales2.Randomizer.mm;
using Microsoft.Xna.Framework;
using Mono.Cecil;
using Mono.Cecil.Cil;
using MonoMod;
using MonoMod.Cil;
using MonoMod.InlineRT;
using MonoMod.Utils;

namespace BlossomTales2
{
    class patch_Chest_Small : Chest_Small
    {
        public patch_Chest_Small(Vector3 position) : base(position)
        {
        }

        [MonoModIgnore]
        [PatchChestSmallUpdate]
        public extern void Update(GameTime gameTime);

        public extern void orig_OpenDoorGates();

        private void OpenDoorGates()
        {
            bool flag = false;
            for (int i = 0; i < Game1.CurrentLevel.LevelObjects.Count; i++)
            {
                if (Game1.CurrentLevel.LevelObjects[i] is CameraOverrideObject && Game1.CurrentLevel.LevelObjects[i].IDNumber == IDNumber)
                {
                    Game1.CamController.focusCameraOnTarget(new Vector2(Game1.CurrentLevel.LevelObjects[i].Position.X, Game1.CurrentLevel.LevelObjects[i].Position.Z), Game1.CurrentLevel.LevelObjects[i].Velocity.X, Game1.CurrentLevel.LevelObjects[i].Velocity.Y);
                    Game1.CamController.IDNumber = IDNumber;
                    Game1.CamController.OpenBoth = true;
                    flag = true;
                    break;
                }
            }

            //Hack to open locked door in Green Gem chest in Morkla.
            if (flag && IDNumber != 11)
            {
                return;
            }

            bool flag2 = false;
            for (int j = 0; j < Game1.CurrentLevel.LevelObjects.Count; j++)
            {
                if (Game1.CurrentLevel.LevelObjects[j] is DoorGate)
                {
                    Game1.CurrentLevel.LevelObjects[j].Velocity.Y = 0f;
                    flag2 = true;
                }
            }

            if (flag2)
            {
                Game1.Camera.Shake(8f, 0.96f);
            }
        }
    }

    public class ModChestSmall
    {
        public static  void Mod_GiveItem(Chest_Small chest)
        {
            GameLogger.LogInfo(new LocationId(Game1.CurrentLevel.Name, chest.Name, chest.Position).ToString() + " IDNumber = " + chest.IDNumber);
            if (RandomizerSingleton.Instance.TryGiveItemAtLocation(chest.Name, chest.Position))
            {
                HandleSpecialChests(chest);
            }
            else //Conserver le comportement de base si le chest n'est pas dans la liste.
            {
                if (chest.IDNumber == 0)
                {
                    int num3 = Game1.RandomNumber.Next(20, 26);
                    for (int k = 0; k < num3; k++)
                    {
                        Vector3 velocity = new Vector3(patch_Game1.RandomFloat(-60, 60, 10f), patch_Game1.RandomFloat(40, 70, 10f), patch_Game1.RandomFloat(30, 60, 10f));
                        Game1.CurrentLevel.LevelObjects.Add(new Coin_PU(chest.Position, velocity));
                    }

                    if (Game1.LevelName == "blossom-house4.tmx" && chest.Position == new Vector3(408f, 0f, 340f))
                    {
                        Game1.Globals.BlossomHouse4Chest = 2;
                    }
                }
                else if (chest.IDNumber == 1)
                {
                    Game1.player.GiveItemReflection(EquipableItem.ItemList.Gold_Key);
                    if (Game1.LevelName == "mansion-4.tmx")
                    {
                        Game1.Globals.Mansion4Chest = 2;
                    }
                    else if (Game1.LevelName == "mansion-16.tmx")
                    {
                        Game1.Globals.Mansion16Chest = 2;
                    }
                    else if (Game1.LevelName == "mansion-20.tmx")
                    {
                        Game1.Globals.Mansion20Chest = 2;
                    }
                    else if (Game1.LevelName == "castle-7.tmx")
                    {
                        Game1.Globals.Castle7Chest = 2;
                        chest.opendoors = true;
                        chest.IDNumber = 4;
                    }
                    else if (Game1.LevelName == "morkla-8.tmx")
                    {
                        Game1.Globals.Morkla8Chest = 2;
                    }
                }
                else if (chest.IDNumber == 2)
                {
                    Game1.player.GiveItemReflection(EquipableItem.ItemList.Gold_Key);
                    chest.opendoors = true;
                }
                else if (chest.IDNumber == 3)
                {
                    Game1.player.GiveItemReflection(EquipableItem.ItemList.HeartQ_1);
                    if (Game1.LevelName == "overworld-23x18.tmx")
                    {
                        Game1.Globals.Ow23x18Chest = 2;
                    }
                }
                else if (chest.IDNumber == 4)
                {
                    Game1.player.GiveItemReflection(EquipableItem.ItemList.Crystal);
                }
                else if (chest.IDNumber == 5)
                {
                    Game1.player.GiveItemReflection(EquipableItem.ItemList.Five_Gems);
                }
                else if (chest.IDNumber == 10)
                {
                    Game1.player.GiveItemReflection(EquipableItem.ItemList.BlueGem);
                    Game1.Globals.foundBlueGem = true;
                }
                else if (chest.IDNumber == 11)
                {
                    Game1.player.GiveItemReflection(EquipableItem.ItemList.GreenGem);
                    Game1.Globals.foundGreenGem = true;
                }
                else if (chest.IDNumber == 13)
                {
                    Game1.player.GiveItemReflection(EquipableItem.ItemList.CombatScroll);
                }
                else if (chest.IDNumber == 14)
                {
                    Game1.player.GiveItemReflection(EquipableItem.ItemList.Honeycomb);
                    Game1.Gui.AddGuiTicker(EquipableItem.IngredientList.HoneycombOLD, Game1.player.Count_Honeycombs);
                }
                else if (chest.IDNumber == 20)
                {
                    Game1.player.GiveItemReflection(EquipableItem.ItemList.HeartQ_1);
                    Game1.Globals.sleepyManState = 3;
                }
                else if (chest.IDNumber == 21)
                {
                    Game1.player.GiveItemReflection(EquipableItem.ItemList.HeartQ_1);
                    Game1.Globals.Lighthouse_State = 5;
                }
                else if (chest.IDNumber == 22)
                {
                    Game1.player.GiveItemReflection(EquipableItem.ItemList.Crystal);
                    Game1.Globals.darklandsGhostTreasureState = 3;
                }
                else if (chest.IDNumber == 30)
                {
                    int num4 = Game1.RandomNumber.Next(30, 40);
                    for (int l = 0; l < num4; l++)
                    {
                        Game1.CurrentLevel.LevelObjects.Add(new Coin_PU(chest.Position, new Vector3(patch_Game1.RandomFloat(-1000, 1000, 100f), patch_Game1.RandomFloat(500, 900, 100f), patch_Game1.RandomFloat(200, 800, 100f))));
                    }
                }
            }
        }

        private static void HandleSpecialChests(Chest_Small chest)
        {
            if (chest.IDNumber == 0)
            {
                if (Game1.LevelName == "blossom-house4.tmx" && chest.Position == new Vector3(408f, 0f, 340f))
                    Game1.Globals.BlossomHouse4Chest = 2;
            }
            else if (chest.IDNumber == 1)
            {
                if (Game1.LevelName == "mansion-4.tmx")
                {
                    Game1.Globals.Mansion4Chest = 2;
                }
                else if (Game1.LevelName == "mansion-16.tmx")
                {
                    Game1.Globals.Mansion16Chest = 2;
                }
                else if (Game1.LevelName == "mansion-20.tmx")
                {
                    Game1.Globals.Mansion20Chest = 2;
                }
                else if (Game1.LevelName == "castle-7.tmx")
                {
                    Game1.Globals.Castle7Chest = 2;
                    chest.opendoors = true;
                    chest.IDNumber = 4;
                }
                else if (Game1.LevelName == "morkla-8.tmx")
                {
                    Game1.Globals.Morkla8Chest = 2;
                }
            }
            else if (chest.IDNumber == 2)
            {
                chest.opendoors = true;
            }
            else if (chest.IDNumber == 3)
            {
                if (Game1.LevelName == "overworld-23x18.tmx")
                    Game1.Globals.Ow23x18Chest = 2;
            }
            else if (chest.IDNumber == 11)
            {
                chest.opendoors = true;
            }
            else if (chest.IDNumber == 20)
            {
                Game1.Globals.sleepyManState = 3;
            }
            else if (chest.IDNumber == 21)
            {
                Game1.Globals.Lighthouse_State = 5;
            }
            else if (chest.IDNumber == 22)
            {
                Game1.Globals.darklandsGhostTreasureState = 3;
            }
        }
    }
}

namespace MonoMod
{
    [MonoModCustomMethodAttribute(nameof(MonoModRules.PatchChestSmallUpdate))]
    class PatchChestSmallUpdateAttribute : Attribute { }

    static partial class MonoModRules
    {
        public static void PatchChestSmallUpdate(ILContext context, CustomAttribute attrib)
        {
            TypeDefinition modChestSmallType = MonoModRule.Modder.FindType("BlossomTales2.ModChestSmall").Resolve();
            MethodDefinition modGiveItemMethod = modChestSmallType.FindMethod("Mod_GiveItem");

            ILCursor cursor = new ILCursor(context);
            //Find
            //if (this.Frame > 2 ...)
            cursor.GotoNext(MoveType.Before,
                instr => instr.MatchLdarg(0),
                instr => instr.MatchLdfld("BlossomTales2.LevelObject", "Frame"),
                instr => instr.MatchLdcI4(2),
                instr => instr.MatchBle(out ILLabel label)
            );
            int endIndex = cursor.Index;
            //Find
            //if (this.IDNumber == 0)
            cursor.GotoPrev(MoveType.Before,
                instr => instr.MatchLdarg(0),
                instr => instr.MatchLdfld("BlossomTales2.LevelObject", "IDNumber"),
                instr => instr.MatchBrtrue(out ILLabel label)
            );
            int startIndex = cursor.Index;
            //Replace
            //ModChestSmall.Mod_GiveItem(this)
            cursor.RemoveRange(endIndex - startIndex);
            cursor.Emit(OpCodes.Ldarg_0);
            cursor.Emit(OpCodes.Call, modGiveItemMethod);
        }
    }
}
