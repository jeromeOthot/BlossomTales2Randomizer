namespace BlossomTales2
{
    internal class patch_CS_ChrysJail : CS_ChrysJail
    {
        public extern void orig_loadMap();

        public void loadMap()
        {
            Globaler.MainGameObjective mainGameObjective = Game1.Globals.MainQuestObjective;
            orig_loadMap();
            if (ModGlobals.OpenWorldState)
                Game1.Globals.MainQuestObjective = mainGameObjective;
        }
    }
}
