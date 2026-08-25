using System;
using BlossomTales2.Extensions;
using BlossomTales2.Randomizer.mm;
using Microsoft.Xna.Framework;

namespace BlossomTales2
{
    public class patch_CS_TreeLord : CS_TreeLord
    {
        public extern void orig_giveHeart();
        public void giveHeart()
        {
            Game1.Globals.TreeLord_State = 11;
            RandomizerSingleton.Instance.GiveItemAtLocation("treeLordReward", Vector3.Zero);
            this.tweener.Timer(3f).OnComplete(new Action(this.moreTalkingHeart));
        }
    }
}
