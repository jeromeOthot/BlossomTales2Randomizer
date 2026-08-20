// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System;
using System.Collections.Generic;
using BlossomTales2.Randomizer.mm;
using Microsoft.Xna.Framework;

namespace BlossomTales2
{

    public class patch_CS_Colosseum : CS_Colosseum
    {
        public extern void orig_round2Talk2();
        public void round2Talk2()
        {
            ItemData price = RandomizerSingleton.Instance.TryGetItem("colosseum.tmx", "price1", new Vector3(0f, 0f, 0f));
            RandomizerSingleton.Instance.GiveItem(price);
            Game1.Dialoger.AddLine("Announcer: Psst! Do you want to continue?", "colosseumNextRound", new string[2]
            {
                "Yes",
                "No"
            });
        }

        public extern void orig_round3Talk2();
        public void round3Talk2()
        {
            ItemData price = RandomizerSingleton.Instance.TryGetItem("colosseum.tmx", "price2", new Vector3(0f, 0f, 0f));
            RandomizerSingleton.Instance.GiveItem(price);
            Game1.Dialoger.AddLine("Announcer: Hey! Are you ready for the next round?", "colosseumNextRound", new string[2]
            {
              "Yes",
              "No"
            });
        }

        public extern void orig_round4Talk2();
        public void round4Talk2()
        {
            ItemData price = RandomizerSingleton.Instance.TryGetItem("colosseum.tmx", "price3", new Vector3(0f, 0f, 0f));
            RandomizerSingleton.Instance.GiveItem(price);
            Game1.Dialoger.AddLine("Announcer: Hey! Are you ready for the next round?", "colosseumNextRound", new string[2]
            {
                "Yes",
                "No"
            });
        }

        public extern void orig_round5Talk2();
        public void round5Talk2()
        {
            ItemData price = RandomizerSingleton.Instance.TryGetItem("colosseum.tmx", "price4", new Vector3(0f, 0f, 0f));
            RandomizerSingleton.Instance.GiveItem(price);
            Game1.Dialoger.AddLine("Announcer: Hey! Are you ready for the next round?", "colosseumNextRound", new string[2]
            {
                "Yes",
                "No"
            });
        }

        public extern void orig_round6Talk2();
        public void round6Talk2()
        {
            ItemData price = RandomizerSingleton.Instance.TryGetItem("colosseum.tmx", "price5", new Vector3(0f, 0f, 0f));
            RandomizerSingleton.Instance.GiveItem(price);
            Game1.Dialoger.AddLine("Announcer: Hey! Are you ready for the next round?", "colosseumNextRound", new string[2]
            {
                "Yes",
                "No"
            });
        }

        public extern void orig_round7Talk2();
        public void round7Talk2()
        {
            ItemData price = RandomizerSingleton.Instance.TryGetItem("colosseum.tmx", "price6", new Vector3(0f, 0f, 0f));
            RandomizerSingleton.Instance.GiveItem(price);
            Game1.Dialoger.AddLine("Announcer: Hey! Are you ready for the next round?", "colosseumNextRound", new string[2]
            {
                "Yes",
                "No"
            });
        }

        public extern void orig_round8Talk2();
        public void round8Talk2()
        {
            ItemData price = RandomizerSingleton.Instance.TryGetItem("colosseum.tmx", "price7", new Vector3(0f, 0f, 0f));
            RandomizerSingleton.Instance.GiveItem(price);
            Game1.Dialoger.AddLine("Announcer: Hey! Are you ready for the next round?", "colosseumNextRound", new string[2]
            {
                "Yes",
                "No"
            });
        }

        public extern void orig_round9Talk2();
        public void round9Talk2()
        {
            ItemData price = RandomizerSingleton.Instance.TryGetItem("colosseum.tmx", "price8", new Vector3(0f, 0f, 0f));
            RandomizerSingleton.Instance.GiveItem(price);
            Game1.Dialoger.AddLine("Announcer: Hey! Are you ready for the next round?", "colosseumNextRound", new string[2]
            {
                "Yes",
                "No"
            });
        }

        public extern void orig_round10Talk2();
        public void round10Talk2()
        {
            ItemData price = RandomizerSingleton.Instance.TryGetItem("colosseum.tmx", "price9", new Vector3(0f, 0f, 0f));
            RandomizerSingleton.Instance.GiveItem(price);
            Game1.Dialoger.AddLine("Announcer: Hey! Are you ready for the next round?", "colosseumNextRound", new string[2]
            {
                "Yes",
                "No"
            });
        }

        public void giveHeart()
        {
            ItemData price = RandomizerSingleton.Instance.TryGetItem("colosseum.tmx", "price10", new Vector3(0f, 0f, 0f));
            RandomizerSingleton.Instance.GiveItem(price);
            this.tweener.Timer(3f).OnComplete(new Action(this.giveCrystal));
        }

        public void giveCrystal()
        {
            ItemData price = RandomizerSingleton.Instance.TryGetItem("colosseum.tmx", "price11", new Vector3(0f, 0f, 0f));
            RandomizerSingleton.Instance.GiveItem(price);
            this.tweener.Timer(3f).OnComplete(new Action(this.keepTalking));
        }
    }
}
