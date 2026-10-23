using System.Collections.Generic;
using BlossomTales2.Extensions;
using BlossomTales2.Randomizer.mm;
using Microsoft.Xna.Framework;

namespace BlossomTales2
{
    public class patch_CS_BossScientist : CS_BossScientist
    {
        private Puppet coilLeft;
        private Puppet coilLeftLight;
        private Puppet coilRight;
        private Puppet coilRightLight;
        private Puppet table;

         public extern void orig_changeLevel();

        public override void Init()
        {
            coilLeft = new Puppet("scientistCoils", new Vector3(324f, 0f, 84f));
            coilLeftLight = new Puppet("scientistCoilLight", new Vector3(324f, 0f, 84f));
            coilLeftLight.IsLight = true;
            coilRight = new Puppet("scientistCoils", new Vector3(684f, 0f, 84f));
            coilRightLight = new Puppet("scientistCoilLight", new Vector3(684f, 0f, 84f));
            coilRightLight.IsLight = true;
            table = new Puppet("scientistTable", new Vector3(636f, 0f, 192f));
            puppets.AddRange(new List<Puppet> { coilLeft, coilRight, table });
            puppetList.AddRange(new List<Puppet> { coilLeft, coilRight, table });
            Game1.LOPuppets.Add(coilLeftLight);
            Game1.LOPuppets.Add(coilRightLight);
            table.play("on");
            coilLeft.play("off");
            coilLeftLight.play("hide");
            coilRightLight.play("hide");
            coilRight.play("offLeft");
            if (!Game1.Globals.Def_BossScientist)
            {
                Vector3 position = new Vector3(Game1.CurrentLevel.Width * 32, 0f, 304f);
                Game1.CurrentLevel.Enemies.Add(new BossScientist(position));
                initScene();
                return;
            }
            Chest chest = new Chest(new Vector3(Game1.CurrentLevel.Width * 32, 0f, 448f));
            chest.IDNumber = 23;
            if (Mod_HasOpenedBossChest())
            {
                chest.Frame = 7;
            }
            Game1.CurrentLevel.LevelObjects.Add(chest);
        }

        public void postFight()
        {
            Game1.Gui.HideHud = true;
            Game1.player.Direction = 3;
            if (RandomizerSettings.GamePlay.SkipCutscenes)
            {
                tweener.Timer(1f).OnComplete(changeLevel);
            }
            else
            {
                Game1.Narrator.AddLine("Grandpa: At long last, <B>Lily possessed all three of the fabled key pieces!", 12);
                Game1.Narrator.AddLine("Grandpa: Well aware of her progress, the <A>Minotaur <A>King began setting his crafty plans in motion.", 12, changeLevel);
            }
        }

        public void changeLevel()
        {
            if (!RandomizerSettings.GamePlay.SkipCutscenes)
            {
                orig_changeLevel();
                return;
            }

            //Copy-pasted from CS_GolemForge.loadMap
            Game1.FadeOut = true;
            Game1.FadeAlpha = 255;
            Game1.TransitionType = Game1.Transitions.FadeBlack;
            Game1.FadeNewLevelName = "overworld-25x16.tmx";
            Game1.FadeNewDoorNumber = 1;
            Game1.FadeNewDoorType = 0;
            Game1.player.RemovePlayerControls = false;
            Game1.fadeController.Clear();
        }

        private bool Mod_HasOpenedBossChest()
        {
            return Game1Extensions.HasLevelPermaObject("Chest");
        }
    }
}
