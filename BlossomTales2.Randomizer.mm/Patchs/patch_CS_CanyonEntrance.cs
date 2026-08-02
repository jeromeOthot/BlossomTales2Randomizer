using BlossomTales2.Extensions;

namespace BlossomTales2
{
    internal class patch_CS_CanyonEntrance : CS_CanyonEntrance
    {
        public extern void orig_Init();
        public extern void orig_goOwl();
        public extern void orig_openDoor();
        public extern void orig_enterDungeon();

        public override void Init()
        {
            if (Mod_HasNotOpenedDoor())
            {
                placeDoorDown();
            }

            if (Game1.Globals.MainQuestObjective == Globaler.MainGameObjective.canyons_talkToOwlAgain)
            {
                Game1.player.EnteringDoor = false;
                Game1.player.RemovePlayerControls = true;
                Game1.player.Direction = 3;
                talkToOwlAgain();
            }

            if (Mod_IsObjectiveEnterDungeon())
            {
                return;
            }

            foreach (LevelObject levelObject in Game1.CurrentLevel.LevelObjects)
            {
                if (levelObject is NPC_2)
                {
                    levelObject.Alive = false;
                    levelObject.Collidable = false;
                }
            }
        }

        public void goOwl()
        {
            if(ModGlobals.OpenWorldState)
                return;
            else
                orig_goOwl();
        }

        public void openDoor()
        {
            if (!Mod_HasNotOpenedDoor())
                return;

            //Temporarely change objective to pass the if in original method.
            Globaler.MainGameObjective mainGameObjective = Game1.Globals.MainQuestObjective;
            Game1.Globals.MainQuestObjective = Globaler.MainGameObjective.canyons_enterDungeon;
            orig_openDoor();
            Game1.Globals.MainQuestObjective = mainGameObjective;
        }

        public void enterDungeon()
        {
            Globaler.MainGameObjective mainGameObjective = Game1.Globals.MainQuestObjective;
            orig_enterDungeon();

            if (ModGlobals.OpenWorldState)
            {
                Game1Extensions.MarkObjectiveComplete(Globaler.MainGameObjective.canyons_enterDungeon);
                Game1.Globals.MainQuestObjective = mainGameObjective;
            }
        }

        public bool Mod_HasNotOpenedDoor()
        {
            if (ModGlobals.OpenWorldState)
                return !Game1Extensions.IsObjectiveCompleted(Globaler.MainGameObjective.canyons_enterDungeon);
            else
                return Game1.Globals.MainQuestObjective <= Globaler.MainGameObjective.canyons_enterDungeon;
        }

        public bool Mod_IsObjectiveEnterDungeon()
        {
            if (ModGlobals.OpenWorldState)
                return !Game1Extensions.IsObjectiveCompleted(Globaler.MainGameObjective.canyons_enterDungeon);
            else
                return Game1.Globals.MainQuestObjective == Globaler.MainGameObjective.canyons_enterDungeon;
        }
    }
}
