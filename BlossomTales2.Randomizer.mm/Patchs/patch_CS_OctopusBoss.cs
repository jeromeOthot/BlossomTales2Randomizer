using BlossomTales2.Extensions;
using Microsoft.Xna.Framework;

namespace BlossomTales2
{
    internal class patch_CS_OctopusBoss : CS_OctopusBoss
    {
        public extern void orig_switchMaps();

        public override void Init()
        {
            if (!Game1.Globals.Def_BossOctopus)
            {
                Game1.SwitchBGMusic("stop", 0.01f);
                Game1.Gui.HideHud = true;
                Game1.player.RemovePlayerControls = true;
                Game1.player.EnteringDoor = false;
                Game1.player.EnteringDoorAmount = 0;
                focusCameraOnTarget(new Vector2(Game1.CurrentLevel.Width * 32, 640f), 2.5f, shutDoor);
                Game1.player.MoveToPosition(new Vector3(Game1.CurrentLevel.Width * 32, 0f, 768f), 1);
            }
            else
            {
                Chest chest = new Chest(new Vector3(Game1.CurrentLevel.Width * 32, 0f, Game1.CurrentLevel.Height * 32));
                chest.IDNumber = 21;
                if (Mod_HasOpenedBossChest())
                {
                    chest.Frame = 7;
                }
                Game1.CurrentLevel.LevelObjects.Add(chest);
            }
        }

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

        private bool Mod_HasOpenedBossChest()
        {
            return Game1Extensions.HasLevelPermaObject("Chest");
        }
    }
}
