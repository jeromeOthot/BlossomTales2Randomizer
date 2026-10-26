using BlossomTales2.Randomizer.mm;

namespace BlossomTales2
{
    public class patch_CS_ChrysBaseball : CS_ChrysBaseball
    {
        public extern void orig_loadMap();

        public void loadMap()
        {
            Globaler.MainGameObjective mainGameObjective = Game1.Globals.MainQuestObjective;
            orig_loadMap();
            if (RandomizerSettings.GamePlay.OpenWorldState)
                Game1.Globals.MainQuestObjective = mainGameObjective;
        }
    }
}
