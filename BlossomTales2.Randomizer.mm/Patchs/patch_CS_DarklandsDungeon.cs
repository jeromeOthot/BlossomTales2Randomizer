using BlossomTales2.Extensions;
using Microsoft.Xna.Framework;

namespace BlossomTales2
{
    public class patch_CS_DarklandsDungeon : CS_DarklandsDungeon
    {
        private Puppet door;
        private bool inited;

        public extern void orig_Init();
        public extern void orig_goPlayer();

        public override void Init()
        {
            if (Mod_IsEnterDungeonObjectiveCompleted())
            {
                foreach (LevelObject levelObject in Game1.CurrentLevel.LevelObjects)
                {
                    if (levelObject is Sign)
                    {
                        levelObject.Alive = false;
                    }
                }
                inited = true;
            }
            else
            {
                door = new Puppet("dlandsDoor", new Vector3(1216f, 0f, 784f));
                door.collide = true;
                puppets.Add(door);
            }
            if (Game1.Globals.MainQuestObjective == Globaler.MainGameObjective.lab_talkToBlacksmith && Game1.Globals.mansionShowUpdate)
            {
                Game1.Globals.mansionShowUpdate = false;
                Game1.Gui.AddGuiTicker(EquipableItem.IngredientList.MapUpdated, 1);
            }
        }
        public void goPlayer()
        {
            Globaler.MainGameObjective mainGameObjective = Game1.Globals.MainQuestObjective;
            orig_goPlayer();

            if (ModGlobals.OpenWorldState)
            {
                Game1Extensions.MarkObjectiveComplete(Globaler.MainGameObjective.dark_enterDungeon);
                Game1.Globals.MainQuestObjective = mainGameObjective;
            }
        }


        private bool Mod_IsEnterDungeonObjectiveCompleted()
        {
            if (ModGlobals.OpenWorldState)
                return Game1Extensions.IsObjectiveCompleted(Globaler.MainGameObjective.dark_enterDungeon);
            else
                return Game1.Globals.MainQuestObjective >= Globaler.MainGameObjective.dark_openMiniBossDoor;
        }
    }
}
