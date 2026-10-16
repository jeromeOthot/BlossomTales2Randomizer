using BlossomTales2.Randomizer.mm;
using Microsoft.Xna.Framework;

namespace BlossomTales2
{
    class patch_CS_LilysHouse : CS_LilysHouse
    {
        private Puppet lily = new Puppet("lily", new Vector3(0f, 0.0f, 0f));
        private Puppet grandma = new Puppet("Fake grandma", new Vector3(0f, 0f, 0f));

        public extern void orig_Init();
        public extern void orig_startScene();
        public extern void orig_equipShield();
        public extern void orig_equipSword();
        public extern void orig_endCutScene();

          public void Init()
          {
              GameLogger.LogInfo("Intro grand-pere Init " + ModGlobals.SkipCutscenes);
              //GameLogger.LogInfo("Test");
              //endCutScene();
              GameLogger.LogInfo("Fin Test");
              if(ModGlobals.SkipCutscenes)
                  tweener.Timer(0.2f).OnComplete(equipShield);
              else
                  orig_Init();
          }

        public void startScene()
        {
            if (ModGlobals.SkipCutscenes)
                this.endCutScene();
            else
                orig_startScene();
        }

        public void equipShield()
        {
            if (ModGlobals.SkipCutscenes)
            {
                lily = new Puppet("lily", new Vector3(480, 0, 428));
            }
            RandomizerSingleton.Instance.GiveItemAtLocation("Grandma 1", new Vector3(480f, 0f, 328f));

            Game1.player.SwordLevel = 0;
            Game1.player.Position = lily.getPosition();

            lily.play("getItem");
            Game1.playSoundCue("newWeapon");
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
            RandomizerSingleton.Instance.GiveItemAtLocation("Grandma 2", new Vector3(480f, 0f, 328f));

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
        public void  endCutScene()
        {
            if(!ModGlobals.SkipFestival)
            {
                orig_endCutScene();
                return;
            }

            //On change de quete pour skipper le tutoriel festival
            if(ModGlobals.OpenWorldState)
                Game1.Globals.MainQuestObjective = Globaler.MainGameObjective.lab_openDoor;
            else
                Game1.Globals.MainQuestObjective = Globaler.MainGameObjective.intro_enterFestival;

            if (ModGlobals.SkipCutscenes)
                Game1.Globals.blossomInvasionState = 2;

            Game1.Globals.Engineer_State = 1;

            giveControlToPlayer(lily, false, 3);
            Game1.LOPuppets.Clear();
           // this.bedSheetLily.Zdepth = -99.5f;
          //  this.bedSheetChrys.Zdepth = -99.5f;
            //Game1.LOPuppets.Add(this.bedSheetLily);
            //Game1.LOPuppets.Add(this.bedSheetChrys);
            Game1.Gui.HideHud = false;
            //grandma.play("hide");
            /*
            NPC npc = new NPC(new Vector3(480, 0, 328));
            npc.IDNumber = 6;
            npc.linePointer = 199;
            Game1.CurrentLevel.LevelObjects.Add(npc);
            Running = false; */

            //Mettre cette ligne si on veut commencer une game avec tout pour debbuger
            //patch_Game1.UnlockAllForDebug();
        }

        private void Mod_GrandmaGiveItem(string locationName)
        {
            RandomizerSingleton.Instance.GiveItemAtLocation(grandma.name + locationName, grandma.getPosition());
        }
    }
}

