using BlossomTales2.Extensions;
using Microsoft.Xna.Framework;

namespace BlossomTales2
{
    internal class patch_DialogLibrary : DialogLibrary
    {
        private static string[] randomLines;

        public static extern void orig_NPC_RunLine(int number, int dialogPointer, Vector2 position);

        public static void NPC_RunLine(int number, int dialogPointer, Vector2 position)
        {
            if(dialogPointer == 81)
            {
                MorklaDialog();
            }
            else
            {
                orig_NPC_RunLine(number, dialogPointer, position);
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
    }
}
