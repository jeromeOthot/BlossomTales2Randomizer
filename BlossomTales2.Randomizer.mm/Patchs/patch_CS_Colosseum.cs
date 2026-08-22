using BlossomTales2.Extensions;
using BlossomTales2.Randomizer.mm;
using Microsoft.Xna.Framework;

namespace BlossomTales2
{
    public class patch_CS_Colosseum : CS_Colosseum
    {
        public extern void orig_round2Talk2();
        public extern void orig_round3Talk2();
        public extern void orig_round4Talk2();
        public extern void orig_round5Talk2();
        public extern void orig_round6Talk2();
        public extern void orig_round7Talk2();
        public extern void orig_round8Talk2();
        public extern void orig_round9Talk2();
        public extern void orig_round10Talk2();

        public void round2Talk2()
        {
            if (ModGlobals.RandomizeColiseumCoins)
                Mod_NextRound("price1", "Announcer: But can she handle a swarm of wild mosquitos?!", "Announcer: Psst! Do you want to continue?");
            else
                orig_round2Talk2();
        }

        public void round3Talk2()
        {
            if (ModGlobals.RandomizeColiseumCoins)
                Mod_NextRound("price2", "Announcer: What's this? A pack of wild wolves entered the restaurant and placed their order... fresh human!", "Announcer: Hey! Are you ready for the next round?");
            else
                orig_round3Talk2();
        }

        public void round4Talk2()
        {
            if (ModGlobals.RandomizeColiseumCoins)
                Mod_NextRound("price3", "Announcer: Oh my! It's a gang of ruthless, toothless pirates ready to teach the landlubber a lesson!", "Announcer: Psst! Want to keep going?");
            else
                orig_round4Talk2();
        }

        public void round5Talk2()
        {
            if (ModGlobals.RandomizeColiseumCoins)
                Mod_NextRound("price4", "Announcer: Up next is a smack of dreaded electric jellyfish! Yes, smack is the correct term. Look it up!", "Announcer: Hey! Still got some fight in ya?");
            else
                orig_round5Talk2();
        }

        public void round6Talk2()
        {
            if (ModGlobals.RandomizeColiseumCoins)
                Mod_NextRound("price5", "Announcer: Folks, we've got undead skeletons in the house, and they've got a bone to pick with our fighter!", "Announcer: Psst! Do you want to keep going?");
            else
                orig_round6Talk2();
        }

        public void round7Talk2()
        {
            if (ModGlobals.RandomizeColiseumCoins)
                Mod_NextRound("price6", "Announcer: Up next; a cacti horde! Will they be on point fighting on their home turf?", "Announcer: Hey! Do you want to continue?");
            else
                orig_round7Talk2();
        }

        public void round8Talk2()
        {
            if (ModGlobals.RandomizeColiseumCoins)
                Mod_NextRound("price7", "Announcer: Will our fighter finally croak to giant frogs with some serious indigestion?!", "Announcer: Psst! Ready to keep going?");
            else
                orig_round8Talk2();
        }

        public void round9Talk2()
        {
            if (ModGlobals.RandomizeColiseumCoins)
                Mod_NextRound("price8", "Announcer: Wow! We've got scythe-wielding specters looking to do what they do best; harvest souls!", "Announcer: Hey! Do you want to continue?");
            else
                orig_round9Talk2();
        }

        public void round10Talk2()
        {
            if (ModGlobals.RandomizeColiseumCoins)
                Mod_NextRound("price9", "Announcer: Final battle time! Can undead axe knights cut her winning streak short?", "Announcer: Psst! Are you sure you want to do this?");
            else
                orig_round10Talk2();
        }

        public void giveHeart()
        {
            RandomizerSingleton.Instance.GiveItemAtLocation("price_heart", Vector3.Zero);
            tweener.Timer(3f).OnComplete(preCrystal);
        }

        public void giveCrystal()
        {
            RandomizerSingleton.Instance.GiveItemAtLocation("price_crystal", Vector3.Zero);
            tweener.Timer(3f).OnComplete(keepTalking);
        }

        private void Mod_NextRound(string itemLocation, string line1, string line2)
        {
            if (!Game1Extensions.HasLevelPermaObject(itemLocation))
            {
                RandomizerSingleton.Instance.GiveItemAtLocation(itemLocation, Vector3.Zero);
                Game1Extensions.AddLevelPermaObject(itemLocation, Vector3.Zero);
            }

            Game1.Dialoger.AddLine(line1);
            Game1.Dialoger.AddLine(line2, "colosseumNextRound", new [] { "Yes", "No" });
        }
    }
}
