using BlossomTales2.Extensions;

namespace BlossomTales2
{
    public class patch_CS_CastleEntrance : CS_CastleEntrance
    {
        public extern void orig_talk();
        public extern void orig_goPlayer();

        internal void talk()
        {
            if (Mod_HasNotSeenCastleEntranceCutscene())
            {
                Game1.Narrator.AddLine("Grandpa: Through dangers untold and hardships unnumbered...", 12);
                Game1.Narrator.AddLine("Grandpa: <B>Lily had fought her way to the castle beyond the labyrinth gates.", 12);
                Game1.Narrator.AddLine("Grandpa: She was ready to face anything inside, except one thing...", 12);
                Game1.Narrator.AddLine("Lily: What, Grandpa?!", 11);
                Game1.Narrator.AddLine("Chrys: TELL US!", 1);
                Game1.Narrator.AddLine("Grandpa: The chance that she had arrived too late to take back her brother...", 12);
                Game1.Narrator.AddLine("Lily / Chrys: *gasp*", 12, goPlayer);
            }
        }

        public void goPlayer()
        {
            Globaler.MainGameObjective mainGameObjective = Game1.Globals.MainQuestObjective;
            orig_goPlayer();

            if (ModGlobals.OpenWorldState)
            {
                Game1Extensions.MarkObjectiveComplete(Globaler.MainGameObjective.lab_headToTop);
                Game1.Globals.MainQuestObjective = mainGameObjective;
            }
        }

        private bool Mod_HasNotSeenCastleEntranceCutscene()
        {
            if (ModGlobals.OpenWorldState)
                return !Game1Extensions.IsObjectiveCompleted(Globaler.MainGameObjective.lab_findCastleEntrance);
            else
                return Game1.Globals.MainQuestObjective < Globaler.MainGameObjective.lab_headToTop;
        }
    }
}
