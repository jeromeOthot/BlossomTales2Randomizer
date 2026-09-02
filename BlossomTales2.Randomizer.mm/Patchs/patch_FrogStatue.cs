using BlossomTales2.Randomizer.mm;
using Microsoft.Xna.Framework;
using MonoMod;

namespace BlossomTales2
{
    public class patch_FrogStatue : FrogStatue
    {
        public patch_FrogStatue(Vector3 position) : base(position) {}

        private string Mod_GetStatueEventFlag(int eventId)
        {
            return "giveStatueItem_" +  eventId;
        }

        [MonoModPatch("<>c__DisplayClass2_0")]
        class patch_UpdateLambda
        {
            private int reqIng;
            [MonoModPatch("<>4__this")]
            private  patch_FrogStatue _this;

            [MonoModPatch("<Update>b__0")]
            public void OnCompleteLambda()
            {
                _this.Frame = 1;
                Game1.player.RemoveIngredientReflection((EquipableItem.IngredientList)reqIng);
                Game1.Perma_Objects.Add(new PermaListItem(Game1.LevelName, _this.Name, _this.Position));
                Game1.playSoundCue("pickUp");
                _this.bounce(4.2f, 0.2f, 1);
                Game1.makeParticleExplosion_PerfectCircle(_this.Position, Color.White, 34, 10);
                Game1.makeLightOrb(_this.Position, 5, 0.45f, 0f, 0f);
                if (_this.IDNumber == 0)
                {
                    Game1.Globals.DonatedOranges++;
                    Game1.Gui.AddGuiTicker(EquipableItem.IngredientList.FrogStatue, 1);
                    if (Game1.Globals.DonatedOranges == 6)
                    {
                        Game1.Narrator.AddLine("Grandpa: The Frog God smiles upon you.", 12, _this.Mod_GetStatueEventFlag(_this.IDNumber));
                    }
                }
                else if (_this.IDNumber == 1)
                {
                    Game1.Globals.DonatedMelons++;
                    Game1.Gui.AddGuiTicker(EquipableItem.IngredientList.BunnyStatue, 1);
                    if (Game1.LevelName == "overworld-24x16.tmx")
                    {
                        Game1.Perma_Objects.Add(new PermaListItem("overworld-24x16-party.tmx", _this.Name, _this.Position));
                    }
                    if (Game1.Globals.DonatedMelons == 6)
                    {
                        Game1.Narrator.AddLine("Grandpa: The Bunny God smiles upon you.", 12, _this.Mod_GetStatueEventFlag(_this.IDNumber));
                    }
                }
                else if (_this.IDNumber == 2)
                {
                    Game1.Globals.DonatedApples++;
                    Game1.Gui.AddGuiTicker(EquipableItem.IngredientList.ChipmunkStatue, 1);
                    if (Game1.Globals.DonatedApples == 6)
                    {
                        Game1.Narrator.AddLine("Grandpa: The Chipmunk God smiles upon you.", 12, _this.Mod_GetStatueEventFlag(_this.IDNumber));
                    }
                }
                else if (_this.IDNumber == 3)
                {
                    Game1.Globals.DonatedJojobas++;
                    Game1.Gui.AddGuiTicker(EquipableItem.IngredientList.LizardStatue, 1);
                    if (Game1.Globals.DonatedJojobas == 6)
                    {
                        Game1.Narrator.AddLine("Grandpa: The Lizard God smiles upon you.", 12, _this.Mod_GetStatueEventFlag(_this.IDNumber));
                    }
                }
                if (Game1.Globals.DonatedOranges > 5 && Game1.Globals.DonatedMelons > 5 && Game1.Globals.DonatedApples > 5 && Game1.Globals.DonatedJojobas > 5)
                {
                    Game1.Achievementer.CheckAchievment(18);
                }
            }
        }
    }
}


