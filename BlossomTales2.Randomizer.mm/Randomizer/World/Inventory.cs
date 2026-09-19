using System.Collections.Generic;

namespace BlossomTales2.Randomizer.mm
{
    public class Inventory
    {
        public Dictionary<ItemType, int> Items { get; set; } =  new Dictionary<ItemType, int>();

        //TODO: Real endgame flag.
        public bool HasBeatenMinotaurKing => Items.ContainsKey(ItemType.BeeMedallion);

        public bool HasTorch => Items.ContainsKey(ItemType.Torch);
        public bool HasBombs => Items.ContainsKey(ItemType.Bombs);
        public bool HasFlipper => Items.ContainsKey(ItemType.Flippers);
        public bool HasInstrument => Items.ContainsKey(ItemType.Accordian) || Items.ContainsKey(ItemType.Guitar);
        public bool HasBow => Items.ContainsKey(ItemType.Bow);
        public bool HasBeatenMorklaBoss => Items.ContainsKey(ItemType.MorklaBoss);

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

        public bool CanDoDamage()
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
