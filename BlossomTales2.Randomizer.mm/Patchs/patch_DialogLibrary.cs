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
                case 95:
                    BarbDialog();
                    break;
                case 102:
                    BlacksmithDialog();
                    break;
                case 109:
                    FarmerConnyDialog();
                    break;
                default:
                    orig_NPC_RunLine(number, dialogPointer, position);
                    break;
            }
        }

        public static void LineTrigger(string Event, int choice)
        {
            if (Event.Contains("buy_"))
            {
                BuyItemEvent(Event,  choice);
                return;
            }

            switch (Event)
            {
                case "giveBow":
                    GiveBowEvent();
                    break;
                case "archGiveCrystal":
                    ArchGiveCrystalEvent();
                    break;
                case "giveShovel":
                    GiveShovelEvent();
                    break;
                case "giveTribow":
                    GiveNpcItem("hunter");
                    break;
                case "giveFarmerItem":
                    GiveNpcItem("farmer");
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

        private static void BarbDialog()
        {
            if (Mod_IsSaveBettyNotCompleted())
            {
                Game1.Dialoger.AddLine("Barb: Oh, hey! Sorry, but the bridge is out!");
                Game1.Dialoger.AddLine("Barb: My co-worker Betty disappeared, and she had all the nails!");
                Game1.Dialoger.AddLine("Barb: Please, Miss Knight! Find Betty so we can finish the bridge!");
                if (Mod_IsHeadToConstructionNotCompleted())
                {
                    Game1.Dialoger.AddLine("Barb: I saw her last just <R>South of here poking around a cave! Please hurry!", "updateMap");
                    Mod_CompleteHeadToConstructionObjective();
                }
                else
                {
                    Game1.Dialoger.AddLine("Barb: I saw her last just <R>South of here poking around a cave! Please hurry!");
                }
            }
            else
            {
                randomLines = new string[4] { "Barb: Thank you again, Miss Knight! You truly are a hero!", "Barb: You're going to cross the bridge to the <P>Periwinkle <P>Woods? Be careful!", "Barb: I've heard an owl hoot on the other side of the river late at night!", "Barb: You saved Betty! We would never have finished the bridge without you!" };
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

        private static void FarmerConnyDialog()
        {
            string mod_giveFarmerItemFlag = "giveFarmerItem";

            if (Game1.Globals.SpiderFarmer_State < 2)
            {
                Game1.Dialoger.AddLine("Farmer Conny: Those dag nabbit spiders!");
                Game1.Dialoger.AddLine("Farmer Conny: I had to move all my chickens inside the mill or they'd be eaten!");
            }
            else if (Game1.Globals.SpiderFarmer_State == 2)
            {
                Game1.Globals.SpiderFarmer_State = 3;
                Game1.Dialoger.AddLine("Farmer Conny: You did it! Oh, I am so glad!");
                Game1.Dialoger.AddLine("Farmer Conny: Please, take this as a token of my appreciation.", mod_giveFarmerItemFlag);
            }
            else if (Game1.Globals.SpiderFarmer_State == 3)
            {
                Game1.Dialoger.AddLine("Farmer Conny: Now if I could only get these chickens outta here!");
            }
            else if (Game1.Globals.SpiderFarmer_State == 4)
            {
                Game1.Dialoger.AddLine("Farmer Conny: Now I can finally get back to work.");
            }
        }

        private static void GiveNpcItem(string npc)
        {
            RandomizerSingleton.Instance.GiveItemAtLocation(npc, Vector3.Zero);
            Game1Extensions.AddLevelPermaObject(npc, Vector3.Zero);
        }

        private static void GiveBowEvent()
        {
            Game1.Globals.Blacksmith_State = 4;
            Game1.player.Direction = 3;
            GiveNpcItem("npc21");
        }

        private static void ArchGiveCrystalEvent()
        {
            Game1.player.RemoveItem_NEReflection(EquipableItem.ItemList.CanyonBone, playAnimation: false, 20);
            GiveNpcItem("archCanyon");
        }

        private static void GiveShovelEvent()
        {
            Game1.Globals.ArchJungle_State = 3;
            GiveNpcItem("archJungle");
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

        private static bool Mod_IsSaveBettyNotCompleted()
        {
            if (ModGlobals.OpenWorldState)
                return !Game1Extensions.IsObjectiveCompleted(Globaler.MainGameObjective.dark_saveBetty);
            else
                return Game1.Globals.MainQuestObjective <= Globaler.MainGameObjective.dark_saveBetty;
        }

        private static bool Mod_IsHeadToConstructionNotCompleted()
        {
            if (ModGlobals.OpenWorldState)
                return !Game1Extensions.IsObjectiveCompleted(Globaler.MainGameObjective.dark_headToConstruction);
            else
                return Game1.Globals.MainQuestObjective < Globaler.MainGameObjective.dark_saveBetty;
        }

        private static void Mod_CompleteHeadToConstructionObjective()
        {
            if (ModGlobals.OpenWorldState)
                Game1Extensions.MarkObjectiveComplete(Globaler.MainGameObjective.dark_headToConstruction);
            else
                Game1.Globals.MainQuestObjective = Globaler.MainGameObjective.dark_saveBetty;
        }

        private static void BuyItemEvent(string Event, int choice)
        {

            switch (choice)
            {
            case 1:
                ItemData item;
              int num1 = int.Parse(Event.Substring(4, 3));
              string str = Event.Substring(8, Event.Length - 8);
                Vector3 distPlayerItem = new Vector3(301, 0, 48);
              Game1.Dialoger.AddLine($"Event: {Event} choice: {choice}, num1:{num1} , str {str}");
              // Game1.Dialoger.AddLine($"Map name: {Game1.CurrentLevel.Name} , num1:{Game1.player.Position + distPlayerItem} ");


              if (Game1.player.Gold >= num1)
              {
               // Game1.player.ChangeGoldAmount(num1 * -1);
                switch (str)
                {
                  case "emptyJar":
                      item = RandomizerSingleton.Instance.TryGetItemWithMapNameAndName(Game1.CurrentLevel.Name, "emptyJar");
                      RandomizerSingleton.Instance.GiveItem(item);
                    break;
                  case "doubleDamage":
                      item = RandomizerSingleton.Instance.TryGetItemWithMapNameAndName(Game1.CurrentLevel.Name, "doubleDamage");
                      RandomizerSingleton.Instance.GiveItem(item);
                    break;
                  case "heartPiece":
                      item = RandomizerSingleton.Instance.TryGetItemWithMapNameAndName(Game1.CurrentLevel.Name, "heart");
                      RandomizerSingleton.Instance.GiveItem(item);
                    break;
                  case "crystal":
                      item = RandomizerSingleton.Instance.TryGetItemWithMapNameAndName(Game1.CurrentLevel.Name, "crystal");
                      RandomizerSingleton.Instance.GiveItem(item);
                    break;
                }
                for (int index = 0; index < Game1.CurrentLevel.LevelObjects.Count; ++index)
                {
                  if (Game1.CurrentLevel.LevelObjects[index] is ShopItem && (double) Game1.CurrentLevel.LevelObjects[index].Velocity.X == 1.0)
                  {
                    Game1.CurrentLevel.LevelObjects[index].Velocity.X = 0.0f;
                    Game1.CurrentLevel.LevelObjects[index].Row = 0;
                    Game1.Globals.ShopItems[Game1.CurrentLevel.LevelObjects[index].IDNumber - 1] = 24;
                    break;
                  }
                }
                break;
              }
              Game1.Dialoger.AddLine("Lily: I don't have enough gold coins.");
              if (Game1.LevelName == "pirateShip-shop.tmx")
                Game1.Dialoger.AddLine("Pirate Jimmy: You can always come back when you do!");
              else
                Game1.Dialoger.AddLine("Shop Owner: You can always come back when you do!");
              for (int index = 0; index < Game1.CurrentLevel.LevelObjects.Count; ++index)
              {
                if (Game1.CurrentLevel.LevelObjects[index] is ShopItem)
                  Game1.CurrentLevel.LevelObjects[index].Velocity.X = 0.0f;
              }
              break;
            case 2:
              if (Game1.LevelName == "pirateShip-shop.tmx")
                Game1.Dialoger.AddLine("Pirate Jimmy: Please come again!");
              else
                Game1.Dialoger.AddLine("Shop Owner: Please come again!");
              for (int index = 0; index < Game1.CurrentLevel.LevelObjects.Count; ++index)
              {
                if (Game1.CurrentLevel.LevelObjects[index] is ShopItem)
                  Game1.CurrentLevel.LevelObjects[index].Velocity.X = 0.0f;
              }
              break;
            }
        }
    }
}
