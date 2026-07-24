using BlossomTales2.Randomizer.mm;
using Microsoft.Xna.Framework;
using System;

namespace BlossomTales2
{
    class patch_CS_LilysHouse : CS_LilysHouse
    {
        private Puppet lily = new Puppet("lily", new Vector3(0f, 0.0f, 0f));
        private Puppet grandma = new Puppet("Fake grandma", new Vector3(0f, 0f, 0f));

        public extern void orig_equipShield();
        public extern void orig_equipSword();
        public extern void orig_endCutScene();
        
        public void equipShield()
        {
            //TODO: Move later if we skip cutscene.
            Game1.player.SwordLevel = 0;
            lily.play("getItem");
            Game1.playSoundCue("newWeapon");
            Game1.player.Position = lily.getPosition();
            GameLogger.LogInfo(Game1.CurrentLevel.Name + " " + grandma.name + " " + grandma.getPosition());
            EquipableItem.ItemList item = RandomizerSingleton.Instance.GetItemAtLocation(new LocationId(Game1.CurrentLevel.Name, grandma.name + "_1", grandma.getPosition()));
            Game1.Particles.Add(new P_GetItem(lily.getPosition() + new Vector3(0.0f, 100f, 0.0f), (int)item));
            Game1.Particles.Add(new GetItemLight(lily.getPosition()));
            Game1.player.GiveItemReflection(item, false);
            tweener.Timer(2.3f).OnComplete(delegate
            {
                lily.play("idleDown");
                tweener.Timer(0.2f).OnComplete(equipSword);
            });
        }

        public void equipSword()
        {            
            lily.play("getItem");
            Game1.playSoundCue("newWeapon");
            EquipableItem.ItemList item = RandomizerSingleton.Instance.GetItemAtLocation(new LocationId(Game1.CurrentLevel.Name, grandma.name + "_2", grandma.getPosition()));
            Game1.Particles.Add(new P_GetItem(lily.getPosition() + new Vector3(0f, 100f, 0f), (int)item));
            Game1.Particles.Add(new GetItemLight(lily.getPosition()));
            Game1.player.GiveItemReflection(item, false);
            tweener.Timer(2.3f).OnComplete(delegate
            {
                lily.play("idleDown");
                if (ModGlobals.SkipCutscenes)
                    tweener.Timer(0.2f).OnComplete(endCutScene);
                else
                    Game1.Dialoger.AddLine("Lily: Thanks, Grandma. Love you!", endCutScene);
            });
        }

        //hack pour skipper le tutoriel festival
        public void endCutScene()
        {
            if(!ModGlobals.SkipFestival)
            {
                orig_endCutScene();
                return;
            }

            //On change de quete pour skipper le tutoriel festival
            Game1.Globals.MainQuestObjective = Globaler.MainGameObjective.intro_getLantern;
            giveControlToPlayer(lily, false, 3);
            Game1.LOPuppets.Clear();
           // this.bedSheetLily.Zdepth = -99.5f;
          //  this.bedSheetChrys.Zdepth = -99.5f;
            //Game1.LOPuppets.Add(this.bedSheetLily);
            //Game1.LOPuppets.Add(this.bedSheetChrys);
            Game1.Gui.HideHud = false;
            grandma.play("hide");
            NPC npc = new NPC(new Vector3(grandma.myX, grandma.myY, grandma.myZ));
            npc.IDNumber = 6;
            npc.linePointer = 199;
            Game1.CurrentLevel.LevelObjects.Add(npc);
            Running = false;
        }
    }
}

