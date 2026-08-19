// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.Collections.Generic;
using BlossomTales2.Randomizer.mm;
using Microsoft.Xna.Framework;

namespace BlossomTales2
{
    public class patch_ShopItem : ShopItem
    {
        private int itemPosition = 0;
        public patch_ShopItem(Vector3 position) : base(position)
        {
        }

        public extern void orig_Init();
        public override void Init()
        {
            this.itemPosition = this.IDNumber % 3;
            if (this.IDNumber <= 0 || this.IDNumber >= 19)
                return;
            this.Row = Game1.Globals.ShopItems[this.IDNumber - 1];

            Game1.Globals.ShopItems = new List<int>((IEnumerable<int>) new int[18]
            {
                (int)RandomizerSingleton.Instance.TryGetItem("blossom-shop.tmx", "left", new Vector3(60f, 0f, 284f)).Item,
                (int)RandomizerSingleton.Instance.TryGetItem("blossom-shop.tmx", "center", new Vector3(52f, 0f, 284f)).Item,
                (int)RandomizerSingleton.Instance.TryGetItem("blossom-shop.tmx", "right", new Vector3(44f, 0f, 284f)).Item,
                (int)RandomizerSingleton.Instance.TryGetItem("anchor-shop.tmx", "left", new Vector3(336f, 0f, 324f)).Item,
                (int)RandomizerSingleton.Instance.TryGetItem("anchor-shop.tmx", "center", new Vector3(536f, 0f, 324f)).Item,
                (int)RandomizerSingleton.Instance.TryGetItem("anchor-shop.tmx", "right", new Vector3(636f, 0f, 324f)).Item,
                (int)RandomizerSingleton.Instance.TryGetItem("canyon-shop.tmx", "left", new Vector3(520f, 0f, 356f)).Item,
                (int)RandomizerSingleton.Instance.TryGetItem("canyon-shop.tmx", "center", new Vector3(620f, 0f, 356f)).Item,
                (int)RandomizerSingleton.Instance.TryGetItem("canyon-shop.tmx", "right", new Vector3(716f, 0f, 356f)).Item,
                (int)RandomizerSingleton.Instance.TryGetItem("darklands-house2-shop.tmx", "left", new Vector3(576f, 0f, 356f)).Item,
                (int)RandomizerSingleton.Instance.TryGetItem("darklands-house2-shop.tmx", "center", new Vector3(664f, 0f, 356f)).Item,
                (int)RandomizerSingleton.Instance.TryGetItem("darklands-house2-shop.tmx", "right", new Vector3(772f, 0f, 356f)).Item,
                (int)RandomizerSingleton.Instance.TryGetItem("pirateShip-shop.tmx", "left", new Vector3(572f, 0f, 136f)).Item,
                (int)RandomizerSingleton.Instance.TryGetItem("pirateShip-shop.tmx", "center", new Vector3(672f, 0f, 136f)).Item,
                (int)RandomizerSingleton.Instance.TryGetItem("pirateShip-shop.tmx", "right", new Vector3(772f, 0f, 136f)).Item,
                (int)RandomizerSingleton.Instance.TryGetItem("labHouse-shop.tmx", "left", new Vector3(360f, 0f, 348f)).Item,
                (int)RandomizerSingleton.Instance.TryGetItem("labHouse-shop.tmx", "center", new Vector3(452f, 0f, 348f)).Item,
                (int)RandomizerSingleton.Instance.TryGetItem("labHouse-shop.tmx", "right", new Vector3(544f, 0f, 348f)).Item,
            });
        }

        public extern void orig_onCollision();

        public override void onCollision(string xz, Player entity)
        {
            //Game1.Dialoger.AddLine($"row: {this.Row} IDNumber: {this.IDNumber} -- ROW: {this.Row}");
            if (!(xz == "z") || this.Row == 0 || this.Row == 24 || Game1.player.Direction != 1 ||
                (double)Game1.player.Position.X + 4.0 <= (double)this.Position.X ||
                (double)Game1.player.Position.X - 4.0 >= (double)this.Position.X + (double)this.Size.Z * 4.0)
                return;
            Game1.player.ShowDialogButton = true;
            if (!Input.A_Button_Pressed() || Game1.player.killInteractButton)
                return;
            this.Velocity.X = 1f;

            int CostItem = 10;

            //left
            if (this.itemPosition == 1)
            {
                Game1.Dialoger.AddLine($"Shop Owner: Would you like to buy that <B>Item <B> for <Y>150 <Y>gold?", $"buy_150_left", new string[2]
                {
                    "Yes",
                    "No"
                });
            }
            //center
            if (this.itemPosition == 2)
            {
                Game1.Dialoger.AddLine($"Shop Owner: Would you like to buy that <B>Item <B> for <Y>250 <Y>gold?", $"buy_250_center", new string[2]
                {
                    "Yes",
                    "No"
                });
            }
            //right
            if (this.itemPosition == 0)
            {
                Game1.Dialoger.AddLine($"Shop Owner: Would you like to buy that <B>Item <B> for <Y>250 <Y>gold?", $"buy_250_right", new string[2]
                {
                    "Yes",
                    "No"
                });
            }

        }
    }
}
