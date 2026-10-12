// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using BlossomTales2.Randomizer.mm;

namespace BlossomTales2
{
    public class patch_CS_LilysHouseWakeUp : CS_LilysHouseWakeUp
    {
        public patch_CS_LilysHouseWakeUp() => this.Init();

        public extern void orig_Init();

        public  void Init()
        {
            GameLogger.LogInfo("patch_CS_LilysHouseWakeUp Init " + ModGlobals.SkipCutscenes);

            if(ModGlobals.SkipCutscenes)
                this.endCutScene();
            else
                orig_Init();
        }
    }
}
