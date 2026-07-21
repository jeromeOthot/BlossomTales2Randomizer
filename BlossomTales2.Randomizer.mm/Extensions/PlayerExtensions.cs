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
    }
}
