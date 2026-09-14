// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System;
using System.Collections.Generic;

namespace BlossomTales2.Randomizer.mm
{
    public class Region
    {
        public string Name { get; set; }
        public List<Location> Locations { get; set; }
        public Predicate<Inventory> CanAccess { get; set; }
    }
}
