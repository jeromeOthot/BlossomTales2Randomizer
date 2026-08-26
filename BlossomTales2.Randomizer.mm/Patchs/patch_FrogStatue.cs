// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System;
using BlossomTales2.Randomizer.mm;
using Microsoft.Xna.Framework;

namespace BlossomTales2
{
    public class patch_FrogStatue : FrogStatue
    {
        private bool inProgress;

        public patch_FrogStatue(Vector3 position) : base(position) {}

        public extern void orig_Update(GameTime gameTime);

        //TODO: Réduire la méthode en gardant que les changements
        public override void Update(GameTime gameTime)
        {
            this.tweener.Update((float) gameTime.ElapsedGameTime.TotalSeconds * Game1.TimeDelta);
            if (this.Frame != 0 || Game1.player.ghostTimer >= 1 || Game1.player.RemovePlayerControls || this.inProgress || Game1.player.Rolling)
              return;
            Rectangle rectangle = this.GetRectangle();
            rectangle.X -= 8;
            rectangle.Y += 16 /*0x10*/;
            rectangle.Width += 16 /*0x10*/;
            rectangle.Height += 12;
            if (!rectangle.Intersects(Game1.player.GetRectangle()) || !Game1.player.IsFacing(this.Position + new Vector3(0.0f, 0.0f, 16f), 0.2f))
              return;
            int reqIng = 0;
            if (this.IDNumber == 0)
              reqIng = 14;
            else if (this.IDNumber == 1)
              reqIng = 20;
            else if (this.IDNumber == 2)
              reqIng = 8;
            else if (this.IDNumber == 3)
              reqIng = 24;
            if (Game1.player.Items_Count[reqIng] <= 0)
              return;
            Game1.player.ShowOpenButton = true;
            if (!Input.A_Button_Pressed())
              return;
            PickUpableGeneric pickUpableGeneric = new PickUpableGeneric(Game1.player.Position);
            pickUpableGeneric.Position = new Vector3((float) (int) Game1.player.Position.X, 48f, (float) (int) Game1.player.Position.Z);
            if (this.IDNumber == 0)
              pickUpableGeneric.Row = 51;
            else if (this.IDNumber == 1)
              pickUpableGeneric.Row = 53;
            else if (this.IDNumber == 2)
              pickUpableGeneric.Row = 50;
            else if (this.IDNumber == 3)
              pickUpableGeneric.Row = 52;
            pickUpableGeneric.PickedUp = true;
            pickUpableGeneric.CollWithPlayer = false;
            pickUpableGeneric.Collidable = false;
            Game1.player.PickedUpObject = true;
            Game1.player.PickedObject = (LevelObject) pickUpableGeneric;
            Game1.player.CurrentAnimation = Player.Animations.PickUp_Side;
            if (Game1.player.Direction == 1)
              Game1.player.CurrentAnimation = Player.Animations.PickUp_Up;
            if (Game1.player.Direction == 3)
              Game1.player.CurrentAnimation = Player.Animations.PickUp_Down;
            Game1.player.Frame = 2;
            Game1.player.Timer = -100;
            Game1.CurrentLevel.LevelObjects.Add((LevelObject) pickUpableGeneric);
            Game1.player.PuttingDown = true;
            Game1.player.RemovePlayerControls = true;
            this.inProgress = true;
            if (Game1.player.Direction == 1)
            {
              if ((double) Game1.player.Position.X > (double) this.Position.X + 16.0)
                Game1.player.DamageVelocity.X = -3f;
              if ((double) Game1.player.Position.X < (double) this.Position.X - 16.0)
                Game1.player.DamageVelocity.X = 3f;
            }
            if (Game1.player.Direction == 2)
            {
              if ((double) Game1.player.Position.X > (double) this.Position.X - 24.0)
                Game1.player.DamageVelocity.X = -3f;
              if ((double) Game1.player.Position.Z > (double) this.Position.Z + 48.0)
                Game1.player.DamageVelocity.Z = -4f;
            }
            if (Game1.player.Direction == 4)
            {
              if ((double) Game1.player.Position.X < (double) this.Position.X + 24.0)
                Game1.player.DamageVelocity.X = 3f;
              if ((double) Game1.player.Position.Z > (double) this.Position.Z + 48.0)
                Game1.player.DamageVelocity.Z = -4f;
            }
            this.tweener.Timer(0.5f).OnComplete((Action) (() =>
            {
              this.Frame = 1;
              Game1.player.RemoveIngredientReflection((EquipableItem.IngredientList) reqIng);
              Game1.Perma_Objects.Add(new PermaListItem(Game1.LevelName, this.Name, this.Position));
              Game1.playSoundCue("pickUp");
              this.bounce(4.2f, 0.2f, 1);
              Game1.makeParticleExplosion_PerfectCircle(this.Position, Color.White, 34, 10);
              Game1.makeLightOrb(this.Position, 5, 0.45f, startScale: 0.0f);
              if (this.IDNumber == 0)
              {
                    ++Game1.Globals.DonatedOranges;
                    Game1.Gui.AddGuiTicker(EquipableItem.IngredientList.FrogStatue, 1);
                    Game1.Dialoger.AddLine($"donate oranges: {Game1.Globals.DonatedOranges}");
                    if (Game1.Globals.DonatedOranges == 6)
                        //Changement
                        RandomizerSingleton.Instance.GiveSideQuestReward("frog_statue_award");
              }
              else if (this.IDNumber == 1)
              {
                ++Game1.Globals.DonatedMelons;
                Game1.Gui.AddGuiTicker(EquipableItem.IngredientList.BunnyStatue, 1);
                if (Game1.LevelName == "overworld-24x16.tmx")
                  Game1.Perma_Objects.Add(new PermaListItem("overworld-24x16-party.tmx", this.Name, this.Position));
                if (Game1.Globals.DonatedMelons == 6)
                    //Changement
                    RandomizerSingleton.Instance.GiveSideQuestReward("bunny_statue_award");
              }
              else if (this.IDNumber == 2)
              {
                ++Game1.Globals.DonatedApples;
                Game1.Gui.AddGuiTicker(EquipableItem.IngredientList.ChipmunkStatue, 1);
                if (Game1.Globals.DonatedApples == 6)
                    //Changement
                    RandomizerSingleton.Instance.GiveSideQuestReward("chipmunk_statue_award");
              }
              else if (this.IDNumber == 3)
              {
                ++Game1.Globals.DonatedJojobas;
                Game1.Gui.AddGuiTicker(EquipableItem.IngredientList.LizardStatue, 1);
                if (Game1.Globals.DonatedJojobas == 6)
                    //Changement
                    RandomizerSingleton.Instance.GiveSideQuestReward("lizard_statue_award");
              }
              if (Game1.Globals.DonatedOranges <= 5 || Game1.Globals.DonatedMelons <= 5 || Game1.Globals.DonatedApples <= 5 || Game1.Globals.DonatedJojobas <= 5)
                return;
              Game1.Achievementer.CheckAchievment(18);
            }));
        }
    }
}
