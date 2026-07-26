using BlossomTales2.Extensions;
using Microsoft.Xna.Framework;

namespace BlossomTales2
{
    public class patch_CS_BridgeTroll : CS_BridgeTroll
    {
        private Puppet bridgeTroll = new Puppet("Fake Troll", Vector3.Zero);

        public extern void orig_Init();
        public extern void orig_armPump();
        public extern void orig_goPlayer();

        public override void Init()
        {
            bridgeTroll = new Puppet("bridgeTroll", new Vector3(1100f, 0f, 968f));
            bridgeTroll.Zdepth = -163.41f;

            if (Mod_ShouldDisplayTroll())
            {
                putTrollOnBridge();
            }
            else
            {
                if (!ModGlobals.OpenWorldState && Game1.Globals.MainQuestObjective < Globaler.MainGameObjective.jungles_headToTown)
                {
                    return;
                }

                foreach (LevelObject levelObject in Game1.CurrentLevel.LevelObjects)
                {
                    if (levelObject is CollisionRect && levelObject.IDNumber == 9)
                    {
                        levelObject.Alive = false;
                    }
                    else if (levelObject is Sign)
                    {
                        levelObject.Alive = false;
                    }
                    else if (levelObject is SpawnDialogRect && levelObject.IDNumber < 5)
                    {
                        levelObject.Alive = false;
                    }
                }
            }
        }

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

        private bool Mod_ShouldDisplayTroll()
        {
            if(ModGlobals.OpenWorldState)
            {
                return (ModGlobals.SkipCutscenes || Game1Extensions.IsObjectiveCompleted(Globaler.MainGameObjective.jungles_talkToGruff))
                && !Game1Extensions.IsObjectiveCompleted(Globaler.MainGameObjective.jungles_giveGruffJuice);
            }
            else
            {
                return Game1.Globals.MainQuestObjective == Globaler.MainGameObjective.jungles_talkToWitch || Game1.Globals.MainQuestObjective == Globaler.MainGameObjective.jungles_giveGruffJuice;
            }
        }
    }
}
