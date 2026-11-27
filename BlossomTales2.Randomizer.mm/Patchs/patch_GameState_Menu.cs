using System;
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
            if (Game1.DidntCompleteLoad)
            {
                if (RandomizerSettings.GamePlay.Mount.HasValue == false)
                {
                    bool randomMount = (new Random().Next(2)) == 0;
                    Game1.player.ChosePig = randomMount;
                }
                else
                {
                    Game1.player.ChosePig = RandomizerSettings.GamePlay.Mount.Value;
                }

                RandomizerSingleton.Instance.RandomizeLocations();
                string json = RandomizerSingleton.Instance.SaveRandomizedLocations();
                Game1.Perma_Objects.Add(new PermaListItem(json, "RandomizedLocations", Vector3.Zero));
#if DEBUG
                if (RandomizerSettings.UnlockAll.HasValue && RandomizerSettings.UnlockAll.Value)
                {
                    patch_Game1.UnlockAllForDebug();
                }
#endif
            }
        }
    }
}
