using BlossomTales2.Extensions;
using Microsoft.Xna.Framework;
using Mono.Cecil;
using Mono.Cecil.Cil;
using MonoMod;
using MonoMod.Cil;
using MonoMod.InlineRT;
using MonoMod.Utils;
using System;

namespace BlossomTales2
{
    internal class patch_BossPirateCaptain : BossPirateCaptain
    {
        public extern void orig_Update(GameTime gameTime);
        public extern void orig_Die();

        public patch_BossPirateCaptain(Vector3 position) : base(position)
        {            
        }

        [MonoModIgnore]
        [PatchBossPirateCaptainUpdate]
        public extern void Update(GameTime gameTime);

        [MonoModIgnore]
        [PatchBossPirateCaptainDie]
        public extern void Die();
    }

    public class ModBossPirateCaptain
    {
        public static void Mod_CompleteMorklaObjective()
        {
            if (ModGlobals.OpenWorldState)
            {
                Game1Extensions.MarkObjectiveComplete(Globaler.MainGameObjective.jungles_morklaEnter);
            }
            else
            {
                if (Game1.Globals.MainQuestObjective < Globaler.MainGameObjective.jungles_pirateDefeated)
                {
                    Game1.Globals.MainQuestObjective = Globaler.MainGameObjective.jungles_pirateDefeated;
                }
            }
        }

        public static void Mod_SetPiratesDefated()
        {
            if (ModGlobals.OpenWorldState)
                Game1Extensions.MarkObjectiveComplete(Globaler.MainGameObjective.jungles_morklaEnter);
            else          
                Game1.Globals.MainQuestObjective = Globaler.MainGameObjective.jungles_pirateDefeated;
        }
    }
}

namespace MonoMod
{
    [MonoModCustomMethodAttribute(nameof(MonoModRules.PatchBossPirateCaptainUpdate))]
    class PatchBossPirateCaptainUpdateAttribute : Attribute { }

    [MonoModCustomMethodAttribute(nameof(MonoModRules.PatchBossPirateCaptainDie))]
    class PatchBossPirateCaptainDieAttribute : Attribute { }

    static partial class MonoModRules
    {
        public static void PatchBossPirateCaptainUpdate(ILContext context, CustomAttribute attrib)
        {
            TypeDefinition patchType = MonoModRule.Modder.FindType("BlossomTales2.ModBossPirateCaptain").Resolve();
            MethodDefinition mod_CompleteMorklaObjectiveMethod = patchType.FindMethod("Mod_CompleteMorklaObjective");

            ILCursor cursor = new ILCursor(context);
            //IL_0206: ldsfld       class BlossomTales2.Globaler BlossomTales2.Game1::Globals
            //IL_020b: ldfld valuetype BlossomTales2.Globaler/MainGameObjective BlossomTales2.Globaler::MainQuestObjective
            //IL_0210: ldc.i4.s     17 // 0x11
            //IL_0212: bge.s IL_0220
            cursor.GotoNext(MoveType.Before,
                instr => instr.MatchLdsfld("BlossomTales2.Game1", "Globals"),
                instr => instr.MatchLdfld("BlossomTales2.Globaler", "MainQuestObjective"),
                instr => instr.MatchLdcI4(17),
                instr => instr.MatchBge(out ILLabel label)
            );

            //en
            //IL_001d: call void BlossomTales2.ModBossPirateCaptain::Mod_CompleteMorklaObjective()
            cursor.RemoveRange(7);
            cursor.Emit(OpCodes.Call, mod_CompleteMorklaObjectiveMethod);
            cursor.Emit(OpCodes.Nop);
        }

        public static void PatchBossPirateCaptainDie(ILContext context, CustomAttribute attrib)
        {
            TypeDefinition patchType = MonoModRule.Modder.FindType("BlossomTales2.ModBossPirateCaptain").Resolve();
            MethodDefinition mod_SetPiratesDefeatedMethod = patchType.FindMethod("Mod_SetPiratesDefated");

            ILCursor cursor = new ILCursor(context);
            //IL_005a: ldsfld       class BlossomTales2.Globaler BlossomTales2.Game1::Globals
            //IL_005f: ldc.i4.s     17 // 0x11
            //IL_0061: stfld valuetype BlossomTales2.Globaler / MainGameObjective BlossomTales2.Globaler::MainQuestObjective
            cursor.GotoNext(MoveType.Before,
                instr => instr.MatchLdsfld("BlossomTales2.Game1", "Globals"),
                instr => instr.MatchLdcI4(17),
                instr => instr.MatchStfld("BlossomTales2.Globaler", "MainQuestObjective")
            );

            //en
            //IL_001d: call void BlossomTales2.ModBossPirateCaptain::Mod_SetPiratesDefated()
            cursor.RemoveRange(3);
            //cursor.Emit(OpCodes.Ldarg_0);
            cursor.Emit(OpCodes.Call, mod_SetPiratesDefeatedMethod);
            cursor.Emit(OpCodes.Nop);
        }
    }
}