using BlossomTales2.Randomizer.mm;

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

        //TODO: Trouver une façon de caller la vraie fonction. Game1.RandomFloat 
        public static float RandomFloat(int a, int b, float divisor)
        {
            if (a == 0 && b == 0)
            {
                return 0f;
            }

            return (float)RandomNumber.Next(a, b) / divisor;
        }
    }
}