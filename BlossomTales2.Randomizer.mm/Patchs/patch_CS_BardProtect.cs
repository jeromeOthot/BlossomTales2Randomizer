using BlossomTales2.Extensions;
using BlossomTales2.Randomizer.mm;
using Microsoft.Xna.Framework;

namespace BlossomTales2
{
    public class patch_CS_BardProtect : CS_BardProtect
    {
        private Puppet bard;
        private bool showSheet;
        private bool shownotes;

        public override void Init()
        {
            if (!Mod_HasItem())
            {
                bard = new Puppet("bard", new Vector3(1412f, 0f, 1628f));
                bard.play("playHarpForever");
                puppets.Add(bard);
                puppetList.Add(bard);
                startNotes();
            }
            else
            {
                if (Game1.EDITING_MODE)
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
        }

        public void learntLesson()
        {
            Mod_GiveSong();
            Game1.Globals.BardProtect_State = 10;
            showSheet = false;
            Game1.player.StopUpdating = false;
            Game1.player.RemovePlayerControls = true;
            FORBARD = true;
            Game1.player.MusicSuccessful = 2;
            Game1.player.SongTimer = 12500;
            Game1.player.SongStartWait = 500;
            tweener.Timer(0.5f).OnComplete(delegate
            {
                bard.play("playHarpForever");
                shownotes = true;
            });
            tweener.Timer(11.5f).OnComplete(delegate
            {
                bard.play("holdHarp");
            });
            tweener.Timer(12f).OnComplete(delegate
            {
                bard.play("putHarpAway");
                shownotes = false;
                BalloonStand balloonStand = new BalloonStand(bard.getPosition() + new Vector3(0f, 0f, 0f))
                {
                    SaveToMap = false,
                    hideStand = true
                };
                Game1.CurrentLevel.LevelObjects.Add(balloonStand);
                balloonStand.CallBalloonForBard();
            });
        }

        private bool Mod_HasItem()
        {
            return Game1Extensions.HasLevelPermaObject("bard_song");
        }

        private void Mod_GiveSong()
        {
            string location = bard.name + "_song";
            RandomizerSingleton.Instance.GiveItemAtLocation(location, Vector3.Zero);
            Game1Extensions.AddLevelPermaObject(location, Vector3.Zero);
        }
    }
}
