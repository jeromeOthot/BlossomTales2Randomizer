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

        public static void Mod_SetPiratesDefeated()
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
            TypeDefinition modBossPirateCaptainType = MonoModRule.Modder.FindType("BlossomTales2.ModBossPirateCaptain").Resolve();
            MethodDefinition mod_CompleteMorklaObjectiveMethod = modBossPirateCaptainType.FindMethod("Mod_CompleteMorklaObjective");

            ILCursor cursor = new ILCursor(context);
            //Find
            //if (Game1.Globals.MainQuestObjective < Globaler.MainGameObjective.jungles_pirateDefeated)
            cursor.GotoNext(MoveType.Before,
                instr => instr.MatchLdsfld("BlossomTales2.Game1", "Globals"),
                instr => instr.MatchLdfld("BlossomTales2.Globaler", "MainQuestObjective"),
                instr => instr.MatchLdcI4(17),
                instr => instr.MatchBge(out ILLabel label)
            );
            //Replace with
            //ModBossPirateCaptain.Mod_CompleteMorklaObjective()
            cursor.RemoveRange(7);
            cursor.Emit(OpCodes.Call, mod_CompleteMorklaObjectiveMethod);
        }

        public static void PatchBossPirateCaptainDie(ILContext context, CustomAttribute attrib)
        {
            TypeDefinition patchType = MonoModRule.Modder.FindType("BlossomTales2.ModBossPirateCaptain").Resolve();
            MethodDefinition mod_SetPiratesDefeatedMethod = patchType.FindMethod("Mod_SetPiratesDefeated");

            ILCursor cursor = new ILCursor(context);
            //Find
            //Game1.Globals.MainQuestObjective = Globaler.MainGameObjective.jungles_pirateDefeated;
            cursor.GotoNext(MoveType.Before,
                instr => instr.MatchLdsfld("BlossomTales2.Game1", "Globals"),
                instr => instr.MatchLdcI4(17),
                instr => instr.MatchStfld("BlossomTales2.Globaler", "MainQuestObjective")
            );
            cursor.RemoveRange(3);
            //Replace with
            //ModBossPirateCaptain.Mod_SetPiratesDefeated();
            cursor.Emit(OpCodes.Call, mod_SetPiratesDefeatedMethod);
        }
    }
}
