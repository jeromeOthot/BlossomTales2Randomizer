using System.Collections.Generic;
using BlossomTales2.Extensions;
using BlossomTales2.Randomizer.mm;
using Microsoft.Xna.Framework;

namespace BlossomTales2
{

    internal class patch_CS_CanyonBard : CS_CanyonBard
    {
        private Puppet cage;
        private Puppet bard;
        private bool showSheet;
        private bool shownotes;

        public extern void orig_Init();
        public extern void orig_giveGuitar();
        public extern void orig_giveAccordion();
        public extern void orig_startTheSong();
        public extern void orig_goPlayer();

        public override void Init()
        {
            if (Mod_HasNotSavedBard())
            {
                cage = new Puppet("cage", new Vector3(720f, 0f, 1288f));
                cage.Zdepth -= 20f;
                bard = new Puppet("bard", new Vector3(724f, 0f, 1260f));
                puppets.AddRange(new List<Puppet> { cage, bard });
                puppetList.AddRange(new List<Puppet> { cage, bard });
                AnimTile animTile = AnimationsTypes.ReturnTile("canyonlands/cageBackbar-22x16x1");
                animTile.position = new Vector2(680f, 1184f);
                Game1.CurrentLevel.Animations.Add(animTile);
                CollisionRect collisionRect = new CollisionRect(new Vector3(672f, 0f, 1216f));
                collisionRect.Size = new Vector3(26f, 0f, 18f);
                Game1.CurrentLevel.LevelObjects.Add(collisionRect);
            }

            foreach (LevelObject levelObject in Game1.CurrentLevel.LevelObjects)
            {
                if (levelObject is RaisingWall)
                {
                    levelObject.Size.Y = 0f;
                }
            }
        }

        public void giveAccordion()
        {
            tweener.Timer(2f).OnComplete(lessonPre);
            Mod_GiveItem();
        }

        public void giveGuitar()
        {
            tweener.Timer(2f).OnComplete(lessonPre);
            Mod_GiveItem();
        }

        public void startTheSong()
        {
            Mod_GiveSong();

            showSheet = false;
            Game1.player.StopUpdating = false;
            Game1.player.RemovePlayerControls = true;
            Game1.player.MusicSuccessful = 1;
            Game1.player.SongTimer = 10000;
            Game1.player.SongStartWait = 500;
            tweener.Timer(0.5f).OnComplete(delegate
            {
                bard.play("playHarpForever");
                shownotes = true;
            });
            tweener.Timer(9f).OnComplete(delegate
            {
                bard.play("holdHarp");
                shownotes = false;
            });
            tweener.Timer(10f).OnComplete(focusMusicDoor2);
        }

        public void goPlayer()
        {
            Globaler.MainGameObjective mainGameObjective = Game1.Globals.MainQuestObjective;
            orig_goPlayer();

            if (ModGlobals.OpenWorldState)
            {
                Game1Extensions.MarkObjectiveComplete(Globaler.MainGameObjective.canyons_headToBard);
                Game1.Globals.MainQuestObjective = mainGameObjective;
            }
        }

        private bool Mod_HasNotSavedBard()
        {
            if (ModGlobals.OpenWorldState)
                return !Game1Extensions.IsObjectiveCompleted(Globaler.MainGameObjective.canyons_headToBard);
            else
                return Game1.Globals.MainQuestObjective <= Globaler.MainGameObjective.canyons_headToBard;
        }

        private void Mod_GiveItem()
        {
            GameLogger.LogInfo(Game1.CurrentLevel.Name + " " + bard.name + " " + bard.getPosition());
            EquipableItem.ItemList item = RandomizerSingleton.Instance.GetItemAtLocation(new LocationId(Game1.CurrentLevel.Name, bard.name, bard.getPosition()));
            Game1.player.GiveItemReflection(item);
        }

        private void Mod_GiveSong()
        {
            if (!Game1.Globals.Learned_Songs.Contains(Globaler.Songs.OpenSesame))
            {
                Game1.Gui.AddGuiTicker(EquipableItem.IngredientList.NewSong, 1);
                Game1.Globals.Learned_Songs.Add(Globaler.Songs.OpenSesame);
            }
        }
    }
}
