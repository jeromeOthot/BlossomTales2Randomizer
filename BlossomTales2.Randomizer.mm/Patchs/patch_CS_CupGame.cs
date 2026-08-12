using BlossomTales2.Randomizer.mm;
using Microsoft.Xna.Framework;

namespace BlossomTales2
{
    public class patch_CS_CupGame : CS_CupGame
    {
        private bool showGold;

        private extern void orig_GiveGold();

        internal void GiveGold()
        {
            Game1.Globals.CampCups_State = 10;
            showGold = false;
            Game1.player.PickedUpObject = false;
            if (Game1.player.PickedObject != null)
            {
                Game1.player.PickedObject.Alive = false;
            }
            Game1.player.PickedObject = null;
            Game1.player.ClearPlayer();
            Mod_GiveItem();
        }

        private void Mod_GiveItem()
        {
            RandomizerSingleton.Instance.GiveItemAtLocation("campCups", Vector3.Zero);
        }
    }
}
