using Microsoft.Xna.Framework.Audio;
using MonoMod;
#nullable disable

namespace BlossomTales2
{
    class patch_Game1 : Game1
    {
        public static Dictionary<string, EquipableItem.ItemList> LootSpots;
                
        public extern void orig_Initialize();
        protected override void Initialize()
        {
            LootSpots = new Dictionary<string, EquipableItem.ItemList>
                {
                    { "blossom-house2.tmx:11:5", EquipableItem.ItemList.Guitar},
                    { "grandma_1", EquipableItem.ItemList.Guitar},
                };
            orig_Initialize();
        }
    }
}