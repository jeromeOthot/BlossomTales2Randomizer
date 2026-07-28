using BlossomTales2.Extensions;
using Microsoft.Xna.Framework;

namespace BlossomTales2
{
    internal class patch_CS_PirateCaptain : CS_PirateCaptain
    {
        private bool inited;
        private Puppet captain;

        public extern void orig_Init();
        public extern void orig_initScene();

        public override void Init()
        {
            if (Mod_IsPirateDefeated())
            {
                inited = true;
                Chest chest = new Chest(new Vector3(Game1.CurrentLevel.Width * 32, 0f, 640f));
                chest.IDNumber = 11;
                if (Mod_HasOpenedChest())
                {
                    chest.Frame = 7;
                }
                else
                {
                    Game1.WaterLevelUp = false;
                }

                Game1.CurrentLevel.LevelObjects.Add(chest);
            }
            else
            {
                captain = new Puppet("pirateCaptain", new Vector3(Game1.CurrentLevel.Width * 32, 0f, 256f));
                captain.play("swimming");
                captain.Alpha = 0f;
                puppets.Add(captain);
            }
        }

        public void initScene()
        {
            Game1.SwitchBGMusic("stop", 0.01f);
            DoorGate doorGate = new DoorGate(new Vector3(736f, 0f, 1480f));
            doorGate.Velocity.X = 2f;
            doorGate.Velocity.Y = 1f;
            doorGate.Row = 2;
            Game1.CurrentLevel.LevelObjects.Add(doorGate);
            Game1.player.RemovePlayerControls = true;
            Game1.Gui.HideHud = true;
            Mod_MovePlayerPosition();
            focusCameraOnTarget(new Vector2(Game1.CurrentLevel.Width * 32, 480f), 2f, raiseWater);
        }

        private void Mod_MovePlayerPosition()
        {
            if (Game1.player.Position.Z > 480f)
                Game1.player.MoveToPosition(new Vector3(Game1.CurrentLevel.Width * 32, 0f, Game1.player.Position.Z - 112f), 1);
        }

        private bool Mod_IsPirateDefeated()
        {
            if (ModGlobals.OpenWorldState)
                return Game1Extensions.IsObjectiveCompleted(Globaler.MainGameObjective.jungles_morklaEnter);
            else
                return Game1.Globals.MainQuestObjective >= Globaler.MainGameObjective.jungles_pirateDefeated;
        }

        private bool Mod_HasOpenedChest()
        {
            return Game1Extensions.HasLevelPermaObject("Chest");
        }
    }
}
