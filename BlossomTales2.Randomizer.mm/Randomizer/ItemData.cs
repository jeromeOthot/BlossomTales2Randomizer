using System;

namespace BlossomTales2.Randomizer.mm
{
    public class ItemData
    {
        public ItemType Item { get; private set; }
        public int SpriteIndex => GetSpriteIndex();

        public ItemData(ItemType item)
        {
            Item = item;
        }

        public bool TryConvertToEquipableItem(out EquipableItem.ItemList item)
        {
            item = default;
            return Enum.TryParse(Item.ToString(), out item);
        }

        public bool TryConvertToIngredientItem(out EquipableItem.IngredientList ingredient)
        {
            ingredient = default;
            return Enum.TryParse(Item.ToString(), out ingredient);
        }

        public bool TryConvertToSongItem(out Globaler.Songs song)
        {
            song = default;
            return Enum.TryParse(Item.ToString(), out song);
        }

        public override string ToString()
        {
            return Item.ToString();
        }

        private int GetSpriteIndex()
        {
            switch (Item)
            {
                case ItemType.Sword:
                {
                    switch (Game1.player.SwordLevel)
                    {
                        case 0:
                            return (int)EquipableItem.ItemList.WoodSword;
                        case 1:
                            return (int)EquipableItem.ItemList.Sword;
                        default:
                            return (int)EquipableItem.ItemList.KingSword;
                    }
                }
                case ItemType.Shield:
                {
                    switch (Game1.player.ShieldLevel)
                    {
                        case 0:
                            return (int)EquipableItem.ItemList.WoodShield;
                        case 1:
                            return (int)EquipableItem.ItemList.Shield;
                        default:
                            return (int)EquipableItem.ItemList.MirrorShield;
                    }
                }
                case ItemType.Bow:
                {
                    return (int)(Game1.player.Inventory.Contains(EquipableItem.ItemList.Bow) ? EquipableItem.ItemList.TriBow : EquipableItem.ItemList.Bow);
                }
                default:
                {
                    return (int)Item;
                }
            }
        }
    }
}
