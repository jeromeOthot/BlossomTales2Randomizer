using System;
using BlossomTales2.Extensions;
using BlossomTales2.Randomizer.mm;
using Mono.Cecil;
using Mono.Cecil.Cil;
using MonoMod;
using MonoMod.Cil;
using MonoMod.InlineRT;
using MonoMod.Utils;

namespace BlossomTales2
{
    internal class patch_CS_CanyonEntrance : CS_CanyonEntrance
    {
        public extern void orig_Init();
        public extern void orig_goOwl();
        public extern void orig_openDoor();
        public extern void orig_enterDungeon();

        public override void Init()
        {
            if (Mod_HasNotOpenedDoor())
            {
                placeDoorDown();
            }

            if (Mod_IsObjectiveTalkToOwlAgain())
            {
                Game1.player.EnteringDoor = false;
                Game1.player.RemovePlayerControls = true;
                Game1.player.Direction = 3;
                talkToOwlAgain();
            }

            if (Mod_IsObjectiveEnterDungeon())
            {
                return;
            }

            foreach (LevelObject levelObject in Game1.CurrentLevel.LevelObjects)
            {
                if (levelObject is NPC_2)
                {
                    levelObject.Alive = false;
                    levelObject.Collidable = false;
                }
            }
        }

        [MonoModIgnore]
        [PatchCSCanyonEntranceTalkToOwlAgain]
        public extern void talkToOwlAgain();

        public void goOwl()
        {
            if(RandomizerSettings.GamePlay.OpenWorldState)
                return;
            else
                orig_goOwl();
        }

        public void openDoor()
        {
            if (!Mod_HasNotOpenedDoor())
                return;

            //Temporarely change objective to pass the if in original method.
            Globaler.MainGameObjective mainGameObjective = Game1.Globals.MainQuestObjective;
            Game1.Globals.MainQuestObjective = Globaler.MainGameObjective.canyons_enterDungeon;
            orig_openDoor();
            Game1.Globals.MainQuestObjective = mainGameObjective;
        }

        public void enterDungeon()
        {
            Globaler.MainGameObjective mainGameObjective = Game1.Globals.MainQuestObjective;
            orig_enterDungeon();

            if (RandomizerSettings.GamePlay.OpenWorldState)
            {
                Game1Extensions.MarkObjectiveComplete(Globaler.MainGameObjective.canyons_enterDungeon);
                Game1.Globals.MainQuestObjective = mainGameObjective;
            }
        }

        public bool Mod_HasNotOpenedDoor()
        {
            if (RandomizerSettings.GamePlay.OpenWorldState)
                return !Game1Extensions.IsObjectiveCompleted(Globaler.MainGameObjective.canyons_enterDungeon);
            else
                return Game1.Globals.MainQuestObjective <= Globaler.MainGameObjective.canyons_enterDungeon;
        }

        public bool Mod_IsObjectiveEnterDungeon()
        {
            if (RandomizerSettings.GamePlay.OpenWorldState)
                return !Game1Extensions.IsObjectiveCompleted(Globaler.MainGameObjective.canyons_enterDungeon);
            else
                return Game1.Globals.MainQuestObjective == Globaler.MainGameObjective.canyons_enterDungeon;
        }

        public bool Mod_IsObjectiveTalkToOwlAgain()
        {
            if (RandomizerSettings.GamePlay.OpenWorldState)
                return Game1Extensions.IsObjectiveCompleted(Globaler.MainGameObjective.canyons_fightGolem) && !Game1Extensions.IsObjectiveCompleted(Globaler.MainGameObjective.canyons_talkToOwlAgain);
            else
                return Game1.Globals.MainQuestObjective == Globaler.MainGameObjective.canyons_talkToOwlAgain;
        }
    }

    public class ModCSCanyonEntrance
    {
        public static void Mod_CompleteTalkToOwlAgain()
        {
            if (RandomizerSettings.GamePlay.OpenWorldState)
                Game1Extensions.MarkObjectiveComplete(Globaler.MainGameObjective.canyons_talkToOwlAgain);
            else
                Game1.Globals.MainQuestObjective = Globaler.MainGameObjective.dark_headToConstruction;
        }
    }
}

namespace MonoMod
{
    [MonoModCustomMethodAttribute(nameof(MonoModRules.PatchCSCanyonEntranceTalkToOwlAgain))]
    class PatchCSCanyonEntranceTalkToOwlAgainAttribute : Attribute { }

    static partial class MonoModRules
    {
        public static void PatchCSCanyonEntranceTalkToOwlAgain(ILContext context, CustomAttribute attrib)
        {
            TypeDefinition modCSCanyonEntranceType = MonoModRule.Modder.FindType("BlossomTales2.CS_CanyonEntrance").Resolve();
            MethodDefinition mod_CompleteTalkToOwlAgainLambda = modCSCanyonEntranceType.FindMethod("<talkToOwlAgain>b__5_1");
            new ILContext(mod_CompleteTalkToOwlAgainLambda).Invoke(PatchTalkToOwlAgainLambda);
        }

        public static void PatchTalkToOwlAgainLambda(ILContext context)
        {
            TypeDefinition modCSCanyonEntranceType = MonoModRule.Modder.FindType("BlossomTales2.ModCSCanyonEntrance").Resolve();
            MethodDefinition mod_CompleteTalkToOwlAgainMethod = modCSCanyonEntranceType.FindMethod("Mod_CompleteTalkToOwlAgain");
            ILCursor cursor = new ILCursor(context);

            //Find L.71
            //Game1.Globals.MainQuestObjective = Globaler.MainGameObjective.dark_headToConstruction;
            cursor.GotoNext(MoveType.Before,
                instr => instr.MatchLdsfld("BlossomTales2.Game1", "Globals"),
                instr => instr.MatchLdcI4(31),
                instr => instr.MatchStfld("BlossomTales2.Globaler", "MainQuestObjective"));

            //Replace with
            //ModCSCanyonEntrance.Mod_CompleteTalkToOwlAgain())
            cursor.RemoveRange(3);
            cursor.Emit(OpCodes.Call, mod_CompleteTalkToOwlAgainMethod);
        }
    }
}
