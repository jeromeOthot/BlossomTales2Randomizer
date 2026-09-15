using System.Collections.Generic;

namespace BlossomTales2.Randomizer.mm
{
    public class Inventory
    {
        public Dictionary<ItemType, int> Items { get; set; }
        public bool HasBeatenMinotaurKing { get; set; }

        public void AddItem(ItemType itemType, int amount)
        {
            if(Items.ContainsKey(itemType))
                Items[itemType] += amount;
            else
                Items.Add(itemType, amount);
        }

        public void RemoveItem(ItemType itemType, int amount)
        {
            if (Items.ContainsKey(itemType))
                Items[itemType] -= amount;
        }
    }
}
