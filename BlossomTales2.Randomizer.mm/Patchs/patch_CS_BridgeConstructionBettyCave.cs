using System.Collections.Generic;
using BlossomTales2.Extensions;
using Microsoft.Xna.Framework;
using MonoMod;

namespace BlossomTales2
{
    public class patch_CS_BridgeConstructionBettyCave : CS_BridgeConstructionBettyCave
    {
        public extern void orig_ctor();
        public extern void orig_speakBetty();
        public extern void orig_Update(GameTime gameTime);

        [MonoModConstructor]
        public void ctor()
        {
            //Base
            CutSceneName = "";
            tweener = new Tweener();
            Running = true;
            puppets = new List<Puppet>();
            CameraPosition = Vector2.Zero;
            mapHeight = Game1.CurrentLevel.Height * 64;
            mapCenter = new Vector2(Game1.CurrentLevel.Width * 64 / 2, Game1.CurrentLevel.Height * 64 / 2);
            puppetList = new List<Puppet>();

            if (Mod_IsObjectiveNotSaveBetty())
            {
                foreach (Enemy enemy in Game1.CurrentLevel.Enemies)
                {
                    enemy.Alive = false;
                }
                {
                    foreach (LevelObject levelObject in Game1.CurrentLevel.LevelObjects)
                    {
                        if (levelObject is NPC_2)
                        {
                            levelObject.Alive = false;
                        }
                    }
                    return;
                }
            }
            moveLily();
        }

        public void speakBetty()
        {
            Globaler.MainGameObjective mainGameObjective = Game1.Globals.MainQuestObjective;
            orig_speakBetty();

            if (ModGlobals.OpenWorldState)
            {
                Game1Extensions.MarkObjectiveComplete(Globaler.MainGameObjective.dark_saveBetty);
                Game1.Globals.MainQuestObjective = mainGameObjective;
            }
        }

        public override void Update(GameTime gameTime)
        {
            Globaler.MainGameObjective mainGameObjective = Game1.Globals.MainQuestObjective;
            if (ModGlobals.OpenWorldState
                && Game1Extensions.IsObjectiveCompleted(Globaler.MainGameObjective.dark_headToConstruction)
                && !Game1Extensions.IsObjectiveCompleted(Globaler.MainGameObjective.dark_saveBetty))
                mainGameObjective = Globaler.MainGameObjective.dark_saveBetty;

            orig_speakBetty();

            if (ModGlobals.OpenWorldState)
                Game1.Globals.MainQuestObjective = mainGameObjective;
        }

        private bool Mod_IsObjectiveNotSaveBetty()
        {
            if (ModGlobals.OpenWorldState)
                return Game1Extensions.IsObjectiveCompleted(Globaler.MainGameObjective.dark_headToConstruction) &&
                       !Game1Extensions.IsObjectiveCompleted(Globaler.MainGameObjective.dark_saveBetty);
            else
                return Game1.Globals.MainQuestObjective != Globaler.MainGameObjective.dark_saveBetty;
        }
    }
}
