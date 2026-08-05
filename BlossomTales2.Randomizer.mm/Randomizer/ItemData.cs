using System;

namespace BlossomTales2.Randomizer.mm
{
    public class ItemData
    {
        public ItemType Item { get; private set; }

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
    }
}
