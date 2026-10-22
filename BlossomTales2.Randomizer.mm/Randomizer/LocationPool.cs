// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System;

namespace BlossomTales2.Randomizer.mm
{
    [Flags]
    public enum LocationPool
    {
        None = 0,
        Default = 1 << 0,
        KeyPieces = 1 << 1,
        Traders = 1 << 2,
        AllExceptKeyPieces = Default | Traders
    }
}
