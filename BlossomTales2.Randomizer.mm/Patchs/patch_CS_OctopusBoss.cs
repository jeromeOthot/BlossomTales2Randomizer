using BlossomTales2.Extensions;

namespace BlossomTales2
{
    internal class patch_CS_OctopusBoss :CS_OctopusBoss
    {
        public extern void orig_switchMaps();

        internal void switchMaps()
        {
            Mod_CompleteGetEyesObjective();
            Game1.FadeOut = true;
            Game1.TransitionType = Game1.Transitions.FadeBlack;
            Game1.FadeAlpha = 1;
            Game1.FadeNewLevelName = "jungles-24x20.tmx";
        }

        private void Mod_CompleteGetEyesObjective()
        {
            if (ModGlobals.OpenWorldState)
                Game1Extensions.MarkObjectiveComplete(Globaler.MainGameObjective.jungles_morklaGetEyes);
            else
                Game1.Globals.MainQuestObjective = Globaler.MainGameObjective.jungles_morklaComplete;
        }
    }
}
