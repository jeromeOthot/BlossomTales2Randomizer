using BlossomTales2.Extensions;
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

        public void changeLevel()
        {
            Globaler.MainGameObjective mainGameObjective = Game1.Globals.MainQuestObjective;
            orig_changeLevel();

            if (ModGlobals.OpenWorldState)
            {
                Game1Extensions.MarkObjectiveComplete(Globaler.MainGameObjective.canyons_fightGolem);
                Game1.Globals.MainQuestObjective = mainGameObjective;
            }
        }

        private bool Mod_HasNotDefeatedGenie()
        {
            if (ModGlobals.OpenWorldState)
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
