using BlossomTales2.Extensions;
using BlossomTales2.Randomizer.mm;

namespace BlossomTales2
{
    public class patch_CS_SickZombie : CS_SickZombie
    {
        private Puppet zombie;

        public void giveHeart()
        {
            Game1.player.RemovePlayerControls = false;
            Mod_GiveItem();
            Game1.Globals.sickZombieState = 2;
        }

        private void Mod_GiveItem()
        {
            GameLogger.LogInfo(zombie.name + " " + zombie.getPosition());
            RandomizerSingleton.Instance.GiveItemAtLocation(zombie.name, zombie.getPosition());
        }
    }
}
