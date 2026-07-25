using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlossomTales2
{
    internal class patch_CS_MorklaBarrier : CS_MorklaBarrier
    {
        public extern void orig_Init();
        public extern void orig_goPirates();

        public override void Init()
        {
            if (ModGlobals.OpenWorldState)
                return;            

            orig_Init();
        }

        public void goPirates()
        {
            if (ModGlobals.OpenWorldState)
                return;

            if (ModGlobals.SkipCutscenes && Game1.Globals.MainQuestObjective <= Globaler.MainGameObjective.jungles_headToTown)
            {
                Game1.Globals.MainQuestObjective = Globaler.MainGameObjective.jungles_getBombs;
                return;
            }

            orig_goPirates();
        }
    }
}
