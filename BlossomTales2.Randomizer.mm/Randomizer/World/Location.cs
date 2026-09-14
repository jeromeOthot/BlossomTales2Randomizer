// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System;

namespace BlossomTales2.Randomizer.mm
{
    public class Location
    {
        public LocationId Id { get; set; }
        public string Name { get; set; }
        public Predicate<Inventory> CanAccess { get; set; }
    }
}
