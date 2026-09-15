// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.Collections.Generic;

namespace BlossomTales2.Randomizer.mm
{
    public class Validator
    {
        public static bool ValidateSeed(List<ItemData> itemPool)
        {
            World world = WorldFactory.Create();
            Inventory inventory = new Inventory();

            bool hasCollectedItem = false;
            do
            {
                hasCollectedItem = false;
                foreach (Region region in world.Regions)
                {
                    bool hasItem = region.TryCollectItems(inventory);
                    if (!hasCollectedItem)
                        hasCollectedItem = hasItem;
                }

                if (inventory.HasBeatenMinotaurKing)
                    return true;

            } while (hasCollectedItem);

            return false;
        }
    }
}
