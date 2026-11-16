using BlossomTales2.Extensions;
using BlossomTales2.Randomizer.mm;
using Microsoft.Xna.Framework;

namespace BlossomTales2
{
    internal class patch_CS_OctopusBoss : CS_OctopusBoss
    {
        public extern void orig_shutDoor();
        public extern void orig_switchMaps();

        public override void Init()
        {
            if (!Game1.Globals.Def_BossOctopus)
            {
                Game1.SwitchBGMusic("stop", 0.01f);
                Game1.Gui.HideHud = true;
                Game1.player.RemovePlayerControls = true;
                Game1.player.EnteringDoor = false;
                Game1.player.EnteringDoorAmount = 0;
                focusCameraOnTarget(new Vector2(Game1.CurrentLevel.Width * 32, 640f), 2.5f, shutDoor);
                Game1.player.MoveToPosition(new Vector3(Game1.CurrentLevel.Width * 32, 0f, 768f), 1);
            }
            else
            {
                Chest chest = new Chest(new Vector3(Game1.CurrentLevel.Width * 32, 0f, Game1.CurrentLevel.Height * 32));
                chest.IDNumber = 21;
                if (Mod_HasOpenedBossChest())
                {
                    chest.Frame = 7;
                }
                Game1.CurrentLevel.LevelObjects.Add(chest);
            }
        }

        public void shutDoor()
        {
            if(RandomizerSettings.GamePlay.SkipCutscenes)
                throwRock();
            else
                orig_shutDoor();
        }

        public void raiseOctopus()
        {
            DoorGate doorGate = new DoorGate(new Vector3(864f, 0f, 968f));
            doorGate.Velocity.X = 2f;
            doorGate.Velocity.Y = 1f;
            doorGate.Row = 2;
            Game1.CurrentLevel.LevelObjects.Add(doorGate);
            Game1.Camera.Shake(12f, 0.96f);
            Game1.playSoundCue("blank180");
            Game1.CurrentLevel.Enemies.Add(new BossOctopus(new Vector3(Game1.CurrentLevel.Width * 32, 0f, 512f)));
            Game1.makeParticleExplosion(new Vector3(Game1.CurrentLevel.Width * 32, 0f, 512f), new Color(175, 224, 255), 100, 10);
            Game1.makeParticleExplosion(new Vector3(Game1.CurrentLevel.Width * 32, 0f, 512f), Color.White, 100, 10);
            Game1.playSoundCue("blank057");
            tweener.Timer(1f).OnComplete(delegate
            {
                Mod_RaiseOctopusDialog();
                tweener.Timer(1f).Repeat(1).OnBegin(delegate
                {
                    Game1.playSoundCue("blank147");
                });
            });
        }

        internal void achoo()
        {
            Game1.player.RemovePlayerControls = true;
            Mod_CompleteGetEyesObjective();
            if (RandomizerSettings.GamePlay.SkipCutscenes)
            {
                tweener.Timer(1f).OnComplete(switchMaps);
            }
            else
            {
                Game1.playSoundCue("blank112");
                Game1.Dialoger.AddLine("<P>Morkla: Ahh...", achoo2);
            }
        }

        //only here so that the code compile.
        internal void achoo2()
        {
            Game1.playSoundCue("blank112");
            Game1.Dialoger.AddLine("<P>Morkla: Ahh...", switchMaps);
        }

        internal void switchMaps()
        {
            Game1.FadeOut = true;
            Game1.TransitionType = Game1.Transitions.FadeBlack;
            Game1.FadeAlpha = 1;
            Game1.FadeNewLevelName = "jungles-24x20.tmx";
        }

        private void Mod_RaiseOctopusDialog()
        {
            if(RandomizerSettings.GamePlay.SkipCutscenes)
                finalOctoDialog();
            else
                Game1.Dialoger.AddLine("Octopus: Freeeeeshhhh fleeeesshhhhhh.", grandpaAgain);
        }

        private void Mod_CompleteGetEyesObjective()
        {
            if (RandomizerSettings.GamePlay.OpenWorldState)
                Game1Extensions.MarkObjectiveComplete(Globaler.MainGameObjective.jungles_morklaGetEyes);
            else
                Game1.Globals.MainQuestObjective = Globaler.MainGameObjective.jungles_morklaComplete;
        }

        private bool Mod_HasOpenedBossChest()
        {
            return Game1Extensions.HasLevelPermaObject("Chest");
        }
    }
}
