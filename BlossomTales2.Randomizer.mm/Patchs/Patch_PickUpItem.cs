using BlossomTales2.Randomizer.mm;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoMod;

namespace BlossomTales2
{
    public class patch_PickUpItem : PickUpItem
    {
        private const int CanyonBoneID = 37;

        private Vector3 startPosition;
        private bool underwater;

        [MonoModAdded]
        private bool hasGivenItem;

        public extern void orig_Draw(SpriteBatch spriteBatch);
        public extern void orig_onCollision(string xz, Player entity);

        public patch_PickUpItem(Vector3 position) : base(position) { }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (!Alive)
                return;

            if(IDNumber == CanyonBoneID)
                Mod_DrawCanyonBoneItem(spriteBatch);
            else
                orig_Draw(spriteBatch);
        }

        public override void onCollision(string xz, Player entity)
        {
            if (!Alive || Position.Y >= 4.0 && IDNumber != 0 || Game1.player.InWater)
                return;

            if (IDNumber == CanyonBoneID)
                Mod_OnCollisionCanyonBoneItem();
            else
                orig_onCollision(xz, entity);
        }

        private void Mod_DrawCanyonBoneItem(SpriteBatch spriteBatch)
        {
            ItemData itemData = RandomizerSingleton.Instance.GetItemByNameAndLocation(Name, startPosition);
            if (itemData == null)
                return;

            spriteBatch.Draw(Game1.masterTileset,new Vector2(Position.X -48, Position.Z - Position.Y-48), new Rectangle(itemData.SpriteIndex * 32, 592, 32, 32), Color.White, 0.0f, Center, 4f, SpriteEffects.None, Position.Z + 48f + Zdepth);
        }

        private void Mod_OnCollisionCanyonBoneItem()
        {
            if (hasGivenItem)
                return;
            
            hasGivenItem = true;
            tweener.Timer(0.01f).OnComplete(delegate
            {
                Alive = false;
                if (SaveToMap)
                    Game1.Perma_Objects.Add(new PermaListItem(Game1.CurrentLevel.Name, Name, startPosition));

                Game1.playSoundCue("pickUp");

                ItemData itemData = RandomizerSingleton.Instance.GetItemByNameAndLocation(Name, startPosition);
                if (itemData != null)
                {
                    GameLogger.LogInfo("PickupItem: " + new LocationId(Game1.CurrentLevel.Name, Name, startPosition));
                    RandomizerSingleton.Instance.GiveItem(itemData);
                }
                else
                {
                    GameLogger.LogInfo("Localisation pickItem NOT FOUND: " + new LocationId(Game1.CurrentLevel.Name, Name, startPosition));
                    RandomizerSingleton.Instance.GiveItem(new ItemData(ItemType.Tomahawk));
                }
            });
        }
    }
}
