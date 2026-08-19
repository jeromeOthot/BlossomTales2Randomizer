using BlossomTales2.Extensions;
using BlossomTales2.Randomizer.mm;
using Microsoft.Xna.Framework;

namespace BlossomTales2
{
    public class patch_CS_HorseMud : CS_HorseMud
    {
        private Enemy horse;
        private Puppet horseP;
        private Puppet bard;
        private bool showSheet;
        private bool shownotes;
        private bool hasSong;

        public override void Init()
        {
            if (Game1.Globals.Learned_Songs.Contains(Globaler.Songs.CallHorse))
            {
                hasSong = true;
            }
            if (Game1.EDITING_MODE)
            {
                horse = new Horse(new Vector3(1768f, 0f, 1312f));
                Game1.CurrentLevel.Enemies.Add(horse);
                horseP = new Puppet("horseMud", new Vector3(1768f, 0f, 1312f));
                bard = new Puppet("bard", new Vector3(724f, 0f, 1260f));
                return;
            }
            if (Game1.Globals.Learned_Songs.Contains(Globaler.Songs.CallHorse))
            {
                foreach (LevelObject levelObject in Game1.CurrentLevel.LevelObjects)
                {
                    if (levelObject is NPC_2 && levelObject.IDNumber == 48)
                    {
                        levelObject.Alive = false;
                    }
                }
                return;
            }
            horse = new Horse(new Vector3(1768f, 0f, 1312f));
            Game1.CurrentLevel.Enemies.Add(horse);
            horseP = new Puppet("horseMud", new Vector3(1768f, 0f, 1312f));
            bard = new Puppet("bard", new Vector3(724f, 0f, 1260f));
        }

        public void learntLesson()
        {
            Mod_GiveSong();
            showSheet = false;
            Game1.player.StopUpdating = false;
            Game1.player.RemovePlayerControls = true;
            Game1.player.MusicSuccessful = 4;
            Game1.player.SongTimer = 7000;
            Game1.player.SongStartWait = 500;
            tweener.Timer(0.5f).OnComplete(delegate
            {
                bard.play("playHarpForever");
                shownotes = true;
            });
            tweener.Timer(6f).OnComplete(delegate
            {
                bard.play("holdHarp");
                shownotes = false;
                bool flag = false;
                foreach (LevelObject levelObject in Game1.CurrentLevel.LevelObjects)
                {
                    if (levelObject is PlayerHorse)
                    {
                        flag = true;
                        if (!Game1.CamRect.Contains((int)levelObject.Position.X, (int)levelObject.Position.Z))
                        {
                            levelObject.Position.Z = Game1.player.Position.Z - 32f;
                            if (levelObject.Position.X > Game1.Camera.Center.X)
                            {
                                levelObject.Position.X = Game1.Camera.Center.X + 700f;
                            }
                            else
                            {
                                levelObject.Position.X = Game1.Camera.Center.X - 700f;
                            }
                        }
                        ((PlayerHorse)levelObject).MoveToPlayer();
                        break;
                    }
                }
                if (!flag)
                {
                    Game1.CurrentLevel.LevelObjects.Add(new PlayerHorse(new Vector3(Game1.Camera.Center.X - 700f, 0f, Game1.player.Position.Z - 32f)));
                }
            });
            tweener.Timer(7f).OnComplete(finishBardDialog);
        }

        private bool Mod_HasItem()
        {
            return Game1Extensions.HasLevelPermaObject(bard.name + "_song");
        }

        private void Mod_GiveSong()
        {
            string location = bard.name + "_song";
            RandomizerSingleton.Instance.GiveItemAtLocation(location, Vector3.Zero);
            Game1Extensions.AddLevelPermaObject(location, Vector3.Zero);
        }
    }
}
