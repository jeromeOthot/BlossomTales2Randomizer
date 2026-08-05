using System.Reflection;
using Microsoft.Xna.Framework;

namespace BlossomTales2.Randomizer.mm
{
    internal static class PlayerExtensions
    {
        public static void GiveItemReflection(this Player player, EquipableItem.ItemList item, bool playAnimation = true)
        {
            MethodInfo info = Game1.player.GetType().GetMethod("GiveItem", BindingFlags.NonPublic | BindingFlags.Instance);
            info.Invoke(Game1.player, new object[] { item, playAnimation });
        }

        public static void GiveIngredientReflection(this Player player, EquipableItem.IngredientList ingred, int amount = 1, bool playAnimation = false)
        {
            MethodInfo info = Game1.player.GetType().GetMethod("GiveItem", BindingFlags.NonPublic | BindingFlags.Instance);
            info.Invoke(Game1.player, new object[] { ingred, amount, playAnimation });
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

                int startIndexSong = 56;
                int itemIndex = (int)newSong;
                Game1.playSoundCue("newWeapon");
                Game1.playSoundCue("blank098");
                GameLogger.LogInfo("Play animation item: " + itemIndex);
                Game1.Particles.Add((Particle)new P_GetItem(Game1.player.Position + new Vector3(0.0f, 100f, 0.0f), itemIndex + startIndexSong));
                Game1.Particles.Add((Particle)new GetItemLight(Game1.player.Position));
            }
        }
    }
}
