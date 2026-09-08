using BlossomTales2.Randomizer.mm;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BlossomTales2
{
    public class patch_PickUpItem : PickUpItem
    {
        private const int CanyonBoneID = 37;

        private bool underwater;

        public extern void orig_Init();

        public patch_PickUpItem(Vector3 position) : base(position)
        {
        }

        public void Init()
        {
            orig_Init();
            //ItemData itemData = RandomizerSingleton.Instance.GetItemByNameAndLocation(Name, Position);
        }

        public extern void orig_Draw(SpriteBatch spriteBatch);
        public override void Draw(SpriteBatch spriteBatch)
        {
            if (!Alive)
                return;

            if(IDNumber == CanyonBoneID)
            {
                ItemData itemData = RandomizerSingleton.Instance.GetItemByNameAndLocation(Name, Position);
                if (itemData == null)
                    return;

                int itemIndex = (int)itemData.Item;
                spriteBatch.Draw(Game1.masterTileset,new Vector2(Position.X -48, Position.Z - Position.Y-48), new Rectangle(itemIndex * 32, 592, 32, 32), Color.White, 0.0f, Center, 4f, SpriteEffects.None, Position.Z + 48f + Zdepth);
            }
            else
            {
                orig_Draw(spriteBatch);
            }
        }

        public extern void orig_onCollision(string xz, Player entity);
        public override void onCollision(string xz, Player entity)
        {
            if (!Alive || Position.Y >= 4.0 && IDNumber != 0 || Game1.player.InWater)
                return;

            if (IDNumber == CanyonBoneID)
            {
                Alive = false;
                if (SaveToMap)
                    Game1.Perma_Objects.Add(new PermaListItem(Game1.CurrentLevel.Name, Name, Position));

                Game1.playSoundCue("pickUp");

                ItemData itemData = RandomizerSingleton.Instance.GetItemByNameAndLocation(Name, Position);
                if (itemData != null)
                {
                    RandomizerSingleton.Instance.GiveItem(itemData);
                }
                else
                {
                    GameLogger.LogInfo("Localisation pickItem NOT FOUND: " + new LocationId(Game1.CurrentLevel.Name, Name, Position));
                    RandomizerSingleton.Instance.GiveItem(new ItemData(ItemType.Tomahawk));
                }
            }
            else
            {
                orig_onCollision(xz, entity);
            }
        }
    }
}
