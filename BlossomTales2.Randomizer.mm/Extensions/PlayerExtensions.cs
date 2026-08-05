using System.Reflection;

namespace BlossomTales2.Randomizer.mm
{
    internal static class PlayerExtensions
    {
        public static void GiveItemReflection(this Player player, EquipableItem.ItemList item, bool playAnimation = true)
        {
            MethodInfo info = Game1.player.GetType().GetMethod("GiveItem", BindingFlags.NonPublic | BindingFlags.Instance);
            info.Invoke(Game1.player, new object[] { item, playAnimation });
        }

        public static void RemoveItem_NEReflection(this Player player, EquipableItem.ItemList item, bool playAnimation = false, int amount = 1)
        {
            MethodInfo info = Game1.player.GetType().GetMethod("RemoveItem_NE", BindingFlags.NonPublic | BindingFlags.Instance);
            info.Invoke(Game1.player, new object[] { item, playAnimation, amount });
        }

        public static void LearnSong(this Player player, Globaler.Songs newSong)
        {
            if (!Game1.Globals.Learned_Songs.Contains(newSong))
            {
                Game1.Gui.AddGuiTicker(EquipableItem.IngredientList.NewSong, 1);
                Game1.Globals.Learned_Songs.Add(newSong);
            }
        }
    }
}
