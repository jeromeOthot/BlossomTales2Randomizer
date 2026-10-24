using BlossomTales2.Randomizer.mm;

namespace BlossomTales2
{
    internal class patch_CS_LoadCutscene : CS_LoadCutscene
    {
        public extern void orig_Init();

        public override void Init()
        {
            if(RandomizerSettings.GamePlay.SkipCutscenes)
                endCutScene();
            else
                orig_Init();
        }
    }
}
