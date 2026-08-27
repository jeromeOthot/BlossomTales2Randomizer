// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System;
using BlossomTales2.Randomizer.mm;
using Microsoft.Xna.Framework;

namespace BlossomTales2
{
    public class patch_Chest_RandomRoll : Chest_RandomRoll
    {
        public patch_Chest_RandomRoll(Vector3 position) : base(position)
        {
        }

        private float shadowScale;
        private float shadowAlpha;
        private bool hasHit;
        private int killOthersTimer;

        public extern void orig_Update(GameTime gameTime);

        public override void Update(GameTime gameTime)
        {

            if (Game1.player.Direction == 1 && this.Frame == 0 && (double)this.Position.Y < 10.0 &&
                new Rectangle((int)Game1.player.Position.X - 4,
                        (int)Game1.player.Position.Z - (int)Game1.player.Size.Z * 2 - 4, 8, 6)
                    .Intersects(this.GetRectangle()))
            {
                Game1.player.ShowOpenButton = true;
                if (BlossomTales2.Input.A_Button_Pressed())
                {
                    this.Frame = 1;
                    Game1.player.LockPosition = true;
                    Game1.player.LockDirection = true;
                    Game1.player.CurrentAnimation = Player.Animations.UseItem;
                    Game1.player.Frame = 0;
                    Game1.player.Timer = 0;
                }
            }

            TimeSpan elapsedGameTime;
            if (this.Frame > 0 && this.Frame < 3)
            {
                int timer = this.Timer;
                elapsedGameTime = gameTime.ElapsedGameTime;
                int milliseconds = elapsedGameTime.Milliseconds;
                this.Timer = timer + milliseconds;
                if (this.Timer > 80 /*0x50*/)
                {
                    this.Timer = 0;
                    ++this.Frame;
                    if (this.Frame == 2)
                        Game1.playSoundCue("chestOpen");
                    if (this.Frame == 3)
                    {
                        Game1.player.LockPosition = false;
                        Game1.player.LockDirection = false;
                        if (this.IDNumber == 1)
                        {
                            RandomizerSingleton.Instance.GiveItemAtLocation("Chest_price_1", Vector3.Zero);
                            ++Game1.Globals.RandomRoll_Interaction;
                        }
                        else if (this.IDNumber == 2)
                        {
                            RandomizerSingleton.Instance.GiveItemAtLocation( "Chest_price_2", Vector3.Zero);
                            ++Game1.Globals.RandomRoll_Interaction;
                        }
                        else if (this.IDNumber == 3)
                        {
                            RandomizerSingleton.Instance.GiveItemAtLocation("Chest_price_3", Vector3.Zero);
                            ++Game1.Globals.RandomRoll_Interaction;
                        }

                        if (Game1.Globals.RandomRoll_Interaction > 3)
                            Game1.Globals.RandomRoll_Interaction = 99;
                        this.killOthersTimer = 1;
                    }
                }
            }

            if (this.killOthersTimer > 0)
            {
                int killOthersTimer = this.killOthersTimer;
                elapsedGameTime = gameTime.ElapsedGameTime;
                int milliseconds = elapsedGameTime.Milliseconds;
                this.killOthersTimer = killOthersTimer + milliseconds;
            }

            if (this.killOthersTimer > 100)
            {
                this.killOthersTimer = 0;
                for (int index = 0; index < Game1.CurrentLevel.LevelObjects.Count; ++index)
                {
                    if (Game1.CurrentLevel.LevelObjects[index] is Chest_RandomRoll &&
                        Game1.CurrentLevel.LevelObjects[index] != this)
                    {
                        Game1.CurrentLevel.LevelObjects[index].Frame = 3;
                        int frame = 0;
                        if (Game1.CurrentLevel.LevelObjects[index].IDNumber == 3)
                            frame = 37;
                        else if (Game1.CurrentLevel.LevelObjects[index].IDNumber == 4)
                            frame = 26;
                        else if (Game1.CurrentLevel.LevelObjects[index].IDNumber == 5)
                            frame = 28;
                        else if (Game1.CurrentLevel.LevelObjects[index].IDNumber == 6)
                            frame = 0;
                        if (frame != 0)
                            Game1.Particles.Add(
                                (Particle)new P_RemoveItem_2(Game1.CurrentLevel.LevelObjects[index].Position, frame));
                    }
                }
            }

            if ((double)this.Position.Y > 0.0)
            {
                if ((double)this.Velocity.Y > -20.0)
                    this.Velocity.Y -= 0.5f;
                this.Position.Y += this.Velocity.Y;
                if ((double)this.Position.Y < 1.0)
                {
                    this.Position.Y = 0.0f;
                    if (!this.hasHit)
                    {
                        Game1.Particles.Add((Particle)new Shockwave(this.Position));
                        Game1.Camera.Shake(8f, 0.96f);
                        this.hasHit = true;
                    }

                    if ((double)this.Velocity.Y < -5.0)
                    {
                        this.Velocity.Y *= -0.5f;
                        this.Position.Y = 1f;
                    }
                    else
                    {
                        this.Position.Y = 0.0f;
                        this.Velocity.Y = 0.0f;
                    }
                }
            }

            this.shadowAlpha = (float)(0.30000001192092896 - (double)this.Position.Y / 500.0);
            this.shadowScale = (float)(4.0 - (double)this.Position.Y / 90.0);
        }
    }
}
