using BlossomTales2.Randomizer.mm;
using Microsoft.Xna.Framework;
using MonoMod;

namespace BlossomTales2
{
    public class patch_CS_Falcon : CS_Falcon
    {
        private Puppet falcon;

        [MonoModPatch("<flyToLily>b__5_0")]
        public void FlyToLilyOnCompleteLambda()
        {
            falcon.play("hide");
            Mod_GiveItem();
            Game1.player.RemovePlayerControls = false;
        }

        private void Mod_GiveItem()
        {
            RandomizerSingleton.Instance.GiveItemAtLocation("postal_falcon", Vector3.Zero);
        }
    }
}
