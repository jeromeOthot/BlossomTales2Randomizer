using BlossomTales2.Extensions;
using BlossomTales2.Randomizer.mm;
using Microsoft.Xna.Framework;

namespace BlossomTales2
{
    public class patch_CS_SleepyTavern : CS_SleepyTavern
    {
        private Puppet bard;
        private bool showSheet;
        private bool shownotes;

        public void learntLesson()
        {
            Mod_GiveSong();
            showSheet = false;
            Game1.player.StopUpdating = false;
            Game1.player.RemovePlayerControls = true;
            Game1.player.MusicSuccessful = 5;
            Game1.player.SongTimer = 10000;
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
            });
            tweener.Timer(7f).OnComplete(finishBardDialog);
        }

        private void Mod_GiveSong()
        {
            string location = bard.name + "_song";
            RandomizerSingleton.Instance.GiveItemAtLocation(location, Vector3.Zero);
            Game1Extensions.AddLevelPermaObject(location, Vector3.Zero);
        }
    }
}
