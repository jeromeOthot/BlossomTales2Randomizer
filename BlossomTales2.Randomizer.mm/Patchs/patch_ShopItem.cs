// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.Collections.Generic;
using BlossomTales2.Randomizer.mm;
using Microsoft.Xna.Framework;

namespace BlossomTales2
{
    public class patch_ShopItem : ShopItem
    {
        public patch_ShopItem(Vector3 position) : base(position)
        {
        }

        public extern void orig_Init();
        public override void Init()
        {
            this.Row = this.IDNumber;
            if (this.IDNumber <= 0 || this.IDNumber >= 19)
                return;
            this.Row = Game1.Globals.ShopItems[this.IDNumber - 1];
            //Game1.Dialoger.AddLine($"Id item: {id}");
            Game1.Globals.ShopItems = new List<int>((IEnumerable<int>) new int[18]
            {
                //{ new LocationId("blossom-shop.tmx", "emptyJar", new Vector3(60f, 0f, 284f)), new ItemData(ItemType.CanyonBone) },
                (int)RandomizerSingleton.Instance.TryGetItem("blossom-shop.tmx", "emptyJar", new Vector3(60f, 0f, 284f)).Item,
                (int)RandomizerSingleton.Instance.TryGetItem("blossom-shop.tmx", "heart", new Vector3(52f, 0f, 284f)).Item,
                (int)RandomizerSingleton.Instance.TryGetItem("blossom-shop.tmx", "crystal", new Vector3(44f, 0f, 284f)).Item,
                4,
                5,
                6,
                7,
                8,
                9,
                10,
                11,
                12,
                13,
                14,
                15,
                16,
                17,
                18
            });
        }

        public extern void orig_onCollision();
        public override void onCollision(string xz, Player entity)
        {
           // Game1.Dialoger.AddLine($"{this.Name}  {this.Position}" );
            if (!(xz == "z") || this.Row == 0 || this.Row == 24 || Game1.player.Direction != 1 || (double) Game1.player.Position.X + 4.0 <= (double) this.Position.X || (double) Game1.player.Position.X - 4.0 >= (double) this.Position.X + (double) this.Size.Z * 4.0)
                return;
            Game1.player.ShowDialogButton = true;
            if (!Input.A_Button_Pressed() || Game1.player.killInteractButton)
                return;
            this.Velocity.X = 1f;

            int CostItem = 10;
            Game1.Dialoger.AddLine($"Shop Owner: Would you like to buy that <B>Empty <B>Bottle for the low price of <Y>{ CostItem }<Y>gold?", "buy_100_emptyJar", new string[2]
            {
                "Yes",
                "No"
            });
        }
    }
}
