using BlossomTales2.Randomizer.mm;
using Microsoft.Xna.Framework;

namespace BlossomTales2
{
    internal class patch_CS_OrchidGhost : CS_OrchidGhost
    {
        private Puppet orchid = new Puppet ("Fake king", Vector3.Zero);
        private Puppet orchidLight = new Puppet ("Fake light", Vector3.Zero);

        public extern void orig_giveHeart();

        public void giveHeart()
        {
            GameLogger.LogInfo(Game1.CurrentLevel.Name + " " + orchid.name + " " + orchid.getPosition());
            Vector3 positionOffset = orchid.getPosition();
            positionOffset.Y = 0f; //King is floating and it screws the LocationId.
            EquipableItem.ItemList item = RandomizerSingleton.Instance.GetItemAtLocation(new LocationId(Game1.CurrentLevel.Name, orchid.name, positionOffset));
            Game1.player.GiveItemReflection(item);
            tweener.Timer(3f).OnComplete(openTomb);
        }
    }
}
