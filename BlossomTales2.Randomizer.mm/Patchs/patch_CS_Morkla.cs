using BlossomTales2.Extensions;
using BlossomTales2.Randomizer.mm;
using Microsoft.Xna.Framework;
using System;

namespace BlossomTales2
{
    //TODO: Refactor conditions if we remove cutscenes.
    internal class patch_CS_Morkla : CS_Morkla
    {
        public extern void orig_Init();
        public extern void orig_morklaDone();
        public extern void orig_fishCaught();
        public extern void orig_goTurtle();
        public extern void orig_piratesPoisoned();
        public extern void orig_lowerMorky();
        public extern void orig_Update(GameTime gameTime);

        public override void Init()
        {
            foreach (Light light in Game1.CurrentLevel.Lights)
            {
                if (light.position.X == 1164f)
                {
                    this.light = light;
                }
            }

            if (Mod_IsMorklaOut())
            {
                placeMorklaOutOfWater();
            }
            else if (Mod_IsMorklaCompleted())
            {
                placeMorklaOutOfWater();
                head.myY = 20;
                Game1.player.RemovePlayerControls = true;
                focusCam = true;
                Game1.FadeNewDoorNumber = 4;
                Game1.FadeNewLevelName = "jungles-24x20.tmx";
                Game1.FadeNewDoorType = 0;
                Game1.FadeNewDoorNumber_B = 4;
                Game1.FadeNewLevelName_B = "jungles-24x20.tmx";
                Game1.FadeNewDoorType_B = 0;
                finalSneeze();
            }
            else
            {
                placeMorklaInWater();
            }
        }

        public void morklaDone()
        {
            Globaler.MainGameObjective mainGameObjective = Game1.Globals.MainQuestObjective;
            orig_morklaDone();

            if (ModGlobals.OpenWorldState)
            {
                Game1Extensions.MarkObjectiveComplete(Globaler.MainGameObjective.jungles_morklaComplete);
                Game1.Globals.MainQuestObjective = mainGameObjective;
            }
        }

        public void fishCaught()
        {
            Globaler.MainGameObjective mainGameObjective = Game1.Globals.MainQuestObjective;
            orig_fishCaught();

            if (ModGlobals.OpenWorldState)
            {
                Game1Extensions.MarkObjectiveComplete(Globaler.MainGameObjective.jungles_fishCrabsMorkla);
                Game1.Globals.MainQuestObjective = mainGameObjective;
            }
        }
        
        public void goTurtle()
        {
            if (Mod_ShouldPlayMorklaIntro())
            {
                if(ModGlobals.SkipCutscenes)
                {
                    if(ModGlobals.OpenWorldState)
                        Game1Extensions.MarkObjectiveComplete(Globaler.MainGameObjective.jungles_talkToMorkla);
                    else
                        Game1.Globals.MainQuestObjective = Globaler.MainGameObjective.jungles_fishCrabsMorkla;
                    return;
                }

                Game1.Gui.HideHud = true;
                Game1.player.MoveToTarget = true;
                Game1.player.RemovePlayerControls = true;
                Game1.player.TargetPosition = new Vector3(1280f, 0f, 1900f);
                focusCam = true;
                focusCameraOnTarget(new Vector2(1280f, 1736f), 3f, morklaTalk);
            }
        }

        public void piratesPoisoned()
        {
            Globaler.MainGameObjective mainGameObjective = Game1.Globals.MainQuestObjective;
            orig_piratesPoisoned();

            if (ModGlobals.OpenWorldState)
            {
                Game1Extensions.MarkObjectiveComplete(Globaler.MainGameObjective.jungles_talkToMorkla);
                Game1.Globals.MainQuestObjective = mainGameObjective;
            }
        }

        public void lowerMorky()
        {
            Globaler.MainGameObjective mainGameObjective = Game1.Globals.MainQuestObjective;
            orig_lowerMorky();

            if (ModGlobals.OpenWorldState)
            {
                Game1Extensions.MarkObjectiveComplete(Globaler.MainGameObjective.jungles_crabsCaughtMorkla);
                Game1.Globals.MainQuestObjective = mainGameObjective;
            }
        }
        public override void Update(GameTime gameTime)
        {
            Mod_BaseUpdate(gameTime);
            door.myY = body.myY;
            doorTop.myY = body.myY;
            slimeOverlay.myX = Game1.player.Position.X;
            slimeOverlay.myZ = Game1.player.Position.Z;
            slimeOverlay.myY = Game1.player.Position.Y;
            if (Mod_IsNotFishingCrabs() || Game1.player.CurrentAnimation == Player.Animations.Fishing || Game1.player.CurrentAnimation == Player.Animations.GetItem)
            {
                return;
            }

            int num = 0;
            foreach (LevelObject levelObject in Game1.CurrentLevel.LevelObjects)
            {
                if (levelObject is FishingSpot)
                {
                    num++;
                }
            }

            if (num < 1)
            {
                fishCaught();
            }
        }


        private bool Mod_IsMorklaOut()
        {
            if (ModGlobals.OpenWorldState)
                return !Game1Extensions.IsObjectiveCompleted(Globaler.MainGameObjective.jungles_crabsCaughtMorkla);
            else
                return Game1.Globals.MainQuestObjective >= Globaler.MainGameObjective.jungles_talkToMorkla && Game1.Globals.MainQuestObjective <= Globaler.MainGameObjective.jungles_crabsCaughtMorkla;
        }

        private bool Mod_ShouldPlayMorklaIntro()
        {
            if(ModGlobals.OpenWorldState)
                return !Game1Extensions.IsObjectiveCompleted(Globaler.MainGameObjective.jungles_talkToMorkla);
            else
                return Game1.Globals.MainQuestObjective == Globaler.MainGameObjective.jungles_talkToMorkla;
        }

        private bool Mod_IsMorklaCompleted()
        {
            if (ModGlobals.OpenWorldState)
                return Game1Extensions.IsObjectiveCompleted(Globaler.MainGameObjective.jungles_morklaGetEyes) && !Game1Extensions.IsObjectiveCompleted(Globaler.MainGameObjective.jungles_morklaComplete);
            else
                return Game1.Globals.MainQuestObjective == Globaler.MainGameObjective.jungles_morklaComplete;
        }

        private bool Mod_IsNotFishingCrabs()
        {
            if (ModGlobals.OpenWorldState)
                return Game1Extensions.IsObjectiveCompleted(Globaler.MainGameObjective.jungles_fishCrabsMorkla);
            else
                return Game1.Globals.MainQuestObjective != Globaler.MainGameObjective.jungles_fishCrabsMorkla;
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
