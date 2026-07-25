namespace BlossomTales2
{
    internal class patch_CS_WitchHut : CS_WitchHut
    {
        public extern void orig_cutsceneOver();

        public void cutsceneOver()
        {
            Globaler.MainGameObjective mainGameObjective = Game1.Globals.MainQuestObjective;
            orig_cutsceneOver();

            if (ModGlobals.OpenWorldState)
                Game1.Globals.MainQuestObjective = mainGameObjective;
        }
    }
}
