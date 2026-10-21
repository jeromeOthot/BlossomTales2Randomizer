using BlossomTales2.Randomizer.mm;

namespace BlossomTales2
{
    internal class patch_CS_WitchHut : CS_WitchHut
    {
        public extern void orig_cutsceneOver();

        public void cutsceneOver()
        {
            Globaler.MainGameObjective mainGameObjective = Game1.Globals.MainQuestObjective;
            orig_cutsceneOver();

            if (RandomizerSettings.GamePlay.OpenWorldState)
                Game1.Globals.MainQuestObjective = mainGameObjective;
        }
    }
}
