using BlossomTales2.Extensions;
using BlossomTales2.Randomizer.mm;
using Microsoft.Xna.Framework;

namespace BlossomTales2
{
    internal class patch_CS_JungleJigsaw : CS_JungleJigsaw
    {
        public extern void orig_giveShovel();

        public void giveShovel()
        {
            focusCam = false;
            Game1.player.CamOffset = Vector2.Zero;
            Game1.player.HasMoved = false;
            Game1.Globals.ArchJungle_State = 3;
            Mod_GiveItem();
        }

        private void Mod_GiveItem()
        {
            //Don't register position.
            EquipableItem.ItemList item = RandomizerSingleton.Instance.GetItemAtLocation("archJungle", Vector3.Zero);
            Game1.player.GiveItemReflection(item);
            Game1Extensions.AddLevelPermaObject("archJungle", Vector3.Zero);
        }
    }
}
