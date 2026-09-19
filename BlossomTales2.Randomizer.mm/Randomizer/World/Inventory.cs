using System.Collections.Generic;

namespace BlossomTales2.Randomizer.mm
{
    public class Inventory
    {
        public Dictionary<ItemType, int> Items { get; set; } =  new Dictionary<ItemType, int>();
        public bool HasBeatenMinotaurKing => Items.ContainsKey(ItemType.HeartQ_1);

        public bool HasBombs => Items.ContainsKey(ItemType.Bombs);
        public bool HasFlipper => Items.ContainsKey(ItemType.Flippers);
        public bool  HasGhostPotion() => Items.ContainsKey(ItemType.Jar_Ghost);
        //todo
        public int  NbBlueGem() => 0;

        public bool CanAccesCanyon()
        {
            //TODO
            return true;
        }

        public bool CanAccesCanyonSteppe()
        {
            //TODO
            return true;
        }

        public bool CanAccesDarkForest()
        {
            //TODO
            return true;
        }

        public bool CanDoDommage()
        {
            //TODO
            return true;
        }


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
