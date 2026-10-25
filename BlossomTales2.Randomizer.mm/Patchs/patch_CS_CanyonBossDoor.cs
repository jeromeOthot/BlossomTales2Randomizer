using BlossomTales2.Extensions;
using BlossomTales2.Randomizer.mm;

namespace BlossomTales2
{
    public class patch_CS_CanyonBossDoor : CS_CanyonBossDoor
    {
        public extern void orig_openDoor();

        public override void Init()
        {
            if (Mod_HasNotDefeatedGolemBoss())
                placeDoorDown();
        }

        public void openDoor()
        {
            Globaler.MainGameObjective mainGameObjective = Game1.Globals.MainQuestObjective;
            orig_openDoor();

            if (RandomizerSettings.GamePlay.OpenWorldState)
            {
                Game1Extensions.MarkObjectiveComplete(Globaler.MainGameObjective.canyons_headToGolem);
                Game1.Globals.MainQuestObjective = mainGameObjective;
            }
        }

        private bool Mod_HasNotDefeatedGolemBoss()
        {
            if (RandomizerSettings.GamePlay.OpenWorldState)
                return !Game1Extensions.IsObjectiveCompleted(Globaler.MainGameObjective.canyons_headToGolem);
            else
                return Game1.Globals.MainQuestObjective <= Globaler.MainGameObjective.canyons_headToGolem;
        }
    }
}
