using BlossomTales2.Randomizer.mm;
using Microsoft.Xna.Framework;

namespace BlossomTales2
{
    public class patch_CS_UFO : CS_UFO
    {
        private extern void orig_getEnergy();

        public void getEnergy()
        {
            Game1.player.RemovePlayerControls = true;
            Game1.Globals.ufoState = 2;
            Mod_GiveItem();
            tweener.Timer(5f).OnComplete(delegate
            {
                returnToBlossom();
            });
        }

        private void Mod_GiveItem()
        {
            RandomizerSingleton.Instance.GiveItemAtLocation("aliens", Vector3.Zero);
        }
    }
}
