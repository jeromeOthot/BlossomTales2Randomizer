// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System;

namespace BlossomTales2
{
    public class patch_CS_LoadCutscene : CS_LoadCutscene
    {
        private extern void orig_startDialog();
        private void startDialog()
        {
            this.endCutScene();
        }
    }
}
