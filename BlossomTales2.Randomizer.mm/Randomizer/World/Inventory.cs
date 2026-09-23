using System.Collections.Generic;

namespace BlossomTales2.Randomizer.mm
{
    public class Inventory
    {
        public Dictionary<ItemType, int> Items { get; set; } =  new Dictionary<ItemType, int>();

        //TODO: Real endgame flag.
        public bool HasBeatenMinotaurKing => Items.ContainsKey(ItemType.BeeMedallion);

        public bool HasSword => Items.ContainsKey(ItemType.Sword);
        public bool HasTorch => Items.ContainsKey(ItemType.Torch);
        public bool HasBombs => Items.ContainsKey(ItemType.Bombs);
        public bool HasFlipper => Items.ContainsKey(ItemType.Flippers);
        public bool HasInstrument => Items.ContainsKey(ItemType.Accordian) || Items.ContainsKey(ItemType.Guitar);
        public bool HasBow => Items.ContainsKey(ItemType.Bow);
        public bool HasGrappleHook  => Items.ContainsKey(ItemType.GrappleHook);
        public bool HasBoomerang => Items.ContainsKey(ItemType.Boomerang);
        public bool HasBeatenMorklaBoss => Items.ContainsKey(ItemType.MorklaBoss);
        public bool HasKeys => Items.ContainsKey(ItemType.Gold_Key);
        public bool HasTreeSeeds => Items.ContainsKey(ItemType.TreeSeed);
        public bool CanOpenNoteDoor => HasInstrument && Items.ContainsKey(ItemType.OpenSesame);
        public bool CanActivateBlueSwitch => true;
        public bool  HasGhostPotion => Items.ContainsKey(ItemType.Jar_Ghost);
        //todo
        public int  NbBlueGem => 0;

        //TODO
        public bool CanCraftGhostPotion => Items.ContainsKey(ItemType.Jar_Empty);
        //TODO
        public bool CanCraftResurrectionPotion => Items.ContainsKey(ItemType.Jar_Empty);

        //TODO
        public bool CanAccessCanyon => true;
        //TODO
        public bool CanAccessCanyonSteppe => true;

        //TODO
        public bool CanAccessDarkForest => true;

        //TODO
        public bool CanDoDamage => HasSword || HasBombs || HasBow;

        public bool CanCutPegs => Items.TryGetValue(ItemType.Sword, out int swordLevel) && swordLevel >= 2;

        public bool CanSwitchLevers => HasSword || HasGrappleHook || HasBoomerang || HasBow;

        public bool CanCollectIngredient(EquipableItem.IngredientList ingredient)
        {
            switch (ingredient)
            {
                case EquipableItem.IngredientList.Apple: return true;
                default: return true;
            }
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
