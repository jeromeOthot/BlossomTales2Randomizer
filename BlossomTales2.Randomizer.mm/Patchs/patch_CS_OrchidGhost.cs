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

        public extern void orig_howDareYou();
        public extern void orig_talkOrchid();
        public extern void orig_giveHeart();
        public extern void orig_openTomb();

        public void howDareYou()
        {
            if (ModGlobals.SkipCutscenes)
                SwitchWithBoss();
            else
                orig_howDareYou();
        }

        public void talkOrchid()
        {
            if (ModGlobals.SkipCutscenes)
                giveHeart();
            else
                orig_talkOrchid();
        }

        public void giveHeart()
        {
            Game1.Gui.HideHud = false;
            GameLogger.LogInfo(Game1.CurrentLevel.Name + " " + orchid.name + " " + orchid.getPosition());
            Vector3 positionOffset = orchid.getPosition();
            positionOffset.Y = 0f; //King is floating and it screws the LocationId.
            EquipableItem.ItemList item = RandomizerSingleton.Instance.GetItemAtLocation(new LocationId(Game1.CurrentLevel.Name, orchid.name + "_heart", positionOffset));
            Game1.player.GiveItemReflection(item);
            if (ModGlobals.SkipCutscenes)
            {
                tweener.Timer(3f).OnComplete(delegate
                {
                    Game1.Gui.HideHud = true;
                    openTomb();
                });
            }
            else
            {
                giveHeartWithCutscene();
            }
        }

        private void giveHeartWithCutscene()
        {
            tweener.Timer(3f).OnComplete(delegate
            {
                Game1.Gui.HideHud = true;
                Game1.Dialoger.AddLine("<E>Old King: Thou fightest with valor and ambition not seen since my mortal days.");
                Game1.Dialoger.AddLine("<E>Old King: But thou art strangely dressed... for a knight.");
                Game1.Dialoger.AddLine("<E>Old King: What is thy purpose for coming here, young one?");
                Game1.Dialoger.AddLine("Lily: I need help defeating the <A>Minotaur <A>King. He took my brother!");
                Game1.Dialoger.AddLine("<E>Old King: Ah, the <A>Minotaur <A>King! I recall hearing stories about him when I was but a young lad.");
                Game1.Dialoger.AddLine("<E>Old King: If the fables are true, the <A>Minotaur <A>King took <D>Chrys to a castle hidden within a labyrinth.");
                Game1.Dialoger.AddLine("Lily: I'm not afraid! I'll rescue him, no matter where he is.");
                Game1.Dialoger.AddLine("<E>Old King: Thy courage may ring true, but none can embark on such a journey wielding wooden weaponry.", openTomb);
            });
        }

        public void openTomb()
        {
            if(!ModGlobals.SkipCutscenes)
            {
                orig_openTomb();
                return;
            }

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

        public void takeSword()
        {
            Game1.Achievementer.CheckAchievment(1);
            orchidTomb.play("noSword");
            Vector3 position = orchid.getPosition();
            position.Y = 0;
            EquipableItem.ItemList item = RandomizerSingleton.Instance.GetItemAtLocation(new LocationId(Game1.CurrentLevel.Name, orchid.name + "_sword", position));
            Game1.player.GiveItemReflection(item);
            tweener.Timer(3f).OnComplete(delegate
            {
                orchidTomb.play("empty");
                item = RandomizerSingleton.Instance.GetItemAtLocation(new LocationId(Game1.CurrentLevel.Name, orchid.name + "_shield", position));
                Game1.player.GiveItemReflection(item);
                tweener.Timer(3f).OnComplete(delegate
                {
                    if (ModGlobals.SkipCutscenes)
                        fadeOut(); //TODO: Skip la cutscene du minotaure et sortir du donjon.
                    else
                        orchidMorkla();
                });
            });
        }

        private void Mod_SkipOpenTombCutscene()
        {
            takeSword();
        }
    }
}
