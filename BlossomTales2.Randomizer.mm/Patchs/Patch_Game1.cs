using BlossomTales2.Randomizer.mm;
#nullable disable

namespace BlossomTales2
{
    class patch_Game1 : Game1
    {           
        public extern void orig_Initialize();
        protected override void Initialize()
        {
            RandomizerSingleton.Initialize();
            GameLogger.LogInfo("Hello world");
            orig_Initialize();
        }
    }
}