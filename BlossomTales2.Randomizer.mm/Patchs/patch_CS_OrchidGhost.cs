using BlossomTales2.Extensions;
using BlossomTales2.Randomizer.mm;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace BlossomTales2
{
    internal class patch_CS_OrchidGhost : CS_OrchidGhost
    {
        private Puppet orchid = new Puppet ("Fake king", Vector3.Zero);
        private Puppet orchidTomb = new Puppet("Fake tomb", Vector3.Zero);

        public extern void orig_Init();
        public extern void orig_howDareYou();
        public extern void orig_talkOrchid();
        public extern void orig_giveHeart();
        public extern void orig_openTomb();

        public override void Init()
        {
            orchidTomb = new Puppet("orchidTomb", new Vector3(608f, 0f, 284f));
            foreach (Light light in Game1.CurrentLevel.Lights)
            {
                if (light.position.X == 492f)
                {
                    light.opacity = 0f;
                    light.maxOpacity = 255;
                    light.minOpacity = 255;
                }
            }

            puppets.Add(orchidTomb);
            puppetList.Add(orchidTomb);
            Game1.CurrentLevel.LevelObjects.Add(new CollisionRect(new Vector3(1248f, 0f, 448f)));
            orchidTomb.play("closed");

            if (Mod_CanInteractWithTomb())
            {
                return;
            }

            foreach (LevelObject levelObject in Game1.CurrentLevel.LevelObjects)
            {
                if (levelObject is Sign)
                {
                    levelObject.Alive = false;
                }
            }
        }

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
            Mod_OrchidGiveItem("_heart");
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
            Mod_OrchidGiveItem("_sword");
            tweener.Timer(3f).OnComplete(delegate
            {
                orchidTomb.play("empty");
                Mod_OrchidGiveItem("_shield");
                tweener.Timer(3f).OnComplete(delegate
                {
                    Mod_SkipMorklaCutscene();
                });
            });
        }

        private bool Mod_CanInteractWithTomb()
        {
            return ModGlobals.OpenWorldState && !Game1Extensions.IsObjectiveCompleted(Globaler.MainGameObjective.intro_enterCatacombs)
                            || !ModGlobals.OpenWorldState && Game1.Globals.MainQuestObjective <= Globaler.MainGameObjective.intro_enterCatacombs;
        }

        private void Mod_OrchidGiveItem(string locationName)
        {
            Vector3 position = orchid.getPosition();
            GameLogger.LogInfo(Game1.CurrentLevel.Name + " " + orchid.name + " " + position);
            position.Y = 0;
            RandomizerSingleton.Instance.GiveItemAtLocation(orchid.name + locationName, position);
        }

        private void Mod_SkipOpenTombCutscene()
        {
            if (ModGlobals.SkipCutscenes)
            {
                takeSword();
            }
            else
            {
                Game1.playSoundCue("blank079");
                Game1.Dialoger.AddLine("<E>Old King: I hereby dub thee a true knight! Accept my gifts, and with them, my royal blessing.", takeSword);
            }
        }

        private void Mod_SkipMorklaCutscene()
        {
            Game1Extensions.MarkObjectiveComplete(Globaler.MainGameObjective.intro_enterCatacombs);
            if (ModGlobals.SkipCutscenes)
                fadeOut(); //TODO: Skip la cutscene du minotaure et sortir du donjon.
            else
                orchidMorkla();
        }
    }
}
