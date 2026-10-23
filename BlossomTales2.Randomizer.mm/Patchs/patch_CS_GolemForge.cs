using BlossomTales2.Extensions;
using BlossomTales2.Randomizer.mm;

namespace BlossomTales2
{
    public class patch_CS_GolemForge : CS_GolemForge
    {
        public extern void orig_loadMap();

        public void loadMap()
        {
            orig_loadMap();
            if (RandomizerSettings.GamePlay.OpenWorldState)
            {
                Game1Extensions.MarkObjectiveComplete(Globaler.MainGameObjective.dark_fightScientist);
                Game1.Globals.MainQuestObjective = Globaler.MainGameObjective.lab_openDoor;
            }
        }
    }
}
