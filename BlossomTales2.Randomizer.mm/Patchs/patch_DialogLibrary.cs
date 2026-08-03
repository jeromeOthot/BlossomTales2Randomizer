using BlossomTales2.Extensions;
using BlossomTales2.Randomizer.mm;
using Microsoft.Xna.Framework;

namespace BlossomTales2
{
    internal class patch_DialogLibrary : DialogLibrary
    {
        private static string[] randomLines;

        public static extern void orig_NPC_RunLine(int number, int dialogPointer, Vector2 position);
        public static extern void orig_LineTrigger(string Event, int choice);

        public static void NPC_RunLine(int number, int dialogPointer, Vector2 position)
        {
            switch (dialogPointer)
            {
                case 81:
                    MorklaDialog();
                    break;
                case 102:
                    BlacksmithDialog();
                    break;
                default:
                    orig_NPC_RunLine(number, dialogPointer, position);
                    break;
            }
        }

        public static void LineTrigger(string Event, int choice)
        {
            switch (Event)
            {
                case "giveBow":
                    GiveBowEvent();
                    break;
                default:
                    orig_LineTrigger(Event, choice);
                    break;
            }
        }

        private static void MorklaDialog()
        {
            if (Mod_ShouldFishCrabs())
            {
                randomLines = new string[4] { "<P>Morkla: Those cursed pirates! They filled my waters with foul sea creatures! Use a <R>Fishing <R>Rod to get them out!", "<P>Morkla: Please, use a <R>Fishing <R>Rod to remove these crabs from around me!", "<P>Morkla: The pirates dumped crabs in the waters around my shell! Use a <R>Fishing <R>Rod to get them out!", "<P>Morkla: You have to help me, you're my only hope." };
                if (!Game1.player.Inventory.Contains(EquipableItem.ItemList.FishingRod))
                {
                    randomLines = new string[1] { "<P>Morkla: Go to <J>Anchortown and get a <R>Fishing <R>Rod. Use it to fish the crabs from the waters around my shell!" };
                }

                Game1.Dialoger.AddLine(randomLines[Game1.RandomNumber.Next(randomLines.Length)]);
            }
            else if (Mod_ShouldPlayLowerMorklaDialog())
            {
                Game1.Dialoger.AddLine("<P>Morkla: You need a key to enter the labyrinth. I ate it long ago.", "lowerMorkla");
            }
            else
            {
                randomLines = new string[3] { "<P>Morkla: The <N>Sunkiss <N>Canyons to the <R>West are a dangerous place...", "<P>Morkla: We do hope you find your brother.", "<P>Morkla: Good luck on your quest." };
                Game1.Dialoger.AddLine(randomLines[Game1.RandomNumber.Next(randomLines.Length)]);
            }
        }

        private static void BlacksmithDialog()
        {
            if (Game1.Globals.MainQuestObjective == Globaler.MainGameObjective.lab_talkToBlacksmith)
            {
                Game1.Dialoger.AddLine("Blacksmith: Ah, you're still alive and well! Thanks to my superior quality weapons, no doubt.");
                Game1.Dialoger.AddLine("Blacksmith: Oooh, what's this? These look to be key pieces! Yes, very old, very powerful.");
                Game1.Dialoger.AddLine("Blacksmith: I can't help you, lass. You see, that which magic separated must be reforged in like manner.");
                Game1.Dialoger.AddLine("Blacksmith: Since these relics are part of our past, I'd ask your Grandma. She's our town's very own historian!");
                Game1.Globals.MainQuestObjective = Globaler.MainGameObjective.lab_talkToGrandma;
            }
            else if (Mod_HasNotCompletedMorkla())
            {
                if (Game1.Globals.Blacksmith_State == 0)
                {
                    Game1.Globals.Blacksmith_State = 1;
                    Game1.Dialoger.AddLine("Blacksmith: <B>Lily! I've been trying to think of some way I could help you on your quest to rescue your brother.");
                    Game1.Dialoger.AddLine("Blacksmith: There is a very ancient weapon called a <R>Bow. It shoots pointy things called arrows.");
                    Game1.Dialoger.AddLine("Blacksmith: I could build one if I had the materials. For some reason I'm not getting shipments from <J>Anchortown.");
                    Game1.Dialoger.AddLine("Blacksmith: If you're over that way maybe you could check things out.");
                }
                else if (Game1.Globals.Blacksmith_State == 1)
                {
                    Game1.Globals.Blacksmith_State = 2;
                    Game1.Dialoger.AddLine("Blacksmith: The <R>Bow I'm going to build you is going to be great!");
                    Game1.Dialoger.AddLine("Blacksmith: Have you figured out why I stopped getting shipments from <J>Anchortown?");
                }
                else if (Game1.Globals.Blacksmith_State == 2)
                {
                    Game1.Dialoger.AddLine("Blacksmith: Have you figured out why I stopped getting shipments from <J>Anchortown?");
                }
            }
            else if (Game1.Globals.Blacksmith_State == 0)
            {
                Game1.Globals.Blacksmith_State = 1;
                Game1.Dialoger.AddLine("Blacksmith: <B>Lily! I've been trying to think of some way I could help you on your quest to rescue your brother.");
                Game1.Dialoger.AddLine("Blacksmith: There is a very ancient weapon called a <R>Bow. It shoots pointy things called arrows.");
                Game1.Dialoger.AddLine("Blacksmith: I could make you one, but the materials are expensive. It would cost you <Y>200 <Y>gold <Y>coins.");
                Game1.Dialoger.AddLine("Blacksmith: Should I get started?", "buyBow", new string[2] { "Pay 200 gold", "Not right now" });
            }
            else if (Game1.Globals.Blacksmith_State == 1 || Game1.Globals.Blacksmith_State == 2)
            {
                Game1.Dialoger.AddLine("Blacksmith: Now that I'm getting shipments from <J>Anchortown again, I can make you a <R>Bow!");
                Game1.Dialoger.AddLine("Blacksmith: It'll cost you, of course. Blacksmith's gotta make a living.", "buyBow", new string[2] { "Pay 200 gold", "Not right now" });
            }
            else if (Game1.Globals.Blacksmith_State == 3)
            {
                if (Mod_HasReceivedBlacksmithItem())
                {
                    Game1.Dialoger.AddLine("Blacksmith: Take it outside if you want to shoot it.");
                }
                else
                {
                    Game1.Dialoger.AddLine("Blacksmith: I finished your <R>Bow!", "giveBow");
                }
            }
            else if (Game1.Globals.Blacksmith_State == 4)
            {
                Game1.Dialoger.AddLine("Blacksmith: You enjoying that <R>Bow?");
            }
        }

        private static void GiveBowEvent()
        {
            Game1.Globals.Blacksmith_State = 4;
            EquipableItem.ItemList item = RandomizerSingleton.Instance.GetItemAtLocation(new LocationId(Game1.CurrentLevel.Name, "npc21", Vector3.Zero));
            Game1.player.GiveItemReflection(item);
            Game1.player.Direction = 3;
        }
        private static bool Mod_ShouldFishCrabs()
        {
            if (ModGlobals.OpenWorldState)
                return !Game1Extensions.IsObjectiveCompleted(Globaler.MainGameObjective.jungles_fishCrabsMorkla);
            else
                return Game1.Globals.MainQuestObjective == Globaler.MainGameObjective.jungles_fishCrabsMorkla;
        }

        private static bool Mod_ShouldPlayLowerMorklaDialog()
        {
            if(ModGlobals.OpenWorldState)
            {
                return Game1Extensions.IsObjectiveCompleted(Globaler.MainGameObjective.jungles_fishCrabsMorkla) && !Game1Extensions.IsObjectiveCompleted(Globaler.MainGameObjective.jungles_crabsCaughtMorkla);
            }
            else
            {
                return Game1.Globals.MainQuestObjective == Globaler.MainGameObjective.jungles_crabsCaughtMorkla;
            }
        }

        private static bool Mod_HasNotCompletedMorkla()
        {
            if (ModGlobals.OpenWorldState)
                return !Game1Extensions.IsObjectiveCompleted(Globaler.MainGameObjective.jungles_morklaComplete);
            else
                return Game1.Globals.MainQuestObjective < Globaler.MainGameObjective.jungles_morklaComplete;
        }

        private static bool Mod_HasReceivedBlacksmithItem()
        {
            return Game1Extensions.HasLevelPermaObject("npc21");
        }
    }
}
