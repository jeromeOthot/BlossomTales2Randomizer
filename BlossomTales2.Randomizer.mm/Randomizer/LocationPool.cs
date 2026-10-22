// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System;

namespace BlossomTales2.Randomizer.mm
{
    [Flags]
    public enum LocationPool
    {
        None = 0,
        TreasureChests = 1 << 0,
        KeyPieces = 1 << 1,
        Traders = 1 << 2,
        Shops = 1 << 3,
        FreeGifts = 1 << 4,
        Sidequests = 1 << 5,
        LongFetchQuests = 1 << 6,
        AnimalStatues = 1 << 7,
        MailDelivery = 1 << 8,
        AllExceptKeyPieces = ~KeyPieces,
    }
}
