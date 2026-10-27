// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System;
using System.Collections.Generic;
using BlossomTales2.Randomizer.mm;
using Microsoft.Xna.Framework;

namespace BlossomTales2
{
    internal class patch_CS_Intro : CS_Intro
    {
        public patch_CS_Intro() => Init();

        public extern void orig_Init();

        public override void Init()
        {
            if(RandomizerSettings.GamePlay.SkipCutscenes)
                endCutScene();
            else
                orig_Init();
        }
    }
}
