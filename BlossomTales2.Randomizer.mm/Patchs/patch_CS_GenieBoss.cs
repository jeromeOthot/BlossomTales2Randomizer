using BlossomTales2.Extensions;
using BlossomTales2.Randomizer.mm;
using Microsoft.Xna.Framework;

namespace BlossomTales2
{
    public class patch_CS_GenieBoss : CS_GenieBoss
    {
        public extern void orig_changeLevel();

        private Vector3 startpos;

        public override void Init()
        {
            if (Game1.Globals.Portal_Temple < 3)
            {
                Game1.Globals.Portal_Temple = 3;
            }
            if (Mod_HasNotDefeatedGenie() && !Game1.Globals.playedGenieIntro && !Game1.Globals.Def_BossDjinn)
            {
                focusCameraOnTarget(new Vector2(Game1.CurrentLevel.Width * 32, 576f), 2f, littleTalk);
            }
            Chest chest = new Chest(startpos);
            if (!Game1.Globals.Def_BossDjinn)
            {
                chest.IDNumber = 101;
            }
            else if (!Mod_HasOpenedBossChest())
            {
                chest.IDNumber = 22;
            }
            else
            {
                chest.Frame = 7;
            }
            Game1.CurrentLevel.LevelObjects.Add(chest);
            foreach (LevelObject levelObject in Game1.CurrentLevel.LevelObjects)
            {
                if (levelObject is CameraOverrider)
                {
                    levelObject.Position.X += 2000f;
                }
                if (levelObject is RaisingWall)
                {
                    levelObject.Size.Y = 0f;
                    levelObject.Frame = 0;
                }
            }
        }

        public void postFight()
        {
            Game1.Gui.HideHud = true;
            Game1.player.Direction = 3;
            if (RandomizerSettings.GamePlay.SkipCutscenes)
            {
                tweener.Timer(1f).OnComplete(changeLevel);
            }
            else
            {
                Game1.Narrator.AddLine("Grandpa: After a hard-fought battle, <B>Lily was one key piece closer to her goal.", 12);
                Game1.Narrator.AddLine("Chrys: <B>Lily found another piece of the key, blah blah blah.", 1);
                Game1.Narrator.AddLine("Chrys: What about me and the <A>Minotaur <A>King, Grandpa?", 1);
                Game1.Narrator.AddLine("Grandpa: Ah, yes. Meanwhile, the enemy's wily plans were also unfolding...", 12, changeLevel);
            }
        }

        public void changeLevel()
        {
            if (RandomizerSettings.GamePlay.SkipCutscenes)
            {
                Game1.player.CamOffset = Vector2.Zero;
                Game1.player.HasMoved = false;
                //Copy-pasted from CS_ChrysBaseball
                Game1.FadeOut = true;
                Game1.FadeAlpha = 255;
                Game1.TransitionType = Game1.Transitions.FadeBlack;
                Game1.FadeNewLevelName = "overworld-16x20.tmx";
                Game1.FadeNewDoorNumber = 1;
                Game1.FadeNewDoorType = 0;

                if (!RandomizerSettings.GamePlay.OpenWorldState)
                    Game1.Globals.MainQuestObjective = Globaler.MainGameObjective.canyons_talkToOwlAgain;
                else
                    Game1Extensions.MarkObjectiveComplete(Globaler.MainGameObjective.canyons_fightGolem);

                Game1.player.RemovePlayerControls = false;
                Game1.fadeController.Clear();
                return;
            }

            Globaler.MainGameObjective mainGameObjective = Game1.Globals.MainQuestObjective;
            orig_changeLevel();

            if (RandomizerSettings.GamePlay.OpenWorldState)
            {
                Game1Extensions.MarkObjectiveComplete(Globaler.MainGameObjective.canyons_fightGolem);
                Game1.Globals.MainQuestObjective = mainGameObjective;
            }
        }

        private bool Mod_HasNotDefeatedGenie()
        {
            if (RandomizerSettings.GamePlay.OpenWorldState)
                return !Game1Extensions.IsObjectiveCompleted(Globaler.MainGameObjective.canyons_fightGolem);
            else
                return Game1.Globals.MainQuestObjective <= Globaler.MainGameObjective.canyons_fightGolem;
        }

        private bool Mod_HasOpenedBossChest()
        {
            return Game1Extensions.HasLevelPermaObject("Chest");
        }
    }
}
