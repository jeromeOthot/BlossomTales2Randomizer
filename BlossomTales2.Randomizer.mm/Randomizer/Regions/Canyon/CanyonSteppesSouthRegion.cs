// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System;

namespace BlossomTales2.Randomizer.mm.Canyon
{
    public class CanyonSteppesSouthRegion : Region
    {
        public override Predicate<Inventory> CanAccess => inventory => inventory.CanAccessCanyonSteppe;

        public CanyonSteppesSouthRegion(World world) : base("Steppes South Region", world)
        {
            //TODO:
        }




    }
}
