using BlossomTales2.Extensions;
using Microsoft.Xna.Framework;

namespace BlossomTales2
{
    public class patch_CS_BossCyclops : CS_BossCyclops
    {
        private bool inited;

        public override void Init()
        {
            Game1.CurrentLevel.LevelObjects.Add(new MirrorPost(new Vector3(1504f, 0f, 1312f)));
            Game1.CurrentLevel.LevelObjects.Add(new MirrorPost(new Vector3(1504f, 0f, 1632f)));
            Game1.CurrentLevel.LevelObjects.Add(new MirrorPost(new Vector3(2208f, 0f, 1312f)));
            Game1.CurrentLevel.LevelObjects.Add(new MirrorPost(new Vector3(2208f, 0f, 1632f)));
            if (Mod_HasNotDefeatedCyclops())
            {
                foreach (LevelObject levelObject in Game1.CurrentLevel.LevelObjects)
                {
                    if (levelObject is CameraOverrider)
                    {
                        levelObject.Position.X += 2000f;
                    }
                    else if (levelObject is RaisingWall && levelObject.IDNumber == 9)
                    {
                        levelObject.Size.Y = 0f;
                    }
                }
                return;
            }
            Chest chest = new Chest(new Vector3(1856f, 0f, 1408f));
            chest.IDNumber = 14;
            if (Mod_HasOpenedChest())
            {
                chest.Frame = 7;
            }
            Game1.CurrentLevel.LevelObjects.Add(chest);
            foreach (LevelObject levelObject2 in Game1.CurrentLevel.LevelObjects)
            {
                if (levelObject2 is CameraOverrider)
                {
                    levelObject2.Alive = false;
                    break;
                }
                if (levelObject2 is RaisingWall && levelObject2.IDNumber == 9)
                {
                    levelObject2.Size.Y = 0f;
                }
            }
            inited = true;
        }

        private bool Mod_HasNotDefeatedCyclops()
        {
            if (ModGlobals.OpenWorldState)
                return !Game1Extensions.IsObjectiveCompleted(Globaler.MainGameObjective.lab_enterLabyrinth);
            else
                return Game1.Globals.MainQuestObjective <= Globaler.MainGameObjective.lab_enterLabyrinth;
        }

        private bool Mod_HasOpenedChest()
        {
            return Game1Extensions.HasLevelPermaObject("Chest");
        }
    }
}
