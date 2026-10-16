using BlossomTales2.Randomizer.mm;
using Microsoft.Xna.Framework;

namespace BlossomTales2
{
    class patch_CS_LilysHouse : CS_LilysHouse
    {
        private Puppet lily;

        public extern void orig_Init();
        public extern void orig_endCutScene();

          public void Init()
          {
              Game1.player.SwordLevel = 0;
              if(ModGlobals.SkipCutscenes)
                  Mod_Init();
              else
                  orig_Init();
          }

        public void equipShield()
        {
            lily.play("getItem");
            Game1.playSoundCue("newWeapon");
            Game1.player.Position = lily.getPosition();
            Mod_GrandmaGiveItem("Grandma 1");
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
            Mod_GrandmaGiveItem("Grandma 2");
            tweener.Timer(2.3f).OnComplete(delegate
            {
                lily.play("idleDown");
                if (ModGlobals.SkipCutscenes)
                    tweener.Timer(0.2f).OnComplete(endCutScene);
                else
                    Game1.Dialoger.AddLine("Lily: Thanks, Grandma. Love you!", endCutScene);
            });
        }

        public void  endCutScene()
        {
            if(!ModGlobals.SkipCutscenes)
            {
                orig_endCutScene();
                if (ModGlobals.OpenWorldState)
                {
                    Game1.Globals.MainQuestObjective = Globaler.MainGameObjective.lab_openDoor;
                    Game1.Globals.blossomInvasionState = 2;
                    Game1.Globals.Engineer_State = 1;
                }
                return;
            }

            //On change de quete pour skipper le tutoriel festival
            if (ModGlobals.OpenWorldState)
            {
                Game1.Globals.MainQuestObjective = Globaler.MainGameObjective.lab_openDoor;
                Game1.Globals.blossomInvasionState = 2;
                Game1.Globals.Engineer_State = 1;
            }
            else
            {
                Game1.Globals.MainQuestObjective = Globaler.MainGameObjective.intro_enterFestival;
            }

            giveControlToPlayer(lily, false, 3);
            Game1.LOPuppets.Clear();
            Game1.Gui.HideHud = false;
            Running = false;

            //Mettre cette ligne si on veut commencer une game avec tout pour debbuger
            //patch_Game1.UnlockAllForDebug();
        }

        private void Mod_Init()
        {
            Game1.player.StopUpdating = true;
            Game1.player.StopDrawing = true;
            Game1.Gui.HideHud = true;
            Game1.Gui.HudOpacity = 0f;
            lily = new Puppet("lily", new Vector3(480, 0, 328));
            lily.play("idleDown");
            puppetList.Add(lily);
            puppets.Add(lily);
            Game1.player.EnteringDoor = false;
            if (puppetList.Count > 0)
                targetPuppet = puppetList[0];

            tweener.Timer(0.5f).OnComplete(equipShield);
        }

        private void Mod_GrandmaGiveItem(string locationName)
        {
            RandomizerSingleton.Instance.GiveItemAtLocation(locationName, Vector3.Zero);
        }
    }
}

