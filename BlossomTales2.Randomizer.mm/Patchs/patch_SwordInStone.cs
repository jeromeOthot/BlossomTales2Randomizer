// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using BlossomTales2.Extensions;
using BlossomTales2.Randomizer.mm;
using Microsoft.Xna.Framework;

namespace BlossomTales2
{
    public class patch_SwordInStone : SwordInStone
    {
        public extern void orig_Init();

        public patch_SwordInStone(Vector3 position) : base(position)
        {
        }

        public override void Init()
        {
            if (Game1Extensions.HasLevelPermaObject("SwordInStone", ignoreLevel: true))
            {
                Frame = 1;
            }
        }
    }
}
