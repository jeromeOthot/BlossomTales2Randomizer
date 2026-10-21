// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System;
using BlossomTales2.Randomizer.mm;

namespace BlossomTales2
{
    internal class patch_CS_LoadCutscene : CS_LoadCutscene
    {
        public extern void orig_startDialog();

        private void  startDialog()
        {
            GameLogger.LogInfo("Starting load cutscene... " + RandomizerSettings.GamePlay.SkipCutscenes);
            if(RandomizerSettings.GamePlay.SkipCutscenes)
                this.endCutScene();
            else
                orig_startDialog();
        }
    }
}
