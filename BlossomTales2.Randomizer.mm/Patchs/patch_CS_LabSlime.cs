// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using BlossomTales2.Extensions;
using BlossomTales2.Randomizer.mm;
using Microsoft.Xna.Framework;

namespace BlossomTales2
{
    public class patch_CS_LabSlime : CS_LabSlime
    {
        public void getHeart()
        {
            RandomizerSingleton.Instance.GiveItemAtLocation("labSlime", Vector3.Zero);
            Game1.Globals.labSlimeState = 2;
        }
    }
}
