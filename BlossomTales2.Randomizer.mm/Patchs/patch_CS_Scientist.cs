// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.Collections.Generic;
using BlossomTales2.Extensions;
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

        public extern void orig_Init();
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

        public void changeLevel()
        {
            Globaler.MainGameObjective mainGameObjective = Game1.Globals.MainQuestObjective;
            orig_changeLevel();

            if (ModGlobals.OpenWorldState)
            {
                Game1Extensions.MarkObjectiveComplete(Globaler.MainGameObjective.dark_fightScientist);
                Game1.Globals.MainQuestObjective = mainGameObjective;
            }
        }

        private bool Mod_HasOpenedBossChest()
        {
            return Game1Extensions.HasLevelPermaObject("Chest");
        }
    }
}
