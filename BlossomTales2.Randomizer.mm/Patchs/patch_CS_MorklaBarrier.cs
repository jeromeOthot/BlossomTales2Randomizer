using BlossomTales2.Extensions;
using Microsoft.Xna.Framework;

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
            {
                if (!Game1Extensions.IsObjectiveCompleted(Globaler.MainGameObjective.jungles_headToTown))
                {
                    SpawnPirates();
                    Game1Extensions.MarkObjectiveComplete(Globaler.MainGameObjective.jungles_headToTown);
                }
                return;
            }

            if (ModGlobals.SkipCutscenes && Game1.Globals.MainQuestObjective <= Globaler.MainGameObjective.jungles_headToTown)
            {
                Game1.Globals.MainQuestObjective = Globaler.MainGameObjective.jungles_getBombs;
                return;
            }

            orig_goPirates();
        }

        private void SpawnPirates()
        {
            PirateSlash pirateSlash = new PirateSlash(new Vector3(1232f, 0f, 628f));
            PirateRapier pirateRapier = new PirateRapier(new Vector3(1404f, 0f, 628f));
            pirateSlash.AttackRadius = new Point(1000, 500);
            pirateRapier.AttackRadius = new Point(1000, 500);
            Game1.CurrentLevel.Enemies.Add(pirateSlash);
            Game1.CurrentLevel.Enemies.Add(pirateRapier);
        }
    }
}
