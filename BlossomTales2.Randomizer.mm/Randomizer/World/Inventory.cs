using System.Collections.Generic;

namespace BlossomTales2.Randomizer.mm
{
    public class Inventory
    {
        public Dictionary<ItemType, int> Items { get; set; } =  new Dictionary<ItemType, int>();

        //TODO: Real endgame flag.
        public bool HasBeatenMinotaurKing => HasBombs && HasSword && HasFlipper && HasTorch && HasGrappleHook &&
                                             HasBoomerang && HasBow && CanWakeUpPeople;

        public bool HasSword => Items.ContainsKey(ItemType.Sword);
        public bool HasTorch => Items.ContainsKey(ItemType.Torch);
        public bool HasBombs => Items.ContainsKey(ItemType.Bombs);
        public bool HasFlipper => Items.ContainsKey(ItemType.Flippers);
        public bool HasInstrument => Items.ContainsKey(ItemType.Accordian) || Items.ContainsKey(ItemType.Guitar);
        public bool HasBow => Items.ContainsKey(ItemType.Bow);
        public bool HasTriBow => Items.TryGetValue(ItemType.Bow, out int bowLevel)  && bowLevel >= 2;
        public bool HasGrappleHook  => Items.ContainsKey(ItemType.GrappleHook);
        public bool HasBoomerang => Items.ContainsKey(ItemType.Boomerang);
        public bool HasShovel => Items.ContainsKey(ItemType.Shovel);
        public bool HasBeatenMorklaBoss => Items.ContainsKey(ItemType.MorklaBoss);
        public bool HasKeys => Items.ContainsKey(ItemType.Gold_Key);
        public bool HasTreeSeeds => Items.ContainsKey(ItemType.TreeSeed);
        public bool CanOpenNoteDoor => HasInstrument && Items.ContainsKey(ItemType.OpenSesame);
        public bool CanWakeUpPeople => HasInstrument && Items.ContainsKey(ItemType.WakeUp);
        public bool CanActivateBlueSwitch => true;
        public bool  HasGhostPotion => Items.ContainsKey(ItemType.Jar_Ghost);
        //todo
        public int  NbBlueGem => 0;

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

        public bool CanCraftPotion(ItemType potionType)
        {
            if (!Items.ContainsKey(ItemType.Jar_Empty))
                return false;

            switch(potionType)
            {
                case ItemType.Jar_Health: return true;
                case ItemType.Jar_ReduceCost: return true;
                case ItemType.Jar_DoubleDamage: return true;
                case ItemType.Jar_SlowTime: return true;
                case ItemType.Jar_BubbleShield: return true;
                case ItemType.Jar_ArmorOrbs: return true;
                case ItemType.Jar_Resurrection: return true;
                case ItemType.Jar_Ghost: return true;
                case ItemType.Jar_Fire: return true;
                case ItemType.Jar_Speedster: return true;
                default: return false;
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
