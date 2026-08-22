// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using BlossomTales2.Randomizer.mm;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BlossomTales2
{
    public class patch_PickUpItem : PickUpItem
    {
        private bool underwater;

        public patch_PickUpItem(Vector3 position) : base(position)
        {
            this.Name = nameof (PickUpItem);
            this.Position = position;
            this.Size = new Vector3(12f, 8f, 10f);
            this.Collidable = true;
            this.Breakable = true;
            if (Game1.USE_TAOS)
                this.TextureOffset = Game1.TextureOffsets[this.Name + ".png"];
            else
                this.Sprite = Game1.content.Load<Texture2D>("Sprites/LevelObjects/" + this.Name);
            int num1 = 3; // (int) this.Position.X / 64;
            int num2 = 3; //(int) this.Position.Z / 64;
        }

        public extern void orig_Draw(SpriteBatch spriteBatch);
        public override void Draw(SpriteBatch spriteBatch)
        {
            if (!this.Alive)
                return;

            //Si c'est un os
            if(this.IDNumber == 37)
            {
                GameLogger.LogInfo(new LocationId(Game1.CurrentLevel.Name, this.Name, this.Position).ToString());
                ItemData item = RandomizerSingleton.Instance.GetItemByNameAndLocation(this.Name, this.Position);
                if (item != null)
                {
                    this.Sprite = Game1.masterTileset; //Game1.content.Load<Texture2D>("Sprites/LevelObjects/_patchPickItem");
                    int itemIndex = (int)item.Item;
                    spriteBatch.Draw(Game1.masterTileset,        new Vector2(this.Position.X -48, this.Position.Z - this.Position.Y-48), new Rectangle?(new Rectangle(itemIndex * 32 /*0x20*/, 592, 32 /*0x20*/, 32 /*0x20*/)), Color.White, 0.0f, this.Center, 4f, SpriteEffects.None, this.Position.Z + 48f + this.Zdepth);
                }
            }
            else
            {
                if (this.grappleOffset != Vector2.Zero)
                {
                    spriteBatch.Draw(this.Sprite, new Vector2((float) (int) this.Position.X, (float) (int) this.Position.Z), new Rectangle?(new Rectangle(this.TextureOffset.X + this.Frame * 16 /*0x10*/, this.TextureOffset.Y + this.IDNumber * 16 /*0x10*/, 16 /*0x10*/, 16 /*0x10*/)), Color.White, 0.0f, new Vector2(8f, 12f), this.Scale, SpriteEffects.None, this.Position.Z + 32f + this.Zdepth);
                }
                else
                {
                    float layerDepth = this.Position.Z + this.Zdepth;
                    //TODO: A verifier c`est quoi les objet qui sont underwater
                    if (this.underwater)
                       layerDepth = (float) (3.0 + (double) this.Zdepth / 100.0);
                    spriteBatch.Draw(this.Sprite, new Vector2((float) (int) this.Position.X, (float) ((int) this.Position.Z - (int) this.Position.Y)), new Rectangle?(new Rectangle(this.TextureOffset.X + this.Frame * 16 /*0x10*/, this.TextureOffset.Y + this.IDNumber * 16 /*0x10*/, 16 /*0x10*/, 16 /*0x10*/)), Color.White, 0.0f, new Vector2(8f, 12f), this.Scale, SpriteEffects.None, layerDepth);
                }
                spriteBatch.Draw(this.Sprite, new Vector2((float) (int) this.Position.X, (float) (int) this.Position.Z), new Rectangle?(new Rectangle(this.TextureOffset.X + 32 /*0x20*/, this.TextureOffset.Y + this.IDNumber * 16 /*0x10*/, 16 /*0x10*/, 16 /*0x10*/)), Color.White * 0.3f, 0.0f, new Vector2(8f, 12f), this.Scale, SpriteEffects.None, 3.02f);

            }
        }

        public extern void orig_onCollision(string xz, Player entity);
        public override void onCollision(string xz, Player entity)
        {
            if (!this.Alive || (double) this.Position.Y >= 4.0 && this.IDNumber != 0 || Game1.player.InWater)
                return;
            this.Alive = false;
            if (this.IDNumber != 0 && this.SaveToMap)
                Game1.Perma_Objects.Add(new PermaListItem(Game1.CurrentLevel.Name, this.Name, this.Position));
            if (this.IDNumber != 0)
                Game1.playSoundCue("pickUp");
            if (this.IDNumber == 0)
            {
                Game1.playSoundCue("heart");
                Game1.player.Health += 2;
                if (Game1.player.Health <= Game1.player.MaxHealth)
                    return;
                Game1.player.Health = Game1.player.MaxHealth;
            }
            else if (this.IDNumber == 1)
                Game1.player.GiveIngredientReflection(EquipableItem.IngredientList.Mushroom);
            else if (this.IDNumber == 37)
            {
                ItemData item = RandomizerSingleton.Instance.GetItemByNameAndLocation(this.Name, this.Position);
                if (item != null)
                {
                    RandomizerSingleton.Instance.GiveItem(item);
                }
                else
                {
                    GameLogger.LogInfo("Localisation pickItem NOT FOUND: " + new LocationId(Game1.CurrentLevel.Name, this.Name, this.Position).ToString());
                    RandomizerSingleton.Instance.GiveItem(new ItemData(ItemType.Tomahawk));
                }
            }
            else if (this.IDNumber == 38)
            {
                Game1.player.GiveItemReflection(EquipableItem.ItemList.Ingred_Gem);
                Game1.Gui.AddGuiTicker(EquipableItem.IngredientList.Gem, Game1.player.Count_Gems);
            }
            else
                Game1.player.GiveIngredientReflection((EquipableItem.IngredientList) this.IDNumber);
        }
    }
}
