using System;
using BlossomTales2.Extensions;
using Mono.Cecil;
using Mono.Cecil.Cil;
using MonoMod;
using MonoMod.Cil;
using MonoMod.InlineRT;
using MonoMod.Utils;

namespace BlossomTales2
{
    public class patch_CS_BridgeTroll : CS_BridgeTroll
    {
        public extern void orig_armPump();
        public extern void orig_goPlayer();

        [MonoModIgnore]
        [PatchCSBridgeTrollInit]
        public extern void Init();
        
        [MonoModIgnore]
        [PatchCSBridgeTrollBye]
        public extern void bye();

        public void armPump()
        {
            orig_armPump();
            Game1Extensions.MarkObjectiveComplete(Globaler.MainGameObjective.jungles_talkToGruff);
        }

        public void goPlayer()
        {
            orig_goPlayer();
            //The "bye" function is too long to mod the objective value.
            //Modding it here instead.
            Game1Extensions.MarkObjectiveComplete(Globaler.MainGameObjective.jungles_giveGruffJuice);
            Globaler.MainGameObjective mainGameObjective = Game1.Globals.MainQuestObjective;
            tweener.Timer(0.1f).OnComplete(delegate
            {
                if (ModGlobals.OpenWorldState)
                    Game1.Globals.MainQuestObjective = mainGameObjective;
            });
        }
    }

    public class ModCSBridgeTroll
    {
        public static bool Mod_ShouldDisplayTroll()
        {
            if(ModGlobals.OpenWorldState)
            {
                return (ModGlobals.SkipCutscenes || Game1Extensions.IsObjectiveCompleted(Globaler.MainGameObjective.jungles_talkToGruff))
                       && !Game1Extensions.IsObjectiveCompleted(Globaler.MainGameObjective.jungles_giveGruffJuice);
            }

            return Game1.Globals.MainQuestObjective == Globaler.MainGameObjective.jungles_talkToWitch || Game1.Globals.MainQuestObjective == Globaler.MainGameObjective.jungles_giveGruffJuice;
        }

        public static bool Mod_ShouldTriggerTrollCutscene()
        {
            return !ModGlobals.OpenWorldState &&
                   Game1.Globals.MainQuestObjective < Globaler.MainGameObjective.jungles_headToTown;
        }

        public static void Mod_CompleteTrollObjective()
        {
            if (ModGlobals.OpenWorldState)
                Game1Extensions.MarkObjectiveComplete(Globaler.MainGameObjective.jungles_giveGruffJuice);
            else
                Game1.Globals.MainQuestObjective = Globaler.MainGameObjective.jungles_headToTown;
        }
    }
}

namespace MonoMod
{
    [MonoModCustomMethodAttribute(nameof(MonoModRules.PatchCSBridgeTrollInit))]
    class PatchCSBridgeTrollInitAttribute : Attribute
    {
    }
    
    [MonoModCustomMethodAttribute(nameof(MonoModRules.PatchCSBridgeTrollBye))]
    class PatchCSBridgeTrollByeAttribute : Attribute
    {
    }

    static partial class MonoModRules
    {
        public static void PatchCSBridgeTrollInit(ILContext context, CustomAttribute attrib)
        {
            TypeDefinition modCSBridgeTroll = MonoModRule.Modder.FindType("BlossomTales2.ModCSBridgeTroll").Resolve();
            MethodDefinition mod_ShouldDisplayTrollMethod = modCSBridgeTroll.FindMethod("Mod_ShouldDisplayTroll");
            MethodDefinition mod_ShouldTriggerTrollCutscene = modCSBridgeTroll.FindMethod("Mod_ShouldTriggerTrollCutscene");

            ILCursor cursor = new ILCursor(context);
            //Find
            //if (Game1.Globals.MainQuestObjective < Globaler.MainGameObjective.jungles_headToTown)
            ILLabel returnLabel = null;
            cursor.GotoNext(MoveType.Before,
                instr => instr.MatchLdsfld("BlossomTales2.Game1", "Globals"),
                instr => instr.MatchLdfld("BlossomTales2.Globaler", "MainQuestObjective"),
                instr => instr.MatchLdcI4(11),
                instr => instr.MatchBlt(out returnLabel)
            );
            //Replace with
            //if(ModCSBridgeTroll.Mod_ShouldTriggerTrollCutscene())
            cursor.RemoveRange(4);
            ILLabel branchFalseLabel = cursor.MarkLabel();
            cursor.Emit(OpCodes.Call, mod_ShouldTriggerTrollCutscene);
            cursor.Emit(OpCodes.Brtrue, returnLabel);
            //Find
            //if (Game1.Globals.MainQuestObjective == Globaler.MainGameObjective.jungles_talkToWitch ...)
            cursor.GotoPrev(MoveType.Before,
                instr => instr.MatchLdsfld("BlossomTales2.Game1", "Globals"),
                instr => instr.MatchLdfld("BlossomTales2.Globaler", "MainQuestObjective"),
                instr => instr.MatchLdcI4(9),
                instr => instr.MatchBeq(out ILLabel label),
                instr => instr.MatchLdsfld("BlossomTales2.Game1", "Globals"),
                instr => instr.MatchLdfld("BlossomTales2.Globaler", "MainQuestObjective"),
                instr => instr.MatchLdcI4(10),
                instr => instr.MatchBneUn(out ILLabel label)
            );
            //Replace with
            //if(ModCSBridgeTroll.Mod_ShouldDisplayTroll())
            cursor.RemoveRange(8);
            cursor.Emit(OpCodes.Call, mod_ShouldDisplayTrollMethod);
            cursor.Emit(OpCodes.Brfalse, branchFalseLabel);
        }

        public static void PatchCSBridgeTrollBye(ILContext context, CustomAttribute attrib)
        {
            TypeDefinition modCSBridgeTroll = MonoModRule.Modder.FindType("BlossomTales2.ModCSBridgeTroll").Resolve();
            MethodDefinition mod_CompleteTrollObjectiveMethod = modCSBridgeTroll.FindMethod("Mod_CompleteTrollObjective");

            ILCursor cursor = new ILCursor(context);
            //Find
            //Game1.Globals.MainQuestObjective = Globaler.MainGameObjective.jungles_pirateDefeated;
            cursor.GotoNext(MoveType.Before,
                instr => instr.MatchLdsfld("BlossomTales2.Game1", "Globals"),
                instr => instr.MatchLdcI4(11),
                instr => instr.MatchStfld("BlossomTales2.Globaler", "MainQuestObjective")
            );
            cursor.RemoveRange(3);
            //Replace with
            //ModBossPirateCaptain.Mod_SetPiratesDefeated();
            cursor.Emit(OpCodes.Call, mod_CompleteTrollObjectiveMethod);
        }
    }
}
