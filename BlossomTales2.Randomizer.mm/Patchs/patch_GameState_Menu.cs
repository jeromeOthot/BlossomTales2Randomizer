// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using BlossomTales2.Randomizer.mm;
using Microsoft.Xna.Framework;

namespace BlossomTales2
{
    public class patch_GameState_Menu
    {
        private int menuState = -1;

        private extern void orig_NewGameReset();
        public extern void orig_Update(GameTime gameTime);

        public void Update(GameTime gameTime)
        {
            orig_Update(gameTime);
        }

        private void NewGameReset()
        {
            orig_NewGameReset();
            GameLogger.LogInfo("NewGameReset was called! Didn't complete Load == " + Game1.DidntCompleteLoad);
            if (Game1.DidntCompleteLoad)
            {
                //TODO: Générer la seed et sauvegarder les locations dans la save.
                RandomizerSingleton.Initialize();
            }
        }
    }
}
