using Microsoft.Xna.Framework.Audio;
using MonoMod;
#nullable disable

namespace BlossomTales2
{
    class patch_Game1 : Game1
    {
       // private AudioEngine engine = new AudioEngine("Content/Audio/SoundBank.xsb");
       // private WaveBank waveBank = null;

        public Dictionary<string, EquipableItem.ItemList> LootSpots;

        public patch_Game1()
        {
            //On initialise les LootSpots avec un randomizer
            LootSpots = new Dictionary<string, EquipableItem.ItemList> { { "blossom-house2.tmx:11:5", EquipableItem.ItemList.Guitar }};
        }
        
        //public extern void orig_Initialize();
        
        /*
        protected override void Initialize()
        {
            LootSpots = new Dictionary<string, EquipableItem.ItemList> { { "blossom-house2.tmx:11:5", EquipableItem.ItemList.Guitar }};
            Game1.Camera = new Camera2D((float) Game1.graphics.PreferredBackBufferWidth, (float) Game1.graphics.PreferredBackBufferHeight);
            this.engine = new AudioEngine("Content/Audio/TheSounds.xgs");
            Game1.soundBank = new SoundBank(this.engine, "Content/Audio/Sound Bank.xsb");
            this.waveBank = new WaveBank(this.engine, "Content/Audio/Wave Bank.xwb");
            Game1.CurrentMusic = Game1.soundBank.GetCue("BT2_Title");
            Game1.CurrentMusic.Play();
            Game1.CurrentMusic.Pause();
            Game1.MusicCategory = this.engine.GetCategory("Music");
            Game1.SfxCategory = this.engine.GetCategory("Default");
            Game1.MenuCategory = this.engine.GetCategory("Menu");
            this.engine.Update();
            this.IsMouseVisible = false;
            base.Initialize();
        } */
    }
}