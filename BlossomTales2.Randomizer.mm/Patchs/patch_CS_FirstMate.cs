using BlossomTales2.Extensions;
using Microsoft.Xna.Framework;

namespace BlossomTales2
{
    internal class patch_CS_FirstMate : CS_FirstMate
    {

        public override void Init()
        {
            if (Mod_ShouldStartMonkeyFight())
            {
                Game1.SwitchBGMusic("stop", 0.01f);
                Game1.CurrentLevel.Enemies.Add(new BossFirstMate(new Vector3(Game1.CurrentLevel.Width * 32, 0f, 224f)));
                Game1.player.RemovePlayerControls = true;
                Game1.Gui.HideHud = true;
                Game1.player.MoveToPosition(new Vector3(Game1.CurrentLevel.Width * 32, 0f, Game1.player.Position.Z - 64f), 1);
                Game1.player.EnteringDoor = false;
                Game1.player.EnteringDoorAmount = 0;
                tweener.Timer(0.5f).OnComplete(closeGate);
                tweener.Timer(1.2f).OnComplete(firstDialog);
            }
        }

        public bool Mod_ShouldStartMonkeyFight()
        {
            if (ModGlobals.OpenWorldState)
                return !Game1Extensions.IsObjectiveCompleted(Globaler.MainGameObjective.jungles_getBombs);
            else
                return Game1.Globals.MainQuestObjective <= Globaler.MainGameObjective.jungles_getBombs;
        }
    }
}
