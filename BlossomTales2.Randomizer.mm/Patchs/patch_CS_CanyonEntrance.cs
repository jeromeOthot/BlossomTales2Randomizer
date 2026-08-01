namespace BlossomTales2
{
    internal class patch_CS_CanyonEntrance : CS_CanyonEntrance
    {
        public extern void orig_goOwl();

        public void goOwl()
        {
            if(ModGlobals.OpenWorldState)
                return;
            else
                orig_goOwl();
        }
    }
}
