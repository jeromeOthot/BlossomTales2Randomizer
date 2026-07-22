using BlossomTales2;
using BlossomTales2.Randomizer.mm;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlossomTales2
{
    public class patch_CS_BridgeTroll : CS_BridgeTroll
    {
        private Puppet bridgeTroll = new Puppet("Fake Troll", Vector3.Zero);

        public extern void orig_Init();
        public extern void orig_armPump();

        public override void Init()
        {
            bridgeTroll = new Puppet("bridgeTroll", new Vector3(1100f, 0f, 968f));
            bridgeTroll.Zdepth = -163.41f;

            //if (Game1.Globals.MainQuestObjective == Globaler.MainGameObjective.jungles_talkToWitch || Game1.Globals.MainQuestObjective == Globaler.MainGameObjective.jungles_giveGruffJuice)
            if ((ModGlobals.SkipCutscenes || RandomizerSingleton.IsObjectiveCompleted(Globaler.MainGameObjective.jungles_talkToGruff)) 
                && !RandomizerSingleton.IsObjectiveCompleted(Globaler.MainGameObjective.jungles_giveGruffJuice))
            {
                putTrollOnBridge();
            }
            else
            {
                if (Game1.Globals.MainQuestObjective < Globaler.MainGameObjective.jungles_headToTown)
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
            RandomizerSingleton.MarkObjectiveComplete(Globaler.MainGameObjective.jungles_talkToGruff);
        }
    }
}
