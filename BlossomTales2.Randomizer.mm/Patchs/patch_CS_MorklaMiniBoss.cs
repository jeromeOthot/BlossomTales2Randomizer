using BlossomTales2.Randomizer.mm;

namespace BlossomTales2
{
    public class patch_CS_MorklaMiniBoss : CS_MorklaMiniBoss
    {
        public extern void orig_initScene();

        public void initScene()
        {
            if (RandomizerSettings.GamePlay.SkipCutscenes)
                return;

            orig_initScene();
        }
    }
}
