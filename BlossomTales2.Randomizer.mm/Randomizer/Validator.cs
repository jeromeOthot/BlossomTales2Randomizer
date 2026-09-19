using System.Collections.Generic;

namespace BlossomTales2.Randomizer.mm
{
    public class Validator
    {
        public static bool ValidateSeed(List<ItemData> itemPool)
        {
            World world = new World();//WorldFactory.Create();
            Inventory inventory = new Inventory();

            bool hasCollectedItem;
            do
            {
                hasCollectedItem = world.TryCollectItems(inventory);
                if (inventory.HasBeatenMinotaurKing)
                    return true;

            } while (hasCollectedItem);

            return false;
        }
    }
}
