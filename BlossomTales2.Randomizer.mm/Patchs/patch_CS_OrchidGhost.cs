using BlossomTales2.Randomizer.mm;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace BlossomTales2
{
    internal class patch_CS_OrchidGhost : CS_OrchidGhost
    {
        private Puppet orchid = new Puppet ("Fake king", Vector3.Zero);
        private Puppet orchidLight = new Puppet ("Fake light", Vector3.Zero);
        private Puppet orchidTomb = new Puppet("Fake tomb", Vector3.Zero);

        public extern void orig_giveHeart();

        public void howDareYou()
        {
            SwitchWithBoss();
        }

        public void talkOrchid()
        {
            giveHeart();
        }

        public void giveHeart()
        {
            GameLogger.LogInfo(Game1.CurrentLevel.Name + " " + orchid.name + " " + orchid.getPosition());
            Vector3 positionOffset = orchid.getPosition();
            positionOffset.Y = 0f; //King is floating and it screws the LocationId.
            EquipableItem.ItemList item = RandomizerSingleton.Instance.GetItemAtLocation(new LocationId(Game1.CurrentLevel.Name, orchid.name + "_heart", positionOffset));
            Game1.player.GiveItemReflection(item);
            tweener.Timer(3f).OnComplete(openTomb);
        }

        public void openTomb()
        {
            tweener.Timer(1f).OnComplete(delegate
            {
                Game1.Camera.Shake(16f, 0.96f);
                orchidTomb.play("open");
                Game1.playSoundCue("unlock_4");
                Game1.SControl.playSounds(new List<string> { "blank103", "blank103" }, new List<int> { 200, 200 });
                Game1.Particles.Add(new Shockwave(orchid.getPosition(), 0f, 12));
                Game1.makeLightOrb(orchidTomb.getPosition(), 30, 1f);
                tweener.Timer(1f).OnComplete(delegate
                {
                    Mod_SkipOpenTombCutscene();
                });
            });
        }

        private void Mod_SkipOpenTombCutscene()
        {
            takeSword();
        }
    }
}
