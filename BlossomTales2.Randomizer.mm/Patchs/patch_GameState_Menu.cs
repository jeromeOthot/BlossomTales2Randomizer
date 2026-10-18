using BlossomTales2.Randomizer.mm;
using Microsoft.Xna.Framework;

namespace BlossomTales2
{
    public class patch_GameState_Menu
    {
        private extern void orig_NewGameReset();

        private void NewGameReset()
        {
            orig_NewGameReset();
            GameLogger.LogInfo("NewGameReset was called! Didn't complete Load == " + Game1.DidntCompleteLoad);
            if (Game1.DidntCompleteLoad)
            {
                RandomizerSingleton.Instance.RandomizeLocations();
                string json = RandomizerSingleton.Instance.SaveRandomizedLocations();
                Game1.Perma_Objects.Add(new PermaListItem(json, "RandomizedLocations", Vector3.Zero));
            }
        }
    }
}
