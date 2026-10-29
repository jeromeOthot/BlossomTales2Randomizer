using BlossomTales2.Randomizer.mm;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BlossomTales2
{
    internal class patch_CS_WitchHut : CS_WitchHut
    {
        public extern void orig_Init();
        public extern void orig_cutsceneOver();
        public extern void orig_DidYouBring();
        public extern void orig_continueLessonB();
        public extern void orig_continueLesson1b();
        public extern void orig_continueLesson3b();

        public bool HasJar => Game1.player.Inventory.Contains(EquipableItem.ItemList.Jar_Empty) || Game1.player.Inventory.Contains(EquipableItem.ItemList.Jar_Health) || Game1.player.Inventory.Contains(EquipableItem.ItemList.Jar_ReduceCost)
                              || Game1.player.Inventory.Contains(EquipableItem.ItemList.Jar_DoubleDamage) || Game1.player.Inventory.Contains(EquipableItem.ItemList.Jar_SlowTime) || Game1.player.Inventory.Contains(EquipableItem.ItemList.Jar_BubbleShield)
                              || Game1.player.Inventory.Contains(EquipableItem.ItemList.Jar_ArmorOrbs) || Game1.player.Inventory.Contains(EquipableItem.ItemList.Jar_Resurrection) || Game1.player.Inventory.Contains(EquipableItem.ItemList.Jar_Ghost)
                              || Game1.player.Inventory.Contains(EquipableItem.ItemList.Jar_Fire) || Game1.player.Inventory.Contains(EquipableItem.ItemList.Jar_Speedster);

        public void Init()
        {
            GameLogger.LogInfo("INIT WITCH !!");
            if (Game1.Globals.Witch_State >= 2)
            {
                return;
            }
            foreach (LevelObject levelObject in Game1.CurrentLevel.LevelObjects)
            {
                if (levelObject is NPC_2)
                {
                    ((NPC_2)levelObject).TotalWayPoints = 1;
                    ((NPC_2)levelObject).Position = new Vector3(512f, 0f, 292f);
                }
            }
            Game1.Gui.HideHud = true;
            Game1.player.Se = SpriteEffects.None;
            Game1.player.RemovePlayerControls = true;
            Game1.player.EnteringDoorAmount = 0;
            Game1.player.EnteringDoor = false;
            Game1.player.MoveToPosition(Game1.player.Position + new Vector3(0f, 0f, -64f), 1);
            tweener.Timer(0.5f).OnComplete(delegate
            {
                Game1.Dialoger.AddLine("Witch: Well, it's about time! You are exceptionally late, my dear.", DidYouBring);
            });
        }

        public void cutsceneOver()
        {
            Globaler.MainGameObjective mainGameObjective = Game1.Globals.MainQuestObjective;
            orig_cutsceneOver();

            if (RandomizerSettings.GamePlay.OpenWorldState)
                Game1.Globals.MainQuestObjective = mainGameObjective;
        }

        public void DidYouBring()
        {
            Game1.Dialoger.AddLine("Witch: Did you bring an <B>empty <B>bottle?");
            if (HasJar)
            {
                Game1.Dialoger.AddLine("Witch: Good! Here your lesson.", giveRecipe1);

            }
            else
            {
                Game1.Globals.BorrowedJar = true;

                Game1.Dialoger.AddLine("Witch: You need to bring a bottle for our lesson.");
                Game1.player.RemovePlayerControls = false;
            }
        }

        public void continueLessonB()
        {
            if (!Game1.Globals.Learned_Potions.Contains(EquipableItem.ItemList.Jar_Health))
            {
                Game1.Dialoger.AddLine("Witch: Hello again. You need to learn the health potion recipe to get your next lesson.");
            }
            else
            {
                Game1.Globals.Witch_State = 5;
                Game1.Dialoger.AddLine("Witch: Hello again. Are you back for your second lesson? Let us continue.", giveRecipe2);
            }
        }

        public void continueLesson1b()
        {
            if (!Game1.Globals.Learned_Potions.Contains(EquipableItem.ItemList.Jar_DoubleDamage))
            {
                Game1.Dialoger.AddLine("Witch: Hello again. You need to learn the double dommage potion recipe to get your next lesson.");
            }
            else
            {
                if (Game1.player.Gold > 149)
                {
                    Game1.player.RemovePlayerControls = true;
                    Game1.player.ChangeGoldAmountReflection(-150);
                    Game1.Globals.Witch_State = 8;
                    tweener.Timer(1f).OnComplete(giveRecipe3);
                }
                else
                {
                    Game1.Dialoger.AddLine("Witch: Come back when you have the 150 gold. The final lesson is a good one.");
                }

            }
        }

        public void continueLesson3b()
        {
            Game1.Globals.Witch_State = 6;
            Game1.Dialoger.AddLine("Witch: That's all for today's lesson. Next time we'll learn all about something else.");
        }


        public void giveRecipe1()
        {

            Game1.Gui.AddGuiTicker(EquipableItem.IngredientList.NewRecipe, 1);
            if (!Game1.Globals.Learned_Potions.Contains(EquipableItem.ItemList.Jar_Health))
            {
                Game1.Globals.Learned_Potions.Add(EquipableItem.ItemList.Jar_Health);
            }
            Game1.Globals.BorrowedJar = false;
            Game1.player.RemovePlayerControls = false;
        }

        public void giveRecipe2()
        {

            Game1.Gui.AddGuiTicker(EquipableItem.IngredientList.NewRecipe, 1);
            if (!Game1.Globals.Learned_Potions.Contains(EquipableItem.ItemList.Jar_DoubleDamage))
            {
                Game1.Globals.Learned_Potions.Add(EquipableItem.ItemList.Jar_DoubleDamage);
            }
            Game1.Globals.BorrowedJar = false;
            Game1.player.RemovePlayerControls = false;
        }

        public void giveRecipe3()
        {

            Game1.Gui.AddGuiTicker(EquipableItem.IngredientList.NewRecipe, 1);
            if (!Game1.Globals.Learned_Potions.Contains(EquipableItem.ItemList.Jar_SlowTime))
            {
                Game1.Globals.Learned_Potions.Add(EquipableItem.ItemList.Jar_SlowTime);
            }
            Game1.Globals.BorrowedJar = false;
            Game1.player.RemovePlayerControls = false;
        }
    }
}
