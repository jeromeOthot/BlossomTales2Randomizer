using BlossomTales2.Extensions;
using Microsoft.Xna.Framework;

namespace BlossomTales2
{
    public class patch_CS_VultureBoss : CS_VultureBoss
    {
        private bool inited;

        public extern void orig_Init();

        public override void Init()
        {
            if (Game1.Globals.Portal_Temple < 2)
            {
                Game1.Globals.Portal_Temple = 2;
            }
            Game1.CurrentLevel.LevelObjects.Add(new Cloud(new Vector3(200f, 0f, 555f), reg: false, 1.5f, 1));
            Game1.CurrentLevel.LevelObjects.Add(new Cloud(new Vector3(555f, 0f, 500f), reg: false, 1.5f, 0));
            Game1.CurrentLevel.LevelObjects.Add(new Cloud(new Vector3(1000f, 0f, 500f), reg: false, 1.5f, 1));
            Game1.CurrentLevel.LevelObjects.Add(new Cloud(new Vector3(1400f, 0f, 555f), reg: false, 1.5f, 1));
            Game1.CurrentLevel.LevelObjects.Add(new Cloud(new Vector3(1700f, 0f, 500f), reg: false, 1.5f, 0));
            Game1.CurrentLevel.LevelObjects.Add(new Cloud(new Vector3(2100f, 0f, 555f), reg: false, 1.5f, 1));
            Game1.CurrentLevel.LevelObjects.Add(new Cloud(new Vector3(200f, 0f, 1500f), reg: false, 2000f, 1));
            Game1.CurrentLevel.LevelObjects.Add(new Cloud(new Vector3(650f, 0f, 1580f), reg: false, 2000f, 0));
            Game1.CurrentLevel.LevelObjects.Add(new Cloud(new Vector3(1100f, 0f, 1580f), reg: false, 2000f, 1));
            Game1.CurrentLevel.LevelObjects.Add(new Cloud(new Vector3(1400f, 0f, 1500f), reg: false, 2000f, 1));
            Game1.CurrentLevel.LevelObjects.Add(new Cloud(new Vector3(1700f, 0f, 1530f), reg: false, 2000f, 0));
            Game1.CurrentLevel.LevelObjects.Add(new Cloud(new Vector3(2100f, 0f, 1500f), reg: false, 2000f, 1));
            foreach (LevelObject levelObject in Game1.CurrentLevel.LevelObjects)
            {
                if (levelObject is RaisingWall)
                {
                    levelObject.Size.Y = 0f;
                }
            }
            if (Mod_HasCompletedVultureObjective())
            {
                inited = true;
                Chest chest = new Chest(new Vector3(Game1.CurrentLevel.Width * 32, 0f, 640f));
                chest.IDNumber = 12;
                if (Mod_HasOpenedChest())
                {
                    chest.Frame = 7;
                }
                Game1.CurrentLevel.LevelObjects.Add(chest);
            }
        }

        private bool Mod_HasCompletedVultureObjective()
        {
            if (ModGlobals.OpenWorldState)
                return Game1Extensions.IsObjectiveCompleted(Globaler.MainGameObjective.canyons_headToVulture);
            else
                return Game1.Globals.MainQuestObjective >= Globaler.MainGameObjective.canyons_headToGolem;
        }

        private bool Mod_HasOpenedChest()
        {
            return Game1Extensions.HasLevelPermaObject("Chest");
        }
    }
}
