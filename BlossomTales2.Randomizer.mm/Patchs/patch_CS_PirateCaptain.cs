using BlossomTales2.Extensions;
using Microsoft.Xna.Framework;

namespace BlossomTales2
{
    internal class patch_CS_PirateCaptain : CS_PirateCaptain
    {
        private bool inited;
        private Puppet captain;

        public extern void orig_Init();

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
