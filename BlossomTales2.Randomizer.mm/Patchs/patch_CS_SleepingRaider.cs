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
    public class patch_CS_SleepingRaider : CS_SleepingRaider
    {
        private Puppet bard;
        private bool showSheet;
        private bool shownotes;

        [MonoModIgnore]
        [PatchCSSleepingRaiderInit]
        public extern override void Init();

        public void startTheSong()
        {
            Mod_GiveSong();
            showSheet = false;
            Game1.player.StopUpdating = false;
            Game1.player.RemovePlayerControls = false;
            Game1.player.MusicSuccessful = 3;
            Game1.player.SongTimer = 10000;
            Game1.player.SongStartWait = 500;
            tweener.Timer(0.5f).OnComplete(delegate
            {
                shownotes = true;
            });
            tweener.Timer(9f).OnComplete(delegate
            {
                shownotes = false;
            });
            tweener.Timer(10f).OnComplete(turnPlayer);
        }

        public void goPlayer()
        {
            focusCam = false;
            Game1.player.RemovePlayerControls = false;
            Game1.player.LockDirection = false;
            Game1.player.LockPosition = false;
            Mod_CompleteLearnSongObjective();
            Game1.Gui.AddGuiTicker(EquipableItem.IngredientList.MapUpdated, 1);
            Game1.Gui.HideHud = false;
            Game1.SwitchBGMusic();
            Game1.player.HasMoved = false;
            Game1.player.CamOffset = Vector2.Zero;
        }

        private void Mod_GiveSong()
        {
            string location = bard.name + "_song";
            RandomizerSingleton.Instance.GiveItemAtLocation(location, Vector3.Zero);
            Game1Extensions.AddLevelPermaObject(location, Vector3.Zero);
        }

        private void Mod_CompleteLearnSongObjective()
        {
            if(!ModGlobals.OpenWorldState)
                Game1.Globals.MainQuestObjective = Globaler.MainGameObjective.dark_talkToOwl;
        }
    }

    public class ModCSSleepingRaider
    {
        public static bool Mod_HasReceivedBardItem()
        {
            return Game1Extensions.HasLevelPermaObject("bard_song");
        }
    }
}

namespace MonoMod
{
    [MonoModCustomMethodAttribute(nameof(MonoModRules.PatchCSSleepingRaiderInit))]
    class PatchCSSleepingRaiderInitAttribute : Attribute { }

    static partial class MonoModRules
    {
        public static void PatchCSSleepingRaiderInit(ILContext context, CustomAttribute attrib)
        {
            TypeDefinition modCsSleepingRaiderType = MonoModRule.Modder.FindType("BlossomTales2.ModCSSleepingRaider").Resolve();
            MethodDefinition mod_HasReceivedBardItem = modCsSleepingRaiderType.FindMethod("Mod_HasReceivedBardItem");

            ILCursor cursor = new ILCursor(context);
            //Find
            //Game1.Globals.Learned_Songs.Contains(Globaler.Songs.WakeUp)
            cursor.GotoNext(MoveType.Before,
                instr => instr.MatchLdsfld("BlossomTales2.Game1", "Globals"),
                instr => instr.MatchLdfld("BlossomTales2.Globaler", "Learned_Songs"),
                instr => instr.MatchLdcI4(1)
            );
            //Replace with
            //ModCSSleepingRaider.Mod_HasLearnedWakeUpSong()
            cursor.RemoveRange(4);
            cursor.Emit(OpCodes.Call, mod_HasReceivedBardItem);
        }
    }
}
