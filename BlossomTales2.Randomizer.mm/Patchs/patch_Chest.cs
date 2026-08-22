using System;
using BlossomTales2.Extensions;
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
    internal class patch_Chest : Chest
    {
        public patch_Chest(Vector3 position) : base(position)
        {
        }

        [MonoModIgnore]
        [PatchChestUpdate]
        public extern void Update(GameTime gameTime);
    }

    public class ModChest
    {
        public static void Mod_GiveItem(Chest chest)
        {
            GameLogger.LogInfo(new LocationId(Game1.CurrentLevel.Name, chest.Name, chest.Position).ToString());
            ItemData item = RandomizerSingleton.Instance.GetItemByNameAndLocation(chest.Name, chest.Position);

            if (item != null)
            {
                RandomizerSingleton.Instance.GiveItem(item);
                HandleSpecialChests(chest);
            }
            else
            {
                if (chest.IDNumber == 0)
                {
                    int num3 = Game1.RandomNumber.Next(20, 30);
                    for (int k = 0; k < num3; k++)
                    {
                        Vector3 velocity = new Vector3(patch_Game1.RandomFloat(-60, 60, 10f), patch_Game1.RandomFloat(40, 70, 10f), patch_Game1.RandomFloat(30, 60, 10f));
                        Game1.CurrentLevel.LevelObjects.Add(new Coin_PU(chest.Position, velocity));
                    }
                }
                else if (chest.IDNumber == 1)
                {
                    Game1.player.GiveItemReflection(EquipableItem.ItemList.Gold_Key);
                }
                else if (chest.IDNumber == 2)
                {
                    Game1.player.GiveItemReflection(EquipableItem.ItemList.Gold_Key);
                    chest.opendoors = true;
                }
                else if (chest.IDNumber == 5)
                {
                    Game1.player.GiveItemReflection(EquipableItem.ItemList.HeartQ_1);
                }
                else if (chest.IDNumber == 12)
                {
                    Game1.player.GiveItemReflection(EquipableItem.ItemList.GrappleHook);
                }
                else if (chest.IDNumber == 13)
                {
                    Game1.player.GiveItemReflection(EquipableItem.ItemList.RexTeleporter);
                }
                else if (chest.IDNumber == 14)
                {
                    Game1.player.GiveItemReflection(EquipableItem.ItemList.MirrorShield);
                    if (Game1.Globals.MainQuestObjective < Globaler.MainGameObjective.lab_findCastleEntrance)
                    {
                        Game1.Globals.MainQuestObjective = Globaler.MainGameObjective.lab_findCastleEntrance;
                    }
                }
                else if (chest.IDNumber == 21)
                {
                    Game1.player.RemovePlayerControls = true;
                }
                else if (chest.IDNumber == 22)
                {
                    Game1.player.GiveItemReflection(EquipableItem.ItemList.KeyPiece2);
                    Game1.player.RemovePlayerControls = true;
                    Game1.Achievementer.CheckAchievment(3);
                }
                else if (chest.IDNumber == 23)
                {
                    Game1.player.GiveItemReflection(EquipableItem.ItemList.KeyPiece3);
                    Game1.player.RemovePlayerControls = true;
                    Game1.Achievementer.CheckAchievment(4);
                }
                else if (chest.IDNumber == 27)
                {
                    int num4 = Game1.RandomNumber.Next(30, 50);
                    for (int l = 0; l < num4; l++)
                    {
                        Vector3 velocity2 = new Vector3(patch_Game1.RandomFloat(-60, 60, 10f), patch_Game1.RandomFloat(40, 70, 10f), patch_Game1.RandomFloat(30, 60, 10f));
                        Game1.CurrentLevel.LevelObjects.Add(new Coin_PU(chest.Position, velocity2));
                    }
                }
            }
        }

        private static void HandleSpecialChests(Chest chest)
        {
            if (chest.IDNumber == 2)
            {
                chest.opendoors = true;
            }
            else if (chest.IDNumber == 10)
            {
                if (ModGlobals.OpenWorldState)
                    Game1Extensions.MarkObjectiveComplete(Globaler.MainGameObjective.jungles_getBombs);
                else
                    Game1.Globals.MainQuestObjective = Globaler.MainGameObjective.jungles_talkToMorkla;
            }
            else if (chest.IDNumber == 11)
            {
                Game1Extensions.AddLevelPermaObject(chest.Name, chest.Position);
                if (!Game1.WaterLevelUp)
                {
                    Game1.playSoundCue("blank154");
                    Game1.WaterLevelUp = true;
                }
            }
            else if (chest.IDNumber == 14)
            {
                if (Game1.Globals.MainQuestObjective < Globaler.MainGameObjective.lab_findCastleEntrance)
                {
                    Game1.Globals.MainQuestObjective = Globaler.MainGameObjective.lab_findCastleEntrance;
                }
            }
            else if (chest.IDNumber == 21)
            {
                Game1.player.RemovePlayerControls = true;
            }
            else if (chest.IDNumber == 22)
            {
                Game1.player.RemovePlayerControls = true;
            }
            else if (chest.IDNumber == 23)
            {
                Game1.player.RemovePlayerControls = true;
            }
        }
    }
}

namespace MonoMod
{
    [MonoModCustomMethodAttribute(nameof(MonoModRules.PatchChestUpdate))]
    class PatchChestUpdateAttribute : Attribute { }

    static partial class MonoModRules
    {
        public static void PatchChestUpdate(ILContext context, CustomAttribute attrib)
        {
            TypeDefinition modChestType = MonoModRule.Modder.FindType("BlossomTales2.ModChest").Resolve();
            MethodDefinition modGiveItemMethod = modChestType.FindMethod("Mod_GiveItem");

            ILCursor cursor = new ILCursor(context);
            //Find
            //if (this.Frame > 6 ...)
            cursor.GotoNext(MoveType.Before,
                instr => instr.MatchLdarg(0),
                instr => instr.MatchLdfld("BlossomTales2.LevelObject", "Frame"),
                instr => instr.MatchLdcI4(6),
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
            //ModChest.Mod_GiveItem(this)
            cursor.RemoveRange(endIndex - startIndex);
            cursor.Emit(OpCodes.Ldarg_0);
            cursor.Emit(OpCodes.Call, modGiveItemMethod);
        }
    }
}
