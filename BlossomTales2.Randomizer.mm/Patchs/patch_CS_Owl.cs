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
    public class patch_CS_Owl : CS_Owl
    {
        private Puppet owlHead;
        private Puppet z1;
        private Puppet z2;

        public extern void orig_wakeOwl();
        public extern void orig_giveBoomerang();
        public extern void orig_endScene();

        [MonoModIgnore]
        [PatchCSOwlInit]
        public extern override void Init();

        public void wakeOwl()
        {
            if (ModCSOwl.Mod_IsTalkToOwlObjectiveNotCompleted())
            {
                owlHead.play("awake", "blink");
                owlHead.bounce();
                Game1.Camera.Shake(12f, 0.95f);
                Game1.Gui.HideHud = true;
                z1.play("hide");
                z2.play("hide");
                Game1.playSoundCue("blank124");
                Game1.Gui.HideHud = true;
                Game1.player.RemovePlayerControls = true;
                Game1.player.Direction = 1;
                tweener.Timer(1f).OnComplete(delegate
                {
                    Game1.player.Direction = 1;
                    Game1.Dialoger.AddLine("Great Owl: HoOoOoOo.......oOo?!");
                    Game1.Dialoger.AddLine("Great Owl: What a dream. I must have dozed off waiting for the girl who made the wish.");
                    Game1.Dialoger.AddLine("Great Owl: Oh, it's you! Did you awaken me? Good. I might have slept for another decade.", kidsInterupt);
                });
            }
        }

        public void giveBoomerang()
        {
            Mod_GiveItem();
            tweener.Timer(3f).OnComplete(talkMore);
        }

        public void endScene()
        {
            Globaler.MainGameObjective mainGameObjective = Game1.Globals.MainQuestObjective;
            orig_endScene();

            if (ModGlobals.OpenWorldState)
                Game1.Globals.MainQuestObjective = mainGameObjective;
        }

        private void Mod_GiveItem()
        {
            RandomizerSingleton.Instance.GiveItemAtLocation("owl", Vector3.Zero);
        }
    }

    public class ModCSOwl
    {
        public static bool Mod_IsTalkToOwlObjectiveNotCompleted()
        {
            if (ModGlobals.OpenWorldState)
                return !Game1Extensions.IsObjectiveCompleted(Globaler.MainGameObjective.dark_talkToOwl);
            else
                return Game1.Globals.MainQuestObjective < Globaler.MainGameObjective.dark_enterDungeon;
        }
    }
}

namespace MonoMod
{
    [MonoModCustomMethodAttribute(nameof(MonoModRules.PatchCSOwlInit))]
    class PatchCSOwlInitAttribute : Attribute { }

    static partial class MonoModRules
    {
        public static void PatchCSOwlInit(ILContext context, CustomAttribute attrib)
        {
            TypeDefinition modCsOwlType = MonoModRule.Modder.FindType("BlossomTales2.ModCSOwl").Resolve();
            MethodDefinition mod_IsTalkToOwlObjectiveNotCompleted = modCsOwlType.FindMethod("Mod_IsTalkToOwlObjectiveNotCompleted");

            ILCursor cursor = new ILCursor(context);
            //Find
            //Game1.Globals.MainQuestObjective < Globaler.MainGameObjective.dark_enterDungeon
            ILLabel returnLabel = null;
            cursor.GotoNext(MoveType.Before,
                instr => instr.MatchLdsfld("BlossomTales2.Game1", "Globals"),
                instr => instr.MatchLdfld("BlossomTales2.Globaler", "MainQuestObjective"),
                instr => instr.MatchLdcI4(39),
                instr => instr.MatchBlt(out returnLabel)
            );
            //Replace with
            //ModCSOwl.Mod_IsTalkToOwlObjectiveNotComplete()
            cursor.RemoveRange(4);
            cursor.Emit(OpCodes.Call, mod_IsTalkToOwlObjectiveNotCompleted);
            cursor.Emit(OpCodes.Brfalse, returnLabel);
        }
    }
}
