using System.Collections.Generic;
using BlossomTales2.Randomizer.mm;
using Microsoft.Xna.Framework;

namespace BlossomTales2
{
    public class patch_ShopItem : ShopItem
    {
        private int itemPosition;

        public patch_ShopItem(Vector3 position) : base(position)
        {
        }

        public override void Init()
        {
            itemPosition = IDNumber % 3;
            if (IDNumber <= 0 || IDNumber >= 19)
                return;

            RandomizerSingleton singleton = RandomizerSingleton.Instance;

            Game1.Globals.ShopItems[0] = Game1.Globals.ShopItems[0] == 24 ? 24 : (int)singleton.GetItemAtLocation("blossom-shop.tmx", "left", Vector3.Zero).Item;
            Game1.Globals.ShopItems[1] = Game1.Globals.ShopItems[1] == 24 ? 24 : (int)singleton.GetItemAtLocation("blossom-shop.tmx", "center", Vector3.Zero).Item;
            Game1.Globals.ShopItems[2] = Game1.Globals.ShopItems[2] == 24 ? 24 : (int)singleton.GetItemAtLocation("blossom-shop.tmx", "right", Vector3.Zero).Item;

            Game1.Globals.ShopItems[3] = Game1.Globals.ShopItems[3] == 24 ? 24 : (int)singleton.GetItemAtLocation("anchor-shop.tmx", "left", Vector3.Zero).Item;
            Game1.Globals.ShopItems[4] = Game1.Globals.ShopItems[4] == 24 ? 24 : (int)singleton.GetItemAtLocation("anchor-shop.tmx", "center", Vector3.Zero).Item;
            Game1.Globals.ShopItems[5] = Game1.Globals.ShopItems[5] == 24 ? 24 : (int)singleton.GetItemAtLocation("anchor-shop.tmx", "right", Vector3.Zero).Item;

            Game1.Globals.ShopItems[6] = Game1.Globals.ShopItems[6] == 24 ? 24 : (int)singleton.GetItemAtLocation("canyon-shop.tmx", "left", Vector3.Zero).Item;
            Game1.Globals.ShopItems[7] = Game1.Globals.ShopItems[7] == 24 ? 24 : (int)singleton.GetItemAtLocation("canyon-shop.tmx", "center", Vector3.Zero).Item;
            Game1.Globals.ShopItems[8] = Game1.Globals.ShopItems[8] == 24 ? 24 : (int)singleton.GetItemAtLocation("canyon-shop.tmx", "right", Vector3.Zero).Item;

            Game1.Globals.ShopItems[9] = Game1.Globals.ShopItems[9] == 24 ? 24 : (int)singleton.GetItemAtLocation("darklands-house2-shop.tmx", "left", Vector3.Zero).Item;
            Game1.Globals.ShopItems[10] = Game1.Globals.ShopItems[10] == 24 ? 24 : (int)singleton.GetItemAtLocation("darklands-house2-shop.tmx", "center", Vector3.Zero).Item;
            Game1.Globals.ShopItems[11] = Game1.Globals.ShopItems[11] == 24 ? 24 : (int)singleton.GetItemAtLocation("darklands-house2-shop.tmx", "right", Vector3.Zero).Item;

            Game1.Globals.ShopItems[12] = Game1.Globals.ShopItems[12] == 24 ? 24 : (int)singleton.GetItemAtLocation("pirateShip-shop.tmx", "left", Vector3.Zero).Item;
            Game1.Globals.ShopItems[13] = Game1.Globals.ShopItems[13] == 24 ? 24 : (int)singleton.GetItemAtLocation("pirateShip-shop.tmx", "center", Vector3.Zero).Item;
            Game1.Globals.ShopItems[14] = Game1.Globals.ShopItems[14] == 24 ? 24 : (int)singleton.GetItemAtLocation("pirateShip-shop.tmx", "right", Vector3.Zero).Item;

            Game1.Globals.ShopItems[15] = Game1.Globals.ShopItems[15] == 24 ? 24 : (int)singleton.GetItemAtLocation("labHouse-shop.tmx", "left", Vector3.Zero).Item;
            Game1.Globals.ShopItems[16] = Game1.Globals.ShopItems[16] == 24 ? 24 : (int)singleton.GetItemAtLocation("labHouse-shop.tmx", "center", Vector3.Zero).Item;
            Game1.Globals.ShopItems[17] = Game1.Globals.ShopItems[17] == 24 ? 24 : (int)singleton.GetItemAtLocation("labHouse-shop.tmx", "right", Vector3.Zero).Item;

            Row = Game1.Globals.ShopItems[IDNumber - 1];
        }

        public override void onCollision(string xz, Player entity)
        {
            if (!(xz == "z") || Row == 0 || Row == 24 || Game1.player.Direction != 1 ||
                !(Game1.player.Position.X + 4f > Position.X) ||
                !(Game1.player.Position.X - 4f < Position.X + Size.Z * 4f))
                return;

            Game1.player.ShowDialogButton = true;
            if (!Input.A_Button_Pressed() || Game1.player.killInteractButton)
                return;

            Velocity.X = 1f;
            Mod_AskPurchaseItem();
        }

        private void Mod_AskPurchaseItem()
        {
            if (Game1.LevelName == "darklands-house2-shop.tmx")
                ProcessGhostStore();
            else if (Game1.LevelName == "pirateShip-shop.tmx")
                ProcessPirateStore();
            else
                ProcessRegularStore();
        }

        private void ProcessGhostStore()
        {
            if (Game1.player.ghostTimer > 0)
            {
                switch (itemPosition)
                {
                    case 1: //left
                        Game1.Dialoger.AddLine(
                            "Shop Owner: Would you like to buy that <B>Item for the low price of <Y>100 <Y>gold?",
                            "buy_100_left", new[] { "Yes", "No" });
                        break;
                    case 2: //center
                        Game1.Dialoger.AddLine("Shop Owner: That's a very popular item, an <G>Item. Very expensive.");
                        Game1.Dialoger.AddLine(
                            "Shop Owner: But I like you! I'll give you a friend discount. How about <Y>150 <Y>gold?",
                            "buy_150_center", new[] { "Yes", "No" });
                        break;
                    case 0: //right
                        Game1.Dialoger.AddLine(
                            "Shop Owner: I don't know if that <R>Item will do you any good... in your condition.");
                        Game1.Dialoger.AddLine(
                            "Shop Owner: Not many around here need it though, I'll let it go for <Y>150 <Y>gold?",
                            "buy_150_right", new[] { "Yes", "No" });
                        break;
                }
            }
            else
            {
                switch (itemPosition)
                {
                    case 1: //left
                        Game1.Dialoger.AddLine("Shop Owner: Would you like to buy that <B>Item for <Y>200 <Y>gold?",
                            "buy_200_left", new[] { "Yes", "No" });
                        break;
                    case 2: //center
                        Game1.Dialoger.AddLine(
                            "Shop Owner: That <G>Item is very rare. It will cost you <Y>350 <Y>gold?", "buy_350_center",
                            new[] { "Yes", "No" });
                        break;
                    case 0: //right
                        Game1.Dialoger.AddLine(
                            "Shop Owner: That's a very special item. A small <R>Item. Would you like to buy it for <Y>350 <Y>gold?",
                            "buy_350_right", new[] { "Yes", "No" });
                        break;
                }
            }
        }

        private void ProcessPirateStore()
        {
            switch (itemPosition)
            {
                case 1: //left
                    Game1.Dialoger.AddLine(
                        "Pirate Jimmy: Would ya like to buy that there <Y>Item that I found for <Y>200 <Y>gold?",
                        "buy_200_left", new[] { "Yes", "No" });
                    break;
                case 2: //center
                    Game1.Dialoger.AddLine(
                        "Pirate Jimmy: That's a very special item. A small <G>Item. Would ye like to buy it for <Y>250 <Y>gold?",
                        "buy_250_center", new[] { "Yes", "No" });
                    break;
                case 0: //right
                    Game1.Dialoger.AddLine(
                        "Pirate Jimmy: That <R>Item for <Y>250 <Y>gold be a good deal I tell ya. Savvy?",
                        "buy_250_right", new[] { "Yes", "No" });
                    break;
            }
        }

        private void ProcessRegularStore()
        {
            switch (itemPosition)
            {
                case 1: //left
                    Game1.Dialoger.AddLine("Shop Owner: Would you like to buy that <B>Item for <Y>150 <Y>gold?",
                        "buy_150_left", new[] { "Yes", "No" });
                    break;
                case 2: //center
                    Game1.Dialoger.AddLine("Shop Owner: Would you like to buy that <G>Item for <Y>250 <Y>gold?",
                        "buy_250_center", new[] { "Yes", "No" });
                    break;
                case 0: //right
                    Game1.Dialoger.AddLine("Shop Owner: Would you like to buy that <R>Item for <Y>250 <Y>gold?",
                        "buy_250_right", new[] { "Yes", "No" });
                    break;
            }
        }
    }
}
