using BlossomTales2.Extensions;
using BlossomTales2.Randomizer.mm;
using Microsoft.Xna.Framework;

namespace BlossomTales2
{
    public class patch_CS_VampireBoss : CS_VampireBoss
    {
        private bool inited;
        private Puppet coffinOpen;

        public override void Init()
        {
            if (Mod_HasNotCompletedVampireObjective())
            {
                coffinOpen = new Puppet("coffinOpen", new Vector3(748f, 0f, 188f));
                coffinOpen.play("closed");
                puppets.Add(coffinOpen);
                return;
            }
            inited = true;
            Chest chest = new Chest(new Vector3(Game1.CurrentLevel.Width * 32, 0f, 448f));
            chest.IDNumber = 13;
            if (Mod_HasOpenedChest())
            {
                chest.Frame = 7;
            }
            Game1.CurrentLevel.LevelObjects.Add(chest);
        }

        public override void Update(GameTime gameTime)
        {
            if (focusCam && Mod_HasOpenedChest() && Game1.player.CurrentAnimation != Player.Animations.GetItem)
            {
                focusCam = false;
                Game1.player.HasMoved = false;
                Game1.player.CamOffset = new Vector2(0f, 0f);
            }
            if (!inited && Game1.player.Position.Z < 1280f)
            {
                inited = true;
                initScene();
            }
            Mod_BaseUpdate(gameTime);
        }

        private bool Mod_HasNotCompletedVampireObjective()
        {
            if (RandomizerSettings.GamePlay.OpenWorldState)
                return !Game1Extensions.IsObjectiveCompleted(Globaler.MainGameObjective.dark_headToVlad);
            else
                return Game1.Globals.MainQuestObjective <= Globaler.MainGameObjective.dark_headToVlad;
        }

        private bool Mod_HasOpenedChest()
        {
            return Game1Extensions.HasLevelPermaObject("Chest");
        }

        private void Mod_BaseUpdate(GameTime gameTime)
        {
            //Copy-pasting base because it somehow crash at launch
            tweener.Update((float)gameTime.ElapsedGameTime.TotalSeconds);
            if (focusCam)
            {
                overrideCamera(CameraPosition, 1f);
            }

            for (int i = 0; i < puppets.Count; i++)
            {
                puppets[i].Update(gameTime);
                if (!puppets[i].Alive)
                {
                    puppets.RemoveAt(i);
                    i--;
                }
            }
        }
    }
}
